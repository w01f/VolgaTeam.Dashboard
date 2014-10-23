using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.InputClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.AdPlan;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.Summary;
using NewBizWiz.AdSchedule.Controls.Properties;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.CommonGUI.Gallery;
using NewBizWiz.CommonGUI.RateCard;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using Schedule = NewBizWiz.Core.AdSchedule.Schedule;

namespace NewBizWiz.AdSchedule.Controls
{
	public class Controller
	{
		private static readonly Controller _instance = new Controller();
		private Controller() { }
		public static Controller Instance
		{
			get { return _instance; }
		}

		public event EventHandler<FloaterRequestedEventArgs> FloaterRequested;
		public event EventHandler<EventArgs> ScheduleChanged;

		public Form FormMain { get; set; }
		public SuperTooltip Supertip { get; set; }
		public RibbonControl Ribbon { get; set; }
		public RibbonTabItem TabHome { get; set; }
		public RibbonTabItem TabPrintProduct { get; set; }
		public RibbonTabItem TabDigitalProduct { get; set; }
		public RibbonTabItem TabDigitalPackage { get; set; }
		public RibbonTabItem TabBasicOverview { get; set; }
		public RibbonTabItem TabMultiSummary { get; set; }
		public RibbonTabItem TabSnapshot { get; set; }
		public RibbonTabItem TabAdPlan { get; set; }
		public RibbonTabItem TabDetailedGrid { get; set; }
		public RibbonTabItem TabMultiGrid { get; set; }
		public RibbonTabItem TabCalendar { get; set; }
		public RibbonTabItem TabSummaryLight { get; set; }
		public RibbonTabItem TabSummaryFull { get; set; }
		public RibbonTabItem TabRateCard { get; set; }
		public RibbonTabItem TabGallery1 { get; set; }
		public RibbonTabItem TabGallery2 { get; set; }

		public void Init()
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(new UserActivity("Application Started"));

			#region Schedule Settings
			ScheduleSettings = new ScheduleSettingsControl();
			HomeHelp.Click += ScheduleSettings.buttonItemPrintScheduleettingsHelp_Click;
			HomeAdProductAdd.Click += ScheduleSettings.PrintProductAdd;
			HomeAdProductClone.Click += ScheduleSettings.PrintProductClone;
			HomeDigitalProductClone.Click += ScheduleSettings.DigitalProductClone;
			HomeSave.Click += ScheduleSettings.buttonItemPrintScheduleettingsSave_Click;
			HomeSaveAs.Click += ScheduleSettings.buttonItemPrintScheduleettingsSaveAs_Click;
			HomeBusinessName.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeDecisionMaker.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeClientType.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeAccountNumberText.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeAccountNumberCheck.CheckedChanged += ScheduleSettings.checkBoxItemAccountNumber_CheckedChanged;
			HomePresentationDate.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += ScheduleSettings.FlightDateStartEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += ScheduleSettings.CalcWeeksOnFlightDatesChange;
			HomeFlightDatesEnd.EditValueChanged += ScheduleSettings.SchedulePropertyEditValueChanged;
			HomeFlightDatesEnd.EditValueChanged += ScheduleSettings.CalcWeeksOnFlightDatesChange;
			HomeFlightDatesStart.CloseUp += ScheduleSettings.dateEditFlightDatesStart_CloseUp;
			HomeFlightDatesEnd.CloseUp += ScheduleSettings.dateEditFlightDatesEnd_CloseUp;
			HomeBusinessName.Enter += Utilities.Instance.Editor_Enter;
			HomeBusinessName.MouseDown += Utilities.Instance.Editor_MouseDown;
			HomeBusinessName.MouseUp += Utilities.Instance.Editor_MouseUp;
			HomeDecisionMaker.Enter += Utilities.Instance.Editor_Enter;
			HomeDecisionMaker.MouseDown += Utilities.Instance.Editor_MouseDown;
			HomeDecisionMaker.MouseUp += Utilities.Instance.Editor_MouseUp;
			HomeClientType.Enter += Utilities.Instance.Editor_Enter;
			HomeClientType.MouseDown += Utilities.Instance.Editor_MouseDown;
			HomeClientType.MouseUp += Utilities.Instance.Editor_MouseUp;
			HomeAccountNumberText.Enter += Utilities.Instance.Editor_Enter;
			HomeAccountNumberText.MouseDown += Utilities.Instance.Editor_MouseDown;
			HomeAccountNumberText.MouseUp += Utilities.Instance.Editor_MouseUp;

