using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.CommonGUI.Gallery;
using NewBizWiz.CommonGUI.RateCard;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;
using NewBizWiz.MediaSchedule.Controls.PresentationClasses.Calendar;
using NewBizWiz.MediaSchedule.Controls.PresentationClasses.Digital;
using NewBizWiz.MediaSchedule.Controls.PresentationClasses.Gallery;
using NewBizWiz.MediaSchedule.Controls.PresentationClasses.OptionsControls;
using NewBizWiz.MediaSchedule.Controls.PresentationClasses.ScheduleControls;
using NewBizWiz.MediaSchedule.Controls.PresentationClasses.SnapshotControls;
using NewBizWiz.MediaSchedule.Controls.PresentationClasses.Strategy;
using NewBizWiz.MediaSchedule.Controls.PresentationClasses.Summary;

namespace NewBizWiz.MediaSchedule.Controls
{
	public class Controller
	{
		private static readonly Controller _instance = new Controller();
		private Controller() { }
		public static Controller Instance
		{
			get { return _instance; }
		}

		public event EventHandler<EventArgs> ScheduleChanged;
		public event EventHandler<FloaterRequestedEventArgs> FloaterRequested;

		public Form FormMain { get; set; }
		public SuperTooltip Supertip { get; set; }
		public RibbonControl Ribbon { get; set; }
		public RibbonTabItem TabHome { get; set; }
		public RibbonTabItem TabWeeklySchedule { get; set; }
		public RibbonTabItem TabMonthlySchedule { get; set; }
		public RibbonTabItem TabDigitalProduct { get; set; }
		public RibbonTabItem TabDigitalPackage { get; set; }
		public RibbonTabItem TabCalendar1 { get; set; }
		public RibbonTabItem TabCalendar2 { get; set; }
		public RibbonTabItem TabSummaryLight { get; set; }
		public RibbonTabItem TabSummaryFull { get; set; }
		public RibbonTabItem TabGallery1 { get; set; }
		public RibbonTabItem TabGallery2 { get; set; }
		public RibbonTabItem TabRateCard { get; set; }
		public RibbonTabItem TabSnapshot { get; set; }
		public RibbonTabItem TabOptions { get; set; }
		public RibbonTabItem TabStrategy { get; set; }

