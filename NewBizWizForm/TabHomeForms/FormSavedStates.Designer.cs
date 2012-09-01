namespace NewBizWizForm.TabHomeForms
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
            DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSavedStates));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.laTitle = new System.Windows.Forms.Label();
            this.buttonXLoad = new DevComponents.DotNetBar.ButtonX();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.gridControlFiles = new DevExpress.XtraGrid.GridControl();
            this.gridViewFiles = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnButtons = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemButtonEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // laTitle
            // 
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
            this.gridControlFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlFiles.Location = new System.Drawing.Point(12, 77);
            this.gridControlFiles.MainView = this.gridViewFiles;
            this.gridControlFiles.Name = "gridControlFiles";
            this.gridControlFiles.Size = new System.Drawing.Size(599, 340);
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
            this.gridViewFiles.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnName,
            this.gridColumnButtons});
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
            this.gridViewFiles.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewFiles_RowClick);
            // 
            // gridColumnName
            // 
            this.gridColumnName.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridColumnName.Caption = "Name";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.OptionsColumn.AllowEdit = false;
            this.gridColumnName.OptionsColumn.ReadOnly = true;
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 0;
            this.gridColumnName.Width = 471;
            // 
            // gridColumnButtons
            // 
            this.gridColumnButtons.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridColumnButtons.AppearanceCell.Options.UseFont = true;
            this.gridColumnButtons.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnButtons.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnButtons.Caption = "Buttons";
            repositoryItemButtonEdit2.AutoHeight = false;
            repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit2.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            repositoryItemButtonEdit2.LookAndFeel.SkinName = "Money Twins";
            repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit";
            repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.gridColumnButtons.ColumnEdit = repositoryItemButtonEdit2;
            this.gridColumnButtons.FieldName = "Status";
            this.gridColumnButtons.Name = "gridColumnButtons";
            this.gridColumnButtons.OptionsColumn.FixedWidth = true;
            this.gridColumnButtons.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridColumnButtons.Visible = true;
            this.gridColumnButtons.VisibleIndex = 1;
            this.gridColumnButtons.Width = 45;
            // 
            // FormSavedStates
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(623, 478);
            this.Controls.Add(this.gridControlFiles);
            this.Controls.Add(this.buttonXCancel);
            this.Controls.Add(this.buttonXLoad);
            this.Controls.Add(this.laTitle);
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
            ((System.ComponentModel.ISupportInitialize)(repositoryItemButtonEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXLoad;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevExpress.XtraGrid.GridControl gridControlFiles;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewFiles;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnButtons;
        protected System.Windows.Forms.Label laTitle;
    }
}