			HomeBusinessName.TabIndex = 0;
			HomeBusinessName.KeyDown += ScheduleSettings.SchedulePropertiesEditor_KeyDown;
			HomeDecisionMaker.KeyDown += ScheduleSettings.SchedulePropertiesEditor_KeyDown;
			HomeClientType.KeyDown += ScheduleSettings.SchedulePropertiesEditor_KeyDown;
			HomePresentationDate.KeyDown += ScheduleSettings.SchedulePropertiesEditor_KeyDown;
			HomeFlightDatesStart.KeyDown += ScheduleSettings.SchedulePropertiesEditor_KeyDown;
			HomeFlightDatesEnd.KeyDown += ScheduleSettings.SchedulePropertiesEditor_KeyDown;
			#endregion

			#region Print Product
			PrintProductContainer = new PrintProductContainerControl();
			PrintProductHelp.Click += PrintProductContainer.buttonItemPrintScheduleHelp_Click;
			PrintProductSave.Click += PrintProductContainer.buttonItemPrintScheduleSave_Click;
			PrintProductSaveAs.Click += PrintProductContainer.buttonItemPrintScheduleSaveAs_Click;
			PrintProductAdPricingColumnInches.Click += PrintProductContainer.buttonItemAdPricingColumnInches_Click;
			PrintProductAdPricingFlat.Click += PrintProductContainer.buttonItemAdPricingColumnInches_Click;
			PrintProductAdPricingPagePercent.Click += PrintProductContainer.buttonItemAdPricingColumnInches_Click;
			PrintProductAdPricingColumnInches.CheckedChanged += PrintProductContainer.buttonItemAdPricing_CheckedChanged;
			PrintProductAdPricingFlat.CheckedChanged += PrintProductContainer.buttonItemAdPricing_CheckedChanged;
			PrintProductAdPricingPagePercent.CheckedChanged += PrintProductContainer.buttonItemAdPricing_CheckedChanged;
			PrintProductAdSizeStandartSquare.CheckedChanged += PrintProductContainer.checkBoxItemAdSizeStandartSquare_CheckedChanged;
			PrintProductPageSizeCheck.CheckedChanged += PrintProductContainer.checkBoxItemSizeOptions_CheckedChanged;
			PrintProductStandartHeight.EditValueChanged += PrintProductContainer.spinEditStandart_EditValueChanged;
			PrintProductStandartWidth.EditValueChanged += PrintProductContainer.spinEditStandart_EditValueChanged;
			PrintProductPageSizeGroup.EditValueChanged += PrintProductContainer.comboBoxEditSizeOptions_EditValueChanged;
			PrintProductPageSizeGroup.EditValueChanged += PrintProductContainer.comboBoxEditPageSizeGroup_EditValueChanged;
			PrintProductPageSizeName.EditValueChanged += PrintProductContainer.comboBoxEditSizeOptions_EditValueChanged;
			PrintProductMechanicalsCheck.CheckedChanged += PrintProductContainer.checkBoxItemSizeOptions_CheckedChanged;
			PrintProductMechanicalsCheck.CheckedChanged += PrintProductContainer.MechanicalsEditValueChanged;
			PrintProductMechanicalsName.EditValueChanged += PrintProductContainer.comboBoxEditSizeOptions_EditValueChanged;
			PrintProductMechanicalsName.EditValueChanged += PrintProductContainer.MechanicalsEditValueChanged;
			PrintProductRateCard.EditValueChanged += PrintProductContainer.comboBoxEditRateCard_EditValueChanged;
			PrintProductPercentOfPage.EditValueChanged += PrintProductContainer.comboBoxEditPercentOfPage_EditValueChanged;
			PrintProductSharePageSquare.ItemCheck += PrintProductContainer.checkedListBoxControlSharePageSquare_ItemCheck;
			PrintProductColor.SelectedIndexChanged += PrintProductContainer.ColorOptions_SelectedIndexChanged;
			PrintProductColorOptionsCostPerAd.Click += PrintProductContainer.buttonItemColorOptions_Click;
			PrintProductColorOptionsPercentOfAd.Click += PrintProductContainer.buttonItemColorOptions_Click;
			PrintProductColorOptionsIncluded.Click += PrintProductContainer.buttonItemColorOptions_Click;
			PrintProductColorOptionsPCI.Click += PrintProductContainer.buttonItemColorOptions_Click;
			PrintProductColorOptionsCostPerAd.CheckedChanged += PrintProductContainer.buttonItemColorOptions_CheckedChanged;
			PrintProductColorOptionsPercentOfAd.CheckedChanged += PrintProductContainer.buttonItemColorOptions_CheckedChanged;
			PrintProductColorOptionsIncluded.CheckedChanged += PrintProductContainer.buttonItemColorOptions_CheckedChanged;
			PrintProductColorOptionsPCI.CheckedChanged += PrintProductContainer.buttonItemColorOptions_CheckedChanged;
			PrintProductAdd.Click += PrintProductContainer.buttonItemAddInsert_Click;
			PrintProductClone.Click += PrintProductContainer.buttonItemCloneInsert_Click;
			PrintProductDelete.Click += PrintProductContainer.buttonItemDeleteInsert_Click;
			PrintProductStandartHeight.Enter += Utilities.Instance.Editor_Enter;
			PrintProductStandartHeight.MouseDown += Utilities.Instance.Editor_MouseDown;
			PrintProductStandartHeight.MouseUp += Utilities.Instance.Editor_MouseUp;
			PrintProductStandartWidth.Enter += Utilities.Instance.Editor_Enter;
			PrintProductStandartWidth.MouseDown += Utilities.Instance.Editor_MouseDown;
			PrintProductStandartWidth.MouseUp += Utilities.Instance.Editor_MouseUp;
			PrintProductPageSizeName.Enter += Utilities.Instance.Editor_Enter;
			PrintProductPageSizeName.MouseDown += Utilities.Instance.Editor_MouseDown;
			PrintProductPageSizeName.MouseUp += Utilities.Instance.Editor_MouseUp;
			PrintProductMechanicalsName.Enter += Utilities.Instance.Editor_Enter;
			PrintProductMechanicalsName.MouseDown += Utilities.Instance.Editor_MouseDown;
			PrintProductMechanicalsName.MouseUp += Utilities.Instance.Editor_MouseUp;
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
			DigitalPackage = new AdWebPackageControl(FormMain);
			DigitalPackageSave.Click += DigitalPackage.Save_Click;
			DigitalPackageSaveAs.Click += DigitalPackage.SaveAs_Click;
			DigitalPackagePowerPoint.Click += DigitalPackage.PowerPoint_Click;
			DigitalPackagePreview.Click += DigitalPackage.Preview_Click;
			DigitalPackageEmail.Click += DigitalPackage.Email_Click;
			DigitalPackageHelp.Click += DigitalPackage.Help_Click;
			DigitalPackageOptions.CheckedChanged += DigitalPackage.TogledButton_CheckedChanged;
			#endregion

