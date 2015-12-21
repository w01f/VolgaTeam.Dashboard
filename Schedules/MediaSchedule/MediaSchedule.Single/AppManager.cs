using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Asa.CommonGUI.Common;
using Asa.CommonGUI.Floater;
using Asa.CommonGUI.SlideSettingsEditors;
using Asa.CommonGUI.ToolForms;
using Asa.Core.Common;
using Asa.Core.Interop;
using Asa.Core.MediaSchedule;

namespace Asa.MediaSchedule.Single
{
	public class AppManager
	{
		private static readonly AppManager _instance = new AppManager();
		private readonly FloaterManager _floater = new FloaterManager();

		private AppManager() { }

		public static AppManager Instance
		{
			get { return _instance; }
		}

		public void RunApplication(MediaDataType mediaType)
		{
			bool stopRun = false;

			LicenseHelper.Register();
			
			MediaMetaData.Instance.Init(mediaType);
			AppProfileManager.Instance.InitApplication(MediaMetaData.Instance.AppType);

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
				if (Utilities.Instance.ShowWarningQuestion("Server is not available. Do you want to continue to work in local mode?", "adSALESapps.com ") != DialogResult.Yes)
				{
					stopRun = true;
					Application.Exit();
				}
			};

			FileStorageManager.Instance.Authorizing += (o, e) =>
			{
				var authManager = new AuthManager();
				authManager.Init();
				FormStart.SetTitle("Checking credentials...", "*This should not take long…");
				authManager.Auth(e);
			};

			FormStart.ShowProgress();
			FormStart.SetTitle("Connecting to adSALEScloud…", "*This should not take long…");
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
					FormStart.SetTitle("Syncing adSALEScloud for the 1st time…", "*This may take a few minutes…");
				else if (FileStorageManager.Instance.DataState == DataActualityState.Outdated)
					FormStart.SetTitle("Refreshing data from adSALEScloud…", "*This may take a few minutes…");
				else
					FormStart.SetTitle("Loading application data...", "*This should not take long…");

				thread = new Thread(() =>
				{
					AsyncHelper.RunSync(() => Controls.Controller.Instance.InitBusinessObjects());
					FileStorageManager.Instance.DataState = DataActualityState.Updated;
				});
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();

				FormMain.Instance.Init();
			}

			FormStart.CloseProgress();
			if (FileStorageManager.Instance.Activated)
			{
				if (PowerPointManager.Instance.SettingsSource == SettingsSourceEnum.PowerPoint &&
				MasterWizardManager.Instance.SelectedWizard != null &&
				!MasterWizardManager.Instance.SelectedWizard.HasSlideConfiguration(PowerPointManager.Instance.SlideSettings))
				{
					var availableMasterWizards = MasterWizardManager.Instance.MasterWizards.Values.Where(w => w.HasSlideConfiguration(PowerPointManager.Instance.SlideSettings)).ToList();
					if (availableMasterWizards.Any())
					{
						using (var form = new FormSelectMasterWizard())
						{
							form.comboBoxEditSlideFormat.Properties.Items.AddRange(availableMasterWizards);
							form.comboBoxEditSlideFormat.EditValue = availableMasterWizards.FirstOrDefault();
							if (form.ShowDialog() != DialogResult.OK)
								return;
							SettingsManager.Instance.SelectedWizard = ((MasterWizard)form.comboBoxEditSlideFormat.EditValue).Name;
							MasterWizardManager.Instance.SetMasterWizard();
						}
					}
					else
					{
						Utilities.Instance.ShowWarning("Slide pack not found for selected size. Contact adSALESapps Support (help@adSALESapps.com)");
						return;
					}
				}
				Application.Run(FormMain.Instance);
			}
			else
				Utilities.Instance.ShowWarning("This app is not activated. Contact adSALESapps Support (help@adSALESapps.com)");
		}

		public void ActivateMainForm()
		{
			var processList = Process.GetProcesses();
			foreach (var process in processList.Where(x => x.ProcessName.ToLower().Contains(String.Format("{0}seller", MediaMetaData.Instance.DataTypeString.ToLower()))))
			{
				if (process.MainWindowHandle.ToInt32() != 0)
				{
					Utilities.Instance.ActivateForm(process.MainWindowHandle, true, false);
					break;
				}
			}
		}

		public void ShowFloater(Form sender, FloaterRequestedEventArgs e)
		{
			var afterBack = new Action(ActivateMainForm);
			_floater.ShowFloater(sender ?? FormMain.Instance, e.Logo, e.AfterShow, null, afterBack);
		}
	}
}