namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    partial class PublicationDetailedGridControl
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
            this.gridControlPublication = new DevExpress.XtraGrid.GridControl();
            this.gridViewPublications = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.gridColumnPCIRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEditPCIRate = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumnADRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEditADRate = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumnDiscountRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEditDiscount = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumnColorPricing = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEditColor = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumnFinalRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnColumnInches = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumnPageSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDimensions = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnMechanicals = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPublication = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnReadership = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDelivery = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDeadline = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPercentOfPage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnHeader = new System.Windows.Forms.Panel();
            this.laDecisionMaker = new System.Windows.Forms.Label();
            this.laBusinessName = new System.Windows.Forms.Label();
            this.laDate = new System.Windows.Forms.Label();
            this.laPublicationName = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.comboBoxEditSchedule = new DevExpress.XtraEditors.ComboBoxEdit();
            this.pnLine = new System.Windows.Forms.Panel();
            this.laFlightDates = new System.Windows.Forms.Label();
            this.textEditHeader = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPublication)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPublications)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDate.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditPCIRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditADRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditDiscount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit)).BeginInit();
            this.pnHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSchedule.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditHeader.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlPublication
            // 
            this.gridControlPublication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlPublication.Location = new System.Drawing.Point(0, 159);
            this.gridControlPublication.MainView = this.gridViewPublications;
            this.gridControlPublication.Name = "gridControlPublication";
            this.gridControlPublication.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEditPCIRate,
            this.repositoryItemSpinEditADRate,
            this.repositoryItemSpinEditColor,
            this.repositoryItemSpinEditDiscount,
            this.repositoryItemDate,
            this.repositoryItemTextEdit});
            this.gridControlPublication.Size = new System.Drawing.Size(717, 261);
            this.gridControlPublication.TabIndex = 1;
            this.gridControlPublication.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPublications});
            // 
            // gridViewPublications
            // 
            this.gridViewPublications.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewPublications.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewPublications.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewPublications.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewPublications.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridViewPublications.Appearance.Preview.ForeColor = System.Drawing.Color.Black;
            this.gridViewPublications.Appearance.Preview.Options.UseFont = true;
            this.gridViewPublications.Appearance.Preview.Options.UseForeColor = true;
            this.gridViewPublications.Appearance.Preview.Options.UseTextOptions = true;
            this.gridViewPublications.Appearance.Preview.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridViewPublications.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewPublications.Appearance.Row.Options.UseFont = true;
            this.gridViewPublications.Appearance.Row.Options.UseTextOptions = true;
            this.gridViewPublications.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridViewPublications.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnID,
            this.gridColumnDate,
            this.gridColumnPCIRate,
            this.gridColumnADRate,
            this.gridColumnDiscountRate,
            this.gridColumnColorPricing,
            this.gridColumnFinalRate,
            this.gridColumnIndex,
            this.gridColumnColumnInches,
            this.gridColumnPageSize,
            this.gridColumnDimensions,
            this.gridColumnMechanicals,
            this.gridColumnPublication,
            this.gridColumnSection,
            this.gridColumnReadership,
            this.gridColumnDelivery,
            this.gridColumnDeadline,
            this.gridColumnPercentOfPage});
            this.gridViewPublications.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewPublications.GridControl = this.gridControlPublication;
            this.gridViewPublications.Name = "gridViewPublications";
            this.gridViewPublications.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewPublications.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewPublications.OptionsBehavior.Editable = false;
            this.gridViewPublications.OptionsBehavior.ReadOnly = true;
            this.gridViewPublications.OptionsCustomization.AllowFilter = false;
            this.gridViewPublications.OptionsCustomization.AllowGroup = false;
            this.gridViewPublications.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewPublications.OptionsCustomization.AllowSort = false;
            this.gridViewPublications.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridViewPublications.OptionsMenu.EnableColumnMenu = false;
            this.gridViewPublications.OptionsMenu.EnableFooterMenu = false;
            this.gridViewPublications.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridViewPublications.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gridViewPublications.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gridViewPublications.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewPublications.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridViewPublications.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridViewPublications.OptionsView.AutoCalcPreviewLineCount = true;
            this.gridViewPublications.OptionsView.ColumnAutoWidth = false;
            this.gridViewPublications.OptionsView.RowAutoHeight = true;
            this.gridViewPublications.OptionsView.ShowDetailButtons = false;
            this.gridViewPublications.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewPublications.OptionsView.ShowGroupPanel = false;
            this.gridViewPublications.OptionsView.ShowIndicator = false;
            this.gridViewPublications.OptionsView.ShowPreview = true;
            this.gridViewPublications.PreviewFieldName = "FullComment";
            this.gridViewPublications.PreviewIndent = 20;
            this.gridViewPublications.RowHeight = 40;
            this.gridViewPublications.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.gridViewPublications_ColumnWidthChanged);
            this.gridViewPublications.CalcPreviewText += new DevExpress.XtraGrid.Views.Grid.CalcPreviewTextEventHandler(this.gridViewPublications_CalcPreviewText);
            this.gridViewPublications.ColumnPositionChanged += new System.EventHandler(this.gridViewPublications_ColumnPositionChanged);
            this.gridViewPublications.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridViewPublications_MouseUp);
            this.gridViewPublications.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridViewPublications_MouseMove);
            this.gridViewPublications.DoubleClick += new System.EventHandler(this.gridViewPublication_DoubleClick);
            // 
            // gridColumnID
            // 
            this.gridColumnID.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnID.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnID.Caption = "ID";
            this.gridColumnID.FieldName = "ID";
            this.gridColumnID.Name = "gridColumnID";
            this.gridColumnID.Visible = true;
            this.gridColumnID.VisibleIndex = 1;
            this.gridColumnID.Width = 50;
            // 
            // gridColumnDate
            // 
            this.gridColumnDate.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnDate.Caption = "Day/Date";
            this.gridColumnDate.ColumnEdit = this.repositoryItemDate;
            this.gridColumnDate.FieldName = "DateObject";
            this.gridColumnDate.Name = "gridColumnDate";
            this.gridColumnDate.Visible = true;
            this.gridColumnDate.VisibleIndex = 0;
            this.gridColumnDate.Width = 155;
            // 
            // repositoryItemDate
            // 
            this.repositoryItemDate.Appearance.Options.UseTextOptions = true;
            this.repositoryItemDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemDate.AppearanceFocused.Options.UseTextOptions = true;
            this.repositoryItemDate.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemDate.AutoHeight = false;
            this.repositoryItemDate.DisplayFormat.FormatString = "ddd, MM/dd/yy";
            this.repositoryItemDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemDate.EditFormat.FormatString = "ddd, MM/dd/yy";
            this.repositoryItemDate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemDate.Name = "repositoryItemDate";
            this.repositoryItemDate.NullText = "Select Date First";
            this.repositoryItemDate.ShowToday = false;
            this.repositoryItemDate.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // gridColumnPCIRate
            // 
            this.gridColumnPCIRate.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnPCIRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnPCIRate.Caption = "PCI";
            this.gridColumnPCIRate.ColumnEdit = this.repositoryItemSpinEditPCIRate;
            this.gridColumnPCIRate.FieldName = "PCIRate";
            this.gridColumnPCIRate.Name = "gridColumnPCIRate";
            this.gridColumnPCIRate.Visible = true;
            this.gridColumnPCIRate.VisibleIndex = 2;
            this.gridColumnPCIRate.Width = 110;
            // 
            // repositoryItemSpinEditPCIRate
            // 
            this.repositoryItemSpinEditPCIRate.AutoHeight = false;
            this.repositoryItemSpinEditPCIRate.DisplayFormat.FormatString = "  $#,###.00";
            this.repositoryItemSpinEditPCIRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditPCIRate.EditFormat.FormatString = "  $#,###.00";
            this.repositoryItemSpinEditPCIRate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditPCIRate.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.repositoryItemSpinEditPCIRate.Name = "repositoryItemSpinEditPCIRate";
            this.repositoryItemSpinEditPCIRate.NullText = "N/A";
            // 
            // gridColumnADRate
            // 
            this.gridColumnADRate.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnADRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnADRate.Caption = "Cost (B&W)";
            this.gridColumnADRate.ColumnEdit = this.repositoryItemSpinEditADRate;
            this.gridColumnADRate.FieldName = "ADRate";
            this.gridColumnADRate.Name = "gridColumnADRate";
            this.gridColumnADRate.Visible = true;
            this.gridColumnADRate.VisibleIndex = 3;
            this.gridColumnADRate.Width = 103;
            // 
            // repositoryItemSpinEditADRate
            // 
            this.repositoryItemSpinEditADRate.Appearance.Options.UseTextOptions = true;
            this.repositoryItemSpinEditADRate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemSpinEditADRate.AppearanceDisabled.Options.UseTextOptions = true;
            this.repositoryItemSpinEditADRate.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemSpinEditADRate.AppearanceFocused.Options.UseTextOptions = true;
            this.repositoryItemSpinEditADRate.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemSpinEditADRate.AppearanceReadOnly.Options.UseTextOptions = true;
            this.repositoryItemSpinEditADRate.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemSpinEditADRate.AutoHeight = false;
            this.repositoryItemSpinEditADRate.DisplayFormat.FormatString = "$#,###.00";
            this.repositoryItemSpinEditADRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditADRate.EditFormat.FormatString = "$#,###.00";
            this.repositoryItemSpinEditADRate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditADRate.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.repositoryItemSpinEditADRate.Name = "repositoryItemSpinEditADRate";
            // 
            // gridColumnDiscountRate
            // 
            this.gridColumnDiscountRate.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnDiscountRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnDiscountRate.Caption = "Discounts";
            this.gridColumnDiscountRate.ColumnEdit = this.repositoryItemSpinEditDiscount;
            this.gridColumnDiscountRate.FieldName = "DiscountRate";
            this.gridColumnDiscountRate.Name = "gridColumnDiscountRate";
            this.gridColumnDiscountRate.Visible = true;
            this.gridColumnDiscountRate.VisibleIndex = 4;
            this.gridColumnDiscountRate.Width = 110;
            // 
            // repositoryItemSpinEditDiscount
            // 
            this.repositoryItemSpinEditDiscount.AutoHeight = false;
            this.repositoryItemSpinEditDiscount.DisplayFormat.FormatString = "$#,###.00";
            this.repositoryItemSpinEditDiscount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditDiscount.EditFormat.FormatString = "$#,###.00";
            this.repositoryItemSpinEditDiscount.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditDiscount.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.repositoryItemSpinEditDiscount.Name = "repositoryItemSpinEditDiscount";
            // 
            // gridColumnColorPricing
            // 
            this.gridColumnColorPricing.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnColorPricing.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnColorPricing.Caption = "Color";
            this.gridColumnColorPricing.ColumnEdit = this.repositoryItemSpinEditColor;
            this.gridColumnColorPricing.FieldName = "ColorPricingObject";
            this.gridColumnColorPricing.Name = "gridColumnColorPricing";
            this.gridColumnColorPricing.Visible = true;
            this.gridColumnColorPricing.VisibleIndex = 5;
            this.gridColumnColorPricing.Width = 110;
            // 
            // repositoryItemSpinEditColor
            // 
            this.repositoryItemSpinEditColor.AutoHeight = false;
            this.repositoryItemSpinEditColor.DisplayFormat.FormatString = "$#,###.00";
            this.repositoryItemSpinEditColor.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditColor.EditFormat.FormatString = "$#,###.00";
            this.repositoryItemSpinEditColor.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditColor.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.repositoryItemSpinEditColor.Name = "repositoryItemSpinEditColor";
            // 
            // gridColumnFinalRate
            // 
            this.gridColumnFinalRate.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnFinalRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnFinalRate.Caption = "Total Cost";
            this.gridColumnFinalRate.ColumnEdit = this.repositoryItemSpinEditADRate;
            this.gridColumnFinalRate.FieldName = "FinalRate";
            this.gridColumnFinalRate.Name = "gridColumnFinalRate";
            this.gridColumnFinalRate.Visible = true;
            this.gridColumnFinalRate.VisibleIndex = 6;
            this.gridColumnFinalRate.Width = 110;
            // 
            // gridColumnIndex
            // 
            this.gridColumnIndex.AppearanceCell.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.gridColumnIndex.AppearanceCell.Options.UseFont = true;
            this.gridColumnIndex.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnIndex.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnIndex.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnIndex.Caption = "INS #";
            this.gridColumnIndex.FieldName = "Index";
            this.gridColumnIndex.Name = "gridColumnIndex";
            this.gridColumnIndex.Visible = true;
            this.gridColumnIndex.VisibleIndex = 7;
            this.gridColumnIndex.Width = 50;
            // 
            // gridColumnColumnInches
            // 
            this.gridColumnColumnInches.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnColumnInches.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnColumnInches.Caption = "Total Col. In.";
            this.gridColumnColumnInches.ColumnEdit = this.repositoryItemTextEdit;
            this.gridColumnColumnInches.FieldName = "SquareStringFormatted";
            this.gridColumnColumnInches.Name = "gridColumnColumnInches";
            this.gridColumnColumnInches.Visible = true;
            this.gridColumnColumnInches.VisibleIndex = 8;
            this.gridColumnColumnInches.Width = 110;
            // 
            // repositoryItemTextEdit
            // 
            this.repositoryItemTextEdit.AutoHeight = false;
            this.repositoryItemTextEdit.Name = "repositoryItemTextEdit";
            this.repositoryItemTextEdit.NullText = "N/A";
            // 
            // gridColumnPageSize
            // 
            this.gridColumnPageSize.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnPageSize.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnPageSize.Caption = "Page Size";
            this.gridColumnPageSize.ColumnEdit = this.repositoryItemTextEdit;
            this.gridColumnPageSize.FieldName = "PageSize";
            this.gridColumnPageSize.Name = "gridColumnPageSize";
            this.gridColumnPageSize.Visible = true;
            this.gridColumnPageSize.VisibleIndex = 9;
            this.gridColumnPageSize.Width = 110;
            // 
            // gridColumnDimensions
            // 
            this.gridColumnDimensions.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnDimensions.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnDimensions.Caption = "Col. x Inches";
            this.gridColumnDimensions.ColumnEdit = this.repositoryItemTextEdit;
            this.gridColumnDimensions.FieldName = "Dimensions";
            this.gridColumnDimensions.Name = "gridColumnDimensions";
            this.gridColumnDimensions.Visible = true;
            this.gridColumnDimensions.VisibleIndex = 11;
            this.gridColumnDimensions.Width = 110;
            // 
            // gridColumnMechanicals
            // 
            this.gridColumnMechanicals.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnMechanicals.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnMechanicals.Caption = "Mechanicals";
            this.gridColumnMechanicals.ColumnEdit = this.repositoryItemTextEdit;
            this.gridColumnMechanicals.FieldName = "Mechanicals";
            this.gridColumnMechanicals.Name = "gridColumnMechanicals";
            this.gridColumnMechanicals.Visible = true;
            this.gridColumnMechanicals.VisibleIndex = 12;
            this.gridColumnMechanicals.Width = 110;
            // 
            // gridColumnPublication
            // 
            this.gridColumnPublication.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnPublication.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnPublication.Caption = "Publication";
            this.gridColumnPublication.FieldName = "Publication";
            this.gridColumnPublication.Name = "gridColumnPublication";
            this.gridColumnPublication.Visible = true;
            this.gridColumnPublication.VisibleIndex = 13;
            this.gridColumnPublication.Width = 160;
            // 
            // gridColumnSection
            // 
            this.gridColumnSection.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnSection.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnSection.Caption = "Section";
            this.gridColumnSection.FieldName = "Section";
            this.gridColumnSection.Name = "gridColumnSection";
            this.gridColumnSection.Visible = true;
            this.gridColumnSection.VisibleIndex = 14;
            this.gridColumnSection.Width = 110;
            // 
            // gridColumnReadership
            // 
            this.gridColumnReadership.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnReadership.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnReadership.Caption = "Readership";
            this.gridColumnReadership.FieldName = "Readership";
            this.gridColumnReadership.Name = "gridColumnReadership";
            this.gridColumnReadership.Visible = true;
            this.gridColumnReadership.VisibleIndex = 15;
            this.gridColumnReadership.Width = 110;
            // 
            // gridColumnDelivery
            // 
            this.gridColumnDelivery.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnDelivery.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnDelivery.Caption = "Delivery";
            this.gridColumnDelivery.FieldName = "Delivery";
            this.gridColumnDelivery.Name = "gridColumnDelivery";
            this.gridColumnDelivery.Visible = true;
            this.gridColumnDelivery.VisibleIndex = 16;
            this.gridColumnDelivery.Width = 110;
            // 
            // gridColumnDeadline
            // 
            this.gridColumnDeadline.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnDeadline.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnDeadline.Caption = "Deadline";
            this.gridColumnDeadline.FieldName = "DeadlineForOutput";
            this.gridColumnDeadline.Name = "gridColumnDeadline";
            this.gridColumnDeadline.Visible = true;
            this.gridColumnDeadline.VisibleIndex = 17;
            this.gridColumnDeadline.Width = 110;
            // 
            // gridColumnPercentOfPage
            // 
            this.gridColumnPercentOfPage.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnPercentOfPage.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnPercentOfPage.Caption = "% of Page";
            this.gridColumnPercentOfPage.ColumnEdit = this.repositoryItemTextEdit;
            this.gridColumnPercentOfPage.FieldName = "PercentOfPage";
            this.gridColumnPercentOfPage.Name = "gridColumnPercentOfPage";
            this.gridColumnPercentOfPage.Visible = true;
            this.gridColumnPercentOfPage.VisibleIndex = 10;
            this.gridColumnPercentOfPage.Width = 110;
            // 
            // pnHeader
            // 
            this.pnHeader.Controls.Add(this.laDecisionMaker);
            this.pnHeader.Controls.Add(this.laBusinessName);
            this.pnHeader.Controls.Add(this.laDate);
            this.pnHeader.Controls.Add(this.laPublicationName);
            this.pnHeader.Controls.Add(this.pbLogo);
            this.pnHeader.Controls.Add(this.comboBoxEditSchedule);
            this.pnHeader.Controls.Add(this.pnLine);
            this.pnHeader.Controls.Add(this.laFlightDates);
            this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnHeader.Location = new System.Drawing.Point(0, 0);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(717, 159);
            this.pnHeader.TabIndex = 3;
            // 
            // laDecisionMaker
            // 
            this.laDecisionMaker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.laDecisionMaker.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic);
            this.laDecisionMaker.Location = new System.Drawing.Point(460, 59);
            this.laDecisionMaker.Name = "laDecisionMaker";
            this.laDecisionMaker.Size = new System.Drawing.Size(248, 16);
            this.laDecisionMaker.TabIndex = 46;
            this.laDecisionMaker.Text = "Decision-Maker Tag";
            this.laDecisionMaker.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // laBusinessName
            // 
            this.laBusinessName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.laBusinessName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic);
            this.laBusinessName.Location = new System.Drawing.Point(457, 12);
            this.laBusinessName.Name = "laBusinessName";
            this.laBusinessName.Size = new System.Drawing.Size(251, 18);
            this.laBusinessName.TabIndex = 45;
            this.laBusinessName.Text = "Business Name Tag";
            this.laBusinessName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // laDate
            // 
            this.laDate.AutoSize = true;
            this.laDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic);
            this.laDate.Location = new System.Drawing.Point(207, 59);
            this.laDate.Name = "laDate";
            this.laDate.Size = new System.Drawing.Size(60, 16);
            this.laDate.TabIndex = 44;
            this.laDate.Text = "Date Tag";
            // 
            // laPublicationName
            // 
            this.laPublicationName.AutoSize = true;
            this.laPublicationName.Font = new System.Drawing.Font("Arial", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.laPublicationName.Location = new System.Drawing.Point(206, 7);
            this.laPublicationName.Name = "laPublicationName";
            this.laPublicationName.Size = new System.Drawing.Size(156, 23);
            this.laPublicationName.TabIndex = 43;
            this.laPublicationName.Text = "Publication Tag";
            // 
            // pbLogo
            // 
            this.pbLogo.Location = new System.Drawing.Point(12, 3);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(165, 75);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 42;
            this.pbLogo.TabStop = false;
            // 
            // comboBoxEditSchedule
            // 
            this.comboBoxEditSchedule.Location = new System.Drawing.Point(12, 105);
            this.comboBoxEditSchedule.Name = "comboBoxEditSchedule";
            this.comboBoxEditSchedule.Properties.Appearance.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxEditSchedule.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEditSchedule.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditSchedule.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditSchedule.Size = new System.Drawing.Size(442, 38);
            this.comboBoxEditSchedule.TabIndex = 41;
            this.comboBoxEditSchedule.EditValueChanged += new System.EventHandler(this.comboBoxEditSchedule_EditValueChanged);
            // 
            // pnLine
            // 
            this.pnLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnLine.Location = new System.Drawing.Point(208, 43);
            this.pnLine.Name = "pnLine";
            this.pnLine.Size = new System.Drawing.Size(496, 1);
            this.pnLine.TabIndex = 11;
            // 
            // laFlightDates
            // 
            this.laFlightDates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.laFlightDates.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic);
            this.laFlightDates.Location = new System.Drawing.Point(460, 94);
            this.laFlightDates.Name = "laFlightDates";
            this.laFlightDates.Size = new System.Drawing.Size(246, 20);
            this.laFlightDates.TabIndex = 47;
            this.laFlightDates.Text = "Flight Dates Tag";
            this.laFlightDates.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textEditHeader
            // 
            this.textEditHeader.Location = new System.Drawing.Point(0, 0);
            this.textEditHeader.Name = "textEditHeader";
            this.textEditHeader.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textEditHeader.Properties.Appearance.Options.UseFont = true;
            this.textEditHeader.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditHeader.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.textEditHeader.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.textEditHeader.Properties.AppearanceFocused.Options.UseFont = true;
            this.textEditHeader.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.textEditHeader.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.textEditHeader.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.textEditHeader.Size = new System.Drawing.Size(100, 20);
            this.textEditHeader.TabIndex = 0;
            this.textEditHeader.Leave += new System.EventHandler(this.textEditHeader_Leave);
            // 
            // PublicationDetailedGridControl
            // 
            this.Controls.Add(this.gridControlPublication);
            this.Controls.Add(this.pnHeader);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Size = new System.Drawing.Size(717, 420);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPublication)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPublications)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDate.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditPCIRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditADRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditDiscount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit)).EndInit();
            this.pnHeader.ResumeLayout(false);
            this.pnHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSchedule.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditHeader.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.GridControl gridControlPublication;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditPCIRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditADRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditDiscount;
        public DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditColor;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.Panel pnLine;
        private DevExpress.XtraEditors.TextEdit textEditHeader;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnColumnInches;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnPageSize;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnMechanicals;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnPublication;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnSection;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnDelivery;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnDeadline;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnID;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnDate;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnPCIRate;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnADRate;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnDiscountRate;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnColorPricing;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnFinalRate;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnIndex;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewPublications;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnReadership;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnDimensions;
        public System.Windows.Forms.PictureBox pbLogo;
        public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSchedule;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnPercentOfPage;
        private System.Windows.Forms.Label laDecisionMaker;
        private System.Windows.Forms.Label laBusinessName;
        private System.Windows.Forms.Label laDate;
        private System.Windows.Forms.Label laPublicationName;
        private System.Windows.Forms.Label laFlightDates;

    }
}