			#region Summaries
			Summaries = new SummariesControl();

			#region Basic Overview
			BasicOverviewPreview.Click += Summaries.Preview_Click;
			BasicOverviewEmail.Click += Summaries.Email_Click;
			BasicOverviewHelp.Click += Summaries.Help_Click;
			BasicOverviewSave.Click += Summaries.Save_Click;
			BasicOverviewSaveAs.Click += Summaries.SaveAs_Click;
			BasicOverviewPowerPoint.Click += Summaries.PowerPoint_Click;
			BasicOverviewDigitalLegend.Click += Summaries.Digital_Click;
			#endregion

			#region Multi Summary
			MultiSummaryPreview.Click += Summaries.Preview_Click;
			MultiSummaryEmail.Click += Summaries.Email_Click;
			MultiSummaryHelp.Click += Summaries.Help_Click;
			MultiSummarySave.Click += Summaries.Save_Click;
			MultiSummarySaveAs.Click += Summaries.SaveAs_Click;
			MultiSummaryPowerPoint.Click += Summaries.PowerPoint_Click;
			MultiSummaryDigitalLegend.Click += Summaries.Digital_Click;
			MultiSummaryHeaderCheck.CheckedChanged += Summaries.ExternalOptionChanged;
			MultiSummaryHeaderText.EditValueChanged += Summaries.ExternalOptionChanged;
			MultiSummaryPresentationDateCheck.CheckedChanged += Summaries.ExternalOptionChanged;
			MultiSummaryBusinessNameCheck.CheckedChanged += Summaries.ExternalOptionChanged;
			MultiSummaryDecisionMakerCheck.CheckedChanged += Summaries.ExternalOptionChanged;
			#endregion

			#region Snapshot
			SnapshotPreview.Click += Summaries.Preview_Click;
			SnapshotEmail.Click += Summaries.Email_Click;
			SnapshotHelp.Click += Summaries.Help_Click;
			SnapshotSave.Click += Summaries.Save_Click;
			SnapshotSaveAs.Click += Summaries.SaveAs_Click;
			SnapshotPowerPoint.Click += Summaries.PowerPoint_Click;
			SnapshotDigitalLegend.Click += Summaries.Digital_Click;
			#endregion
			#endregion

			#region Grids
			Grids = new GridsControl();

			#region Detailed Grid
			DetailedGridHelp.Click += Grids.Help_Click;
			DetailedGridSave.Click += Grids.Save_Click;
			DetailedGridSaveAs.Click += Grids.SaveAs_Click;
			DetailedGridPowerPoint.Click += Grids.PowerPoint_Click;
			DetailedGridEmail.Click += Grids.Email_Click;
			DetailedGridPreview.Click += Grids.Preview_Click;
			DetailedGridDigitalLegend.Click += Grids.Digital_Click;
			#endregion

