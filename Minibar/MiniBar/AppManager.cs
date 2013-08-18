﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Win32;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.MiniBar.BusinessClasses;
using NewBizWiz.MiniBar.InteropClasses;
using SettingsManager = NewBizWiz.MiniBar.BusinessClasses.SettingsManager;

namespace NewBizWiz.MiniBar
{
	internal class AppManager
	{
		#region Delegates
		public delegate void SingleParamDelegate(object parameter);
		#endregion

		private static readonly AppManager _instance = new AppManager();

		private readonly Dictionary<int, IntPtr> processesAndMaximizedWindows = new Dictionary<int, IntPtr>();

		private AppManager()
		{
			SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
			HelpManager = new HelpManager(SettingsManager.Instance.HelpLinksPath);
		}

		public static object Locker { get; set; }

		public bool ShowHidden { get; set; }
		public bool ShowFloat { get; set; }
		public HelpManager HelpManager { get; private set; }

		public static AppManager Instance
		{
			get { return _instance; }
		}

		private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
		{
			if (e.Reason == SessionSwitchReason.SessionUnlock && !RegistryHelper.ShowFloat && !RegistryHelper.ShowHidden)
			{
				RunMinibarLoader();
			}
		}

		public void RunForm()
		{
			UpdateSyncFiles();
			if (LoadSettings())
			{
				MinibarPowerPointHelper.Instance.Connect(false);
				Application.Run(FormMain.Instance);
			}
		}

		private bool LoadSettings()
		{
			bool result = false;
			SettingsManager.Instance.CreateStaticFolders();
			SettingsManager.Instance.LoadSettings();
			if (result = SyncManager.CheckSyncSetting())
			{
				SettingsManager.Instance.LoadMinibarSettings();
				SettingsManager.Instance.LoadMinibarApplicationSettings();
				SettingsManager.Instance.SaveMinibarSettings();
				SyncManager.SchedulerSyncInBackground();
			}
			return result;
		}

		private void UpdateSyncFiles()
		{
			var thread = new Thread(delegate()
			{
				try
				{
					var source = new DirectoryInfo(SettingsManager.Instance.SyncFilesSourcePath);
					if (source.Exists)
					{
						foreach (FileInfo sourceFile in source.GetFiles())
						{
							string destinationFilePath = Path.Combine(SettingsManager.Instance.SyncSettingsFolderPath, sourceFile.Name);
							if (File.Exists(destinationFilePath))
							{
								while (Process.GetProcesses().Where(x => x.ProcessName.ToLower().Contains(Path.GetFileNameWithoutExtension(destinationFilePath).ToLower())).Count() > 0)
									Thread.Sleep(1000);
								if (File.GetLastWriteTime(destinationFilePath) >= sourceFile.LastWriteTime)
									continue;
								try
								{
									File.SetAttributes(destinationFilePath, FileAttributes.Normal);
								}
								catch { }
							}
							try
							{
								sourceFile.CopyTo(destinationFilePath, true);
							}
							catch { }
						}
					}
				}
				catch { }
			});
			thread.Start();
		}