		public void Init()
		{
			SetDefaultCulture();
			Utilities.Instance.Title = String.Format("SellerPoint for {0}", MediaMetaData.Instance.DataTypeString);

			BusinessWrapper.Instance.ActivityManager.AddActivity(new UserActivity("Application Started"));

			#region Schedule Settings
			HomeControl = new HomeControl();
			HomeHelp.Click += HomeControl.Help_Click;
			HomeSave.Click += HomeControl.Save_Click;
			HomeSaveAs.Click += HomeControl.SaveAs_Click;
			HomeBusinessName.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeDecisionMaker.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeClientType.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeAccountNumberText.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeAccountNumberCheck.CheckedChanged += HomeControl.checkBoxItemAccountNumber_CheckedChanged;
			HomeFlightDatesStart.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += HomeControl.FlightDateStartEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += HomeControl.CalcWeeksOnFlightDatesChange;
			HomeFlightDatesEnd.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeFlightDatesEnd.EditValueChanged += HomeControl.CalcWeeksOnFlightDatesChange;
			HomeFlightDatesEnd.EditValueChanged += HomeControl.FlightDateEndEditValueChanged;
			HomeFlightDatesStart.CloseUp += HomeControl.dateEditFlightDatesStart_CloseUp;
			HomeFlightDatesEnd.CloseUp += HomeControl.dateEditFlightDatesEnd_CloseUp;
			HomeProductClone.Click += HomeControl.DigitalProductClone;
			HomeBusinessName.Enter += Utilities.Instance.Editor_Enter;
			HomeBusinessName.MouseDown += Utilities.Instance.Editor_MouseDown;
			HomeBusinessName.MouseUp += Utilities.Instance.Editor_MouseUp;
			HomeDecisionMaker.Enter += Utilities.Instance.Editor_Enter;
			HomeDecisionMaker.MouseDown += Utilities.Instance.Editor_MouseDown;
			HomeDecisionMaker.MouseUp += Utilities.Instance.Editor_MouseUp;

			HomeBusinessName.TabIndex = 0;
			HomeBusinessName.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomeDecisionMaker.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomeClientType.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomePresentationDate.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomeFlightDatesStart.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomeFlightDatesEnd.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			#endregion

			#region Weekly Schedule
			WeeklySchedule = new WeeklyScheduleControl();
			WeeklyScheduleSave.Click += WeeklySchedule.Save_Click;
			WeeklyScheduleSaveAs.Click += WeeklySchedule.SaveAs_Click;
			WeeklyScheduleHelp.Click += WeeklySchedule.Help_Click;
			WeeklyScheduleProgramAdd.Click += WeeklySchedule.AddProgram_Click;
			WeeklyScheduleProgramDelete.Click += WeeklySchedule.DeleteProgram_Click;
			WeeklyScheduleQuarterButton.CheckedChanged += WeeklySchedule.QuarterCheckedChanged;
			WeeklySchedulePowerPoint.AddEventHandler(CheckPowerPointRunning, WeeklySchedule.PowerPoint_Click);
			WeeklySchedulePreview.AddEventHandler(CheckPowerPointRunning, WeeklySchedule.Preview_Click);
			WeeklyScheduleEmail.AddEventHandler(CheckPowerPointRunning, WeeklySchedule.Email_Click);
			WeeklySchedulePdf.AddEventHandler(CheckPowerPointRunning, WeeklySchedule.Pdf_Click);
			#endregion

			#region Monthly Schedule
			MonthlySchedule = new MonthlyScheduleControl();
			MonthlyScheduleSave.Click += MonthlySchedule.Save_Click;
			MonthlyScheduleSaveAs.Click += MonthlySchedule.SaveAs_Click;
			MonthlyScheduleHelp.Click += MonthlySchedule.Help_Click;
			MonthlyScheduleProgramAdd.Click += MonthlySchedule.AddProgram_Click;
			MonthlyScheduleProgramDelete.Click += MonthlySchedule.DeleteProgram_Click;
			MonthlyScheduleQuarterButton.CheckedChanged += MonthlySchedule.QuarterCheckedChanged;
			MonthlySchedulePowerPoint.AddEventHandler(CheckPowerPointRunning, MonthlySchedule.PowerPoint_Click);
			MonthlySchedulePreview.AddEventHandler(CheckPowerPointRunning, MonthlySchedule.Preview_Click);
			MonthlyScheduleEmail.AddEventHandler(CheckPowerPointRunning, MonthlySchedule.Email_Click);
			MonthlySchedulePdf.AddEventHandler(CheckPowerPointRunning, MonthlySchedule.Pdf_Click);
			#endregion

			#region Digital Product
			DigitalProductContainer = new DigitalProductContainerControl(FormMain);
			DigitalProductSave.Click += DigitalProductContainer.Save_Click;
			DigitalProductSaveAs.Click += DigitalProductContainer.SaveAs_Click;
			DigitalProductPowerPoint.AddEventHandler(CheckPowerPointRunning, DigitalProductContainer.PowerPoint_Click);
			DigitalProductEmail.AddEventHandler(CheckPowerPointRunning, DigitalProductContainer.Email_Click);
			DigitalProductHelp.Click += DigitalProductContainer.Help_Click;
			DigitalProductPreview.AddEventHandler(CheckPowerPointRunning, DigitalProductContainer.Preview_Click);
			DigitalProductPdf.AddEventHandler(CheckPowerPointRunning, DigitalProductContainer.Pdf_Click);
			#endregion

			#region Web Package
			DigitalPackage = new MediaWebPackageControl(FormMain);
			DigitalPackageSave.Click += DigitalPackage.Save_Click;
			DigitalPackageSaveAs.Click += DigitalPackage.SaveAs_Click;
			DigitalPackagePowerPoint.AddEventHandler(CheckPowerPointRunning, DigitalPackage.PowerPoint_Click);
			DigitalPackagePreview.AddEventHandler(CheckPowerPointRunning, DigitalPackage.Preview_Click);
			DigitalPackageEmail.AddEventHandler(CheckPowerPointRunning, DigitalPackage.Email_Click);
			DigitalPackagePdf.AddEventHandler(CheckPowerPointRunning, DigitalPackage.Pdf_Click);
			DigitalPackageHelp.Click += DigitalPackage.Help_Click;
			#endregion

			#region Calendar1
			BroadcastCalendar = new BroadcastCalendarControl();
			Calendar1MonthsList.SelectedIndexChanged += BroadcastCalendar.MonthList_SelectedIndexChanged;
			Calendar1Copy.Click += BroadcastCalendar.CalendarCopy_Click;
			Calendar1Paste.Click += BroadcastCalendar.CalendarPaste_Click;
			Calendar1Clone.Click += BroadcastCalendar.CalendarClone_Click;
			Calendar1Save.Click += BroadcastCalendar.Save_Click;
			Calendar1SaveAs.Click += BroadcastCalendar.SaveAs_Click;
			Calendar1Preview.AddEventHandler(CheckPowerPointRunning, BroadcastCalendar.Preview_Click);
			Calendar1PowerPoint.AddEventHandler(CheckPowerPointRunning, BroadcastCalendar.PowerPoint_Click);
			Calendar1Email.AddEventHandler(CheckPowerPointRunning, BroadcastCalendar.Email_Click);
			Calendar1Pdf.AddEventHandler(CheckPowerPointRunning, BroadcastCalendar.Pdf_Click);
			Calendar1Help.Click += BroadcastCalendar.Help_Click;
			#endregion

			#region Calendar2
			CustomCalendar = new CustomCalendarControl();
			Calendar2MonthsList.SelectedIndexChanged += CustomCalendar.MonthList_SelectedIndexChanged;
			Calendar2Copy.Click += CustomCalendar.CalendarCopy_Click;
			Calendar2Paste.Click += CustomCalendar.CalendarPaste_Click;
			Calendar2Clone.Click += CustomCalendar.CalendarClone_Click;
			Calendar2Save.Click += CustomCalendar.Save_Click;
			Calendar2SaveAs.Click += CustomCalendar.SaveAs_Click;
			Calendar2Preview.AddEventHandler(CheckPowerPointRunning, CustomCalendar.Preview_Click);
			Calendar2PowerPoint.AddEventHandler(CheckPowerPointRunning, CustomCalendar.PowerPoint_Click);
			Calendar2Email.AddEventHandler(CheckPowerPointRunning, CustomCalendar.Email_Click);
			Calendar2Pdf.AddEventHandler(CheckPowerPointRunning, CustomCalendar.Pdf_Click);
			Calendar2Help.Click += CustomCalendar.Help_Click;
			#endregion

			#region Summary Light
			SummaryLight = new MediaSummaryLight();
			SummaryLightSave.Click += SummaryLight.Save_Click;
			SummaryLightSaveAs.Click += SummaryLight.SaveAs_Click;
			SummaryLightHelp.Click += (o, e) => SummaryLight.OpenHelp();
			#endregion

			#region Summary Full
			SummaryFull = new MediaSummaryFull();
			SummaryFullSave.Click += SummaryFull.Save_Click;
			SummaryFullSaveAs.Click += SummaryFull.SaveAs_Click;
			SummaryFullHelp.Click += (o, e) => SummaryFull.OpenHelp();
			#endregion

			#region Strategy
			Strategy = new ProgramStrategyControl();
			StrategySave.Click += Strategy.Save_Click;
			StrategySaveAs.Click += Strategy.SaveAs_Click;
			StrategyHelp.Click += Strategy.Help_Click;
			StrategyPowerPoint.AddEventHandler(CheckPowerPointRunning, Strategy.PowerPoint_Click);
			StrategyEmail.AddEventHandler(CheckPowerPointRunning, Strategy.Email_Click);
			StrategyPreview.AddEventHandler(CheckPowerPointRunning, Strategy.Preview_Click);
			StrategyPdf.AddEventHandler(CheckPowerPointRunning, Strategy.Pdf_Click);
			#endregion

			#region Snapshot
			Snapshot = new SnapshotContainer();
			SnapshotSave.Click += Snapshot.Save_Click;
			SnapshotSaveAs.Click += Snapshot.SaveAs_Click;
			SnapshotPowerPoint.AddEventHandler(CheckPowerPointRunning, Snapshot.PowerPoint_Click);
			SnapshotPreview.AddEventHandler(CheckPowerPointRunning, Snapshot.Preview_Click);
			SnapshotEmail.AddEventHandler(CheckPowerPointRunning, Snapshot.Email_Click);
			SnapshotPdf.AddEventHandler(CheckPowerPointRunning, Snapshot.Pdf_Click);
			SnapshotHelp.Click += Snapshot.Help_Click;
			SnapshotNew.Click += Snapshot.New_Click;
			SnapshotProgramAdd.Click += Snapshot.AddProgram_Click;
			SnapshotProgramDelete.Click += Snapshot.DeleteProgram_Click;
			#endregion

			#region Options
			Options = new OptionsContainer();
			OptionsSave.Click += Options.Save_Click;
			OptionsSaveAs.Click += Options.SaveAs_Click;
			OptionsPowerPoint.AddEventHandler(CheckPowerPointRunning, Options.PowerPoint_Click);
			OptionsPreview.AddEventHandler(CheckPowerPointRunning, Options.Preview_Click);
			OptionsEmail.AddEventHandler(CheckPowerPointRunning, Options.Email_Click);
			OptionsPdf.AddEventHandler(CheckPowerPointRunning, Options.Pdf_Click);
			OptionsHelp.Click += Options.Help_Click;
			OptionsNew.Click += Options.New_Click;
			OptionsProgramAdd.Click += Options.AddProgram_Click;
			OptionsProgramDelete.Click += Options.DeleteProgram_Click;
			#endregion

			#region Rate Card Events
			RateCard = new RateCardControl(BusinessWrapper.Instance.RateCardManager, RateCardCombo);
			RateCardHelp.Click += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("ratecard");
			#endregion

			#region Gallery 1
			Gallery1 = new MediaGallery1Control();
			Gallery1Help.Click += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("gallery1");
			#endregion

			#region Gallery 2
			Gallery2 = new MediaGallery2Control();
			Gallery2Help.Click += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("gallery2");
			#endregion

			ConfigureTabPages();

			UpdateOutputButtonsAccordingThemeStatus();

			ConfigureSpecialButtons();

			Ribbon_SelectedRibbonTabChanged(Ribbon, EventArgs.Empty);
			Ribbon.SelectedRibbonTabChanged -= Ribbon_SelectedRibbonTabChanged;
			Ribbon.SelectedRibbonTabChanged += Ribbon_SelectedRibbonTabChanged;
		}

