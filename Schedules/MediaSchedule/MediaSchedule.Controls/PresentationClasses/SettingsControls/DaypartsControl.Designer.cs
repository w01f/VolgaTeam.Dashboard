namespace Asa.Media.Controls.PresentationClasses.SettingsControls
{
    partial class DaypartsControl
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
			this.gridControlItems = new DevExpress.XtraGrid.GridControl();
			this.gridViewItems = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnAvailable = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControlItems
			// 
			this.gridControlItems.AllowDrop = true;
			this.gridControlItems.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlItems.Location = new System.Drawing.Point(0, 0);
			this.gridControlItems.MainView = this.gridViewItems;
			this.gridControlItems.Name = "gridControlItems";
			this.gridControlItems.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit});
			this.gridControlItems.Size = new System.Drawing.Size(263, 316);
			this.gridControlItems.TabIndex = 7;
			this.gridControlItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewItems});
			// 
			// gridViewItems
			// 
			this.gridViewItems.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewItems.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewItems.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewItems.Appearance.Row.Options.UseFont = true;
			this.gridViewItems.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewItems.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnName,
            this.gridColumnAvailable});
			this.gridViewItems.GridControl = this.gridControlItems;
			this.gridViewItems.Name = "gridViewItems";
			this.gridViewItems.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewItems.OptionsCustomization.AllowColumnResizing = false;
			this.gridViewItems.OptionsCustomization.AllowFilter = false;
			this.gridViewItems.OptionsCustomization.AllowGroup = false;
			this.gridViewItems.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewItems.OptionsCustomization.AllowSort = false;
			this.gridViewItems.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewItems.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewItems.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewItems.OptionsView.RowAutoHeight = true;
			this.gridViewItems.OptionsView.ShowColumnHeaders = false;
			this.gridViewItems.OptionsView.ShowDetailButtons = false;
			this.gridViewItems.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewItems.OptionsView.ShowGroupPanel = false;
			this.gridViewItems.OptionsView.ShowIndicator = false;
			this.gridViewItems.RowHeight = 35;
			this.gridViewItems.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewItems_CellValueChanged);
			// 
			// gridColumnName
			// 
			this.gridColumnName.AppearanceCell.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridColumnName.AppearanceCell.Options.UseFont = true;
			this.gridColumnName.Caption = "Logo";
			this.gridColumnName.FieldName = "Name";
			this.gridColumnName.Name = "gridColumnName";
			this.gridColumnName.OptionsColumn.AllowEdit = false;
			this.gridColumnName.OptionsColumn.ReadOnly = true;
			this.gridColumnName.Visible = true;
			this.gridColumnName.VisibleIndex = 1;
			this.gridColumnName.Width = 201;
			// 
			// gridColumnAvailable
			// 
			this.gridColumnAvailable.Caption = "gridColumnAvailable";
			this.gridColumnAvailable.ColumnEdit = this.repositoryItemCheckEdit;
			this.gridColumnAvailable.FieldName = "Available";
			this.gridColumnAvailable.Name = "gridColumnAvailable";
			this.gridColumnAvailable.OptionsColumn.FixedWidth = true;
			this.gridColumnAvailable.Visible = true;
			this.gridColumnAvailable.VisibleIndex = 0;
			this.gridColumnAvailable.Width = 50;
			// 
			// repositoryItemCheckEdit
			// 
			this.repositoryItemCheckEdit.AutoHeight = false;
			this.repositoryItemCheckEdit.Caption = "Check";
			this.repositoryItemCheckEdit.Name = "repositoryItemCheckEdit";
			this.repositoryItemCheckEdit.CheckedChanged += new System.EventHandler(this.repositoryItemCheckEdit_CheckedChanged);
			// 
			// DaypartsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.gridControlItems);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "DaypartsControl";
			this.Size = new System.Drawing.Size(263, 316);
			((System.ComponentModel.ISupportInitialize)(this.gridControlItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraGrid.GridControl gridControlItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItems;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnAvailable;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit;
    }
}