		public void ShowWarning(string text)
		{
			MessageBox.Show(text, "adSALESapps.com", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public DialogResult ShowWarningQuestion(string text)
		{
			return MessageBox.Show(text, "adSALESapps.com", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}

		public void ShowInformation(string text)
		{
			MessageBox.Show(text, "adSALESapps.com", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public bool AplicationDetected()
		{
			return Process.GetProcesses().Where(x => x.ProcessName.Contains("adSALESapp") || x.ProcessName.Contains("ProSlides") || x.ProcessName.Contains("OneDomain")).Count() > 0;
		}

		public void CloseActiveApplications()
		{
			Process[] processList = Process.GetProcesses();
			foreach (Process process in processList.Where(x => x.ProcessName.ToLower().Contains("adsalesapp") || x.ProcessName.ToLower().Contains("proslides") || x.ProcessName.ToLower().Contains("onedomain") || x.ProcessName.ToLower().Contains("salesdepot") || x.ProcessName.ToLower().Contains("medialibrary")))
				process.Kill();
		}

		public Process GetActiveProcess()
		{
			uint lpdwProcessId = 0;
			WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId);
			Process activeProcess = Process.GetProcessById((int)lpdwProcessId);
			return activeProcess ?? null;
		}

		public void WipeSoftware()
		{
			CloseActiveApplications();

			if (File.Exists(SettingsManager.Instance.ResetPath))
				Process.Start(SettingsManager.Instance.ResetPath);
		}

		public void SetClickEventHandler(Control control)
		{
			foreach (Control childControl in control.Controls)
				SetClickEventHandler(childControl);
			if (control.GetType() != typeof(TextBoxMaskBox) && control.GetType() != typeof(TextEdit) && control.GetType() != typeof(MemoEdit) && control.GetType() != typeof(ComboBoxEdit) && control.GetType() != typeof(LookUpEdit) && control.GetType() != typeof(DateEdit) && control.GetType() != typeof(CheckedListBoxControl) && control.GetType() != typeof(SpinEdit) && control.GetType() != typeof(CheckEdit))
			{
				control.Click += ControlClick;
			}
		}

		private void ControlClick(object sender, EventArgs e)
		{
			((Control)sender).Select();
			if (((Control)sender).Parent != null)
				((Control)sender).Parent.Select();
		}

		public bool IsProcessWindowMaximized(Process p)
		{
			bool result = false;
			IntPtr handle;
			var placement = new WINDOWPLACEMENT();
			placement.length = Marshal.SizeOf(placement);
			if (processesAndMaximizedWindows.ContainsKey(p.Id))
			{
				handle = processesAndMaximizedWindows[p.Id];
			}
			else
			{
				processesAndMaximizedWindows.Add(p.Id, p.MainWindowHandle);
				handle = p.MainWindowHandle;
			}

			if (WinAPIHelper.GetWindowPlacement(handle, out placement))
			{
				if (placement.showCmd == 3) // WM_MAXIMIZED
				{
					processesAndMaximizedWindows[p.Id] = handle;
					result = true;
				}
			}
			else if (processesAndMaximizedWindows[p.Id] != p.MainWindowHandle)
			{
				processesAndMaximizedWindows[p.Id] = p.MainWindowHandle;
				result = IsProcessWindowMaximized(p);
			}
			return result;
		}

		public Bitmap MakeGrayscale(Bitmap original)
		{
			var newBitmap = new Bitmap(original.Width, original.Height);

			//get a graphics object from the new image
			Graphics g = Graphics.FromImage(newBitmap);

			//create the grayscale ColorMatrix
			var colorMatrix = new ColorMatrix(
				new[]
				{
					new[] { .3f, .3f, .3f, 0, 0 },
					new[] { .59f, .59f, .59f, 0, 0 },
					new[] { .11f, .11f, .11f, 0, 0 },
					new float[] { 0, 0, 0, 1, 0 },
					new float[] { 0, 0, 0, 0, 1 }
				});

			//create some image attributes
			var attributes = new ImageAttributes();

			//set the color matrix attribute
			attributes.SetColorMatrix(colorMatrix);

			//draw the original image on the new image
			//using the grayscale color matrix
			g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
						0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

			//dispose the Graphics object
			g.Dispose();
			return newBitmap;
		}

		#region Run Internal Apps
		public void RunDashboard(string parameter = "")
		{
			if (File.Exists(SettingsManager.Instance.DashboardPath))
			{
				var process = new Process();
				process.StartInfo.FileName = SettingsManager.Instance.DashboardPath;
				process.StartInfo.Arguments = parameter;
				process.Start();
			}
			else
				ShowWarning("Couldn't find Dashboard app");
		}

		public void RunLocalSalesDepot()
		{
			if (File.Exists(SettingsManager.Instance.SalesDepotSettings.ExecutablePath))
				Process.Start(SettingsManager.Instance.SalesDepotSettings.ExecutablePath);
			else
				ShowWarning("Couldn't find Sales Depot app");
		}

		public void RunWebSalesDepot()
		{
			string browser = string.Empty;
			if (SettingsManager.Instance.SalesDepotBrowserChrome)
				browser = "chrome.exe";
			else if (SettingsManager.Instance.SalesDepotBrowserFirefox)
				browser = "firefox.exe";
			else if (SettingsManager.Instance.SalesDepotBrowserFirefox)
				browser = "opera.exe";
			else
				browser = "iexplore.exe";
			try
			{
				var process = new Process
							  {
								  StartInfo =
								  {
									  FileName = browser,
									  Arguments = SettingsManager.Instance.SalesDepotSettings.Url
								  }
							  };
				process.Start();
			}
			catch
			{
				ShowWarning("Couldn't open the website");
			}
		}

		public void RunSalesDepotRemote()
		{
			if (File.Exists(SettingsManager.Instance.SalesDepotSettings.ExecutablePath))
			{
				var process = new Process();
				process.StartInfo.Arguments = "-remote";
				process.StartInfo.FileName = SettingsManager.Instance.SalesDepotSettings.ExecutablePath;
				process.Start();
			}
			else
				ShowWarning("Couldn't find Remote Libraries app");
		}

		public void RunMinibarLoader()
		{
			if (File.Exists(SettingsManager.Instance.MinibarLoaderPath))
			{
				var process = new Process();
				process.StartInfo.Arguments = "-f";
				process.StartInfo.FileName = SettingsManager.Instance.MinibarLoaderPath;
				process.Start();
			}
			else
				ShowWarning("Couldn't find Minibar Loader app");
		}

		public void RunPowerPointLoader()
		{
			if (File.Exists(SettingsManager.Instance.PowerPointLoaderPath))
			{
				var process = new Process();
				process.StartInfo.FileName = SettingsManager.Instance.PowerPointLoaderPath;
				process.Start();

				var thread = new Thread(delegate()
				{
					while (Process.GetProcesses().Where(x => x.ProcessName.ToLower().Contains(Path.GetFileNameWithoutExtension(SettingsManager.Instance.PowerPointLoaderPath).ToLower())).Any())
						Thread.Sleep(1000);
				});
				thread.Start();

				while (thread.IsAlive)
					Application.DoEvents();
			}
			else
				ShowWarning("Couldn't find PowerPointLoader app");
		}

		public void RunClientLogos()
		{
			if (File.Exists(SettingsManager.Instance.ClientLogosPath))
			{
				Process.Start(SettingsManager.Instance.ClientLogosPath);
			}
			else
				ShowWarning("Couldn't find Client Logos app");
		}

		public void RunSalesGallery()
		{
			if (File.Exists(SettingsManager.Instance.SalesGalleryPath))
			{
				Process.Start(SettingsManager.Instance.SalesGalleryPath);
			}
			else
				ShowWarning("Couldn't find Sales Gallery app");
		}

		public void RunWebArt()
		{
			if (File.Exists(SettingsManager.Instance.WebArtPath))
			{
				Process.Start(SettingsManager.Instance.WebArtPath);
			}
			else
				ShowWarning("Couldn't find Web Art app");
		}

		public void KillPowerPoint()
		{
			foreach (Process process in Process.GetProcesses().Where(x => x.ProcessName.ToUpper().Equals("POWERPNT")))
				process.Kill();
		}

		public void KillExcel()
		{
			foreach (Process process in Process.GetProcesses().Where(x => x.ProcessName.ToUpper().Equals("EXCEL")))
				process.Kill();
		}

		public void KillFMAutoSync()
		{
			foreach (Process process in Process.GetProcesses().Where(x => x.ProcessName.ToUpper().Equals("AUTOFMSYNC")))
				process.Kill();
		}
		#endregion
	}
}