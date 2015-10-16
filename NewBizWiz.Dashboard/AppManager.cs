﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.CommonGUI.SlideSettingsEditors;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.Dashboard.InteropClasses;
using NewBizWiz.Dashboard.Properties;

namespace NewBizWiz.Dashboard
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
			LicenseHelper.Register();

			AppProfileManager.Instance.InitApplication(AppTypeEnum.Dashboard);

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

				thread = new Thread(() => AsyncHelper.RunSync(Init));
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
			if (Utilities.Instance.ShowWarningQuestion(String.Format("PowerPoint must be open if you want to build a 6 Minute Seller Presentation.{0}Do you want to open PowerPoint now?", Environment.NewLine)) == DialogResult.Yes)
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