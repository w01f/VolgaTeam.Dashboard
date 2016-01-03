namespace Asa.Calendar.Controls.PresentationClasses.SlideInfo
{
	partial class SlideInfoControl
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
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageComments = new DevExpress.XtraTab.XtraTabPage();
			this.pnComment = new System.Windows.Forms.Panel();
			this.memoEditComment = new DevExpress.XtraEditors.MemoEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.laCommentMonth = new System.Windows.Forms.Label();
			this.checkEditCommentApplyForAll = new DevExpress.XtraEditors.CheckEdit();
			this.buttonXComment = new DevComponents.DotNetBar.ButtonX();
			this.xtraTabPageStyle = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabControlStyle = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageStyleColor = new DevExpress.XtraTab.XtraTabPage();
			this.pnStyle = new System.Windows.Forms.Panel();
			this.outputColorSelector = new Asa.CommonGUI.OutputColors.OutputColorSelector();
			this.checkEditStyleBigDate = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditThemeColorApplyForAll = new DevExpress.XtraEditors.CheckEdit();
			this.laThemeColor = new System.Windows.Forms.Label();
			this.xtraTabPageStyleLogo = new DevExpress.XtraTab.XtraTabPage();
			this.pnLogo = new System.Windows.Forms.Panel();
			this.calendarHeaderSelector = new Asa.Calendar.Controls.PresentationClasses.SlideInfo.CalendarHeaderSelector();
			this.checkEditShowLogo = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditLogoApplyForAll = new DevExpress.XtraEditors.CheckEdit();
			this.xtraTabPageFavorites = new DevExpress.XtraTab.XtraTabPage();
			this.favoriteImagesControl = new Asa.CommonGUI.FavoriteImages.FavoriteImagesControl();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.xtraTabControl.SuspendLayout();
			this.xtraTabPageComments.SuspendLayout();
			this.pnComment.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditComment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditCommentApplyForAll.Properties)).BeginInit();
			this.xtraTabPageStyle.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlStyle)).BeginInit();
			this.xtraTabControlStyle.SuspendLayout();
			this.xtraTabPageStyleColor.SuspendLayout();
			this.pnStyle.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditStyleBigDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditThemeColorApplyForAll.Properties)).BeginInit();
			this.xtraTabPageStyleLogo.SuspendLayout();
			this.pnLogo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowLogo.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditLogoApplyForAll.Properties)).BeginInit();
			this.xtraTabPageFavorites.SuspendLayout();
			this.SuspendLayout();
			// 
			// xtraTabControl
			// 
			this.xtraTabControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.Appearance.Options.UseFont = true;
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
			this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControl.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControl.MultiLine = DevExpress.Utils.DefaultBoolean.True;
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.SelectedTabPage = this.xtraTabPageComments;
			this.xtraTabControl.Size = new System.Drawing.Size(304, 525);
			this.xtraTabControl.TabIndex = 10;
			this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageComments,
            this.xtraTabPageStyle,
            this.xtraTabPageFavorites});
			// 
			// xtraTabPageComments
			// 
			this.xtraTabPageComments.Controls.Add(this.pnComment);
			this.xtraTabPageComments.Name = "xtraTabPageComments";
			this.xtraTabPageComments.Size = new System.Drawing.Size(302, 497);
			this.xtraTabPageComments.Text = "Comments";
			this.xtraTabPageComments.Tooltip = "Show more info on your calendar";
			// 
			// pnComment
			// 
			this.pnComment.BackColor = System.Drawing.Color.White;
			this.pnComment.Controls.Add(this.memoEditComment);
			this.pnComment.Controls.Add(this.laCommentMonth);
			this.pnComment.Controls.Add(this.checkEditCommentApplyForAll);
			this.pnComment.Controls.Add(this.buttonXComment);
			this.pnComment.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnComment.Location = new System.Drawing.Point(0, 0);
			this.pnComment.Name = "pnComment";
			this.pnComment.Size = new System.Drawing.Size(302, 497);
			this.pnComment.TabIndex = 1;
			// 
			// memoEditComment
			// 
			this.memoEditComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditComment.Enabled = false;
			this.memoEditComment.Location = new System.Drawing.Point(14, 94);
			this.memoEditComment.Name = "memoEditComment";
			this.memoEditComment.Size = new System.Drawing.Size(275, 353);
			this.memoEditComment.StyleController = this.styleController;
			this.memoEditComment.TabIndex = 10;
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
			// laCommentMonth
			// 
			this.laCommentMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laCommentMonth.Location = new System.Drawing.Point(14, 70);
			this.laCommentMonth.Name = "laCommentMonth";
			this.laCommentMonth.Size = new System.Drawing.Size(275, 21);
			this.laCommentMonth.TabIndex = 9;
			this.laCommentMonth.Text = "label1";
			// 
			// checkEditCommentApplyForAll
			// 
			this.checkEditCommentApplyForAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditCommentApplyForAll.Location = new System.Drawing.Point(10, 464);
			this.checkEditCommentApplyForAll.Name = "checkEditCommentApplyForAll";
			this.checkEditCommentApplyForAll.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditCommentApplyForAll.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditCommentApplyForAll.Properties.Caption = "Show this Comment on all calendar slides";
			this.checkEditCommentApplyForAll.Size = new System.Drawing.Size(277, 20);
			this.checkEditCommentApplyForAll.StyleController = this.styleController;
			this.checkEditCommentApplyForAll.TabIndex = 8;
			// 
			// buttonXComment
			// 
			this.buttonXComment.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXComment.AutoCheckOnClick = true;
			this.buttonXComment.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXComment.Font = new System.Drawing.Font("Arial", 9.75F);
			this.buttonXComment.Location = new System.Drawing.Point(14, 18);
			this.buttonXComment.Name = "buttonXComment";
			this.buttonXComment.Size = new System.Drawing.Size(275, 29);
			this.buttonXComment.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXComment.TabIndex = 0;
			this.buttonXComment.Text = "Add a Comment to this slide";
			this.buttonXComment.TextColor = System.Drawing.Color.Black;
			this.buttonXComment.CheckedChanged += new System.EventHandler(this.ShowComment_CheckedChanged);
			// 
			// xtraTabPageStyle
			// 
			this.xtraTabPageStyle.Controls.Add(this.xtraTabControlStyle);
			this.xtraTabPageStyle.Name = "xtraTabPageStyle";
			this.xtraTabPageStyle.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
			this.xtraTabPageStyle.Size = new System.Drawing.Size(302, 497);
			this.xtraTabPageStyle.Text = "Slide Style";
			this.xtraTabPageStyle.Tooltip = "Change the Style Of your calendar";
			// 
			// xtraTabControlStyle
			// 
			this.xtraTabControlStyle.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlStyle.Appearance.Options.UseFont = true;
			this.xtraTabControlStyle.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlStyle.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlStyle.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlStyle.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlStyle.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlStyle.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlStyle.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlStyle.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlStyle.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlStyle.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlStyle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.xtraTabControlStyle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlStyle.Location = new System.Drawing.Point(0, 20);
			this.xtraTabControlStyle.Name = "xtraTabControlStyle";
			this.xtraTabControlStyle.SelectedTabPage = this.xtraTabPageStyleColor;
			this.xtraTabControlStyle.Size = new System.Drawing.Size(302, 477);
			this.xtraTabControlStyle.TabIndex = 11;
			this.xtraTabControlStyle.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageStyleColor,
            this.xtraTabPageStyleLogo});
			// 
			// xtraTabPageStyleColor
			// 
			this.xtraTabPageStyleColor.Controls.Add(this.pnStyle);
			this.xtraTabPageStyleColor.Name = "xtraTabPageStyleColor";
			this.xtraTabPageStyleColor.Size = new System.Drawing.Size(300, 449);
			this.xtraTabPageStyleColor.Text = "Color Theme";
			this.xtraTabPageStyleColor.Tooltip = "Change the Color Style Of your calendar";
			// 
			// pnStyle
			// 
			this.pnStyle.BackColor = System.Drawing.Color.White;
			this.pnStyle.Controls.Add(this.outputColorSelector);
			this.pnStyle.Controls.Add(this.checkEditStyleBigDate);
			this.pnStyle.Controls.Add(this.checkEditThemeColorApplyForAll);
			this.pnStyle.Controls.Add(this.laThemeColor);
			this.pnStyle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnStyle.Location = new System.Drawing.Point(0, 0);
			this.pnStyle.Name = "pnStyle";
			this.pnStyle.Size = new System.Drawing.Size(300, 449);
			this.pnStyle.TabIndex = 12;
			// 
			// outputColorSelector
			// 
			this.outputColorSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.outputColorSelector.BackColor = System.Drawing.Color.White;
			this.outputColorSelector.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.outputColorSelector.Location = new System.Drawing.Point(1, 37);
			this.outputColorSelector.Name = "outputColorSelector";
			this.outputColorSelector.Size = new System.Drawing.Size(298, 338);
			this.outputColorSelector.TabIndex = 51;
			// 
			// checkEditStyleBigDate
			// 
			this.checkEditStyleBigDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditStyleBigDate.Location = new System.Drawing.Point(10, 418);
			this.checkEditStyleBigDate.Name = "checkEditStyleBigDate";
			this.checkEditStyleBigDate.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditStyleBigDate.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditStyleBigDate.Properties.Caption = "Show BIG date numbers";
			this.checkEditStyleBigDate.Size = new System.Drawing.Size(287, 20);
			this.checkEditStyleBigDate.StyleController = this.styleController;
			this.checkEditStyleBigDate.TabIndex = 12;
			this.checkEditStyleBigDate.CheckedChanged += new System.EventHandler(this.checkEditStyleBigDate_CheckedChanged);
			// 
			// checkEditThemeColorApplyForAll
			// 
			this.checkEditThemeColorApplyForAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditThemeColorApplyForAll.Location = new System.Drawing.Point(10, 381);
			this.checkEditThemeColorApplyForAll.Name = "checkEditThemeColorApplyForAll";
			this.checkEditThemeColorApplyForAll.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditThemeColorApplyForAll.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditThemeColorApplyForAll.Properties.Caption = "Use this Color on all calendar slides";
			this.checkEditThemeColorApplyForAll.Size = new System.Drawing.Size(287, 20);
			this.checkEditThemeColorApplyForAll.StyleController = this.styleController;
			this.checkEditThemeColorApplyForAll.TabIndex = 11;
			// 
			// laThemeColor
			// 
			this.laThemeColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laThemeColor.BackColor = System.Drawing.Color.White;
			this.laThemeColor.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laThemeColor.ForeColor = System.Drawing.Color.Black;
			this.laThemeColor.Location = new System.Drawing.Point(9, 11);
			this.laThemeColor.Name = "laThemeColor";
			this.laThemeColor.Size = new System.Drawing.Size(273, 23);
			this.laThemeColor.TabIndex = 9;
			this.laThemeColor.Text = "Style Color Theme Options";
			this.laThemeColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// xtraTabPageStyleLogo
			// 
			this.xtraTabPageStyleLogo.Controls.Add(this.pnLogo);
			this.xtraTabPageStyleLogo.Name = "xtraTabPageStyleLogo";
			this.xtraTabPageStyleLogo.Size = new System.Drawing.Size(300, 449);
			this.xtraTabPageStyleLogo.Text = "Header Logo";
			this.xtraTabPageStyleLogo.Tooltip = "Show a logo at the TOP of your calendar";
			// 
			// pnLogo
			// 
			this.pnLogo.BackColor = System.Drawing.Color.White;
			this.pnLogo.Controls.Add(this.calendarHeaderSelector);
			this.pnLogo.Controls.Add(this.checkEditShowLogo);
			this.pnLogo.Controls.Add(this.checkEditLogoApplyForAll);
			this.pnLogo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnLogo.Location = new System.Drawing.Point(0, 0);
			this.pnLogo.Name = "pnLogo";
			this.pnLogo.Size = new System.Drawing.Size(300, 449);
			this.pnLogo.TabIndex = 0;
			// 
			// calendarHeaderSelector
			// 
			this.calendarHeaderSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.calendarHeaderSelector.Enabled = false;
			this.calendarHeaderSelector.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.calendarHeaderSelector.Location = new System.Drawing.Point(0, 42);
			this.calendarHeaderSelector.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.calendarHeaderSelector.Name = "calendarHeaderSelector";
			this.calendarHeaderSelector.SelectedImageSource = null;
			this.calendarHeaderSelector.Size = new System.Drawing.Size(300, 367);
			this.calendarHeaderSelector.TabIndex = 45;
			// 
			// checkEditShowLogo
			// 
			this.checkEditShowLogo.Location = new System.Drawing.Point(10, 15);
			this.checkEditShowLogo.Name = "checkEditShowLogo";
			this.checkEditShowLogo.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditShowLogo.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditShowLogo.Properties.AutoWidth = true;
			this.checkEditShowLogo.Properties.Caption = "Show a Logo at the top of the slide";
			this.checkEditShowLogo.Size = new System.Drawing.Size(223, 20);
			this.checkEditShowLogo.StyleController = this.styleController;
			this.checkEditShowLogo.TabIndex = 44;
			this.checkEditShowLogo.CheckedChanged += new System.EventHandler(this.buttonXLogo_CheckedChanged);
			// 
			// checkEditLogoApplyForAll
			// 
			this.checkEditLogoApplyForAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditLogoApplyForAll.Location = new System.Drawing.Point(10, 416);
			this.checkEditLogoApplyForAll.Name = "checkEditLogoApplyForAll";
			this.checkEditLogoApplyForAll.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditLogoApplyForAll.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditLogoApplyForAll.Properties.Caption = "Show this logo at the top of all slides";
			this.checkEditLogoApplyForAll.Size = new System.Drawing.Size(275, 20);
			this.checkEditLogoApplyForAll.StyleController = this.styleController;
			this.checkEditLogoApplyForAll.TabIndex = 8;
			// 
			// xtraTabPageFavorites
			// 
			this.xtraTabPageFavorites.Controls.Add(this.favoriteImagesControl);
			this.xtraTabPageFavorites.Name = "xtraTabPageFavorites";
			this.xtraTabPageFavorites.Size = new System.Drawing.Size(302, 497);
			this.xtraTabPageFavorites.Text = "My Gallery";
			this.xtraTabPageFavorites.Tooltip = "Add Product Logos to your Calendar";
			// 
			// favoriteImagesControl
			// 
			this.favoriteImagesControl.BackColor = System.Drawing.Color.White;
			this.favoriteImagesControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.favoriteImagesControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.favoriteImagesControl.ImageTooltip = null;
			this.favoriteImagesControl.Location = new System.Drawing.Point(0, 0);
			this.favoriteImagesControl.Name = "favoriteImagesControl";
			this.favoriteImagesControl.Size = new System.Drawing.Size(302, 497);
			this.favoriteImagesControl.TabIndex = 1;
			// 
			// SlideInfoControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.xtraTabControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "SlideInfoControl";
			this.Size = new System.Drawing.Size(304, 525);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.xtraTabControl.ResumeLayout(false);
			this.xtraTabPageComments.ResumeLayout(false);
			this.pnComment.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.memoEditComment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditCommentApplyForAll.Properties)).EndInit();
			this.xtraTabPageStyle.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlStyle)).EndInit();
			this.xtraTabControlStyle.ResumeLayout(false);
			this.xtraTabPageStyleColor.ResumeLayout(false);
			this.pnStyle.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkEditStyleBigDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditThemeColorApplyForAll.Properties)).EndInit();
			this.xtraTabPageStyleLogo.ResumeLayout(false);
			this.pnLogo.ResumeLayout(false);
			this.pnLogo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowLogo.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditLogoApplyForAll.Properties)).EndInit();
			this.xtraTabPageFavorites.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.CheckEdit checkEditLogoApplyForAll;
		private System.Windows.Forms.Panel pnStyle;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageComments;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageStyle;
		private System.Windows.Forms.Label laThemeColor;
		private System.Windows.Forms.Panel pnLogo;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageFavorites;
		private CommonGUI.FavoriteImages.FavoriteImagesControl favoriteImagesControl;
		private System.Windows.Forms.Panel pnComment;
		private DevExpress.XtraEditors.MemoEdit memoEditComment;
		private System.Windows.Forms.Label laCommentMonth;
		private DevExpress.XtraEditors.CheckEdit checkEditCommentApplyForAll;
		private DevComponents.DotNetBar.ButtonX buttonXComment;
		private DevExpress.XtraEditors.CheckEdit checkEditStyleBigDate;
		private DevExpress.XtraEditors.CheckEdit checkEditThemeColorApplyForAll;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlStyle;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageStyleColor;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageStyleLogo;
		private CommonGUI.OutputColors.OutputColorSelector outputColorSelector;
		private CalendarHeaderSelector calendarHeaderSelector;
		private DevExpress.XtraEditors.CheckEdit checkEditShowLogo;
    }
}