		public void RemoveInstance()
		{
			HomeControl.Dispose();
			WeeklySchedule.Dispose();
			MonthlySchedule.Dispose();
			DigitalProductContainer.Dispose();
			DigitalPackage.Dispose();
			BroadcastCalendar.Dispose();
			CustomCalendar.Dispose();
			SummaryLight.Dispose();
			SummaryFull.Dispose();
			Strategy.Dispose();
			Snapshot.Dispose();
			Options.Dispose();
			Gallery1.Dispose();
			Gallery2.Dispose();
			RateCard.Dispose();
			FloaterRequested = null;
			SetDefaultCulture();
		}

		public void LoadData()
		{
			MediaMetaData.Instance.SettingsManager.InitThemeHelper(BusinessWrapper.Instance.ThemeManager);
			HomeControl.LoadSchedule(false);
			WeeklySchedule.LoadSchedule(false);
			MonthlySchedule.LoadSchedule(false);
			DigitalProductContainer.LoadSchedule(false);
			DigitalPackage.LoadSchedule(false);
			BroadcastCalendar.LoadCalendar(false);
			CustomCalendar.LoadCalendar(false);
			SummaryLight.LoadData(false);
			SummaryFull.LoadData(false);
			Strategy.LoadSchedule(false);
			Snapshot.LoadSchedule(false);
			Options.LoadSchedule(false);

			BusinessWrapper.Instance.RateCardManager.LoadRateCards();
			TabRateCard.Enabled = BusinessWrapper.Instance.RateCardManager.RateCardFolders.Any();
		}