			#region Multi Grid
			MultiGridHelp.Click += Grids.Help_Click;
			MultiGridSave.Click += Grids.Save_Click;
			MultiGridSaveAs.Click += Grids.SaveAs_Click;
			MultiGridPowerPoint.Click += Grids.PowerPoint_Click;
			MultiGridEmail.Click += Grids.Email_Click;
			MultiGridPreview.Click += Grids.Preview_Click;
			MultiGridDigitalLegend.Click += Grids.Digital_Click;
			#endregion

			#endregion

			#region Calendars
			Calendar = new AdCalendarControl();
			CalendarMonthList.SelectedIndexChanged += Calendar.MonthList_SelectedIndexChanged;
			CalendarCopy.Click += Calendar.CalendarCopy_Click;
			CalendarPaste.Click += Calendar.CalendarPaste_Click;
			CalendarClone.Click += Calendar.CalendarClone_Click;
			CalendarSave.Click += Calendar.Save_Click;
			CalendarSaveAs.Click += Calendar.SaveAs_Click;
			CalendarPreview.Click += Calendar.Preview_Click;
			CalendarPowerPoint.Click += Calendar.PowerPoint_Click;
			CalendarEmail.Click += Calendar.Email_Click;
			CalendarHelp.Click += Calendar.Help_Click;
			CalendarExport.Click += Calendar.Export_Click;
			#endregion

			#region AdPlan
			AdPlan = new PrintAdPlanControl(FormMain);
			AdPlanPreview.Click += AdPlan.Preview_Click;
			AdPlanEmail.Click += AdPlan.Email_Click;
			AdPlanHelp.Click += AdPlan.Help_Click;
			AdPlanSave.Click += AdPlan.Save_Click;
			AdPlanSaveAs.Click += AdPlan.SaveAs_Click;
			AdPlanPowerPoint.Click += AdPlan.PowerPoint_Click;
			#endregion

			#region Summary Light
			SummaryLight = new PrintSummaryLight();
			SummaryLightSave.Click += SummaryLight.Save_Click;
			SummaryLightSaveAs.Click += SummaryLight.SaveAs_Click;
			SummaryLightHelp.Click += (o, e) => SummaryLight.OpenHelp();
			#endregion

			#region Summary Full
			SummaryFull = new PrintSummaryFull();
			SummaryFullSave.Click += SummaryFull.Save_Click;
			SummaryFullSaveAs.Click += SummaryFull.SaveAs_Click;
			SummaryFullHelp.Click += (o, e) => SummaryFull.OpenHelp();
			#endregion

			#region Rate Card Events
			RateCard = new RateCardControl(BusinessWrapper.Instance.RateCardManager, RateCardCombo);
			RateCardHelp.Click += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("ratecard");
			#endregion

			#region Gallery 1
			Gallery1 = new PrintGallery1Control();
			Gallery1Help.Click += (o, e) => BusinessWrapper.Instance.HelpManager.OpenHelpLink("gallery");
			#endregion

			#region Gallery 2
			Gallery2 = new PrintGallery2Control();
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
			ScheduleSettings.Dispose();
			PrintProductContainer.Dispose();
			DigitalProductContainer.Dispose();
			DigitalPackage.Dispose();
			Summaries.BasicOverview.Dispose();
			Summaries.MultiSummary.Dispose();
			Summaries.Snapshot.Dispose();
			Summaries.Dispose();
			Grids.DetailedGrid.Dispose();
			Grids.MultiGrid.Dispose();
			Grids.Dispose();
			AdPlan.Dispose();
			SummaryLight.Dispose();
			SummaryFull.Dispose();
			Calendar.Dispose();
			RateCard.Dispose();
			Gallery1.Dispose();
			Gallery2.Dispose();
			FloaterRequested = null;
		}

		public void LoadData()
		{
			ScheduleSettings.LoadSchedule(false);
			PrintProductContainer.LoadSchedule(false);
			DigitalProductContainer.LoadSchedule(false);
			DigitalPackage.LoadSchedule(false);
			Grids.DetailedGrid.UpdateOutput(false);
			Grids.MultiGrid.UpdateOutput(false);
			Summaries.BasicOverview.UpdateOutput(false);
			Summaries.MultiSummary.UpdateOutput(false);
			Summaries.Snapshot.UpdateOutput(false);
			AdPlan.LoadSchedule(false);
			SummaryLight.LoadData(false);
			SummaryFull.LoadData(false);
			Calendar.LoadCalendar(false);

			BusinessWrapper.Instance.RateCardManager.LoadRateCards();
			TabRateCard.Enabled = BusinessWrapper.Instance.RateCardManager.RateCardFolders.Any();
		}

		public void UpdatePrintProductTab(bool enable)
		{
			TabPrintProduct.Enabled = enable;
		}

