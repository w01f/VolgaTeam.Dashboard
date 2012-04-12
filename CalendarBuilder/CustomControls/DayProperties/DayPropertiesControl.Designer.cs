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
            this.laTitle = new System.Windows.Forms.Label();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barToolbar = new DevExpress.XtraBars.Bar();
            this.barLargeButtonItemApply = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barLargeButtonItemClone = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barLargeButtonItemClose = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.pnNavbar = new System.Windows.Forms.Panel();
            this.navBarControlDayProperties = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroupDigital = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainerDigital = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.digitalPropertiesControl = new CalendarBuilder.CustomControls.DayProperties.DigitalPropertiesControl();
            this.navBarGroupControlContainerTV = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.navBarGroupControlContainerNewspaper = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.newspaperPropertiesControl = new CalendarBuilder.CustomControls.DayProperties.NewspaperPropertiesControl();
            this.navBarGroupControlContainerComment = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.commentControl = new CalendarBuilder.CustomControls.DayProperties.CommentControl();
            this.navBarGroupNewspaper = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupTV = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupComment = new DevExpress.XtraNavBar.NavBarGroup();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.pnNavbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControlDayProperties)).BeginInit();
            this.navBarControlDayProperties.SuspendLayout();
            this.navBarGroupControlContainerDigital.SuspendLayout();
            this.navBarGroupControlContainerNewspaper.SuspendLayout();
            this.navBarGroupControlContainerComment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            this.SuspendLayout();
            // 
            // laTitle
            // 
            this.laTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.laTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.laTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.Location = new System.Drawing.Point(0, 0);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(300, 34);
            this.laTitle.TabIndex = 0;
            this.laTitle.Text = "Day Title";
            this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.barLargeButtonItemClone,
            this.barLargeButtonItemClose});
            this.barManager.MaxItemId = 7;
            // 
            // barToolbar
            // 
            this.barToolbar.BarName = "Tools";
            this.barToolbar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.barToolbar.DockCol = 0;
            this.barToolbar.DockRow = 0;
            this.barToolbar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barToolbar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemApply),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemClone),
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
            // barLargeButtonItemClone
            // 
            this.barLargeButtonItemClone.Caption = "Clone";
            this.barLargeButtonItemClone.Glyph = global::CalendarBuilder.Properties.Resources.CloneDayProperties;
            this.barLargeButtonItemClone.Id = 4;
            this.barLargeButtonItemClone.Name = "barLargeButtonItemClone";
            // 
            // barLargeButtonItemClose
            // 
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
            this.barDockControlTop.Size = new System.Drawing.Size(296, 44);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 583);
            this.barDockControlBottom.Size = new System.Drawing.Size(296, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 44);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 539);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(296, 44);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 539);
            // 
            // pnNavbar
            // 
            this.pnNavbar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnNavbar.Controls.Add(this.navBarControlDayProperties);
            this.pnNavbar.Controls.Add(this.barDockControlLeft);
            this.pnNavbar.Controls.Add(this.barDockControlRight);
            this.pnNavbar.Controls.Add(this.barDockControlBottom);
            this.pnNavbar.Controls.Add(this.barDockControlTop);
            this.pnNavbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnNavbar.Location = new System.Drawing.Point(0, 34);
            this.pnNavbar.Name = "pnNavbar";
            this.pnNavbar.Size = new System.Drawing.Size(300, 587);
            this.pnNavbar.TabIndex = 5;
            // 
            // navBarControlDayProperties
            // 
            this.navBarControlDayProperties.ActiveGroup = this.navBarGroupDigital;
            this.navBarControlDayProperties.BackColor = System.Drawing.Color.Transparent;
            this.navBarControlDayProperties.Controls.Add(this.navBarGroupControlContainerDigital);
            this.navBarControlDayProperties.Controls.Add(this.navBarGroupControlContainerTV);
            this.navBarControlDayProperties.Controls.Add(this.navBarGroupControlContainerNewspaper);
            this.navBarControlDayProperties.Controls.Add(this.navBarGroupControlContainerComment);
            this.navBarControlDayProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControlDayProperties.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroupDigital,
            this.navBarGroupNewspaper,
            this.navBarGroupTV,
            this.navBarGroupComment});
            this.navBarControlDayProperties.Location = new System.Drawing.Point(0, 44);
            this.navBarControlDayProperties.Name = "navBarControlDayProperties";
            this.navBarControlDayProperties.OptionsNavPane.ExpandedWidth = 212;
            this.navBarControlDayProperties.OptionsNavPane.ShowExpandButton = false;
            this.navBarControlDayProperties.OptionsNavPane.ShowOverflowButton = false;
            this.navBarControlDayProperties.OptionsNavPane.ShowOverflowPanel = false;
            this.navBarControlDayProperties.OptionsNavPane.ShowSplitter = false;
            this.navBarControlDayProperties.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.ExplorerBar;
            this.navBarControlDayProperties.ShowGroupHint = false;
            this.navBarControlDayProperties.Size = new System.Drawing.Size(296, 539);
            this.navBarControlDayProperties.TabIndex = 4;
            this.navBarControlDayProperties.Text = "navBarControl1";
            this.navBarControlDayProperties.View = new DevExpress.XtraNavBar.ViewInfo.SkinNavigationPaneViewInfoRegistrator();
            this.navBarControlDayProperties.ActiveGroupChanged += new DevExpress.XtraNavBar.NavBarGroupEventHandler(this.navBarControlDayProperties_ActiveGroupChanged);
            // 
            // navBarGroupDigital
            // 
            this.navBarGroupDigital.Appearance.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.navBarGroupDigital.Appearance.Options.UseFont = true;
            this.navBarGroupDigital.Caption = "";
            this.navBarGroupDigital.ControlContainer = this.navBarGroupControlContainerDigital;
            this.navBarGroupDigital.Expanded = true;
            this.navBarGroupDigital.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Small;
            this.navBarGroupDigital.GroupClientHeight = 80;
            this.navBarGroupDigital.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroupDigital.Hint = "Digital Info";
            this.navBarGroupDigital.Name = "navBarGroupDigital";
            this.navBarGroupDigital.SmallImage = global::CalendarBuilder.Properties.Resources.DayPropertiesDigital;
            // 
            // navBarGroupControlContainerDigital
            // 
            this.navBarGroupControlContainerDigital.Controls.Add(this.digitalPropertiesControl);
            this.navBarGroupControlContainerDigital.Name = "navBarGroupControlContainerDigital";
            this.navBarGroupControlContainerDigital.Size = new System.Drawing.Size(294, 325);
            this.navBarGroupControlContainerDigital.TabIndex = 1;
            // 
            // digitalPropertiesControl
            // 
            this.digitalPropertiesControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.digitalPropertiesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.digitalPropertiesControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.digitalPropertiesControl.Location = new System.Drawing.Point(0, 0);
            this.digitalPropertiesControl.Name = "digitalPropertiesControl";
            this.digitalPropertiesControl.Size = new System.Drawing.Size(294, 325);
            this.digitalPropertiesControl.TabIndex = 0;
            this.digitalPropertiesControl.PropertiesChanged += new System.EventHandler(this.propertiesControl_PropertiesChanged);
            // 
            // navBarGroupControlContainerTV
            // 
            this.navBarGroupControlContainerTV.Name = "navBarGroupControlContainerTV";
            this.navBarGroupControlContainerTV.Size = new System.Drawing.Size(210, 259);
            this.navBarGroupControlContainerTV.TabIndex = 0;
            // 
            // navBarGroupControlContainerNewspaper
            // 
            this.navBarGroupControlContainerNewspaper.Controls.Add(this.newspaperPropertiesControl);
            this.navBarGroupControlContainerNewspaper.Name = "navBarGroupControlContainerNewspaper";
            this.navBarGroupControlContainerNewspaper.Size = new System.Drawing.Size(234, 323);
            this.navBarGroupControlContainerNewspaper.TabIndex = 2;
            // 
            // newspaperPropertiesControl
            // 
            this.newspaperPropertiesControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.newspaperPropertiesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newspaperPropertiesControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.newspaperPropertiesControl.Location = new System.Drawing.Point(0, 0);
            this.newspaperPropertiesControl.Name = "newspaperPropertiesControl";
            this.newspaperPropertiesControl.Size = new System.Drawing.Size(234, 323);
            this.newspaperPropertiesControl.TabIndex = 0;
            this.newspaperPropertiesControl.PropertiesChanged += new System.EventHandler(this.propertiesControl_PropertiesChanged);
            // 
            // navBarGroupControlContainerComment
            // 
            this.navBarGroupControlContainerComment.Controls.Add(this.commentControl);
            this.navBarGroupControlContainerComment.Name = "navBarGroupControlContainerComment";
            this.navBarGroupControlContainerComment.Size = new System.Drawing.Size(234, 323);
            this.navBarGroupControlContainerComment.TabIndex = 3;
            // 
            // commentControl
            // 
            this.commentControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.commentControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commentControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.commentControl.Location = new System.Drawing.Point(0, 0);
            this.commentControl.Name = "commentControl";
            this.commentControl.Size = new System.Drawing.Size(234, 323);
            this.commentControl.TabIndex = 0;
            this.commentControl.PropertiesChanged += new System.EventHandler(this.propertiesControl_PropertiesChanged);
            // 
            // navBarGroupNewspaper
            // 
            this.navBarGroupNewspaper.Appearance.Font = new System.Drawing.Font("Arial", 15.75F);
            this.navBarGroupNewspaper.Appearance.Options.UseFont = true;
            this.navBarGroupNewspaper.Caption = "";
            this.navBarGroupNewspaper.ControlContainer = this.navBarGroupControlContainerNewspaper;
            this.navBarGroupNewspaper.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Small;
            this.navBarGroupNewspaper.GroupClientHeight = 80;
            this.navBarGroupNewspaper.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroupNewspaper.Hint = "Newspaper Info";
            this.navBarGroupNewspaper.Name = "navBarGroupNewspaper";
            this.navBarGroupNewspaper.SmallImage = global::CalendarBuilder.Properties.Resources.DayPropertiesNewspaper;
            // 
            // navBarGroupTV
            // 
            this.navBarGroupTV.Appearance.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.navBarGroupTV.Appearance.Options.UseFont = true;
            this.navBarGroupTV.Caption = "";
            this.navBarGroupTV.ControlContainer = this.navBarGroupControlContainerTV;
            this.navBarGroupTV.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Small;
            this.navBarGroupTV.GroupClientHeight = 80;
            this.navBarGroupTV.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroupTV.Hint = "Television Info";
            this.navBarGroupTV.Name = "navBarGroupTV";
            this.navBarGroupTV.SmallImage = global::CalendarBuilder.Properties.Resources.DayPropertiesTV;
            // 
            // navBarGroupComment
            // 
            this.navBarGroupComment.Appearance.Font = new System.Drawing.Font("Arial", 15.75F);
            this.navBarGroupComment.Appearance.Options.UseFont = true;
            this.navBarGroupComment.Caption = "";
            this.navBarGroupComment.ControlContainer = this.navBarGroupControlContainerComment;
            this.navBarGroupComment.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Small;
            this.navBarGroupComment.GroupClientHeight = 80;
            this.navBarGroupComment.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroupComment.Hint = "Custom Comments";
            this.navBarGroupComment.Name = "navBarGroupComment";
            this.navBarGroupComment.SmallImage = global::CalendarBuilder.Properties.Resources.DayPropertiesComment;
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
            // DayPropertiesControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.pnNavbar);
            this.Controls.Add(this.laTitle);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "DayPropertiesControl";
            this.Size = new System.Drawing.Size(300, 621);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.pnNavbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControlDayProperties)).EndInit();
            this.navBarControlDayProperties.ResumeLayout(false);
            this.navBarGroupControlContainerDigital.ResumeLayout(false);
            this.navBarGroupControlContainerNewspaper.ResumeLayout(false);
            this.navBarGroupControlContainerComment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laTitle;
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
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemClone;
        private DevExpress.XtraNavBar.NavBarControl navBarControlDayProperties;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupNewspaper;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainerNewspaper;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainerDigital;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainerTV;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainerComment;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupDigital;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupTV;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupComment;
        private NewspaperPropertiesControl newspaperPropertiesControl;
        private DigitalPropertiesControl digitalPropertiesControl;
        private CommentControl commentControl;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemClose;
    }
}
