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
		public PowerPointManager<SlideTemplateViewerPowerPointHelper> PowerPointManager { get; }
		public TextResourcesManager TextResourcesManager { get; }
		public ImageResourcesManager ImageResourcesManager { get; }
		public FormStyleManager FormStyleManager { get; set; }

		private readonly FloaterManager _floater = new FloaterManager();

		private AppManager()
		{
			SlideManager = new SlideManager();
			HelpManager = new HelpManager();
			PowerPointManager = new PowerPointManager<SlideTemplateViewerPowerPointHelper>();
			ImageResourcesManager = new ImageResourcesManager();
			TextResourcesManager = new TextResourcesManager();
		}

		public static AppManager Instance { get; } = new AppManager();

		public string FormCaption => String.Format("{0} v{1} - {2}",
			TextResourcesManager.FormText?? "Add Slides",
			FileStorageManager.Instance.Version,
			SlideSettingsManager.Instance.SlideSettings.SizeFormatted);

		public void RunForm()
		{
			bool stopRun = false;

			var appTitle = "Add Slides";
			if (PowerPointManager.IsPowerPointMultipleInstances())
			{
				using (var form = new FormPowerPointSeveralInstancesWarning())
				{
					form.Text = appTitle;
					if (form.ShowDialog() != DialogResult.OK)
						return;
				}
			}

			PopupMessageHelper.Instance.Title = appTitle;

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
				if (PowerPointManager.SettingsSource == SettingsSourceEnum.PowerPoint &&
					MasterWizardManager.Instance.SelectedWizard != null &&
					!MasterWizardManager.Instance.SelectedWizard.HasSlideConfiguration(SlideSettingsManager.Instance.SlideSettings))
				{
					var availableMasterWizards = MasterWizardManager.Instance.MasterWizards.Values.Where(w => w.HasSlideConfiguration(SlideSettingsManager.Instance.SlideSettings)).ToList();
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
						PopupMessageHelper.Instance.ShowWarning("You already have a PowerPoint file opened that is not compatible with this application.\nPlease close that presentation, and open Sales Ninja again.");
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

			PowerPointManager.Init();

			MasterWizardManager.Instance.Load();
			SettingsManager.Instance.LoadSharedSettings();
			SlideManager.Load();

			HelpManager.LoadHelpLinks();

			TextResourcesManager.Load();
			ImageResourcesManager.Load();

			FormStyleManager = new FormStyleManager(Business.Dashboard.Configuration.ResourceManager.Instance.FormStyleConfigFile);

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

		public bool CheckPowerPointRunning(Action afterRun = null)
		{
			if (PowerPointManager.Processor.Connect(false))
				return true;
			if (PopupMessageHelper.Instance.ShowWarningQuestion(String.Format("PowerPoint is required to run this application.{0}Do you want to go ahead and open PowerPoint?", Environment.NewLine)) == DialogResult.Yes)
				ShowFloater(() => PowerPointManager.RunPowerPointLoader(), afterRun);
			return false;
		}

		public void ShowFloater(Action afterShow, Action afterBack = null)
		{
			ShowFloater(null, new FloaterRequestedEventArgs
			{
				Logo = SlideManager.RibbonBarLogo ?? Resources.AddSlidesLogo,
				AfterShow = afterShow,
				AfterBack = afterBack
			});
		}

		public void ShowFloater(Form sender, FloaterRequestedEventArgs e)
		{
			var afterBack = new Action<bool>(b => ActivateMainForm());
			_floater.ShowFloater(sender ?? FormMain.Instance, null, e.Logo ?? SlideManager.RibbonBarLogo ?? Resources.AddSlidesLogo, e.AfterShow, null, afterBack);
		}
	}
}