		public void UpdateDigitalProductTab(bool enable)
		{
			TabDigitalProduct.Enabled = enable;
			TabDigitalPackage.Enabled = enable && DigitalPackage.SlidesAvailable;
			BasicOverviewDigitalLegend.Enabled = enable;
			MultiSummaryDigitalLegend.Enabled = enable;
			SnapshotDigitalLegend.Enabled = enable;
			DetailedGridDigitalLegend.Enabled = enable;
			MultiGridDigitalLegend.Enabled = enable;
		}

		public void UpdateOutputTabs(bool enable)
		{
			TabBasicOverview.Enabled = enable;
			TabMultiSummary.Enabled = enable;
			TabSnapshot.Enabled = enable;
			TabAdPlan.Enabled = enable && Directory.Exists(BusinessWrapper.Instance.OutputManager.AdPlanTemlatesFolderPath);
			TabDetailedGrid.Enabled = enable;
			TabMultiGrid.Enabled = enable;
			TabCalendar.Enabled = enable;
			TabSummaryLight.Enabled = enable;
			TabSummaryFull.Enabled = enable;
		}

		public void UpdateOutputButtonsAccordingThemeStatus()
		{
			var themesExisted = BusinessWrapper.Instance.ThemeManager.GetThemes(SlideType.None).Any();
			if (!themesExisted)
			{
				var selectorToolTip = new SuperTooltipInfo("Important Info", "", "Click to get more info why output is disabled", null, null, eTooltipColor.Gray);
				var themesDisabledHandler = new Action(() => BusinessWrapper.Instance.HelpManager.OpenHelpLink("NoTheme"));

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

				BasicOverviewPowerPoint.Visible = false;
				(BasicOverviewPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(BasicOverviewEmail.ContainerControl as RibbonBar).Visible = false;
				(BasicOverviewPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(BasicOverviewTheme, selectorToolTip);
				BasicOverviewTheme.Click += (o, e) => themesDisabledHandler();

				MultiSummaryPowerPoint.Visible = false;
				(MultiSummaryPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(MultiSummaryEmail.ContainerControl as RibbonBar).Visible = false;
				(MultiSummaryPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(MultiSummaryTheme, selectorToolTip);
				MultiSummaryTheme.Click += (o, e) => themesDisabledHandler();

				SnapshotPowerPoint.Visible = false;
				(SnapshotPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(SnapshotEmail.ContainerControl as RibbonBar).Visible = false;
				(SnapshotPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(SnapshotTheme, selectorToolTip);
				SnapshotTheme.Click += (o, e) => themesDisabledHandler();

				AdPlanPowerPoint.Visible = false;
				(AdPlanPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(AdPlanEmail.ContainerControl as RibbonBar).Visible = false;
				(AdPlanPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(AdPlanTheme, selectorToolTip);
				AdPlanTheme.Click += (o, e) => themesDisabledHandler();

				DetailedGridPowerPoint.Visible = false;
				(DetailedGridPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(DetailedGridEmail.ContainerControl as RibbonBar).Visible = false;
				(DetailedGridPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(DetailedGridTheme, selectorToolTip);
				DetailedGridTheme.Click += (o, e) => themesDisabledHandler();

				MultiGridPowerPoint.Visible = false;
				(MultiGridPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(MultiGridEmail.ContainerControl as RibbonBar).Visible = false;
				(MultiGridPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(MultiGridTheme, selectorToolTip);
				MultiGridTheme.Click += (o, e) => themesDisabledHandler();

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
			}
			else
			{
				var selectorToolTip = new SuperTooltipInfo("Slide Theme", "", "Select the PowerPoint Slide theme you want to use for this schedule", null, null, eTooltipColor.Gray);
				Supertip.SetSuperTooltip(DigitalProductTheme, selectorToolTip);
				Supertip.SetSuperTooltip(DigitalPackageTheme, selectorToolTip);
				Supertip.SetSuperTooltip(BasicOverviewTheme, selectorToolTip);
				Supertip.SetSuperTooltip(MultiSummaryTheme, selectorToolTip);
				Supertip.SetSuperTooltip(SnapshotTheme, selectorToolTip);
				Supertip.SetSuperTooltip(AdPlanTheme, selectorToolTip);
				Supertip.SetSuperTooltip(DetailedGridTheme, selectorToolTip);
				Supertip.SetSuperTooltip(MultiGridTheme, selectorToolTip);
				Supertip.SetSuperTooltip(SummaryLightTheme, selectorToolTip);
				Supertip.SetSuperTooltip(SummaryFullTheme, selectorToolTip);
			}

			Ribbon.SelectedRibbonTabChanged += (o, e) =>
			{
				(DigitalProductPowerPoint.ContainerControl as RibbonBar).Text = (DigitalProductTheme.Tag as Theme).Name;
				(DigitalPackagePowerPoint.ContainerControl as RibbonBar).Text = (DigitalPackageTheme.Tag as Theme).Name;
				(BasicOverviewPowerPoint.ContainerControl as RibbonBar).Text = (BasicOverviewTheme.Tag as Theme).Name;
				(MultiSummaryPowerPoint.ContainerControl as RibbonBar).Text = (MultiSummaryTheme.Tag as Theme).Name;
				(SnapshotPowerPoint.ContainerControl as RibbonBar).Text = (SnapshotTheme.Tag as Theme).Name;
				(AdPlanPowerPoint.ContainerControl as RibbonBar).Text = (AdPlanTheme.Tag as Theme).Name;
				(DetailedGridPowerPoint.ContainerControl as RibbonBar).Text = (DetailedGridTheme.Tag as Theme).Name;
				(MultiGridPowerPoint.ContainerControl as RibbonBar).Text = (MultiGridTheme.Tag as Theme).Name;
				(SummaryLightPowerPoint.ContainerControl as RibbonBar).Text = (SummaryLightTheme.Tag as Theme).Name;
				(SummaryFullPowerPoint.ContainerControl as RibbonBar).Text = (SummaryFullTheme.Tag as Theme).Name;
			};
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
					case "Ad Schedule":
						TabPrintProduct.Text = tabPageConfig.Name;
						tabPages.Add(TabPrintProduct);
						break;
					case "Digital Slides":
						TabDigitalProduct.Text = tabPageConfig.Name;
						tabPages.Add(TabDigitalProduct);
						break;
					case "Digital PKG":
						TabDigitalPackage.Text = tabPageConfig.Name;
						tabPages.Add(TabDigitalPackage);
						break;
					case "1. AdPlan":
						TabAdPlan.Text = tabPageConfig.Name;
						tabPages.Add(TabAdPlan);
						break;
					case "2. Overview":
						TabBasicOverview.Text = tabPageConfig.Name;
						tabPages.Add(TabBasicOverview);
						break;
					case "3. Analysis":
						TabMultiSummary.Text = tabPageConfig.Name;
						tabPages.Add(TabMultiSummary);
						break;
					case "4. Snapshot":
						TabSnapshot.Text = tabPageConfig.Name;
						tabPages.Add(TabSnapshot);
						break;
					case "5. Detailed Grid":
						TabDetailedGrid.Text = tabPageConfig.Name;
						tabPages.Add(TabDetailedGrid);
						break;
					case "6. Logo Grid":
						TabMultiGrid.Text = tabPageConfig.Name;
						tabPages.Add(TabMultiGrid);
						break;
					case "7. Calendar":
						TabCalendar.Text = tabPageConfig.Name;
						tabPages.Add(TabCalendar);
						break;
					case "Summary1":
						TabSummaryLight.Text = tabPageConfig.Name;
						tabPages.Add(TabSummaryLight);
						break;
					case "Summary2":
						TabSummaryFull.Text = tabPageConfig.Name;
						tabPages.Add(TabSummaryFull);
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
				}
			}
			Ribbon.Items.AddRange(tabPages.ToArray());
		}

		private void ConfigureSpecialButtons()
		{
			var specialLinkContainers = new[]
			{
				HomeSpecialButtons,
				PrintProductSpecialButtons,
				DigitalProductSpecialButtons,
				DigitalPackageSpecialButtons,
				AdPlanSpecialButtons,
				BasicOverviewSpecialButtons,
				MultiSummarySpecialButtons,
				SnapshotSpecialButtons,
				DetailedGridSpecialButtons,
				MultiGridSpecialButtons,
				CalendarSpecialButtons,
				SummaryLightSpecialButtons,
				SummaryFullSpecialButtons,
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

		public void SaveSchedule(Schedule localSchedule, bool nameChanged, bool quickSave, Control sender)
		{
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nSaving settings...";
				form.TopMost = true;
				var thread = new Thread(() => BusinessWrapper.Instance.ScheduleManager.SaveSchedule(localSchedule, quickSave, sender));
				form.Show();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
			if (nameChanged)
				BusinessWrapper.Instance.ActivityManager.AddActivity(new ScheduleActivity("Saved As", localSchedule.Name));
			if (ScheduleChanged != null)
				ScheduleChanged(this, EventArgs.Empty);
		}

		public void ShowFloater(Action afterShow)
		{
			var args = new FloaterRequestedEventArgs { AfterShow = afterShow, Logo = Resources.RibbonLogo };
			if (FloaterRequested != null)
				FloaterRequested(null, args);
		}

		private void Ribbon_SelectedRibbonTabChanged(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(new TabActivity(Ribbon.SelectedRibbonTabItem.Text));
			if (Ribbon.SelectedRibbonTabItem == TabRateCard)
				RateCard.LoadRateCards();
			else if (Ribbon.SelectedRibbonTabItem == TabGallery1)
				Gallery1.InitControl();
			else if (Ribbon.SelectedRibbonTabItem == TabGallery2)
				Gallery2.InitControl();
		}
		#region Command Controls

		#region Home
		public RibbonPanel HomePanel { get; set; }
		public ButtonItem HomeHelp { get; set; }
		public ButtonItem HomeSave { get; set; }
		public ButtonItem HomeSaveAs { get; set; }
		public RibbonBar HomeProduct { get; set; }
		public RibbonBar HomeSpecialButtons { get; set; }
		public ItemContainer HomeAdProduct { get; set; }
		public ButtonItem HomeAdProductAdd { get; set; }
		public ButtonItem HomeAdProductClone { get; set; }
		public ItemContainer HomeDigitalProduct { get; set; }
		public ButtonItem HomeDigitalProductAdd { get; set; }
		public ButtonItem HomeDigitalProductClone { get; set; }
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

		#region Print Product
		public ButtonItem PrintProductHelp { get; set; }
		public ButtonItem PrintProductSave { get; set; }
		public ButtonItem PrintProductSaveAs { get; set; }
		public ButtonItem PrintProductAdd { get; set; }
		public ButtonItem PrintProductClone { get; set; }
		public ButtonItem PrintProductDelete { get; set; }
		public ButtonItem PrintProductAdPricingColumnInches { get; set; }
		public ButtonItem PrintProductAdPricingFlat { get; set; }
		public ButtonItem PrintProductAdPricingPagePercent { get; set; }
		public ButtonItem PrintProductColorOptionsCostPerAd { get; set; }
		public ButtonItem PrintProductColorOptionsPercentOfAd { get; set; }
		public ButtonItem PrintProductColorOptionsIncluded { get; set; }
		public ButtonItem PrintProductColorOptionsPCI { get; set; }
		public RibbonBar PrintProductStrategy { get; set; }
		public RibbonBar PrintProductSpecialButtons { get; set; }
		public ItemContainer PrintProductAdSizeStandart { get; set; }
		public ItemContainer PrintProductAdSizeSharePage { get; set; }
		public Label PrintProductStandardSquareValue { get; set; }
		public ControlContainerItem PrintProductStandardSquareValueContainer { get; set; }
		public RibbonBar PrintProductDimensionsRibbonBar { get; set; }
		public RibbonPanel PrintProductPanel { get; set; }
		public CheckBoxItem PrintProductAdSizeStandartSquare { get; set; }
		public CheckBoxItem PrintProductPageSizeCheck { get; set; }
		public ComboBoxEdit PrintProductPageSizeGroup { get; set; }
		public ControlContainerItem PrintProductPageSizeGroupContainer { get; set; }
		public ComboBoxEdit PrintProductPageSizeName { get; set; }
		public CheckBoxItem PrintProductMechanicalsCheck { get; set; }
		public ButtonEdit PrintProductMechanicalsName { get; set; }
		public ComboBoxEdit PrintProductRateCard { get; set; }
		public ComboBoxEdit PrintProductPercentOfPage { get; set; }
		public ComboBoxEdit PrintProductColor { get; set; }
		public SpinEdit PrintProductStandartHeight { get; set; }
		public SpinEdit PrintProductStandartWidth { get; set; }
		public ControlContainerItem PrintProductSharePageSquareContainer { get; set; }
		public CheckedListBoxControl PrintProductSharePageSquare { get; set; }
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

		#region Basic Overview
		public RibbonBar BasicOverviewSpecialButtons { get; set; }
		public ButtonItem BasicOverviewHelp { get; set; }
		public ButtonItem BasicOverviewSave { get; set; }
		public ButtonItem BasicOverviewSaveAs { get; set; }
		public ButtonItem BasicOverviewPreview { get; set; }
		public ButtonItem BasicOverviewEmail { get; set; }
		public ButtonItem BasicOverviewPowerPoint { get; set; }
		public ButtonItem BasicOverviewTheme { get; set; }
		public ButtonItem BasicOverviewDigitalLegend { get; set; }
		#endregion

		#region Multi Summary
		public RibbonBar MultiSummarySpecialButtons { get; set; }
		public ButtonItem MultiSummaryHelp { get; set; }
		public ButtonItem MultiSummarySave { get; set; }
		public ButtonItem MultiSummarySaveAs { get; set; }
		public ButtonItem MultiSummaryPreview { get; set; }
		public ButtonItem MultiSummaryEmail { get; set; }
		public ButtonItem MultiSummaryPowerPoint { get; set; }
		public ButtonItem MultiSummaryTheme { get; set; }
		public ButtonItem MultiSummaryDigitalLegend { get; set; }
		public CheckBoxItem MultiSummaryHeaderCheck { get; set; }
		public ComboBoxEdit MultiSummaryHeaderText { get; set; }
		public CheckBoxItem MultiSummaryPresentationDateCheck { get; set; }
		public LabelItem MultiSummaryPresentationDateText { get; set; }
		public CheckBoxItem MultiSummaryBusinessNameCheck { get; set; }
		public LabelItem MultiSummaryBusinessNameText { get; set; }
		public CheckBoxItem MultiSummaryDecisionMakerCheck { get; set; }
		public LabelItem MultiSummaryDecisionMakerText { get; set; }
		#endregion

		#region Snapshot
		public RibbonBar SnapshotSpecialButtons { get; set; }
		public ButtonItem SnapshotHelp { get; set; }
		public ButtonItem SnapshotSave { get; set; }
		public ButtonItem SnapshotSaveAs { get; set; }
		public ButtonItem SnapshotPreview { get; set; }
		public ButtonItem SnapshotEmail { get; set; }
		public ButtonItem SnapshotPowerPoint { get; set; }
		public ButtonItem SnapshotTheme { get; set; }
		public ButtonItem SnapshotDigitalLegend { get; set; }
		#endregion

		#region AdPlan
		public RibbonBar AdPlanSpecialButtons { get; set; }
		public ButtonItem AdPlanHelp { get; set; }
		public ButtonItem AdPlanSave { get; set; }
		public ButtonItem AdPlanSaveAs { get; set; }
		public ButtonItem AdPlanPreview { get; set; }
		public ButtonItem AdPlanEmail { get; set; }
		public ButtonItem AdPlanPowerPoint { get; set; }
		public ButtonItem AdPlanTheme { get; set; }
		#endregion

		#region Detailed Grid
		public RibbonBar DetailedGridSpecialButtons { get; set; }
		public ButtonItem DetailedGridHelp { get; set; }
		public ButtonItem DetailedGridSave { get; set; }
		public ButtonItem DetailedGridSaveAs { get; set; }
		public ButtonItem DetailedGridPreview { get; set; }
		public ButtonItem DetailedGridEmail { get; set; }
		public ButtonItem DetailedGridPowerPoint { get; set; }
		public ButtonItem DetailedGridTheme { get; set; }
		public ButtonItem DetailedGridDigitalLegend { get; set; }
		#endregion

		#region Multi Grid
		public RibbonBar MultiGridSpecialButtons { get; set; }
		public ButtonItem MultiGridHelp { get; set; }
		public ButtonItem MultiGridSave { get; set; }
		public ButtonItem MultiGridSaveAs { get; set; }
		public ButtonItem MultiGridPreview { get; set; }
		public ButtonItem MultiGridEmail { get; set; }
		public ButtonItem MultiGridPowerPoint { get; set; }
		public ButtonItem MultiGridTheme { get; set; }
		public ButtonItem MultiGridDigitalLegend { get; set; }
		#endregion

		#region Calendar
		public ImageListBoxControl CalendarMonthList { get; set; }
		public ButtonItem CalendarCopy { get; set; }
		public ButtonItem CalendarPaste { get; set; }
		public ButtonItem CalendarClone { get; set; }
		public ButtonItem CalendarHelp { get; set; }
		public ButtonItem CalendarSave { get; set; }
		public ButtonItem CalendarSaveAs { get; set; }
		public ButtonItem CalendarPreview { get; set; }
		public ButtonItem CalendarEmail { get; set; }
		public ButtonItem CalendarPowerPoint { get; set; }
		public ButtonItem CalendarExport { get; set; }
		public RibbonBar CalendarSpecialButtons { get; set; }
		#endregion

		#region Summary Light
		public RibbonBar SummaryLightSpecialButtons { get; set; }
		public ButtonItem SummaryLightHelp { get; set; }
		public ButtonItem SummaryLightSave { get; set; }
		public ButtonItem SummaryLightSaveAs { get; set; }
		public ButtonItem SummaryLightPreview { get; set; }
		public ButtonItem SummaryLightEmail { get; set; }
		public ButtonItem SummaryLightPowerPoint { get; set; }
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
		public ButtonItem SummaryFullTheme { get; set; }
		public CheckEdit SummaryFullSlideOutputToggle { get; set; }
		public CheckEdit SummaryFullTableOutputToggle { get; set; }
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
		public ScheduleSettingsControl ScheduleSettings { get; private set; }
		public PrintProductContainerControl PrintProductContainer { get; private set; }
		public DigitalProductContainerControl DigitalProductContainer { get; private set; }
		public AdWebPackageControl DigitalPackage { get; private set; }
		public SummariesControl Summaries { get; private set; }
		public GridsControl Grids { get; private set; }
		public PrintSummaryLight SummaryLight { get; private set; }
		public PrintSummaryFull SummaryFull { get; private set; }
		public PrintAdPlanControl AdPlan { get; private set; }
		public AdCalendarControl Calendar { get; private set; }
		public RateCardControl RateCard { get; private set; }
		public GalleryControl Gallery1 { get; private set; }
		public GalleryControl Gallery2 { get; private set; }
		#endregion
	}
}