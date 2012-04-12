namespace MobileScheduleBuilder.CustomControls
{
    partial class ScheduleSettingsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduleSettingsControl));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            this.gridControlProducts = new DevExpress.XtraGrid.GridControl();
            this.advBandedGridViewProducts = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridColumnPosition = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumnType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemComboBoxProductType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumnCategory = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumnSubCategory = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumnName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemComboBoxProductNames = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumnWidth = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemSpinEditSize = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumnHeight = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumnRateType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemComboBoxRateType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumnDelete = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemButtonEditDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.pnHeader = new System.Windows.Forms.Panel();
            this.laScheduleName = new System.Windows.Forms.Label();
            this.laProducts = new System.Windows.Forms.Label();
            this.repositoryItemSpinEditRate = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumnRate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBandPosition = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBandType = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBandName = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBandWidth = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBandHeight = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBandRate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBandDelete = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxProductType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxProductNames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxRateType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditDelete)).BeginInit();
            this.pnHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditRate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlProducts
            // 
            this.gridControlProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlProducts.Location = new System.Drawing.Point(0, 30);
            this.gridControlProducts.MainView = this.advBandedGridViewProducts;
            this.gridControlProducts.Name = "gridControlProducts";
            this.gridControlProducts.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit,
            this.repositoryItemComboBoxProductNames,
            this.repositoryItemComboBoxProductType,
            this.repositoryItemComboBoxRateType,
            this.repositoryItemSpinEditSize,
            this.repositoryItemButtonEditDelete,
            this.repositoryItemSpinEditRate});
            this.gridControlProducts.Size = new System.Drawing.Size(828, 400);
            this.gridControlProducts.TabIndex = 0;
            this.gridControlProducts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridViewProducts});
            // 
            // advBandedGridViewProducts
            // 
            this.advBandedGridViewProducts.Appearance.BandPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.advBandedGridViewProducts.Appearance.BandPanel.Options.UseFont = true;
            this.advBandedGridViewProducts.Appearance.BandPanel.Options.UseTextOptions = true;
            this.advBandedGridViewProducts.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.advBandedGridViewProducts.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.advBandedGridViewProducts.Appearance.HeaderPanel.Options.UseFont = true;
            this.advBandedGridViewProducts.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.advBandedGridViewProducts.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.advBandedGridViewProducts.Appearance.Preview.Font = new System.Drawing.Font("Tahoma", 3.75F);
            this.advBandedGridViewProducts.Appearance.Preview.Options.UseFont = true;
            this.advBandedGridViewProducts.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
            this.advBandedGridViewProducts.Appearance.Row.Options.UseFont = true;
            this.advBandedGridViewProducts.Appearance.RowSeparator.BackColor = System.Drawing.Color.AliceBlue;
            this.advBandedGridViewProducts.Appearance.RowSeparator.Options.UseBackColor = true;
            this.advBandedGridViewProducts.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandPosition,
            this.gridBandType,
            this.gridBandName,
            this.gridBandWidth,
            this.gridBandHeight,
            this.gridBandRate,
            this.gridBandDelete});
            this.advBandedGridViewProducts.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gridColumnPosition,
            this.gridColumnType,
            this.gridColumnName,
            this.gridColumnRateType,
            this.gridColumnWidth,
            this.gridColumnHeight,
            this.gridColumnDelete,
            this.gridColumnCategory,
            this.gridColumnSubCategory,
            this.gridColumnRate});
            this.advBandedGridViewProducts.GridControl = this.gridControlProducts;
            this.advBandedGridViewProducts.Name = "advBandedGridViewProducts";
            this.advBandedGridViewProducts.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.advBandedGridViewProducts.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.advBandedGridViewProducts.OptionsBehavior.AutoPopulateColumns = false;
            this.advBandedGridViewProducts.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.advBandedGridViewProducts.OptionsCustomization.AllowBandMoving = false;
            this.advBandedGridViewProducts.OptionsCustomization.AllowFilter = false;
            this.advBandedGridViewProducts.OptionsCustomization.AllowGroup = false;
            this.advBandedGridViewProducts.OptionsCustomization.AllowQuickHideColumns = false;
            this.advBandedGridViewProducts.OptionsCustomization.AllowSort = false;
            this.advBandedGridViewProducts.OptionsFilter.AllowColumnMRUFilterList = false;
            this.advBandedGridViewProducts.OptionsFilter.AllowFilterEditor = false;
            this.advBandedGridViewProducts.OptionsFilter.AllowMRUFilterList = false;
            this.advBandedGridViewProducts.OptionsMenu.EnableColumnMenu = false;
            this.advBandedGridViewProducts.OptionsMenu.EnableFooterMenu = false;
            this.advBandedGridViewProducts.OptionsMenu.EnableGroupPanelMenu = false;
            this.advBandedGridViewProducts.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.advBandedGridViewProducts.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.advBandedGridViewProducts.OptionsSelection.EnableAppearanceHideSelection = false;
            this.advBandedGridViewProducts.OptionsSelection.UseIndicatorForSelection = false;
            this.advBandedGridViewProducts.OptionsView.ColumnAutoWidth = true;
            this.advBandedGridViewProducts.OptionsView.ShowColumnHeaders = false;
            this.advBandedGridViewProducts.OptionsView.ShowDetailButtons = false;
            this.advBandedGridViewProducts.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.advBandedGridViewProducts.OptionsView.ShowGroupPanel = false;
            this.advBandedGridViewProducts.OptionsView.ShowIndicator = false;
            this.advBandedGridViewProducts.RowSeparatorHeight = 10;
            this.advBandedGridViewProducts.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridViewProducts_ShowingEditor);
            this.advBandedGridViewProducts.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewProducts_CellValueChanged);
            // 
            // gridColumnPosition
            // 
            this.gridColumnPosition.AppearanceCell.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold);
            this.gridColumnPosition.AppearanceCell.Options.UseFont = true;
            this.gridColumnPosition.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnPosition.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnPosition.Caption = "Position";
            this.gridColumnPosition.ColumnEdit = this.repositoryItemButtonEdit;
            this.gridColumnPosition.FieldName = "Index";
            this.gridColumnPosition.Name = "gridColumnPosition";
            this.gridColumnPosition.OptionsColumn.AllowMove = false;
            this.gridColumnPosition.OptionsColumn.AllowSize = false;
            this.gridColumnPosition.OptionsColumn.FixedWidth = true;
            this.gridColumnPosition.OptionsColumn.ReadOnly = true;
            this.gridColumnPosition.OptionsColumn.ShowCaption = false;
            this.gridColumnPosition.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridColumnPosition.Visible = true;
            this.gridColumnPosition.Width = 120;
            // 
            // repositoryItemButtonEdit
            // 
            this.repositoryItemButtonEdit.Appearance.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repositoryItemButtonEdit.Appearance.Options.UseFont = true;
            this.repositoryItemButtonEdit.Appearance.Options.UseTextOptions = true;
            this.repositoryItemButtonEdit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemButtonEdit.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repositoryItemButtonEdit.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemButtonEdit.AppearanceDisabled.Options.UseTextOptions = true;
            this.repositoryItemButtonEdit.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemButtonEdit.AutoHeight = false;
            this.repositoryItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "Nudge Up", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit.Buttons1"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "Nudge Down", null, null, true)});
            this.repositoryItemButtonEdit.Name = "repositoryItemButtonEdit";
            this.repositoryItemButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemButtonEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit_ButtonClick);
            // 
            // gridColumnType
            // 
            this.gridColumnType.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridColumnType.AppearanceCell.Options.UseFont = true;
            this.gridColumnType.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnType.Caption = "Mobile Category";
            this.gridColumnType.ColumnEdit = this.repositoryItemComboBoxProductType;
            this.gridColumnType.FieldName = "WebCategory";
            this.gridColumnType.Name = "gridColumnType";
            this.gridColumnType.Visible = true;
            this.gridColumnType.Width = 615;
            // 
            // repositoryItemComboBoxProductType
            // 
            this.repositoryItemComboBoxProductType.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxProductType.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxProductType.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxProductType.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxProductType.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxProductType.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxProductType.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxProductType.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxProductType.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxProductType.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxProductType.AutoHeight = false;
            this.repositoryItemComboBoxProductType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxProductType.Items.AddRange(new object[] {
            "Display Ads",
            "Premium Opportunities",
            "Rich Media & Video",
            "Sponsorships",
            "Text Links"});
            this.repositoryItemComboBoxProductType.Name = "repositoryItemComboBoxProductType";
            this.repositoryItemComboBoxProductType.NullText = "Select web category";
            this.repositoryItemComboBoxProductType.Sorted = true;
            this.repositoryItemComboBoxProductType.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBoxProductType.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(this.repositoryItemComboBoxProductType_CloseUp);
            // 
            // gridColumnCategory
            // 
            this.gridColumnCategory.Caption = "Category";
            this.gridColumnCategory.FieldName = "Category";
            this.gridColumnCategory.Name = "gridColumnCategory";
            // 
            // gridColumnSubCategory
            // 
            this.gridColumnSubCategory.Caption = "SubCategory";
            this.gridColumnSubCategory.FieldName = "SubCategory";
            this.gridColumnSubCategory.Name = "gridColumnSubCategory";
            // 
            // gridColumnName
            // 
            this.gridColumnName.AppearanceCell.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.gridColumnName.AppearanceCell.Options.UseFont = true;
            this.gridColumnName.Caption = "Mobile Product";
            this.gridColumnName.ColumnEdit = this.repositoryItemComboBoxProductNames;
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.Visible = true;
            this.gridColumnName.Width = 600;
            // 
            // repositoryItemComboBoxProductNames
            // 
            this.repositoryItemComboBoxProductNames.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repositoryItemComboBoxProductNames.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxProductNames.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 12F);
            this.repositoryItemComboBoxProductNames.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxProductNames.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repositoryItemComboBoxProductNames.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxProductNames.AppearanceFocused.Font = new System.Drawing.Font("Arial", 12F);
            this.repositoryItemComboBoxProductNames.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxProductNames.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 12F);
            this.repositoryItemComboBoxProductNames.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxProductNames.AutoHeight = false;
            this.repositoryItemComboBoxProductNames.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxProductNames.Name = "repositoryItemComboBoxProductNames";
            this.repositoryItemComboBoxProductNames.NullText = "Select Product";
            this.repositoryItemComboBoxProductNames.Sorted = true;
            this.repositoryItemComboBoxProductNames.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.repositoryItemComboBoxProductName_Closed);
            // 
            // gridColumnWidth
            // 
            this.gridColumnWidth.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridColumnWidth.AppearanceCell.Options.UseFont = true;
            this.gridColumnWidth.Caption = "Width";
            this.gridColumnWidth.ColumnEdit = this.repositoryItemSpinEditSize;
            this.gridColumnWidth.FieldName = "Width";
            this.gridColumnWidth.Name = "gridColumnWidth";
            this.gridColumnWidth.OptionsColumn.FixedWidth = true;
            this.gridColumnWidth.Visible = true;
            this.gridColumnWidth.Width = 80;
            // 
            // repositoryItemSpinEditSize
            // 
            this.repositoryItemSpinEditSize.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditSize.Appearance.Options.UseFont = true;
            this.repositoryItemSpinEditSize.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditSize.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemSpinEditSize.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditSize.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemSpinEditSize.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditSize.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemSpinEditSize.AutoHeight = false;
            this.repositoryItemSpinEditSize.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditSize.DisplayFormat.FormatString = "#,##0";
            this.repositoryItemSpinEditSize.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditSize.EditFormat.FormatString = "#,##0";
            this.repositoryItemSpinEditSize.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditSize.HideSelection = false;
            this.repositoryItemSpinEditSize.IsFloatValue = false;
            this.repositoryItemSpinEditSize.Mask.EditMask = "N00";
            this.repositoryItemSpinEditSize.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.repositoryItemSpinEditSize.Name = "repositoryItemSpinEditSize";
            this.repositoryItemSpinEditSize.NullText = "N/A";
            // 
            // gridColumnHeight
            // 
            this.gridColumnHeight.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridColumnHeight.AppearanceCell.Options.UseFont = true;
            this.gridColumnHeight.Caption = "Height";
            this.gridColumnHeight.ColumnEdit = this.repositoryItemSpinEditSize;
            this.gridColumnHeight.FieldName = "Height";
            this.gridColumnHeight.Name = "gridColumnHeight";
            this.gridColumnHeight.OptionsColumn.FixedWidth = true;
            this.gridColumnHeight.Visible = true;
            this.gridColumnHeight.Width = 80;
            // 
            // gridColumnRateType
            // 
            this.gridColumnRateType.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridColumnRateType.AppearanceCell.Options.UseFont = true;
            this.gridColumnRateType.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnRateType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumnRateType.Caption = "Pricing Strategy";
            this.gridColumnRateType.ColumnEdit = this.repositoryItemComboBoxRateType;
            this.gridColumnRateType.FieldName = "RateTypeText";
            this.gridColumnRateType.Name = "gridColumnRateType";
            this.gridColumnRateType.OptionsColumn.FixedWidth = true;
            this.gridColumnRateType.Visible = true;
            this.gridColumnRateType.Width = 108;
            // 
            // repositoryItemComboBoxRateType
            // 
            this.repositoryItemComboBoxRateType.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxRateType.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxRateType.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxRateType.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxRateType.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxRateType.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxRateType.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxRateType.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxRateType.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxRateType.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxRateType.AutoHeight = false;
            this.repositoryItemComboBoxRateType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxRateType.Items.AddRange(new object[] {
            "CPM",
            "Fixed"});
            this.repositoryItemComboBoxRateType.Name = "repositoryItemComboBoxRateType";
            this.repositoryItemComboBoxRateType.NullText = "Select pricing strategy";
            this.repositoryItemComboBoxRateType.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // gridColumnDelete
            // 
            this.gridColumnDelete.Caption = "Delete";
            this.gridColumnDelete.ColumnEdit = this.repositoryItemButtonEditDelete;
            this.gridColumnDelete.FieldName = "Index";
            this.gridColumnDelete.Name = "gridColumnDelete";
            this.gridColumnDelete.OptionsColumn.AllowMove = false;
            this.gridColumnDelete.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnDelete.OptionsColumn.FixedWidth = true;
            this.gridColumnDelete.OptionsColumn.ReadOnly = true;
            this.gridColumnDelete.OptionsColumn.ShowCaption = false;
            this.gridColumnDelete.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridColumnDelete.Visible = true;
            this.gridColumnDelete.Width = 48;
            // 
            // repositoryItemButtonEditDelete
            // 
            this.repositoryItemButtonEditDelete.AutoHeight = false;
            this.repositoryItemButtonEditDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEditDelete.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "Delete this line", null, null, true)});
            this.repositoryItemButtonEditDelete.Name = "repositoryItemButtonEditDelete";
            this.repositoryItemButtonEditDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEditDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditDelete_ButtonClick);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // pnHeader
            // 
            this.pnHeader.Controls.Add(this.laScheduleName);
            this.pnHeader.Controls.Add(this.laProducts);
            this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnHeader.Location = new System.Drawing.Point(0, 0);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(828, 30);
            this.pnHeader.TabIndex = 1;
            // 
            // laScheduleName
            // 
            this.laScheduleName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.laScheduleName.Location = new System.Drawing.Point(272, 0);
            this.laScheduleName.Name = "laScheduleName";
            this.laScheduleName.Size = new System.Drawing.Size(556, 30);
            this.laScheduleName.TabIndex = 0;
            this.laScheduleName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // laProducts
            // 
            this.laProducts.Dock = System.Windows.Forms.DockStyle.Left;
            this.laProducts.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laProducts.Location = new System.Drawing.Point(0, 0);
            this.laProducts.Name = "laProducts";
            this.laProducts.Size = new System.Drawing.Size(272, 30);
            this.laProducts.TabIndex = 1;
            this.laProducts.Text = "Mobile Sales Products:";
            this.laProducts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // repositoryItemSpinEditRate
            // 
            this.repositoryItemSpinEditRate.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditRate.Appearance.Options.UseFont = true;
            this.repositoryItemSpinEditRate.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditRate.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemSpinEditRate.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditRate.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemSpinEditRate.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditRate.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemSpinEditRate.AutoHeight = false;
            this.repositoryItemSpinEditRate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditRate.DisplayFormat.FormatString = "$#,##0.00";
            this.repositoryItemSpinEditRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditRate.EditFormat.FormatString = "$#,##0.00";
            this.repositoryItemSpinEditRate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditRate.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.repositoryItemSpinEditRate.Name = "repositoryItemSpinEditRate";
            // 
            // gridColumnRate
            // 
            this.gridColumnRate.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridColumnRate.AppearanceCell.Options.UseFont = true;
            this.gridColumnRate.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumnRate.Caption = "Rate";
            this.gridColumnRate.ColumnEdit = this.repositoryItemSpinEditRate;
            this.gridColumnRate.FieldName = "DefaultRate";
            this.gridColumnRate.Name = "gridColumnRate";
            this.gridColumnRate.OptionsColumn.FixedWidth = true;
            this.gridColumnRate.Visible = true;
            this.gridColumnRate.Width = 87;
            // 
            // gridBandPosition
            // 
            this.gridBandPosition.Columns.Add(this.gridColumnPosition);
            this.gridBandPosition.MinWidth = 20;
            this.gridBandPosition.Name = "gridBandPosition";
            this.gridBandPosition.OptionsBand.AllowSize = false;
            this.gridBandPosition.OptionsBand.FixedWidth = true;
            this.gridBandPosition.Width = 120;
            // 
            // gridBandType
            // 
            this.gridBandType.Caption = "Mobile Category";
            this.gridBandType.Columns.Add(this.gridColumnType);
            this.gridBandType.Columns.Add(this.gridColumnCategory);
            this.gridBandType.Columns.Add(this.gridColumnSubCategory);
            this.gridBandType.MinWidth = 20;
            this.gridBandType.Name = "gridBandType";
            this.gridBandType.Width = 615;
            // 
            // gridBandName
            // 
            this.gridBandName.Caption = "Mobile Product";
            this.gridBandName.Columns.Add(this.gridColumnName);
            this.gridBandName.MinWidth = 20;
            this.gridBandName.Name = "gridBandName";
            this.gridBandName.Width = 600;
            // 
            // gridBandWidth
            // 
            this.gridBandWidth.Caption = "Width";
            this.gridBandWidth.Columns.Add(this.gridColumnWidth);
            this.gridBandWidth.MinWidth = 20;
            this.gridBandWidth.Name = "gridBandWidth";
            this.gridBandWidth.OptionsBand.AllowSize = false;
            this.gridBandWidth.OptionsBand.FixedWidth = true;
            this.gridBandWidth.Width = 80;
            // 
            // gridBandHeight
            // 
            this.gridBandHeight.Caption = "Height";
            this.gridBandHeight.Columns.Add(this.gridColumnHeight);
            this.gridBandHeight.MinWidth = 20;
            this.gridBandHeight.Name = "gridBandHeight";
            this.gridBandHeight.OptionsBand.AllowSize = false;
            this.gridBandHeight.OptionsBand.FixedWidth = true;
            this.gridBandHeight.Width = 80;
            // 
            // gridBandRate
            // 
            this.gridBandRate.Caption = "Pricing Strategy";
            this.gridBandRate.Columns.Add(this.gridColumnRateType);
            this.gridBandRate.Columns.Add(this.gridColumnRate);
            this.gridBandRate.MinWidth = 20;
            this.gridBandRate.Name = "gridBandRate";
            this.gridBandRate.OptionsBand.AllowSize = false;
            this.gridBandRate.OptionsBand.FixedWidth = true;
            this.gridBandRate.Width = 195;
            // 
            // gridBandDelete
            // 
            this.gridBandDelete.Columns.Add(this.gridColumnDelete);
            this.gridBandDelete.MinWidth = 20;
            this.gridBandDelete.Name = "gridBandDelete";
            this.gridBandDelete.OptionsBand.AllowSize = false;
            this.gridBandDelete.OptionsBand.FixedWidth = true;
            this.gridBandDelete.Width = 48;
            // 
            // ScheduleSettingsControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.gridControlProducts);
            this.Controls.Add(this.pnHeader);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "ScheduleSettingsControl";
            this.Size = new System.Drawing.Size(828, 430);
            this.Load += new System.EventHandler(this.ScheduleSettingsControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxProductType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxProductNames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxRateType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditDelete)).EndInit();
            this.pnHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlProducts;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxProductNames;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.Label laScheduleName;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxProductType;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxRateType;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditSize;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditDelete;
        private System.Windows.Forms.Label laProducts;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridViewProducts;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnPosition;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnRateType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnWidth;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnHeight;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnDelete;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnCategory;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnSubCategory;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditRate;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandPosition;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandType;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandName;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandWidth;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandHeight;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandRate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnRate;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandDelete;

    }
}