		private void ConfigureTabPages()
		{
			Ribbon.Items.Clear();
			var tabPages = new List<BaseItem>();
			foreach (var tabPageConfig in BusinessWrapper.Instance.TabPageManager.TabPageSettings)
			{
				switch (tabPageConfig.Id)
				{
					case "Home":
						TabHome.Text = tabPageConfig.Name;
						tabPages.Add(TabHome);
						break;
					case "Weekly Schedule":
						TabWeeklySchedule.Text = tabPageConfig.Name;
						tabPages.Add(TabWeeklySchedule);
						break;
					case "Monthly Schedule":
						TabMonthlySchedule.Text = tabPageConfig.Name;
						tabPages.Add(TabMonthlySchedule);
						break;
					case "Digital Slides":
						TabDigitalProduct.Text = tabPageConfig.Name;
						tabPages.Add(TabDigitalProduct);
						break;
					case "Digital PKG":
						TabDigitalPackage.Text = tabPageConfig.Name;
						tabPages.Add(TabDigitalPackage);
						break;
					case "Calendar":
						TabCalendar1.Text = tabPageConfig.Name;
						tabPages.Add(TabCalendar1);
						break;
					case "Calendar2":
						TabCalendar2.Text = tabPageConfig.Name;
						tabPages.Add(TabCalendar2);
						break;
					case "Gallery1":
						TabGallery1.Text = tabPageConfig.Name;
						tabPages.Add(TabGallery1);
						break;
					case "Gallery2":
						TabGallery2.Text = tabPageConfig.Name;
						tabPages.Add(TabGallery2);
						break;
					case "Rate Card":
						TabRateCard.Text = tabPageConfig.Name;
						tabPages.Add(TabRateCard);
						break;
					case "Snapshot":
						TabSnapshot.Text = tabPageConfig.Name;
						tabPages.Add(TabSnapshot);
						break;
					case "Options":
						TabOptions.Text = tabPageConfig.Name;
						tabPages.Add(TabOptions);
						break;
					case "Summary1":
						TabSummaryLight.Text = tabPageConfig.Name;
						tabPages.Add(TabSummaryLight);
						break;
					case "Summary2":
						TabSummaryFull.Text = tabPageConfig.Name;
						tabPages.Add(TabSummaryFull);
						break;
					case "Strategy":
						TabStrategy.Text = tabPageConfig.Name;
						tabPages.Add(TabStrategy);
						break;
				}
			}
			Ribbon.Items.AddRange(tabPages.ToArray());
		}

		public void SaveSchedule(RegularSchedule localSchedule, bool nameChanged, bool quickSave, bool updateDigital, bool calendarTypeChanged, Control sender)
		{
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nSaving settings...";
				form.TopMost = true;
				var thread = new Thread(() => BusinessWrapper.Instance.ScheduleManager.SaveSchedule(localSchedule, quickSave, updateDigital, calendarTypeChanged, sender));
				form.Show();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
			if (nameChanged)
			{
				var options = new Dictionary<string, object>();
				options.Add("Advertiser", localSchedule.BusinessName);
				if (localSchedule.Section.Programs.Any())
				{
					options.Add("TotalSpots", localSchedule.Section.TotalSpots);
					options.Add("AverageRate", localSchedule.Section.AvgRate);
					options.Add("GrossInvestment", localSchedule.Section.TotalCost);
				}
				BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("Saved As", localSchedule.Name));
			}
			if (ScheduleChanged != null)
				ScheduleChanged(this, EventArgs.Empty);
		}

		public void UpdateScheduleTabs(bool enable)
		{
			TabWeeklySchedule.Enabled = enable;
			TabMonthlySchedule.Enabled = enable;
			TabCalendar2.Enabled = enable;
			TabSnapshot.Enabled = enable;
			TabOptions.Enabled = enable;
		}

		public void UpdateOutputTabs(bool enable)
		{
			TabSummaryLight.Enabled = enable;
			TabSummaryFull.Enabled = enable;
			TabStrategy.Enabled = enable;
		}

		public void UpdateCalendarTabs(bool enable)
		{
			TabCalendar1.Enabled = enable;
		}

		public void UpdateDigitalProductTab(bool enable)
		{
			TabDigitalProduct.Enabled = enable;
			TabDigitalPackage.Enabled = enable && DigitalPackage.SlidesAvailable;
		}

