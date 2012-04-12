namespace CalendarBuilder.ToolForms
{
    partial class FormImageGallery
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
            this.gridControlImageGallery = new DevExpress.XtraGrid.GridControl();
            this.gridViewImageGallery = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.pnBottom = new System.Windows.Forms.Panel();
            this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlImageGallery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewImageGallery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
            this.pnBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControlImageGallery
            // 
            this.gridControlImageGallery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlImageGallery.Location = new System.Drawing.Point(0, 0);
            this.gridControlImageGallery.MainView = this.gridViewImageGallery;
            this.gridControlImageGallery.Name = "gridControlImageGallery";
            this.gridControlImageGallery.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit});
            this.gridControlImageGallery.Size = new System.Drawing.Size(392, 361);
            this.gridControlImageGallery.TabIndex = 0;
            this.gridControlImageGallery.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewImageGallery});
            // 
            // gridViewImageGallery
            // 
            this.gridViewImageGallery.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnImage});
            this.gridViewImageGallery.GridControl = this.gridControlImageGallery;
            this.gridViewImageGallery.Name = "gridViewImageGallery";
            this.gridViewImageGallery.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewImageGallery.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewImageGallery.OptionsBehavior.AutoPopulateColumns = false;
            this.gridViewImageGallery.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gridViewImageGallery.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gridViewImageGallery.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gridViewImageGallery.OptionsBehavior.Editable = false;
            this.gridViewImageGallery.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gridViewImageGallery.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridViewImageGallery.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewImageGallery.OptionsCustomization.AllowColumnResizing = false;
            this.gridViewImageGallery.OptionsCustomization.AllowFilter = false;
            this.gridViewImageGallery.OptionsCustomization.AllowGroup = false;
            this.gridViewImageGallery.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewImageGallery.OptionsCustomization.AllowSort = false;
            this.gridViewImageGallery.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridViewImageGallery.OptionsFilter.AllowFilterEditor = false;
            this.gridViewImageGallery.OptionsFilter.AllowMRUFilterList = false;
            this.gridViewImageGallery.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewImageGallery.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridViewImageGallery.OptionsSelection.UseIndicatorForSelection = false;
            this.gridViewImageGallery.OptionsView.AllowCellMerge = true;
            this.gridViewImageGallery.OptionsView.RowAutoHeight = true;
            this.gridViewImageGallery.OptionsView.ShowColumnHeaders = false;
            this.gridViewImageGallery.OptionsView.ShowDetailButtons = false;
            this.gridViewImageGallery.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewImageGallery.OptionsView.ShowGroupPanel = false;
            this.gridViewImageGallery.OptionsView.ShowIndicator = false;
            this.gridViewImageGallery.OptionsView.ShowPreviewLines = false;
            this.gridViewImageGallery.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewImageGallery_RowClick);
            // 
            // gridColumnImage
            // 
            this.gridColumnImage.Caption = "Image";
            this.gridColumnImage.ColumnEdit = this.repositoryItemPictureEdit;
            this.gridColumnImage.FieldName = "SmallLogo";
            this.gridColumnImage.Name = "gridColumnImage";
            this.gridColumnImage.Visible = true;
            this.gridColumnImage.VisibleIndex = 0;
            // 
            // repositoryItemPictureEdit
            // 
            this.repositoryItemPictureEdit.Name = "repositoryItemPictureEdit";
            this.repositoryItemPictureEdit.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.Image;
            this.repositoryItemPictureEdit.ReadOnly = true;
            this.repositoryItemPictureEdit.ShowMenu = false;
            // 
            // pnBottom
            // 
            this.pnBottom.Controls.Add(this.buttonXCancel);
            this.pnBottom.Controls.Add(this.buttonXOK);
            this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom.Location = new System.Drawing.Point(0, 361);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(392, 43);
            this.pnBottom.TabIndex = 1;
            // 
            // buttonXCancel
            // 
            this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonXCancel.Location = new System.Drawing.Point(305, 6);
            this.buttonXCancel.Name = "buttonXCancel";
            this.buttonXCancel.Size = new System.Drawing.Size(75, 31);
            this.buttonXCancel.TabIndex = 1;
            this.buttonXCancel.Text = "Cancel";
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXOK.ForeColor = System.Drawing.Color.Black;
            this.buttonXOK.Location = new System.Drawing.Point(222, 6);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(75, 31);
            this.buttonXOK.TabIndex = 0;
            this.buttonXOK.Text = "OK";
            // 
            // FormImageGallery
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(392, 404);
            this.Controls.Add(this.gridControlImageGallery);
            this.Controls.Add(this.pnBottom);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 430);
            this.Name = "FormImageGallery";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Logo";
            this.Load += new System.EventHandler(this.FormImageGallery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlImageGallery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewImageGallery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
            this.pnBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlImageGallery;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewImageGallery;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnImage;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
        private System.Windows.Forms.Panel pnBottom;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevComponents.DotNetBar.ButtonX buttonXOK;


    }
}