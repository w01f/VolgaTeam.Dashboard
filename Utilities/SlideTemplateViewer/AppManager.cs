using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Activities;
using Asa.Common.Core.Objects.Output;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Floater;
using Asa.Common.GUI.SlideSettingsEditors;
using Asa.Common.GUI.ToolForms;
using Asa.SlideTemplateViewer.Properties;

namespace Asa.SlideTemplateViewer
{
	public class AppManager
	{
		public SlideManager SlideManager { get; }
		public HelpManager HelpManager { get; }
		public ActivityManager ActivityManager { get; private set; }
		private readonly FloaterManager _floater = new FloaterManager();

		private AppManager()
		{
			SlideManager = new SlideManager();
			HelpManager = new HelpManager();
		}

		public static AppManager Instance { get; } = new AppManager();

		public string FormCaption => String.Format("{0} v{1}- {2}",
			SlideManager.FormTitle ?? "Add Slides",
			FileStorageManager.Instance.Version,
			PowerPointManager.Instance.SlideSettings.SizeFormatted);

		public void RunForm()
		{
			bool stopRun = false;

			PopupMessageHelper.Instance.Title = "Add Slides";

			LicenseHelper.Register();

			AppProfileManager.Instance.InitApplication(AppTypeEnum.Dashboard);

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
				var authManager = new AuthManager();
				FormStart.SetTitle("Checking credentials...");
				e.LightCheck = true;
				authManager.Auth(e);
			};

			FormStart.ShowProgress();
			FormStart.SetTitle("Connecting to adSALEScloud…");
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
					FormStart.SetTitle("Syncing adSALEScloud for the 1st time…");
				else if (FileStorageManager.Instance.DataState == DataActualityState.Outdated)
					FormStart.SetTitle("Refreshing data from adSALEScloud…");
				else
					FormStart.SetTitle("Loading application data...");

				thread = new Thread(() =>
				{
					AsyncHelper.RunSync(Init);
					AsyncHelper.RunSync(FileStorageManager.Instance.FixDataState);
				});
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();

				FormMain.Instance.Init();
			}
			FormStart.CloseProgress();
			FormStart.Destroy();

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
						PopupMessageHelper.Instance.ShowWarning("Slide pack not found for selected size. Contact adSALESapps Support (help@adSALESapps.com)");
						return;
					}
				}
				Application.Run(FormMain.Instance);
			}
			else
				PopupMessageHelper.Instance.ShowWarning("This app is not activated. Contact adSALESapps Support (help@adSALESapps.com)");
		}

		public async Task Init()
		{
			await AppProfileManager.Instance.LoadProfile();
			await Business.Dashboard.Configuration.ResourceManager.Instance.Load();

			PowerPointManager.Instance.Init(SlideTemplateViewerPowerPointHelper.Instance);

			MasterWizardManager.Instance.Load();
			SettingsManager.Instance.LoadSharedSettings();
			SlideManager.Load();

			HelpManager.LoadHelpLinks();

			ActivityManager = ActivityManager.OpenStorage();
			ActivityManager.AddActivity(new UserActivity("Application started"));
		}

		public void ActivateMainForm()
		{
			IntPtr mainFormHandle = IntPtr.Zero;
			var processList = Process.GetProcesses();
			foreach (var process in processList.Where(x => x.ProcessName.ToLower().Contains("add slides")).Where(process => process.MainWindowHandle.ToInt32() != 0))
			{
				mainFormHandle = process.MainWindowHandle;
				break;
			}
			Utilities.ActivateForm(mainFormHandle, false, false);
		}

		public bool CheckPowerPointRunning()
		{
			if (SlideTemplateViewerPowerPointHelper.Instance.Connect(false))
				return true;
			if (PopupMessageHelper.Instance.ShowWarningQuestion(String.Format("PowerPoint is required to run this application.{0}Do you want to go ahead and open PowerPoint?", Environment.NewLine)) == DialogResult.Yes)
				ShowFloater(() => PowerPointManager.Instance.RunPowerPointLoader());
			return false;
		}

		public void ShowFloater(Action afterShow)
		{
			ShowFloater(null, new FloaterRequestedEventArgs
			{
				Logo = SlideManager.RibbonBarLogo ?? Resources.AddSlidesLogo,
				AfterShow = afterShow
			});
		}

		public void ShowFloater(Form sender, FloaterRequestedEventArgs e)
		{
			var afterBack = new Action<bool>(b => ActivateMainForm());
			_floater.ShowFloater(sender ?? FormMain.Instance, null, e.Logo ?? SlideManager.RibbonBarLogo ?? Resources.AddSlidesLogo, e.AfterShow, null, afterBack);
		}
	}
}
