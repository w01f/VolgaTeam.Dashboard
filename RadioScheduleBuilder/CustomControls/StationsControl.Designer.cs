﻿namespace RadioScheduleBuilder.CustomControls
{
    partial class StationsControl
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
            this.panelExMain = new DevComponents.DotNetBar.PanelEx();
            this.gridControlItems = new DevExpress.XtraGrid.GridControl();
            this.gridViewItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnLogo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.gridColumnAvailable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panelExTop = new DevComponents.DotNetBar.PanelEx();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.panelExMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).BeginInit();
            this.panelExTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelExMain
            // 
            this.panelExMain.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelExMain.Controls.Add(this.gridControlItems);
            this.panelExMain.Controls.Add(this.panelExTop);
            this.panelExMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExMain.Location = new System.Drawing.Point(3, 3);
            this.panelExMain.Name = "panelExMain";
            this.panelExMain.Size = new System.Drawing.Size(257, 310);
            this.panelExMain.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExMain.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExMain.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelExMain.Style.BorderColor.Color = System.Drawing.Color.White;
            this.panelExMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelExMain.Style.GradientAngle = 90;
            this.panelExMain.TabIndex = 2;
            // 
            // gridControlItems
            // 
            this.gridControlItems.AllowDrop = true;
            this.gridControlItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlItems.Location = new System.Drawing.Point(1, 58);
            this.gridControlItems.MainView = this.gridViewItems;
            this.gridControlItems.Name = "gridControlItems";
            this.gridControlItems.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit,
            this.repositoryItemPictureEdit});
            this.gridControlItems.Size = new System.Drawing.Size(255, 251);
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
            this.gridColumnLogo,
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
            this.gridViewItems.RowHeight = 85;
            this.gridViewItems.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewItems_CellValueChanged);
            // 
            // gridColumnLogo
            // 
            this.gridColumnLogo.Caption = "Logo";
            this.gridColumnLogo.ColumnEdit = this.repositoryItemPictureEdit;
            this.gridColumnLogo.FieldName = "Logo";
            this.gridColumnLogo.Name = "gridColumnLogo";
            this.gridColumnLogo.OptionsColumn.AllowEdit = false;
            this.gridColumnLogo.OptionsColumn.ReadOnly = true;
            this.gridColumnLogo.Visible = true;
            this.gridColumnLogo.VisibleIndex = 1;
            this.gridColumnLogo.Width = 201;
            // 
            // repositoryItemPictureEdit
            // 
            this.repositoryItemPictureEdit.Name = "repositoryItemPictureEdit";
            this.repositoryItemPictureEdit.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.Image;
            this.repositoryItemPictureEdit.ReadOnly = true;
            this.repositoryItemPictureEdit.ShowMenu = false;
            this.repositoryItemPictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
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
            this.repositoryItemCheckEdit.Name = "repositoryItemCheckEdit";
            this.repositoryItemCheckEdit.CheckedChanged += new System.EventHandler(this.repositoryItemCheckEdit_CheckedChanged);
            // 
            // panelExTop
            // 
            this.panelExTop.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExTop.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelExTop.Controls.Add(this.pbLogo);
            this.panelExTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExTop.Location = new System.Drawing.Point(0, 0);
            this.panelExTop.Name = "panelExTop";
            this.panelExTop.Padding = new System.Windows.Forms.Padding(10, 1, 1, 1);
            this.panelExTop.Size = new System.Drawing.Size(257, 58);
            this.panelExTop.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExTop.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExTop.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExTop.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelExTop.Style.BorderColor.Color = System.Drawing.Color.White;
            this.panelExTop.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelExTop.Style.GradientAngle = 90;
            this.panelExTop.TabIndex = 6;
            // 
            // pbLogo
            // 
            this.pbLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbLogo.Image = global::RadioScheduleBuilder.Properties.Resources.Stations;
            this.pbLogo.Location = new System.Drawing.Point(10, 1);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(246, 56);
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // StationsControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.panelExMain);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "StationsControl";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(263, 316);
            this.panelExMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit)).EndInit();
            this.panelExTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelExMain;
        private DevExpress.XtraGrid.GridControl gridControlItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewItems;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnLogo;
        private DevComponents.DotNetBar.PanelEx panelExTop;
        private System.Windows.Forms.PictureBox pbLogo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnAvailable;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
    }
}
