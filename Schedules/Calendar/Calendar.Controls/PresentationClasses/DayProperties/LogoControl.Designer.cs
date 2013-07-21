namespace CalendarBuilder.PresentationClasses.DayProperties
{
    partial class LogoControl
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
            this.buttonXLogos = new DevComponents.DotNetBar.ButtonX();
            this.laSelectedLogo = new System.Windows.Forms.Label();
            this.pbSelectedLogo = new System.Windows.Forms.PictureBox();
            this.laAvailableLogos = new System.Windows.Forms.Label();
            this.gridControlImageGallery = new DevExpress.XtraGrid.GridControl();
            this.gridViewImageGallery = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlImageGallery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewImageGallery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
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
            // buttonXLogos
            // 
            this.buttonXLogos.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXLogos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXLogos.AutoCheckOnClick = true;
            this.buttonXLogos.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXLogos.Location = new System.Drawing.Point(8, 4);
            this.buttonXLogos.Name = "buttonXLogos";
            this.buttonXLogos.Size = new System.Drawing.Size(223, 24);
            this.buttonXLogos.TabIndex = 28;
            this.buttonXLogos.Text = "A. Enable Logos";
            this.buttonXLogos.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.buttonXLogos.TextColor = System.Drawing.Color.Black;
            this.buttonXLogos.CheckedChanged += new System.EventHandler(this.checkEditLogos_CheckedChanged);
            // 
            // laSelectedLogo
            // 
            this.laSelectedLogo.AutoSize = true;
            this.laSelectedLogo.Enabled = false;
            this.laSelectedLogo.Location = new System.Drawing.Point(5, 41);
            this.laSelectedLogo.Name = "laSelectedLogo";
            this.laSelectedLogo.Size = new System.Drawing.Size(95, 16);
            this.laSelectedLogo.TabIndex = 29;
            this.laSelectedLogo.Text = "Selected Logo:";
            // 
            // pbSelectedLogo
            // 
            this.pbSelectedLogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSelectedLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSelectedLogo.Enabled = false;
            this.pbSelectedLogo.Location = new System.Drawing.Point(8, 60);
            this.pbSelectedLogo.Name = "pbSelectedLogo";
            this.pbSelectedLogo.Size = new System.Drawing.Size(223, 75);
            this.pbSelectedLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSelectedLogo.TabIndex = 30;
            this.pbSelectedLogo.TabStop = false;
            // 
            // laAvailableLogos
            // 
            this.laAvailableLogos.AutoSize = true;
            this.laAvailableLogos.Enabled = false;
            this.laAvailableLogos.Location = new System.Drawing.Point(5, 149);
            this.laAvailableLogos.Name = "laAvailableLogos";
            this.laAvailableLogos.Size = new System.Drawing.Size(102, 16);
            this.laAvailableLogos.TabIndex = 31;
            this.laAvailableLogos.Text = "Available Logos:";
            // 
            // gridControlImageGallery
            // 
            this.gridControlImageGallery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlImageGallery.Enabled = false;
            this.gridControlImageGallery.Location = new System.Drawing.Point(8, 168);
            this.gridControlImageGallery.MainView = this.gridViewImageGallery;
            this.gridControlImageGallery.Name = "gridControlImageGallery";
            this.gridControlImageGallery.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit});
            this.gridControlImageGallery.Size = new System.Drawing.Size(223, 319);
            this.gridControlImageGallery.TabIndex = 32;
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
            this.gridViewImageGallery.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewImageGallery_FocusedRowChanged);
            this.gridViewImageGallery.Click += new System.EventHandler(this.gridViewImageGallery_Click);
            // 
            // gridColumnImage
            // 
            this.gridColumnImage.Caption = "Image";
            this.gridColumnImage.ColumnEdit = this.repositoryItemPictureEdit;
            this.gridColumnImage.FieldName = "SmallImage";
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
            // LogoControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.gridControlImageGallery);
            this.Controls.Add(this.laAvailableLogos);
            this.Controls.Add(this.pbSelectedLogo);
            this.Controls.Add(this.laSelectedLogo);
            this.Controls.Add(this.buttonXLogos);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "LogoControl";
            this.Size = new System.Drawing.Size(240, 500);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlImageGallery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewImageGallery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.StyleController styleController;
        private DevComponents.DotNetBar.ButtonX buttonXLogos;
        private System.Windows.Forms.Label laSelectedLogo;
        private System.Windows.Forms.PictureBox pbSelectedLogo;
        private System.Windows.Forms.Label laAvailableLogos;
        private DevExpress.XtraGrid.GridControl gridControlImageGallery;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewImageGallery;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnImage;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
    }
}
