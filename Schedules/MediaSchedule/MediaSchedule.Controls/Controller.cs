using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Asa.MediaSchedule.Controls.PresentationClasses.SettingsControls;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using Asa.CommonGUI.Common;
using Asa.CommonGUI.Floater;
using Asa.CommonGUI.Gallery;
using Asa.CommonGUI.RateCard;
using Asa.CommonGUI.SlideSettingsEditors;
using Asa.CommonGUI.ToolForms;
using Asa.Core.Common;
using Asa.Core.MediaSchedule;
using Asa.MediaSchedule.Controls.BusinessClasses;
using Asa.MediaSchedule.Controls.InteropClasses;
using Asa.MediaSchedule.Controls.PresentationClasses.Calendar;
using Asa.MediaSchedule.Controls.PresentationClasses.Digital;
using Asa.MediaSchedule.Controls.PresentationClasses.Gallery;
using Asa.MediaSchedule.Controls.PresentationClasses.OptionsControls;
using Asa.MediaSchedule.Controls.PresentationClasses.ScheduleControls;
using Asa.MediaSchedule.Controls.PresentationClasses.SnapshotControls;
using Asa.MediaSchedule.Controls.PresentationClasses.Summary;
using Asa.OnlineSchedule.Controls.InteropClasses;

namespace Asa.MediaSchedule.Controls
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
		public RibbonTabItem TabProgramSchedule { get; set; }
		public RibbonTabItem TabDigitalProduct { get; set; }
		public RibbonTabItem TabDigitalPackage { get; set; }
		public RibbonTabItem TabCalendar1 { get; set; }
		public RibbonTabItem TabCalendar2 { get; set; }
		public RibbonTabItem TabSummary { get; set; }
		public RibbonTabItem TabGallery1 { get; set; }
		public RibbonTabItem TabGallery2 { get; set; }
		public RibbonTabItem TabRateCard { get; set; }
		public RibbonTabItem TabSnapshot { get; set; }
		public RibbonTabItem TabOptions { get; set; }

		public async Task InitBusinessObjects()
		{
			await AppProfileManager.Instance.LoadProfile();

			await Core.MediaSchedule.ResourceManager.Instance.Load();

			PowerPointManager.Instance.Init(RegularMediaSchedulePowerPointHelper.Instance);

			MasterWizardManager.Instance.Load();

			MediaMetaData.Instance.SettingsManager.InitThemeHelper(BusinessObjects.Instance.ThemeManager);
			MediaMetaData.Instance.SettingsManager.LoadSettings();

			Core.Common.ListManager.Instance.Init();
			MediaMetaData.Instance.ListManager.Load();
			Core.OnlineSchedule.ListManager.Instance.Load(Core.Common.ResourceManager.Instance.OnlineListsFile);

			BusinessObjects.Instance.Init();
			BusinessObjects.Instance.ThemeManager.ThemesChanged += (o, e) => UpdateOutputButtonsAccordingThemeStatus();
		}

		public void InitForm()
		{
			SetDefaultCulture();

			ConfigureTabPages();

			InitControls();

			ConfigureThemeButtons();

			ConfigureSpecialButtons();

			SlideSettingsButton.Visible =
				MasterWizardManager.Instance.MasterWizards.Count > 1 ||
				(MasterWizardManager.Instance.MasterWizards.Count == 1 && SlideSettings.GetAvailableConfigurations().Count(MasterWizardManager.Instance.MasterWizards.First().Value.HasSlideConfiguration) > 1);

			BusinessObjects.Instance.ActivityManager.AddActivity(new UserActivity("Application Started"));

			Ribbon_SelectedRibbonTabChanged(Ribbon, EventArgs.Empty);
			Ribbon.SelectedRibbonTabChanged -= Ribbon_SelectedRibbonTabChanged;
			Ribbon.SelectedRibbonTabChanged += Ribbon_SelectedRibbonTabChanged;
		}

		private void InitControls()
		{
			SlideSettingsButton.Click += OnSlideSettingsClick;

			#region Schedule Settings
			if (TabHome.Visible)
			{
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
				HomeBusinessName.Enter += TextEditorsHelper.Editor_Enter;
				HomeBusinessName.MouseDown += TextEditorsHelper.Editor_MouseDown;
				HomeBusinessName.MouseUp += TextEditorsHelper.Editor_MouseUp;
				HomeDecisionMaker.Enter += TextEditorsHelper.Editor_Enter;
				HomeDecisionMaker.MouseDown += TextEditorsHelper.Editor_MouseDown;
				HomeDecisionMaker.MouseUp += TextEditorsHelper.Editor_MouseUp;

				HomeBusinessName.TabIndex = 0;
				HomeBusinessName.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
				HomeDecisionMaker.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
				HomeClientType.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
				HomePresentationDate.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
				HomeFlightDatesStart.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
				HomeFlightDatesEnd.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			}
			#endregion

			#region Program Schedule
			if (TabProgramSchedule.Visible)
			{
				ProgramSchedule = new ScheduleContainer();
				ProgramScheduleSave.Click += ProgramSchedule.Save_Click;
				ProgramScheduleSaveAs.Click += ProgramSchedule.SaveAs_Click;
				ProgramScheduleHelp.Click += ProgramSchedule.Help_Click;
				ProgramScheduleNew.Click += ProgramSchedule.OnAddSection;
				ProgramScheduleProgramAdd.Click += ProgramSchedule.OnAddProgram;
				ProgramScheduleProgramDelete.Click += ProgramSchedule.OnDeleteProgram;
				ProgramSchedulePowerPoint.AddEventHandler(CheckPowerPointRunning, ProgramSchedule.OnPowerPointOutput);
				ProgramSchedulePreview.AddEventHandler(CheckPowerPointRunning, ProgramSchedule.OnOutputPreview);
				ProgramScheduleEmail.AddEventHandler(CheckPowerPointRunning, ProgramSchedule.OnEmailOutput);
				ProgramSchedulePdf.AddEventHandler(CheckPowerPointRunning, ProgramSchedule.OnPdfOutput);
			}
			#endregion

			#region Digital Product
			if (TabDigitalProduct.Visible)
			{
				DigitalProductContainer = new DigitalProductContainerControl(FormMain);
				DigitalProductSave.Click += DigitalProductContainer.Save_Click;
				DigitalProductSaveAs.Click += DigitalProductContainer.SaveAs_Click;
				DigitalProductPowerPoint.AddEventHandler(CheckPowerPointRunning, DigitalProductContainer.PowerPoint_Click);
				DigitalProductEmail.AddEventHandler(CheckPowerPointRunning, DigitalProductContainer.Email_Click);
				DigitalProductHelp.Click += DigitalProductContainer.Help_Click;
				DigitalProductPreview.AddEventHandler(CheckPowerPointRunning, DigitalProductContainer.Preview_Click);
				DigitalProductPdf.AddEventHandler(CheckPowerPointRunning, DigitalProductContainer.Pdf_Click);
			}
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
			if (TabCalendar1.Visible)
			{
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
			}
			#endregion

			#region Calendar2
			if (TabCalendar2.Visible)
			{
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
			}
			#endregion

			#region Summary Light
			if (TabSummary.Visible)
			{
				Summary = new SummaryContainer();

				SummarySave.Click += Summary.Save_Click;
				SummarySaveAs.Click += Summary.SaveAs_Click;
				SummaryHelp.Click += Summary.Help_Click;
				SummaryPowerPoint.AddEventHandler(CheckPowerPointRunning, Summary.OnPowerPointOutput);
				SummaryPreview.AddEventHandler(CheckPowerPointRunning, Summary.OnOutputPreview);
				SummaryEmail.AddEventHandler(CheckPowerPointRunning, Summary.OnEmailOutput);
				SummaryPdf.AddEventHandler(CheckPowerPointRunning, Summary.OnPdfOutput);
			}
			#endregion

			#region Snapshot
			if (TabSnapshot.Visible)
			{
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
			}
			#endregion

			#region Options
			if (TabOptions.Visible)
			{
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
			}
			#endregion

			#region Rate Card Events
			if (TabRateCard.Visible)
			{
				RateCard = new RateCardControl(BusinessObjects.Instance.RateCardManager, RateCardCombo);
				RateCardHelp.Click += (o, e) => BusinessObjects.Instance.HelpManager.OpenHelpLink("ratecard");
			}
			#endregion

			#region Gallery 1
			if (TabGallery1.Visible)
			{
				Gallery1 = new MediaGallery1Control();
				Gallery1Help.Click += (o, e) => BusinessObjects.Instance.HelpManager.OpenHelpLink("gallery1");
			}
			#endregion

			#region Gallery 2
			if (TabGallery2.Visible)
			{
				Gallery2 = new MediaGallery2Control();
				Gallery2Help.Click += (o, e) => BusinessObjects.Instance.HelpManager.OpenHelpLink("gallery2");
			}
			#endregion
		}

		public void LoadData()
		{
			if (TabHome.Visible)
				HomeControl.LoadSchedule(false);
			if (TabProgramSchedule.Visible)
				ProgramSchedule.LoadSchedule(false);
			if (TabDigitalProduct.Visible)
				DigitalProductContainer.LoadSchedule(false);
			if (TabDigitalPackage.Visible)
				DigitalPackage.LoadSchedule(false);
			if (TabCalendar1.Visible)
				BroadcastCalendar.LoadCalendar(false);
			if (TabCalendar2.Visible)
				CustomCalendar.LoadCalendar(false);
			if (TabSummary.Visible)
				Summary.LoadSchedule(false);
			if (TabSnapshot.Visible)
				Snapshot.LoadSchedule(false);
			if (TabOptions.Visible)
				Options.LoadSchedule(false);
			TabRateCard.Enabled = TabRateCard.Visible && BusinessObjects.Instance.RateCardManager.RateCardFolders.Any();
		}

		private void ConfigureTabPages()
		{
			TabHome.Visible = false;
			TabProgramSchedule.Visible = false;
			TabDigitalProduct.Visible = false;
			TabDigitalPackage.Visible = false;
			TabCalendar1.Visible = false;
			TabCalendar2.Visible = false;
			TabSummary.Visible = false;
			TabGallery1.Visible = false;
			TabGallery2.Visible = false;
			TabRateCard.Visible = false;
			TabSnapshot.Visible = false;
			TabOptions.Visible = false;

			Ribbon.Items.Clear();
			var tabPages = new List<BaseItem>();
			foreach (var tabPageConfig in BusinessObjects.Instance.TabPageManager.TabPageSettings)
			{
				switch (tabPageConfig.Id)
				{
					case "Home":
						TabHome.Text = tabPageConfig.Name;
						TabHome.Visible = true;
						tabPages.Add(TabHome);
						break;
					case "Weekly Schedule":
					case "Monthly Schedule":
						if (!tabPages.Contains(TabProgramSchedule))
						{
							TabProgramSchedule.Visible = true;
							tabPages.Add(TabProgramSchedule);
						}
						break;
					case "Digital Slides":
						TabDigitalProduct.Text = tabPageConfig.Name;
						TabDigitalProduct.Visible = true;
						tabPages.Add(TabDigitalProduct);
						break;
					case "Digital PKG":
						TabDigitalPackage.Text = tabPageConfig.Name;
						TabDigitalPackage.Visible = true;
						tabPages.Add(TabDigitalPackage);
						break;
					case "Calendar":
						TabCalendar1.Text = tabPageConfig.Name;
						TabCalendar1.Visible = true;
						tabPages.Add(TabCalendar1);
						break;
					case "Calendar2":
						TabCalendar2.Text = tabPageConfig.Name;
						TabCalendar2.Visible = true;
						tabPages.Add(TabCalendar2);
						break;
					case "Gallery1":
						TabGallery1.Text = tabPageConfig.Name;
						TabGallery1.Visible = true;
						tabPages.Add(TabGallery1);
						break;
					case "Gallery2":
						TabGallery2.Text = tabPageConfig.Name;
						TabGallery2.Visible = true;
						tabPages.Add(TabGallery2);
						break;
					case "Rate Card":
						TabRateCard.Text = tabPageConfig.Name;
						TabRateCard.Visible = true;
						tabPages.Add(TabRateCard);
						break;
					case "Snapshot":
						TabSnapshot.Text = tabPageConfig.Name;
						TabSnapshot.Visible = true;
						tabPages.Add(TabSnapshot);
						break;
					case "Options":
						TabOptions.Text = tabPageConfig.Name;
						TabOptions.Visible = true;
						tabPages.Add(TabOptions);
						break;
					case "Summaries":
						TabSummary.Text = tabPageConfig.Name;
						TabSummary.Visible = true;
						tabPages.Add(TabSummary);
						break;
				}
			}
			Ribbon.Items.AddRange(tabPages.ToArray());
			Ribbon.Items.Add(SlideSettingsButton);
		}

		public void SaveSchedule(RegularSchedule localSchedule, bool nameChanged, bool quickSave, bool updateDigital, bool calendarTypeChanged, Control sender)
		{
			FormProgress.SetTitle("Chill-Out for a few seconds...\nSaving settings...");
			var thread = new Thread(() => BusinessObjects.Instance.ScheduleManager.SaveSchedule(localSchedule, quickSave, updateDigital, calendarTypeChanged, sender));
			FormProgress.ShowProgress();
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			FormProgress.CloseProgress();

			if (nameChanged)
			{
				var options = new Dictionary<string, object>();
				options.Add("Advertiser", localSchedule.BusinessName);
				if (localSchedule.ProgramSchedule.Sections.SelectMany(s => s.Programs).Any())
				{
					options.Add("TotalSpots", localSchedule.ProgramSchedule.TotalSpots);
					options.Add("AverageRate", localSchedule.ProgramSchedule.AvgRate);
					options.Add("GrossInvestment", localSchedule.ProgramSchedule.TotalCost);
				}
			}
			if (ScheduleChanged != null)
				ScheduleChanged(this, EventArgs.Empty);
		}

		public void UpdateScheduleTabs(bool enable)
		{
			TabProgramSchedule.Enabled = enable;
			TabCalendar1.Enabled = enable;
			TabCalendar2.Enabled = enable;
			TabSnapshot.Enabled = enable;
			TabOptions.Enabled = enable;
		}

		public void UpdateOutputTabs(bool enable)
		{
			TabSummary.Enabled = enable;
		}

		public void UpdateDigitalProductTab(bool enable)
		{
			TabDigitalProduct.Enabled = enable;
			TabDigitalPackage.Enabled = enable;
		}

		public void ConfigureThemeButtons()
		{
			UpdateOutputButtonsAccordingThemeStatus();
			Ribbon.SelectedRibbonTabChanged += (o, e) =>
			{
				if (TabProgramSchedule.Visible)
					(ProgramSchedulePowerPoint.ContainerControl as RibbonBar).Text = (ProgramScheduleTheme.Tag as Theme).Name;
				if (TabDigitalProduct.Visible)
					(DigitalProductPowerPoint.ContainerControl as RibbonBar).Text = (DigitalProductTheme.Tag as Theme).Name;
				if (TabDigitalPackage.Visible)
					(DigitalPackagePowerPoint.ContainerControl as RibbonBar).Text = (DigitalPackageTheme.Tag as Theme).Name;
				if (TabSummary.Visible)
					(SummaryPowerPoint.ContainerControl as RibbonBar).Text = (SummaryTheme.Tag as Theme).Name;
				if (TabSnapshot.Visible)
					(SnapshotPowerPoint.ContainerControl as RibbonBar).Text = (SnapshotTheme.Tag as Theme).Name;
				if (TabOptions.Visible)
					(OptionsPowerPoint.ContainerControl as RibbonBar).Text = (OptionsTheme.Tag as Theme).Name;

				if (TabProgramSchedule.Visible)
				{
					ProgramScheduleThemeBar.RecalcLayout();
					ProgramSchedulePanel.PerformLayout();
				}

				if (TabDigitalProduct.Visible)
				{
					DigitalProductThemeBar.RecalcLayout();
					DigitalProductPanel.PerformLayout();
				}

				if (TabDigitalPackage.Visible)
				{
					DigitalPackageThemeBar.RecalcLayout();
					DigitalPackagePanel.PerformLayout();
				}

				if (TabSummary.Visible)
				{
					SummaryThemeBar.RecalcLayout();
					SummaryPanel.PerformLayout();
				}

				if (TabSnapshot.Visible)
				{
					SnapshotThemeBar.RecalcLayout();
					SnapshotPanel.PerformLayout();
				}

				if (TabOptions.Visible)
				{
					OptionsThemeBar.RecalcLayout();
					OptionsPanel.PerformLayout();
				}
			};
		}

		private void UpdateOutputButtonsAccordingThemeStatus()
		{
			if (!BusinessObjects.Instance.ThemeManager.GetThemes(SlideType.None).Any())
			{
				var selectorToolTip = new SuperTooltipInfo("Important Info", "", "Click to get more info why output is disabled", null, null, eTooltipColor.Gray);

				ProgramSchedulePowerPoint.Visible = false;
				(ProgramSchedulePowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(ProgramScheduleEmail.ContainerControl as RibbonBar).Visible = false;
				(ProgramSchedulePreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(ProgramScheduleTheme, selectorToolTip);
				ProgramScheduleTheme.Click -= OnThemeClick;
				ProgramScheduleTheme.Click += OnThemeClick;

				DigitalProductPowerPoint.Visible = false;
				(DigitalProductPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(DigitalProductEmail.ContainerControl as RibbonBar).Visible = false;
				(DigitalProductPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(DigitalProductTheme, selectorToolTip);
				DigitalProductTheme.Click -= OnThemeClick;
				DigitalProductTheme.Click += OnThemeClick;

				DigitalPackagePowerPoint.Visible = false;
				(DigitalPackagePowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(DigitalPackageEmail.ContainerControl as RibbonBar).Visible = false;
				(DigitalPackagePreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(DigitalPackageTheme, selectorToolTip);
				DigitalPackageTheme.Click -= OnThemeClick;
				DigitalPackageTheme.Click += OnThemeClick;

				SummaryPowerPoint.Visible = false;
				(SummaryPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(SummaryEmail.ContainerControl as RibbonBar).Visible = false;
				(SummaryPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(SummaryTheme, selectorToolTip);
				SummaryTheme.Click -= OnThemeClick;
				SummaryTheme.Click += OnThemeClick;

				SnapshotPowerPoint.Visible = false;
				(SnapshotPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(SnapshotEmail.ContainerControl as RibbonBar).Visible = false;
				(SnapshotPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(SnapshotTheme, selectorToolTip);
				SnapshotTheme.Click -= OnThemeClick;
				SnapshotTheme.Click += OnThemeClick;

				OptionsPowerPoint.Visible = false;
				(OptionsPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(OptionsEmail.ContainerControl as RibbonBar).Visible = false;
				(OptionsPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(OptionsTheme, selectorToolTip);
				OptionsTheme.Click -= OnThemeClick;
				OptionsTheme.Click += OnThemeClick;
			}
			else
			{
				ProgramSchedulePowerPoint.Visible = true;
				(ProgramScheduleEmail.ContainerControl as RibbonBar).Visible = true;
				(ProgramSchedulePreview.ContainerControl as RibbonBar).Visible = true;
				ProgramScheduleTheme.Click -= OnThemeClick;

				DigitalProductPowerPoint.Visible = true;
				(DigitalProductEmail.ContainerControl as RibbonBar).Visible = true;
				(DigitalProductPreview.ContainerControl as RibbonBar).Visible = true;
				DigitalProductTheme.Click -= OnThemeClick;

				DigitalPackagePowerPoint.Visible = true;
				(DigitalPackageEmail.ContainerControl as RibbonBar).Visible = true;
				(DigitalPackagePreview.ContainerControl as RibbonBar).Visible = true;
				DigitalPackageTheme.Click -= OnThemeClick;

				SummaryPowerPoint.Visible = true;
				(SummaryEmail.ContainerControl as RibbonBar).Visible = true;
				(SummaryPreview.ContainerControl as RibbonBar).Visible = true;
				SummaryTheme.Click -= OnThemeClick;

				SnapshotPowerPoint.Visible = true;
				(SnapshotEmail.ContainerControl as RibbonBar).Visible = true;
				(SnapshotPreview.ContainerControl as RibbonBar).Visible = true;
				SnapshotTheme.Click -= OnThemeClick;

				OptionsPowerPoint.Visible = true;
				(OptionsEmail.ContainerControl as RibbonBar).Visible = true;
				(OptionsPreview.ContainerControl as RibbonBar).Visible = true;
				OptionsTheme.Click -= OnThemeClick;

				var selectorToolTip = new SuperTooltipInfo("Slide Theme", "", "Select the PowerPoint Slide theme you want to use for this schedule", null, null, eTooltipColor.Gray);
				Supertip.SetSuperTooltip(ProgramScheduleTheme, selectorToolTip);
				Supertip.SetSuperTooltip(DigitalProductTheme, selectorToolTip);
				Supertip.SetSuperTooltip(DigitalPackageTheme, selectorToolTip);
				Supertip.SetSuperTooltip(SummaryTheme, selectorToolTip);
				Supertip.SetSuperTooltip(SnapshotTheme, selectorToolTip);
				Supertip.SetSuperTooltip(OptionsTheme, selectorToolTip);
			}
		}

		private void OnThemeClick(object sender, EventArgs args)
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("NoTheme");
		}

		private void ConfigureSpecialButtons()
		{
			var specialLinkContainers = new[]
			{
				HomeSpecialButtons,
				ProgramScheduleSpecialButtons,
				DigitalProductSpecialButtons,
				DigitalPackageSpecialButtons,
				Calendar1SpecialButtons,
				Calendar2SpecialButtons,
				SummarySpecialButtons,
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
			var args = new FloaterRequestedEventArgs { AfterShow = afterShow, Logo = MediaMetaData.Instance.DataType == MediaDataType.TVSchedule ? Properties.Resources.TVRibbonLogo : Properties.Resources.RadioRibbonLogo };
			if (FloaterRequested != null)
				FloaterRequested(null, args);
		}

		private void Ribbon_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
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
			if (RegularMediaSchedulePowerPointHelper.Instance.IsLinkedWithApplication)
			{
				OnlineSchedulePowerPointHelper.Instance.Connect(false);
				return true;
			}
			if (Utilities.Instance.ShowWarningQuestion(String.Format("PowerPoint is required to run this application.{0}Do you want to go ahead and open PowerPoint?", Environment.NewLine)) == DialogResult.Yes)
				ShowFloater(() => PowerPointManager.Instance.RunPowerPointLoader());
			return false;
		}

		private void OnSlideSettingsClick(object sender, EventArgs e)
		{
			using (var form = new FormEditSlideSettings())
			{
				form.ShowDialog(FormMain);
			}
		}

		#region Command Controls
		public ButtonItem SlideSettingsButton { get; set; }

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

		#region Program Schedule
		public RibbonPanel ProgramSchedulePanel { get; set; }
		public RibbonBar ProgramScheduleThemeBar { get; set; }
		public RibbonBar ProgramScheduleSpecialButtons { get; set; }
		public ButtonItem ProgramScheduleNew { get; set; }
		public ButtonItem ProgramScheduleProgramAdd { get; set; }
		public ButtonItem ProgramScheduleProgramDelete { get; set; }
		public ButtonItem ProgramSchedulePreview { get; set; }
		public ButtonItem ProgramSchedulePowerPoint { get; set; }
		public ButtonItem ProgramScheduleEmail { get; set; }
		public ButtonItem ProgramSchedulePdf { get; set; }
		public ButtonItem ProgramScheduleTheme { get; set; }
		public ButtonItem ProgramScheduleSave { get; set; }
		public ButtonItem ProgramScheduleSaveAs { get; set; }
		public ButtonItem ProgramScheduleHelp { get; set; }
		#endregion

		#region Digital Product
		public RibbonPanel DigitalProductPanel { get; set; }
		public RibbonBar DigitalProductThemeBar { get; set; }
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
		public RibbonPanel DigitalPackagePanel { get; set; }
		public RibbonBar DigitalPackageThemeBar { get; set; }
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

		#region Summary
		public RibbonPanel SummaryPanel { get; set; }
		public RibbonBar SummaryThemeBar { get; set; }
		public RibbonBar SummarySpecialButtons { get; set; }
		public ButtonItem SummaryHelp { get; set; }
		public ButtonItem SummarySave { get; set; }
		public ButtonItem SummarySaveAs { get; set; }
		public ButtonItem SummaryPreview { get; set; }
		public ButtonItem SummaryEmail { get; set; }
		public ButtonItem SummaryPowerPoint { get; set; }
		public ButtonItem SummaryPdf { get; set; }
		public ButtonItem SummaryTheme { get; set; }
		#endregion

		#region Snapshot
		public RibbonPanel SnapshotPanel { get; set; }
		public RibbonBar SnapshotThemeBar { get; set; }
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
		public RibbonPanel OptionsPanel { get; set; }
		public RibbonBar OptionsThemeBar { get; set; }
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
		public ScheduleContainer ProgramSchedule { get; private set; }
		public DigitalProductContainerControl DigitalProductContainer { get; private set; }
		public MediaWebPackageControl DigitalPackage { get; private set; }
		public BroadcastCalendarControl BroadcastCalendar { get; private set; }
		public CustomCalendarControl CustomCalendar { get; private set; }
		public SummaryContainer Summary { get; private set; }
		public SnapshotContainer Snapshot { get; private set; }
		public OptionsContainer Options { get; private set; }
		public RateCardControl RateCard { get; private set; }
		public GalleryControl Gallery1 { get; private set; }
		public GalleryControl Gallery2 { get; private set; }
		#endregion
	}
}
