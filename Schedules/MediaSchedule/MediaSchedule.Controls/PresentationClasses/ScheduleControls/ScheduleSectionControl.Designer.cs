using System.Windows.Forms;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.ScheduleControls
{
    partial class ScheduleSectionControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduleSectionControl));
			DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
			this.gridControlSchedule = new DevExpress.XtraGrid.GridControl();
			this.advBandedGridViewSchedule = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridBandId = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnIndex = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandLogo = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnLogoImage = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.gridBandStation = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnStation = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemComboBoxStations = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.bandedGridColumnDaypart = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemComboBoxDayparts = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.gridBandProgram = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemPopupContainerEditProgram = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
			this.popupContainerControlProgramSource = new DevExpress.XtraEditors.PopupContainerControl();
			this.gridControlProgramSource = new DevExpress.XtraGrid.GridControl();
			this.gridViewProgramSource = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnProgramSourceId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnProgramSourceName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnProgramSourceStation = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnProgramSourceDaypart = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnProgramSourceDay = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnProgramSourceTime = new DevExpress.XtraGrid.Columns.GridColumn();
			this.laProgramSourceInfo = new System.Windows.Forms.Label();
			this.bandedGridColumnDay = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemComboBoxDays = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.bandedGridColumnTime = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemComboBoxTimes = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.bandedGridColumnLength = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemComboBoxLengths = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.gridBandRate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnRate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditRate = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.bandedGridColumnRating = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditRating = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.gridBandSpots = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.gridBandTotals = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnTotalSpots = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumnCost = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumnGRP = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumnCPP = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumnLogoSource = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditSpot = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.repositoryItemSpinEdit000s = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.repositoryItemTextEditProgram = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.pnTop = new System.Windows.Forms.Panel();
			this.quarterSelectorControl = new NewBizWiz.MediaSchedule.Controls.PresentationClasses.ScheduleControls.QuarterSelectorControl();
			this.laScheduleInfo = new System.Windows.Forms.Label();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.pnBottom = new System.Windows.Forms.Panel();
			this.pnAgencyDiscount = new System.Windows.Forms.Panel();
			this.laAgencyDiscountValue = new System.Windows.Forms.Label();
			this.laAgencyDiscountTitle = new System.Windows.Forms.Label();
			this.pnNetRate = new System.Windows.Forms.Panel();
			this.laNetRateValue = new System.Windows.Forms.Label();
			this.laNetRateTitle = new System.Windows.Forms.Label();
			this.pnTotalCost = new System.Windows.Forms.Panel();
			this.laTotalCostValue = new System.Windows.Forms.Label();
			this.laTotalCostTitle = new System.Windows.Forms.Label();
			this.pnAvgRate = new System.Windows.Forms.Panel();
			this.laAvgRateValue = new System.Windows.Forms.Label();
			this.laAvgRateTitle = new System.Windows.Forms.Label();
			this.pnTotalCPP = new System.Windows.Forms.Panel();
			this.laTotalCPPValue = new System.Windows.Forms.Label();
			this.laTotalCPPTitle = new System.Windows.Forms.Label();
			this.pnTotalGRP = new System.Windows.Forms.Panel();
			this.laTotalGRPValue = new System.Windows.Forms.Label();
			this.laTotalGRPTitle = new System.Windows.Forms.Label();
			this.pnTotalSpots = new System.Windows.Forms.Panel();
			this.laTotalSpotsValue = new System.Windows.Forms.Label();
			this.laTotalSpotsTitle = new System.Windows.Forms.Label();
			this.pnTotalPeriods = new System.Windows.Forms.Panel();
			this.laTotalPeriodsValue = new System.Windows.Forms.Label();
			this.laTotalPeriodsTitle = new System.Windows.Forms.Label();
			this.pnPageSchedule = new System.Windows.Forms.Panel();
			this.labelControlFlexFlightDatesWarning = new DevExpress.XtraEditors.LabelControl();
			this.pbNoPrograms = new System.Windows.Forms.PictureBox();
			this.xtraTabControlOptions = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageOptionsQuickShare = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageOptionsLine = new DevExpress.XtraTab.XtraTabPage();
			this.pnOptionsLine = new System.Windows.Forms.Panel();
			this.hyperLinkEditLineAdvanced = new DevExpress.XtraEditors.HyperLinkEdit();
			this.buttonXLogo = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTime = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSpots = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCost = new DevComponents.DotNetBar.ButtonX();
			this.buttonXRate = new DevComponents.DotNetBar.ButtonX();
			this.buttonXGRP = new DevComponents.DotNetBar.ButtonX();
			this.buttonXRating = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDay = new DevComponents.DotNetBar.ButtonX();
			this.buttonXDaypart = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCPP = new DevComponents.DotNetBar.ButtonX();
			this.buttonXLength = new DevComponents.DotNetBar.ButtonX();
			this.buttonXStation = new DevComponents.DotNetBar.ButtonX();
			this.xtraTabPageOptionsDigital = new DevExpress.XtraTab.XtraTabPage();
			this.digitalInfoControl = new NewBizWiz.MediaSchedule.Controls.PresentationClasses.Digital.DigitalInfoControl();
			this.xtraTabPageOptionsTotals = new DevExpress.XtraTab.XtraTabPage();
			this.pnOptionsTotals = new System.Windows.Forms.Panel();
			this.buttonXDiscount = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTotalGRP = new DevComponents.DotNetBar.ButtonX();
			this.buttonXNetRate = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTotalCPP = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTotalCost = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTotalSpots = new DevComponents.DotNetBar.ButtonX();
			this.buttonXAvgRate = new DevComponents.DotNetBar.ButtonX();
			this.buttonXTotalPeriods = new DevComponents.DotNetBar.ButtonX();
			this.xtraTabPageOptionsStyle = new DevExpress.XtraTab.XtraTabPage();
			this.pnStyle = new System.Windows.Forms.Panel();
			this.laColorsTitle = new System.Windows.Forms.Label();
			this.pnColors = new System.Windows.Forms.Panel();
			this.xtraScrollableControlColors = new DevExpress.XtraEditors.XtraScrollableControl();
			this.retractableBarControl = new NewBizWiz.CommonGUI.RetractableBar.RetractableBarLeft();
			this.xtraTabControlData = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageDataSchedule = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageDataSpecs = new DevExpress.XtraTab.XtraTabPage();
			((System.ComponentModel.ISupportInitialize)(this.gridControlSchedule)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewSchedule)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStations)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxDayparts)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEditProgram)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.popupContainerControlProgramSource)).BeginInit();
			this.popupContainerControlProgramSource.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlProgramSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewProgramSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxDays)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxTimes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxLengths)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditRate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditRating)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditSpot)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit000s)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditProgram)).BeginInit();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.pnBottom.SuspendLayout();
			this.pnAgencyDiscount.SuspendLayout();
			this.pnNetRate.SuspendLayout();
			this.pnTotalCost.SuspendLayout();
			this.pnAvgRate.SuspendLayout();
			this.pnTotalCPP.SuspendLayout();
			this.pnTotalGRP.SuspendLayout();
			this.pnTotalSpots.SuspendLayout();
			this.pnTotalPeriods.SuspendLayout();
			this.pnPageSchedule.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbNoPrograms)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlOptions)).BeginInit();
			this.xtraTabControlOptions.SuspendLayout();
			this.xtraTabPageOptionsLine.SuspendLayout();
			this.pnOptionsLine.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditLineAdvanced.Properties)).BeginInit();
			this.xtraTabPageOptionsDigital.SuspendLayout();
			this.xtraTabPageOptionsTotals.SuspendLayout();
			this.pnOptionsTotals.SuspendLayout();
			this.xtraTabPageOptionsStyle.SuspendLayout();
			this.pnStyle.SuspendLayout();
			this.pnColors.SuspendLayout();
			this.retractableBarControl.Content.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlData)).BeginInit();
			this.xtraTabControlData.SuspendLayout();
			this.xtraTabPageDataSchedule.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridControlSchedule
			// 
			this.gridControlSchedule.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlSchedule.Location = new System.Drawing.Point(0, 67);
			this.gridControlSchedule.MainView = this.advBandedGridViewSchedule;
			this.gridControlSchedule.Name = "gridControlSchedule";
			this.gridControlSchedule.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBoxStations,
            this.repositoryItemComboBoxDays,
            this.repositoryItemComboBoxTimes,
            this.repositoryItemComboBoxLengths,
            this.repositoryItemSpinEditRate,
            this.repositoryItemSpinEditRating,
            this.repositoryItemSpinEditSpot,
            this.repositoryItemSpinEdit000s,
            this.repositoryItemComboBoxDayparts,
            this.repositoryItemPopupContainerEditProgram,
            this.repositoryItemTextEditProgram,
            this.repositoryItemPictureEdit});
			this.gridControlSchedule.Size = new System.Drawing.Size(826, 477);
			this.gridControlSchedule.TabIndex = 0;
			this.gridControlSchedule.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridViewSchedule});
			// 
			// advBandedGridViewSchedule
			// 
			this.advBandedGridViewSchedule.Appearance.BandPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.advBandedGridViewSchedule.Appearance.BandPanel.Options.UseFont = true;
			this.advBandedGridViewSchedule.Appearance.BandPanel.Options.UseTextOptions = true;
			this.advBandedGridViewSchedule.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridViewSchedule.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewSchedule.Appearance.EvenRow.Options.UseFont = true;
			this.advBandedGridViewSchedule.Appearance.EvenRow.Options.UseTextOptions = true;
			this.advBandedGridViewSchedule.Appearance.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.advBandedGridViewSchedule.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewSchedule.Appearance.FocusedCell.Options.UseFont = true;
			this.advBandedGridViewSchedule.Appearance.FocusedCell.Options.UseTextOptions = true;
			this.advBandedGridViewSchedule.Appearance.FocusedCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridViewSchedule.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewSchedule.Appearance.FocusedRow.Options.UseFont = true;
			this.advBandedGridViewSchedule.Appearance.FocusedRow.Options.UseTextOptions = true;
			this.advBandedGridViewSchedule.Appearance.FocusedRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridViewSchedule.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewSchedule.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
			this.advBandedGridViewSchedule.Appearance.FooterPanel.Options.UseFont = true;
			this.advBandedGridViewSchedule.Appearance.FooterPanel.Options.UseForeColor = true;
			this.advBandedGridViewSchedule.Appearance.FooterPanel.Options.UseTextOptions = true;
			this.advBandedGridViewSchedule.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridViewSchedule.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewSchedule.Appearance.GroupFooter.Options.UseFont = true;
			this.advBandedGridViewSchedule.Appearance.GroupFooter.Options.UseTextOptions = true;
			this.advBandedGridViewSchedule.Appearance.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridViewSchedule.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.advBandedGridViewSchedule.Appearance.HeaderPanel.Options.UseFont = true;
			this.advBandedGridViewSchedule.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.advBandedGridViewSchedule.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridViewSchedule.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewSchedule.Appearance.OddRow.Options.UseFont = true;
			this.advBandedGridViewSchedule.Appearance.OddRow.Options.UseTextOptions = true;
			this.advBandedGridViewSchedule.Appearance.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.advBandedGridViewSchedule.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewSchedule.Appearance.SelectedRow.Options.UseFont = true;
			this.advBandedGridViewSchedule.Appearance.SelectedRow.Options.UseTextOptions = true;
			this.advBandedGridViewSchedule.Appearance.SelectedRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridViewSchedule.AppearancePrint.BandPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.advBandedGridViewSchedule.AppearancePrint.BandPanel.Options.UseFont = true;
			this.advBandedGridViewSchedule.AppearancePrint.BandPanel.Options.UseTextOptions = true;
			this.advBandedGridViewSchedule.AppearancePrint.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridViewSchedule.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewSchedule.AppearancePrint.EvenRow.Options.UseFont = true;
			this.advBandedGridViewSchedule.AppearancePrint.EvenRow.Options.UseTextOptions = true;
			this.advBandedGridViewSchedule.AppearancePrint.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.advBandedGridViewSchedule.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewSchedule.AppearancePrint.FooterPanel.Options.UseFont = true;
			this.advBandedGridViewSchedule.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.advBandedGridViewSchedule.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.advBandedGridViewSchedule.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
			this.advBandedGridViewSchedule.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridViewSchedule.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewSchedule.AppearancePrint.OddRow.Options.UseFont = true;
			this.advBandedGridViewSchedule.AppearancePrint.OddRow.Options.UseTextOptions = true;
			this.advBandedGridViewSchedule.AppearancePrint.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.advBandedGridViewSchedule.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridViewSchedule.AppearancePrint.Row.Options.UseFont = true;
			this.advBandedGridViewSchedule.AppearancePrint.Row.Options.UseTextOptions = true;
			this.advBandedGridViewSchedule.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.advBandedGridViewSchedule.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandId,
            this.gridBandLogo,
            this.gridBandStation,
            this.gridBandProgram,
            this.gridBandRate,
            this.gridBandSpots,
            this.gridBandTotals});
			this.advBandedGridViewSchedule.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumnIndex,
            this.bandedGridColumnLogoImage,
            this.bandedGridColumnLogoSource,
            this.bandedGridColumnStation,
            this.bandedGridColumnName,
            this.bandedGridColumnDay,
            this.bandedGridColumnTime,
            this.bandedGridColumnLength,
            this.bandedGridColumnRate,
            this.bandedGridColumnRating,
            this.bandedGridColumnCPP,
            this.bandedGridColumnGRP,
            this.bandedGridColumnTotalSpots,
            this.bandedGridColumnCost,
            this.bandedGridColumnDaypart});
			this.advBandedGridViewSchedule.FooterPanelHeight = 0;
			this.advBandedGridViewSchedule.GridControl = this.gridControlSchedule;
			this.advBandedGridViewSchedule.Name = "advBandedGridViewSchedule";
			this.advBandedGridViewSchedule.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.advBandedGridViewSchedule.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.advBandedGridViewSchedule.OptionsCustomization.AllowBandMoving = false;
			this.advBandedGridViewSchedule.OptionsCustomization.AllowBandResizing = false;
			this.advBandedGridViewSchedule.OptionsCustomization.AllowColumnMoving = false;
			this.advBandedGridViewSchedule.OptionsCustomization.AllowColumnResizing = false;
			this.advBandedGridViewSchedule.OptionsCustomization.AllowFilter = false;
			this.advBandedGridViewSchedule.OptionsCustomization.AllowGroup = false;
			this.advBandedGridViewSchedule.OptionsCustomization.AllowQuickHideColumns = false;
			this.advBandedGridViewSchedule.OptionsCustomization.AllowSort = false;
			this.advBandedGridViewSchedule.OptionsCustomization.ShowBandsInCustomizationForm = false;
			this.advBandedGridViewSchedule.OptionsFilter.AllowFilterEditor = false;
			this.advBandedGridViewSchedule.OptionsFind.AllowFindPanel = false;
			this.advBandedGridViewSchedule.OptionsMenu.EnableColumnMenu = false;
			this.advBandedGridViewSchedule.OptionsMenu.EnableFooterMenu = false;
			this.advBandedGridViewSchedule.OptionsMenu.EnableGroupPanelMenu = false;
			this.advBandedGridViewSchedule.OptionsMenu.ShowAutoFilterRowItem = false;
			this.advBandedGridViewSchedule.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.advBandedGridViewSchedule.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.advBandedGridViewSchedule.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.advBandedGridViewSchedule.OptionsSelection.EnableAppearanceHideSelection = false;
			this.advBandedGridViewSchedule.OptionsView.EnableAppearanceEvenRow = true;
			this.advBandedGridViewSchedule.OptionsView.EnableAppearanceOddRow = true;
			this.advBandedGridViewSchedule.OptionsView.ShowBands = false;
			this.advBandedGridViewSchedule.OptionsView.ShowDetailButtons = false;
			this.advBandedGridViewSchedule.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			this.advBandedGridViewSchedule.OptionsView.ShowFooter = true;
			this.advBandedGridViewSchedule.OptionsView.ShowGroupPanel = false;
			this.advBandedGridViewSchedule.OptionsView.ShowIndicator = false;
			this.advBandedGridViewSchedule.RowHeight = 30;
			this.advBandedGridViewSchedule.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.advBandedGridViewSchedule_RowCellClick);
			this.advBandedGridViewSchedule.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.advBandedGridViewSchedule_CustomDrawColumnHeader);
			this.advBandedGridViewSchedule.CustomDrawFooter += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.advBandedGridViewSchedule_CustomDrawFooter);
			this.advBandedGridViewSchedule.CustomRowCellEditForEditing += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.advBandedGridViewSchedule_CustomRowCellEditForEditing);
			this.advBandedGridViewSchedule.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.advBandedGridViewSchedule_PopupMenuShowing);
			this.advBandedGridViewSchedule.ShownEditor += new System.EventHandler(this.advBandedGridViewSchedule_ShownEditor);
			this.advBandedGridViewSchedule.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.advBandedGridViewSchedule_CellValueChanged);
			this.advBandedGridViewSchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.advBandedGridViewSchedule_MouseDown);
			// 
			// gridBandId
			// 
			this.gridBandId.Caption = "ID";
			this.gridBandId.Columns.Add(this.bandedGridColumnIndex);
			this.gridBandId.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.gridBandId.Name = "gridBandId";
			this.gridBandId.OptionsBand.FixedWidth = true;
			this.gridBandId.VisibleIndex = 0;
			this.gridBandId.Width = 35;
			// 
			// bandedGridColumnIndex
			// 
			this.bandedGridColumnIndex.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnIndex.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnIndex.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnIndex.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnIndex.AutoFillDown = true;
			this.bandedGridColumnIndex.Caption = "ID";
			this.bandedGridColumnIndex.FieldName = "Index";
			this.bandedGridColumnIndex.Name = "bandedGridColumnIndex";
			this.bandedGridColumnIndex.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnIndex.OptionsColumn.AllowSize = false;
			this.bandedGridColumnIndex.OptionsColumn.FixedWidth = true;
			this.bandedGridColumnIndex.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnIndex.RowCount = 2;
			this.bandedGridColumnIndex.Visible = true;
			this.bandedGridColumnIndex.Width = 35;
			// 
			// gridBandLogo
			// 
			this.gridBandLogo.Caption = "Logo";
			this.gridBandLogo.Columns.Add(this.bandedGridColumnLogoImage);
			this.gridBandLogo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.gridBandLogo.Name = "gridBandLogo";
			this.gridBandLogo.OptionsBand.FixedWidth = true;
			this.gridBandLogo.VisibleIndex = 1;
			this.gridBandLogo.Width = 120;
			// 
			// bandedGridColumnLogoImage
			// 
			this.bandedGridColumnLogoImage.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnLogoImage.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnLogoImage.AutoFillDown = true;
			this.bandedGridColumnLogoImage.Caption = "Logo";
			this.bandedGridColumnLogoImage.ColumnEdit = this.repositoryItemPictureEdit;
			this.bandedGridColumnLogoImage.FieldName = "LogoImage";
			this.bandedGridColumnLogoImage.Name = "bandedGridColumnLogoImage";
			this.bandedGridColumnLogoImage.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnLogoImage.OptionsColumn.AllowSize = false;
			this.bandedGridColumnLogoImage.OptionsColumn.FixedWidth = true;
			this.bandedGridColumnLogoImage.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnLogoImage.Visible = true;
			this.bandedGridColumnLogoImage.Width = 120;
			// 
			// repositoryItemPictureEdit
			// 
			this.repositoryItemPictureEdit.AllowFocused = false;
			this.repositoryItemPictureEdit.Appearance.Options.UseTextOptions = true;
			this.repositoryItemPictureEdit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPictureEdit.AppearanceDisabled.Options.UseTextOptions = true;
			this.repositoryItemPictureEdit.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPictureEdit.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemPictureEdit.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPictureEdit.AppearanceReadOnly.Options.UseTextOptions = true;
			this.repositoryItemPictureEdit.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPictureEdit.Name = "repositoryItemPictureEdit";
			this.repositoryItemPictureEdit.NullText = "No Logo";
			this.repositoryItemPictureEdit.ReadOnly = true;
			this.repositoryItemPictureEdit.ShowMenu = false;
			this.repositoryItemPictureEdit.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.False;
			this.repositoryItemPictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
			// 
			// gridBandStation
			// 
			this.gridBandStation.Caption = "Station";
			this.gridBandStation.Columns.Add(this.bandedGridColumnStation);
			this.gridBandStation.Columns.Add(this.bandedGridColumnDaypart);
			this.gridBandStation.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.gridBandStation.MinWidth = 20;
			this.gridBandStation.Name = "gridBandStation";
			this.gridBandStation.OptionsBand.FixedWidth = true;
			this.gridBandStation.VisibleIndex = 2;
			this.gridBandStation.Width = 73;
			// 
			// bandedGridColumnStation
			// 
			this.bandedGridColumnStation.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnStation.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnStation.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnStation.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnStation.AutoFillDown = true;
			this.bandedGridColumnStation.Caption = "Station";
			this.bandedGridColumnStation.ColumnEdit = this.repositoryItemComboBoxStations;
			this.bandedGridColumnStation.FieldName = "Station";
			this.bandedGridColumnStation.MinWidth = 73;
			this.bandedGridColumnStation.Name = "bandedGridColumnStation";
			this.bandedGridColumnStation.Visible = true;
			this.bandedGridColumnStation.Width = 73;
			// 
			// repositoryItemComboBoxStations
			// 
			this.repositoryItemComboBoxStations.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.repositoryItemComboBoxStations.Appearance.Options.UseFont = true;
			this.repositoryItemComboBoxStations.Appearance.Options.UseTextOptions = true;
			this.repositoryItemComboBoxStations.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxStations.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxStations.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemComboBoxStations.AppearanceDisabled.Options.UseTextOptions = true;
			this.repositoryItemComboBoxStations.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxStations.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxStations.AppearanceDropDown.Options.UseFont = true;
			this.repositoryItemComboBoxStations.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxStations.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemComboBoxStations.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemComboBoxStations.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxStations.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxStations.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemComboBoxStations.AppearanceReadOnly.Options.UseTextOptions = true;
			this.repositoryItemComboBoxStations.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxStations.AutoHeight = false;
			this.repositoryItemComboBoxStations.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemComboBoxStations.Name = "repositoryItemComboBoxStations";
			this.repositoryItemComboBoxStations.NullText = "Select";
			this.repositoryItemComboBoxStations.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			// 
			// bandedGridColumnDaypart
			// 
			this.bandedGridColumnDaypart.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnDaypart.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnDaypart.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnDaypart.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnDaypart.AutoFillDown = true;
			this.bandedGridColumnDaypart.Caption = "Daypart";
			this.bandedGridColumnDaypart.ColumnEdit = this.repositoryItemComboBoxDayparts;
			this.bandedGridColumnDaypart.FieldName = "Daypart";
			this.bandedGridColumnDaypart.MinWidth = 39;
			this.bandedGridColumnDaypart.Name = "bandedGridColumnDaypart";
			this.bandedGridColumnDaypart.RowIndex = 1;
			this.bandedGridColumnDaypart.Visible = true;
			this.bandedGridColumnDaypart.Width = 73;
			// 
			// repositoryItemComboBoxDayparts
			// 
			this.repositoryItemComboBoxDayparts.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxDayparts.Appearance.Options.UseFont = true;
			this.repositoryItemComboBoxDayparts.Appearance.Options.UseTextOptions = true;
			this.repositoryItemComboBoxDayparts.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxDayparts.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxDayparts.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemComboBoxDayparts.AppearanceDisabled.Options.UseTextOptions = true;
			this.repositoryItemComboBoxDayparts.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxDayparts.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxDayparts.AppearanceDropDown.Options.UseFont = true;
			this.repositoryItemComboBoxDayparts.AppearanceDropDown.Options.UseTextOptions = true;
			this.repositoryItemComboBoxDayparts.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxDayparts.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxDayparts.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemComboBoxDayparts.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemComboBoxDayparts.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxDayparts.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxDayparts.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemComboBoxDayparts.AppearanceReadOnly.Options.UseTextOptions = true;
			this.repositoryItemComboBoxDayparts.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxDayparts.AutoHeight = false;
			this.repositoryItemComboBoxDayparts.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemComboBoxDayparts.Name = "repositoryItemComboBoxDayparts";
			this.repositoryItemComboBoxDayparts.NullText = "Select or Type";
			// 
			// gridBandProgram
			// 
			this.gridBandProgram.Caption = "Program";
			this.gridBandProgram.Columns.Add(this.bandedGridColumnName);
			this.gridBandProgram.Columns.Add(this.bandedGridColumnDay);
			this.gridBandProgram.Columns.Add(this.bandedGridColumnTime);
			this.gridBandProgram.Columns.Add(this.bandedGridColumnLength);
			this.gridBandProgram.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.gridBandProgram.MinWidth = 238;
			this.gridBandProgram.Name = "gridBandProgram";
			this.gridBandProgram.VisibleIndex = 3;
			this.gridBandProgram.Width = 240;
			// 
			// bandedGridColumnName
			// 
			this.bandedGridColumnName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnName.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnName.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnName.AutoFillDown = true;
			this.bandedGridColumnName.Caption = "Program";
			this.bandedGridColumnName.ColumnEdit = this.repositoryItemPopupContainerEditProgram;
			this.bandedGridColumnName.FieldName = "Name";
			this.bandedGridColumnName.MinWidth = 238;
			this.bandedGridColumnName.Name = "bandedGridColumnName";
			this.bandedGridColumnName.Visible = true;
			this.bandedGridColumnName.Width = 240;
			// 
			// repositoryItemPopupContainerEditProgram
			// 
			this.repositoryItemPopupContainerEditProgram.Appearance.Options.UseTextOptions = true;
			this.repositoryItemPopupContainerEditProgram.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPopupContainerEditProgram.AppearanceDisabled.Options.UseTextOptions = true;
			this.repositoryItemPopupContainerEditProgram.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPopupContainerEditProgram.AppearanceDropDown.Options.UseTextOptions = true;
			this.repositoryItemPopupContainerEditProgram.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPopupContainerEditProgram.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemPopupContainerEditProgram.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPopupContainerEditProgram.AppearanceReadOnly.Options.UseTextOptions = true;
			this.repositoryItemPopupContainerEditProgram.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemPopupContainerEditProgram.AutoHeight = false;
			this.repositoryItemPopupContainerEditProgram.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemPopupContainerEditProgram.Name = "repositoryItemPopupContainerEditProgram";
			this.repositoryItemPopupContainerEditProgram.PopupControl = this.popupContainerControlProgramSource;
			this.repositoryItemPopupContainerEditProgram.PopupFormSize = new System.Drawing.Size(550, 200);
			this.repositoryItemPopupContainerEditProgram.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
			this.repositoryItemPopupContainerEditProgram.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(this.repositoryItemPopupContainerEditProgram_CloseUp);
			this.repositoryItemPopupContainerEditProgram.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.repositoryItemPopupContainerEditProgram_Closed);
			// 
			// popupContainerControlProgramSource
			// 
			this.popupContainerControlProgramSource.Controls.Add(this.gridControlProgramSource);
			this.popupContainerControlProgramSource.Controls.Add(this.laProgramSourceInfo);
			this.popupContainerControlProgramSource.Location = new System.Drawing.Point(460, 220);
			this.popupContainerControlProgramSource.Name = "popupContainerControlProgramSource";
			this.popupContainerControlProgramSource.Size = new System.Drawing.Size(474, 199);
			this.popupContainerControlProgramSource.TabIndex = 3;
			// 
			// gridControlProgramSource
			// 
			this.gridControlProgramSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlProgramSource.Location = new System.Drawing.Point(0, 0);
			this.gridControlProgramSource.MainView = this.gridViewProgramSource;
			this.gridControlProgramSource.Name = "gridControlProgramSource";
			this.gridControlProgramSource.Size = new System.Drawing.Size(474, 168);
			this.gridControlProgramSource.TabIndex = 0;
			this.gridControlProgramSource.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewProgramSource});
			// 
			// gridViewProgramSource
			// 
			this.gridViewProgramSource.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewProgramSource.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewProgramSource.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewProgramSource.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewProgramSource.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewProgramSource.Appearance.Row.Options.UseFont = true;
			this.gridViewProgramSource.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewProgramSource.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewProgramSource.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnProgramSourceId,
            this.gridColumnProgramSourceName,
            this.gridColumnProgramSourceStation,
            this.gridColumnProgramSourceDaypart,
            this.gridColumnProgramSourceDay,
            this.gridColumnProgramSourceTime});
			this.gridViewProgramSource.GridControl = this.gridControlProgramSource;
			this.gridViewProgramSource.Name = "gridViewProgramSource";
			this.gridViewProgramSource.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewProgramSource.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
			this.gridViewProgramSource.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewProgramSource.OptionsBehavior.AutoSelectAllInEditor = false;
			this.gridViewProgramSource.OptionsBehavior.AutoUpdateTotalSummary = false;
			this.gridViewProgramSource.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
			this.gridViewProgramSource.OptionsBehavior.Editable = false;
			this.gridViewProgramSource.OptionsBehavior.ReadOnly = true;
			this.gridViewProgramSource.OptionsCustomization.AllowFilter = false;
			this.gridViewProgramSource.OptionsCustomization.AllowGroup = false;
			this.gridViewProgramSource.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewProgramSource.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewProgramSource.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewProgramSource.OptionsView.ShowDetailButtons = false;
			this.gridViewProgramSource.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewProgramSource.OptionsView.ShowGroupPanel = false;
			this.gridViewProgramSource.OptionsView.ShowIndicator = false;
			// 
			// gridColumnProgramSourceId
			// 
			this.gridColumnProgramSourceId.Caption = "Id";
			this.gridColumnProgramSourceId.FieldName = "Id";
			this.gridColumnProgramSourceId.Name = "gridColumnProgramSourceId";
			// 
			// gridColumnProgramSourceName
			// 
			this.gridColumnProgramSourceName.Caption = "Program";
			this.gridColumnProgramSourceName.FieldName = "Name";
			this.gridColumnProgramSourceName.Name = "gridColumnProgramSourceName";
			this.gridColumnProgramSourceName.Visible = true;
			this.gridColumnProgramSourceName.VisibleIndex = 0;
			this.gridColumnProgramSourceName.Width = 178;
			// 
			// gridColumnProgramSourceStation
			// 
			this.gridColumnProgramSourceStation.Caption = "Station";
			this.gridColumnProgramSourceStation.FieldName = "Station";
			this.gridColumnProgramSourceStation.Name = "gridColumnProgramSourceStation";
			this.gridColumnProgramSourceStation.OptionsColumn.FixedWidth = true;
			this.gridColumnProgramSourceStation.Visible = true;
			this.gridColumnProgramSourceStation.VisibleIndex = 1;
			this.gridColumnProgramSourceStation.Width = 100;
			// 
			// gridColumnProgramSourceDaypart
			// 
			this.gridColumnProgramSourceDaypart.Caption = "DP";
			this.gridColumnProgramSourceDaypart.FieldName = "Daypart";
			this.gridColumnProgramSourceDaypart.Name = "gridColumnProgramSourceDaypart";
			this.gridColumnProgramSourceDaypart.OptionsColumn.FixedWidth = true;
			this.gridColumnProgramSourceDaypart.Visible = true;
			this.gridColumnProgramSourceDaypart.VisibleIndex = 2;
			this.gridColumnProgramSourceDaypart.Width = 50;
			// 
			// gridColumnProgramSourceDay
			// 
			this.gridColumnProgramSourceDay.Caption = "Day";
			this.gridColumnProgramSourceDay.FieldName = "Day";
			this.gridColumnProgramSourceDay.Name = "gridColumnProgramSourceDay";
			this.gridColumnProgramSourceDay.OptionsColumn.FixedWidth = true;
			this.gridColumnProgramSourceDay.Visible = true;
			this.gridColumnProgramSourceDay.VisibleIndex = 3;
			this.gridColumnProgramSourceDay.Width = 50;
			// 
			// gridColumnProgramSourceTime
			// 
			this.gridColumnProgramSourceTime.Caption = "Time";
			this.gridColumnProgramSourceTime.FieldName = "Time";
			this.gridColumnProgramSourceTime.Name = "gridColumnProgramSourceTime";
			this.gridColumnProgramSourceTime.OptionsColumn.FixedWidth = true;
			this.gridColumnProgramSourceTime.Visible = true;
			this.gridColumnProgramSourceTime.VisibleIndex = 4;
			this.gridColumnProgramSourceTime.Width = 100;
			// 
			// laProgramSourceInfo
			// 
			this.laProgramSourceInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.laProgramSourceInfo.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laProgramSourceInfo.ForeColor = System.Drawing.Color.DarkRed;
			this.laProgramSourceInfo.Location = new System.Drawing.Point(0, 168);
			this.laProgramSourceInfo.Name = "laProgramSourceInfo";
			this.laProgramSourceInfo.Size = new System.Drawing.Size(474, 31);
			this.laProgramSourceInfo.TabIndex = 2;
			this.laProgramSourceInfo.Text = "Double-Click to Select a Program";
			this.laProgramSourceInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// bandedGridColumnDay
			// 
			this.bandedGridColumnDay.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnDay.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnDay.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnDay.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnDay.AutoFillDown = true;
			this.bandedGridColumnDay.Caption = "Days";
			this.bandedGridColumnDay.ColumnEdit = this.repositoryItemComboBoxDays;
			this.bandedGridColumnDay.FieldName = "Day";
			this.bandedGridColumnDay.Name = "bandedGridColumnDay";
			this.bandedGridColumnDay.OptionsColumn.FixedWidth = true;
			this.bandedGridColumnDay.RowIndex = 1;
			this.bandedGridColumnDay.Visible = true;
			this.bandedGridColumnDay.Width = 90;
			// 
			// repositoryItemComboBoxDays
			// 
			this.repositoryItemComboBoxDays.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxDays.Appearance.Options.UseFont = true;
			this.repositoryItemComboBoxDays.Appearance.Options.UseTextOptions = true;
			this.repositoryItemComboBoxDays.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxDays.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxDays.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemComboBoxDays.AppearanceDisabled.Options.UseTextOptions = true;
			this.repositoryItemComboBoxDays.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxDays.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxDays.AppearanceDropDown.Options.UseFont = true;
			this.repositoryItemComboBoxDays.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxDays.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemComboBoxDays.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemComboBoxDays.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxDays.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxDays.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemComboBoxDays.AppearanceReadOnly.Options.UseTextOptions = true;
			this.repositoryItemComboBoxDays.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxDays.AutoHeight = false;
			this.repositoryItemComboBoxDays.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemComboBoxDays.Name = "repositoryItemComboBoxDays";
			this.repositoryItemComboBoxDays.NullText = "Select or Type";
			// 
			// bandedGridColumnTime
			// 
			this.bandedGridColumnTime.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnTime.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnTime.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnTime.AutoFillDown = true;
			this.bandedGridColumnTime.Caption = "Time";
			this.bandedGridColumnTime.ColumnEdit = this.repositoryItemComboBoxTimes;
			this.bandedGridColumnTime.FieldName = "Time";
			this.bandedGridColumnTime.Name = "bandedGridColumnTime";
			this.bandedGridColumnTime.OptionsColumn.FixedWidth = true;
			this.bandedGridColumnTime.RowIndex = 1;
			this.bandedGridColumnTime.Visible = true;
			this.bandedGridColumnTime.Width = 90;
			// 
			// repositoryItemComboBoxTimes
			// 
			this.repositoryItemComboBoxTimes.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxTimes.Appearance.Options.UseFont = true;
			this.repositoryItemComboBoxTimes.Appearance.Options.UseTextOptions = true;
			this.repositoryItemComboBoxTimes.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxTimes.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxTimes.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemComboBoxTimes.AppearanceDisabled.Options.UseTextOptions = true;
			this.repositoryItemComboBoxTimes.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxTimes.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxTimes.AppearanceDropDown.Options.UseFont = true;
			this.repositoryItemComboBoxTimes.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxTimes.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemComboBoxTimes.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemComboBoxTimes.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxTimes.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxTimes.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemComboBoxTimes.AppearanceReadOnly.Options.UseTextOptions = true;
			this.repositoryItemComboBoxTimes.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxTimes.AutoHeight = false;
			this.repositoryItemComboBoxTimes.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemComboBoxTimes.Name = "repositoryItemComboBoxTimes";
			this.repositoryItemComboBoxTimes.NullText = "Select or Type";
			// 
			// bandedGridColumnLength
			// 
			this.bandedGridColumnLength.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnLength.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnLength.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnLength.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnLength.AutoFillDown = true;
			this.bandedGridColumnLength.Caption = "Lgth";
			this.bandedGridColumnLength.ColumnEdit = this.repositoryItemComboBoxLengths;
			this.bandedGridColumnLength.FieldName = "Length";
			this.bandedGridColumnLength.Name = "bandedGridColumnLength";
			this.bandedGridColumnLength.OptionsColumn.FixedWidth = true;
			this.bandedGridColumnLength.RowIndex = 1;
			this.bandedGridColumnLength.Visible = true;
			this.bandedGridColumnLength.Width = 60;
			// 
			// repositoryItemComboBoxLengths
			// 
			this.repositoryItemComboBoxLengths.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxLengths.Appearance.Options.UseFont = true;
			this.repositoryItemComboBoxLengths.Appearance.Options.UseTextOptions = true;
			this.repositoryItemComboBoxLengths.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxLengths.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxLengths.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemComboBoxLengths.AppearanceDisabled.Options.UseTextOptions = true;
			this.repositoryItemComboBoxLengths.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxLengths.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxLengths.AppearanceDropDown.Options.UseFont = true;
			this.repositoryItemComboBoxLengths.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxLengths.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemComboBoxLengths.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemComboBoxLengths.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxLengths.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxLengths.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemComboBoxLengths.AppearanceReadOnly.Options.UseTextOptions = true;
			this.repositoryItemComboBoxLengths.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemComboBoxLengths.AutoHeight = false;
			this.repositoryItemComboBoxLengths.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemComboBoxLengths.Name = "repositoryItemComboBoxLengths";
			this.repositoryItemComboBoxLengths.NullText = "Select";
			this.repositoryItemComboBoxLengths.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			// 
			// gridBandRate
			// 
			this.gridBandRate.Caption = "Rate";
			this.gridBandRate.Columns.Add(this.bandedGridColumnRate);
			this.gridBandRate.Columns.Add(this.bandedGridColumnRating);
			this.gridBandRate.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
			this.gridBandRate.MinWidth = 20;
			this.gridBandRate.Name = "gridBandRate";
			this.gridBandRate.OptionsBand.FixedWidth = true;
			this.gridBandRate.VisibleIndex = 4;
			this.gridBandRate.Width = 100;
			// 
			// bandedGridColumnRate
			// 
			this.bandedGridColumnRate.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnRate.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnRate.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnRate.AutoFillDown = true;
			this.bandedGridColumnRate.Caption = "Rate";
			this.bandedGridColumnRate.ColumnEdit = this.repositoryItemSpinEditRate;
			this.bandedGridColumnRate.FieldName = "Rate";
			this.bandedGridColumnRate.Name = "bandedGridColumnRate";
			this.bandedGridColumnRate.OptionsColumn.FixedWidth = true;
			this.bandedGridColumnRate.Visible = true;
			this.bandedGridColumnRate.Width = 100;
			// 
			// repositoryItemSpinEditRate
			// 
			this.repositoryItemSpinEditRate.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEditRate.Appearance.Options.UseFont = true;
			this.repositoryItemSpinEditRate.Appearance.Options.UseTextOptions = true;
			this.repositoryItemSpinEditRate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemSpinEditRate.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEditRate.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemSpinEditRate.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEditRate.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemSpinEditRate.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemSpinEditRate.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemSpinEditRate.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEditRate.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemSpinEditRate.AutoHeight = false;
			this.repositoryItemSpinEditRate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.repositoryItemSpinEditRate.DisplayFormat.FormatString = "$#,##0";
			this.repositoryItemSpinEditRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditRate.EditFormat.FormatString = "$#,##0";
			this.repositoryItemSpinEditRate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditRate.IsFloatValue = false;
			this.repositoryItemSpinEditRate.Mask.EditMask = "N00";
			this.repositoryItemSpinEditRate.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.repositoryItemSpinEditRate.Name = "repositoryItemSpinEditRate";
			// 
			// bandedGridColumnRating
			// 
			this.bandedGridColumnRating.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnRating.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnRating.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnRating.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnRating.AutoFillDown = true;
			this.bandedGridColumnRating.Caption = "Rtg";
			this.bandedGridColumnRating.ColumnEdit = this.repositoryItemSpinEditRating;
			this.bandedGridColumnRating.FieldName = "Rating";
			this.bandedGridColumnRating.Name = "bandedGridColumnRating";
			this.bandedGridColumnRating.OptionsColumn.FixedWidth = true;
			this.bandedGridColumnRating.RowIndex = 1;
			this.bandedGridColumnRating.Visible = true;
			this.bandedGridColumnRating.Width = 100;
			// 
			// repositoryItemSpinEditRating
			// 
			this.repositoryItemSpinEditRating.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemSpinEditRating.Appearance.Options.UseFont = true;
			this.repositoryItemSpinEditRating.Appearance.Options.UseTextOptions = true;
			this.repositoryItemSpinEditRating.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemSpinEditRating.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEditRating.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemSpinEditRating.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEditRating.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemSpinEditRating.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemSpinEditRating.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemSpinEditRating.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEditRating.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemSpinEditRating.AutoHeight = false;
			this.repositoryItemSpinEditRating.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
			this.repositoryItemSpinEditRating.DisplayFormat.FormatString = "#,##0.0";
			this.repositoryItemSpinEditRating.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditRating.EditFormat.FormatString = "#,##0.0";
			this.repositoryItemSpinEditRating.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditRating.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.repositoryItemSpinEditRating.Name = "repositoryItemSpinEditRating";
			// 
			// gridBandSpots
			// 
			this.gridBandSpots.Caption = "Spots";
			this.gridBandSpots.MinWidth = 20;
			this.gridBandSpots.Name = "gridBandSpots";
			this.gridBandSpots.VisibleIndex = 5;
			this.gridBandSpots.Width = 793;
			// 
			// gridBandTotals
			// 
			this.gridBandTotals.Caption = "Totals";
			this.gridBandTotals.Columns.Add(this.bandedGridColumnTotalSpots);
			this.gridBandTotals.Columns.Add(this.bandedGridColumnCost);
			this.gridBandTotals.Columns.Add(this.bandedGridColumnGRP);
			this.gridBandTotals.Columns.Add(this.bandedGridColumnCPP);
			this.gridBandTotals.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
			this.gridBandTotals.Name = "gridBandTotals";
			this.gridBandTotals.OptionsBand.FixedWidth = true;
			this.gridBandTotals.VisibleIndex = 6;
			this.gridBandTotals.Width = 140;
			// 
			// bandedGridColumnTotalSpots
			// 
			this.bandedGridColumnTotalSpots.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnTotalSpots.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnTotalSpots.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnTotalSpots.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnTotalSpots.AppearanceHeader.Options.UseTextOptions = true;
			this.bandedGridColumnTotalSpots.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnTotalSpots.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.bandedGridColumnTotalSpots.AutoFillDown = true;
			this.bandedGridColumnTotalSpots.Caption = "Spots";
			this.bandedGridColumnTotalSpots.DisplayFormat.FormatString = "#,##0";
			this.bandedGridColumnTotalSpots.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.bandedGridColumnTotalSpots.FieldName = "TotalSpots";
			this.bandedGridColumnTotalSpots.Name = "bandedGridColumnTotalSpots";
			this.bandedGridColumnTotalSpots.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnTotalSpots.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnTotalSpots.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalSpots", "{0:n0}")});
			this.bandedGridColumnTotalSpots.Visible = true;
			this.bandedGridColumnTotalSpots.Width = 69;
			// 
			// bandedGridColumnCost
			// 
			this.bandedGridColumnCost.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnCost.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnCost.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnCost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnCost.AppearanceHeader.Options.UseTextOptions = true;
			this.bandedGridColumnCost.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnCost.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.bandedGridColumnCost.AutoFillDown = true;
			this.bandedGridColumnCost.Caption = "Cost";
			this.bandedGridColumnCost.DisplayFormat.FormatString = "$#,##0";
			this.bandedGridColumnCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.bandedGridColumnCost.FieldName = "TotalCost";
			this.bandedGridColumnCost.Name = "bandedGridColumnCost";
			this.bandedGridColumnCost.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnCost.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnCost.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalCost", "{0:c0}")});
			this.bandedGridColumnCost.Visible = true;
			this.bandedGridColumnCost.Width = 71;
			// 
			// bandedGridColumnGRP
			// 
			this.bandedGridColumnGRP.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnGRP.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnGRP.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnGRP.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnGRP.AutoFillDown = true;
			this.bandedGridColumnGRP.Caption = "GRPs";
			this.bandedGridColumnGRP.ColumnEdit = this.repositoryItemSpinEditRating;
			this.bandedGridColumnGRP.FieldName = "GRP";
			this.bandedGridColumnGRP.Name = "bandedGridColumnGRP";
			this.bandedGridColumnGRP.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnGRP.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnGRP.RowIndex = 1;
			this.bandedGridColumnGRP.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
			this.bandedGridColumnGRP.Visible = true;
			this.bandedGridColumnGRP.Width = 69;
			// 
			// bandedGridColumnCPP
			// 
			this.bandedGridColumnCPP.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.bandedGridColumnCPP.AppearanceCell.Options.UseFont = true;
			this.bandedGridColumnCPP.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnCPP.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnCPP.AutoFillDown = true;
			this.bandedGridColumnCPP.Caption = "CPP";
			this.bandedGridColumnCPP.DisplayFormat.FormatString = "$#,###.00";
			this.bandedGridColumnCPP.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.bandedGridColumnCPP.FieldName = "CPP";
			this.bandedGridColumnCPP.Name = "bandedGridColumnCPP";
			this.bandedGridColumnCPP.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnCPP.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnCPP.RowIndex = 1;
			this.bandedGridColumnCPP.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, "CPP", "{0:c2}", "")});
			this.bandedGridColumnCPP.Visible = true;
			this.bandedGridColumnCPP.Width = 71;
			// 
			// bandedGridColumnLogoSource
			// 
			this.bandedGridColumnLogoSource.Caption = "Logo Source";
			this.bandedGridColumnLogoSource.FieldName = "LogoSource";
			this.bandedGridColumnLogoSource.Name = "bandedGridColumnLogoSource";
			// 
			// repositoryItemSpinEditSpot
			// 
			this.repositoryItemSpinEditSpot.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
			this.repositoryItemSpinEditSpot.Appearance.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemSpinEditSpot.Appearance.Options.UseFont = true;
			this.repositoryItemSpinEditSpot.Appearance.Options.UseTextOptions = true;
			this.repositoryItemSpinEditSpot.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemSpinEditSpot.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemSpinEditSpot.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemSpinEditSpot.AppearanceDisabled.Options.UseTextOptions = true;
			this.repositoryItemSpinEditSpot.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemSpinEditSpot.AppearanceFocused.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemSpinEditSpot.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemSpinEditSpot.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemSpinEditSpot.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemSpinEditSpot.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.repositoryItemSpinEditSpot.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemSpinEditSpot.AppearanceReadOnly.Options.UseTextOptions = true;
			this.repositoryItemSpinEditSpot.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemSpinEditSpot.AutoHeight = false;
			this.repositoryItemSpinEditSpot.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true)});
			this.repositoryItemSpinEditSpot.DisplayFormat.FormatString = "#,##0";
			this.repositoryItemSpinEditSpot.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditSpot.EditFormat.FormatString = "#,##0";
			this.repositoryItemSpinEditSpot.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditSpot.IsFloatValue = false;
			this.repositoryItemSpinEditSpot.Mask.EditMask = "N00";
			this.repositoryItemSpinEditSpot.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.repositoryItemSpinEditSpot.Name = "repositoryItemSpinEditSpot";
			this.repositoryItemSpinEditSpot.NullText = "-";
			// 
			// repositoryItemSpinEdit000s
			// 
			this.repositoryItemSpinEdit000s.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEdit000s.Appearance.Options.UseFont = true;
			this.repositoryItemSpinEdit000s.Appearance.Options.UseTextOptions = true;
			this.repositoryItemSpinEdit000s.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemSpinEdit000s.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEdit000s.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemSpinEdit000s.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEdit000s.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemSpinEdit000s.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemSpinEdit000s.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemSpinEdit000s.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemSpinEdit000s.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemSpinEdit000s.AutoHeight = false;
			this.repositoryItemSpinEdit000s.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, "", null, null, true)});
			this.repositoryItemSpinEdit000s.DisplayFormat.FormatString = "#,##0";
			this.repositoryItemSpinEdit000s.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEdit000s.EditFormat.FormatString = "#,##0";
			this.repositoryItemSpinEdit000s.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEdit000s.IsFloatValue = false;
			this.repositoryItemSpinEdit000s.Mask.EditMask = "N00";
			this.repositoryItemSpinEdit000s.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.repositoryItemSpinEdit000s.Name = "repositoryItemSpinEdit000s";
			// 
			// repositoryItemTextEditProgram
			// 
			this.repositoryItemTextEditProgram.Appearance.Options.UseTextOptions = true;
			this.repositoryItemTextEditProgram.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemTextEditProgram.AppearanceDisabled.Options.UseTextOptions = true;
			this.repositoryItemTextEditProgram.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemTextEditProgram.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemTextEditProgram.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemTextEditProgram.AppearanceReadOnly.Options.UseTextOptions = true;
			this.repositoryItemTextEditProgram.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemTextEditProgram.AutoHeight = false;
			this.repositoryItemTextEditProgram.Name = "repositoryItemTextEditProgram";
			// 
			// pnTop
			// 
			this.pnTop.Controls.Add(this.quarterSelectorControl);
			this.pnTop.Controls.Add(this.laScheduleInfo);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.Location = new System.Drawing.Point(0, 0);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(826, 40);
			this.pnTop.TabIndex = 1;
			// 
			// quarterSelectorControl
			// 
			this.quarterSelectorControl.BackColor = System.Drawing.Color.Transparent;
			this.quarterSelectorControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.quarterSelectorControl.Enabled = false;
			this.quarterSelectorControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.quarterSelectorControl.Location = new System.Drawing.Point(248, 0);
			this.quarterSelectorControl.Name = "quarterSelectorControl";
			this.quarterSelectorControl.Size = new System.Drawing.Size(578, 40);
			this.quarterSelectorControl.TabIndex = 51;
			// 
			// laScheduleInfo
			// 
			this.laScheduleInfo.Dock = System.Windows.Forms.DockStyle.Left;
			this.laScheduleInfo.Location = new System.Drawing.Point(0, 0);
			this.laScheduleInfo.Name = "laScheduleInfo";
			this.laScheduleInfo.Size = new System.Drawing.Size(248, 40);
			this.laScheduleInfo.TabIndex = 4;
			this.laScheduleInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			// 
			// pnBottom
			// 
			this.pnBottom.Controls.Add(this.pnAgencyDiscount);
			this.pnBottom.Controls.Add(this.pnNetRate);
			this.pnBottom.Controls.Add(this.pnTotalCost);
			this.pnBottom.Controls.Add(this.pnAvgRate);
			this.pnBottom.Controls.Add(this.pnTotalCPP);
			this.pnBottom.Controls.Add(this.pnTotalGRP);
			this.pnBottom.Controls.Add(this.pnTotalSpots);
			this.pnBottom.Controls.Add(this.pnTotalPeriods);
			this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottom.Location = new System.Drawing.Point(0, 550);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Size = new System.Drawing.Size(1132, 43);
			this.pnBottom.TabIndex = 2;
			// 
			// pnAgencyDiscount
			// 
			this.pnAgencyDiscount.Controls.Add(this.laAgencyDiscountValue);
			this.pnAgencyDiscount.Controls.Add(this.laAgencyDiscountTitle);
			this.pnAgencyDiscount.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnAgencyDiscount.Location = new System.Drawing.Point(860, 0);
			this.pnAgencyDiscount.Name = "pnAgencyDiscount";
			this.pnAgencyDiscount.Size = new System.Drawing.Size(145, 43);
			this.pnAgencyDiscount.TabIndex = 6;
			// 
			// laAgencyDiscountValue
			// 
			this.laAgencyDiscountValue.Dock = System.Windows.Forms.DockStyle.Top;
			this.laAgencyDiscountValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAgencyDiscountValue.Location = new System.Drawing.Point(0, 19);
			this.laAgencyDiscountValue.Name = "laAgencyDiscountValue";
			this.laAgencyDiscountValue.Size = new System.Drawing.Size(145, 19);
			this.laAgencyDiscountValue.TabIndex = 2;
			this.laAgencyDiscountValue.Text = "Agency Discount:";
			this.laAgencyDiscountValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laAgencyDiscountTitle
			// 
			this.laAgencyDiscountTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laAgencyDiscountTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAgencyDiscountTitle.Location = new System.Drawing.Point(0, 0);
			this.laAgencyDiscountTitle.Name = "laAgencyDiscountTitle";
			this.laAgencyDiscountTitle.Size = new System.Drawing.Size(145, 19);
			this.laAgencyDiscountTitle.TabIndex = 1;
			this.laAgencyDiscountTitle.Text = "Agency Discount:";
			this.laAgencyDiscountTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnNetRate
			// 
			this.pnNetRate.Controls.Add(this.laNetRateValue);
			this.pnNetRate.Controls.Add(this.laNetRateTitle);
			this.pnNetRate.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnNetRate.Location = new System.Drawing.Point(720, 0);
			this.pnNetRate.Name = "pnNetRate";
			this.pnNetRate.Size = new System.Drawing.Size(140, 43);
			this.pnNetRate.TabIndex = 5;
			// 
			// laNetRateValue
			// 
			this.laNetRateValue.Dock = System.Windows.Forms.DockStyle.Top;
			this.laNetRateValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laNetRateValue.Location = new System.Drawing.Point(0, 19);
			this.laNetRateValue.Name = "laNetRateValue";
			this.laNetRateValue.Size = new System.Drawing.Size(140, 19);
			this.laNetRateValue.TabIndex = 2;
			this.laNetRateValue.Text = "Net Investment:";
			this.laNetRateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laNetRateTitle
			// 
			this.laNetRateTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laNetRateTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laNetRateTitle.Location = new System.Drawing.Point(0, 0);
			this.laNetRateTitle.Name = "laNetRateTitle";
			this.laNetRateTitle.Size = new System.Drawing.Size(140, 19);
			this.laNetRateTitle.TabIndex = 1;
			this.laNetRateTitle.Text = "Net Investment:";
			this.laNetRateTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnTotalCost
			// 
			this.pnTotalCost.Controls.Add(this.laTotalCostValue);
			this.pnTotalCost.Controls.Add(this.laTotalCostTitle);
			this.pnTotalCost.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnTotalCost.Location = new System.Drawing.Point(575, 0);
			this.pnTotalCost.Name = "pnTotalCost";
			this.pnTotalCost.Size = new System.Drawing.Size(145, 43);
			this.pnTotalCost.TabIndex = 4;
			// 
			// laTotalCostValue
			// 
			this.laTotalCostValue.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalCostValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalCostValue.Location = new System.Drawing.Point(0, 19);
			this.laTotalCostValue.Name = "laTotalCostValue";
			this.laTotalCostValue.Size = new System.Drawing.Size(145, 19);
			this.laTotalCostValue.TabIndex = 2;
			this.laTotalCostValue.Text = "Gross Investment:";
			this.laTotalCostValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laTotalCostTitle
			// 
			this.laTotalCostTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalCostTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalCostTitle.Location = new System.Drawing.Point(0, 0);
			this.laTotalCostTitle.Name = "laTotalCostTitle";
			this.laTotalCostTitle.Size = new System.Drawing.Size(145, 19);
			this.laTotalCostTitle.TabIndex = 1;
			this.laTotalCostTitle.Text = "Gross Investment:";
			this.laTotalCostTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnAvgRate
			// 
			this.pnAvgRate.Controls.Add(this.laAvgRateValue);
			this.pnAvgRate.Controls.Add(this.laAvgRateTitle);
			this.pnAvgRate.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnAvgRate.Location = new System.Drawing.Point(460, 0);
			this.pnAvgRate.Name = "pnAvgRate";
			this.pnAvgRate.Size = new System.Drawing.Size(115, 43);
			this.pnAvgRate.TabIndex = 3;
			// 
			// laAvgRateValue
			// 
			this.laAvgRateValue.Dock = System.Windows.Forms.DockStyle.Top;
			this.laAvgRateValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAvgRateValue.Location = new System.Drawing.Point(0, 19);
			this.laAvgRateValue.Name = "laAvgRateValue";
			this.laAvgRateValue.Size = new System.Drawing.Size(115, 19);
			this.laAvgRateValue.TabIndex = 2;
			this.laAvgRateValue.Text = "Average Rate:";
			this.laAvgRateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laAvgRateTitle
			// 
			this.laAvgRateTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laAvgRateTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAvgRateTitle.Location = new System.Drawing.Point(0, 0);
			this.laAvgRateTitle.Name = "laAvgRateTitle";
			this.laAvgRateTitle.Size = new System.Drawing.Size(115, 19);
			this.laAvgRateTitle.TabIndex = 1;
			this.laAvgRateTitle.Text = "Average Rate:";
			this.laAvgRateTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnTotalCPP
			// 
			this.pnTotalCPP.Controls.Add(this.laTotalCPPValue);
			this.pnTotalCPP.Controls.Add(this.laTotalCPPTitle);
			this.pnTotalCPP.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnTotalCPP.Location = new System.Drawing.Point(345, 0);
			this.pnTotalCPP.Name = "pnTotalCPP";
			this.pnTotalCPP.Size = new System.Drawing.Size(115, 43);
			this.pnTotalCPP.TabIndex = 2;
			// 
			// laTotalCPPValue
			// 
			this.laTotalCPPValue.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalCPPValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalCPPValue.Location = new System.Drawing.Point(0, 19);
			this.laTotalCPPValue.Name = "laTotalCPPValue";
			this.laTotalCPPValue.Size = new System.Drawing.Size(115, 19);
			this.laTotalCPPValue.TabIndex = 2;
			this.laTotalCPPValue.Text = "Overall CPP:";
			this.laTotalCPPValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laTotalCPPTitle
			// 
			this.laTotalCPPTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalCPPTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalCPPTitle.Location = new System.Drawing.Point(0, 0);
			this.laTotalCPPTitle.Name = "laTotalCPPTitle";
			this.laTotalCPPTitle.Size = new System.Drawing.Size(115, 19);
			this.laTotalCPPTitle.TabIndex = 1;
			this.laTotalCPPTitle.Text = "Overall CPP:";
			this.laTotalCPPTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnTotalGRP
			// 
			this.pnTotalGRP.Controls.Add(this.laTotalGRPValue);
			this.pnTotalGRP.Controls.Add(this.laTotalGRPTitle);
			this.pnTotalGRP.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnTotalGRP.Location = new System.Drawing.Point(230, 0);
			this.pnTotalGRP.Name = "pnTotalGRP";
			this.pnTotalGRP.Size = new System.Drawing.Size(115, 43);
			this.pnTotalGRP.TabIndex = 1;
			// 
			// laTotalGRPValue
			// 
			this.laTotalGRPValue.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalGRPValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalGRPValue.Location = new System.Drawing.Point(0, 19);
			this.laTotalGRPValue.Name = "laTotalGRPValue";
			this.laTotalGRPValue.Size = new System.Drawing.Size(115, 19);
			this.laTotalGRPValue.TabIndex = 2;
			this.laTotalGRPValue.Text = "Total GRPs:";
			this.laTotalGRPValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laTotalGRPTitle
			// 
			this.laTotalGRPTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalGRPTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalGRPTitle.Location = new System.Drawing.Point(0, 0);
			this.laTotalGRPTitle.Name = "laTotalGRPTitle";
			this.laTotalGRPTitle.Size = new System.Drawing.Size(115, 19);
			this.laTotalGRPTitle.TabIndex = 1;
			this.laTotalGRPTitle.Text = "Total GRPs:";
			this.laTotalGRPTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnTotalSpots
			// 
			this.pnTotalSpots.Controls.Add(this.laTotalSpotsValue);
			this.pnTotalSpots.Controls.Add(this.laTotalSpotsTitle);
			this.pnTotalSpots.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnTotalSpots.Location = new System.Drawing.Point(115, 0);
			this.pnTotalSpots.Name = "pnTotalSpots";
			this.pnTotalSpots.Size = new System.Drawing.Size(115, 43);
			this.pnTotalSpots.TabIndex = 7;
			// 
			// laTotalSpotsValue
			// 
			this.laTotalSpotsValue.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalSpotsValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalSpotsValue.Location = new System.Drawing.Point(0, 19);
			this.laTotalSpotsValue.Name = "laTotalSpotsValue";
			this.laTotalSpotsValue.Size = new System.Drawing.Size(115, 19);
			this.laTotalSpotsValue.TabIndex = 2;
			this.laTotalSpotsValue.Text = "Total Spots:";
			this.laTotalSpotsValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laTotalSpotsTitle
			// 
			this.laTotalSpotsTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalSpotsTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalSpotsTitle.Location = new System.Drawing.Point(0, 0);
			this.laTotalSpotsTitle.Name = "laTotalSpotsTitle";
			this.laTotalSpotsTitle.Size = new System.Drawing.Size(115, 19);
			this.laTotalSpotsTitle.TabIndex = 1;
			this.laTotalSpotsTitle.Text = "Total Spots:";
			this.laTotalSpotsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnTotalPeriods
			// 
			this.pnTotalPeriods.Controls.Add(this.laTotalPeriodsValue);
			this.pnTotalPeriods.Controls.Add(this.laTotalPeriodsTitle);
			this.pnTotalPeriods.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnTotalPeriods.Location = new System.Drawing.Point(0, 0);
			this.pnTotalPeriods.Name = "pnTotalPeriods";
			this.pnTotalPeriods.Size = new System.Drawing.Size(115, 43);
			this.pnTotalPeriods.TabIndex = 0;
			// 
			// laTotalPeriodsValue
			// 
			this.laTotalPeriodsValue.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalPeriodsValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalPeriodsValue.Location = new System.Drawing.Point(0, 19);
			this.laTotalPeriodsValue.Name = "laTotalPeriodsValue";
			this.laTotalPeriodsValue.Size = new System.Drawing.Size(115, 19);
			this.laTotalPeriodsValue.TabIndex = 2;
			this.laTotalPeriodsValue.Text = "Total Weeks:";
			this.laTotalPeriodsValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laTotalPeriodsTitle
			// 
			this.laTotalPeriodsTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalPeriodsTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalPeriodsTitle.Location = new System.Drawing.Point(0, 0);
			this.laTotalPeriodsTitle.Name = "laTotalPeriodsTitle";
			this.laTotalPeriodsTitle.Size = new System.Drawing.Size(115, 19);
			this.laTotalPeriodsTitle.TabIndex = 1;
			this.laTotalPeriodsTitle.Text = "Total Weeks:";
			this.laTotalPeriodsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnPageSchedule
			// 
			this.pnPageSchedule.BackColor = System.Drawing.Color.White;
			this.pnPageSchedule.Controls.Add(this.gridControlSchedule);
			this.pnPageSchedule.Controls.Add(this.labelControlFlexFlightDatesWarning);
			this.pnPageSchedule.Controls.Add(this.pbNoPrograms);
			this.pnPageSchedule.Controls.Add(this.pnTop);
			this.pnPageSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnPageSchedule.Location = new System.Drawing.Point(0, 0);
			this.pnPageSchedule.Name = "pnPageSchedule";
			this.pnPageSchedule.Size = new System.Drawing.Size(826, 544);
			this.pnPageSchedule.TabIndex = 0;
			// 
			// labelControlFlexFlightDatesWarning
			// 
			this.labelControlFlexFlightDatesWarning.AllowHtmlString = true;
			this.labelControlFlexFlightDatesWarning.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControlFlexFlightDatesWarning.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlFlexFlightDatesWarning.Appearance.ForeColor = System.Drawing.Color.Red;
			this.labelControlFlexFlightDatesWarning.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlFlexFlightDatesWarning.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlFlexFlightDatesWarning.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labelControlFlexFlightDatesWarning.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelControlFlexFlightDatesWarning.Location = new System.Drawing.Point(0, 40);
			this.labelControlFlexFlightDatesWarning.LookAndFeel.UseDefaultLookAndFeel = false;
			this.labelControlFlexFlightDatesWarning.Name = "labelControlFlexFlightDatesWarning";
			this.labelControlFlexFlightDatesWarning.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.labelControlFlexFlightDatesWarning.Size = new System.Drawing.Size(826, 27);
			this.labelControlFlexFlightDatesWarning.TabIndex = 4;
			this.labelControlFlexFlightDatesWarning.Text = "*You have PARTIAL WEEKS in your schedule. <u><b><color=red>CLICK HERE</color></b>" +
    "</u>";
			this.labelControlFlexFlightDatesWarning.Click += new System.EventHandler(this.labelControlFlexFlightDatesWarning_Click);
			// 
			// pbNoPrograms
			// 
			this.pbNoPrograms.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbNoPrograms.Image = ((System.Drawing.Image)(resources.GetObject("pbNoPrograms.Image")));
			this.pbNoPrograms.Location = new System.Drawing.Point(0, 40);
			this.pbNoPrograms.Name = "pbNoPrograms";
			this.pbNoPrograms.Size = new System.Drawing.Size(826, 504);
			this.pbNoPrograms.TabIndex = 1;
			this.pbNoPrograms.TabStop = false;
			// 
			// xtraTabControlOptions
			// 
			this.xtraTabControlOptions.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlOptions.Appearance.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlOptions.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlOptions.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlOptions.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlOptions.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.xtraTabControlOptions.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlOptions.AppearancePage.PageClient.Options.UseBackColor = true;
			this.xtraTabControlOptions.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlOptions.Dock = System.Windows.Forms.DockStyle.Left;
			this.xtraTabControlOptions.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControlOptions.Name = "xtraTabControlOptions";
			this.xtraTabControlOptions.SelectedTabPage = this.xtraTabPageOptionsQuickShare;
			this.xtraTabControlOptions.Size = new System.Drawing.Size(298, 506);
			this.xtraTabControlOptions.TabIndex = 0;
			this.xtraTabControlOptions.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageOptionsQuickShare,
            this.xtraTabPageOptionsLine,
            this.xtraTabPageOptionsDigital,
            this.xtraTabPageOptionsTotals,
            this.xtraTabPageOptionsStyle});
			// 
			// xtraTabPageOptionsQuickShare
			// 
			this.xtraTabPageOptionsQuickShare.Name = "xtraTabPageOptionsQuickShare";
			this.xtraTabPageOptionsQuickShare.Size = new System.Drawing.Size(292, 475);
			this.xtraTabPageOptionsQuickShare.Text = "My Share";
			// 
			// xtraTabPageOptionsLine
			// 
			this.xtraTabPageOptionsLine.Appearance.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.xtraTabPageOptionsLine.Appearance.PageClient.Options.UseBackColor = true;
			this.xtraTabPageOptionsLine.Controls.Add(this.pnOptionsLine);
			this.xtraTabPageOptionsLine.Name = "xtraTabPageOptionsLine";
			this.xtraTabPageOptionsLine.Size = new System.Drawing.Size(292, 475);
			this.xtraTabPageOptionsLine.Text = "Radio";
			// 
			// pnOptionsLine
			// 
			this.pnOptionsLine.BackColor = System.Drawing.Color.Transparent;
			this.pnOptionsLine.Controls.Add(this.hyperLinkEditLineAdvanced);
			this.pnOptionsLine.Controls.Add(this.buttonXLogo);
			this.pnOptionsLine.Controls.Add(this.buttonXTime);
			this.pnOptionsLine.Controls.Add(this.buttonXSpots);
			this.pnOptionsLine.Controls.Add(this.buttonXCost);
			this.pnOptionsLine.Controls.Add(this.buttonXRate);
			this.pnOptionsLine.Controls.Add(this.buttonXGRP);
			this.pnOptionsLine.Controls.Add(this.buttonXRating);
			this.pnOptionsLine.Controls.Add(this.buttonXDay);
			this.pnOptionsLine.Controls.Add(this.buttonXDaypart);
			this.pnOptionsLine.Controls.Add(this.buttonXCPP);
			this.pnOptionsLine.Controls.Add(this.buttonXLength);
			this.pnOptionsLine.Controls.Add(this.buttonXStation);
			this.pnOptionsLine.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnOptionsLine.Location = new System.Drawing.Point(0, 0);
			this.pnOptionsLine.Name = "pnOptionsLine";
			this.pnOptionsLine.Size = new System.Drawing.Size(292, 475);
			this.pnOptionsLine.TabIndex = 0;
			// 
			// hyperLinkEditLineAdvanced
			// 
			this.hyperLinkEditLineAdvanced.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.hyperLinkEditLineAdvanced.EditValue = "Advanced Settings";
			this.hyperLinkEditLineAdvanced.Location = new System.Drawing.Point(0, 417);
			this.hyperLinkEditLineAdvanced.Name = "hyperLinkEditLineAdvanced";
			this.hyperLinkEditLineAdvanced.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.hyperLinkEditLineAdvanced.Properties.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.hyperLinkEditLineAdvanced.Properties.Appearance.ForeColor = System.Drawing.Color.DarkBlue;
			this.hyperLinkEditLineAdvanced.Properties.Appearance.Options.UseBackColor = true;
			this.hyperLinkEditLineAdvanced.Properties.Appearance.Options.UseFont = true;
			this.hyperLinkEditLineAdvanced.Properties.Appearance.Options.UseForeColor = true;
			this.hyperLinkEditLineAdvanced.Properties.Appearance.Options.UseTextOptions = true;
			this.hyperLinkEditLineAdvanced.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.hyperLinkEditLineAdvanced.Properties.AutoHeight = false;
			this.hyperLinkEditLineAdvanced.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.hyperLinkEditLineAdvanced.Size = new System.Drawing.Size(292, 58);
			toolTipItem1.Text = "Change Slide Output Settings";
			superToolTip1.Items.Add(toolTipItem1);
			this.hyperLinkEditLineAdvanced.SuperTip = superToolTip1;
			this.hyperLinkEditLineAdvanced.TabIndex = 117;
			this.hyperLinkEditLineAdvanced.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hyperLinkEditLineAdvanced_OpenLink);
			// 
			// buttonXLogo
			// 
			this.buttonXLogo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXLogo.AutoCheckOnClick = true;
			this.buttonXLogo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLogo.Location = new System.Drawing.Point(158, 256);
			this.buttonXLogo.Name = "buttonXLogo";
			this.buttonXLogo.Size = new System.Drawing.Size(113, 27);
			this.buttonXLogo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLogo.TabIndex = 116;
			this.buttonXLogo.Text = "Logo";
			this.buttonXLogo.TextColor = System.Drawing.Color.Black;
			this.buttonXLogo.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXTime
			// 
			this.buttonXTime.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXTime.AutoCheckOnClick = true;
			this.buttonXTime.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTime.Location = new System.Drawing.Point(158, 158);
			this.buttonXTime.Name = "buttonXTime";
			this.buttonXTime.Size = new System.Drawing.Size(113, 27);
			this.buttonXTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTime.TabIndex = 49;
			this.buttonXTime.Text = "Time";
			this.buttonXTime.TextColor = System.Drawing.Color.Black;
			this.buttonXTime.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXSpots
			// 
			this.buttonXSpots.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSpots.AutoCheckOnClick = true;
			this.buttonXSpots.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSpots.Location = new System.Drawing.Point(20, 109);
			this.buttonXSpots.Name = "buttonXSpots";
			this.buttonXSpots.Size = new System.Drawing.Size(113, 27);
			this.buttonXSpots.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSpots.TabIndex = 48;
			this.buttonXSpots.Text = "Weeks";
			this.buttonXSpots.TextColor = System.Drawing.Color.Black;
			this.buttonXSpots.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXCost
			// 
			this.buttonXCost.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCost.AutoCheckOnClick = true;
			this.buttonXCost.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCost.Location = new System.Drawing.Point(158, 207);
			this.buttonXCost.Name = "buttonXCost";
			this.buttonXCost.Size = new System.Drawing.Size(113, 27);
			this.buttonXCost.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCost.TabIndex = 47;
			this.buttonXCost.Text = "Total Cost";
			this.buttonXCost.TextColor = System.Drawing.Color.Black;
			this.buttonXCost.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXRate
			// 
			this.buttonXRate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRate.AutoCheckOnClick = true;
			this.buttonXRate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRate.Location = new System.Drawing.Point(20, 207);
			this.buttonXRate.Name = "buttonXRate";
			this.buttonXRate.Size = new System.Drawing.Size(113, 27);
			this.buttonXRate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRate.TabIndex = 46;
			this.buttonXRate.Text = "Rate";
			this.buttonXRate.TextColor = System.Drawing.Color.Black;
			this.buttonXRate.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXGRP
			// 
			this.buttonXGRP.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXGRP.AutoCheckOnClick = true;
			this.buttonXGRP.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXGRP.Location = new System.Drawing.Point(20, 256);
			this.buttonXGRP.Name = "buttonXGRP";
			this.buttonXGRP.Size = new System.Drawing.Size(113, 27);
			this.buttonXGRP.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXGRP.TabIndex = 45;
			this.buttonXGRP.Text = "GRPs";
			this.buttonXGRP.TextColor = System.Drawing.Color.Black;
			this.buttonXGRP.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXRating
			// 
			this.buttonXRating.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRating.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXRating.AutoCheckOnClick = true;
			this.buttonXRating.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRating.Location = new System.Drawing.Point(158, 109);
			this.buttonXRating.Name = "buttonXRating";
			this.buttonXRating.Size = new System.Drawing.Size(113, 27);
			this.buttonXRating.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRating.TabIndex = 44;
			this.buttonXRating.Text = "Impressions";
			this.buttonXRating.TextColor = System.Drawing.Color.Black;
			this.buttonXRating.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXDay
			// 
			this.buttonXDay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDay.AutoCheckOnClick = true;
			this.buttonXDay.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDay.Location = new System.Drawing.Point(20, 158);
			this.buttonXDay.Name = "buttonXDay";
			this.buttonXDay.Size = new System.Drawing.Size(113, 27);
			this.buttonXDay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDay.TabIndex = 43;
			this.buttonXDay.Text = "Day";
			this.buttonXDay.TextColor = System.Drawing.Color.Black;
			this.buttonXDay.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXDaypart
			// 
			this.buttonXDaypart.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDaypart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXDaypart.AutoCheckOnClick = true;
			this.buttonXDaypart.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDaypart.Location = new System.Drawing.Point(158, 60);
			this.buttonXDaypart.Name = "buttonXDaypart";
			this.buttonXDaypart.Size = new System.Drawing.Size(113, 27);
			this.buttonXDaypart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDaypart.TabIndex = 42;
			this.buttonXDaypart.Text = "Daypart";
			this.buttonXDaypart.TextColor = System.Drawing.Color.Black;
			this.buttonXDaypart.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXCPP
			// 
			this.buttonXCPP.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCPP.AutoCheckOnClick = true;
			this.buttonXCPP.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCPP.Location = new System.Drawing.Point(20, 60);
			this.buttonXCPP.Name = "buttonXCPP";
			this.buttonXCPP.Size = new System.Drawing.Size(113, 27);
			this.buttonXCPP.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCPP.TabIndex = 41;
			this.buttonXCPP.Text = "CPP";
			this.buttonXCPP.TextColor = System.Drawing.Color.Black;
			this.buttonXCPP.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXLength
			// 
			this.buttonXLength.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXLength.AutoCheckOnClick = true;
			this.buttonXLength.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLength.Location = new System.Drawing.Point(158, 11);
			this.buttonXLength.Name = "buttonXLength";
			this.buttonXLength.Size = new System.Drawing.Size(113, 27);
			this.buttonXLength.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLength.TabIndex = 40;
			this.buttonXLength.Text = "Length";
			this.buttonXLength.TextColor = System.Drawing.Color.Black;
			this.buttonXLength.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXStation
			// 
			this.buttonXStation.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXStation.AutoCheckOnClick = true;
			this.buttonXStation.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXStation.Location = new System.Drawing.Point(20, 11);
			this.buttonXStation.Name = "buttonXStation";
			this.buttonXStation.Size = new System.Drawing.Size(113, 27);
			this.buttonXStation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXStation.TabIndex = 39;
			this.buttonXStation.Text = "Station";
			this.buttonXStation.TextColor = System.Drawing.Color.Black;
			this.buttonXStation.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// xtraTabPageOptionsDigital
			// 
			this.xtraTabPageOptionsDigital.Controls.Add(this.digitalInfoControl);
			this.xtraTabPageOptionsDigital.Name = "xtraTabPageOptionsDigital";
			this.xtraTabPageOptionsDigital.Size = new System.Drawing.Size(292, 475);
			this.xtraTabPageOptionsDigital.Text = "Digital";
			// 
			// digitalInfoControl
			// 
			this.digitalInfoControl.BackColor = System.Drawing.Color.White;
			this.digitalInfoControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.digitalInfoControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.digitalInfoControl.Location = new System.Drawing.Point(0, 0);
			this.digitalInfoControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.digitalInfoControl.Name = "digitalInfoControl";
			this.digitalInfoControl.Size = new System.Drawing.Size(292, 475);
			this.digitalInfoControl.TabIndex = 0;
			// 
			// xtraTabPageOptionsTotals
			// 
			this.xtraTabPageOptionsTotals.Controls.Add(this.pnOptionsTotals);
			this.xtraTabPageOptionsTotals.Name = "xtraTabPageOptionsTotals";
			this.xtraTabPageOptionsTotals.Size = new System.Drawing.Size(292, 475);
			this.xtraTabPageOptionsTotals.Text = "Info";
			// 
			// pnOptionsTotals
			// 
			this.pnOptionsTotals.BackColor = System.Drawing.Color.Transparent;
			this.pnOptionsTotals.Controls.Add(this.buttonXDiscount);
			this.pnOptionsTotals.Controls.Add(this.buttonXTotalGRP);
			this.pnOptionsTotals.Controls.Add(this.buttonXNetRate);
			this.pnOptionsTotals.Controls.Add(this.buttonXTotalCPP);
			this.pnOptionsTotals.Controls.Add(this.buttonXTotalCost);
			this.pnOptionsTotals.Controls.Add(this.buttonXTotalSpots);
			this.pnOptionsTotals.Controls.Add(this.buttonXAvgRate);
			this.pnOptionsTotals.Controls.Add(this.buttonXTotalPeriods);
			this.pnOptionsTotals.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnOptionsTotals.Location = new System.Drawing.Point(0, 0);
			this.pnOptionsTotals.Name = "pnOptionsTotals";
			this.pnOptionsTotals.Size = new System.Drawing.Size(292, 475);
			this.pnOptionsTotals.TabIndex = 1;
			// 
			// buttonXDiscount
			// 
			this.buttonXDiscount.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXDiscount.AutoCheckOnClick = true;
			this.buttonXDiscount.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXDiscount.Location = new System.Drawing.Point(160, 158);
			this.buttonXDiscount.Name = "buttonXDiscount";
			this.buttonXDiscount.Size = new System.Drawing.Size(113, 27);
			this.buttonXDiscount.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXDiscount.TabIndex = 63;
			this.buttonXDiscount.Text = "Agency Discount";
			this.buttonXDiscount.TextColor = System.Drawing.Color.Black;
			this.buttonXDiscount.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXTotalGRP
			// 
			this.buttonXTotalGRP.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTotalGRP.AutoCheckOnClick = true;
			this.buttonXTotalGRP.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTotalGRP.Location = new System.Drawing.Point(20, 109);
			this.buttonXTotalGRP.Name = "buttonXTotalGRP";
			this.buttonXTotalGRP.Size = new System.Drawing.Size(113, 27);
			this.buttonXTotalGRP.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTotalGRP.TabIndex = 62;
			this.buttonXTotalGRP.Text = "Total GRPs";
			this.buttonXTotalGRP.TextColor = System.Drawing.Color.Black;
			this.buttonXTotalGRP.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXNetRate
			// 
			this.buttonXNetRate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXNetRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXNetRate.AutoCheckOnClick = true;
			this.buttonXNetRate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXNetRate.Location = new System.Drawing.Point(160, 109);
			this.buttonXNetRate.Name = "buttonXNetRate";
			this.buttonXNetRate.Size = new System.Drawing.Size(113, 27);
			this.buttonXNetRate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXNetRate.TabIndex = 58;
			this.buttonXNetRate.Text = "Net Investment";
			this.buttonXNetRate.TextColor = System.Drawing.Color.Black;
			this.buttonXNetRate.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXTotalCPP
			// 
			this.buttonXTotalCPP.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTotalCPP.AutoCheckOnClick = true;
			this.buttonXTotalCPP.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTotalCPP.Location = new System.Drawing.Point(20, 158);
			this.buttonXTotalCPP.Name = "buttonXTotalCPP";
			this.buttonXTotalCPP.Size = new System.Drawing.Size(113, 27);
			this.buttonXTotalCPP.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTotalCPP.TabIndex = 57;
			this.buttonXTotalCPP.Text = "Overall CPP";
			this.buttonXTotalCPP.TextColor = System.Drawing.Color.Black;
			this.buttonXTotalCPP.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXTotalCost
			// 
			this.buttonXTotalCost.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTotalCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXTotalCost.AutoCheckOnClick = true;
			this.buttonXTotalCost.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTotalCost.Location = new System.Drawing.Point(160, 60);
			this.buttonXTotalCost.Name = "buttonXTotalCost";
			this.buttonXTotalCost.Size = new System.Drawing.Size(113, 27);
			this.buttonXTotalCost.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTotalCost.TabIndex = 56;
			this.buttonXTotalCost.Text = "Gross Investment";
			this.buttonXTotalCost.TextColor = System.Drawing.Color.Black;
			this.buttonXTotalCost.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXTotalSpots
			// 
			this.buttonXTotalSpots.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTotalSpots.AutoCheckOnClick = true;
			this.buttonXTotalSpots.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTotalSpots.Location = new System.Drawing.Point(20, 60);
			this.buttonXTotalSpots.Name = "buttonXTotalSpots";
			this.buttonXTotalSpots.Size = new System.Drawing.Size(113, 27);
			this.buttonXTotalSpots.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTotalSpots.TabIndex = 55;
			this.buttonXTotalSpots.Text = "Total Spots";
			this.buttonXTotalSpots.TextColor = System.Drawing.Color.Black;
			this.buttonXTotalSpots.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXAvgRate
			// 
			this.buttonXAvgRate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAvgRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXAvgRate.AutoCheckOnClick = true;
			this.buttonXAvgRate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAvgRate.Location = new System.Drawing.Point(160, 11);
			this.buttonXAvgRate.Name = "buttonXAvgRate";
			this.buttonXAvgRate.Size = new System.Drawing.Size(113, 27);
			this.buttonXAvgRate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXAvgRate.TabIndex = 54;
			this.buttonXAvgRate.Text = "Average Rate";
			this.buttonXAvgRate.TextColor = System.Drawing.Color.Black;
			this.buttonXAvgRate.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// buttonXTotalPeriods
			// 
			this.buttonXTotalPeriods.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXTotalPeriods.AutoCheckOnClick = true;
			this.buttonXTotalPeriods.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXTotalPeriods.Location = new System.Drawing.Point(20, 11);
			this.buttonXTotalPeriods.Name = "buttonXTotalPeriods";
			this.buttonXTotalPeriods.Size = new System.Drawing.Size(113, 27);
			this.buttonXTotalPeriods.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXTotalPeriods.TabIndex = 53;
			this.buttonXTotalPeriods.Text = "Total Weeks";
			this.buttonXTotalPeriods.TextColor = System.Drawing.Color.Black;
			this.buttonXTotalPeriods.CheckedChanged += new System.EventHandler(this.button_CheckedChanged);
			// 
			// xtraTabPageOptionsStyle
			// 
			this.xtraTabPageOptionsStyle.Controls.Add(this.pnStyle);
			this.xtraTabPageOptionsStyle.Name = "xtraTabPageOptionsStyle";
			this.xtraTabPageOptionsStyle.Size = new System.Drawing.Size(292, 475);
			this.xtraTabPageOptionsStyle.Text = "Options";
			// 
			// pnStyle
			// 
			this.pnStyle.BackColor = System.Drawing.Color.Transparent;
			this.pnStyle.Controls.Add(this.laColorsTitle);
			this.pnStyle.Controls.Add(this.pnColors);
			this.pnStyle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnStyle.Location = new System.Drawing.Point(0, 0);
			this.pnStyle.Name = "pnStyle";
			this.pnStyle.Size = new System.Drawing.Size(292, 475);
			this.pnStyle.TabIndex = 0;
			// 
			// laColorsTitle
			// 
			this.laColorsTitle.AutoSize = true;
			this.laColorsTitle.Location = new System.Drawing.Point(3, 13);
			this.laColorsTitle.Name = "laColorsTitle";
			this.laColorsTitle.Size = new System.Drawing.Size(134, 16);
			this.laColorsTitle.TabIndex = 48;
			this.laColorsTitle.Text = "Schedule Table Color:";
			// 
			// pnColors
			// 
			this.pnColors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnColors.Controls.Add(this.xtraScrollableControlColors);
			this.pnColors.Location = new System.Drawing.Point(6, 32);
			this.pnColors.Name = "pnColors";
			this.pnColors.Size = new System.Drawing.Size(281, 390);
			this.pnColors.TabIndex = 46;
			// 
			// xtraScrollableControlColors
			// 
			this.xtraScrollableControlColors.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.xtraScrollableControlColors.Appearance.Options.UseBackColor = true;
			this.xtraScrollableControlColors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraScrollableControlColors.Location = new System.Drawing.Point(0, 0);
			this.xtraScrollableControlColors.Name = "xtraScrollableControlColors";
			this.xtraScrollableControlColors.Size = new System.Drawing.Size(281, 390);
			this.xtraScrollableControlColors.TabIndex = 45;
			// 
			// retractableBarControl
			// 
			this.retractableBarControl.AnimationDelay = 0;
			this.retractableBarControl.BackColor = System.Drawing.Color.Transparent;
			// 
			// retractableBarControl.Content
			// 
			this.retractableBarControl.Content.Controls.Add(this.xtraTabControlOptions);
			this.retractableBarControl.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBarControl.Content.Location = new System.Drawing.Point(2, 42);
			this.retractableBarControl.Content.Name = "Content";
			this.retractableBarControl.Content.Size = new System.Drawing.Size(296, 506);
			this.retractableBarControl.Content.TabIndex = 1;
			this.retractableBarControl.ContentSize = 300;
			this.retractableBarControl.Dock = System.Windows.Forms.DockStyle.Left;
			this.retractableBarControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.retractableBarControl.Location = new System.Drawing.Point(0, 0);
			this.retractableBarControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.retractableBarControl.Name = "retractableBarControl";
			this.retractableBarControl.Size = new System.Drawing.Size(300, 550);
			this.retractableBarControl.TabIndex = 4;
			// 
			// xtraTabControlData
			// 
			this.xtraTabControlData.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlData.Appearance.Options.UseFont = true;
			this.xtraTabControlData.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlData.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlData.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlData.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlData.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlData.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlData.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlData.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlData.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlData.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlData.Location = new System.Drawing.Point(300, 0);
			this.xtraTabControlData.Name = "xtraTabControlData";
			this.xtraTabControlData.SelectedTabPage = this.xtraTabPageDataSchedule;
			this.xtraTabControlData.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
			this.xtraTabControlData.Size = new System.Drawing.Size(832, 550);
			this.xtraTabControlData.TabIndex = 5;
			this.xtraTabControlData.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageDataSchedule,
            this.xtraTabPageDataSpecs});
			// 
			// xtraTabPageDataSchedule
			// 
			this.xtraTabPageDataSchedule.Controls.Add(this.pnPageSchedule);
			this.xtraTabPageDataSchedule.Name = "xtraTabPageDataSchedule";
			this.xtraTabPageDataSchedule.Size = new System.Drawing.Size(826, 544);
			this.xtraTabPageDataSchedule.Text = "Schedule";
			// 
			// xtraTabPageDataSpecs
			// 
			this.xtraTabPageDataSpecs.Name = "xtraTabPageDataSpecs";
			this.xtraTabPageDataSpecs.Size = new System.Drawing.Size(826, 544);
			this.xtraTabPageDataSpecs.Text = "Pre-Buy Specs";
			// 
			// ScheduleSectionControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.xtraTabControlData);
			this.Controls.Add(this.popupContainerControlProgramSource);
			this.Controls.Add(this.retractableBarControl);
			this.Controls.Add(this.pnBottom);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "ScheduleSectionControl";
			this.Size = new System.Drawing.Size(1132, 593);
			((System.ComponentModel.ISupportInitialize)(this.gridControlSchedule)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewSchedule)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStations)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxDayparts)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEditProgram)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.popupContainerControlProgramSource)).EndInit();
			this.popupContainerControlProgramSource.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlProgramSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewProgramSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxDays)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxTimes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxLengths)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditRate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditRating)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditSpot)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit000s)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditProgram)).EndInit();
			this.pnTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.pnBottom.ResumeLayout(false);
			this.pnAgencyDiscount.ResumeLayout(false);
			this.pnNetRate.ResumeLayout(false);
			this.pnTotalCost.ResumeLayout(false);
			this.pnAvgRate.ResumeLayout(false);
			this.pnTotalCPP.ResumeLayout(false);
			this.pnTotalGRP.ResumeLayout(false);
			this.pnTotalSpots.ResumeLayout(false);
			this.pnTotalPeriods.ResumeLayout(false);
			this.pnPageSchedule.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbNoPrograms)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlOptions)).EndInit();
			this.xtraTabControlOptions.ResumeLayout(false);
			this.xtraTabPageOptionsLine.ResumeLayout(false);
			this.pnOptionsLine.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditLineAdvanced.Properties)).EndInit();
			this.xtraTabPageOptionsDigital.ResumeLayout(false);
			this.xtraTabPageOptionsTotals.ResumeLayout(false);
			this.pnOptionsTotals.ResumeLayout(false);
			this.xtraTabPageOptionsStyle.ResumeLayout(false);
			this.pnStyle.ResumeLayout(false);
			this.pnStyle.PerformLayout();
			this.pnColors.ResumeLayout(false);
			this.retractableBarControl.Content.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlData)).EndInit();
			this.xtraTabControlData.ResumeLayout(false);
			this.xtraTabPageDataSchedule.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlSchedule;
	    protected System.Windows.Forms.Panel pnTop;
	    protected System.Windows.Forms.Panel pnBottom;
	    protected DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridViewSchedule;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnIndex;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnStation;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnLength;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnTime;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnRate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnRating;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnCPP;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnGRP;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnDay;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxStations;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxDays;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxTimes;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxLengths;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditRating;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditSpot;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit000s;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnTotalSpots;
        private System.Windows.Forms.Panel pnAgencyDiscount;
        private System.Windows.Forms.Label laAgencyDiscountValue;
        private System.Windows.Forms.Label laAgencyDiscountTitle;
        private System.Windows.Forms.Panel pnNetRate;
        private System.Windows.Forms.Label laNetRateValue;
        private System.Windows.Forms.Label laNetRateTitle;
        private System.Windows.Forms.Panel pnTotalCost;
        private System.Windows.Forms.Label laTotalCostValue;
        private System.Windows.Forms.Label laTotalCostTitle;
        private System.Windows.Forms.Panel pnAvgRate;
        private System.Windows.Forms.Label laAvgRateValue;
        private System.Windows.Forms.Label laAvgRateTitle;
        private System.Windows.Forms.Panel pnTotalCPP;
        private System.Windows.Forms.Label laTotalCPPValue;
        private System.Windows.Forms.Label laTotalCPPTitle;
        private System.Windows.Forms.Panel pnTotalGRP;
        private System.Windows.Forms.Label laTotalGRPValue;
        private System.Windows.Forms.Label laTotalGRPTitle;
        private System.Windows.Forms.Panel pnTotalPeriods;
		private System.Windows.Forms.Label laTotalPeriodsValue;
        private System.Windows.Forms.Panel pnTotalSpots;
        private System.Windows.Forms.Label laTotalSpotsValue;
		private System.Windows.Forms.Label laTotalSpotsTitle;
	    protected System.Windows.Forms.Label laScheduleInfo;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnCost;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnDaypart;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxDayparts;
        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit repositoryItemPopupContainerEditProgram;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControlProgramSource;
        private DevExpress.XtraGrid.GridControl gridControlProgramSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProgramSource;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProgramSourceId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProgramSourceName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProgramSourceStation;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProgramSourceDaypart;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProgramSourceDay;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProgramSourceTime;
		private System.Windows.Forms.Label laProgramSourceInfo;
		private System.Windows.Forms.Panel pnPageSchedule;
	    protected DevExpress.XtraTab.XtraTabControl xtraTabControlOptions;
	    protected DevExpress.XtraTab.XtraTabPage xtraTabPageOptionsLine;
	    protected DevExpress.XtraTab.XtraTabPage xtraTabPageOptionsTotals;
	    protected System.Windows.Forms.Panel pnOptionsLine;
		private System.Windows.Forms.Panel pnOptionsTotals;
		private DevComponents.DotNetBar.ButtonX buttonXTime;
		private DevComponents.DotNetBar.ButtonX buttonXSpots;
		private DevComponents.DotNetBar.ButtonX buttonXCost;
		private DevComponents.DotNetBar.ButtonX buttonXRate;
		private DevComponents.DotNetBar.ButtonX buttonXGRP;
		private DevComponents.DotNetBar.ButtonX buttonXRating;
		private DevComponents.DotNetBar.ButtonX buttonXDay;
		private DevComponents.DotNetBar.ButtonX buttonXDaypart;
		private DevComponents.DotNetBar.ButtonX buttonXCPP;
		private DevComponents.DotNetBar.ButtonX buttonXLength;
		private DevComponents.DotNetBar.ButtonX buttonXStation;
		private DevComponents.DotNetBar.ButtonX buttonXDiscount;
		private DevComponents.DotNetBar.ButtonX buttonXTotalGRP;
		private DevComponents.DotNetBar.ButtonX buttonXNetRate;
		private DevComponents.DotNetBar.ButtonX buttonXTotalCPP;
		private DevComponents.DotNetBar.ButtonX buttonXTotalCost;
		private DevComponents.DotNetBar.ButtonX buttonXTotalSpots;
		private DevComponents.DotNetBar.ButtonX buttonXAvgRate;
		private DevComponents.DotNetBar.ButtonX buttonXTotalPeriods;
	    protected DevExpress.XtraTab.XtraTabPage xtraTabPageOptionsDigital;
	    protected NewBizWiz.MediaSchedule.Controls.PresentationClasses.Digital.DigitalInfoControl digitalInfoControl;
	    protected DevExpress.XtraTab.XtraTabPage xtraTabPageOptionsStyle;
		private System.Windows.Forms.Panel pnStyle;
	    protected System.Windows.Forms.Panel pnColors;
	    protected DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControlColors;
		protected System.Windows.Forms.Label laTotalPeriodsTitle;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEditProgram;
		private QuarterSelectorControl quarterSelectorControl;
		private System.Windows.Forms.PictureBox pbNoPrograms;
		private System.Windows.Forms.Label laColorsTitle;
	    protected CommonGUI.RetractableBar.RetractableBarLeft retractableBarControl;
		private DevExpress.XtraEditors.LabelControl labelControlFlexFlightDatesWarning;
		protected DevExpress.XtraTab.XtraTabPage xtraTabPageOptionsQuickShare;
	    protected DevExpress.XtraTab.XtraTabControl xtraTabControlData;
	    protected DevExpress.XtraTab.XtraTabPage xtraTabPageDataSchedule;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageDataSpecs;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandId;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandLogo;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnLogoImage;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandStation;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandProgram;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandRate;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandSpots;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandTotals;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnLogoSource;
		private DevComponents.DotNetBar.ButtonX buttonXLogo;
		private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEditLineAdvanced;
    }
}
