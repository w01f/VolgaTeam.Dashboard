namespace Asa.Browser.Controls.Controls
{
	partial class SiteContainerControl
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
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
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
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			((System.ComponentModel.ISupportInitialize)(this.barMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.SuspendLayout();
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
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
			this.barMain.Size = new System.Drawing.Size(800, 33);
			this.barMain.Stretch = true;
			this.barMain.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.barMain.TabIndex = 1;
			this.barMain.TabStop = false;
			this.barMain.Text = "Main Menu";
			// 
			// buttonItemMenuNavigationBack
			// 
			this.buttonItemMenuNavigationBack.Image = global::Asa.Browser.Controls.Properties.Resources.NavigationBack;
			this.buttonItemMenuNavigationBack.Name = "buttonItemMenuNavigationBack";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuNavigationBack, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Back", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(40, 20)));
			this.buttonItemMenuNavigationBack.Text = "buttonItem2";
			this.buttonItemMenuNavigationBack.Click += new System.EventHandler(this.OnMenuNavigationBackClick);
			// 
			// buttonItemMenuNavigationForward
			// 
			this.buttonItemMenuNavigationForward.Image = global::Asa.Browser.Controls.Properties.Resources.NavigationForward;
			this.buttonItemMenuNavigationForward.Name = "buttonItemMenuNavigationForward";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuNavigationForward, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Forward", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(50, 20)));
			this.buttonItemMenuNavigationForward.Text = "buttonItem1";
			this.buttonItemMenuNavigationForward.Click += new System.EventHandler(this.OnMenuNavigationForwardClick);
			// 
			// buttonItemMenuNavigationRefresh
			// 
			this.buttonItemMenuNavigationRefresh.Image = global::Asa.Browser.Controls.Properties.Resources.NavigationRefresh;
			this.buttonItemMenuNavigationRefresh.Name = "buttonItemMenuNavigationRefresh";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuNavigationRefresh, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Refresh", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, true, false, new System.Drawing.Size(50, 20)));
			this.buttonItemMenuNavigationRefresh.Text = "buttonItem1";
			this.buttonItemMenuNavigationRefresh.Click += new System.EventHandler(this.OnMenuNavigationRefreshClick);
			// 
			// buttonItemMenuBrowserChrome
			// 
			this.buttonItemMenuBrowserChrome.BeginGroup = true;
			this.buttonItemMenuBrowserChrome.Image = global::Asa.Browser.Controls.Properties.Resources.ExternalBrowserChrome;
			this.buttonItemMenuBrowserChrome.Name = "buttonItemMenuBrowserChrome";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuBrowserChrome, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Open in Chrome", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(90, 20)));
			this.buttonItemMenuBrowserChrome.Tag = "chrome";
			this.buttonItemMenuBrowserChrome.Text = "buttonItem1";
			this.buttonItemMenuBrowserChrome.Visible = false;
			this.buttonItemMenuBrowserChrome.Click += new System.EventHandler(this.OnExternalBrowserOpenClick);
			// 
			// buttonItemMenuBrowserFirefox
			// 
			this.buttonItemMenuBrowserFirefox.Image = global::Asa.Browser.Controls.Properties.Resources.ExternalBrowserFirefox;
			this.buttonItemMenuBrowserFirefox.Name = "buttonItemMenuBrowserFirefox";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuBrowserFirefox, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Open in Firefox", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(90, 20)));
			this.buttonItemMenuBrowserFirefox.Tag = "firefox";
			this.buttonItemMenuBrowserFirefox.Text = "buttonItem1";
			this.buttonItemMenuBrowserFirefox.Visible = false;
			this.buttonItemMenuBrowserFirefox.Click += new System.EventHandler(this.OnExternalBrowserOpenClick);
			// 
			// buttonItemMenuBrowserIE
			// 
			this.buttonItemMenuBrowserIE.Image = global::Asa.Browser.Controls.Properties.Resources.ExternalBrowserInternetExplorer;
			this.buttonItemMenuBrowserIE.Name = "buttonItemMenuBrowserIE";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuBrowserIE, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Open in Internet Explorer", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(140, 20)));
			this.buttonItemMenuBrowserIE.Tag = "iexplore";
			this.buttonItemMenuBrowserIE.Text = "buttonItem1";
			this.buttonItemMenuBrowserIE.Visible = false;
			this.buttonItemMenuBrowserIE.Click += new System.EventHandler(this.OnExternalBrowserOpenClick);
			// 
			// buttonItemMenuBrowserEdge
			// 
			this.buttonItemMenuBrowserEdge.Image = global::Asa.Browser.Controls.Properties.Resources.ExternalBrowserEdge;
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
			this.buttonItemDetails.Image = global::Asa.Browser.Controls.Properties.Resources.UrlDetailsMenu;
			this.buttonItemDetails.Name = "buttonItemDetails";
			this.superTooltip.SetSuperTooltip(this.buttonItemDetails, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "URL Info", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(50, 20)));
			this.buttonItemDetails.Text = "buttonItem1";
			this.buttonItemDetails.Click += new System.EventHandler(this.OnMenuDetailsClick);
			// 
			// buttonItemFloater
			// 
			this.buttonItemFloater.BeginGroup = true;
			this.buttonItemFloater.Image = global::Asa.Browser.Controls.Properties.Resources.FloaterMenu;
			this.buttonItemFloater.Name = "buttonItemFloater";
			this.superTooltip.SetSuperTooltip(this.buttonItemFloater, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Mini-Floater", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(67, 20)));
			this.buttonItemFloater.Text = "buttonItem1";
			this.buttonItemFloater.Click += new System.EventHandler(this.OnFloaterClick);
			// 
			// buttonItemMenuExtensionsAddSlide
			// 
			this.buttonItemMenuExtensionsAddSlide.BeginGroup = true;
			this.buttonItemMenuExtensionsAddSlide.Image = global::Asa.Browser.Controls.Properties.Resources.ExtensionsAddSlide;
			this.buttonItemMenuExtensionsAddSlide.Name = "buttonItemMenuExtensionsAddSlide";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuExtensionsAddSlide, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Insert Slide", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(70, 20)));
			this.buttonItemMenuExtensionsAddSlide.Text = "buttonItem1";
			this.buttonItemMenuExtensionsAddSlide.Visible = false;
			this.buttonItemMenuExtensionsAddSlide.Click += new System.EventHandler(this.OnMenuExtensionsAddSlideClick);
			// 
			// buttonItemMenuExtensionsAddSlides
			// 
			this.buttonItemMenuExtensionsAddSlides.Image = global::Asa.Browser.Controls.Properties.Resources.ExtensionsAddSlides;
			this.buttonItemMenuExtensionsAddSlides.Name = "buttonItemMenuExtensionsAddSlides";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuExtensionsAddSlides, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Add all slides", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(80, 20)));
			this.buttonItemMenuExtensionsAddSlides.Text = "buttonItem1";
			this.buttonItemMenuExtensionsAddSlides.Visible = false;
			this.buttonItemMenuExtensionsAddSlides.Click += new System.EventHandler(this.OnMenuExtensionsAddSlidesClick);
			// 
			// buttonItemMenuExtensionsPrint
			// 
			this.buttonItemMenuExtensionsPrint.BeginGroup = true;
			this.buttonItemMenuExtensionsPrint.Image = global::Asa.Browser.Controls.Properties.Resources.ExtensionsPrint;
			this.buttonItemMenuExtensionsPrint.Name = "buttonItemMenuExtensionsPrint";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuExtensionsPrint, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Print file", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, true, true, new System.Drawing.Size(60, 20)));
			this.buttonItemMenuExtensionsPrint.Click += new System.EventHandler(this.OnMenuExtensionsPrintClick);
			// 
			// buttonItemMenuExtensionsAddVideo
			// 
			this.buttonItemMenuExtensionsAddVideo.BeginGroup = true;
			this.buttonItemMenuExtensionsAddVideo.Image = global::Asa.Browser.Controls.Properties.Resources.ExtensionsAddVideo;
			this.buttonItemMenuExtensionsAddVideo.Name = "buttonItemMenuExtensionsAddVideo";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuExtensionsAddVideo, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Add this video to your slide", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(150, 20)));
			this.buttonItemMenuExtensionsAddVideo.Text = "buttonItem1";
			this.buttonItemMenuExtensionsAddVideo.Visible = false;
			this.buttonItemMenuExtensionsAddVideo.Click += new System.EventHandler(this.OnMenuExtensionsAddVideoClick);
			// 
			// buttonItemMenuExtensionsDownloadYouTube
			// 
			this.buttonItemMenuExtensionsDownloadYouTube.BeginGroup = true;
			this.buttonItemMenuExtensionsDownloadYouTube.Image = global::Asa.Browser.Controls.Properties.Resources.ExtensionsDownloadYouTube;
			this.buttonItemMenuExtensionsDownloadYouTube.Name = "buttonItemMenuExtensionsDownloadYouTube";
			this.superTooltip.SetSuperTooltip(this.buttonItemMenuExtensionsDownloadYouTube, new DevComponents.DotNetBar.SuperTooltipInfo("", "", "Save a copy of this MP4 file", null, null, DevComponents.DotNetBar.eTooltipColor.Gray, false, false, new System.Drawing.Size(150, 20)));
			this.buttonItemMenuExtensionsDownloadYouTube.Text = "buttonItem1";
			this.buttonItemMenuExtensionsDownloadYouTube.Visible = false;
			this.buttonItemMenuExtensionsDownloadYouTube.Click += new System.EventHandler(this.OnMenuExtensionsDownloadYouTubeClick);
			// 
			// labelItemMenuWarning
			// 
			this.labelItemMenuWarning.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelItemMenuWarning.ForeColor = System.Drawing.Color.Gray;
			this.labelItemMenuWarning.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.labelItemMenuWarning.Name = "labelItemMenuWarning";
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
			this.xtraTabControl.Size = new System.Drawing.Size(800, 474);
			this.xtraTabControl.TabIndex = 2;
			// 
			// SiteContainerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.xtraTabControl);
			this.Controls.Add(this.barMain);
			this.Name = "SiteContainerControl";
			this.Size = new System.Drawing.Size(800, 507);
			((System.ComponentModel.ISupportInitialize)(this.barMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.SuperTooltip superTooltip;
		public DevComponents.DotNetBar.Bar barMain;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuNavigationBack;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuNavigationForward;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuNavigationRefresh;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuBrowserChrome;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuBrowserFirefox;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuBrowserIE;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuBrowserEdge;
		private DevComponents.DotNetBar.ButtonItem buttonItemDetails;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsAddSlide;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsAddSlides;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsPrint;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsAddVideo;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsDownloadYouTube;
		private DevComponents.DotNetBar.LabelItem labelItemMenuWarning;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		protected DevComponents.DotNetBar.ButtonItem buttonItemFloater;
	}
}
