using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdScheduleBuilder
{
    public partial class FormMain : Form
    {
        private static FormMain _instance = null;
        private Control _currentControl = null;

        public event EventHandler<EventArgs> FloaterRequested;

        private FormMain()
        {
            InitializeComponent();

            #region Schedule Settings Events
            buttonItemHomeHelp.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemScheduleSettingsHelp_Click);
            buttonItemHomePublicationsAdd.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemPublicationsAdd_Click);
            buttonItemHomePublicationsClone.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemPublicationsClone_Click);
            buttonItemHomePublicationsDelete.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemPublicationsDelete_Click);
            buttonItemHomeSave.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemScheduleSettingsSave_Click);
            buttonItemHomeSaveAs.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemScheduleSettingsSaveAs_Click);
            comboBoxEditBusinessName.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            comboBoxEditDecisionMaker.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            comboBoxEditClientType.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            textEditAccountNumber.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            checkBoxItemHomeAccountNumber.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(CustomControls.ScheduleSettingsControl.Instance.checkBoxItemAccountNumber_CheckedChanged);
            buttonItemHomeSalesStrategyEmail.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemSalesStrategyEmail_Click);
            buttonItemHomeSalesStrategyFaceCall.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemSalesStrategyFaceCall_Click);
            buttonItemHomeSalesStrategyFax.Click += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemSalesStrategyFax_Click);
            buttonItemHomeSalesStrategyEmail.CheckedChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            buttonItemHomeSalesStrategyFax.CheckedChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            buttonItemHomeSalesStrategyFaceCall.CheckedChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            dateEditPresentationDate.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            dateEditFlightDatesStart.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            dateEditFlightDatesStart.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.FlightDateStartEditValueChanged);
            dateEditFlightDatesStart.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.CalcWeeksOnFlightDatesChange);
            dateEditFlightDatesEnd.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.SchedulePropertyEditValueChanged);
            dateEditFlightDatesEnd.EditValueChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.CalcWeeksOnFlightDatesChange);
            dateEditFlightDatesStart.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(CustomControls.ScheduleSettingsControl.Instance.dateEditFlightDatesStart_CloseUp);
            dateEditFlightDatesEnd.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(CustomControls.ScheduleSettingsControl.Instance.dateEditFlightDatesEnd_CloseUp);
            buttonItemHomeOptionsDelivery.CheckedChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemSalesStrategyDelivery_CheckedChanged);
            buttonItemHomeOptionsReadership.CheckedChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemSalesStrategyReadership_CheckedChanged);
            buttonItemHomeOptionsLogo.CheckedChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemSalesStrategyLogo_CheckedChanged);
            buttonItemHomeOptionsAbbreviation.CheckedChanged += new EventHandler(CustomControls.ScheduleSettingsControl.Instance.buttonItemSalesStrategyAbbreviation_CheckedChanged);
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
            buttonItemSchedulesAdPricingColumnInches.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemAdPricingColumnInches_Click);
            buttonItemSchedulesAdPricingFlat.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemAdPricingColumnInches_Click);
            buttonItemSchedulesAdPricingPagePercent.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemAdPricingColumnInches_Click);
            buttonItemSchedulesAdPricingColumnInches.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemAdPricing_CheckedChanged);
            buttonItemSchedulesAdPricingFlat.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemAdPricing_CheckedChanged);
            buttonItemSchedulesAdPricingPagePercent.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemAdPricing_CheckedChanged);
            checkBoxItemSchedulesAdSizeStandartSquare.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(CustomControls.ScheduleBuilderControl.Instance.checkBoxItemAdSizeStandartSquare_CheckedChanged);
            checkBoxItemSchedulesStandartPageSize.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(CustomControls.ScheduleBuilderControl.Instance.checkBoxItemSizeOptions_CheckedChanged);
            checkBoxItemSchedulesSharePagePageSize.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(CustomControls.ScheduleBuilderControl.Instance.checkBoxItemSizeOptions_CheckedChanged);
            spinEditStandartHeight.EditValueChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.spinEditStandart_EditValueChanged);
            spinEditStandartWidth.EditValueChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.spinEditStandart_EditValueChanged);
            comboBoxEditStandartPageSize.EditValueChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.comboBoxEditSizeOptions_EditValueChanged);
            comboBoxEditSharePagePageSize.EditValueChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.comboBoxEditSizeOptions_EditValueChanged);
            comboBoxEditRateCard.EditValueChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.comboBoxEditRateCard_EditValueChanged);
            comboBoxEditPercentOfPage.EditValueChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.comboBoxEditPercentOfPage_EditValueChanged);
            checkedListBoxControlSharePageSquare.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(CustomControls.ScheduleBuilderControl.Instance.checkedListBoxControlSharePageSquare_ItemCheck);
            buttonItemSchedulesColorOptionsSingle.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.ColorOptions_Click);
            buttonItemSchedulesColorOptionsSpot.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.ColorOptions_Click);
            buttonItemSchedulesColorOptionsFull.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.ColorOptions_Click);
            buttonItemSchedulesColorOptionsSingle.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.ColorOptions_CheckedChanged);
            buttonItemSchedulesColorOptionsSpot.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.ColorOptions_CheckedChanged);
            buttonItemSchedulesColorOptionsFull.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.ColorOptions_CheckedChanged);
            buttonItemSchedulesColorOptionsCostPerAd.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemColorOptions_Click);
            buttonItemSchedulesColorOptionsPercentOfAd.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemColorOptions_Click);
            buttonItemSchedulesColorOptionsIncluded.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemColorOptions_Click);
            buttonItemSchedulesColorOptionsPCI.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemColorOptions_Click);
            buttonItemSchedulesColorOptionsCostPerAd.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemColorOptions_CheckedChanged);
            buttonItemSchedulesColorOptionsPercentOfAd.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemColorOptions_CheckedChanged);
            buttonItemSchedulesColorOptionsIncluded.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemColorOptions_CheckedChanged);
            buttonItemSchedulesColorOptionsPCI.CheckedChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemColorOptions_CheckedChanged);
            spinEditCostPerInch.EditValueChanged += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.spinEditCostPerInch_EditValueChanged);
            buttonItemSchedulesAdd.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemAddInsert_Click);
            buttonItemSchedulesCloneInsert.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemCloneInsert_Click);
            buttonItemSchedulesDeleteInsert.Click += new EventHandler(CustomControls.ScheduleBuilderControl.Instance.buttonItemDeleteInsert_Click);
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
            buttonItemOverviewPreview.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesPreview_Click);
            buttonItemOverviewEmail.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesEmail_Click);
            buttonItemOverviewHelp.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesHelp_Click);
            buttonItemOverviewSave.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesSave_Click);
            buttonItemOverviewSaveAs.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesSaveAs_Click);
            buttonItemOverviewPowerPoint.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesPowerPoint_Click);
            buttonItemMultiSummaryPreview.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesPreview_Click);
            buttonItemMultiSummaryEmail.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesEmail_Click);
            buttonItemMultiSummaryHelp.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesHelp_Click);
            buttonItemMultiSummarySave.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesSave_Click);
            buttonItemMultiSummarySaveAs.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesSaveAs_Click);
            buttonItemMultiSummaryPowerPoint.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesPowerPoint_Click);
            buttonItemSnapshotPreview.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesPreview_Click);
            buttonItemSnapshotEmail.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesEmail_Click);
            buttonItemSnapshotHelp.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesHelp_Click);
            buttonItemSnapshotSave.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesSave_Click);
            buttonItemSnapshotSaveAs.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesSaveAs_Click);
            buttonItemSnapshotPowerPoint.Click += new EventHandler(OutputClasses.OutputControls.SummariesControl.Instance.buttonItemSummariesPowerPoint_Click);
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
            buttonItemDetailedGridHelp.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsHelp_Click);
            buttonItemDetailedGridSave.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsSave_Click);
            buttonItemDetailedGridSaveAs.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsSaveAs_Click);
            buttonItemDetailedGridColumnsColor.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsCost.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsDate.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsDeadline.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsDelivery.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsDiscounts.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsTotalCost.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsID.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsIndex.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsMechanicals.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsPageSize.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsPercentOfPage.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsDimensions.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsPCI.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsPublication.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsReadership.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsSection.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsSquare.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemDetailedGridColumnsColor.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsCost.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsDate.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsDeadline.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsDelivery.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsDiscounts.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsTotalCost.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsID.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsIndex.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsMechanicals.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsPageSize.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsPercentOfPage.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsDimensions.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsPCI.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsPublication.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsReadership.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsSection.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridColumnsSquare.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemDetailedGridDetails.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsDetails_CheckedChanged);
            buttonItemDetailedGridPowerPoint.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsPowerPoint_Click);
            buttonItemDetailedGridEmail.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsEmail_Click);
            buttonItemDetailedGridPreview.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsPreview_Click);

            buttonItemMultiGridHelp.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsHelp_Click);
            buttonItemMultiGridSave.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsSave_Click);
            buttonItemMultiGridSaveAs.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsSaveAs_Click);
            buttonItemMultiGridColumnsColor.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsCost.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsDate.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsDeadline.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsDelivery.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsDiscounts.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsTotalCost.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsID.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsIndex.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsMechanicals.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsPageSize.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsPercentOfPage.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsDimensions.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsPCI.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsPublication.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsReadership.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsSection.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsSquare.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemMultiGridColumnsColor.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsCost.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsDate.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsDeadline.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsDelivery.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsDiscounts.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsTotalCost.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsID.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsIndex.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsMechanicals.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsPageSize.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsPercentOfPage.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsDimensions.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsPCI.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsPublication.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsReadership.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsSection.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridColumnsSquare.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemMultiGridDetails.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsDetails_CheckedChanged);
            buttonItemMultiGridPowerPoint.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsPowerPoint_Click);
            buttonItemMultiGridEmail.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsEmail_Click);
            buttonItemMultiGridPreview.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsPreview_Click);

            buttonItemChronoGridHelp.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsHelp_Click);
            buttonItemChronoGridSave.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsSave_Click);
            buttonItemChronoGridSaveAs.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsSaveAs_Click);
            buttonItemChronoGridColumnsColor.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsCost.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsDate.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsDeadline.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsDelivery.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsDiscounts.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsTotalCost.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsID.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsIndex.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsMechanicals.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsPageSize.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsPercentOfPage.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsDimensions.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsPCI.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsPublication.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsReadership.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsSection.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsSquare.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_Click);
            buttonItemChronoGridColumnsColor.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsCost.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsDate.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsDeadline.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsDelivery.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsDiscounts.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsTotalCost.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsID.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsIndex.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsMechanicals.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsPageSize.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsPercentOfPage.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsDimensions.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsPCI.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsPublication.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsReadership.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsSection.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridColumnsSquare.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsColumns_CheckedChanged);
            buttonItemChronoGridDetails.CheckedChanged += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsDetails_CheckedChanged);
            buttonItemChronoGridPowerPoint.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsPowerPoint_Click);
            buttonItemChronoGridEmail.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsEmail_Click);
            buttonItemChronoGridPreview.Click += new EventHandler(OutputClasses.OutputControls.GridsControl.Instance.buttonItemGridsPreview_Click);
            #endregion

            #region Calendars Events
            buttonItemCalendarsHelp.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsHelp_Click);
            buttonItemCalendarsSave.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarSave_Click);
            buttonItemCalendarsSaveAs.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarSaveAs_Click);
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
            buttonItemCalendarsPreview.Click += new EventHandler(OutputClasses.OutputControls.CalendarsControl.Instance.buttonItemCalendarsPreview_Click);
            #endregion

            #region Rate Card Events
            buttonItemRateCardHelp.Click += new EventHandler(CustomControls.RateCardControl.Instance.buttonItemRateCardHelp_Click);
            comboBoxEditRateCards.EditValueChanged += new EventHandler(CustomControls.RateCardControl.Instance.comboBoxEditRateCards_EditValueChanged);
            #endregion

            #region Success Models Events
            buttonItemSuccessModelsHelp.Click += new EventHandler(CustomControls.ModelsOfSuccessContainerControl.Instance.buttonItemSuccessModelsHelp_Click);
            #endregion

            ribbonTabItemSuccessModels.Enabled = false;

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

                laStandartEqualSign.Font = new Font(laStandartEqualSign.Font.FontFamily, laStandartEqualSign.Font.Size - 2, laStandartEqualSign.Font.Style);
                laRateCards.Font = new Font(laRateCards.Font.FontFamily, laRateCards.Font.Size - 3, laRateCards.Font.Style);
                laStandartSquareMetric.Font = new Font(laStandartSquareMetric.Font.FontFamily, laStandartSquareMetric.Font.Size - 2, laStandartSquareMetric.Font.Style);
                laStandartSquareValue.Font = new Font(laStandartSquareValue.Font.FontFamily, laStandartSquareValue.Font.Size - 2, laStandartSquareValue.Font.Style);

                ribbonBarSchedulesAdPricingStrategy.RecalcLayout();
                ribbonBarSchedulesAdSize.RecalcLayout();
                ribbonBarHomeAdvertiser.RecalcLayout();
                ribbonBarCalendarsExit.RecalcLayout();
                ribbonBarCalendarsHelp.RecalcLayout();
                ribbonBarCalendarsPowerPoint.RecalcLayout();
                ribbonBarCalendarsSave.RecalcLayout();
                ribbonBarCalendarsDayOptions.RecalcLayout();
                ribbonBarCalendarsThemeColor.RecalcLayout();
                ribbonBarSchedulesColorPricing.RecalcLayout();
                ribbonBarHomeFlightDates.RecalcLayout();
                ribbonBarHomeExit.RecalcLayout();
                ribbonBarHomePublications.RecalcLayout();
                ribbonBarModelsOfSuccess.RecalcLayout();
                ribbonBarRateCards.RecalcLayout();
                ribbonBarSalesStrategy.RecalcLayout();
                ribbonBarSchedulesLines.RecalcLayout();
                ribbonBarHomeHelp.RecalcLayout();
                ribbonBarHomeSave.RecalcLayout();
                ribbonBarSchedulesExit.RecalcLayout();
                ribbonBarSchedulesHelp.RecalcLayout();
                ribbonBarSchedulesSave.RecalcLayout();
                ribbonBarSuccessModelsExit.RecalcLayout();
                ribbonBarSuccessModelsHelp.RecalcLayout();
                ribbonBarSnapshotExit.RecalcLayout();
                ribbonBarSnapshotHelp.RecalcLayout();
                ribbonBarSnapshotPowerPoint.RecalcLayout();
                ribbonBarSnapshotSave.RecalcLayout();
                ribbonBarSnapshotOptions.RecalcLayout();
                ribbonPanelBuildSchedules.PerformLayout();
                ribbonPanelScheduleSettings.PerformLayout();
                ribbonPanelCalendars.PerformLayout();
                ribbonPanelSuccessModels.PerformLayout();
                ribbonPanelSnapshot.PerformLayout();
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
                result = CustomControls.ScheduleSettingsControl.Instance.AllowToLeaveControl;
            }
            else if ((_currentControl == CustomControls.ScheduleBuilderControl.Instance))
            {
                result = CustomControls.ScheduleBuilderControl.Instance.AllowToLeaveControl;
            }
            else if ((_currentControl == OutputClasses.OutputControls.SummariesControl.Instance))
            {
                result = OutputClasses.OutputControls.SummariesControl.Instance.AllowToLeaveControl;
            }
            else if ((_currentControl == OutputClasses.OutputControls.GridsControl.Instance))
            {
                result = OutputClasses.OutputControls.GridsControl.Instance.AllowToLeaveControl;
            }
            else if ((_currentControl == OutputClasses.OutputControls.CalendarsControl.Instance))
            {
                result = OutputClasses.OutputControls.CalendarsControl.Instance.AllowToLeaveControl;
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
            ribbonTabItemOverview.Enabled = enable;
            ribbonTabItemMultiSummary.Enabled = enable;
            ribbonTabItemSnapshot.Enabled = enable;
            ribbonTabItemDetailedGrid.Enabled = enable;
            ribbonTabItemMultiGrid.Enabled = enable;
            ribbonTabItemChronoGrid.Enabled = enable;
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
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemOverview)
            {
                if (AllowToLeaveCurrentControl() || _currentControl == null)
                {
                    OutputClasses.OutputControls.SummariesControl.Instance.SelectSummary(OutputClasses.OutputControls.SummaryType.Overview);
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
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMultiSummary)
            {
                if (AllowToLeaveCurrentControl() || _currentControl == null)
                {
                    OutputClasses.OutputControls.SummariesControl.Instance.SelectSummary(OutputClasses.OutputControls.SummaryType.MultiSummary);
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
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemSnapshot)
            {
                if (AllowToLeaveCurrentControl() || _currentControl == null)
                {
                    OutputClasses.OutputControls.SummariesControl.Instance.SelectSummary(OutputClasses.OutputControls.SummaryType.Snapshot);
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
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemDetailedGrid)
            {
                if (AllowToLeaveCurrentControl() || _currentControl == null)
                {
                    OutputClasses.OutputControls.GridsControl.Instance.SelectGrid(OutputClasses.OutputControls.GridType.DetailedGrid);
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
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemMultiGrid)
            {
                if (AllowToLeaveCurrentControl() || _currentControl == null)
                {
                    OutputClasses.OutputControls.GridsControl.Instance.SelectGrid(OutputClasses.OutputControls.GridType.MultiGrid);
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
            else if (ribbonControl.SelectedRibbonTabItem == ribbonTabItemChronoGrid)
            {
                if (AllowToLeaveCurrentControl() || _currentControl == null)
                {
                    OutputClasses.OutputControls.GridsControl.Instance.SelectGrid(OutputClasses.OutputControls.GridType.ChronoGrid);
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

        private void buttonItemFloater_Click(object sender, EventArgs e)
        {
            if (FloaterRequested != null)
                this.FloaterRequested(this, e);
        }

        private void buttonItemExit_Click(object sender, EventArgs e)
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

        private void buttonItemDetailedGridId_Click(object sender, EventArgs e)
        {

        }
    }
}
