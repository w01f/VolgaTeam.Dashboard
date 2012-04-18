using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdScheduleBuilder
{
    public partial class FormMain : Form
    {
        private static FormMain _instance = null;
        private Control _currentControl = null;

        private FormMain()
        {
            InitializeComponent();

            #region Schedule Settings Events
            buttonItemScheduleSettingsHelp.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemScheduleSettingsHelp_Click);
            buttonItemPublicationsAdd.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemPublicationsAdd_Click);
            buttonItemPublicationsClone.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemPublicationsClone_Click);
            buttonItemPublicationsDelete.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemPublicationsDelete_Click);
            buttonItemScheduleSettingsSave.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemScheduleSettingsSave_Click);
            buttonItemScheduleSettingsSaveAs.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemScheduleSettingsSaveAs_Click);
            comboBoxEditBusinessName.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            comboBoxEditDecisionMaker.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            comboBoxEditClientType.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            textEditAccountNumber.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            checkBoxItemAccountNumber.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(CustomControls.ScheduleSettingsControl.Instance.checkBoxItemAccountNumber_CheckedChanged);
            buttonItemSalesStrategyEmail.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemSalesStrategyEmail_Click);
            buttonItemSalesStrategyFaceCall.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemSalesStrategyFaceCall_Click);
            buttonItemSalesStrategyFax.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemSalesStrategyFax_Click);
            buttonItemSalesStrategyEmail.CheckedChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            buttonItemSalesStrategyFax.CheckedChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            buttonItemSalesStrategyFaceCall.CheckedChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            dateEditPresentationDate.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            dateEditFlightDatesStart.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            dateEditFlightDatesStart.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.FlightDateStartEditValueChanged);
            dateEditFlightDatesStart.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.CalcWeeksOnFlightDatesChange);
            dateEditFlightDatesEnd.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            dateEditFlightDatesEnd.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.CalcWeeksOnFlightDatesChange);
            dateEditFlightDatesStart.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(CustomControls.ScheduleSettingsControl.Instance.dateEditFlightDatesStart_CloseUp);
            dateEditFlightDatesEnd.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(CustomControls.ScheduleSettingsControl.Instance.dateEditFlightDatesEnd_CloseUp);
            buttonItemSalesStrategyDelivery.CheckedChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemSalesStrategyDelivery_CheckedChanged);
            buttonItemSalesStrategyReadership.CheckedChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemSalesStrategyReadership_CheckedChanged);
            buttonItemSalesStrategyLogo.CheckedChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemSalesStrategyLogo_CheckedChanged);
            buttonItemSalesStrategyAbbreviation.CheckedChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemSalesStrategyAbbreviation_CheckedChanged);
            comboBoxEditBusinessName.Enter += new EventHandler(Editor_Enter);
            comboBoxEditBusinessName.MouseDown += new MouseEventHandler(Editor_MouseDown);
            comboBoxEditBusinessName.MouseUp += new MouseEventHandler(Editor_MouseUp);
            comboBoxEditDecisionMaker.Enter += new EventHandler(Editor_Enter);
            comboBoxEditDecisionMaker.MouseDown += new MouseEventHandler(Editor_MouseDown);
            comboBoxEditDecisionMaker.MouseUp += new MouseEventHandler(Editor_MouseUp);
            comboBoxEditClientType.Enter += new EventHandler(Editor_Enter);
            comboBoxEditClientType.MouseDown += new MouseEventHandler(Editor_MouseDown);
            comboBoxEditClientType.MouseUp += new MouseEventHandler(Editor_MouseUp);
            textEditAccountNumber.Enter += new EventHandler(Editor_Enter);
            textEditAccountNumber.MouseDown += new MouseEventHandler(Editor_MouseDown);
            textEditAccountNumber.MouseUp += new MouseEventHandler(Editor_MouseUp);
            #endregion

            #region Schedule Builder Events
            buttonItemSchedulesHelp.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesHelp_Click);
            buttonItemSchedulesSave.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesSave_Click);
            buttonItemSchedulesSaveAs.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemSchedulesSaveAs_Click);
            buttonItemAdPricingColumnInches.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemAdPricingColumnInches_Click);
            buttonItemAdPricingFlat.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemAdPricingColumnInches_Click);
            buttonItemAdPricingPagePercent.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemAdPricingColumnInches_Click);
            buttonItemAdPricingColumnInches.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemAdPricing_CheckedChanged);
            buttonItemAdPricingFlat.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemAdPricing_CheckedChanged);
            buttonItemAdPricingPagePercent.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemAdPricing_CheckedChanged);
            checkBoxItemAdSizeStandartSquare.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(CustomControls.ScheduleBuilderControl.Instance.checkBoxItemAdSizeStandartSquare_CheckedChanged);
            checkBoxItemStandartPageSize.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(CustomControls.ScheduleBuilderControl.Instance.checkBoxItemSizeOptions_CheckedChanged);
            checkBoxItemSharePagePageSize.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(CustomControls.ScheduleBuilderControl.Instance.checkBoxItemSizeOptions_CheckedChanged);
            spinEditStandartHeight.EditValueChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.spinEditStandart_EditValueChanged);
            spinEditStandartWidth.EditValueChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.spinEditStandart_EditValueChanged);
            comboBoxEditStandartPageSize.EditValueChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.comboBoxEditSizeOptions_EditValueChanged);
            comboBoxEditSharePagePageSize.EditValueChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.comboBoxEditSizeOptions_EditValueChanged);
            comboBoxEditRateCard.EditValueChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.comboBoxEditRateCard_EditValueChanged);
            comboBoxEditPercentOfPage.EditValueChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.comboBoxEditPercentOfPage_EditValueChanged);
            checkedListBoxControlSharePageSquare.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(CustomControls.ScheduleBuilderControl.Instance.checkedListBoxControlSharePageSquare_ItemCheck);
            buttonItemColorOptionsSingle.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.ColorOptions_Click);
            buttonItemColorOptionsSpot.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.ColorOptions_Click);
            buttonItemColorOptionsFull.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.ColorOptions_Click);
            buttonItemColorOptionsSingle.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.ColorOptions_CheckedChanged);
            buttonItemColorOptionsSpot.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.ColorOptions_CheckedChanged);
            buttonItemColorOptionsFull.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.ColorOptions_CheckedChanged);
            buttonItemColorOptionsCostPerAd.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemColorOptions_Click);
            buttonItemColorOptionsPercentOfAd.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemColorOptions_Click);
            buttonItemColorOptionsIncluded.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemColorOptions_Click);
            buttonItemColorOptionsPCI.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemColorOptions_Click);
            buttonItemColorOptionsCostPerAd.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemColorOptions_CheckedChanged);
            buttonItemColorOptionsPercentOfAd.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemColorOptions_CheckedChanged);
            buttonItemColorOptionsIncluded.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemColorOptions_CheckedChanged);
            buttonItemColorOptionsPCI.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemColorOptions_CheckedChanged);
            spinEditCostPerInch.EditValueChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.spinEditCostPerInch_EditValueChanged);
            buttonItemSchedulesAdd.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemAddInsert_Click);
            buttonItemCloneInsert.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemCloneInsert_Click);
            buttonItemDeleteInsert.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemDeleteInsert_Click);
            spinEditCostPerInch.Enter += new EventHandler(Editor_Enter);
            spinEditCostPerInch.MouseDown += new MouseEventHandler(Editor_MouseDown);
            spinEditCostPerInch.MouseUp += new MouseEventHandler(Editor_MouseUp);
            spinEditStandartHeight.Enter += new EventHandler(Editor_Enter);
            spinEditStandartHeight.MouseDown += new MouseEventHandler(Editor_MouseDown);
            spinEditStandartHeight.MouseUp += new MouseEventHandler(Editor_MouseUp);
            spinEditStandartWidth.Enter += new EventHandler(Editor_Enter);
            spinEditStandartWidth.MouseDown += new MouseEventHandler(Editor_MouseDown);
            spinEditStandartWidth.MouseUp += new MouseEventHandler(Editor_MouseUp);
            comboBoxEditStandartPageSize.Enter += new EventHandler(Editor_Enter);
            comboBoxEditStandartPageSize.MouseDown += new MouseEventHandler(Editor_MouseDown);
            comboBoxEditStandartPageSize.MouseUp += new MouseEventHandler(Editor_MouseUp);
            comboBoxEditSharePagePageSize.Enter += new EventHandler(Editor_Enter);
            comboBoxEditSharePagePageSize.MouseDown += new MouseEventHandler(Editor_MouseDown);
            comboBoxEditSharePagePageSize.MouseUp += new MouseEventHandler(Editor_MouseUp);
            #endregion

            #region Summaries Events
            buttonItemSummariesEmail.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesEmail_Click);
            buttonItemSummariesHelp.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesHelp_Click);
            buttonItemSummariesSave.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesSave_Click);
            buttonItemSummariesSaveAs.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesSaveAs_Click);
            buttonItemSummariesBasicOverview.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesBasicOverview_Click);
            buttonItemSummariesMultiSummary.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesMultiSummary_Click);
            buttonItemSummariesSnapshot.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesSnapshot_Click);
            buttonItemSummariesPowerPoint.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesPowerPoint_Click);
            buttonItemSnapshotAvgAdCost.CheckedChanged += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotToggle_CheckedChanged);
            buttonItemSnapshotAvgFinalCost.CheckedChanged += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotToggle_CheckedChanged);
            buttonItemSnapshotAvgPCI.CheckedChanged += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotToggle_CheckedChanged);
            buttonItemSnapshotDelivery.CheckedChanged += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotToggle_CheckedChanged);
            buttonItemSnapshotDimensions.CheckedChanged += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotToggle_CheckedChanged);
            buttonItemSnapshotLogo.CheckedChanged += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotToggle_CheckedChanged);
            buttonItemSnapshotPageSize.CheckedChanged += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotToggle_CheckedChanged);
            buttonItemSnapshotPercentOfPage.CheckedChanged += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotToggle_CheckedChanged);
            buttonItemSnapshotReadership.CheckedChanged += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotToggle_CheckedChanged);
            buttonItemSnapshotSquare.CheckedChanged += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotToggle_CheckedChanged);
            buttonItemSnapshotTotalColorRate.CheckedChanged += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotToggle_CheckedChanged);
            buttonItemSnapshotTotalDiscounts.CheckedChanged += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotToggle_CheckedChanged);
            buttonItemSnapshotTotalFinalCost.CheckedChanged += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotToggle_CheckedChanged);
            buttonItemSnapshotTotalInserts.CheckedChanged += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotToggle_CheckedChanged);
            buttonItemSnapshotTotalSquare.CheckedChanged += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotToggle_CheckedChanged);
            buttonItemSnapshotAvgAdCost.Click += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotButton_Click);
            buttonItemSnapshotAvgFinalCost.Click += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotButton_Click);
            buttonItemSnapshotAvgPCI.Click += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotButton_Click);
            buttonItemSnapshotDelivery.Click += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotButton_Click);
            buttonItemSnapshotDimensions.Click += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotButton_Click);
            buttonItemSnapshotLogo.Click += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotButton_Click);
            buttonItemSnapshotPageSize.Click += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotButton_Click);
            buttonItemSnapshotPercentOfPage.Click += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotButton_Click);
            buttonItemSnapshotReadership.Click += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotButton_Click);
            buttonItemSnapshotSquare.Click += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotButton_Click);
            buttonItemSnapshotTotalColorRate.Click += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotButton_Click);
            buttonItemSnapshotTotalDiscounts.Click += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotButton_Click);
            buttonItemSnapshotTotalFinalCost.Click += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotButton_Click);
            buttonItemSnapshotTotalInserts.Click += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotButton_Click);
            buttonItemSnapshotTotalSquare.Click += new EventHandler(OutputClasses.OutputControls.OutputSnapshotControl.Instance.buttonItemSnapshotButton_Click);
            #endregion

            #region Grids Events
            buttonItemGridsHelp.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsHelp_Click);
            buttonItemGridsSave.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsSave_Click);
            buttonItemGridsSaveAs.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsSaveAs_Click);
            buttonItemGridsDetailedGrid.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsDetailedGrid_Click);
            buttonItemGridsMultiGrid.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsMultiGrid_Click);
            buttonItemGridsChronological.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsChronological_Click);
            buttonItemGridsColumnsColor.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsCost.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsDate.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsDeadline.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsDelivery.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsDiscounts.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsFinalCost.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsID.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsIndex.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsMechanicals.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsPageSize.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsPercentOfPage.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsDimensions.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsPCI.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsPublication.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsReadership.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsSection.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsSquare.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemGridsColumnsColor.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsCost.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsDate.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsDeadline.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsDelivery.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsDiscounts.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsFinalCost.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsID.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsIndex.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsMechanicals.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsPageSize.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsPercentOfPage.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsDimensions.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsPCI.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsPublication.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsReadership.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsSection.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsColumnsSquare.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemGridsDetails.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsDetails_CheckedChanged);
            buttonItemGridsPowerPoint.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsPowerPoint_Click);
            buttonItemGridsEmail.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsEmail_Click);
            #endregion

            #region Calendars Events
            buttonItemCalendarsHelp.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsHelp_Click);
            buttonItemCalendarsSave.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarSave_Click);
            buttonItemCalendarsSaveAs.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarSaveAs_Click);
            listBoxControlCalendar.SelectedIndexChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.comboBoxEditCalendar_SelectedIndexChanged);
            buttonItemCalendarsShowAbbreviation.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowColor.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowCost.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowSection.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowAdSize.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggledSize_CheckedChanged);
            buttonItemCalendarsShowPageSize.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggledSize_CheckedChanged);
            buttonItemCalendarsShowPercentOfPage.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggledSize_CheckedChanged);
            buttonItemCalendarsShowAdSize.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowPageSize.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowBigDates.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowPercentOfPage.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowLegend.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowTitle.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowDate.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowBusinessName.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowDecisionMaker.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowLogo.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowTotalCost.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowAvgCost.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowTotalAds.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowActiveDays.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowComment.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsShowLegend.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggledAdditional_Click);
            buttonItemCalendarsShowTitle.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggledAdditional_Click);
            buttonItemCalendarsShowDate.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggledAdditional_Click);
            buttonItemCalendarsShowBusinessName.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggledAdditional_Click);
            buttonItemCalendarsShowDecisionMaker.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggledAdditional_Click);
            buttonItemCalendarsShowLogo.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggledAdditional_Click);
            buttonItemCalendarsShowTotalCost.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggledAdditional_Click);
            buttonItemCalendarsShowAvgCost.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggledAdditional_Click);
            buttonItemCalendarsShowTotalAds.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggledAdditional_Click);
            buttonItemCalendarsShowActiveDays.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggledAdditional_Click);
            buttonItemCalendarsShowComment.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggledAdditional_Click);
            buttonItemCalendarsColorBlack.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsThemeColor_Click);
            buttonItemCalendarsColorBlue.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsThemeColor_Click);
            buttonItemCalendarsColorGray.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsThemeColor_Click);
            buttonItemCalendarsColorGreen.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsThemeColor_Click);
            buttonItemCalendarsColorOrange.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsThemeColor_Click);
            buttonItemCalendarsColorTeal.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsThemeColor_Click);
            buttonItemCalendarsColorBlack.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsColorBlue.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsColorGray.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsColorGreen.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsColorOrange.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsColorTeal.CheckedChanged += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsToggled_CheckedChanged);
            buttonItemCalendarsPowerPoint.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsPowerPoint_Click);
            buttonItemCalendarsEmail.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsEmail_Click);
            #endregion

            #region Rate Card Events
            buttonItemRateCardHelp.Click += new EventHandler(CustomControls.RateCardControl.Instance.buttonItemRateCardHelp_Click);
            comboBoxEditRateCards.EditValueChanged += new EventHandler(CustomControls.RateCardControl.Instance.comboBoxEditRateCards_EditValueChanged);
            #endregion

            #region Success Models Events
            buttonItemSuccessModelsHelp.Click += new EventHandler(CustomControls.ModelsOfSuccessContainerControl.Instance.buttonItemSuccessModelsHelp_Click);
            #endregion

            if ((base.CreateGraphics()).DpiX > 96)
            {
                Font font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 1, styleController.Appearance.Font.Style);
                ribbonControl.Font = font;
                styleController.Appearance.Font = font;
                styleController.AppearanceDisabled.Font = font;
                styleController.AppearanceDropDown.Font = font;
                styleController.AppearanceDropDownHeader.Font = font;
                styleController.AppearanceFocused.Font = font;
                styleController.AppearanceReadOnly.Font = font;
                comboBoxEditBusinessName.Font = font;
                comboBoxEditClientType.Font = font;
                comboBoxEditDecisionMaker.Font = font;
                textEditAccountNumber.Font = font;
                spinEditCostPerInch.Font = font;
                spinEditStandartHeight.Font = font;
                spinEditStandartWidth.Font = font;
                comboBoxEditPercentOfPage.Font = font;
                comboBoxEditRateCard.Font = font;
                comboBoxEditRateCards.Font = font;
                comboBoxEditSharePagePageSize.Font = font;
                comboBoxEditStandartPageSize.Font = font;
                checkedListBoxControlSharePageSquare.Font = font;
                dateEditFlightDatesEnd.Font = font;
                dateEditFlightDatesStart.Font = font;
                dateEditPresentationDate.Font = font;

                listBoxControlCalendar.Font = new Font(listBoxControlCalendar.Font.FontFamily, listBoxControlCalendar.Font.Size - 2, listBoxControlCalendar.Font.Style);
                laStandartEqualSign.Font = new Font(laStandartEqualSign.Font.FontFamily, laStandartEqualSign.Font.Size - 2, laStandartEqualSign.Font.Style);
                laRateCards.Font = new Font(laRateCards.Font.FontFamily, laRateCards.Font.Size - 3, laRateCards.Font.Style);
                laStandartSquareMetric.Font = new Font(laStandartSquareMetric.Font.FontFamily, laStandartSquareMetric.Font.Size - 2, laStandartSquareMetric.Font.Style);
                laStandartSquareValue.Font = new Font(laStandartSquareValue.Font.FontFamily, laStandartSquareValue.Font.Size - 2, laStandartSquareValue.Font.Style);

                ribbonBarAdPricingStrategy.RecalcLayout();
                ribbonBarAdSize.RecalcLayout();
                ribbonBarAdvertiser.RecalcLayout();
                ribbonBarCalendarsCalendar.RecalcLayout();
                ribbonBarCalendarsExit.RecalcLayout();
                ribbonBarCalendarsHelp.RecalcLayout();
                ribbonBarCalendarsPowerPoint.RecalcLayout();
                ribbonBarCalendarsSave.RecalcLayout();
                ribbonBarCalendarsDayOptions.RecalcLayout();
                ribbonBarCalendarsThemeColor.RecalcLayout();
                ribbonBarColorPricing.RecalcLayout();
                ribbonBarFlightDates.RecalcLayout();
                ribbonBarGridHelp.RecalcLayout();
                ribbonBarGridsDetails.RecalcLayout();
                ribbonBarGridsColumns.RecalcLayout();
                ribbonBarGridsExit.RecalcLayout();
                ribbonBarGridsPowerPoint.RecalcLayout();
                ribbonBarGridsSave.RecalcLayout();
                ribbonBarGridsStyles.RecalcLayout();
                ribbonBarHomeExit.RecalcLayout();
                ribbonBarHomePublications.RecalcLayout();
                ribbonBarModelsOfSuccess.RecalcLayout();
                ribbonBarRateCards.RecalcLayout();
                ribbonBarSalesStrategy.RecalcLayout();
                ribbonBarSchedulesLines.RecalcLayout();
                ribbonBarScheduleSettingsHelp.RecalcLayout();
                ribbonBarScheduleSettingsSave.RecalcLayout();
                ribbonBarSchedulesExit.RecalcLayout();
                ribbonBarSchedulesHelp.RecalcLayout();
                ribbonBarSchedulesSave.RecalcLayout();
                ribbonBarSuccessModelsExit.RecalcLayout();
                ribbonBarSuccessModelsHelp.RecalcLayout();
                ribbonBarSummariesBasicOverview.RecalcLayout();
                ribbonBarSummariesExit.RecalcLayout();
                ribbonBarSummariesHelp.RecalcLayout();
                ribbonBarSummariesMultiSummary.RecalcLayout();
                ribbonBarSummariesPowerPoint.RecalcLayout();
                ribbonBarSummariesSave.RecalcLayout();
                ribbonBarSummariesSnapshot.RecalcLayout();
                ribbonPanelBuildSchedules.PerformLayout();
                ribbonPanelScheduleSettings.PerformLayout();
                ribbonPanelCalendars.PerformLayout();
                ribbonPanelGrids.PerformLayout();
                ribbonPanelSuccessModels.PerformLayout();
                ribbonPanelSummaries.PerformLayout();
            }
        }

        public static void RemoveInstance()
        {
            _instance.Dispose();
            _instance = null;
        }

        public static FormMain Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FormMain();
                return _instance;
            }
        }

        private bool AllowToLeaveCurrentControl()
        {
            bool result = false;
            if ((_currentControl == CustomControls.ScheduleSettingsControl.Instance))
            {
                if (CustomControls.ScheduleSettingsControl.Instance.AllowToLeaveControl)
                    result = true;
                else
                {
                    ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                    ribbonControl.SelectedRibbonTabItem = ribbonTabItemScheduleSettings;
                    ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                }
            }
            else if ((_currentControl == CustomControls.ScheduleBuilderControl.Instance))
            {
                if (CustomControls.ScheduleBuilderControl.Instance.AllowToLeaveControl)
                    result = true;
                else
                {
                    ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                    ribbonControl.SelectedRibbonTabItem = ribbonTabItemBuildSchedules;
                    ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                }
            }
            else if ((_currentControl == OutputClasses.OutputControls.SummariesControl.Instance))
            {
                if (OutputClasses.OutputControls.SummariesControl.Instance.AllowToLeaveControl)
                    result = true;
                else
                {
                    ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                    ribbonControl.SelectedRibbonTabItem = ribbonTabItemSummaries;
                    ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                }
            }
            else if ((_currentControl == OutputClasses.OutputControls.GridsControl.Instance))
            {
                if (OutputClasses.OutputControls.GridsControl.Instance.AllowToLeaveControl)
                    result = true;
                else
                {
                    ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                    ribbonControl.SelectedRibbonTabItem = ribbonTabItemGrids;
                    ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                }
            }
            else if ((_currentControl == OutputClasses.OutputControls.CalendarsControl.Instance))
            {
                if (OutputClasses.OutputControls.CalendarsControl.Instance.AllowToLeaveControl)
                    result = true;
                else
                {
                    ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                    ribbonControl.SelectedRibbonTabItem = ribbonTabItemCalendars;
                    ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
                }
            }
            else
                result = true;
            return result;
        }

        public void UpdateScheduleTab(bool enable)
        {
            ribbonTabItemBuildSchedules.Enabled = enable;
        }

        public void UpdateOutputTabs(bool enable)
        {
            ribbonTabItemSummaries.Enabled = enable;
            ribbonTabItemGrids.Enabled = enable;
            ribbonTabItemCalendars.Enabled = enable;
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.SelectedWizard))
                FormMain.Instance.Text = "Ad Schedule Builder - " + ConfigurationClasses.SettingsManager.Instance.SelectedWizard + " - " + ConfigurationClasses.SettingsManager.Instance.Size;
            ribbonControl.Enabled = false;
            using (ToolForms.FormProgress form = new ToolForms.FormProgress())
            {
                form.laProgress.Text = "Chill-Out for a few seconds...\nLoading Ad Schedule...";
                form.TopMost = true;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(delegate()
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        CustomControls.ScheduleSettingsControl.Instance.LoadSchedule(false);
                        CustomControls.ScheduleBuilderControl.Instance.LoadSchedule(false);
                        OutputClasses.OutputControls.OutputBasicOverviewControl.Instance.UpdateOutput(false);
                        OutputClasses.OutputControls.OutputCalendarControl.Instance.UpdateOutput(false);
                        OutputClasses.OutputControls.OutputChronologicalControl.Instance.UpdateOutput(false);
                        OutputClasses.OutputControls.OutputDetailedGridControl.Instance.UpdateOutput(false);
                        OutputClasses.OutputControls.OutputMultiGridControl.Instance.UpdateOutput(false);
                        OutputClasses.OutputControls.OutputMultiSummaryControl.Instance.UpdateOutput(false);
                        OutputClasses.OutputControls.OutputSnapshotControl.Instance.UpdateOutput(false);

                    });
                }));
                thread.Start();

                form.Show();

                while (thread.IsAlive)
                    System.Windows.Forms.Application.DoEvents();
                form.Close();
            }

            FormMain.Instance.ribbonTabItemRateCard.Visible = BusinessClasses.RateCardManager.Instance.RateCardFolders.Count > 0;
            ribbonControl.SelectedRibbonTabItem = ribbonTabItemScheduleSettings;
            ribbonControl.SelectedRibbonTabChanged -= new EventHandler(ribbonControl_SelectedRibbonTabChanged);
            ribbonControl_SelectedRibbonTabChanged(null, null);
            ribbonControl.SelectedRibbonTabChanged += new EventHandler(ribbonControl_SelectedRibbonTabChanged);
            if (ConfigurationClasses.SettingsManager.Instance.SizeWidth == 10 && ConfigurationClasses.SettingsManager.Instance.SizeHeght == 5.63)
                buttonItemCalendarsPowerPoint.Enabled = false;
            else
                buttonItemCalendarsPowerPoint.Enabled = true;
            ribbonControl.Enabled = true;
        }

        private void ribbonControl_SelectedRibbonTabChanged(object sender, EventArgs e)
        {
            if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemScheduleSettings)
            {
                if (AllowToLeaveCurrentControl() || _currentControl == null)
                {
                    if (!pnMain.Controls.Contains(CustomControls.ScheduleSettingsControl.Instance))
                    {
                        Application.DoEvents();
                        pnEmpty.BringToFront();
                        Application.DoEvents();
                        pnMain.Controls.Add(CustomControls.ScheduleSettingsControl.Instance);
                        Application.DoEvents();
                        pnMain.BringToFront();
                        Application.DoEvents();
                    }
                    CustomControls.ScheduleSettingsControl.Instance.BringToFront();
                    _currentControl = CustomControls.ScheduleSettingsControl.Instance;
                }
                else
                    _currentControl.BringToFront();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemBuildSchedules)
            {
                if (AllowToLeaveCurrentControl() || _currentControl == null)
                {
                    if (!pnMain.Controls.Contains(CustomControls.ScheduleBuilderControl.Instance))
                    {
                        Application.DoEvents();
                        pnEmpty.BringToFront();
                        Application.DoEvents();
                        pnMain.Controls.Add(CustomControls.ScheduleBuilderControl.Instance);
                        Application.DoEvents();
                        pnMain.BringToFront();
                        Application.DoEvents();
                    }
                    CustomControls.ScheduleBuilderControl.Instance.BringToFront();
                    _currentControl = CustomControls.ScheduleBuilderControl.Instance;
                }
                else
                    _currentControl.BringToFront();

            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSummaries)
            {
                if (AllowToLeaveCurrentControl() || _currentControl == null)
                {
                    OutputClasses.OutputControls.SummariesControl.Instance.UpdatePageAccordingToggledButton();
                    if (!pnMain.Controls.Contains(OutputClasses.OutputControls.SummariesControl.Instance))
                    {
                        Application.DoEvents();
                        pnEmpty.BringToFront();
                        Application.DoEvents();
                        pnMain.Controls.Add(OutputClasses.OutputControls.SummariesControl.Instance);
                        Application.DoEvents();
                        pnMain.BringToFront();
                        Application.DoEvents();
                    }
                    OutputClasses.OutputControls.SummariesControl.Instance.BringToFront();
                    _currentControl = OutputClasses.OutputControls.SummariesControl.Instance;
                }
                else
                    _currentControl.BringToFront();
            }

            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemGrids)
            {
                if (AllowToLeaveCurrentControl() || _currentControl == null)
                {
                    OutputClasses.OutputControls.GridsControl.Instance.UpdatePageAccordingToggledButton();
                    if (!pnMain.Controls.Contains(OutputClasses.OutputControls.GridsControl.Instance))
                    {
                        Application.DoEvents();
                        pnEmpty.BringToFront();
                        Application.DoEvents();
                        pnMain.Controls.Add(OutputClasses.OutputControls.GridsControl.Instance);
                        Application.DoEvents();
                        pnMain.BringToFront();
                        Application.DoEvents();
                    }
                    OutputClasses.OutputControls.GridsControl.Instance.BringToFront();
                    _currentControl = OutputClasses.OutputControls.GridsControl.Instance;
                }
                else
                    _currentControl.BringToFront();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemCalendars)
            {
                if (AllowToLeaveCurrentControl() || _currentControl == null)
                {
                    OutputClasses.OutputControls.CalendarsControl.Instance.UpdatePageAccordingToggledButton();
                    if (!pnMain.Controls.Contains(OutputClasses.OutputControls.CalendarsControl.Instance))
                    {
                        Application.DoEvents();
                        pnEmpty.BringToFront();
                        Application.DoEvents();
                        pnMain.Controls.Add(OutputClasses.OutputControls.CalendarsControl.Instance);
                        Application.DoEvents();
                        pnMain.BringToFront();
                        Application.DoEvents();
                    }
                    OutputClasses.OutputControls.CalendarsControl.Instance.BringToFront();
                    _currentControl = OutputClasses.OutputControls.CalendarsControl.Instance;
                }
                else
                    _currentControl.BringToFront();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemRateCard)
            {
                if (AllowToLeaveCurrentControl() || _currentControl == null)
                {
                    if (!pnMain.Controls.Contains(CustomControls.RateCardControl.Instance))
                    {
                        Application.DoEvents();
                        pnEmpty.BringToFront();
                        Application.DoEvents();
                        CustomControls.RateCardControl.Instance.LoadRateCards();
                        pnMain.Controls.Add(CustomControls.RateCardControl.Instance);
                        Application.DoEvents();
                        pnMain.BringToFront();
                        Application.DoEvents();
                    }
                    CustomControls.RateCardControl.Instance.BringToFront();
                    _currentControl = CustomControls.RateCardControl.Instance;
                }
                else
                    _currentControl.BringToFront();
            }
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSuccessModels)
            {
                if (AllowToLeaveCurrentControl() || _currentControl == null)
                {
                    if (!pnMain.Controls.Contains(CustomControls.ModelsOfSuccessContainerControl.Instance))
                    {
                        Application.DoEvents();
                        pnEmpty.BringToFront();
                        Application.DoEvents();
                        pnMain.Controls.Add(CustomControls.ModelsOfSuccessContainerControl.Instance);
                        Application.DoEvents();
                        pnMain.BringToFront();
                        Application.DoEvents();
                    }
                    CustomControls.ModelsOfSuccessContainerControl.Instance.BringToFront();
                    CustomControls.ModelsOfSuccessContainerControl.Instance.UpdateSuccessModels();
                    _currentControl = CustomControls.ModelsOfSuccessContainerControl.Instance;
                }
                else
                    _currentControl.BringToFront();
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool result = true;
            if (_currentControl == CustomControls.ScheduleSettingsControl.Instance)
                result = CustomControls.ScheduleSettingsControl.Instance.AllowToLeaveControl;
            else if (_currentControl == CustomControls.ScheduleBuilderControl.Instance)
                result = CustomControls.ScheduleBuilderControl.Instance.AllowToLeaveControl;
            else if (_currentControl == OutputClasses.OutputControls.SummariesControl.Instance)
                result = OutputClasses.OutputControls.SummariesControl.Instance.AllowToLeaveControl;
            else if (_currentControl == OutputClasses.OutputControls.GridsControl.Instance)
                result = OutputClasses.OutputControls.GridsControl.Instance.AllowToLeaveControl;
            else if (_currentControl == OutputClasses.OutputControls.CalendarsControl.Instance)
                result = OutputClasses.OutputControls.CalendarsControl.Instance.AllowToLeaveControl;
        }

        private void buttonItemHomeExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Select All in Editor Handlers
        private bool enter = false;
        private bool needSelect = false;

        public void Editor_Enter(object sender, EventArgs e)
        {
            enter = true;
            BeginInvoke(new MethodInvoker(ResetEnterFlag));
        }

        public void Editor_MouseUp(object sender, MouseEventArgs e)
        {
            if (needSelect)
            {
                (sender as DevExpress.XtraEditors.BaseEdit).SelectAll();
            }
        }

        public void Editor_MouseDown(object sender, MouseEventArgs e)
        {
            needSelect = enter;
        }

        private void ResetEnterFlag()
        {
            enter = false;
        }
        #endregion

        private void buttonItemCalendarsShowTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
