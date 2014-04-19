using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.CommonGUI.Gallery;
using NewBizWiz.CommonGUI.RateCard;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;
using NewBizWiz.MediaSchedule.Controls.PresentationClasses;

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
		public RibbonTabItem TabCalendar { get; set; }
		public RibbonTabItem TabGallery1 { get; set; }
		public RibbonTabItem TabGallery2 { get; set; }
		public RibbonTabItem TabRateCard { get; set; }
		public RibbonTabItem TabMarketing { get; set; }
		public RibbonTabItem TabProduction { get; set; }

		public void Init()
		{
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
			HomeFlightDatesEnd.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += HomeControl.CalcWeeksOnFlightDatesChange;
			HomeFlightDatesEnd.EditValueChanged += HomeControl.CalcWeeksOnFlightDatesChange;
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
			WeeklySchedulePowerPoint.Click += WeeklySchedule.PowerPoint_Click;
			WeeklySchedulePreview.Click += WeeklySchedule.Preview_Click;
			WeeklyScheduleEmail.Click += WeeklySchedule.Email_Click;
			WeeklyScheduleHelp.Click += WeeklySchedule.Help_Click;
			WeeklyScheduleOptions.CheckedChanged += WeeklySchedule.Options_CheckedChaged;
			WeeklyScheduleProgramAdd.Click += WeeklySchedule.AddProgram_Click;
			WeeklyScheduleProgramDelete.Click += WeeklySchedule.DeleteProgram_Click;
			WeeklyScheduleQuarterButton.CheckedChanged += WeeklySchedule.QuarterCheckedChanged;
			#endregion

			#region Monthly Schedule
			MonthlySchedule = new MonthlyScheduleControl();
			MonthlyScheduleSave.Click += MonthlySchedule.Save_Click;
			MonthlyScheduleSaveAs.Click += MonthlySchedule.SaveAs_Click;
			MonthlySchedulePowerPoint.Click += MonthlySchedule.PowerPoint_Click;
			MonthlySchedulePreview.Click += MonthlySchedule.Preview_Click;
			MonthlyScheduleEmail.Click += MonthlySchedule.Email_Click;
			MonthlyScheduleHelp.Click += MonthlySchedule.Help_Click;
			MonthlyScheduleOptions.CheckedChanged += MonthlySchedule.Options_CheckedChaged;
			MonthlyScheduleProgramAdd.Click += MonthlySchedule.AddProgram_Click;
			MonthlyScheduleProgramDelete.Click += MonthlySchedule.DeleteProgram_Click;
			MonthlyScheduleQuarterButton.CheckedChanged += WeeklySchedule.QuarterCheckedChanged;
			#endregion

			#region Digital Product
			DigitalProductContainer = new DigitalProductContainerControl(FormMain);
			DigitalProductSave.Click += DigitalProductContainer.Save_Click;
			DigitalProductSaveAs.Click += DigitalProductContainer.SaveAs_Click;
			DigitalProductPowerPoint.Click += DigitalProductContainer.PowerPoint_Click;
			DigitalProductEmail.Click += DigitalProductContainer.Email_Click;
			DigitalProductHelp.Click += DigitalProductContainer.Help_Click;
			DigitalProductPreview.Click += DigitalProductContainer.Preview_Click;
			#endregion

			#region Web Package
			DigitalPackage = new MediaWebPackageControl(FormMain);
			DigitalPackageSave.Click += DigitalPackage.Save_Click;
			DigitalPackageSaveAs.Click += DigitalPackage.SaveAs_Click;
			DigitalPackagePowerPoint.Click += DigitalPackage.PowerPoint_Click;
			DigitalPackagePreview.Click += DigitalPackage.Preview_Click;
			DigitalPackageEmail.Click += DigitalPackage.Email_Click;
			DigitalPackageHelp.Click += DigitalPackage.Help_Click;
			DigitalPackageOptions.CheckedChanged += DigitalPackage.TogledButton_CheckedChanged;
			#endregion

			#region Calendar
			BroadcastCalendar = new BroadcastCalendarControl();
			CalendarMonthsList.SelectedIndexChanged += BroadcastCalendar.MonthList_SelectedIndexChanged;
			CalendarSlideInfo.CheckedChanged += BroadcastCalendar.SlideInfo_CheckedChanged;
			CalendarCopy.Click += BroadcastCalendar.CalendarCopy_Click;
			CalendarPaste.Click += BroadcastCalendar.CalendarPaste_Click;
			CalendarClone.Click += BroadcastCalendar.CalendarClone_Click;
			CalendarSave.Click += BroadcastCalendar.Save_Click;
			CalendarSaveAs.Click += BroadcastCalendar.SaveAs_Click;
			CalendarPreview.Click += BroadcastCalendar.Preview_Click;
			CalendarPowerPoint.Click += BroadcastCalendar.PowerPoint_Click;
			CalendarEmail.Click += BroadcastCalendar.Email_Click;
			CalendarHelp.Click += BroadcastCalendar.Help_Click;
			#endregion

			#region Rate Card Events
			RateCard = new RateCardControl(BusinessWrapper.Instance.RateCardManager, RateCardCombo);
			RateCardHelp.Click += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("ratecard");
			#endregion

			#region Gallery 1
			Gallery1 = new MediaGallery1Control();
			Gallery1Help.Click += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("gallery");
			#endregion

			#region Gallery 2
			Gallery2 = new MediaGallery2Control();
			Gallery2Help.Click += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("gallery");
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
			Gallery1.Dispose();
			Gallery2.Dispose();
			RateCard.Dispose();
			FloaterRequested = null;
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
						TabCalendar.Text = tabPageConfig.Name;
						tabPages.Add(TabCalendar);
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
					case "Marketing":
						TabMarketing.Text = tabPageConfig.Name;
						tabPages.Add(TabMarketing);
						break;
					case "Production":
						TabProduction.Text = tabPageConfig.Name;
						tabPages.Add(TabProduction);
						break;
				}
			}
			Ribbon.Items.AddRange(tabPages.ToArray());
		}

		public void SaveSchedule(Schedule localSchedule, bool quickSave, Control sender)
		{
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nSaving settings...";
				form.TopMost = true;
				var thread = new Thread(delegate() { BusinessWrapper.Instance.ScheduleManager.SaveSchedule(localSchedule, quickSave, sender); });
				form.Show();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
			if (ScheduleChanged != null)
				ScheduleChanged(this, EventArgs.Empty);
		}

		public void UpdateScheduleTabs(bool enable)
		{
			TabWeeklySchedule.Enabled = enable;
			TabMonthlySchedule.Enabled = enable;
		}

		public void UpdateCalendarTab(bool enable)
		{
			TabCalendar.Enabled = enable;
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
			}
			else
			{
				var selectorToolTip = new SuperTooltipInfo("Slide Theme", "", "Select the PowerPoint Slide theme you want to use for this schedule", null, null, eTooltipColor.Gray);
				Supertip.SetSuperTooltip(WeeklyScheduleTheme, selectorToolTip);
				Supertip.SetSuperTooltip(MonthlyScheduleTheme, selectorToolTip);
				Supertip.SetSuperTooltip(DigitalProductTheme, selectorToolTip);
				Supertip.SetSuperTooltip(DigitalPackageTheme, selectorToolTip);
			}

			Ribbon.SelectedRibbonTabChanged += (o, e) =>
			{
				(WeeklySchedulePowerPoint.ContainerControl as RibbonBar).Text = (WeeklyScheduleTheme.Tag as Theme).Name;
				(MonthlySchedulePowerPoint.ContainerControl as RibbonBar).Text = (MonthlyScheduleTheme.Tag as Theme).Name;
				(DigitalProductPowerPoint.ContainerControl as RibbonBar).Text = (DigitalProductTheme.Tag as Theme).Name;
				(DigitalPackagePowerPoint.ContainerControl as RibbonBar).Text = (DigitalPackageTheme.Tag as Theme).Name;
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
				CalendarSpecialButtons,
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
			var args = new FloaterRequestedEventArgs { AfterShow = afterShow };
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
		public ButtonItem WeeklyScheduleOptions { get; set; }
		public ButtonItem WeeklySchedulePreview { get; set; }
		public ButtonItem WeeklySchedulePowerPoint { get; set; }
		public ButtonItem WeeklyScheduleEmail { get; set; }
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
		public ButtonItem MonthlyScheduleOptions { get; set; }
		public ButtonItem MonthlySchedulePreview { get; set; }
		public ButtonItem MonthlySchedulePowerPoint { get; set; }
		public ButtonItem MonthlyScheduleEmail { get; set; }
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
		public ButtonItem DigitalPackageTheme { get; set; }
		public ButtonItem DigitalPackageOptions { get; set; }
		#endregion

		#region Calendar
		public RibbonBar CalendarSpecialButtons { get; set; }
		public ImageListBoxControl CalendarMonthsList { get; set; }
		public ButtonItem CalendarSlideInfo { get; set; }
		public ButtonItem CalendarCopy { get; set; }
		public ButtonItem CalendarPaste { get; set; }
		public ButtonItem CalendarClone { get; set; }
		public ButtonItem CalendarHelp { get; set; }
		public ButtonItem CalendarSave { get; set; }
		public ButtonItem CalendarSaveAs { get; set; }
		public ButtonItem CalendarPreview { get; set; }
		public ButtonItem CalendarEmail { get; set; }
		public ButtonItem CalendarPowerPoint { get; set; }
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
		public RateCardControl RateCard { get; private set; }
		public GalleryControl Gallery1 { get; private set; }
		public GalleryControl Gallery2 { get; private set; }
		#endregion
	}
}
