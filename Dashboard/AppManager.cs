using System;
using System.Diagnostics;
using System.Globalization;
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
using DevExpress.XtraEditors;
using Asa.Dashboard.InteropClasses;
using Asa.Dashboard.Properties;

namespace Asa.Dashboard
{
	public class AppManager
	{
		#region Delegates
		public delegate void EmptyParametersDelegate();
		#endregion

		public HelpManager HelpManager { get; }
		public ActivityManager ActivityManager { get; private set; }
		public PowerPointManager<DashboardPowerPointProcessor> PowerPointManager { get; }

		private readonly FloaterManager _floater = new FloaterManager();

		private AppManager()
		{
			HelpManager = new HelpManager();
			PowerPointManager = new PowerPointManager<DashboardPowerPointProcessor>();
		}

		public static AppManager Instance { get; } = new AppManager();

		public string FormCaption => String.Format("{0} v{1}- {2}",
			"6ms",
			FileStorageManager.Instance.Version,
			SlideSettingsManager.Instance.SlideSettings.SizeFormatted);

		public void RunForm()
		{
			bool stopRun = false;

			var appTitle = "6 Minute Seller";

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
			var thread = new Thread(() => AsyncHelper.RunSync(() => FileStorageManager.Instance.Init()));
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
			await Business.Dashboard.Configuration.SettingsManager.Instance.LoadSettings();

			Business.Dashboard.Dictionaries.ListManager.Instance.Init();
			HelpManager.LoadHelpLinks();

			ActivityManager = ActivityManager.OpenStorage();
			ActivityManager.AddActivity(new UserActivity("Application started"));

			SetCultureSettings();
		}

		public void ActivateMainForm()
		{
			IntPtr mainFormHandle = IntPtr.Zero;
			var processList = Process.GetProcesses();
			foreach (var process in processList.Where(x => x.ProcessName.ToLower().Contains("6ms")).Where(process => process.MainWindowHandle.ToInt32() != 0))
			{
				mainFormHandle = process.MainWindowHandle;
				break;
			}
			Utilities.ActivateForm(mainFormHandle, false, false);
		}

		public void SetClickEventHandler(Control control)
		{
			foreach (Control childControl in control.Controls)
				SetClickEventHandler(childControl);
			if (control.GetType() != typeof(TextBoxMaskBox) &&
				control.GetType() != typeof(TextEdit) &&
				control.GetType() != typeof(MemoEdit) &&
				control.GetType() != typeof(ComboBoxEdit) &&
				control.GetType() != typeof(ComboBoxListEdit) &&
				control.GetType() != typeof(LookUpEdit) &&
				control.GetType() != typeof(DateEdit) &&
				control.GetType() != typeof(CheckedListBoxControl) &&
				control.GetType() != typeof(SpinEdit) &&
				control.GetType() != typeof(CheckEdit))
			{
				control.Click += ControlClick;
			}
			Application.DoEvents();
		}

		public bool CheckPowerPointRunning(Action afterRun = null)
		{
			if (PowerPointManager.Processor.Connect())
				return true;
			if (PopupMessageHelper.Instance.ShowWarningQuestion(String.Format("PowerPoint is required to run this application.{0}Do you want to go ahead and open PowerPoint?", Environment.NewLine)) == DialogResult.Yes)
				ShowFloater(() => PowerPointManager.RunPowerPointLoader(), afterRun);
			return false;
		}

		private void SetCultureSettings()
		{
			switch (SettingsManager.Instance.DashboardCode)
			{
				case "tv":
				case "radio":
				case "cable":
					Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
					Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
					Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
					Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
					break;
				default:
					Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
					Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Sunday;
					Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
					Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
					break;
			}
		}

		private void ControlClick(object sender, EventArgs e)
		{
			((Control)sender).Select();
			((Control)sender).Parent?.Select();
		}

		public void ShowFloater(Action afterShow, Action afterBack = null)
		{
			ShowFloater(null, new FloaterRequestedEventArgs
			{
				Logo = Resources.RibbonLogo,
				AfterShow = afterShow,
				AfterBack = afterBack,
			});
		}

		public void ShowFloater(Form sender, FloaterRequestedEventArgs e)
		{
			var afterBack = new Action<bool>(b => ActivateMainForm());
			_floater.ShowFloater(sender ?? FormMain.Instance, null, e.Logo ?? Resources.RibbonLogo, e.AfterShow, null, afterBack);
		}
	}
}