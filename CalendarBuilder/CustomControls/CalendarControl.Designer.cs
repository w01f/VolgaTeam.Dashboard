namespace CalendarBuilder.CustomControls
{
    partial class CalendarControl
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
            this.pnTop = new System.Windows.Forms.Panel();
            this.laCalendarWindow = new System.Windows.Forms.Label();
            this.laAdvertiser = new System.Windows.Forms.Label();
            this.laCalendarName = new System.Windows.Forms.Label();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.xtraScrollableControlMain = new DevExpress.XtraEditors.XtraScrollableControl();
            this.pnEmpty = new System.Windows.Forms.Panel();
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanelDayProperties = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanelDayProperties_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dayPropertiesControl = new CalendarBuilder.CustomControls.DayProperties.DayPropertiesControl();
            this.dockPanelSlideInfo = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanelSlideInfo_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.slideInfoControl = new CalendarBuilder.CustomControls.SlideInfo.SlideInfoControl();
            this.pnMain = new System.Windows.Forms.Panel();
            this.gridControl = new CalendarBuilder.CustomControls.Grid.GridControl();
            this.pnTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            this.dockPanelDayProperties.SuspendLayout();
            this.dockPanelDayProperties_Container.SuspendLayout();
            this.dockPanelSlideInfo.SuspendLayout();
            this.dockPanelSlideInfo_Container.SuspendLayout();
            this.pnMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnTop
            // 
            this.pnTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnTop.Controls.Add(this.laCalendarWindow);
            this.pnTop.Controls.Add(this.laAdvertiser);
            this.pnTop.Controls.Add(this.laCalendarName);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(653, 35);
            this.pnTop.TabIndex = 1;
            // 
            // laCalendarWindow
            // 
            this.laCalendarWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.laCalendarWindow.Location = new System.Drawing.Point(248, 0);
            this.laCalendarWindow.Name = "laCalendarWindow";
            this.laCalendarWindow.Size = new System.Drawing.Size(171, 31);
            this.laCalendarWindow.TabIndex = 3;
            this.laCalendarWindow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // laAdvertiser
            // 
            this.laAdvertiser.Dock = System.Windows.Forms.DockStyle.Left;
            this.laAdvertiser.Location = new System.Drawing.Point(0, 0);
            this.laAdvertiser.Name = "laAdvertiser";
            this.laAdvertiser.Size = new System.Drawing.Size(248, 31);
            this.laAdvertiser.TabIndex = 4;
            this.laAdvertiser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // laCalendarName
            // 
            this.laCalendarName.Dock = System.Windows.Forms.DockStyle.Right;
            this.laCalendarName.Location = new System.Drawing.Point(419, 0);
            this.laCalendarName.Name = "laCalendarName";
            this.laCalendarName.Size = new System.Drawing.Size(230, 31);
            this.laCalendarName.TabIndex = 1;
            this.laCalendarName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // xtraScrollableControlMain
            // 
            this.xtraScrollableControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControlMain.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControlMain.Name = "xtraScrollableControlMain";
            this.xtraScrollableControlMain.Size = new System.Drawing.Size(653, 519);
            this.xtraScrollableControlMain.TabIndex = 0;
            // 
            // pnEmpty
            // 
            this.pnEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnEmpty.Location = new System.Drawing.Point(0, 0);
            this.pnEmpty.Name = "pnEmpty";
            this.pnEmpty.Size = new System.Drawing.Size(653, 519);
            this.pnEmpty.TabIndex = 3;
            // 
            // dockManager
            // 
            this.dockManager.DockingOptions.FloatOnDblClick = false;
            this.dockManager.DockingOptions.ShowAutoHideButton = false;
            this.dockManager.Form = this;
            this.dockManager.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanelDayProperties,
            this.dockPanelSlideInfo});
            this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            this.dockManager.Sizing += new DevExpress.XtraBars.Docking.SizingEventHandler(this.dockManager_Sizing);
            // 
            // dockPanelDayProperties
            // 
            this.dockPanelDayProperties.Controls.Add(this.dockPanelDayProperties_Container);
            this.dockPanelDayProperties.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
            this.dockPanelDayProperties.FloatLocation = new System.Drawing.Point(500, 200);
            this.dockPanelDayProperties.FloatSize = new System.Drawing.Size(300, 650);
            this.dockPanelDayProperties.ID = new System.Guid("5efc064b-cdb3-4a88-a882-a8a8d5885097");
            this.dockPanelDayProperties.Location = new System.Drawing.Point(-32768, -32768);
            this.dockPanelDayProperties.Name = "dockPanelDayProperties";
            this.dockPanelDayProperties.Options.AllowDockBottom = false;
            this.dockPanelDayProperties.Options.AllowDockFill = false;
            this.dockPanelDayProperties.Options.AllowDockLeft = false;
            this.dockPanelDayProperties.Options.AllowDockTop = false;
            this.dockPanelDayProperties.Options.ShowAutoHideButton = false;
            this.dockPanelDayProperties.Options.ShowMaximizeButton = false;
            this.dockPanelDayProperties.OriginalSize = new System.Drawing.Size(300, 200);
            this.dockPanelDayProperties.SavedIndex = 0;
            this.dockPanelDayProperties.Size = new System.Drawing.Size(300, 650);
            this.dockPanelDayProperties.Text = "Day Properties";
            this.dockPanelDayProperties.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            this.dockPanelDayProperties.ClosingPanel += new DevExpress.XtraBars.Docking.DockPanelCancelEventHandler(this.dockPanelDayProperties_ClosingPanel);
            this.dockPanelDayProperties.DockChanged += new System.EventHandler(this.dockPanelDayProperties_DockChanged);
            this.dockPanelDayProperties.DoubleClick += new System.EventHandler(this.dockPanelDayProperties_DoubleClick);
            // 
            // dockPanelDayProperties_Container
            // 
            this.dockPanelDayProperties_Container.Controls.Add(this.dayPropertiesControl);
            this.dockPanelDayProperties_Container.Location = new System.Drawing.Point(1, 23);
            this.dockPanelDayProperties_Container.Name = "dockPanelDayProperties_Container";
            this.dockPanelDayProperties_Container.Size = new System.Drawing.Size(298, 626);
            this.dockPanelDayProperties_Container.TabIndex = 0;
            // 
            // dayPropertiesControl
            // 
            this.dayPropertiesControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.dayPropertiesControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dayPropertiesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dayPropertiesControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dayPropertiesControl.Location = new System.Drawing.Point(0, 0);
            this.dayPropertiesControl.Name = "dayPropertiesControl";
            this.dayPropertiesControl.SettingsNotSaved = false;
            this.dayPropertiesControl.Size = new System.Drawing.Size(298, 626);
            this.dayPropertiesControl.TabIndex = 0;
            this.dayPropertiesControl.PropertiesApplied += new System.EventHandler(this.dayPropertiesControl_PropertiesApplied);
            this.dayPropertiesControl.PropertiesClosed += new System.EventHandler(this.dayPropertiesControl_PropertiesClosed);
            this.dayPropertiesControl.PropertiesGroupChanged += new DevExpress.XtraNavBar.NavBarGroupEventHandler(this.dayPropertiesControl_PropertiesGroupChanged);
            // 
            // dockPanelSlideInfo
            // 
            this.dockPanelSlideInfo.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dockPanelSlideInfo.Appearance.Options.UseFont = true;
            this.dockPanelSlideInfo.Controls.Add(this.dockPanelSlideInfo_Container);
            this.dockPanelSlideInfo.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
            this.dockPanelSlideInfo.FloatLocation = new System.Drawing.Point(200, 200);
            this.dockPanelSlideInfo.FloatSize = new System.Drawing.Size(300, 650);
            this.dockPanelSlideInfo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dockPanelSlideInfo.ID = new System.Guid("2d8b0597-b069-4719-bcbe-3171d0a38c9c");
            this.dockPanelSlideInfo.Location = new System.Drawing.Point(-32768, -32768);
            this.dockPanelSlideInfo.Name = "dockPanelSlideInfo";
            this.dockPanelSlideInfo.Options.AllowDockBottom = false;
            this.dockPanelSlideInfo.Options.AllowDockFill = false;
            this.dockPanelSlideInfo.Options.AllowDockRight = false;
            this.dockPanelSlideInfo.Options.AllowDockTop = false;
            this.dockPanelSlideInfo.Options.ShowAutoHideButton = false;
            this.dockPanelSlideInfo.Options.ShowMaximizeButton = false;
            this.dockPanelSlideInfo.OriginalSize = new System.Drawing.Size(300, 200);
            this.dockPanelSlideInfo.SavedIndex = 0;
            this.dockPanelSlideInfo.Size = new System.Drawing.Size(300, 650);
            this.dockPanelSlideInfo.Text = "Slide Info";
            this.dockPanelSlideInfo.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            this.dockPanelSlideInfo.ClosedPanel += new DevExpress.XtraBars.Docking.DockPanelEventHandler(this.dockPanelSlideInfo_ClosedPanel);
            this.dockPanelSlideInfo.DockChanged += new System.EventHandler(this.dockPanelSlideInfo_DockChanged);
            this.dockPanelSlideInfo.DoubleClick += new System.EventHandler(this.dockPanelSlideInfo_DoubleClick);
            // 
            // dockPanelSlideInfo_Container
            // 
            this.dockPanelSlideInfo_Container.Controls.Add(this.slideInfoControl);
            this.dockPanelSlideInfo_Container.Location = new System.Drawing.Point(1, 23);
            this.dockPanelSlideInfo_Container.Name = "dockPanelSlideInfo_Container";
            this.dockPanelSlideInfo_Container.Size = new System.Drawing.Size(298, 626);
            this.dockPanelSlideInfo_Container.TabIndex = 0;
            // 
            // slideInfoControl
            // 
            this.slideInfoControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.slideInfoControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.slideInfoControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slideInfoControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.slideInfoControl.Location = new System.Drawing.Point(0, 0);
            this.slideInfoControl.Name = "slideInfoControl";
            this.slideInfoControl.SettingsNotSaved = false;
            this.slideInfoControl.Size = new System.Drawing.Size(298, 626);
            this.slideInfoControl.TabIndex = 0;
            this.slideInfoControl.PropertiesApplied += new System.EventHandler(this.slideInfoControl_PropertiesApplied);
            this.slideInfoControl.PropertiesClosed += new System.EventHandler(this.slideInfoControl_PropertiesClosed);
            // 
            // pnMain
            // 
            this.pnMain.Controls.Add(this.xtraScrollableControlMain);
            this.pnMain.Controls.Add(this.gridControl);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 0);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(653, 519);
            this.pnMain.TabIndex = 4;
            // 
            // gridControl
            // 
            this.gridControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.gridControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.Name = "gridControl";
            this.gridControl.SettingsNotSaved = false;
            this.gridControl.Size = new System.Drawing.Size(653, 519);
            this.gridControl.TabIndex = 1;
            this.gridControl.PropertiesApplied += new System.EventHandler(this.gridControl_PropertiesApplied);
            this.gridControl.PropertiesClosed += new System.EventHandler(this.gridControl_PropertiesClosed);
            // 
            // CalendarControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnTop);
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.pnEmpty);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "CalendarControl";
            this.Size = new System.Drawing.Size(653, 519);
            this.pnTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            this.dockPanelDayProperties.ResumeLayout(false);
            this.dockPanelDayProperties_Container.ResumeLayout(false);
            this.dockPanelSlideInfo.ResumeLayout(false);
            this.dockPanelSlideInfo_Container.ResumeLayout(false);
            this.pnMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnTop;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Label laCalendarName;
        private DevExpress.XtraEditors.StyleController styleController;
        private System.Windows.Forms.Label laCalendarWindow;
        private System.Windows.Forms.Label laAdvertiser;
        private System.Windows.Forms.Panel pnEmpty;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControlMain;
        private DevExpress.XtraBars.Docking.DockManager dockManager;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelDayProperties;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanelDayProperties_Container;
        public DayProperties.DayPropertiesControl dayPropertiesControl;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelSlideInfo;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanelSlideInfo_Container;
        public SlideInfo.SlideInfoControl slideInfoControl;
        private System.Windows.Forms.Panel pnMain;
        private Grid.GridControl gridControl;
    }
}
