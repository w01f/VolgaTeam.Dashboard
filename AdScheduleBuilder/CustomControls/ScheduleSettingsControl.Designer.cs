namespace AdScheduleBuilder.CustomControls
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
            this.gridControlPublications = new DevExpress.XtraGrid.GridControl();
            this.gridViewPublications = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBandPosition = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumnPosition = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridBandPublication = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumnName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumnDailyDelivery = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemSpinEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumnSundayDelivery = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumnDailyReadership = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumnSundayReadership = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBandAbbreviation = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumnAbbreviation = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridBandLogo = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumnLogo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.gridColumnChangeLogo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemButtonEditChangeLogo = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.pnHeader = new System.Windows.Forms.Panel();
            this.laScheduleName = new System.Windows.Forms.Label();
            this.laPublications = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPublications)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPublications)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditChangeLogo)).BeginInit();
            this.pnHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControlPublications
            // 
            this.gridControlPublications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlPublications.Location = new System.Drawing.Point(0, 30);
            this.gridControlPublications.MainView = this.gridViewPublications;
            this.gridControlPublications.Name = "gridControlPublications";
            this.gridControlPublications.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit,
            this.repositoryItemComboBox,
            this.repositoryItemPictureEdit,
            this.repositoryItemSpinEdit,
            this.repositoryItemButtonEditChangeLogo,
            this.repositoryItemTextEdit});
            this.gridControlPublications.Size = new System.Drawing.Size(828, 400);
            this.gridControlPublications.TabIndex = 0;
            this.gridControlPublications.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPublications});
            // 
            // gridViewPublications
            // 
            this.gridViewPublications.Appearance.BandPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewPublications.Appearance.BandPanel.Options.UseFont = true;
            this.gridViewPublications.Appearance.BandPanel.Options.UseTextOptions = true;
            this.gridViewPublications.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewPublications.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewPublications.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewPublications.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewPublications.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewPublications.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewPublications.Appearance.Row.Options.UseFont = true;
            this.gridViewPublications.Appearance.RowSeparator.BackColor = System.Drawing.Color.AliceBlue;
            this.gridViewPublications.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gridViewPublications.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandPosition,
            this.gridBandPublication,
            this.gridBandAbbreviation,
            this.gridBandLogo});
            this.gridViewPublications.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gridColumnPosition,
            this.gridColumnName,
            this.gridColumnLogo,
            this.gridColumnAbbreviation,
            this.gridColumnDailyDelivery,
            this.gridColumnSundayDelivery,
            this.gridColumnDailyReadership,
            this.gridColumnSundayReadership,
            this.gridColumnChangeLogo});
            this.gridViewPublications.GridControl = this.gridControlPublications;
            this.gridViewPublications.Name = "gridViewPublications";
            this.gridViewPublications.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewPublications.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewPublications.OptionsBehavior.AutoPopulateColumns = false;
            this.gridViewPublications.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gridViewPublications.OptionsCustomization.AllowFilter = false;
            this.gridViewPublications.OptionsCustomization.AllowGroup = false;
            this.gridViewPublications.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewPublications.OptionsCustomization.AllowSort = false;
            this.gridViewPublications.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridViewPublications.OptionsFilter.AllowFilterEditor = false;
            this.gridViewPublications.OptionsFilter.AllowMRUFilterList = false;
            this.gridViewPublications.OptionsMenu.EnableColumnMenu = false;
            this.gridViewPublications.OptionsMenu.EnableFooterMenu = false;
            this.gridViewPublications.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridViewPublications.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gridViewPublications.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gridViewPublications.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridViewPublications.OptionsSelection.UseIndicatorForSelection = false;
            this.gridViewPublications.OptionsView.ColumnAutoWidth = true;
            this.gridViewPublications.OptionsView.ShowBands = false;
            this.gridViewPublications.OptionsView.ShowDetailButtons = false;
            this.gridViewPublications.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewPublications.OptionsView.ShowGroupPanel = false;
            this.gridViewPublications.OptionsView.ShowIndicator = false;
            this.gridViewPublications.RowHeight = 25;
            this.gridViewPublications.RowSeparatorHeight = 10;
            this.gridViewPublications.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewPublications_CellValueChanged);
            // 
            // gridBandPosition
            // 
            this.gridBandPosition.Caption = "Position";
            this.gridBandPosition.Columns.Add(this.gridColumnPosition);
            this.gridBandPosition.Name = "gridBandPosition";
            this.gridBandPosition.OptionsBand.AllowMove = false;
            this.gridBandPosition.OptionsBand.AllowSize = false;
            this.gridBandPosition.OptionsBand.FixedWidth = true;
            this.gridBandPosition.Width = 120;
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
            this.gridColumnPosition.RowCount = 2;
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
            // gridBandPublication
            // 
            this.gridBandPublication.Caption = "Publication";
            this.gridBandPublication.Columns.Add(this.gridColumnName);
            this.gridBandPublication.Columns.Add(this.gridColumnDailyDelivery);
            this.gridBandPublication.Columns.Add(this.gridColumnSundayDelivery);
            this.gridBandPublication.Columns.Add(this.gridColumnDailyReadership);
            this.gridBandPublication.Columns.Add(this.gridColumnSundayReadership);
            this.gridBandPublication.Name = "gridBandPublication";
            this.gridBandPublication.Width = 679;
            // 
            // gridColumnName
            // 
            this.gridColumnName.AppearanceCell.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridColumnName.AppearanceCell.Options.UseFont = true;
            this.gridColumnName.Caption = "Publication";
            this.gridColumnName.ColumnEdit = this.repositoryItemComboBox;
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.RowCount = 2;
            this.gridColumnName.Visible = true;
            this.gridColumnName.Width = 679;
            // 
            // repositoryItemComboBox
            // 
            this.repositoryItemComboBox.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repositoryItemComboBox.Appearance.Options.UseFont = true;
            this.repositoryItemComboBox.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repositoryItemComboBox.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBox.AppearanceFocused.Font = new System.Drawing.Font("Arial", 12F);
            this.repositoryItemComboBox.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBox.AutoHeight = false;
            this.repositoryItemComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox.Name = "repositoryItemComboBox";
            this.repositoryItemComboBox.NullText = "Click here to select publication";
            this.repositoryItemComboBox.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.repositoryItemComboBox_Closed);
            // 
            // gridColumnDailyDelivery
            // 
            this.gridColumnDailyDelivery.Caption = "Daily Delivery";
            this.gridColumnDailyDelivery.ColumnEdit = this.repositoryItemSpinEdit;
            this.gridColumnDailyDelivery.FieldName = "DailyDelivery";
            this.gridColumnDailyDelivery.Name = "gridColumnDailyDelivery";
            this.gridColumnDailyDelivery.RowIndex = 1;
            this.gridColumnDailyDelivery.Width = 86;
            // 
            // repositoryItemSpinEdit
            // 
            this.repositoryItemSpinEdit.AutoHeight = false;
            this.repositoryItemSpinEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit.DisplayFormat.FormatString = "#,##0";
            this.repositoryItemSpinEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEdit.IsFloatValue = false;
            this.repositoryItemSpinEdit.Mask.EditMask = "N00";
            this.repositoryItemSpinEdit.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.repositoryItemSpinEdit.Name = "repositoryItemSpinEdit";
            this.repositoryItemSpinEdit.NullText = "N/A";
            // 
            // gridColumnSundayDelivery
            // 
            this.gridColumnSundayDelivery.Caption = "Sunday Delivery";
            this.gridColumnSundayDelivery.ColumnEdit = this.repositoryItemSpinEdit;
            this.gridColumnSundayDelivery.FieldName = "SundayDelivery";
            this.gridColumnSundayDelivery.Name = "gridColumnSundayDelivery";
            this.gridColumnSundayDelivery.RowIndex = 1;
            this.gridColumnSundayDelivery.Width = 89;
            // 
            // gridColumnDailyReadership
            // 
            this.gridColumnDailyReadership.Caption = "Daily Readership";
            this.gridColumnDailyReadership.ColumnEdit = this.repositoryItemSpinEdit;
            this.gridColumnDailyReadership.FieldName = "DailyReadership";
            this.gridColumnDailyReadership.Name = "gridColumnDailyReadership";
            this.gridColumnDailyReadership.RowIndex = 1;
            this.gridColumnDailyReadership.Width = 88;
            // 
            // gridColumnSundayReadership
            // 
            this.gridColumnSundayReadership.Caption = "Sunday Readership";
            this.gridColumnSundayReadership.ColumnEdit = this.repositoryItemSpinEdit;
            this.gridColumnSundayReadership.FieldName = "SundayReadership";
            this.gridColumnSundayReadership.Name = "gridColumnSundayReadership";
            this.gridColumnSundayReadership.RowIndex = 1;
            this.gridColumnSundayReadership.Width = 116;
            // 
            // gridBandAbbreviation
            // 
            this.gridBandAbbreviation.Caption = "Code";
            this.gridBandAbbreviation.Columns.Add(this.gridColumnAbbreviation);
            this.gridBandAbbreviation.Name = "gridBandAbbreviation";
            this.gridBandAbbreviation.OptionsBand.AllowMove = false;
            this.gridBandAbbreviation.OptionsBand.AllowSize = false;
            this.gridBandAbbreviation.OptionsBand.FixedWidth = true;
            this.gridBandAbbreviation.Visible = false;
            this.gridBandAbbreviation.Width = 114;
            // 
            // gridColumnAbbreviation
            // 
            this.gridColumnAbbreviation.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnAbbreviation.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnAbbreviation.Caption = "Code";
            this.gridColumnAbbreviation.ColumnEdit = this.repositoryItemTextEdit;
            this.gridColumnAbbreviation.FieldName = "Abbreviation";
            this.gridColumnAbbreviation.Name = "gridColumnAbbreviation";
            this.gridColumnAbbreviation.RowCount = 2;
            this.gridColumnAbbreviation.Visible = true;
            this.gridColumnAbbreviation.Width = 114;
            // 
            // repositoryItemTextEdit
            // 
            this.repositoryItemTextEdit.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.repositoryItemTextEdit.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit.Appearance.Options.UseTextOptions = true;
            this.repositoryItemTextEdit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTextEdit.AppearanceFocused.Font = new System.Drawing.Font("Arial", 10F);
            this.repositoryItemTextEdit.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemTextEdit.AppearanceFocused.Options.UseTextOptions = true;
            this.repositoryItemTextEdit.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTextEdit.AutoHeight = false;
            this.repositoryItemTextEdit.Name = "repositoryItemTextEdit";
            // 
            // gridBandLogo
            // 
            this.gridBandLogo.Caption = "Logo";
            this.gridBandLogo.Columns.Add(this.gridColumnLogo);
            this.gridBandLogo.Columns.Add(this.gridColumnChangeLogo);
            this.gridBandLogo.Name = "gridBandLogo";
            this.gridBandLogo.OptionsBand.AllowMove = false;
            this.gridBandLogo.OptionsBand.AllowSize = false;
            this.gridBandLogo.OptionsBand.FixedWidth = true;
            this.gridBandLogo.Visible = false;
            this.gridBandLogo.Width = 170;
            // 
            // gridColumnLogo
            // 
            this.gridColumnLogo.Caption = "Logo";
            this.gridColumnLogo.ColumnEdit = this.repositoryItemPictureEdit;
            this.gridColumnLogo.FieldName = "SmallLogo";
            this.gridColumnLogo.Name = "gridColumnLogo";
            this.gridColumnLogo.OptionsColumn.AllowEdit = false;
            this.gridColumnLogo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumnLogo.OptionsColumn.AllowSize = false;
            this.gridColumnLogo.OptionsColumn.FixedWidth = true;
            this.gridColumnLogo.OptionsColumn.ReadOnly = true;
            this.gridColumnLogo.RowCount = 2;
            this.gridColumnLogo.Visible = true;
            this.gridColumnLogo.Width = 120;
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
            this.repositoryItemPictureEdit.CustomHeight = 50;
            this.repositoryItemPictureEdit.Name = "repositoryItemPictureEdit";
            this.repositoryItemPictureEdit.NullText = "No Logo";
            this.repositoryItemPictureEdit.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.Image;
            this.repositoryItemPictureEdit.ReadOnly = true;
            this.repositoryItemPictureEdit.ShowMenu = false;
            this.repositoryItemPictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            // 
            // gridColumnChangeLogo
            // 
            this.gridColumnChangeLogo.Caption = "Change Logo";
            this.gridColumnChangeLogo.ColumnEdit = this.repositoryItemButtonEditChangeLogo;
            this.gridColumnChangeLogo.Name = "gridColumnChangeLogo";
            this.gridColumnChangeLogo.OptionsColumn.ShowCaption = false;
            this.gridColumnChangeLogo.RowCount = 2;
            this.gridColumnChangeLogo.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridColumnChangeLogo.Visible = true;
            this.gridColumnChangeLogo.Width = 50;
            // 
            // repositoryItemButtonEditChangeLogo
            // 
            this.repositoryItemButtonEditChangeLogo.AutoHeight = false;
            this.repositoryItemButtonEditChangeLogo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "Browse for a logo", null, null, true)});
            this.repositoryItemButtonEditChangeLogo.Name = "repositoryItemButtonEditChangeLogo";
            this.repositoryItemButtonEditChangeLogo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEditChangeLogo.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditChangeLogo_ButtonClick);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // pnHeader
            // 
            this.pnHeader.Controls.Add(this.laScheduleName);
            this.pnHeader.Controls.Add(this.laPublications);
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
            // laPublications
            // 
            this.laPublications.Dock = System.Windows.Forms.DockStyle.Left;
            this.laPublications.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laPublications.Location = new System.Drawing.Point(0, 0);
            this.laPublications.Name = "laPublications";
            this.laPublications.Size = new System.Drawing.Size(272, 30);
            this.laPublications.TabIndex = 1;
            this.laPublications.Text = "Publications:";
            this.laPublications.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ScheduleSettingsControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.gridControlPublications);
            this.Controls.Add(this.pnHeader);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "ScheduleSettingsControl";
            this.Size = new System.Drawing.Size(828, 430);
            this.Load += new System.EventHandler(this.ScheduleSettingsControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPublications)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPublications)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditChangeLogo)).EndInit();
            this.pnHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlPublications;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.Label laScheduleName;
        private System.Windows.Forms.Label laPublications;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnPosition;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnAbbreviation;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnDailyDelivery;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnSundayDelivery;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnDailyReadership;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnSundayReadership;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnLogo;
        public DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView gridViewPublications;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumnChangeLogo;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditChangeLogo;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandPosition;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandPublication;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandAbbreviation;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandLogo;

    }
}
