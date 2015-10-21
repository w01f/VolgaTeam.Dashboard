using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.CommonGUI.SlideSettingsEditors;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;

namespace NewBizWiz.AdSchedule.Single
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

		public void RunApplication()
		{
			LicenseHelper.Register();

			AppProfileManager.Instance.InitApplication(AppTypeEnum.PrintSchedule);

			FormProgress.ShowProgress();
			FormProgress.SetTitle("Checking data version...");
			var thread = new Thread(() => AsyncHelper.RunSync(FileStorageManager.Instance.Init));
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			FileStorageManager.Instance.Downloading += (sender, args) =>
				FormProgress.SetDetails(args.ProgressPercent < 100 ?
					String.Format("Loading {0} - {1}%", args.FileName, args.ProgressPercent) :
					String.Empty);
			FileStorageManager.Instance.Extracting += (sender, args) =>
				FormProgress.SetDetails(args.ProgressPercent < 100 ?
					String.Format("Extracting {0} - {1}%", args.FileName, args.ProgressPercent) :
					String.Empty);

			if (FileStorageManager.Instance.Connected)
			{
				if (FileStorageManager.Instance.DataState == DataActualityState.NotExisted)
					FormProgress.SetTitle("Loading data from server for the 1st time...", true);
				else if (FileStorageManager.Instance.DataState == DataActualityState.Outdated)
					FormProgress.SetTitle("Updating data from server...", true);
				else
					FormProgress.SetTitle("Loading data...");

				thread = new Thread(() => AsyncHelper.RunSync(() => Controls.Controller.Instance.InitBusinessObjects()));
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();

				FormMain.Instance.Init();
			}

			FormProgress.CloseProgress();
			if (FileStorageManager.Instance.Connected)
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
			foreach (Process process in processList.Where(x => x.ProcessName.ToLower().Contains("adseller")))
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