namespace CalendarBuilder.ToolForms
{
	partial class FormDayProperties
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
			this.components = new System.ComponentModel.Container();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.simpleButtonCancel = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
			this.gridControlLogoGallery = new DevExpress.XtraGrid.GridControl();
			this.layoutViewLogoGallery = new DevExpress.XtraGrid.Views.Layout.LayoutView();
			this.gridColumnLogoGallery = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
			this.repositoryItemPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
			this.layoutViewFieldBannerGallery = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
			this.layoutViewCardGallery = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
			this.labelControlLogoTitle = new DevExpress.XtraEditors.LabelControl();
			this.checkEditComment = new DevExpress.XtraEditors.CheckEdit();
			this.memoEditComment = new DevExpress.XtraEditors.MemoEdit();
			this.labelControlCommentDisclaimer = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlLogoGallery)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewLogoGallery)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewFieldBannerGallery)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewCardGallery)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditComment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditComment.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
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
			// simpleButtonCancel
			// 
			this.simpleButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonCancel.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonCancel.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonCancel.Appearance.Options.UseFont = true;
			this.simpleButtonCancel.Appearance.Options.UseForeColor = true;
			this.simpleButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.simpleButtonCancel.Location = new System.Drawing.Point(376, 344);
			this.simpleButtonCancel.Name = "simpleButtonCancel";
			this.simpleButtonCancel.Size = new System.Drawing.Size(97, 37);
			this.simpleButtonCancel.TabIndex = 2;
			this.simpleButtonCancel.Text = "Cancel";
			// 
			// simpleButtonOK
			// 
			this.simpleButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButtonOK.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.simpleButtonOK.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButtonOK.Appearance.Options.UseFont = true;
			this.simpleButtonOK.Appearance.Options.UseForeColor = true;
			this.simpleButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.simpleButtonOK.Location = new System.Drawing.Point(376, 296);
			this.simpleButtonOK.Name = "simpleButtonOK";
			this.simpleButtonOK.Size = new System.Drawing.Size(97, 37);
			this.simpleButtonOK.TabIndex = 5;
			this.simpleButtonOK.Text = "OK";
			// 
			// gridControlLogoGallery
			// 
			this.gridControlLogoGallery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridControlLogoGallery.Location = new System.Drawing.Point(6, 30);
			this.gridControlLogoGallery.MainView = this.layoutViewLogoGallery;
			this.gridControlLogoGallery.Name = "gridControlLogoGallery";
			this.gridControlLogoGallery.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit});
			this.gridControlLogoGallery.Size = new System.Drawing.Size(467, 231);
			this.gridControlLogoGallery.TabIndex = 35;
			this.gridControlLogoGallery.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutViewLogoGallery});
			// 
			// layoutViewLogoGallery
			// 
			this.layoutViewLogoGallery.Appearance.SelectionFrame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
			this.layoutViewLogoGallery.Appearance.SelectionFrame.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.layoutViewLogoGallery.Appearance.SelectionFrame.Options.UseBackColor = true;
			this.layoutViewLogoGallery.CardMinSize = new System.Drawing.Size(199, 76);
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
			this.layoutViewLogoGallery.OptionsItemText.TextToControlDistance = 2;
			this.layoutViewLogoGallery.OptionsMultiRecordMode.MultiColumnScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Vertical;
			this.layoutViewLogoGallery.OptionsMultiRecordMode.MultiRowScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Vertical;
			this.layoutViewLogoGallery.OptionsView.ShowCardBorderIfCaptionHidden = false;
			this.layoutViewLogoGallery.OptionsView.ShowCardCaption = false;
			this.layoutViewLogoGallery.OptionsView.ShowCardExpandButton = false;
			this.layoutViewLogoGallery.OptionsView.ShowCardLines = false;
			this.layoutViewLogoGallery.OptionsView.ShowFieldHints = false;
			this.layoutViewLogoGallery.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
			this.layoutViewLogoGallery.OptionsView.ShowHeaderPanel = false;
			this.layoutViewLogoGallery.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.MultiRow;
			this.layoutViewLogoGallery.TemplateCard = this.layoutViewCardGallery;
			this.layoutViewLogoGallery.CustomFieldValueStyle += new DevExpress.XtraGrid.Views.Layout.Events.LayoutViewFieldValueStyleEventHandler(this.layoutViewLogoGallery_CustomFieldValueStyle);
			// 
			// gridColumnLogoGallery
			// 
			this.gridColumnLogoGallery.Caption = "Image";
			this.gridColumnLogoGallery.ColumnEdit = this.repositoryItemPictureEdit;
			this.gridColumnLogoGallery.FieldName = "SmallImage";
			this.gridColumnLogoGallery.LayoutViewField = this.layoutViewFieldBannerGallery;
			this.gridColumnLogoGallery.Name = "gridColumnLogoGallery";
			// 
			// repositoryItemPictureEdit
			// 
			this.repositoryItemPictureEdit.Name = "repositoryItemPictureEdit";
			this.repositoryItemPictureEdit.ShowMenu = false;
			this.repositoryItemPictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
			// 
			// layoutViewFieldBannerGallery
			// 
			this.layoutViewFieldBannerGallery.EditorPreferredWidth = 193;
			this.layoutViewFieldBannerGallery.Location = new System.Drawing.Point(0, 0);
			this.layoutViewFieldBannerGallery.Name = "layoutViewFieldBannerGallery";
			this.layoutViewFieldBannerGallery.Size = new System.Drawing.Size(199, 22);
			this.layoutViewFieldBannerGallery.Spacing = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
			this.layoutViewFieldBannerGallery.TextSize = new System.Drawing.Size(0, 0);
			this.layoutViewFieldBannerGallery.TextToControlDistance = 0;
			this.layoutViewFieldBannerGallery.TextVisible = false;
			// 
			// layoutViewCardGallery
			// 
			this.layoutViewCardGallery.CustomizationFormText = "TemplateCard";
			this.layoutViewCardGallery.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
			this.layoutViewCardGallery.GroupBordersVisible = false;
			this.layoutViewCardGallery.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewFieldBannerGallery});
			this.layoutViewCardGallery.Name = "layoutViewCard1";
			this.layoutViewCardGallery.OptionsItemText.TextToControlDistance = 2;
			this.layoutViewCardGallery.Text = "TemplateCard";
			// 
			// labelControlLogoTitle
			// 
			this.labelControlLogoTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlLogoTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlLogoTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlLogoTitle.Location = new System.Drawing.Point(6, 0);
			this.labelControlLogoTitle.Name = "labelControlLogoTitle";
			this.labelControlLogoTitle.Size = new System.Drawing.Size(463, 24);
			this.labelControlLogoTitle.StyleController = this.styleController;
			this.labelControlLogoTitle.TabIndex = 3;
			this.labelControlLogoTitle.Text = "Select a Logo:";
			// 
			// checkEditComment
			// 
			this.checkEditComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditComment.Location = new System.Drawing.Point(4, 267);
			this.checkEditComment.Name = "checkEditComment";
			this.checkEditComment.Properties.Caption = "Show Comment";
			this.checkEditComment.Size = new System.Drawing.Size(145, 21);
			this.checkEditComment.StyleController = this.styleController;
			this.checkEditComment.TabIndex = 36;
			this.checkEditComment.CheckedChanged += new System.EventHandler(this.checkEditComment_CheckedChanged);
			// 
			// memoEditComment
			// 
			this.memoEditComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditComment.Enabled = false;
			this.memoEditComment.Location = new System.Drawing.Point(6, 294);
			this.memoEditComment.Name = "memoEditComment";
			this.memoEditComment.Properties.NullText = "Type Here";
			this.memoEditComment.Size = new System.Drawing.Size(335, 64);
			this.memoEditComment.StyleController = this.styleController;
			this.memoEditComment.TabIndex = 37;
			// 
			// labelControlCommentDisclaimer
			// 
			this.labelControlCommentDisclaimer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlCommentDisclaimer.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlCommentDisclaimer.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlCommentDisclaimer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlCommentDisclaimer.Location = new System.Drawing.Point(6, 364);
			this.labelControlCommentDisclaimer.Name = "labelControlCommentDisclaimer";
			this.labelControlCommentDisclaimer.Size = new System.Drawing.Size(335, 17);
			this.labelControlCommentDisclaimer.StyleController = this.styleController;
			this.labelControlCommentDisclaimer.TabIndex = 38;
			this.labelControlCommentDisclaimer.Text = "*Keep Comments Short & Sweet";
			this.labelControlCommentDisclaimer.UseMnemonic = false;
			// 
			// FormDayProperties
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(481, 389);
			this.Controls.Add(this.labelControlCommentDisclaimer);
			this.Controls.Add(this.memoEditComment);
			this.Controls.Add(this.checkEditComment);
			this.Controls.Add(this.gridControlLogoGallery);
			this.Controls.Add(this.simpleButtonCancel);
			this.Controls.Add(this.simpleButtonOK);
			this.Controls.Add(this.labelControlLogoTitle);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDayProperties";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Day Properties:";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDayProperties_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlLogoGallery)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewLogoGallery)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewFieldBannerGallery)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutViewCardGallery)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditComment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditComment.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.SimpleButton simpleButtonCancel;
		private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
		private DevExpress.XtraGrid.GridControl gridControlLogoGallery;
		private DevExpress.XtraGrid.Views.Layout.LayoutView layoutViewLogoGallery;
		private DevExpress.XtraGrid.Columns.LayoutViewColumn gridColumnLogoGallery;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewFieldBannerGallery;
		private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCardGallery;
		private DevExpress.XtraEditors.LabelControl labelControlLogoTitle;
		private DevExpress.XtraEditors.CheckEdit checkEditComment;
		public DevExpress.XtraEditors.MemoEdit memoEditComment;
		private DevExpress.XtraEditors.LabelControl labelControlCommentDisclaimer;
	}
}