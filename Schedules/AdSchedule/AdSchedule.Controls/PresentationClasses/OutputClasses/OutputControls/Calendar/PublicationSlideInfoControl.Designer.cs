namespace Asa.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar
{
    partial class PublicationSlideInfoControl
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
			this.xtraTabPageInfo = new DevExpress.XtraTab.XtraTabPage();
			this.pnInfo = new System.Windows.Forms.Panel();
			this.xtraScrollableControlInfo = new DevExpress.XtraEditors.XtraScrollableControl();
			this.checkEditShowComment = new DevExpress.XtraEditors.CheckEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.buttonXShowAbbreviation = new DevComponents.DotNetBar.ButtonX();
			this.buttonXShowPersentOfPage = new DevComponents.DotNetBar.ButtonX();
			this.buttonXShowColor = new DevComponents.DotNetBar.ButtonX();
			this.buttonXShowPageSize = new DevComponents.DotNetBar.ButtonX();
			this.buttonXShowCost = new DevComponents.DotNetBar.ButtonX();
			this.buttonXShowAdSize = new DevComponents.DotNetBar.ButtonX();
			this.buttonXShowSection = new DevComponents.DotNetBar.ButtonX();
			this.buttonXShowBigDate = new DevComponents.DotNetBar.ButtonX();
			this.checkEditInfoApplyForAll = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditNotesCustomCommentApplyFoAll = new DevExpress.XtraEditors.CheckEdit();
			this.memoEditNotesCustomComment = new DevExpress.XtraEditors.MemoEdit();
			this.xtraTabPageStyle = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabControlStyle = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageStyleColor = new DevExpress.XtraTab.XtraTabPage();
			this.pnStyle = new System.Windows.Forms.Panel();
			this.outputColorSelector = new Asa.CommonGUI.OutputColors.OutputColorSelector();
			this.laThemeColor = new System.Windows.Forms.Label();
			this.checkEditThemeColorApplyForAll = new DevExpress.XtraEditors.CheckEdit();
			this.xtraTabPageStyleLogo = new DevExpress.XtraTab.XtraTabPage();
			this.pnLogo = new System.Windows.Forms.Panel();
			this.checkEditShowLogo = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditLogoApplyForAll = new DevExpress.XtraEditors.CheckEdit();
			this.xtraTabPageFavorites = new DevExpress.XtraTab.XtraTabPage();
			this.favoriteImagesControl = new Asa.CommonGUI.FavoriteImages.FavoriteImagesControl();
			this.calendarHeaderSelector = new Asa.Calendar.Controls.PresentationClasses.SlideInfo.CalendarHeaderSelector();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.xtraTabControl.SuspendLayout();
			this.xtraTabPageInfo.SuspendLayout();
			this.pnInfo.SuspendLayout();
			this.xtraScrollableControlInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowComment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditInfoApplyForAll.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditNotesCustomCommentApplyFoAll.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditNotesCustomComment.Properties)).BeginInit();
			this.xtraTabPageStyle.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlStyle)).BeginInit();
			this.xtraTabControlStyle.SuspendLayout();
			this.xtraTabPageStyleColor.SuspendLayout();
			this.pnStyle.SuspendLayout();
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
			this.xtraTabControl.SelectedTabPage = this.xtraTabPageInfo;
			this.xtraTabControl.Size = new System.Drawing.Size(304, 525);
			this.xtraTabControl.TabIndex = 10;
			this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageInfo,
            this.xtraTabPageStyle,
            this.xtraTabPageFavorites});
			// 
			// xtraTabPageInfo
			// 
			this.xtraTabPageInfo.Controls.Add(this.pnInfo);
			this.xtraTabPageInfo.Name = "xtraTabPageInfo";
			this.xtraTabPageInfo.Size = new System.Drawing.Size(298, 494);
			this.xtraTabPageInfo.Text = "Info";
			this.xtraTabPageInfo.Tooltip = "Show more info on your calendar";
			// 
			// pnInfo
			// 
			this.pnInfo.BackColor = System.Drawing.Color.White;
			this.pnInfo.Controls.Add(this.xtraScrollableControlInfo);
			this.pnInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnInfo.Location = new System.Drawing.Point(0, 0);
			this.pnInfo.Name = "pnInfo";
			this.pnInfo.Size = new System.Drawing.Size(298, 494);
			this.pnInfo.TabIndex = 3;
			// 
			// xtraScrollableControlInfo
			// 
			this.xtraScrollableControlInfo.Appearance.BackColor = System.Drawing.Color.White;
			this.xtraScrollableControlInfo.Appearance.Options.UseBackColor = true;
			this.xtraScrollableControlInfo.Controls.Add(this.checkEditShowComment);
			this.xtraScrollableControlInfo.Controls.Add(this.buttonXShowAbbreviation);
			this.xtraScrollableControlInfo.Controls.Add(this.buttonXShowPersentOfPage);
			this.xtraScrollableControlInfo.Controls.Add(this.buttonXShowColor);
			this.xtraScrollableControlInfo.Controls.Add(this.buttonXShowPageSize);
			this.xtraScrollableControlInfo.Controls.Add(this.buttonXShowCost);
			this.xtraScrollableControlInfo.Controls.Add(this.buttonXShowAdSize);
			this.xtraScrollableControlInfo.Controls.Add(this.buttonXShowSection);
			this.xtraScrollableControlInfo.Controls.Add(this.buttonXShowBigDate);
			this.xtraScrollableControlInfo.Controls.Add(this.checkEditInfoApplyForAll);
			this.xtraScrollableControlInfo.Controls.Add(this.checkEditNotesCustomCommentApplyFoAll);
			this.xtraScrollableControlInfo.Controls.Add(this.memoEditNotesCustomComment);
			this.xtraScrollableControlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraScrollableControlInfo.Location = new System.Drawing.Point(0, 0);
			this.xtraScrollableControlInfo.Name = "xtraScrollableControlInfo";
			this.xtraScrollableControlInfo.Size = new System.Drawing.Size(298, 494);
			this.xtraScrollableControlInfo.TabIndex = 2;
			// 
			// checkEditShowComment
			// 
			this.checkEditShowComment.Location = new System.Drawing.Point(10, 317);
			this.checkEditShowComment.Name = "checkEditShowComment";
			this.checkEditShowComment.Properties.AutoWidth = true;
			this.checkEditShowComment.Properties.Caption = "{0} Slide Comment";
			this.checkEditShowComment.Size = new System.Drawing.Size(130, 20);
			this.checkEditShowComment.StyleController = this.styleController;
			this.checkEditShowComment.TabIndex = 33;
			this.checkEditShowComment.CheckedChanged += new System.EventHandler(this.ShowComment_CheckedChanged);
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
			// buttonXShowAbbreviation
			// 
			this.buttonXShowAbbreviation.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShowAbbreviation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXShowAbbreviation.AutoCheckOnClick = true;
			this.buttonXShowAbbreviation.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShowAbbreviation.Location = new System.Drawing.Point(166, 172);
			this.buttonXShowAbbreviation.Name = "buttonXShowAbbreviation";
			this.buttonXShowAbbreviation.Size = new System.Drawing.Size(121, 29);
			this.buttonXShowAbbreviation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXShowAbbreviation.TabIndex = 17;
			this.buttonXShowAbbreviation.Text = "Codes Only";
			this.buttonXShowAbbreviation.TextColor = System.Drawing.Color.Black;
			this.buttonXShowAbbreviation.CheckedChanged += new System.EventHandler(this.buttonXShowProperty_CheckedChanged);
			// 
			// buttonXShowPersentOfPage
			// 
			this.buttonXShowPersentOfPage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShowPersentOfPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXShowPersentOfPage.AutoCheckOnClick = true;
			this.buttonXShowPersentOfPage.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShowPersentOfPage.Location = new System.Drawing.Point(166, 119);
			this.buttonXShowPersentOfPage.Name = "buttonXShowPersentOfPage";
			this.buttonXShowPersentOfPage.Size = new System.Drawing.Size(121, 29);
			this.buttonXShowPersentOfPage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXShowPersentOfPage.TabIndex = 15;
			this.buttonXShowPersentOfPage.Text = "% of Page";
			this.buttonXShowPersentOfPage.TextColor = System.Drawing.Color.Black;
			this.buttonXShowPersentOfPage.CheckedChanged += new System.EventHandler(this.buttonXShowProperty_CheckedChanged);
			// 
			// buttonXShowColor
			// 
			this.buttonXShowColor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShowColor.AutoCheckOnClick = true;
			this.buttonXShowColor.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShowColor.Location = new System.Drawing.Point(12, 119);
			this.buttonXShowColor.Name = "buttonXShowColor";
			this.buttonXShowColor.Size = new System.Drawing.Size(121, 29);
			this.buttonXShowColor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXShowColor.TabIndex = 14;
			this.buttonXShowColor.Text = "Color-BW";
			this.buttonXShowColor.TextColor = System.Drawing.Color.Black;
			this.buttonXShowColor.CheckedChanged += new System.EventHandler(this.buttonXShowProperty_CheckedChanged);
			// 
			// buttonXShowPageSize
			// 
			this.buttonXShowPageSize.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShowPageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXShowPageSize.AutoCheckOnClick = true;
			this.buttonXShowPageSize.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShowPageSize.Location = new System.Drawing.Point(166, 66);
			this.buttonXShowPageSize.Name = "buttonXShowPageSize";
			this.buttonXShowPageSize.Size = new System.Drawing.Size(121, 29);
			this.buttonXShowPageSize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXShowPageSize.TabIndex = 13;
			this.buttonXShowPageSize.Text = "Page Size";
			this.buttonXShowPageSize.TextColor = System.Drawing.Color.Black;
			this.buttonXShowPageSize.CheckedChanged += new System.EventHandler(this.buttonXShowProperty_CheckedChanged);
			// 
			// buttonXShowCost
			// 
			this.buttonXShowCost.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShowCost.AutoCheckOnClick = true;
			this.buttonXShowCost.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShowCost.Location = new System.Drawing.Point(12, 66);
			this.buttonXShowCost.Name = "buttonXShowCost";
			this.buttonXShowCost.Size = new System.Drawing.Size(121, 29);
			this.buttonXShowCost.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXShowCost.TabIndex = 12;
			this.buttonXShowCost.Text = "Ad Cost";
			this.buttonXShowCost.TextColor = System.Drawing.Color.Black;
			this.buttonXShowCost.CheckedChanged += new System.EventHandler(this.buttonXShowProperty_CheckedChanged);
			// 
			// buttonXShowAdSize
			// 
			this.buttonXShowAdSize.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShowAdSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXShowAdSize.AutoCheckOnClick = true;
			this.buttonXShowAdSize.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShowAdSize.Location = new System.Drawing.Point(166, 14);
			this.buttonXShowAdSize.Name = "buttonXShowAdSize";
			this.buttonXShowAdSize.Size = new System.Drawing.Size(121, 29);
			this.buttonXShowAdSize.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXShowAdSize.TabIndex = 11;
			this.buttonXShowAdSize.Text = "Col x In.";
			this.buttonXShowAdSize.TextColor = System.Drawing.Color.Black;
			this.buttonXShowAdSize.CheckedChanged += new System.EventHandler(this.buttonXShowProperty_CheckedChanged);
			// 
			// buttonXShowSection
			// 
			this.buttonXShowSection.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShowSection.AutoCheckOnClick = true;
			this.buttonXShowSection.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShowSection.Location = new System.Drawing.Point(12, 14);
			this.buttonXShowSection.Name = "buttonXShowSection";
			this.buttonXShowSection.Size = new System.Drawing.Size(121, 29);
			this.buttonXShowSection.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXShowSection.TabIndex = 10;
			this.buttonXShowSection.Text = "Ad Section";
			this.buttonXShowSection.TextColor = System.Drawing.Color.Black;
			this.buttonXShowSection.CheckedChanged += new System.EventHandler(this.buttonXShowProperty_CheckedChanged);
			// 
			// buttonXShowBigDate
			// 
			this.buttonXShowBigDate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXShowBigDate.AutoCheckOnClick = true;
			this.buttonXShowBigDate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXShowBigDate.Location = new System.Drawing.Point(12, 172);
			this.buttonXShowBigDate.Name = "buttonXShowBigDate";
			this.buttonXShowBigDate.Size = new System.Drawing.Size(121, 29);
			this.buttonXShowBigDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXShowBigDate.TabIndex = 9;
			this.buttonXShowBigDate.Text = "Big Dates";
			this.buttonXShowBigDate.TextColor = System.Drawing.Color.Black;
			this.buttonXShowBigDate.CheckedChanged += new System.EventHandler(this.buttonXShowProperty_CheckedChanged);
			// 
			// checkEditInfoApplyForAll
			// 
			this.checkEditInfoApplyForAll.Location = new System.Drawing.Point(10, 222);
			this.checkEditInfoApplyForAll.Name = "checkEditInfoApplyForAll";
			this.checkEditInfoApplyForAll.Properties.AutoWidth = true;
			this.checkEditInfoApplyForAll.Properties.Caption = "Apply this Info on ALL Slides";
			this.checkEditInfoApplyForAll.Size = new System.Drawing.Size(189, 20);
			this.checkEditInfoApplyForAll.StyleController = this.styleController;
			this.checkEditInfoApplyForAll.TabIndex = 8;
			// 
			// checkEditNotesCustomCommentApplyFoAll
			// 
			this.checkEditNotesCustomCommentApplyFoAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditNotesCustomCommentApplyFoAll.Location = new System.Drawing.Point(10, 461);
			this.checkEditNotesCustomCommentApplyFoAll.Name = "checkEditNotesCustomCommentApplyFoAll";
			this.checkEditNotesCustomCommentApplyFoAll.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditNotesCustomCommentApplyFoAll.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditNotesCustomCommentApplyFoAll.Properties.Caption = "Show this Comment on all slides";
			this.checkEditNotesCustomCommentApplyFoAll.Size = new System.Drawing.Size(277, 19);
			this.checkEditNotesCustomCommentApplyFoAll.StyleController = this.styleController;
			this.checkEditNotesCustomCommentApplyFoAll.TabIndex = 8;
			// 
			// memoEditNotesCustomComment
			// 
			this.memoEditNotesCustomComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditNotesCustomComment.Enabled = false;
			this.memoEditNotesCustomComment.Location = new System.Drawing.Point(27, 352);
			this.memoEditNotesCustomComment.Name = "memoEditNotesCustomComment";
			this.memoEditNotesCustomComment.Size = new System.Drawing.Size(260, 102);
			this.memoEditNotesCustomComment.StyleController = this.styleController;
			this.memoEditNotesCustomComment.TabIndex = 1;
			this.memoEditNotesCustomComment.UseOptimizedRendering = true;
			// 
			// xtraTabPageStyle
			// 
			this.xtraTabPageStyle.Controls.Add(this.xtraTabControlStyle);
			this.xtraTabPageStyle.Name = "xtraTabPageStyle";
			this.xtraTabPageStyle.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
			this.xtraTabPageStyle.Size = new System.Drawing.Size(298, 494);
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
			this.xtraTabControlStyle.Size = new System.Drawing.Size(298, 474);
			this.xtraTabControlStyle.TabIndex = 1;
			this.xtraTabControlStyle.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageStyleColor,
            this.xtraTabPageStyleLogo});
			// 
			// xtraTabPageStyleColor
			// 
			this.xtraTabPageStyleColor.Controls.Add(this.pnStyle);
			this.xtraTabPageStyleColor.Name = "xtraTabPageStyleColor";
			this.xtraTabPageStyleColor.Size = new System.Drawing.Size(292, 443);
			this.xtraTabPageStyleColor.Text = "Color Theme";
			this.xtraTabPageStyleColor.Tooltip = "Change the Color Style Of your calendar";
			// 
			// pnStyle
			// 
			this.pnStyle.BackColor = System.Drawing.Color.White;
			this.pnStyle.Controls.Add(this.outputColorSelector);
			this.pnStyle.Controls.Add(this.laThemeColor);
			this.pnStyle.Controls.Add(this.checkEditThemeColorApplyForAll);
			this.pnStyle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnStyle.Location = new System.Drawing.Point(0, 0);
			this.pnStyle.Name = "pnStyle";
			this.pnStyle.Size = new System.Drawing.Size(292, 443);
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
			this.outputColorSelector.Size = new System.Drawing.Size(290, 367);
			this.outputColorSelector.TabIndex = 52;
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
			this.laThemeColor.Size = new System.Drawing.Size(265, 23);
			this.laThemeColor.TabIndex = 9;
			this.laThemeColor.Text = "Style Color Theme Options";
			this.laThemeColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkEditThemeColorApplyForAll
			// 
			this.checkEditThemeColorApplyForAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditThemeColorApplyForAll.Location = new System.Drawing.Point(10, 410);
			this.checkEditThemeColorApplyForAll.Name = "checkEditThemeColorApplyForAll";
			this.checkEditThemeColorApplyForAll.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditThemeColorApplyForAll.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditThemeColorApplyForAll.Properties.Caption = "Use this Color on all calendar slides";
			this.checkEditThemeColorApplyForAll.Size = new System.Drawing.Size(279, 19);
			this.checkEditThemeColorApplyForAll.StyleController = this.styleController;
			this.checkEditThemeColorApplyForAll.TabIndex = 8;
			// 
			// xtraTabPageStyleLogo
			// 
			this.xtraTabPageStyleLogo.Controls.Add(this.pnLogo);
			this.xtraTabPageStyleLogo.Name = "xtraTabPageStyleLogo";
			this.xtraTabPageStyleLogo.Size = new System.Drawing.Size(292, 443);
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
			this.pnLogo.Size = new System.Drawing.Size(292, 443);
			this.pnLogo.TabIndex = 0;
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
			this.checkEditShowLogo.TabIndex = 43;
			this.checkEditShowLogo.CheckedChanged += new System.EventHandler(this.buttonXLogo_CheckedChanged);
			// 
			// checkEditLogoApplyForAll
			// 
			this.checkEditLogoApplyForAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditLogoApplyForAll.Location = new System.Drawing.Point(10, 410);
			this.checkEditLogoApplyForAll.Name = "checkEditLogoApplyForAll";
			this.checkEditLogoApplyForAll.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditLogoApplyForAll.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditLogoApplyForAll.Properties.Caption = "Show this logo at the top of all slides";
			this.checkEditLogoApplyForAll.Size = new System.Drawing.Size(267, 19);
			this.checkEditLogoApplyForAll.StyleController = this.styleController;
			this.checkEditLogoApplyForAll.TabIndex = 8;
			// 
			// xtraTabPageFavorites
			// 
			this.xtraTabPageFavorites.Controls.Add(this.favoriteImagesControl);
			this.xtraTabPageFavorites.Name = "xtraTabPageFavorites";
			this.xtraTabPageFavorites.Size = new System.Drawing.Size(298, 494);
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
			this.favoriteImagesControl.Size = new System.Drawing.Size(298, 494);
			this.favoriteImagesControl.TabIndex = 1;
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
			this.calendarHeaderSelector.Size = new System.Drawing.Size(292, 361);
			this.calendarHeaderSelector.TabIndex = 47;
			// 
			// PublicationSlideInfoControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.xtraTabControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "PublicationSlideInfoControl";
			this.Size = new System.Drawing.Size(304, 525);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.xtraTabControl.ResumeLayout(false);
			this.xtraTabPageInfo.ResumeLayout(false);
			this.pnInfo.ResumeLayout(false);
			this.xtraScrollableControlInfo.ResumeLayout(false);
			this.xtraScrollableControlInfo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowComment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditInfoApplyForAll.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditNotesCustomCommentApplyFoAll.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditNotesCustomComment.Properties)).EndInit();
			this.xtraTabPageStyle.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlStyle)).EndInit();
			this.xtraTabControlStyle.ResumeLayout(false);
			this.xtraTabPageStyleColor.ResumeLayout(false);
			this.pnStyle.ResumeLayout(false);
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
		private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControlInfo;
        private DevExpress.XtraEditors.CheckEdit checkEditInfoApplyForAll;
		private System.Windows.Forms.Panel pnInfo;
		private DevExpress.XtraEditors.CheckEdit checkEditNotesCustomCommentApplyFoAll;
		private DevExpress.XtraEditors.MemoEdit memoEditNotesCustomComment;
		private DevExpress.XtraEditors.CheckEdit checkEditLogoApplyForAll;
		private System.Windows.Forms.Panel pnStyle;
		private DevExpress.XtraEditors.CheckEdit checkEditThemeColorApplyForAll;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageInfo;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageStyle;
		private System.Windows.Forms.Label laThemeColor;
		private System.Windows.Forms.Panel pnLogo;
		private DevComponents.DotNetBar.ButtonX buttonXShowBigDate;
		private DevExpress.XtraEditors.CheckEdit checkEditShowComment;
		private DevComponents.DotNetBar.ButtonX buttonXShowAbbreviation;
		private DevComponents.DotNetBar.ButtonX buttonXShowPersentOfPage;
		private DevComponents.DotNetBar.ButtonX buttonXShowColor;
		private DevComponents.DotNetBar.ButtonX buttonXShowPageSize;
		private DevComponents.DotNetBar.ButtonX buttonXShowCost;
		private DevComponents.DotNetBar.ButtonX buttonXShowAdSize;
		private DevComponents.DotNetBar.ButtonX buttonXShowSection;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageFavorites;
		private CommonGUI.FavoriteImages.FavoriteImagesControl favoriteImagesControl;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlStyle;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageStyleColor;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageStyleLogo;
		private CommonGUI.OutputColors.OutputColorSelector outputColorSelector;
		private DevExpress.XtraEditors.CheckEdit checkEditShowLogo;
		private Asa.Calendar.Controls.PresentationClasses.SlideInfo.CalendarHeaderSelector calendarHeaderSelector;
    }
}
