namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	abstract partial class WebPackageControl
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
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
			this.buttonXInfo = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCPM = new DevComponents.DotNetBar.ButtonX();
			this.buttonXScreenshot = new DevComponents.DotNetBar.ButtonX();
			this.buttonXComments = new DevComponents.DotNetBar.ButtonX();
			this.buttonXRate = new DevComponents.DotNetBar.ButtonX();
			this.buttonXInvestment = new DevComponents.DotNetBar.ButtonX();
			this.buttonXImpressions = new DevComponents.DotNetBar.ButtonX();
			this.buttonXProduct = new DevComponents.DotNetBar.ButtonX();
			this.buttonXGroup = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCategory = new DevComponents.DotNetBar.ButtonX();
			this.gridControl = new DevExpress.XtraGrid.GridControl();
			this.advBandedGridView = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
			this.gridBandId = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnId = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandProduct = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnCategory = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumnGroup = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.bandedGridColumnProduct = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandInfo = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnInfo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandComments = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnComments = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandRate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnImpressions = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditImpressions = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.bandedGridColumnCPM = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.repositoryItemSpinEditInvestment = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.bandedGridColumnRate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.gridBandInvestment = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
			this.bandedGridColumnInvestment = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.hyperLinkEditReset = new DevExpress.XtraEditors.HyperLinkEdit();
			this.laAdvertiser = new System.Windows.Forms.Label();
			this.repositoryItemMemoEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
			this.splitContainerControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditImpressions)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditInvestment)).BeginInit();
			this.pnHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditReset.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit)).BeginInit();
			this.SuspendLayout();
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
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
			// 
			// splitContainerControl
			// 
			this.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl.Name = "splitContainerControl";
			this.splitContainerControl.Panel1.Controls.Add(this.buttonXInfo);
			this.splitContainerControl.Panel1.Controls.Add(this.buttonXCPM);
			this.splitContainerControl.Panel1.Controls.Add(this.buttonXScreenshot);
			this.splitContainerControl.Panel1.Controls.Add(this.buttonXComments);
			this.splitContainerControl.Panel1.Controls.Add(this.buttonXRate);
			this.splitContainerControl.Panel1.Controls.Add(this.buttonXInvestment);
			this.splitContainerControl.Panel1.Controls.Add(this.buttonXImpressions);
			this.splitContainerControl.Panel1.Controls.Add(this.buttonXProduct);
			this.splitContainerControl.Panel1.Controls.Add(this.buttonXGroup);
			this.splitContainerControl.Panel1.Controls.Add(this.buttonXCategory);
			this.splitContainerControl.Panel1.MinSize = 230;
			this.splitContainerControl.Panel1.Text = "Panel1";
			this.splitContainerControl.Panel2.Controls.Add(this.gridControl);
			this.splitContainerControl.Panel2.Controls.Add(this.pnHeader);
			this.splitContainerControl.Panel2.Text = "Panel2";
			this.splitContainerControl.Size = new System.Drawing.Size(1000, 513);
			this.splitContainerControl.SplitterPosition = 230;
			this.splitContainerControl.TabIndex = 101;
			this.splitContainerControl.Text = "splitContainerControl1";
			// 
			// buttonXInfo
			// 
			this.buttonXInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXInfo.AutoCheckOnClick = true;
			this.buttonXInfo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXInfo.Location = new System.Drawing.Point(126, 155);
			this.buttonXInfo.Name = "buttonXInfo";
			this.buttonXInfo.Size = new System.Drawing.Size(95, 27);
			this.buttonXInfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXInfo.TabIndex = 48;
			this.buttonXInfo.Text = "Schedule Info";
			this.buttonXInfo.TextColor = System.Drawing.Color.Black;
			this.buttonXInfo.CheckedChanged += new System.EventHandler(this.TogledButton_CheckedChanged);
			// 
			// buttonXCPM
			// 
			this.buttonXCPM.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCPM.AutoCheckOnClick = true;
			this.buttonXCPM.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCPM.Location = new System.Drawing.Point(12, 106);
			this.buttonXCPM.Name = "buttonXCPM";
			this.buttonXCPM.Size = new System.Drawing.Size(95, 27);
			this.buttonXCPM.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCPM.TabIndex = 47;
			this.buttonXCPM.Text = "CPM";
			this.buttonXCPM.TextColor = System.Drawing.Color.Black;
			this.buttonXCPM.CheckedChanged += new System.EventHandler(this.TogledButton_CheckedChanged);
			// 
			// buttonXScreenshot
			// 
			this.buttonXScreenshot.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXScreenshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXScreenshot.AutoCheckOnClick = true;
			this.buttonXScreenshot.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXScreenshot.Location = new System.Drawing.Point(126, 204);
			this.buttonXScreenshot.Name = "buttonXScreenshot";
			this.buttonXScreenshot.Size = new System.Drawing.Size(95, 27);
			this.buttonXScreenshot.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXScreenshot.TabIndex = 46;
			this.buttonXScreenshot.Text = "Screenshot";
			this.buttonXScreenshot.TextColor = System.Drawing.Color.Black;
			this.buttonXScreenshot.CheckedChanged += new System.EventHandler(this.TogledButton_CheckedChanged);
			// 
			// buttonXComments
			// 
			this.buttonXComments.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXComments.AutoCheckOnClick = true;
			this.buttonXComments.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXComments.Location = new System.Drawing.Point(12, 204);
			this.buttonXComments.Name = "buttonXComments";
			this.buttonXComments.Size = new System.Drawing.Size(95, 27);
			this.buttonXComments.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXComments.TabIndex = 45;
			this.buttonXComments.Text = "Comments";
			this.buttonXComments.TextColor = System.Drawing.Color.Black;
			this.buttonXComments.CheckedChanged += new System.EventHandler(this.TogledButton_CheckedChanged);
			// 
			// buttonXRate
			// 
			this.buttonXRate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXRate.AutoCheckOnClick = true;
			this.buttonXRate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRate.Location = new System.Drawing.Point(126, 106);
			this.buttonXRate.Name = "buttonXRate";
			this.buttonXRate.Size = new System.Drawing.Size(95, 27);
			this.buttonXRate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRate.TabIndex = 43;
			this.buttonXRate.Text = "Rate";
			this.buttonXRate.TextColor = System.Drawing.Color.Black;
			this.buttonXRate.CheckedChanged += new System.EventHandler(this.TogledButton_CheckedChanged);
			// 
			// buttonXInvestment
			// 
			this.buttonXInvestment.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXInvestment.AutoCheckOnClick = true;
			this.buttonXInvestment.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXInvestment.Location = new System.Drawing.Point(12, 155);
			this.buttonXInvestment.Name = "buttonXInvestment";
			this.buttonXInvestment.Size = new System.Drawing.Size(95, 27);
			this.buttonXInvestment.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXInvestment.TabIndex = 42;
			this.buttonXInvestment.Text = "Investment";
			this.buttonXInvestment.TextColor = System.Drawing.Color.Black;
			this.buttonXInvestment.CheckedChanged += new System.EventHandler(this.TogledButton_CheckedChanged);
			// 
			// buttonXImpressions
			// 
			this.buttonXImpressions.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXImpressions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXImpressions.AutoCheckOnClick = true;
			this.buttonXImpressions.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXImpressions.Location = new System.Drawing.Point(126, 57);
			this.buttonXImpressions.Name = "buttonXImpressions";
			this.buttonXImpressions.Size = new System.Drawing.Size(95, 27);
			this.buttonXImpressions.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXImpressions.TabIndex = 41;
			this.buttonXImpressions.Text = "Impressions";
			this.buttonXImpressions.TextColor = System.Drawing.Color.Black;
			this.buttonXImpressions.CheckedChanged += new System.EventHandler(this.TogledButton_CheckedChanged);
			// 
			// buttonXProduct
			// 
			this.buttonXProduct.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXProduct.AutoCheckOnClick = true;
			this.buttonXProduct.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXProduct.Location = new System.Drawing.Point(12, 57);
			this.buttonXProduct.Name = "buttonXProduct";
			this.buttonXProduct.Size = new System.Drawing.Size(95, 27);
			this.buttonXProduct.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXProduct.TabIndex = 40;
			this.buttonXProduct.Text = "Product";
			this.buttonXProduct.TextColor = System.Drawing.Color.Black;
			this.buttonXProduct.CheckedChanged += new System.EventHandler(this.TogledButton_CheckedChanged);
			// 
			// buttonXGroup
			// 
			this.buttonXGroup.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXGroup.AutoCheckOnClick = true;
			this.buttonXGroup.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXGroup.Location = new System.Drawing.Point(126, 8);
			this.buttonXGroup.Name = "buttonXGroup";
			this.buttonXGroup.Size = new System.Drawing.Size(95, 27);
			this.buttonXGroup.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXGroup.TabIndex = 39;
			this.buttonXGroup.Text = "Group";
			this.buttonXGroup.TextColor = System.Drawing.Color.Black;
			this.buttonXGroup.CheckedChanged += new System.EventHandler(this.TogledButton_CheckedChanged);
			// 
			// buttonXCategory
			// 
			this.buttonXCategory.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCategory.AutoCheckOnClick = true;
			this.buttonXCategory.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCategory.Location = new System.Drawing.Point(12, 8);
			this.buttonXCategory.Name = "buttonXCategory";
			this.buttonXCategory.Size = new System.Drawing.Size(95, 27);
			this.buttonXCategory.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCategory.TabIndex = 38;
			this.buttonXCategory.Text = "Category";
			this.buttonXCategory.TextColor = System.Drawing.Color.Black;
			this.buttonXCategory.CheckedChanged += new System.EventHandler(this.TogledButton_CheckedChanged);
			// 
			// gridControl
			// 
			this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControl.Location = new System.Drawing.Point(0, 30);
			this.gridControl.MainView = this.advBandedGridView;
			this.gridControl.Name = "gridControl";
			this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEditImpressions,
            this.repositoryItemSpinEditInvestment,
            this.repositoryItemMemoEdit});
			this.gridControl.Size = new System.Drawing.Size(764, 483);
			this.gridControl.TabIndex = 4;
			this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView});
			// 
			// advBandedGridView
			// 
			this.advBandedGridView.Appearance.BandPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.advBandedGridView.Appearance.BandPanel.Options.UseFont = true;
			this.advBandedGridView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.EvenRow.Options.UseFont = true;
			this.advBandedGridView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.FocusedCell.Options.UseFont = true;
			this.advBandedGridView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.FocusedRow.Options.UseFont = true;
			this.advBandedGridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.advBandedGridView.Appearance.HeaderPanel.Options.UseFont = true;
			this.advBandedGridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.advBandedGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.advBandedGridView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.OddRow.Options.UseFont = true;
			this.advBandedGridView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.Row.Options.UseFont = true;
			this.advBandedGridView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.advBandedGridView.Appearance.SelectedRow.Options.UseFont = true;
			this.advBandedGridView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandId,
            this.gridBandProduct,
            this.gridBandInfo,
            this.gridBandComments,
            this.gridBandRate,
            this.gridBandInvestment});
			this.advBandedGridView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumnId,
            this.bandedGridColumnCategory,
            this.bandedGridColumnGroup,
            this.bandedGridColumnProduct,
            this.bandedGridColumnInfo,
            this.bandedGridColumnComments,
            this.bandedGridColumnImpressions,
            this.bandedGridColumnCPM,
            this.bandedGridColumnRate,
            this.bandedGridColumnInvestment});
			this.advBandedGridView.GridControl = this.gridControl;
			this.advBandedGridView.Name = "advBandedGridView";
			this.advBandedGridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.advBandedGridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.advBandedGridView.OptionsBehavior.AutoPopulateColumns = false;
			this.advBandedGridView.OptionsBehavior.AutoUpdateTotalSummary = false;
			this.advBandedGridView.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
			this.advBandedGridView.OptionsCustomization.AllowFilter = false;
			this.advBandedGridView.OptionsCustomization.AllowGroup = false;
			this.advBandedGridView.OptionsCustomization.AllowQuickHideColumns = false;
			this.advBandedGridView.OptionsCustomization.AllowSort = false;
			this.advBandedGridView.OptionsCustomization.ShowBandsInCustomizationForm = false;
			this.advBandedGridView.OptionsMenu.EnableColumnMenu = false;
			this.advBandedGridView.OptionsMenu.EnableFooterMenu = false;
			this.advBandedGridView.OptionsMenu.EnableGroupPanelMenu = false;
			this.advBandedGridView.OptionsMenu.ShowAutoFilterRowItem = false;
			this.advBandedGridView.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.advBandedGridView.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.advBandedGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.advBandedGridView.OptionsSelection.EnableAppearanceHideSelection = false;
			this.advBandedGridView.OptionsView.ColumnAutoWidth = true;
			this.advBandedGridView.OptionsView.EnableAppearanceEvenRow = true;
			this.advBandedGridView.OptionsView.EnableAppearanceOddRow = true;
			this.advBandedGridView.OptionsView.ShowBands = false;
			this.advBandedGridView.OptionsView.ShowDetailButtons = false;
			this.advBandedGridView.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.advBandedGridView.OptionsView.ShowGroupPanel = false;
			this.advBandedGridView.OptionsView.ShowIndicator = false;
			this.advBandedGridView.RowSeparatorHeight = 10;
			this.advBandedGridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.advBandedGridView_CellValueChanged);
			// 
			// gridBandId
			// 
			this.gridBandId.Caption = "ID";
			this.gridBandId.Columns.Add(this.bandedGridColumnId);
			this.gridBandId.Name = "gridBandId";
			this.gridBandId.OptionsBand.FixedWidth = true;
			this.gridBandId.Width = 53;
			// 
			// bandedGridColumnId
			// 
			this.bandedGridColumnId.AppearanceCell.Options.UseTextOptions = true;
			this.bandedGridColumnId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.bandedGridColumnId.Caption = "ID";
			this.bandedGridColumnId.FieldName = "Parent.Index";
			this.bandedGridColumnId.Name = "bandedGridColumnId";
			this.bandedGridColumnId.OptionsColumn.AllowEdit = false;
			this.bandedGridColumnId.OptionsColumn.ReadOnly = true;
			this.bandedGridColumnId.RowCount = 3;
			this.bandedGridColumnId.Visible = true;
			this.bandedGridColumnId.Width = 53;
			// 
			// gridBandProduct
			// 
			this.gridBandProduct.Caption = "Product";
			this.gridBandProduct.Columns.Add(this.bandedGridColumnCategory);
			this.gridBandProduct.Columns.Add(this.bandedGridColumnGroup);
			this.gridBandProduct.Columns.Add(this.bandedGridColumnProduct);
			this.gridBandProduct.Name = "gridBandProduct";
			this.gridBandProduct.Width = 591;
			// 
			// bandedGridColumnCategory
			// 
			this.bandedGridColumnCategory.Caption = "Digital Category";
			this.bandedGridColumnCategory.FieldName = "Category";
			this.bandedGridColumnCategory.Name = "bandedGridColumnCategory";
			this.bandedGridColumnCategory.Visible = true;
			this.bandedGridColumnCategory.Width = 591;
			// 
			// bandedGridColumnGroup
			// 
			this.bandedGridColumnGroup.Caption = "Ad Group";
			this.bandedGridColumnGroup.FieldName = "SubCategory";
			this.bandedGridColumnGroup.Name = "bandedGridColumnGroup";
			this.bandedGridColumnGroup.RowIndex = 1;
			this.bandedGridColumnGroup.Visible = true;
			this.bandedGridColumnGroup.Width = 591;
			// 
			// bandedGridColumnProduct
			// 
			this.bandedGridColumnProduct.Caption = "Specific Digital Product";
			this.bandedGridColumnProduct.FieldName = "Name";
			this.bandedGridColumnProduct.Name = "bandedGridColumnProduct";
			this.bandedGridColumnProduct.RowIndex = 2;
			this.bandedGridColumnProduct.Visible = true;
			this.bandedGridColumnProduct.Width = 591;
			// 
			// gridBandInfo
			// 
			this.gridBandInfo.Caption = "Info";
			this.gridBandInfo.Columns.Add(this.bandedGridColumnInfo);
			this.gridBandInfo.Name = "gridBandInfo";
			this.gridBandInfo.Width = 435;
			// 
			// bandedGridColumnInfo
			// 
			this.bandedGridColumnInfo.Caption = "Schedule & Product Info";
			this.bandedGridColumnInfo.ColumnEdit = this.repositoryItemMemoEdit;
			this.bandedGridColumnInfo.FieldName = "Info";
			this.bandedGridColumnInfo.Name = "bandedGridColumnInfo";
			this.bandedGridColumnInfo.RowCount = 3;
			this.bandedGridColumnInfo.Visible = true;
			this.bandedGridColumnInfo.Width = 435;
			// 
			// gridBandComments
			// 
			this.gridBandComments.Caption = "Comments";
			this.gridBandComments.Columns.Add(this.bandedGridColumnComments);
			this.gridBandComments.Name = "gridBandComments";
			this.gridBandComments.Width = 427;
			// 
			// bandedGridColumnComments
			// 
			this.bandedGridColumnComments.Caption = "Notes & Comments";
			this.bandedGridColumnComments.ColumnEdit = this.repositoryItemMemoEdit;
			this.bandedGridColumnComments.FieldName = "Comments";
			this.bandedGridColumnComments.Name = "bandedGridColumnComments";
			this.bandedGridColumnComments.RowCount = 3;
			this.bandedGridColumnComments.Visible = true;
			this.bandedGridColumnComments.Width = 427;
			// 
			// gridBandRate
			// 
			this.gridBandRate.Caption = "Rate";
			this.gridBandRate.Columns.Add(this.bandedGridColumnImpressions);
			this.gridBandRate.Columns.Add(this.bandedGridColumnCPM);
			this.gridBandRate.Columns.Add(this.bandedGridColumnRate);
			this.gridBandRate.Name = "gridBandRate";
			this.gridBandRate.OptionsBand.FixedWidth = true;
			this.gridBandRate.Width = 131;
			// 
			// bandedGridColumnImpressions
			// 
			this.bandedGridColumnImpressions.Caption = "Impressions";
			this.bandedGridColumnImpressions.ColumnEdit = this.repositoryItemSpinEditImpressions;
			this.bandedGridColumnImpressions.FieldName = "Impressions";
			this.bandedGridColumnImpressions.Name = "bandedGridColumnImpressions";
			this.bandedGridColumnImpressions.Visible = true;
			this.bandedGridColumnImpressions.Width = 131;
			// 
			// repositoryItemSpinEditImpressions
			// 
			this.repositoryItemSpinEditImpressions.AutoHeight = false;
			this.repositoryItemSpinEditImpressions.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.repositoryItemSpinEditImpressions.DisplayFormat.FormatString = "#,##0";
			this.repositoryItemSpinEditImpressions.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditImpressions.EditFormat.FormatString = "#,##0";
			this.repositoryItemSpinEditImpressions.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditImpressions.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.repositoryItemSpinEditImpressions.IsFloatValue = false;
			this.repositoryItemSpinEditImpressions.Mask.EditMask = "N00";
			this.repositoryItemSpinEditImpressions.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.repositoryItemSpinEditImpressions.Name = "repositoryItemSpinEditImpressions";
			// 
			// bandedGridColumnCPM
			// 
			this.bandedGridColumnCPM.Caption = "CPM";
			this.bandedGridColumnCPM.ColumnEdit = this.repositoryItemSpinEditInvestment;
			this.bandedGridColumnCPM.FieldName = "CPM";
			this.bandedGridColumnCPM.Name = "bandedGridColumnCPM";
			this.bandedGridColumnCPM.RowIndex = 1;
			this.bandedGridColumnCPM.Visible = true;
			this.bandedGridColumnCPM.Width = 131;
			// 
			// repositoryItemSpinEditInvestment
			// 
			this.repositoryItemSpinEditInvestment.AutoHeight = false;
			this.repositoryItemSpinEditInvestment.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.repositoryItemSpinEditInvestment.DisplayFormat.FormatString = "$#,###.00";
			this.repositoryItemSpinEditInvestment.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditInvestment.EditFormat.FormatString = "$#,###.00";
			this.repositoryItemSpinEditInvestment.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemSpinEditInvestment.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.repositoryItemSpinEditInvestment.Name = "repositoryItemSpinEditInvestment";
			// 
			// bandedGridColumnRate
			// 
			this.bandedGridColumnRate.Caption = "Rate";
			this.bandedGridColumnRate.ColumnEdit = this.repositoryItemSpinEditInvestment;
			this.bandedGridColumnRate.FieldName = "Rate";
			this.bandedGridColumnRate.Name = "bandedGridColumnRate";
			this.bandedGridColumnRate.RowIndex = 2;
			this.bandedGridColumnRate.Visible = true;
			this.bandedGridColumnRate.Width = 131;
			// 
			// gridBandInvestment
			// 
			this.gridBandInvestment.Caption = "Investment";
			this.gridBandInvestment.Columns.Add(this.bandedGridColumnInvestment);
			this.gridBandInvestment.Name = "gridBandInvestment";
			this.gridBandInvestment.OptionsBand.FixedWidth = true;
			this.gridBandInvestment.Width = 109;
			// 
			// bandedGridColumnInvestment
			// 
			this.bandedGridColumnInvestment.Caption = "Investment";
			this.bandedGridColumnInvestment.ColumnEdit = this.repositoryItemSpinEditInvestment;
			this.bandedGridColumnInvestment.FieldName = "Investment";
			this.bandedGridColumnInvestment.Name = "bandedGridColumnInvestment";
			this.bandedGridColumnInvestment.RowCount = 3;
			this.bandedGridColumnInvestment.Visible = true;
			this.bandedGridColumnInvestment.Width = 109;
			// 
			// pnHeader
			// 
			this.pnHeader.Controls.Add(this.hyperLinkEditReset);
			this.pnHeader.Controls.Add(this.laAdvertiser);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(764, 30);
			this.pnHeader.TabIndex = 3;
			// 
			// hyperLinkEditReset
			// 
			this.hyperLinkEditReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.hyperLinkEditReset.EditValue = "Reset";
			this.hyperLinkEditReset.Location = new System.Drawing.Point(697, 4);
			this.hyperLinkEditReset.Name = "hyperLinkEditReset";
			this.hyperLinkEditReset.Properties.AllowFocused = false;
			this.hyperLinkEditReset.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.hyperLinkEditReset.Properties.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.hyperLinkEditReset.Properties.Appearance.ForeColor = System.Drawing.Color.DarkBlue;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseBackColor = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseFont = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseForeColor = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseTextOptions = true;
			this.hyperLinkEditReset.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.hyperLinkEditReset.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.hyperLinkEditReset.Size = new System.Drawing.Size(64, 22);
			this.hyperLinkEditReset.TabIndex = 102;
			this.hyperLinkEditReset.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hyperLinkEditReset_OpenLink);
			// 
			// laAdvertiser
			// 
			this.laAdvertiser.Dock = System.Windows.Forms.DockStyle.Left;
			this.laAdvertiser.Location = new System.Drawing.Point(0, 0);
			this.laAdvertiser.Name = "laAdvertiser";
			this.laAdvertiser.Size = new System.Drawing.Size(300, 30);
			this.laAdvertiser.TabIndex = 2;
			this.laAdvertiser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// repositoryItemMemoEdit
			// 
			this.repositoryItemMemoEdit.Appearance.Options.UseTextOptions = true;
			this.repositoryItemMemoEdit.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.repositoryItemMemoEdit.AppearanceFocused.Options.UseTextOptions = true;
			this.repositoryItemMemoEdit.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.repositoryItemMemoEdit.Name = "repositoryItemMemoEdit";
			// 
			// WebPackageControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.splitContainerControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "WebPackageControl";
			this.Size = new System.Drawing.Size(1000, 513);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
			this.splitContainerControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.advBandedGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditImpressions)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditInvestment)).EndInit();
			this.pnHeader.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditReset.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
		private DevComponents.DotNetBar.ButtonX buttonXInfo;
		private DevComponents.DotNetBar.ButtonX buttonXCPM;
		private DevComponents.DotNetBar.ButtonX buttonXScreenshot;
		private DevComponents.DotNetBar.ButtonX buttonXComments;
		private DevComponents.DotNetBar.ButtonX buttonXRate;
		private DevComponents.DotNetBar.ButtonX buttonXInvestment;
		private DevComponents.DotNetBar.ButtonX buttonXImpressions;
		private DevComponents.DotNetBar.ButtonX buttonXProduct;
		private DevComponents.DotNetBar.ButtonX buttonXCategory;
		public DevComponents.DotNetBar.ButtonX buttonXGroup;
		private System.Windows.Forms.Panel pnHeader;
		private System.Windows.Forms.Label laAdvertiser;
		private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEditReset;
		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandId;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnId;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandProduct;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnCategory;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnGroup;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnProduct;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandInfo;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnInfo;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandComments;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnComments;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandRate;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnImpressions;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnCPM;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnRate;
		private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandInvestment;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnInvestment;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditImpressions;
		private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditInvestment;
		private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit;

    }
}
