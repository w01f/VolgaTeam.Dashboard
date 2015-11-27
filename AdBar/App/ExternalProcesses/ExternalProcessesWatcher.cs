using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Asa.Bar.App.Configuration;
using Asa.Core.Interop;
using Timer = System.Windows.Forms.Timer;

namespace Asa.Bar.App.ExternalProcesses
{
	class ExternalProcessesWatcher
	{
		private const string TagType = "type";
		private const string TagName = "name";

		// With every event, the application will iterate with a specified delay checking for changes
		private const UInt16 Delay = 100;
		private const UInt16 Iterations = 10;

		private readonly List<ExternalProcessConfiguration> _processConfiguration = new List<ExternalProcessConfiguration>();

		private BarVsProcessStatus _newStatus;
		private BarVsProcessStatus _lastStatus;

		private readonly BackgroundWorker _checkProcess;
		private readonly Timer _checkTimer;

		private UInt16 _currentIteration;

		private WinAPIHelper.WinEventDelegate _handler;
		private IntPtr _hook;

		private readonly Dictionary<int, IntPtr> _processesAndMaximizedWindows = new Dictionary<int, IntPtr>();

		public event EventHandler<ProcessStatusEventArgs> OnStatusChanged;

		public ExternalProcessesWatcher()
		{
			_lastStatus = BarVsProcessStatus.NotOnTop;
			_newStatus = BarVsProcessStatus.Hidden;

			_checkProcess = new BackgroundWorker();
			_checkProcess.WorkerReportsProgress = true;
			_checkProcess.WorkerSupportsCancellation = true;
			_checkProcess.DoWork += OnCheckProcessDoWork;
			_checkProcess.RunWorkerCompleted += OnCheckProcessCompleted;
			_checkProcess.ProgressChanged += OnCheckProcessProgressChanged;

			_checkTimer = new Timer();
			_checkTimer.Enabled = false;
			_checkTimer.Tick += OnCheckTimerTick;
		}

		public void Load()
		{
			_checkTimer.Interval = AppManager.Instance.Settings.Config.CheckProcessesInterval;

			_processConfiguration.Clear();

			var r = new Regex(@"<Application[\s]*(?<" + TagType + ">.*?)>(?<" + TagName + ">.*?)</Application>",
				RegexOptions.IgnoreCase | RegexOptions.Multiline);

			foreach (Match match in r.Matches(
				File.ReadAllText(ResourceManager.Instance.WatchedProcessesFile.LocalPath, Encoding.Default)))
			{
				var name = match.Groups[TagName].Value;
				var processConfiguration = new ExternalProcessConfiguration();
				processConfiguration.Name = name.ToLower();
				var type = match.Groups[TagType].Value.ToLower();

				if (!String.IsNullOrEmpty(type))
				{
					if (type.StartsWith(TagType + "="))
					{
						switch (type.Split(new[] { '=' }, 2)[1].Replace("\"", ""))
						{
							case "1":
							case "active":
								processConfiguration.Behaviour = ExternalProcessBehaviour.HideIfIsActive;
								break;

							case "2":
							case "maximized":
								processConfiguration.Behaviour = ExternalProcessBehaviour.HideIfIsActiveAndMaximized;
								break;

							case "3":
							case "notontop":
								processConfiguration.Behaviour = ExternalProcessBehaviour.SetNotOnTopIfIsActive;
								break;

							case "4":
							case "running":
								processConfiguration.Behaviour = ExternalProcessBehaviour.HideIfProcessIsRunning;
								break;

							case "5":
							case "titlebar":
								processConfiguration.Behaviour = ExternalProcessBehaviour.HideIfTitlebarMatches;
								processConfiguration.Name = name; // Fix case
								break;
						}
					}
				}
				_processConfiguration.Add(processConfiguration);
			}
		}

		public void StartWatching()
		{
			_checkProcess.RunWorkerAsync();
			_handler = WinEventProc;
			_hook = WinAPIHelper.SetWinEventHook(3, 23, IntPtr.Zero, _handler, 0, 0, 0); // first args is EVENT_SYSTEM_FOREGROUND, last argument is WINEVENT_OUTOFCONTEXT
		}

		public void StopWatching()
		{
			if (_checkProcess.IsBusy)
				_checkProcess.CancelAsync();
			_checkTimer.Stop();
			WinAPIHelper.UnhookWinEvent(_hook);
		}

