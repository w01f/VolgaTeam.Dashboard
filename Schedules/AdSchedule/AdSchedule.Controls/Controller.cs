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
using NewBizWiz.AdSchedule.Controls.PresentationClasses.RateCard;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.Summary;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.CommonGUI.Floater;
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
		public RibbonTabItem TabSummary { get; set; }
		public RibbonTabItem TabRateCard { get; set; }

		public void Init()
		{
			#region Schedule Settings
			ScheduleSettings = new ScheduleSettingsControl();
			HomeHelp.Click += ScheduleSettings.buttonItemPrintScheduleettingsHelp_Click;
			HomeAdProductAdd.Click += ScheduleSettings.PrintProductAdd;
			HomeAdProductClone.Click += ScheduleSettings.PrintProductClone;
			HomeAdProductDelete.Click += ScheduleSettings.PrintProductDelete;
			HomeDigitalProductClone.Click += ScheduleSettings.DigitalProductClone;
			HomeDigitalProductDelete.Click += ScheduleSettings.DigitalProductDelete;
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
			PrintProductStandartPageSizeCheck.CheckedChanged += PrintProductContainer.checkBoxItemSizeOptions_CheckedChanged;
			PrintProductSharePagePageSizeCheck.CheckedChanged += PrintProductContainer.checkBoxItemSizeOptions_CheckedChanged;
			PrintProductStandartHeight.EditValueChanged += PrintProductContainer.spinEditStandart_EditValueChanged;
			PrintProductStandartWidth.EditValueChanged += PrintProductContainer.spinEditStandart_EditValueChanged;
			PrintProductStandartPageSizeCombo.EditValueChanged += PrintProductContainer.comboBoxEditSizeOptions_EditValueChanged;
			PrintProductSharePagePageSizeCombo.EditValueChanged += PrintProductContainer.comboBoxEditSizeOptions_EditValueChanged;
			PrintProductRateCard.EditValueChanged += PrintProductContainer.comboBoxEditRateCard_EditValueChanged;
			PrintProductPercentOfPage.EditValueChanged += PrintProductContainer.comboBoxEditPercentOfPage_EditValueChanged;
			PrintProductSharePageSquare.ItemCheck += PrintProductContainer.checkedListBoxControlSharePageSquare_ItemCheck;
			PrintProductColorOptionsSingle.Click += PrintProductContainer.ColorOptions_Click;
			PrintProductColorOptionsSpot.Click += PrintProductContainer.ColorOptions_Click;
			PrintProductColorOptionsFull.Click += PrintProductContainer.ColorOptions_Click;
			PrintProductColorOptionsSingle.CheckedChanged += PrintProductContainer.ColorOptions_CheckedChanged;
			PrintProductColorOptionsSpot.CheckedChanged += PrintProductContainer.ColorOptions_CheckedChanged;
			PrintProductColorOptionsFull.CheckedChanged += PrintProductContainer.ColorOptions_CheckedChanged;
			PrintProductColorOptionsCostPerAd.Click += PrintProductContainer.buttonItemColorOptions_Click;
			PrintProductColorOptionsPercentOfAd.Click += PrintProductContainer.buttonItemColorOptions_Click;
			PrintProductColorOptionsIncluded.Click += PrintProductContainer.buttonItemColorOptions_Click;
			PrintProductColorOptionsPCI.Click += PrintProductContainer.buttonItemColorOptions_Click;
			PrintProductColorOptionsCostPerAd.CheckedChanged += PrintProductContainer.buttonItemColorOptions_CheckedChanged;
			PrintProductColorOptionsPercentOfAd.CheckedChanged += PrintProductContainer.buttonItemColorOptions_CheckedChanged;
			PrintProductColorOptionsIncluded.CheckedChanged += PrintProductContainer.buttonItemColorOptions_CheckedChanged;
			PrintProductColorOptionsPCI.CheckedChanged += PrintProductContainer.buttonItemColorOptions_CheckedChanged;
			PrintProductCostPerInch.EditValueChanged += PrintProductContainer.spinEditCostPerInch_EditValueChanged;
			PrintProductAdd.Click += PrintProductContainer.buttonItemAddInsert_Click;
			PrintProductClone.Click += PrintProductContainer.buttonItemCloneInsert_Click;
			PrintProductDelete.Click += PrintProductContainer.buttonItemDeleteInsert_Click;
			PrintProductCostPerInch.Enter += Utilities.Instance.Editor_Enter;
			PrintProductCostPerInch.MouseDown += Utilities.Instance.Editor_MouseDown;
			PrintProductCostPerInch.MouseUp += Utilities.Instance.Editor_MouseUp;
			PrintProductStandartHeight.Enter += Utilities.Instance.Editor_Enter;
			PrintProductStandartHeight.MouseDown += Utilities.Instance.Editor_MouseDown;
			PrintProductStandartHeight.MouseUp += Utilities.Instance.Editor_MouseUp;
			PrintProductStandartWidth.Enter += Utilities.Instance.Editor_Enter;
			PrintProductStandartWidth.MouseDown += Utilities.Instance.Editor_MouseDown;
			PrintProductStandartWidth.MouseUp += Utilities.Instance.Editor_MouseUp;
			PrintProductStandartPageSizeCombo.Enter += Utilities.Instance.Editor_Enter;
			PrintProductStandartPageSizeCombo.MouseDown += Utilities.Instance.Editor_MouseDown;
			PrintProductStandartPageSizeCombo.MouseUp += Utilities.Instance.Editor_MouseUp;
			PrintProductSharePagePageSizeCombo.Enter += Utilities.Instance.Editor_Enter;
			PrintProductSharePagePageSizeCombo.MouseDown += Utilities.Instance.Editor_MouseDown;
			PrintProductSharePagePageSizeCombo.MouseUp += Utilities.Instance.Editor_MouseUp;
			#endregion

			#region Digital Product
			DigitalProductContainer = new DigitalProductContainerControl(FormMain);
			DigitalProductSave.Click += DigitalProductContainer.Save_Click;
			DigitalProductSaveAs.Click += DigitalProductContainer.SaveAs_Click;
			DigitalProductPowerPoint.Click += DigitalProductContainer.PowerPoint_Click;
			DigitalProductEmail.Click += DigitalProductContainer.Email_Click;
			DigitalProductHelp.Click += DigitalProductContainer.Help_Click;
			DigitalProductOptions.CheckedChanged += DigitalProductContainer.Options_CheckedChanged;
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
			#endregion

			#region Snapshot
			SnapshotOptions.CheckedChanged += Summaries.Snapshot.buttonItemSnapshotOptions_CheckedChanged;
			SnapshotPreview.Click += Summaries.Preview_Click;
			SnapshotEmail.Click += Summaries.Email_Click;
			SnapshotHelp.Click += Summaries.Help_Click;
			SnapshotSave.Click += Summaries.Save_Click;
			SnapshotSaveAs.Click += Summaries.SaveAs_Click;
			SnapshotPowerPoint.Click += Summaries.PowerPoint_Click;
			SnapshotDigitalLegend.Click += Summaries.Digital_Click;
			#endregion

			#region AdPlan
			AdPlanPreview.Click += Summaries.Preview_Click;
			AdPlanEmail.Click += Summaries.Email_Click;
			AdPlanHelp.Click += Summaries.Help_Click;
			AdPlanSave.Click += Summaries.Save_Click;
			AdPlanSaveAs.Click += Summaries.SaveAs_Click;
			AdPlanPowerPoint.Click += Summaries.PowerPoint_Click;
			#endregion

			#endregion

			#region Grids
			Grids = new GridsControl();

			#region Detailed Grid
			DetailedGridHelp.Click += Grids.Help_Click;
			DetailedGridSave.Click += Grids.Save_Click;
			DetailedGridSaveAs.Click += Grids.SaveAs_Click;
			DetailedGridOptions.CheckedChanged += Grids.Details_CheckedChanged;
			DetailedGridPowerPoint.Click += Grids.PowerPoint_Click;
			DetailedGridEmail.Click += Grids.Email_Click;
			DetailedGridPreview.Click += Grids.Preview_Click;
			DetailedGridDigitalLegend.Click += Grids.Digital_Click;
			#endregion

			#region Multi Grid
			MultiGridHelp.Click += Grids.Help_Click;
			MultiGridSave.Click += Grids.Save_Click;
			MultiGridSaveAs.Click += Grids.SaveAs_Click;
			MultiGridOptions.CheckedChanged += Grids.Details_CheckedChanged;
			MultiGridPowerPoint.Click += Grids.PowerPoint_Click;
			MultiGridEmail.Click += Grids.Email_Click;
			MultiGridPreview.Click += Grids.Preview_Click;
			MultiGridDigitalLegend.Click += Grids.Digital_Click;
			#endregion

			#endregion

			#region Calendars
			Calendars = new CalendarsControl();
			CalendarOptions.CheckedChanged += Calendars.buttonItemCalendarsOptions_CheckedChanged;
			CalendarHelp.Click += Calendars.buttonItemCalendarsHelp_Click;
			CalendarSave.Click += Calendars.buttonItemCalendarSave_Click;
			CalendarSaveAs.Click += Calendars.buttonItemCalendarSaveAs_Click;
			CalendarPowerPoint.Click += Calendars.buttonItemCalendarsPowerPoint_Click;
			CalendarEmail.Click += Calendars.buttonItemCalendarsEmail_Click;
			CalendarPreview.Click += Calendars.buttonItemCalendarsPreview_Click;
			CalendarExport.Click += Calendars.buttonItemCalendarsExport_Click;
			CalendarMonthList.SelectedIndexChanged += Calendars.MonthList_SelectedIndexChanged;
			#endregion

			#region Summary
			Summary = new SummaryControl();
			SummaryAddItem.Click += Summary.AddItem;
			SummarySave.Click += Summary.Save_Click;
			SummarySaveAs.Click += Summary.SaveAs_Click;
			SummaryHelp.Click += (o, e) => Summary.OpenHelp();
			SummaryPowerPoint.Click += (o, e) => Summary.Output();
			SummaryEmail.Click += (o, e) => Summary.Email();
			SummaryPreview.Click += (o, e) => Summary.Preview();
			#endregion

			#region Rate Card Events
			RateCard = new RateCardControl();
			RateCardHelp.Click += RateCard.buttonItemRateCardHelp_Click;
			RateCardCombo.EditValueChanged += RateCard.comboBoxEditRateCards_EditValueChanged;
			#endregion

			ConfigureTabPages();

			UpdateOutputButtonsAccordingThemeStatus();
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
			Summaries.AdPlan.Dispose();
			Summaries.Dispose();
			Grids.DetailedGrid.Dispose();
			Grids.MultiGrid.Dispose();
			Grids.Dispose();
			Calendars.Calendar.Dispose();
			Calendars.Dispose();
			Summary.Dispose();
			RateCard.Dispose();
		}

		public void LoadData()
		{
			ScheduleSettings.LoadSchedule(false);
			PrintProductContainer.LoadSchedule(false);
			DigitalProductContainer.LoadSchedule(false);
			DigitalPackage.LoadSchedule(false);
			Calendars.Calendar.UpdateOutput(false);
			Grids.DetailedGrid.UpdateOutput(false);
			Grids.MultiGrid.UpdateOutput(false);
			Summaries.BasicOverview.UpdateOutput(false);
			Summaries.MultiSummary.UpdateOutput(false);
			Summaries.Snapshot.UpdateOutput(false);
			Summaries.AdPlan.UpdateOutput(false);
			Summary.UpdateOutput(false);
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
			Calendars.buttonXMonthShowDigital.Enabled = enable;
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
			TabSummary.Enabled = enable;
		}

		public void UpdateOutputButtonsAccordingThemeStatus()
		{
			var themesExisted = BusinessWrapper.Instance.ThemeManager.Themes.Any();
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

				SummaryPowerPoint.Visible = false;
				(SummaryPowerPoint.ContainerControl as RibbonBar).Text = "Important Info";
				(SummaryEmail.ContainerControl as RibbonBar).Visible = false;
				(SummaryPreview.ContainerControl as RibbonBar).Visible = false;
				Supertip.SetSuperTooltip(SummaryTheme, selectorToolTip);
				SummaryTheme.Click += (o, e) => themesDisabledHandler();
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
				Supertip.SetSuperTooltip(SummaryTheme, selectorToolTip);
			}
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
					case "8. Summary":
						TabSummary.Text = tabPageConfig.Name;
						tabPages.Add(TabSummary);
						break;
					case "Rate Card":
						TabRateCard.Text = tabPageConfig.Name;
						tabPages.Add(TabRateCard);
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

		public void ShowFloater(Action afterShow)
		{
			var args = new FloaterRequestedEventArgs { AfterShow = afterShow };
			if (FloaterRequested != null)
				FloaterRequested(null, args);
		}

		#region Command Controls

		#region Home
		public RibbonPanel HomePanel { get; set; }
		public ButtonItem HomeHelp { get; set; }
		public ButtonItem HomeSave { get; set; }
		public ButtonItem HomeSaveAs { get; set; }
		public RibbonBar HomeProduct { get; set; }
		public ItemContainer HomeAdProduct { get; set; }
		public ButtonItem HomeAdProductAdd { get; set; }
		public ButtonItem HomeAdProductClone { get; set; }
		public ButtonItem HomeAdProductDelete { get; set; }
		public ItemContainer HomeDigitalProduct { get; set; }
		public ButtonItem HomeDigitalProductAdd { get; set; }
		public ButtonItem HomeDigitalProductClone { get; set; }
		public ButtonItem HomeDigitalProductDelete { get; set; }
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
		public ButtonItem PrintProductColorOptionsSingle { get; set; }
		public ButtonItem PrintProductColorOptionsSpot { get; set; }
		public ButtonItem PrintProductColorOptionsFull { get; set; }
		public ButtonItem PrintProductColorOptionsCostPerAd { get; set; }
		public ButtonItem PrintProductColorOptionsPercentOfAd { get; set; }
		public ButtonItem PrintProductColorOptionsIncluded { get; set; }
		public ButtonItem PrintProductColorOptionsPCI { get; set; }
		public ItemContainer PrintProductAdSizeStandart { get; set; }
		public ItemContainer PrintProductAdSizeSharePage { get; set; }
		public Label PrintProductStandartEqualSign { get; set; }
		public Label PrintProductStandartSquareMetric { get; set; }
		public Label PrintProductStandartSquareValue { get; set; }
		public ControlContainerItem PrintProductStandartEqualSignContainer { get; set; }
		public ControlContainerItem PrintProductStandartSquareMetricContainer { get; set; }
		public ControlContainerItem PrintProductStandartSquareValueContainer { get; set; }
		public LabelItem PrintProductAdSizeSharePagePercentOfPageLabel { get; set; }
		public LabelItem PrintProductAdSizeSharePageDimensionsLabel { get; set; }
		public RibbonBar PrintProductAdSizeRibbonBar { get; set; }
		public RibbonPanel PrintProductPanel { get; set; }
		public CheckBoxItem PrintProductAdSizeStandartSquare { get; set; }
		public CheckBoxItem PrintProductStandartPageSizeCheck { get; set; }
		public CheckBoxItem PrintProductSharePagePageSizeCheck { get; set; }
		public ComboBoxEdit PrintProductStandartPageSizeCombo { get; set; }
		public ComboBoxEdit PrintProductSharePagePageSizeCombo { get; set; }
		public ComboBoxEdit PrintProductRateCard { get; set; }
		public ComboBoxEdit PrintProductPercentOfPage { get; set; }
		public SpinEdit PrintProductStandartHeight { get; set; }
		public SpinEdit PrintProductStandartWidth { get; set; }
		public CheckedListBoxControl PrintProductSharePageSquare { get; set; }
		public SpinEdit PrintProductCostPerInch { get; set; }
		#endregion

		#region Digital Product
		public ButtonItem DigitalProductOptions { get; set; }
		public ButtonItem DigitalProductPreview { get; set; }
		public ButtonItem DigitalProductPowerPoint { get; set; }
		public ButtonItem DigitalProductEmail { get; set; }
		public ButtonItem DigitalProductTheme { get; set; }
		public ButtonItem DigitalProductSave { get; set; }
		public ButtonItem DigitalProductSaveAs { get; set; }
		public ButtonItem DigitalProductHelp { get; set; }
		#endregion

		#region Digital Package
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
		public ButtonItem MultiSummaryHelp { get; set; }
		public ButtonItem MultiSummarySave { get; set; }
		public ButtonItem MultiSummarySaveAs { get; set; }
		public ButtonItem MultiSummaryPreview { get; set; }
		public ButtonItem MultiSummaryEmail { get; set; }
		public ButtonItem MultiSummaryPowerPoint { get; set; }
		public ButtonItem MultiSummaryTheme { get; set; }
		public ButtonItem MultiSummaryDigitalLegend { get; set; }
		#endregion

		#region Snapshot
		public ButtonItem SnapshotOptions { get; set; }
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
		public ButtonItem AdPlanHelp { get; set; }
		public ButtonItem AdPlanSave { get; set; }
		public ButtonItem AdPlanSaveAs { get; set; }
		public ButtonItem AdPlanPreview { get; set; }
		public ButtonItem AdPlanEmail { get; set; }
		public ButtonItem AdPlanPowerPoint { get; set; }
		public ButtonItem AdPlanTheme { get; set; }
		#endregion

		#region Detailed Grid
		public ButtonItem DetailedGridOptions { get; set; }
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
		public ButtonItem MultiGridOptions { get; set; }
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
		public ButtonItem CalendarOptions { get; set; }
		public ButtonItem CalendarExport { get; set; }
		public ButtonItem CalendarHelp { get; set; }
		public ButtonItem CalendarSave { get; set; }
		public ButtonItem CalendarSaveAs { get; set; }
		public ButtonItem CalendarPreview { get; set; }
		public ButtonItem CalendarEmail { get; set; }
		public ButtonItem CalendarPowerPoint { get; set; }
		public ImageListBoxControl CalendarMonthList { get; set; }
		#endregion

		#region Summary
		public ButtonItem SummaryAddItem { get; set; }
		public ButtonItem SummaryHelp { get; set; }
		public ButtonItem SummarySave { get; set; }
		public ButtonItem SummarySaveAs { get; set; }
		public ButtonItem SummaryPreview { get; set; }
		public ButtonItem SummaryEmail { get; set; }
		public ButtonItem SummaryPowerPoint { get; set; }
		public ButtonItem SummaryTheme { get; set; }
		#endregion

		#region Rate Card
		public ButtonItem RateCardHelp { get; set; }
		public ComboBoxEdit RateCardCombo { get; set; }
		#endregion

		#endregion

		#region Forms
		public ScheduleSettingsControl ScheduleSettings { get; private set; }
		public PrintProductContainerControl PrintProductContainer { get; private set; }
		public DigitalProductContainerControl DigitalProductContainer { get; private set; }
		public AdWebPackageControl DigitalPackage { get; private set; }
		public SummariesControl Summaries { get; private set; }
		public GridsControl Grids { get; private set; }
		public CalendarsControl Calendars { get; private set; }
		public SummaryControl Summary { get; private set; }
		public RateCardControl RateCard { get; private set; }
		#endregion
	}
}