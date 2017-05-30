namespace Asa.Dashboard.TabHomeForms
{
	partial class FormSavedStates
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSavedStates));
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			this.laTitle = new System.Windows.Forms.Label();
			this.buttonXLoad = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.gridControlFiles = new DevExpress.XtraGrid.GridControl();
			this.gridViewFiles = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnFilesName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnFilesButtons = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditFiles = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageFiles = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageTemplates = new DevExpress.XtraTab.XtraTabPage();
			this.gridControlTemplates = new DevExpress.XtraGrid.GridControl();
			this.gridViewTemplates = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnTemplatesName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnTemplatesButtons = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEditTemplates = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.xtraTabControl.SuspendLayout();
			this.xtraTabPageFiles.SuspendLayout();
			this.xtraTabPageTemplates.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlTemplates)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewTemplates)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditTemplates)).BeginInit();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.BackColor = System.Drawing.Color.White;
			this.laTitle.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.Black;
			this.laTitle.Location = new System.Drawing.Point(12, 9);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(599, 55);
			this.laTitle.TabIndex = 9;
			this.laTitle.Text = "Select Saved State";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonXLoad
			// 
			this.buttonXLoad.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXLoad.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXLoad.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXLoad.Location = new System.Drawing.Point(146, 428);
			this.buttonXLoad.Name = "buttonXLoad";
			this.buttonXLoad.Size = new System.Drawing.Size(125, 38);
			this.buttonXLoad.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXLoad.TabIndex = 10;
			this.buttonXLoad.Text = "Load";
			this.buttonXLoad.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(351, 428);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(125, 38);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 11;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// gridControlFiles
			// 
			this.gridControlFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlFiles.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControlFiles.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControlFiles.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControlFiles.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			this.gridControlFiles.Location = new System.Drawing.Point(0, 0);
			this.gridControlFiles.MainView = this.gridViewFiles;
			this.gridControlFiles.Name = "gridControlFiles";
			this.gridControlFiles.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditFiles});
			this.gridControlFiles.Size = new System.Drawing.Size(593, 324);
			this.gridControlFiles.TabIndex = 18;
			this.gridControlFiles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewFiles});
			// 
			// gridViewFiles
			// 
			this.gridViewFiles.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewFiles.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewFiles.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gridViewFiles.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridViewFiles.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewFiles.Appearance.Row.Options.UseFont = true;
			this.gridViewFiles.Appearance.Row.Options.UseTextOptions = true;
			this.gridViewFiles.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridViewFiles.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.gridViewFiles.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnFilesName,
            this.gridColumnFilesButtons});
			this.gridViewFiles.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridViewFiles.GridControl = this.gridControlFiles;
			this.gridViewFiles.Name = "gridViewFiles";
			this.gridViewFiles.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewFiles.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewFiles.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewFiles.OptionsCustomization.AllowFilter = false;
			this.gridViewFiles.OptionsCustomization.AllowGroup = false;
			this.gridViewFiles.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewFiles.OptionsFilter.AllowColumnMRUFilterList = false;
			this.gridViewFiles.OptionsMenu.EnableColumnMenu = false;
			this.gridViewFiles.OptionsMenu.EnableFooterMenu = false;
			this.gridViewFiles.OptionsMenu.EnableGroupPanelMenu = false;
			this.gridViewFiles.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.gridViewFiles.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.gridViewFiles.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewFiles.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewFiles.OptionsView.ShowColumnHeaders = false;
			this.gridViewFiles.OptionsView.ShowDetailButtons = false;
			this.gridViewFiles.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewFiles.OptionsView.ShowGroupPanel = false;
			this.gridViewFiles.OptionsView.ShowIndicator = false;
			this.gridViewFiles.RowHeight = 40;
			this.gridViewFiles.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView_RowClick);
			// 
			// gridColumnFilesName
			// 
			this.gridColumnFilesName.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnFilesName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridColumnFilesName.Caption = "Name";
			this.gridColumnFilesName.FieldName = "Name";
			this.gridColumnFilesName.Name = "gridColumnFilesName";
			this.gridColumnFilesName.OptionsColumn.AllowEdit = false;
			this.gridColumnFilesName.OptionsColumn.ReadOnly = true;
			this.gridColumnFilesName.Visible = true;
			this.gridColumnFilesName.VisibleIndex = 0;
			this.gridColumnFilesName.Width = 471;
			// 
			// gridColumnFilesButtons
			// 
			this.gridColumnFilesButtons.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridColumnFilesButtons.AppearanceCell.Options.UseFont = true;
			this.gridColumnFilesButtons.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnFilesButtons.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnFilesButtons.Caption = "Buttons";
			this.gridColumnFilesButtons.ColumnEdit = this.repositoryItemButtonEditFiles;
			this.gridColumnFilesButtons.FieldName = "Status";
			this.gridColumnFilesButtons.Name = "gridColumnFilesButtons";
			this.gridColumnFilesButtons.OptionsColumn.FixedWidth = true;
			this.gridColumnFilesButtons.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnFilesButtons.Visible = true;
			this.gridColumnFilesButtons.VisibleIndex = 1;
			this.gridColumnFilesButtons.Width = 45;
			// 
			// repositoryItemButtonEditFiles
			// 
			this.repositoryItemButtonEditFiles.AutoHeight = false;
			this.repositoryItemButtonEditFiles.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEditFiles.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.repositoryItemButtonEditFiles.Name = "repositoryItemButtonEditFiles";
			this.repositoryItemButtonEditFiles.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditFiles.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditFiles_ButtonClick);
			// 
			// xtraTabControl
			// 
			this.xtraTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.xtraTabControl.Appearance.BackColor = System.Drawing.Color.White;
			this.xtraTabControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.xtraTabControl.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControl.Appearance.Options.UseBackColor = true;
			this.xtraTabControl.Appearance.Options.UseFont = true;
			this.xtraTabControl.Appearance.Options.UseForeColor = true;
			this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControl.Location = new System.Drawing.Point(12, 67);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.SelectedTabPage = this.xtraTabPageFiles;
			this.xtraTabControl.Size = new System.Drawing.Size(599, 355);
			this.xtraTabControl.TabIndex = 19;
			this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageFiles,
            this.xtraTabPageTemplates});
			// 
			// xtraTabPageFiles
			// 
			this.xtraTabPageFiles.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageFiles.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageFiles.Controls.Add(this.gridControlFiles);
			this.xtraTabPageFiles.Name = "xtraTabPageFiles";
			this.xtraTabPageFiles.Size = new System.Drawing.Size(593, 324);
			this.xtraTabPageFiles.Text = "My Files";
			// 
			// xtraTabPageTemplates
			// 
			this.xtraTabPageTemplates.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageTemplates.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageTemplates.Controls.Add(this.gridControlTemplates);
			this.xtraTabPageTemplates.Name = "xtraTabPageTemplates";
			this.xtraTabPageTemplates.Size = new System.Drawing.Size(593, 324);
			this.xtraTabPageTemplates.Text = "My Templates";
			// 
			// gridControlTemplates
			// 
			this.gridControlTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlTemplates.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControlTemplates.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControlTemplates.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControlTemplates.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			this.gridControlTemplates.Location = new System.Drawing.Point(0, 0);
			this.gridControlTemplates.MainView = this.gridViewTemplates;
			this.gridControlTemplates.Name = "gridControlTemplates";
			this.gridControlTemplates.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEditTemplates});
			this.gridControlTemplates.Size = new System.Drawing.Size(593, 324);
			this.gridControlTemplates.TabIndex = 19;
			this.gridControlTemplates.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTemplates});
			// 
			// gridViewTemplates
			// 
			this.gridViewTemplates.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewTemplates.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewTemplates.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gridViewTemplates.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridViewTemplates.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewTemplates.Appearance.Row.Options.UseFont = true;
			this.gridViewTemplates.Appearance.Row.Options.UseTextOptions = true;
			this.gridViewTemplates.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridViewTemplates.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.gridViewTemplates.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnTemplatesName,
            this.gridColumnTemplatesButtons});
			this.gridViewTemplates.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridViewTemplates.GridControl = this.gridControlTemplates;
			this.gridViewTemplates.Name = "gridViewTemplates";
			this.gridViewTemplates.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewTemplates.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewTemplates.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewTemplates.OptionsCustomization.AllowFilter = false;
			this.gridViewTemplates.OptionsCustomization.AllowGroup = false;
			this.gridViewTemplates.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewTemplates.OptionsFilter.AllowColumnMRUFilterList = false;
			this.gridViewTemplates.OptionsMenu.EnableColumnMenu = false;
			this.gridViewTemplates.OptionsMenu.EnableFooterMenu = false;
			this.gridViewTemplates.OptionsMenu.EnableGroupPanelMenu = false;
			this.gridViewTemplates.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.gridViewTemplates.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.gridViewTemplates.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewTemplates.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewTemplates.OptionsView.ShowColumnHeaders = false;
			this.gridViewTemplates.OptionsView.ShowDetailButtons = false;
			this.gridViewTemplates.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewTemplates.OptionsView.ShowGroupPanel = false;
			this.gridViewTemplates.OptionsView.ShowIndicator = false;
			this.gridViewTemplates.RowHeight = 40;
			this.gridViewTemplates.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView_RowClick);
			// 
			// gridColumnTemplatesName
			// 
			this.gridColumnTemplatesName.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnTemplatesName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridColumnTemplatesName.Caption = "Name";
			this.gridColumnTemplatesName.FieldName = "Name";
			this.gridColumnTemplatesName.Name = "gridColumnTemplatesName";
			this.gridColumnTemplatesName.OptionsColumn.AllowEdit = false;
			this.gridColumnTemplatesName.OptionsColumn.ReadOnly = true;
			this.gridColumnTemplatesName.Visible = true;
			this.gridColumnTemplatesName.VisibleIndex = 0;
			this.gridColumnTemplatesName.Width = 471;
			// 
			// gridColumnTemplatesButtons
			// 
			this.gridColumnTemplatesButtons.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridColumnTemplatesButtons.AppearanceCell.Options.UseFont = true;
			this.gridColumnTemplatesButtons.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnTemplatesButtons.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnTemplatesButtons.Caption = "Buttons";
			this.gridColumnTemplatesButtons.ColumnEdit = this.repositoryItemButtonEditTemplates;
			this.gridColumnTemplatesButtons.FieldName = "Status";
			this.gridColumnTemplatesButtons.Name = "gridColumnTemplatesButtons";
			this.gridColumnTemplatesButtons.OptionsColumn.FixedWidth = true;
			this.gridColumnTemplatesButtons.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumnTemplatesButtons.Visible = true;
			this.gridColumnTemplatesButtons.VisibleIndex = 1;
			this.gridColumnTemplatesButtons.Width = 45;
			// 
			// repositoryItemButtonEditTemplates
			// 
			this.repositoryItemButtonEditTemplates.AutoHeight = false;
			this.repositoryItemButtonEditTemplates.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEditTemplates.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
			this.repositoryItemButtonEditTemplates.Name = "repositoryItemButtonEditTemplates";
			this.repositoryItemButtonEditTemplates.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repositoryItemButtonEditTemplates.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEditTemplates_ButtonClick);
			// 
			// FormSavedStates
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(623, 478);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXLoad);
			this.Controls.Add(this.laTitle);
			this.Controls.Add(this.xtraTabControl);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSavedStates";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Saved State";
			((System.ComponentModel.ISupportInitialize)(this.gridControlFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.xtraTabControl.ResumeLayout(false);
			this.xtraTabPageFiles.ResumeLayout(false);
			this.xtraTabPageTemplates.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlTemplates)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewTemplates)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEditTemplates)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXLoad;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevExpress.XtraGrid.GridControl gridControlFiles;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewFiles;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFilesName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnFilesButtons;
		protected System.Windows.Forms.Label laTitle;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditFiles;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageFiles;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageTemplates;
		private DevExpress.XtraGrid.GridControl gridControlTemplates;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewTemplates;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnTemplatesName;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnTemplatesButtons;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEditTemplates;
    }
}