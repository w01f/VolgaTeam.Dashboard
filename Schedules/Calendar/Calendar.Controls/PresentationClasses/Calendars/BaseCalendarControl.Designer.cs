namespace NewBizWiz.Calendar.Controls.PresentationClasses.Calendars
{
	public partial class BaseCalendarControl
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
			DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
			this.pnTop = new System.Windows.Forms.Panel();
			this.hyperLinkEditReset = new DevExpress.XtraEditors.HyperLinkEdit();
			this.laCalendarWindow = new System.Windows.Forms.Label();
			this.laCalendarName = new System.Windows.Forms.Label();
			this.laAdvertiser = new System.Windows.Forms.Label();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.pnEmpty = new System.Windows.Forms.Panel();
			this.dockManager = new DevExpress.XtraBars.Docking.DockManager();
			this.dockPanelSlideInfo = new DevExpress.XtraBars.Docking.DockPanel();
			this.dockPanelSlideInfo_Container = new DevExpress.XtraBars.Docking.ControlContainer();
			this.dockPanelDayProperties = new DevExpress.XtraBars.Docking.DockPanel();
			this.dockPanelDayProperties_Container = new DevExpress.XtraBars.Docking.ControlContainer();
			this.pnMain = new System.Windows.Forms.Panel();
			this.pictureBoxNoData = new System.Windows.Forms.PictureBox();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditReset.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
			this.dockPanelSlideInfo.SuspendLayout();
			this.dockPanelDayProperties.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxNoData)).BeginInit();
			this.SuspendLayout();
			// 
			// pnTop
			// 
			this.pnTop.Controls.Add(this.hyperLinkEditReset);
			this.pnTop.Controls.Add(this.laCalendarWindow);
			this.pnTop.Controls.Add(this.laCalendarName);
			this.pnTop.Controls.Add(this.laAdvertiser);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.Location = new System.Drawing.Point(0, 0);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(653, 35);
			this.pnTop.TabIndex = 1;
			// 
			// hyperLinkEditReset
			// 
			this.hyperLinkEditReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.hyperLinkEditReset.EditValue = "Reset";
			this.hyperLinkEditReset.Location = new System.Drawing.Point(586, 4);
			this.hyperLinkEditReset.Name = "hyperLinkEditReset";
			this.hyperLinkEditReset.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.hyperLinkEditReset.Properties.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.hyperLinkEditReset.Properties.Appearance.ForeColor = System.Drawing.Color.DarkBlue;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseBackColor = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseFont = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseForeColor = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseTextOptions = true;
			this.hyperLinkEditReset.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.hyperLinkEditReset.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.hyperLinkEditReset.Size = new System.Drawing.Size(64, 22);
			toolTipItem2.Text = "Reset original default data";
			superToolTip2.Items.Add(toolTipItem2);
			this.hyperLinkEditReset.SuperTip = superToolTip2;
			this.hyperLinkEditReset.TabIndex = 104;
			this.hyperLinkEditReset.Visible = false;
			// 
			// laCalendarWindow
			// 
			this.laCalendarWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laCalendarWindow.Location = new System.Drawing.Point(248, 0);
			this.laCalendarWindow.Name = "laCalendarWindow";
			this.laCalendarWindow.Size = new System.Drawing.Size(187, 35);
			this.laCalendarWindow.TabIndex = 3;
			this.laCalendarWindow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// laCalendarName
			// 
			this.laCalendarName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laCalendarName.Location = new System.Drawing.Point(441, 0);
			this.laCalendarName.Name = "laCalendarName";
			this.laCalendarName.Size = new System.Drawing.Size(211, 35);
			this.laCalendarName.TabIndex = 1;
			this.laCalendarName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// laAdvertiser
			// 
			this.laAdvertiser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.laAdvertiser.Location = new System.Drawing.Point(0, 0);
			this.laAdvertiser.Name = "laAdvertiser";
			this.laAdvertiser.Size = new System.Drawing.Size(248, 35);
			this.laAdvertiser.TabIndex = 4;
			this.laAdvertiser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			// pnEmpty
			// 
			this.pnEmpty.Location = new System.Drawing.Point(404, 59);
			this.pnEmpty.Name = "pnEmpty";
			this.pnEmpty.Size = new System.Drawing.Size(246, 175);
			this.pnEmpty.TabIndex = 3;
			// 
			// dockManager
			// 
			this.dockManager.DockingOptions.FloatOnDblClick = false;
			this.dockManager.DockingOptions.ShowAutoHideButton = false;
			this.dockManager.Form = this;
			this.dockManager.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanelSlideInfo,
            this.dockPanelDayProperties});
			this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
			this.dockManager.Sizing += new DevExpress.XtraBars.Docking.SizingEventHandler(this.dockManager_Sizing);
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
			// 
			// dockPanelSlideInfo_Container
			// 
			this.dockPanelSlideInfo_Container.Location = new System.Drawing.Point(1, 23);
			this.dockPanelSlideInfo_Container.Name = "dockPanelSlideInfo_Container";
			this.dockPanelSlideInfo_Container.Size = new System.Drawing.Size(298, 626);
			this.dockPanelSlideInfo_Container.TabIndex = 0;
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
			// 
			// dockPanelDayProperties_Container
			// 
			this.dockPanelDayProperties_Container.Location = new System.Drawing.Point(1, 23);
			this.dockPanelDayProperties_Container.Name = "dockPanelDayProperties_Container";
			this.dockPanelDayProperties_Container.Size = new System.Drawing.Size(298, 626);
			this.dockPanelDayProperties_Container.TabIndex = 0;
			// 
			// pnMain
			// 
			this.pnMain.Location = new System.Drawing.Point(5, 59);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(277, 175);
			this.pnMain.TabIndex = 4;
			// 
			// pictureBoxNoData
			// 
			this.pictureBoxNoData.BackColor = System.Drawing.Color.White;
			this.pictureBoxNoData.Location = new System.Drawing.Point(239, 279);
			this.pictureBoxNoData.Name = "pictureBoxNoData";
			this.pictureBoxNoData.Size = new System.Drawing.Size(194, 183);
			this.pictureBoxNoData.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxNoData.TabIndex = 5;
			this.pictureBoxNoData.TabStop = false;
			this.pictureBoxNoData.Visible = false;
			// 
			// BaseCalendarControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pictureBoxNoData);
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.pnTop);
			this.Controls.Add(this.pnEmpty);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "BaseCalendarControl";
			this.Size = new System.Drawing.Size(653, 519);
			this.pnTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditReset.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
			this.dockPanelSlideInfo.ResumeLayout(false);
			this.dockPanelDayProperties.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxNoData)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		protected System.Windows.Forms.Panel pnTop;
		private DevExpress.XtraEditors.StyleController styleController;
        private System.Windows.Forms.Panel pnEmpty;
        private DevExpress.XtraBars.Docking.DockManager dockManager;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelDayProperties;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanelDayProperties_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelSlideInfo;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanelSlideInfo_Container;
		protected System.Windows.Forms.Panel pnMain;
		protected System.Windows.Forms.PictureBox pictureBoxNoData;
		protected System.Windows.Forms.Label laCalendarName;
		protected System.Windows.Forms.Label laCalendarWindow;
		protected System.Windows.Forms.Label laAdvertiser;
		protected DevExpress.XtraEditors.HyperLinkEdit hyperLinkEditReset;
    }
}
