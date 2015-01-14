namespace NewBizWiz.CommonGUI.ToolForms
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
			this.pnBottom = new System.Windows.Forms.Panel();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.gridControlLogoGallery = new DevExpress.XtraGrid.GridControl();
			this.layoutViewLogoGallery = new DevExpress.XtraGrid.Views.Layout.LayoutView();
			this.gridColumnLogoGallery = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
			this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.layoutViewField_gridColumnLogoGallery = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
			this.layoutViewCardLogoGallery = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
			this.toolTipController = new DevExpress.Utils.ToolTipController();
			this.pnBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlLogoGallery)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewLogoGallery)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewField_gridColumnLogoGallery)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewCardLogoGallery)).BeginInit();
			this.SuspendLayout();
			// 
			// pnBottom
			// 
			this.pnBottom.BackColor = System.Drawing.Color.Transparent;
			this.pnBottom.Controls.Add(this.buttonXCancel);
			this.pnBottom.Controls.Add(this.buttonXOK);
			this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottom.ForeColor = System.Drawing.Color.Black;
			this.pnBottom.Location = new System.Drawing.Point(0, 361);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Size = new System.Drawing.Size(411, 43);
			this.pnBottom.TabIndex = 1;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(324, 6);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(75, 31);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 1;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(241, 6);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(75, 31);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 0;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// gridControlLogoGallery
			// 
			this.gridControlLogoGallery.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlLogoGallery.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlLogoGallery.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControlLogoGallery.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControlLogoGallery.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControlLogoGallery.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			this.gridControlLogoGallery.Location = new System.Drawing.Point(0, 0);
			this.gridControlLogoGallery.MainView = this.layoutViewLogoGallery;
			this.gridControlLogoGallery.Name = "gridControlLogoGallery";
			this.gridControlLogoGallery.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit});
			this.gridControlLogoGallery.Size = new System.Drawing.Size(411, 361);
			this.gridControlLogoGallery.TabIndex = 38;
			this.gridControlLogoGallery.ToolTipController = this.toolTipController;
			this.gridControlLogoGallery.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutViewLogoGallery});
			// 
			// layoutViewLogoGallery
			// 
			this.layoutViewLogoGallery.Appearance.SelectionFrame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
			this.layoutViewLogoGallery.Appearance.SelectionFrame.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.layoutViewLogoGallery.Appearance.SelectionFrame.Options.UseBackColor = true;
			this.layoutViewLogoGallery.CardMinSize = new System.Drawing.Size(91, 51);
			this.layoutViewLogoGallery.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.gridColumnLogoGallery});
			this.layoutViewLogoGallery.GridControl = this.gridControlLogoGallery;
			this.layoutViewLogoGallery.Name = "layoutViewLogoGallery";
			this.layoutViewLogoGallery.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.layoutViewLogoGallery.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.layoutViewLogoGallery.OptionsBehavior.AllowExpandCollapse = false;
			this.layoutViewLogoGallery.OptionsBehavior.AllowRuntimeCustomization = false;
			this.layoutViewLogoGallery.OptionsBehavior.AutoSelectAllInEditor = false;
			this.layoutViewLogoGallery.OptionsBehavior.Editable = false;
			this.layoutViewLogoGallery.OptionsBehavior.ReadOnly = true;
			this.layoutViewLogoGallery.OptionsCustomization.AllowFilter = false;
			this.layoutViewLogoGallery.OptionsCustomization.AllowSort = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowGroupCardCaptions = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowGroupCardIndents = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowGroupCards = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowGroupFields = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowGroupHiddenItems = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowGroupLayout = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowGroupLayoutTreeView = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowGroupView = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowResetShrinkButtons = false;
			this.layoutViewLogoGallery.OptionsCustomization.ShowSaveLoadLayoutButtons = false;
			this.layoutViewLogoGallery.OptionsFind.AllowFindPanel = false;
			this.layoutViewLogoGallery.OptionsFind.ClearFindOnClose = false;
			this.layoutViewLogoGallery.OptionsFind.ShowCloseButton = false;
			this.layoutViewLogoGallery.OptionsHeaderPanel.EnableCarouselModeButton = false;
			this.layoutViewLogoGallery.OptionsHeaderPanel.EnableColumnModeButton = false;
			this.layoutViewLogoGallery.OptionsHeaderPanel.EnableCustomizeButton = false;
			this.layoutViewLogoGallery.OptionsHeaderPanel.EnableMultiColumnModeButton = false;
			this.layoutViewLogoGallery.OptionsHeaderPanel.EnableMultiRowModeButton = false;
			this.layoutViewLogoGallery.OptionsHeaderPanel.EnablePanButton = false;
			this.layoutViewLogoGallery.OptionsHeaderPanel.EnableRowModeButton = false;
			this.layoutViewLogoGallery.OptionsHeaderPanel.EnableSingleModeButton = false;
			this.layoutViewLogoGallery.OptionsHeaderPanel.ShowCarouselModeButton = false;
			this.layoutViewLogoGallery.OptionsHeaderPanel.ShowColumnModeButton = false;
			this.layoutViewLogoGallery.OptionsHeaderPanel.ShowCustomizeButton = false;
			this.layoutViewLogoGallery.OptionsHeaderPanel.ShowMultiColumnModeButton = false;
			this.layoutViewLogoGallery.OptionsHeaderPanel.ShowMultiRowModeButton = false;
			this.layoutViewLogoGallery.OptionsHeaderPanel.ShowPanButton = false;
			this.layoutViewLogoGallery.OptionsHeaderPanel.ShowRowModeButton = false;
			this.layoutViewLogoGallery.OptionsHeaderPanel.ShowSingleModeButton = false;
			this.layoutViewLogoGallery.OptionsItemText.TextToControlDistance = 1;
			this.layoutViewLogoGallery.OptionsMultiRecordMode.MultiColumnScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Vertical;
			this.layoutViewLogoGallery.OptionsMultiRecordMode.MultiRowScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Vertical;
			this.layoutViewLogoGallery.OptionsView.ContentAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutViewLogoGallery.OptionsView.ShowCardBorderIfCaptionHidden = false;
			this.layoutViewLogoGallery.OptionsView.ShowCardCaption = false;
			this.layoutViewLogoGallery.OptionsView.ShowCardExpandButton = false;
			this.layoutViewLogoGallery.OptionsView.ShowCardLines = false;
			this.layoutViewLogoGallery.OptionsView.ShowFieldHints = false;
			this.layoutViewLogoGallery.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			this.layoutViewLogoGallery.OptionsView.ShowHeaderPanel = false;
			this.layoutViewLogoGallery.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiRow;
			this.layoutViewLogoGallery.TemplateCard = this.layoutViewCardLogoGallery;
			this.layoutViewLogoGallery.CustomFieldValueStyle += new DevExpress.XtraGrid.Views.Layout.Events.LayoutViewFieldValueStyleEventHandler(this.layoutViewLogoGallery_CustomFieldValueStyle);
			this.layoutViewLogoGallery.DoubleClick += new System.EventHandler(this.layoutViewLogoGallery_DoubleClick);
			// 
			// gridColumnLogoGallery
			// 
			this.gridColumnLogoGallery.Caption = "TinyImage";
			this.gridColumnLogoGallery.ColumnEdit = this.repositoryItemPictureEdit;
			this.gridColumnLogoGallery.FieldName = "TinyImage";
			this.gridColumnLogoGallery.LayoutViewField = this.layoutViewField_gridColumnLogoGallery;
			this.gridColumnLogoGallery.Name = "gridColumnLogoGallery";
			// 
			// repositoryItemPictureEdit
			// 
			this.repositoryItemPictureEdit.Name = "repositoryItemPictureEdit";
			this.repositoryItemPictureEdit.ShowMenu = false;
			this.repositoryItemPictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
			// 
			// layoutViewField_gridColumnLogoGallery
			// 
			this.layoutViewField_gridColumnLogoGallery.EditorPreferredWidth = 85;
			this.layoutViewField_gridColumnLogoGallery.Location = new System.Drawing.Point(0, 0);
			this.layoutViewField_gridColumnLogoGallery.Name = "layoutViewField_gridColumnLogoGallery";
			this.layoutViewField_gridColumnLogoGallery.Size = new System.Drawing.Size(91, 22);
			this.layoutViewField_gridColumnLogoGallery.Spacing = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
			this.layoutViewField_gridColumnLogoGallery.TextSize = new System.Drawing.Size(0, 0);
			this.layoutViewField_gridColumnLogoGallery.TextToControlDistance = 0;
			this.layoutViewField_gridColumnLogoGallery.TextVisible = false;
			// 
			// layoutViewCardLogoGallery
			// 
			this.layoutViewCardLogoGallery.CustomizationFormText = "TemplateCard";
			this.layoutViewCardLogoGallery.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
			this.layoutViewCardLogoGallery.GroupBordersVisible = false;
			this.layoutViewCardLogoGallery.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_gridColumnLogoGallery});
			this.layoutViewCardLogoGallery.Name = "layoutViewCard1";
			this.layoutViewCardLogoGallery.OptionsItemText.TextToControlDistance = 1;
			this.layoutViewCardLogoGallery.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
			this.layoutViewCardLogoGallery.Text = "TemplateCard";
			// 
			// toolTipController
			// 
			this.toolTipController.Rounded = true;
			this.toolTipController.ShowShadow = false;
			this.toolTipController.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController_GetActiveObjectInfo);
			// 
			// FormImageGallery
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(411, 404);
			this.Controls.Add(this.gridControlLogoGallery);
			this.Controls.Add(this.pnBottom);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(400, 430);
			this.Name = "FormImageGallery";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Logo";
			this.pnBottom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlLogoGallery)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewLogoGallery)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewField_gridColumnLogoGallery)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewCardLogoGallery)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel pnBottom;
        private DevComponents.DotNetBar.ButtonX buttonXCancel;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevExpress.XtraGrid.GridControl gridControlLogoGallery;
		private DevExpress.XtraGrid.Views.Layout.LayoutView layoutViewLogoGallery;
		private DevExpress.XtraGrid.Columns.LayoutViewColumn gridColumnLogoGallery;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_gridColumnLogoGallery;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCardLogoGallery;
		private DevExpress.Utils.ToolTipController toolTipController;


    }
}