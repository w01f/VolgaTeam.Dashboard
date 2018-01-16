﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asa.Bar.App.Authorization;
using Asa.Bar.App.BarItems;
using Asa.Bar.App.Common;
using Asa.Bar.App.Configuration;
using Asa.Bar.App.ExternalProcesses;
using Asa.Bar.App.Forms;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Bar.App
{
	class AppManager
	{
		protected FormMain MainForm { get; }

		public SettingsManager Settings { get; }

		public ActivityManager ActivityManager { get; private set; }

		public BarItemsManager BarItemsManager { get; }

		public ExternalProcessesWatcher ExternalProcessesWatcher { get; }
		public MonitorConfigurationWatcher MonitorConfigurationWatcher { get; }
		public WebBrowserManager WebBrowserManager { get; }

		public static AppManager Instance { get; } = new AppManager();

		private AppManager()
		{
			Settings = new SettingsManager();
			BarItemsManager = new BarItemsManager();
			ExternalProcessesWatcher = new ExternalProcessesWatcher();
			MonitorConfigurationWatcher = new MonitorConfigurationWatcher();
			WebBrowserManager = new WebBrowserManager();

			MainForm = new FormMain();
		}

		public void RunApplication()
		{
			bool stopRun = false;

			AppProfileManager.Instance.InitApplication(AppTypeEnum.AdBar);
			PopupMessageHelper.Instance.Title = "adsalesapps";

			FileStorageManager.Instance.UsingLocalMode += (o, e) =>
			{
				if (FileStorageManager.Instance.UseLocalMode) return;
				FormStart.CloseProgress();
				if (FileStorageManager.Instance.DataState != DataActualityState.Updated)
				{
					PopupMessageHelper.Instance.ShowWarning("Server is not available. Application will be closed");
					stopRun = true;
				}
				else if (PopupMessageHelper.Instance.ShowWarningQuestion("Server is not available. Do you want to continue to work in local mode?") != DialogResult.Yes)
				{
					stopRun = true;
				}
				if (stopRun)
					FormStart.Destroy();
			};

			FileStorageManager.Instance.Authorizing += (o, e) =>
			{
				var authManager = new AdBarAuthManager();
				FormStart.SetTitle("Checking credentials...");
				authManager.Auth(e);
				if (!e.Authorized)
					LoadAtStartupHelper.UnsetLoadAtStartup();
			};

			FormStart.ShowProgress();
			FormStart.SetTitle("Connecting to adSALEScloud…");
			var thread = new Thread(() => AsyncHelper.RunSync(FileStorageManager.Instance.Init));
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();

			if (stopRun) return;

			FileStorageManager.Instance.Downloading += OnFileDownloading;
			FileStorageManager.Instance.Extracting += OnFileExtracting;

			if (FileStorageManager.Instance.Activated)
			{
				if (FileStorageManager.Instance.DataState == DataActualityState.NotExisted)
					FormStart.SetTitle("Syncing adSALEScloud for the 1st time…");
				else if (FileStorageManager.Instance.DataState == DataActualityState.Outdated)
					FormStart.SetTitle("Refreshing data from adSALEScloud…");
				else
					FormStart.SetTitle("Loading application data...");

				thread = new Thread(() =>
				{
					AsyncHelper.RunSync(LoadBusinessObjects);
					AsyncHelper.RunSync(FileStorageManager.Instance.FixDataState);
				});
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
			}

			FileStorageManager.Instance.Downloading -= OnFileDownloading;
			FileStorageManager.Instance.Extracting -= OnFileExtracting;
			FormStart.CloseProgress();
			FormStart.Destroy();

			if (FileStorageManager.Instance.Activated)
			{
				if (Settings.MaintenanceConfig.MaintenanceEnabled)
				{
					if(Settings.MaintenanceConfig.ShowInfo)
						Application.Run(new FormMaintenance());
				}
				else
					Application.Run(MainForm);
			}
			else
				PopupMessageHelper.Instance.ShowWarning("This app is not activated. Contact adSALESapps Support (help@adSALESapps.com)");
		}

		private async Task LoadBusinessObjects()
		{
			await AppProfileManager.Instance.LoadProfile();
			await ResourceManager.Instance.Load();
			Settings.Load();

			ActivityManager = ActivityManager.OpenStorage();

			ExternalProcessesWatcher.Load();
			MonitorConfigurationWatcher.Load();
			WebBrowserManager.Load();

			await BarItemsManager.Load();
		}

		private void OnFileDownloading(Object sender, FileProcessingProgressEventArgs args)
		{
			FormStart.SetDetails(args.ProgressPercent < 100 ?
				String.Format("Loading {0} - {1}%", args.FileName, args.ProgressPercent) :
				String.Empty);
		}

		private void OnFileExtracting(Object sender, FileProcessingProgressEventArgs args)
		{
			FormStart.SetDetails(args.ProgressPercent < 100 ?
					String.Format("Extracting {0} - {1}%", args.FileName, args.ProgressPercent) :
					String.Empty);
		}
	}
}
