namespace CalendarBuilder.CustomControls.DayProperties
{
    partial class DayPropertiesControl
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
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barToolbar = new DevExpress.XtraBars.Bar();
            this.barLargeButtonItemApply = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barLargeButtonItemDelete = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barLargeButtonItemClose = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.pnNavbar = new System.Windows.Forms.Panel();
            this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageDigital = new DevExpress.XtraTab.XtraTabPage();
            this.digitalPropertiesControl = new CalendarBuilder.CustomControls.DayProperties.DigitalPropertiesControl();
            this.xtraTabPageNewspaper = new DevExpress.XtraTab.XtraTabPage();
            this.newspaperPropertiesControl = new CalendarBuilder.CustomControls.DayProperties.NewspaperPropertiesControl();
            this.xtraTabPageTV = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPageComment = new DevExpress.XtraTab.XtraTabPage();
            this.commentControl = new CalendarBuilder.CustomControls.DayProperties.CommentControl();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.laDigitalTitle = new System.Windows.Forms.Label();
            this.laNewspaperTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.pnNavbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.xtraTabPageDigital.SuspendLayout();
            this.xtraTabPageNewspaper.SuspendLayout();
            this.xtraTabPageComment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowMoveBarOnToolbar = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.AllowShowToolbarsPopup = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barToolbar});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this.pnNavbar;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barLargeButtonItemApply,
            this.barLargeButtonItemDelete,
            this.barLargeButtonItemClose});
            this.barManager.MaxItemId = 7;
            // 
            // barToolbar
            // 
            this.barToolbar.BarName = "Tools";
            this.barToolbar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.barToolbar.DockCol = 0;
            this.barToolbar.DockRow = 0;
            this.barToolbar.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.barToolbar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemApply),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemClose)});
            this.barToolbar.OptionsBar.AllowQuickCustomization = false;
            this.barToolbar.OptionsBar.DrawDragBorder = false;
            this.barToolbar.OptionsBar.UseWholeRow = true;
            this.barToolbar.Text = "Tools";
            // 
            // barLargeButtonItemApply
            // 
            this.barLargeButtonItemApply.Caption = "Apply";
            this.barLargeButtonItemApply.Glyph = global::CalendarBuilder.Properties.Resources.ApplyDayProperties;
            this.barLargeButtonItemApply.Id = 3;
            this.barLargeButtonItemApply.Name = "barLargeButtonItemApply";
            this.barLargeButtonItemApply.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemApply_ItemClick);
            // 
            // barLargeButtonItemDelete
            // 
            this.barLargeButtonItemDelete.Caption = "Delete Data";
            this.barLargeButtonItemDelete.Glyph = global::CalendarBuilder.Properties.Resources.DeleteData;
            this.barLargeButtonItemDelete.Id = 4;
            this.barLargeButtonItemDelete.Name = "barLargeButtonItemDelete";
            this.barLargeButtonItemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemDelete_ItemClick);
            // 
            // barLargeButtonItemClose
            // 
            this.barLargeButtonItemClose.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barLargeButtonItemClose.Caption = "Close";
            this.barLargeButtonItemClose.Glyph = global::CalendarBuilder.Properties.Resources.CloseDayProperties;
            this.barLargeButtonItemClose.Id = 6;
            this.barLargeButtonItemClose.Name = "barLargeButtonItemClose";
            this.barLargeButtonItemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(296, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 573);
            this.barDockControlBottom.Size = new System.Drawing.Size(296, 44);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 573);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(296, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 573);
            // 
            // pnNavbar
            // 
            this.pnNavbar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnNavbar.Controls.Add(this.xtraTabControl);
            this.pnNavbar.Controls.Add(this.barDockControlLeft);
            this.pnNavbar.Controls.Add(this.barDockControlRight);
            this.pnNavbar.Controls.Add(this.barDockControlBottom);
            this.pnNavbar.Controls.Add(this.barDockControlTop);
            this.pnNavbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnNavbar.Location = new System.Drawing.Point(0, 0);
            this.pnNavbar.Name = "pnNavbar";
            this.pnNavbar.Size = new System.Drawing.Size(300, 621);
            this.pnNavbar.TabIndex = 5;
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
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.SelectedTabPage = this.xtraTabPageDigital;
            this.xtraTabControl.Size = new System.Drawing.Size(296, 573);
            this.xtraTabControl.TabIndex = 9;
            this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageDigital,
            this.xtraTabPageNewspaper,
            this.xtraTabPageTV,
            this.xtraTabPageComment});
            this.xtraTabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl_SelectedPageChanged);
            // 
            // xtraTabPageDigital
            // 
            this.xtraTabPageDigital.Controls.Add(this.digitalPropertiesControl);
            this.xtraTabPageDigital.Controls.Add(this.laDigitalTitle);
            this.xtraTabPageDigital.Name = "xtraTabPageDigital";
            this.xtraTabPageDigital.Size = new System.Drawing.Size(294, 547);
            this.xtraTabPageDigital.Text = "Digital";
            // 
            // digitalPropertiesControl
            // 
            this.digitalPropertiesControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.digitalPropertiesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.digitalPropertiesControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.digitalPropertiesControl.ForeColor = System.Drawing.Color.Black;
            this.digitalPropertiesControl.Location = new System.Drawing.Point(0, 31);
            this.digitalPropertiesControl.Name = "digitalPropertiesControl";
            this.digitalPropertiesControl.Size = new System.Drawing.Size(294, 516);
            this.digitalPropertiesControl.TabIndex = 0;
            this.digitalPropertiesControl.PropertiesChanged += new System.EventHandler(this.propertiesControl_PropertiesChanged);
            // 
            // xtraTabPageNewspaper
            // 
            this.xtraTabPageNewspaper.Controls.Add(this.newspaperPropertiesControl);
            this.xtraTabPageNewspaper.Controls.Add(this.laNewspaperTitle);
            this.xtraTabPageNewspaper.Name = "xtraTabPageNewspaper";
            this.xtraTabPageNewspaper.Size = new System.Drawing.Size(294, 547);
            this.xtraTabPageNewspaper.Text = "Newspapaer";
            // 
            // newspaperPropertiesControl
            // 
            this.newspaperPropertiesControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.newspaperPropertiesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newspaperPropertiesControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.newspaperPropertiesControl.ForeColor = System.Drawing.Color.Black;
            this.newspaperPropertiesControl.Location = new System.Drawing.Point(0, 31);
            this.newspaperPropertiesControl.Name = "newspaperPropertiesControl";
            this.newspaperPropertiesControl.Size = new System.Drawing.Size(294, 516);
            this.newspaperPropertiesControl.TabIndex = 0;
            this.newspaperPropertiesControl.PropertiesChanged += new System.EventHandler(this.propertiesControl_PropertiesChanged);
            // 
            // xtraTabPageTV
            // 
            this.xtraTabPageTV.Name = "xtraTabPageTV";
            this.xtraTabPageTV.Size = new System.Drawing.Size(294, 547);
            this.xtraTabPageTV.Text = "TV";
            // 
            // xtraTabPageComment
            // 
            this.xtraTabPageComment.Controls.Add(this.commentControl);
            this.xtraTabPageComment.Name = "xtraTabPageComment";
            this.xtraTabPageComment.Size = new System.Drawing.Size(294, 547);
            this.xtraTabPageComment.Text = "Comment";
            // 
            // commentControl
            // 
            this.commentControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.commentControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commentControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.commentControl.ForeColor = System.Drawing.Color.Black;
            this.commentControl.Location = new System.Drawing.Point(0, 0);
            this.commentControl.Name = "commentControl";
            this.commentControl.Size = new System.Drawing.Size(294, 547);
            this.commentControl.TabIndex = 0;
            this.commentControl.PropertiesChanged += new System.EventHandler(this.propertiesControl_PropertiesChanged);
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
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // laDigitalTitle
            // 
            this.laDigitalTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.laDigitalTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.laDigitalTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laDigitalTitle.ForeColor = System.Drawing.Color.White;
            this.laDigitalTitle.Location = new System.Drawing.Point(0, 0);
            this.laDigitalTitle.Name = "laDigitalTitle";
            this.laDigitalTitle.Size = new System.Drawing.Size(294, 31);
            this.laDigitalTitle.TabIndex = 1;
            this.laDigitalTitle.Text = "What Digital products are you selling?";
            this.laDigitalTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // laNewspaperTitle
            // 
            this.laNewspaperTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.laNewspaperTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.laNewspaperTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laNewspaperTitle.ForeColor = System.Drawing.Color.White;
            this.laNewspaperTitle.Location = new System.Drawing.Point(0, 0);
            this.laNewspaperTitle.Name = "laNewspaperTitle";
            this.laNewspaperTitle.Size = new System.Drawing.Size(294, 31);
            this.laNewspaperTitle.TabIndex = 2;
            this.laNewspaperTitle.Text = "What Print products are you selling?";
            this.laNewspaperTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DayPropertiesControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.pnNavbar);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "DayPropertiesControl";
            this.Size = new System.Drawing.Size(300, 621);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.pnNavbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.xtraTabPageDigital.ResumeLayout(false);
            this.xtraTabPageNewspaper.ResumeLayout(false);
            this.xtraTabPageComment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar barToolbar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.StyleController styleController;
        private System.Windows.Forms.Panel pnNavbar;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemApply;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemDelete;
        private NewspaperPropertiesControl newspaperPropertiesControl;
        private DigitalPropertiesControl digitalPropertiesControl;
        private CommentControl commentControl;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemClose;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageDigital;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageNewspaper;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageTV;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageComment;
        private System.Windows.Forms.Label laDigitalTitle;
        private System.Windows.Forms.Label laNewspaperTitle;
    }
}
