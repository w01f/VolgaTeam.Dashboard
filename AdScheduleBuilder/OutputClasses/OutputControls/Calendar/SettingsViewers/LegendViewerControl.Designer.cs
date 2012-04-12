namespace AdScheduleBuilder.OutputClasses.OutputControls.Calendar.SettingsViewers
{
    partial class LegendViewerControl
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
            this.gridControlLegend = new DevExpress.XtraGrid.GridControl();
            this.gridViewLegend = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnVisible = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumnCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLegend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLegend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlLegend
            // 
            this.gridControlLegend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlLegend.Location = new System.Drawing.Point(0, 0);
            this.gridControlLegend.MainView = this.gridViewLegend;
            this.gridControlLegend.Name = "gridControlLegend";
            this.gridControlLegend.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit});
            this.gridControlLegend.Size = new System.Drawing.Size(345, 313);
            this.gridControlLegend.TabIndex = 1;
            this.gridControlLegend.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLegend});
            // 
            // gridViewLegend
            // 
            this.gridViewLegend.Appearance.HorzLine.BackColor = System.Drawing.Color.Black;
            this.gridViewLegend.Appearance.HorzLine.Options.UseBackColor = true;
            this.gridViewLegend.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewLegend.Appearance.Row.Options.UseFont = true;
            this.gridViewLegend.Appearance.Row.Options.UseTextOptions = true;
            this.gridViewLegend.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewLegend.Appearance.VertLine.BackColor = System.Drawing.Color.Black;
            this.gridViewLegend.Appearance.VertLine.Options.UseBackColor = true;
            this.gridViewLegend.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnVisible,
            this.gridColumnCode,
            this.gridColumnDescription});
            this.gridViewLegend.GridControl = this.gridControlLegend;
            this.gridViewLegend.Name = "gridViewLegend";
            this.gridViewLegend.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewLegend.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewLegend.OptionsBehavior.AutoPopulateColumns = false;
            this.gridViewLegend.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewLegend.OptionsCustomization.AllowColumnResizing = false;
            this.gridViewLegend.OptionsCustomization.AllowFilter = false;
            this.gridViewLegend.OptionsCustomization.AllowGroup = false;
            this.gridViewLegend.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewLegend.OptionsCustomization.AllowSort = false;
            this.gridViewLegend.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewLegend.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridViewLegend.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridViewLegend.OptionsView.ShowColumnHeaders = false;
            this.gridViewLegend.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewLegend.OptionsView.ShowGroupPanel = false;
            this.gridViewLegend.OptionsView.ShowIndicator = false;
            this.gridViewLegend.OptionsView.ShowPreviewLines = false;
            this.gridViewLegend.RowHeight = 40;
            // 
            // gridColumnVisible
            // 
            this.gridColumnVisible.Caption = "Visible";
            this.gridColumnVisible.ColumnEdit = this.repositoryItemCheckEdit;
            this.gridColumnVisible.FieldName = "Visible";
            this.gridColumnVisible.Name = "gridColumnVisible";
            this.gridColumnVisible.OptionsColumn.FixedWidth = true;
            this.gridColumnVisible.Visible = true;
            this.gridColumnVisible.VisibleIndex = 0;
            this.gridColumnVisible.Width = 63;
            // 
            // repositoryItemCheckEdit
            // 
            this.repositoryItemCheckEdit.AutoHeight = false;
            this.repositoryItemCheckEdit.Name = "repositoryItemCheckEdit";
            // 
            // gridColumnCode
            // 
            this.gridColumnCode.Caption = "Code";
            this.gridColumnCode.FieldName = "Code";
            this.gridColumnCode.Name = "gridColumnCode";
            this.gridColumnCode.OptionsColumn.FixedWidth = true;
            this.gridColumnCode.Visible = true;
            this.gridColumnCode.VisibleIndex = 1;
            this.gridColumnCode.Width = 97;
            // 
            // gridColumnDescription
            // 
            this.gridColumnDescription.AppearanceCell.BackColor = System.Drawing.Color.Gainsboro;
            this.gridColumnDescription.AppearanceCell.Options.UseBackColor = true;
            this.gridColumnDescription.Caption = "Description";
            this.gridColumnDescription.FieldName = "Description";
            this.gridColumnDescription.Name = "gridColumnDescription";
            this.gridColumnDescription.OptionsColumn.AllowEdit = false;
            this.gridColumnDescription.OptionsColumn.ReadOnly = true;
            this.gridColumnDescription.Visible = true;
            this.gridColumnDescription.VisibleIndex = 2;
            this.gridColumnDescription.Width = 1580;
            // 
            // styleController
            // 
            this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.styleController.Appearance.Options.UseFont = true;
            this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.styleController.AppearanceDisabled.Options.UseFont = true;
            this.styleController.AppearanceDisabled.Options.UseForeColor = true;
            this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDropDown.Options.UseFont = true;
            this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
            this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceFocused.Options.UseFont = true;
            this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.styleController.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            this.styleController.AppearanceReadOnly.Options.UseFont = true;
            this.styleController.AppearanceReadOnly.Options.UseForeColor = true;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // LegendViewerControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.gridControlLegend);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "LegendViewerControl";
            this.Size = new System.Drawing.Size(345, 313);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLegend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLegend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlLegend;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLegend;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnVisible;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDescription;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
    }
}
