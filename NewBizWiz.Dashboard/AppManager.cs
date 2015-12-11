using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Asa.CommonGUI.Common;
using Asa.CommonGUI.Floater;
using Asa.CommonGUI.SlideSettingsEditors;
using Asa.CommonGUI.ToolForms;
using Asa.Core.Common;
using Asa.Core.Interop;
using Asa.Dashboard.InteropClasses;
using Asa.Dashboard.Properties;

namespace Asa.Dashboard
{
	public class AppManager
	{
		#region Delegates
		public delegate void EmptyParametersDelegate();
		#endregion

		private static readonly AppManager _instance = new AppManager();
		public HelpManager HelpManager { get; private set; }
		public ActivityManager ActivityManager { get; private set; }
		private readonly FloaterManager _floater = new FloaterManager();

		private AppManager()
		{
			HelpManager = new HelpManager();
		}

		public static AppManager Instance
		{
			get { return _instance; }
		}

		public static string FormCaption
		{
			get
			{
				return String.Format("{0} v{1}- {2}",
					SettingsManager.Instance.DashboardName,
					FileStorageManager.Instance.Version,
					PowerPointManager.Instance.SlideSettings.SizeFormatted);
			}
		}

		public void RunForm()
		{
			bool stopRun = false;

			LicenseHelper.Register();

			AppProfileManager.Instance.InitApplication(AppTypeEnum.Dashboard);

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
					FormStart.SetTitle("Syncing with adSALEScloud for the 1st time…", "*This may take a few minutes…");
				else if (FileStorageManager.Instance.DataState == DataActualityState.Outdated)
					FormStart.SetTitle("Refreshing data from adSALEScloud…", "*This may take a few minutes…");
				else
					FormStart.SetTitle("Loading application data...", "*This should not take long…");

				thread = new Thread(() =>
				{
					AsyncHelper.RunSync(Init);
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

		public async Task Init()
		{
			await AppProfileManager.Instance.LoadProfile();
			await Core.Dashboard.ResourceManager.Instance.Load();

			PowerPointManager.Instance.Init(DashboardPowerPointHelper.Instance);

			MasterWizardManager.Instance.Load();
			await Core.Dashboard.SettingsManager.Instance.LoadSettings();

			Core.Dashboard.ListManager.Instance.Init();
			HelpManager.LoadHelpLinks();

			ActivityManager = ActivityManager.OpenStorage();
			ActivityManager.AddActivity(new UserActivity("Application started"));

			SetCultureSettings();
		}

		public void ActivateMainForm()
		{
			var mainFormHandle = RegistryHelper.MainFormHandle;
			if (mainFormHandle.ToInt32() == 0)
			{
				var processList = Process.GetProcesses();
				foreach (var process in processList.Where(x => x.ProcessName.ToLower().Contains("adsalesapp")).Where(process => process.MainWindowHandle.ToInt32() != 0))
				{
					mainFormHandle = process.MainWindowHandle;
					break;
				}
			}
			Utilities.Instance.ActivateForm(mainFormHandle, RegistryHelper.MaximizeMainForm, false);
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

		public bool CheckPowerPointRunning()
		{
			if (DashboardPowerPointHelper.Instance.IsLinkedWithApplication) return true;
			if (Utilities.Instance.ShowWarningQuestion(String.Format("PowerPoint is required to run this application.{0}Do you want to go ahead and open PowerPoint?", Environment.NewLine)) == DialogResult.Yes)
				ShowFloater(() => PowerPointManager.Instance.RunPowerPointLoader());
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
			if (((Control)sender).Parent != null)
				((Control)sender).Parent.Select();
		}

		public void ShowFloater(Action afterShow)
		{
			ShowFloater(null, new FloaterRequestedEventArgs() { AfterShow = afterShow });
		}

		public void ShowFloater(Form sender, FloaterRequestedEventArgs e)
		{
			var afterBack = new Action(ActivateMainForm);
			_floater.ShowFloater(sender ?? FormMain.Instance, e.Logo ?? Resources.RibbonLogo, e.AfterShow, null, afterBack);
		}
	}
}