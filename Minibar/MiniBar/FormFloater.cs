using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.MiniBar.BusinessClasses;
using SettingsManager = NewBizWiz.MiniBar.BusinessClasses.SettingsManager;

namespace NewBizWiz.MiniBar
{
	public partial class FormFloater : Form
	{
		private readonly Timer _hideTimer;

		public FormFloater()
		{
			InitializeComponent();

			_hideTimer = new Timer();
			_hideTimer.Interval = 30;
			_hideTimer.Tick += _hideTimer_Tick;
			_hideTimer.Start();
		}

		private void buttonXMinibar_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonXSync_Click(object sender, EventArgs e)
		{
			SyncManager.RegularSynchronize();
		}

		private void _hideTimer_Tick(object sender, EventArgs e)
		{
			lock (AppManager.Locker)
			{
				bool visible = FormMain.Instance.RibbonVisible;

				visible = visible & !SettingsManager.Instance.VisiblePowerPoint;
				visible = visible & !SettingsManager.Instance.VisiblePowerPointMaximaized;

				if ((SettingsManager.Instance.HideAll || SettingsManager.Instance.HideSpecificProgram || SettingsManager.Instance.HideSpecificProgramMaximized) && visible)
				{
					try
					{
						Process activeProcess = AppManager.Instance.GetActiveProcess();
						visible = !activeProcess.MainWindowTitle.ToUpper().Contains(@"\\REMOTE") && !activeProcess.MainWindowTitle.ToUpper().Contains("POWERPOINT SLIDE SHOW");
						if (visible)
						{
							if (SettingsManager.Instance.HideAll)
								visible = !AppManager.Instance.IsProcessWindowMaximized(activeProcess) || activeProcess.ProcessName.ToLower().Contains("powerpnt");
							else if (SettingsManager.Instance.HideSpecificProgram || SettingsManager.Instance.HideSpecificProgramMaximized)
							{
								visible = !(SettingsManager.Instance.PrimaryApplications.Where(x => (activeProcess.ProcessName.ToUpper().Contains(x.ToUpper()))).Count() > 0);
								if (SettingsManager.Instance.HideSpecificProgramMaximized && !visible)
									visible = !AppManager.Instance.IsProcessWindowMaximized(activeProcess);
							}
						}
					}
					catch {}
				}
				if (!visible && (SettingsManager.Instance.VisiblePowerPoint || SettingsManager.Instance.VisiblePowerPointMaximaized))
				{
					try
					{
						Process activeProcess = AppManager.Instance.GetActiveProcess();
						if (activeProcess.ProcessName.ToLower().Contains("powerpnt"))
							if (!activeProcess.MainWindowTitle.ToUpper().Contains("POWERPOINT SLIDE SHOW"))
								visible = SettingsManager.Instance.VisiblePowerPointMaximaized ? AppManager.Instance.IsProcessWindowMaximized(activeProcess) || activeProcess.ProcessName.ToLower().Contains("minibar") : SettingsManager.Instance.VisiblePowerPoint;
						if (!visible && activeProcess.ProcessName.ToLower().Contains("minibar"))
						{
							Process process = Process.GetProcesses().Where(x => x.ProcessName.ToLower().Contains("powerpnt")).FirstOrDefault();
							if (process != null)
								if (!process.MainWindowTitle.ToUpper().Contains("POWERPOINT SLIDE SHOW"))
									visible = SettingsManager.Instance.VisiblePowerPointMaximaized ? AppManager.Instance.IsProcessWindowMaximized(process) : !WinAPIHelper.IsIconic(process.MainWindowHandle);
						}
					}
					catch {}
				}
				try
				{
					if (visible)
					{
						Opacity = 1;
						FormBorderStyle = FormBorderStyle.FixedToolWindow;
					}
					else
					{
						Opacity = 0;
						FormBorderStyle = FormBorderStyle.None;
					}
					Size size = visible ? new Size(175, 85) : new Size(0, 0);
					Size = size;
				}
				catch {}
			}
		}

		private void FormFloater_Shown(object sender, EventArgs e)
		{
			lock (AppManager.Locker)
			{
				RegistryHelper.ShowFloat = true;
			}
			RegistryHelper.MinibarHandle = Handle;
		}

		private void FormFloater_FormClosed(object sender, FormClosedEventArgs e)
		{
			RegistryHelper.MinibarHandle = FormMain.Instance.Handle;
			lock (AppManager.Locker)
			{
				RegistryHelper.ShowFloat = false;
				_hideTimer.Stop();
			}
		}

		private void FormFloater_LocationChanged(object sender, EventArgs e)
		{
			lock (AppManager.Locker)
			{
				SettingsManager.Instance.FloaterTop = Top;
				SettingsManager.Instance.FloaterLeft = Left;
				SettingsManager.Instance.SaveMinibarSettings();
			}
		}
	}
}