		public void UpdateOutputButtonsAccordingThemeStatus()
		{
			if (!BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.None).Any())
			{
				var selectorToolTip = new SuperTooltipInfo("Important Info", "", "Click to get more info why output is disabled", null, null, eTooltipColor.Gray);
				var themesDisabledHandler = new Action(() => BusinessWrapper.Instance.HelpManager.OpenHelpLink("NoTheme"));

				WeeklySchedulePowerPoint.Visible = false;
				(WeeklySchedulePowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(WeeklyScheduleEmail.ContainerControl as RibbonBar).Visible = false;
				(WeeklySchedulePreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(WeeklyScheduleTheme, selectorToolTip);
				WeeklyScheduleTheme.Click += (o, e) => themesDisabledHandler();

				MonthlySchedulePowerPoint.Visible = false;
				(MonthlySchedulePowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(MonthlyScheduleEmail.ContainerControl as RibbonBar).Visible = false;
				(MonthlySchedulePreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(MonthlyScheduleTheme, selectorToolTip);
				MonthlyScheduleTheme.Click += (o, e) => themesDisabledHandler();

				DigitalProductPowerPoint.Visible = false;
				(DigitalProductPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(DigitalProductEmail.ContainerControl as RibbonBar).Visible = false;
				(DigitalProductPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(DigitalProductTheme, selectorToolTip);
				DigitalProductTheme.Click += (o, e) => themesDisabledHandler();

				DigitalPackagePowerPoint.Visible = false;
				(DigitalPackagePowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(DigitalPackageEmail.ContainerControl as RibbonBar).Visible = false;
				(DigitalPackagePreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(DigitalPackageTheme, selectorToolTip);
				DigitalPackageTheme.Click += (o, e) => themesDisabledHandler();

				SummaryLightPowerPoint.Visible = false;
				(SummaryLightPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(SummaryLightEmail.ContainerControl as RibbonBar).Visible = false;
				(SummaryLightPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(SummaryLightTheme, selectorToolTip);
				SummaryLightTheme.Click += (o, e) => themesDisabledHandler();

				SummaryFullPowerPoint.Visible = false;
				(SummaryFullPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(SummaryFullEmail.ContainerControl as RibbonBar).Visible = false;
				(SummaryFullPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(SummaryFullTheme, selectorToolTip);
				SummaryFullTheme.Click += (o, e) => themesDisabledHandler();

				StrategyPowerPoint.Visible = false;
				(StrategyPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(StrategyEmail.ContainerControl as RibbonBar).Visible = false;
				(StrategyPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(StrategyTheme, selectorToolTip);
				StrategyTheme.Click += (o, e) => themesDisabledHandler();

				SnapshotPowerPoint.Visible = false;
				(SnapshotPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(SnapshotEmail.ContainerControl as RibbonBar).Visible = false;
				(SnapshotPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(SnapshotTheme, selectorToolTip);
				SnapshotTheme.Click += (o, e) => themesDisabledHandler();

				OptionsPowerPoint.Visible = false;
				(OptionsPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(OptionsEmail.ContainerControl as RibbonBar).Visible = false;
				(OptionsPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(OptionsTheme, selectorToolTip);
				OptionsTheme.Click += (o, e) => themesDisabledHandler();
			}
			else
			{
				var selectorToolTip = new SuperTooltipInfo("Slide Theme", "", "Select the PowerPoint Slide theme you want to use for this schedule", null, null, eTooltipColor.Gray);
				Supertip.SetSuperTooltip(WeeklyScheduleTheme, selectorToolTip);
				Supertip.SetSuperTooltip(MonthlyScheduleTheme, selectorToolTip);
				Supertip.SetSuperTooltip(DigitalProductTheme, selectorToolTip);
				Supertip.SetSuperTooltip(DigitalPackageTheme, selectorToolTip);
				Supertip.SetSuperTooltip(SummaryLightTheme, selectorToolTip);
				Supertip.SetSuperTooltip(SummaryFullTheme, selectorToolTip);
				Supertip.SetSuperTooltip(StrategyTheme, selectorToolTip);
				Supertip.SetSuperTooltip(SnapshotTheme, selectorToolTip);
				Supertip.SetSuperTooltip(OptionsTheme, selectorToolTip);
			}

			Ribbon.SelectedRibbonTabChanged += (o, e) =>
			{
				(WeeklySchedulePowerPoint.ContainerControl as RibbonBar).Text = (WeeklyScheduleTheme.Tag as Theme).Name;
				(MonthlySchedulePowerPoint.ContainerControl as RibbonBar).Text = (MonthlyScheduleTheme.Tag as Theme).Name;
				(DigitalProductPowerPoint.ContainerControl as RibbonBar).Text = (DigitalProductTheme.Tag as Theme).Name;
				(DigitalPackagePowerPoint.ContainerControl as RibbonBar).Text = (DigitalPackageTheme.Tag as Theme).Name;
				(SummaryLightPowerPoint.ContainerControl as RibbonBar).Text = (SummaryLightTheme.Tag as Theme).Name;
				(SummaryFullPowerPoint.ContainerControl as RibbonBar).Text = (SummaryFullTheme.Tag as Theme).Name;
				(StrategyPowerPoint.ContainerControl as RibbonBar).Text = (StrategyTheme.Tag as Theme).Name;
				(SnapshotPowerPoint.ContainerControl as RibbonBar).Text = (SnapshotTheme.Tag as Theme).Name;
				(OptionsPowerPoint.ContainerControl as RibbonBar).Text = (OptionsTheme.Tag as Theme).Name;
			};
		}

		private void ConfigureSpecialButtons()
		{
			var specialLinkContainers = new[]
			{
				HomeSpecialButtons,
				WeeklyScheduleSpecialButtons,
				MonthlyScheduleSpecialButtons,
				DigitalProductSpecialButtons,
				DigitalPackageSpecialButtons,
				Calendar1SpecialButtons,
				Calendar2SpecialButtons,
				SummaryLightSpecialButtons,
				SummaryFullSpecialButtons,
				StrategySpecialButtons,
				SnapshotSpecialButtons,
				OptionsSpecialButtons,
				RateCardSpecialButtons,
				Gallery1SpecialButtons,
				Gallery2SpecialButtons
			};
			foreach (var ribbonBar in specialLinkContainers)
			{
				if (Core.OnlineSchedule.ListManager.Instance.SpecialLinksEnable)
				{
					ribbonBar.Text = Core.OnlineSchedule.ListManager.Instance.SpecialLinksGroupName;
					var containerButton = new ButtonItem();
					containerButton.Image = Core.OnlineSchedule.ListManager.Instance.SpecialLinksGroupLogo;
					containerButton.AutoExpandOnClick = true;
					Supertip.SetSuperTooltip(containerButton, new SuperTooltipInfo("Links", "", "Helpful schedule building Links and resources", null, null, eTooltipColor.Gray));
					ribbonBar.Items.Add(containerButton);
					foreach (var specialLinkButton in Core.OnlineSchedule.ListManager.Instance.SpecialLinkButtons)
					{
						var clickAction = new Action(() => { specialLinkButton.Open(); });
						var button = new ButtonItem();
						button.Image = specialLinkButton.Logo;
						button.Text = String.Format("<b>{0}</b><p>{1}</p>", specialLinkButton.Name, specialLinkButton.Tooltip);
						button.Tag = specialLinkButton;
						button.Click += (o, e) => clickAction();
						containerButton.SubItems.Add(button);
					}
				}
				else
				{
					ribbonBar.Visible = false;
				}
			}
		}

		public void ShowFloater(Action afterShow)
		{
			var args = new FloaterRequestedEventArgs { AfterShow = afterShow, Logo = MediaMetaData.Instance.DataType == MediaDataType.TV ? Properties.Resources.TVRibbonLogo : Properties.Resources.RadioRibbonLogo };
			if (FloaterRequested != null)
				FloaterRequested(null, args);
		}

		private void Ribbon_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(new TabActivity(Ribbon.SelectedRibbonTabItem.Text, BusinessWrapper.Instance.ScheduleManager.CurrentAdvertiser));
			if (Ribbon.SelectedRibbonTabItem == TabRateCard)
				RateCard.LoadRateCards();
			if (Ribbon.SelectedRibbonTabItem == TabGallery1)
				Gallery1.InitControl();
			else if (Ribbon.SelectedRibbonTabItem == TabGallery2)
				Gallery2.InitControl();
		}

		public void SetDefaultCulture()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
			Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
		}

		public bool CheckPowerPointRunning()
		{
			if (RegularMediaSchedulePowerPointHelper.Instance.IsLinkedWithApplication) return true;
			if (Utilities.Instance.ShowWarningQuestion(String.Format("PowerPoint must be open if you want to build a SellerPoint Schedule.{0}Do you want to open PowerPoint now?", Environment.NewLine)) == DialogResult.Yes)
				ShowFloater(() => Utilities.Instance.RunPowerPointLoader());
			return false;
		}

		#region Command Controls

		#region Home
		public RibbonBar HomeSpecialButtons { get; set; }
		public ButtonItem HomeHelp { get; set; }
		public ButtonItem HomeSave { get; set; }
		public ButtonItem HomeSaveAs { get; set; }
		public ButtonItem HomeProductAdd { get; set; }
		public ButtonItem HomeProductClone { get; set; }
		public ComboBoxEdit HomeBusinessName { get; set; }
		public ComboBoxEdit HomeDecisionMaker { get; set; }
		public ComboBoxEdit HomeClientType { get; set; }
		public TextEdit HomeAccountNumberText { get; set; }
		public CheckBoxItem HomeAccountNumberCheck { get; set; }
		public DateEdit HomePresentationDate { get; set; }
		public DateEdit HomeFlightDatesStart { get; set; }
		public DateEdit HomeFlightDatesEnd { get; set; }
		public LabelItem HomeWeeks { get; set; }
		#endregion

		#region Weekly Schedule
		public RibbonBar WeeklyScheduleSpecialButtons { get; set; }
		public ButtonItem WeeklyScheduleProgramAdd { get; set; }
		public ButtonItem WeeklyScheduleProgramDelete { get; set; }
		public ButtonItem WeeklySchedulePreview { get; set; }
		public ButtonItem WeeklySchedulePowerPoint { get; set; }
		public ButtonItem WeeklyScheduleEmail { get; set; }
		public ButtonItem WeeklySchedulePdf { get; set; }
		public ButtonItem WeeklyScheduleTheme { get; set; }
		public ButtonItem WeeklyScheduleSave { get; set; }
		public ButtonItem WeeklyScheduleSaveAs { get; set; }
		public ButtonItem WeeklyScheduleHelp { get; set; }
		public RibbonBar WeeklyScheduleQuarterBar { get; set; }
		public ButtonItem WeeklyScheduleQuarterButton { get; set; }
		#endregion

		#region Monthly Schedule
		public RibbonBar MonthlyScheduleSpecialButtons { get; set; }
		public ButtonItem MonthlyScheduleProgramAdd { get; set; }
		public ButtonItem MonthlyScheduleProgramDelete { get; set; }
		public ButtonItem MonthlySchedulePreview { get; set; }
		public ButtonItem MonthlySchedulePowerPoint { get; set; }
		public ButtonItem MonthlyScheduleEmail { get; set; }
		public ButtonItem MonthlySchedulePdf { get; set; }
		public ButtonItem MonthlyScheduleTheme { get; set; }
		public ButtonItem MonthlyScheduleSave { get; set; }
		public ButtonItem MonthlyScheduleSaveAs { get; set; }
		public ButtonItem MonthlyScheduleHelp { get; set; }
		public RibbonBar MonthlyScheduleQuarterBar { get; set; }
		public ButtonItem MonthlyScheduleQuarterButton { get; set; }
		#endregion

		#region Digital Product
		public RibbonBar DigitalProductSpecialButtons { get; set; }
		public ButtonItem DigitalProductPreview { get; set; }
		public ButtonItem DigitalProductPowerPoint { get; set; }
		public ButtonItem DigitalProductEmail { get; set; }
		public ButtonItem DigitalProductPdf { get; set; }
		public ButtonItem DigitalProductTheme { get; set; }
		public ButtonItem DigitalProductSave { get; set; }
		public ButtonItem DigitalProductSaveAs { get; set; }
		public ButtonItem DigitalProductHelp { get; set; }
		#endregion

		#region Digital Package
		public RibbonBar DigitalPackageSpecialButtons { get; set; }
		public ButtonItem DigitalPackageHelp { get; set; }
		public ButtonItem DigitalPackageSave { get; set; }
		public ButtonItem DigitalPackageSaveAs { get; set; }
		public ButtonItem DigitalPackagePreview { get; set; }
		public ButtonItem DigitalPackageEmail { get; set; }
		public ButtonItem DigitalPackagePowerPoint { get; set; }
		public ButtonItem DigitalPackagePdf { get; set; }
		public ButtonItem DigitalPackageTheme { get; set; }
		#endregion

		#region Calendar 1
		public RibbonBar Calendar1SpecialButtons { get; set; }
		public ImageListBoxControl Calendar1MonthsList { get; set; }
		public ButtonItem Calendar1Copy { get; set; }
		public ButtonItem Calendar1Paste { get; set; }
		public ButtonItem Calendar1Clone { get; set; }
		public ButtonItem Calendar1Help { get; set; }
		public ButtonItem Calendar1Save { get; set; }
		public ButtonItem Calendar1SaveAs { get; set; }
		public ButtonItem Calendar1Preview { get; set; }
		public ButtonItem Calendar1Email { get; set; }
		public ButtonItem Calendar1PowerPoint { get; set; }
		public ButtonItem Calendar1Pdf { get; set; }
		#endregion

		#region Calendar 2
		public RibbonBar Calendar2SpecialButtons { get; set; }
		public ImageListBoxControl Calendar2MonthsList { get; set; }
		public ButtonItem Calendar2Copy { get; set; }
		public ButtonItem Calendar2Paste { get; set; }
		public ButtonItem Calendar2Clone { get; set; }
		public ButtonItem Calendar2Help { get; set; }
		public ButtonItem Calendar2Save { get; set; }
		public ButtonItem Calendar2SaveAs { get; set; }
		public ButtonItem Calendar2Preview { get; set; }
		public ButtonItem Calendar2Email { get; set; }
		public ButtonItem Calendar2PowerPoint { get; set; }
		public ButtonItem Calendar2Pdf { get; set; }
		#endregion

		#region Summary Light
		public RibbonBar SummaryLightSpecialButtons { get; set; }
		public ButtonItem SummaryLightHelp { get; set; }
		public ButtonItem SummaryLightSave { get; set; }
		public ButtonItem SummaryLightSaveAs { get; set; }
		public ButtonItem SummaryLightPreview { get; set; }
		public ButtonItem SummaryLightEmail { get; set; }
		public ButtonItem SummaryLightPowerPoint { get; set; }
		public ButtonItem SummaryLightPdf { get; set; }
		public ButtonItem SummaryLightTheme { get; set; }
		public CheckEdit SummaryLightSlideOutputToggle { get; set; }
		public CheckEdit SummaryLightTableOutputToggle { get; set; }
		#endregion

		#region Summary Full
		public RibbonBar SummaryFullSpecialButtons { get; set; }
		public ButtonItem SummaryFullHelp { get; set; }
		public ButtonItem SummaryFullSave { get; set; }
		public ButtonItem SummaryFullSaveAs { get; set; }
		public ButtonItem SummaryFullPreview { get; set; }
		public ButtonItem SummaryFullEmail { get; set; }
		public ButtonItem SummaryFullPowerPoint { get; set; }
		public ButtonItem SummaryFullPdf { get; set; }
		public ButtonItem SummaryFullTheme { get; set; }
		public CheckEdit SummaryFullSlideOutputToggle { get; set; }
		public CheckEdit SummaryFullTableOutputToggle { get; set; }
		#endregion

		#region Strategy
		public RibbonBar StrategySpecialButtons { get; set; }
		public ButtonItem StrategyHelp { get; set; }
		public ButtonItem StrategySave { get; set; }
		public ButtonItem StrategySaveAs { get; set; }
		public ButtonItem StrategyPreview { get; set; }
		public ButtonItem StrategyEmail { get; set; }
		public ButtonItem StrategyPowerPoint { get; set; }
		public ButtonItem StrategyPdf { get; set; }
		public ButtonItem StrategyTheme { get; set; }
		public CheckEdit StrategyShowStationToggle { get; set; }
		public CheckEdit StrategyShowDescriptionToggle { get; set; }
		#endregion

		#region Snapshot
		public RibbonBar SnapshotSpecialButtons { get; set; }
		public ButtonItem SnapshotNew { get; set; }
		public ButtonItem SnapshotProgramAdd { get; set; }
		public ButtonItem SnapshotProgramDelete { get; set; }
		public ButtonItem SnapshotPreview { get; set; }
		public ButtonItem SnapshotPowerPoint { get; set; }
		public ButtonItem SnapshotPdf { get; set; }
		public ButtonItem SnapshotEmail { get; set; }
		public ButtonItem SnapshotTheme { get; set; }
		public ButtonItem SnapshotSave { get; set; }
		public ButtonItem SnapshotSaveAs { get; set; }
		public ButtonItem SnapshotHelp { get; set; }
		public RibbonBar SnapshotQuarterBar { get; set; }
		public ButtonItem SnapshotQuarterButton { get; set; }
		#endregion

		#region Options
		public RibbonBar OptionsSpecialButtons { get; set; }
		public ButtonItem OptionsNew { get; set; }
		public ButtonItem OptionsProgramAdd { get; set; }
		public ButtonItem OptionsProgramDelete { get; set; }
		public ButtonItem OptionsPreview { get; set; }
		public ButtonItem OptionsPowerPoint { get; set; }
		public ButtonItem OptionsPdf { get; set; }
		public ButtonItem OptionsEmail { get; set; }
		public ButtonItem OptionsTheme { get; set; }
		public ButtonItem OptionsSave { get; set; }
		public ButtonItem OptionsSaveAs { get; set; }
		public ButtonItem OptionsHelp { get; set; }
		public RibbonBar OptionsQuarterBar { get; set; }
		public ButtonItem OptionsQuarterButton { get; set; }
		#endregion

		#region Rate Card
		public RibbonBar RateCardSpecialButtons { get; set; }
		public ButtonItem RateCardHelp { get; set; }
		public ComboBoxEdit RateCardCombo { get; set; }
		#endregion

		#region Gallery1
		public RibbonPanel Gallery1Panel { get; set; }
		public RibbonBar Gallery1SpecialButtons { get; set; }
		public RibbonBar Gallery1BrowseBar { get; set; }
		public RibbonBar Gallery1ImageBar { get; set; }
		public RibbonBar Gallery1ZoomBar { get; set; }
		public RibbonBar Gallery1CopyBar { get; set; }
		public ItemContainer Gallery1BrowseModeContainer { get; set; }
		public ButtonItem Gallery1View { get; set; }
		public ButtonItem Gallery1Edit { get; set; }
		public ButtonItem Gallery1ImageSelect { get; set; }
		public ButtonItem Gallery1ImageCrop { get; set; }
		public ButtonItem Gallery1ZoomIn { get; set; }
		public ButtonItem Gallery1ZoomOut { get; set; }
		public ButtonItem Gallery1Copy { get; set; }
		public ButtonItem Gallery1Help { get; set; }
		public ComboBoxEdit Gallery1Sections { get; set; }
		public ComboBoxEdit Gallery1Groups { get; set; }
		#endregion

		#region Gallery2
		public RibbonPanel Gallery2Panel { get; set; }
		public RibbonBar Gallery2SpecialButtons { get; set; }
		public RibbonBar Gallery2BrowseBar { get; set; }
		public RibbonBar Gallery2ImageBar { get; set; }
		public RibbonBar Gallery2ZoomBar { get; set; }
		public RibbonBar Gallery2CopyBar { get; set; }
		public ItemContainer Gallery2BrowseModeContainer { get; set; }
		public ButtonItem Gallery2View { get; set; }
		public ButtonItem Gallery2Edit { get; set; }
		public ButtonItem Gallery2ImageSelect { get; set; }
		public ButtonItem Gallery2ImageCrop { get; set; }
		public ButtonItem Gallery2ZoomIn { get; set; }
		public ButtonItem Gallery2ZoomOut { get; set; }
		public ButtonItem Gallery2Copy { get; set; }
		public ButtonItem Gallery2Help { get; set; }
		public ComboBoxEdit Gallery2Sections { get; set; }
		public ComboBoxEdit Gallery2Groups { get; set; }
		#endregion

		#endregion

		#region Forms
		public HomeControl HomeControl { get; private set; }
		public WeeklyScheduleControl WeeklySchedule { get; private set; }
		public MonthlyScheduleControl MonthlySchedule { get; private set; }
		public DigitalProductContainerControl DigitalProductContainer { get; private set; }
		public MediaWebPackageControl DigitalPackage { get; private set; }
		public BroadcastCalendarControl BroadcastCalendar { get; private set; }
		public CustomCalendarControl CustomCalendar { get; private set; }
		public MediaSummaryLight SummaryLight { get; private set; }
		public MediaSummaryFull SummaryFull { get; private set; }
		public ProgramStrategyControl Strategy { get; private set; }
		public SnapshotContainer Snapshot { get; private set; }
		public OptionsContainer Options { get; private set; }
		public RateCardControl RateCard { get; private set; }
		public GalleryControl Gallery1 { get; private set; }
		public GalleryControl Gallery2 { get; private set; }
		#endregion
	}
}
