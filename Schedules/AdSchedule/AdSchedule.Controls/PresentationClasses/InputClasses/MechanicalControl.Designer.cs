namespace Asa.AdSchedule.Controls.PresentationClasses.InputClasses
{
    partial class MechanicalControl
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
            this.gridControlMechanicals = new DevExpress.XtraGrid.GridControl();
            this.gridViewMechanicals = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnValue = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMechanicals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMechanicals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlMechanicals
            // 
            this.gridControlMechanicals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlMechanicals.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridControlMechanicals.Location = new System.Drawing.Point(0, 0);
            this.gridControlMechanicals.MainView = this.gridViewMechanicals;
            this.gridControlMechanicals.Name = "gridControlMechanicals";
            this.gridControlMechanicals.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit});
            this.gridControlMechanicals.Size = new System.Drawing.Size(491, 352);
            this.gridControlMechanicals.TabIndex = 0;
            this.gridControlMechanicals.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMechanicals});
            // 
            // gridViewMechanicals
            // 
            this.gridViewMechanicals.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.gridViewMechanicals.Appearance.Empty.Options.UseBackColor = true;
            this.gridViewMechanicals.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.gridViewMechanicals.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridViewMechanicals.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.gridViewMechanicals.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridViewMechanicals.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.gridViewMechanicals.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridViewMechanicals.Appearance.Row.Options.UseBackColor = true;
            this.gridViewMechanicals.Appearance.Row.Options.UseFont = true;
            this.gridViewMechanicals.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.gridViewMechanicals.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridViewMechanicals.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnSelected,
            this.gridColumnName,
            this.gridColumnValue});
            this.gridViewMechanicals.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewMechanicals.GridControl = this.gridControlMechanicals;
            this.gridViewMechanicals.Name = "gridViewMechanicals";
            this.gridViewMechanicals.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewMechanicals.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewMechanicals.OptionsBehavior.Editable = false;
            this.gridViewMechanicals.OptionsBehavior.ReadOnly = true;
            this.gridViewMechanicals.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewMechanicals.OptionsCustomization.AllowColumnResizing = false;
            this.gridViewMechanicals.OptionsCustomization.AllowFilter = false;
            this.gridViewMechanicals.OptionsCustomization.AllowGroup = false;
            this.gridViewMechanicals.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewMechanicals.OptionsCustomization.AllowSort = false;
            this.gridViewMechanicals.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewMechanicals.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridViewMechanicals.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridViewMechanicals.OptionsView.ColumnAutoWidth = false;
            this.gridViewMechanicals.OptionsView.ShowColumnHeaders = false;
            this.gridViewMechanicals.OptionsView.ShowDetailButtons = false;
            this.gridViewMechanicals.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewMechanicals.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewMechanicals.OptionsView.ShowGroupPanel = false;
            this.gridViewMechanicals.OptionsView.ShowHorzLines = false;
            this.gridViewMechanicals.OptionsView.ShowIndicator = false;
            this.gridViewMechanicals.OptionsView.ShowPreviewLines = false;
            this.gridViewMechanicals.OptionsView.ShowVertLines = false;
            this.gridViewMechanicals.RowHeight = 25;
            this.gridViewMechanicals.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewMechanicals_RowClick);
            // 
            // gridColumnSelected
            // 
            this.gridColumnSelected.Caption = "Selected";
            this.gridColumnSelected.ColumnEdit = this.repositoryItemCheckEdit;
            this.gridColumnSelected.FieldName = "Selected";
            this.gridColumnSelected.Name = "gridColumnSelected";
            this.gridColumnSelected.OptionsColumn.FixedWidth = true;
            this.gridColumnSelected.Width = 20;
            // 
            // repositoryItemCheckEdit
            // 
            this.repositoryItemCheckEdit.AutoHeight = false;
            this.repositoryItemCheckEdit.Name = "repositoryItemCheckEdit";
            this.repositoryItemCheckEdit.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "Name";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 0;
            this.gridColumnName.Width = 187;
            // 
            // gridColumnValue
            // 
            this.gridColumnValue.Caption = "Value";
            this.gridColumnValue.FieldName = "Value";
            this.gridColumnValue.Name = "gridColumnValue";
            this.gridColumnValue.Visible = true;
            this.gridColumnValue.VisibleIndex = 1;
            this.gridColumnValue.Width = 189;
            // 
            // MechanicalControl
            // 
            this.Controls.Add(this.gridControlMechanicals);
            this.Name = "MechanicalControl";
            this.Size = new System.Drawing.Size(491, 352);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMechanicals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMechanicals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlMechanicals;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMechanicals;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnValue;

    }
}
