namespace AdSalesBrowser
{
	partial class FormMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.styleManager = new DevComponents.DotNetBar.StyleManager(this.components);
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.barMain = new DevComponents.DotNetBar.Bar();
			this.buttonItemMenuNavigationBack = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuNavigationForward = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuNavigationRefresh = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuBrowserChrome = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuBrowserFirefox = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuBrowserIE = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuBrowserEdge = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemDetails = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemFloater = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuExtensionsAddSlide = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuExtensionsAddSlides = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuExtensionsPrint = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuExtensionsAddVideo = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuExtensionsDownloadYouTube = new DevComponents.DotNetBar.ButtonItem();
			this.labelItemMenuWarning = new DevComponents.DotNetBar.LabelItem();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barMain)).BeginInit();
			this.SuspendLayout();
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
			// 
			// xtraTabControl
			// 
			this.xtraTabControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.Appearance.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.Header.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.Header.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
			this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.HeaderActive.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseTextOptions = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
			this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControl.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
			this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControl.Location = new System.Drawing.Point(0, 33);
			this.xtraTabControl.MaxTabPageWidth = 200;
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
			this.xtraTabControl.Size = new System.Drawing.Size(737, 522);
			this.xtraTabControl.TabIndex = 0;
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013";
			// 
			// barMain
			// 
			this.barMain.AntiAlias = true;
			this.barMain.AutoCreateCaptionMenu = false;
			this.barMain.CanCustomize = false;
			this.barMain.CanDockBottom = false;
			this.barMain.CanDockLeft = false;
			this.barMain.CanDockRight = false;
			this.barMain.CanDockTab = false;
			this.barMain.CanDockTop = false;
			this.barMain.CanMaximizeFloating = false;
			this.barMain.CanMove = false;
			this.barMain.CanReorderTabs = false;
			this.barMain.CanUndock = false;
			this.barMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.barMain.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.barMain.IsMaximized = false;
			this.barMain.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemMenuNavigationBack,
            this.buttonItemMenuNavigationForward,
            this.buttonItemMenuNavigationRefresh,
            this.buttonItemMenuBrowserChrome,
            this.buttonItemMenuBrowserFirefox,
            this.buttonItemMenuBrowserIE,
            this.buttonItemMenuBrowserEdge,
            this.buttonItemDetails,
            this.buttonItemFloater,
            this.buttonItemMenuExtensionsAddSlide,
            this.buttonItemMenuExtensionsAddSlides,
            this.buttonItemMenuExtensionsPrint,
            this.buttonItemMenuExtensionsAddVideo,
            this.buttonItemMenuExtensionsDownloadYouTube,
            this.labelItemMenuWarning});
			this.barMain.Location = new System.Drawing.Point(0, 0);
			this.barMain.Name = "barMain";
			this.barMain.Size = new System.Drawing.Size(737, 33);
			this.barMain.Stretch = true;
			this.barMain.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.barMain.TabIndex = 0;
			this.barMain.TabStop = false;
			this.barMain.Text = "Main Menu";
			// 
			// buttonItemMenuNavigationBack
			// 
			this.buttonItemMenuNavigationBack.Image = global::AdSalesBrowser.Properties.Resources.NavigationBack;
			this.buttonItemMenuNavigationBack.Name = "buttonItemMenuNavigationBack";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuNavigationBack, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Back", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(40, 20)));
			this.buttonItemMenuNavigationBack.Text = "buttonItem2";
			this.buttonItemMenuNavigationBack.Click += new System.EventHandler(this.buttonItemMenuNavigationBack_Click);
			// 
			// buttonItemMenuNavigationForward
			// 
			this.buttonItemMenuNavigationForward.Image = global::AdSalesBrowser.Properties.Resources.NavigationForward;
			this.buttonItemMenuNavigationForward.Name = "buttonItemMenuNavigationForward";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuNavigationForward, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Forward", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(50, 20)));
			this.buttonItemMenuNavigationForward.Text = "buttonItem1";
			this.buttonItemMenuNavigationForward.Click += new System.EventHandler(this.buttonItemMenuNavigationForward_Click);
			// 
			// buttonItemMenuNavigationRefresh
			// 
			this.buttonItemMenuNavigationRefresh.Image = global::AdSalesBrowser.Properties.Resources.NavigationRefresh;
			this.buttonItemMenuNavigationRefresh.Name = "buttonItemMenuNavigationRefresh";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuNavigationRefresh, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Refresh", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, true, false, new System.Drawing.Size(50, 20)));
			this.buttonItemMenuNavigationRefresh.Text = "buttonItem1";
			this.buttonItemMenuNavigationRefresh.Click += new System.EventHandler(this.buttonItemMenuNavigationRefresh_Click);
			// 
			// buttonItemMenuBrowserChrome
			// 
			this.buttonItemMenuBrowserChrome.BeginGroup = true;
			this.buttonItemMenuBrowserChrome.Image = global::AdSalesBrowser.Properties.Resources.ch;
			this.buttonItemMenuBrowserChrome.Name = "buttonItemMenuBrowserChrome";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuBrowserChrome, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Open in Chrome", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(90, 20)));
			this.buttonItemMenuBrowserChrome.Tag = "chrome";
			this.buttonItemMenuBrowserChrome.Text = "buttonItem1";
			this.buttonItemMenuBrowserChrome.Visible = false;
			this.buttonItemMenuBrowserChrome.Click += new System.EventHandler(this.OnExternalBrowserOpenClick);
			// 
			// buttonItemMenuBrowserFirefox
			// 
			this.buttonItemMenuBrowserFirefox.Image = global::AdSalesBrowser.Properties.Resources.ff;
			this.buttonItemMenuBrowserFirefox.Name = "buttonItemMenuBrowserFirefox";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuBrowserFirefox, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Open in Firefox", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(90, 20)));
			this.buttonItemMenuBrowserFirefox.Tag = "firefox";
			this.buttonItemMenuBrowserFirefox.Text = "buttonItem1";
			this.buttonItemMenuBrowserFirefox.Visible = false;
			this.buttonItemMenuBrowserFirefox.Click += new System.EventHandler(this.OnExternalBrowserOpenClick);
			// 
			// buttonItemMenuBrowserIE
			// 
			this.buttonItemMenuBrowserIE.Image = global::AdSalesBrowser.Properties.Resources.ie;
			this.buttonItemMenuBrowserIE.Name = "buttonItemMenuBrowserIE";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuBrowserIE, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Open in Internet Explorer", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(140, 20)));
			this.buttonItemMenuBrowserIE.Tag = "iexplore";
			this.buttonItemMenuBrowserIE.Text = "buttonItem1";
			this.buttonItemMenuBrowserIE.Visible = false;
			this.buttonItemMenuBrowserIE.Click += new System.EventHandler(this.OnExternalBrowserOpenClick);
			// 
			// buttonItemMenuBrowserEdge
			// 
			this.buttonItemMenuBrowserEdge.Image = global::AdSalesBrowser.Properties.Resources.ed;
			this.buttonItemMenuBrowserEdge.Name = "buttonItemMenuBrowserEdge";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuBrowserEdge, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Open in Edge", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(80, 20)));
			this.buttonItemMenuBrowserEdge.Tag = "edge";
			this.buttonItemMenuBrowserEdge.Text = "buttonItem1";
			this.buttonItemMenuBrowserEdge.Visible = false;
			this.buttonItemMenuBrowserEdge.Click += new System.EventHandler(this.OnExternalBrowserOpenClick);
			// 
			// buttonItemDetails
			// 
			this.buttonItemDetails.BeginGroup = true;
			this.buttonItemDetails.Image = global::AdSalesBrowser.Properties.Resources.UrlDetailsMenu;
			this.buttonItemDetails.Name = "buttonItemDetails";
			this.superTooltip.SetSuperTooltip(this.buttonItemDetails, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "URL Info", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(50, 20)));
			this.buttonItemDetails.Text = "buttonItem1";
			this.buttonItemDetails.Click += new System.EventHandler(this.buttonItemDetails_Click);
			// 
			// buttonItemFloater
			// 
			this.buttonItemFloater.BeginGroup = true;
			this.buttonItemFloater.Image = global::AdSalesBrowser.Properties.Resources.FloaterMenu;
			this.buttonItemFloater.Name = "buttonItemFloater";
			this.superTooltip.SetSuperTooltip(this.buttonItemFloater, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Mini-Floater", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(67, 20)));
			this.buttonItemFloater.Text = "buttonItem1";
			this.buttonItemFloater.Click += new System.EventHandler(this.OnFloaterClick);
			// 
			// buttonItemMenuExtensionsAddSlide
			// 
			this.buttonItemMenuExtensionsAddSlide.BeginGroup = true;
			this.buttonItemMenuExtensionsAddSlide.Image = global::AdSalesBrowser.Properties.Resources.ExtensionsAddSlide;
			this.buttonItemMenuExtensionsAddSlide.Name = "buttonItemMenuExtensionsAddSlide";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuExtensionsAddSlide, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Insert Slide", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(70, 20)));
			this.buttonItemMenuExtensionsAddSlide.Text = "buttonItem1";
			this.buttonItemMenuExtensionsAddSlide.Visible = false;
			this.buttonItemMenuExtensionsAddSlide.Click += new System.EventHandler(this.buttonItemMenuExtensionsAddSlide_Click);
			// 
			// buttonItemMenuExtensionsAddSlides
			// 
			this.buttonItemMenuExtensionsAddSlides.Image = global::AdSalesBrowser.Properties.Resources.ExtensionsAddSlides;
			this.buttonItemMenuExtensionsAddSlides.Name = "buttonItemMenuExtensionsAddSlides";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuExtensionsAddSlides, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Add all slides", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(80, 20)));
			this.buttonItemMenuExtensionsAddSlides.Text = "buttonItem1";
			this.buttonItemMenuExtensionsAddSlides.Visible = false;
			this.buttonItemMenuExtensionsAddSlides.Click += new System.EventHandler(this.buttonItemMenuExtensionsAddSlides_Click);
			// 
			// buttonItemMenuExtensionsPrint
			// 
			this.buttonItemMenuExtensionsPrint.BeginGroup = true;
			this.buttonItemMenuExtensionsPrint.Image = global::AdSalesBrowser.Properties.Resources.ExtensionsPrint;
			this.buttonItemMenuExtensionsPrint.Name = "buttonItemMenuExtensionsPrint";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuExtensionsPrint, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Print file", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, true, true, new System.Drawing.Size(60, 20)));
			this.buttonItemMenuExtensionsPrint.Click += new System.EventHandler(this.buttonItemMenuExtensionsPrint_Click);
			// 
			// buttonItemMenuExtensionsAddVideo
			// 
			this.buttonItemMenuExtensionsAddVideo.BeginGroup = true;
			this.buttonItemMenuExtensionsAddVideo.Image = global::AdSalesBrowser.Properties.Resources.ExtensionsAddVideo;
			this.buttonItemMenuExtensionsAddVideo.Name = "buttonItemMenuExtensionsAddVideo";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuExtensionsAddVideo, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Add this video to your slide", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(150, 20)));
			this.buttonItemMenuExtensionsAddVideo.Text = "buttonItem1";
			this.buttonItemMenuExtensionsAddVideo.Visible = false;
			this.buttonItemMenuExtensionsAddVideo.Click += new System.EventHandler(this.buttonItemMenuExtensionsAddVideo_Click);
			// 
			// buttonItemMenuExtensionsDownloadYouTube
			// 
			this.buttonItemMenuExtensionsDownloadYouTube.BeginGroup = true;
			this.buttonItemMenuExtensionsDownloadYouTube.Image = global::AdSalesBrowser.Properties.Resources.ExtensionsDownloadYouTube;
			this.buttonItemMenuExtensionsDownloadYouTube.Name = "buttonItemMenuExtensionsDownloadYouTube";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuExtensionsDownloadYouTube, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Save a copy of this MP4 file", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(150, 20)));
			this.buttonItemMenuExtensionsDownloadYouTube.Text = "buttonItem1";
			this.buttonItemMenuExtensionsDownloadYouTube.Visible = false;
			this.buttonItemMenuExtensionsDownloadYouTube.Click += new System.EventHandler(this.buttonItemMenuExtensionsDownloadYouTube_Click);
			// 
			// labelItemMenuWarning
			// 
			this.labelItemMenuWarning.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelItemMenuWarning.ForeColor = System.Drawing.Color.Gray;
			this.labelItemMenuWarning.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.labelItemMenuWarning.Name = "labelItemMenuWarning";
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// FormMain
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(737, 555);
			this.Controls.Add(this.xtraTabControl);
			this.Controls.Add(this.barMain);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Browser";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barMain)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		public DevComponents.DotNetBar.Bar barMain;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuBrowserChrome;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuNavigationBack;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuNavigationForward;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuNavigationRefresh;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuBrowserFirefox;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuBrowserIE;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuBrowserEdge;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsAddSlide;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsAddSlides;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsAddVideo;
		private DevComponents.DotNetBar.SuperTooltip superTooltip;
		private DevComponents.DotNetBar.LabelItem labelItemMenuWarning;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsDownloadYouTube;
		private DevComponents.DotNetBar.ButtonItem buttonItemDetails;
		private DevComponents.DotNetBar.ButtonItem buttonItemFloater;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsPrint;
	}
}