		private void WinEventProc(
			IntPtr hWinEventHook,
			uint eventType,
			IntPtr hwnd,
			int idObject,
			int idChild,
			uint dwEventThread,
			uint dwmsEventTime)
		{
			if (_checkProcess.IsBusy)
				_currentIteration = 0;
			else
				_checkProcess.RunWorkerAsync();
		}

		private bool IsMaximized(Process p)
		{
			IntPtr handle;
			if (_processesAndMaximizedWindows.ContainsKey(p.Id))
			{
				handle = _processesAndMaximizedWindows[p.Id];
			}
			else
			{
				_processesAndMaximizedWindows.Add(p.Id, p.MainWindowHandle);
				handle = p.MainWindowHandle;
			}

			var placement = new WindowPlacement();
			placement.length = Marshal.SizeOf(placement);

			if (WinAPIHelper.GetWindowPlacement(handle, ref placement))
			{
				if (placement.showCmd == 3) // WM_MAXIMIZED
				{
					_processesAndMaximizedWindows[p.Id] = handle;
					return true;
				}
			}
			else
				if (_processesAndMaximizedWindows[p.Id] != p.MainWindowHandle)
				{
					_processesAndMaximizedWindows[p.Id] = p.MainWindowHandle;
					return IsMaximized(p);
				}
				else
					return false;

			// Error retrieving the Window placement
			return false;
		}

		private void OnCheckProcessDoWork(object sender, DoWorkEventArgs e)
		{
			_checkTimer.Stop();
			_currentIteration = 0;
			_newStatus = BarVsProcessStatus.OnTop;

			if (Process.GetProcesses()
				.Any(process => _processConfiguration
					.Any(processConfiguration => processConfiguration.Behaviour == ExternalProcessBehaviour.HideIfProcessIsRunning &&
						String.Compare(processConfiguration.Name, process.ProcessName, StringComparison.OrdinalIgnoreCase) == 0)))
			{
				_newStatus = BarVsProcessStatus.Hidden;
				_checkProcess.ReportProgress(-1);
				return;
			}

			do
			{
				uint processId;
				var foregroundWindow = WinAPIHelper.GetForegroundWindow();

				WinAPIHelper.GetWindowThreadProcessId(foregroundWindow, out processId);
				var p = Process.GetProcessById((int)processId);
				var processName = p.ProcessName.ToLower();

				if (processName == "idle") continue; // Ignore Idle process
				_newStatus = BarVsProcessStatus.OnTop;

				foreach (var processConfiguration in _processConfiguration.Where(configuration => new[] { 
						ExternalProcessBehaviour.HideIfIsActive, 
						ExternalProcessBehaviour.HideIfIsActiveAndMaximized, 
						ExternalProcessBehaviour.SetNotOnTopIfIsActive, 
						ExternalProcessBehaviour.HideIfTitlebarMatches}
					.Contains(configuration.Behaviour)))
				{
					try
					{
						if (processConfiguration.Behaviour == ExternalProcessBehaviour.HideIfTitlebarMatches)
						{
							if (!p.MainWindowTitle.Contains(processConfiguration.Name)) continue;
							_newStatus = BarVsProcessStatus.Hidden;
							break;
						}

						if (String.Compare(processName, processConfiguration.Name, StringComparison.OrdinalIgnoreCase) != 0) continue;

						switch (processConfiguration.Behaviour)
						{
							case ExternalProcessBehaviour.HideIfIsActive:
								_newStatus = BarVsProcessStatus.Hidden;
								break;

							case ExternalProcessBehaviour.HideIfIsActiveAndMaximized:
								if (IsMaximized(p))
									_newStatus = BarVsProcessStatus.Hidden;
								break;

							case ExternalProcessBehaviour.SetNotOnTopIfIsActive:
								_newStatus = BarVsProcessStatus.NotOnTop;
								break;
						}
					}
					catch { }
				}

				if (_newStatus != _lastStatus)
				{
					_checkProcess.ReportProgress(-1);
					return;
				}
				Thread.Sleep(Delay);
			}
			while (_currentIteration++ < Iterations);
		}

		private void OnCheckProcessCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			_checkTimer.Start();
		}

		private void OnCheckProcessProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage != -1) return;
			if (OnStatusChanged != null)
				OnStatusChanged(this, new ProcessStatusEventArgs(_newStatus));
			_lastStatus = _newStatus;
		}

		private void OnCheckTimerTick(object sender, EventArgs e)
		{
			if (!_checkProcess.IsBusy)
				_checkProcess.RunWorkerAsync();
		}
	}
}
