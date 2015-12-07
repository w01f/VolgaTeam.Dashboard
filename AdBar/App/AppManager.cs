using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asa.Bar.App.Authorization;
using Asa.Bar.App.BarItems;
using Asa.Bar.App.Common;
using Asa.Bar.App.ExternalProcesses;
using Asa.Bar.App.Forms;
using Asa.Core.Common;
using Asa.Core.Interop;
using ResourceManager = Asa.Bar.App.Configuration.ResourceManager;
using SettingsManager = Asa.Bar.App.Configuration.SettingsManager;

namespace Asa.Bar.App
{
	class AppManager
	{
		private static readonly AppManager _instance = new AppManager();

		protected FormMain MainForm { get; private set; }

		public SettingsManager Settings { get; private set; }

		public ActivityManager ActivityManager { get; private set; }

		public BarItemsManager BarItemsManager { get; private set; }

		public ExternalProcessesWatcher ExternalProcessesWatcher { get; private set; }
		public MonitorConfigurationWatcher MonitorConfigurationWatcher { get; private set; }
		public WebBrowserManager WebBrowserManager { get; private set; }

		public static AppManager Instance
		{
			get { return _instance; }
		}

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

			FileStorageManager.Instance.UsingLocalMode += (o, e) =>
			{
				if (FileStorageManager.Instance.UseLocalMode) return;
				FormStart.CloseProgress();
				if (FileStorageManager.Instance.DataState != DataActualityState.Updated)
				{
					Utilities.Instance.ShowWarning("Server is not available. Application will be closed");
					stopRun = true;
					Application.Exit();
					return;
				}
				if (Utilities.Instance.ShowWarningQuestion("Server is not available. Do you want to continue to work in local mode?") != DialogResult.Yes)
				{
					stopRun = true;
					Application.Exit();
				}
			};

			FileStorageManager.Instance.Authorizing += (o, e) =>
			{
				var authManager = new AdBarAuthManager();
				authManager.Init();
				FormStart.SetTitle("Checking credentials...", "*This should not take long…");
				authManager.Auth(e);
			};

			FormStart.ShowProgress();
			FormStart.SetTitle("Checking data version...", "*This should not take long…");
			var thread = new Thread(() => AsyncHelper.RunSync(FileStorageManager.Instance.Init));
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();

			if (stopRun) return;

			FileStorageManager.Instance.Downloading += (sender, args) =>
				FormStart.SetDetails(args.ProgressPercent < 100 ?
					String.Format("Loading {0} - {1}%", args.FileName, args.ProgressPercent) :
					String.Empty);
			FileStorageManager.Instance.Extracting += (sender, args) =>
				FormStart.SetDetails(args.ProgressPercent < 100 ?
					String.Format("Extracting {0} - {1}%", args.FileName, args.ProgressPercent) :
					String.Empty);

			if (FileStorageManager.Instance.Activated)
			{
				if (FileStorageManager.Instance.DataState == DataActualityState.NotExisted)
					FormStart.SetTitle("Loading data from server for the 1st time...", "*This may take a few minutes…");
				else if (FileStorageManager.Instance.DataState == DataActualityState.Outdated)
					FormStart.SetTitle("Updating data from server...", "*This may take a few minutes…");
				else
					FormStart.SetTitle("Loading application data...", "*This should not take long…");

				thread = new Thread(() =>
				{
					AsyncHelper.RunSync(LoadBusinessObjects);
					FileStorageManager.Instance.DataState = DataActualityState.Updated;
				});
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
			}
			FormStart.CloseProgress();

			if (FileStorageManager.Instance.Activated)
			{
				Application.Run(MainForm);
			}
			else
				Utilities.Instance.ShowWarning("This app is not activated. Contact adSALESapps Support (help@adSALESapps.com)");
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
	}
}
