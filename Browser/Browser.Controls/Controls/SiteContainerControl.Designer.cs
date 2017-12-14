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
			this.buttonItemMenuNavigationBack = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuNavigationForward = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuNavigationRefresh = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuBrowserChrome = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuBrowserFirefox = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuBrowserIE = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuBrowserEdge = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemFloater = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuExtensionsAddSlide = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuExtensionsAddSlides = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuExtensionsPrint = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuExtensionsAddVideo = new DevComponents.DotNetBar.ButtonItem();
			this.buttonItemMenuExtensionsDownloadYouTube = new DevComponents.DotNetBar.ButtonItem();
			this.barMain = new DevComponents.DotNetBar.Bar();
			this.labelItemMenuWarning = new DevComponents.DotNetBar.LabelItem();
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			((System.ComponentModel.ISupportInitialize)(this.barMain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonItemMenuNavigationBack
			// 
			this.buttonItemMenuNavigationBack.BeginGroup = true;
			this.buttonItemMenuNavigationBack.Image = global::Asa.Browser.Controls.Properties.Resources.NavigationBack;
			this.buttonItemMenuNavigationBack.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.buttonItemMenuNavigationBack.Name = "buttonItemMenuNavigationBack";
			this.buttonItemMenuNavigationBack.Text = "buttonItem2";
			this.buttonItemMenuNavigationBack.Tooltip = "Back";
			this.buttonItemMenuNavigationBack.Click += new System.EventHandler(this.OnMenuNavigationBackClick);
			// 
			// buttonItemMenuNavigationForward
			// 
			this.buttonItemMenuNavigationForward.Image = global::Asa.Browser.Controls.Properties.Resources.NavigationForward;
			this.buttonItemMenuNavigationForward.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.buttonItemMenuNavigationForward.Name = "buttonItemMenuNavigationForward";
			this.buttonItemMenuNavigationForward.Text = "buttonItem1";
			this.buttonItemMenuNavigationForward.Tooltip = "Forward";
			this.buttonItemMenuNavigationForward.Click += new System.EventHandler(this.OnMenuNavigationForwardClick);
			// 
			// buttonItemMenuNavigationRefresh
			// 
			this.buttonItemMenuNavigationRefresh.BeginGroup = true;
			this.buttonItemMenuNavigationRefresh.Image = global::Asa.Browser.Controls.Properties.Resources.NavigationRefresh;
			this.buttonItemMenuNavigationRefresh.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
			this.buttonItemMenuNavigationRefresh.Name = "buttonItemMenuNavigationRefresh";
			this.buttonItemMenuNavigationRefresh.Text = "buttonItem1";
			this.buttonItemMenuNavigationRefresh.Tooltip = "Reload Page";
			this.buttonItemMenuNavigationRefresh.Click += new System.EventHandler(this.OnMenuNavigationRefreshClick);
			// 
			// buttonItemMenuBrowserChrome
			// 
			this.buttonItemMenuBrowserChrome.BeginGroup = true;
			this.buttonItemMenuBrowserChrome.Image = global::Asa.Browser.Controls.Properties.Resources.ExternalBrowserChrome;
			this.buttonItemMenuBrowserChrome.Name = "buttonItemMenuBrowserChrome";
			this.buttonItemMenuBrowserChrome.Tag = "chrome";
			this.buttonItemMenuBrowserChrome.Text = "buttonItem1";
			this.buttonItemMenuBrowserChrome.Tooltip = "Chrome";
			this.buttonItemMenuBrowserChrome.Visible = false;
			this.buttonItemMenuBrowserChrome.Click += new System.EventHandler(this.OnExternalBrowserOpenClick);
			// 
			// buttonItemMenuBrowserFirefox
			// 
			this.buttonItemMenuBrowserFirefox.Image = global::Asa.Browser.Controls.Properties.Resources.ExternalBrowserFirefox;
			this.buttonItemMenuBrowserFirefox.Name = "buttonItemMenuBrowserFirefox";
			this.buttonItemMenuBrowserFirefox.Tag = "firefox";
			this.buttonItemMenuBrowserFirefox.Text = "buttonItem1";
			this.buttonItemMenuBrowserFirefox.Tooltip = "Firefox";
			this.buttonItemMenuBrowserFirefox.Visible = false;
			this.buttonItemMenuBrowserFirefox.Click += new System.EventHandler(this.OnExternalBrowserOpenClick);
			// 
			// buttonItemMenuBrowserIE
			// 
			this.buttonItemMenuBrowserIE.Image = global::Asa.Browser.Controls.Properties.Resources.ExternalBrowserInternetExplorer;
			this.buttonItemMenuBrowserIE.Name = "buttonItemMenuBrowserIE";
			this.buttonItemMenuBrowserIE.Tag = "iexplore";
			this.buttonItemMenuBrowserIE.Text = "buttonItem1";
			this.buttonItemMenuBrowserIE.Tooltip = "IE";
			this.buttonItemMenuBrowserIE.Visible = false;
			this.buttonItemMenuBrowserIE.Click += new System.EventHandler(this.OnExternalBrowserOpenClick);
			// 
			// buttonItemMenuBrowserEdge
			// 
			this.buttonItemMenuBrowserEdge.Image = global::Asa.Browser.Controls.Properties.Resources.ExternalBrowserEdge;
			this.buttonItemMenuBrowserEdge.Name = "buttonItemMenuBrowserEdge";
			this.buttonItemMenuBrowserEdge.Tag = "edge";
			this.buttonItemMenuBrowserEdge.Text = "buttonItem1";
			this.buttonItemMenuBrowserEdge.Tooltip = "Edge";
			this.buttonItemMenuBrowserEdge.Visible = false;
			this.buttonItemMenuBrowserEdge.Click += new System.EventHandler(this.OnExternalBrowserOpenClick);
			// 
			// buttonItemFloater
			// 
			this.buttonItemFloater.BeginGroup = true;
			this.buttonItemFloater.Image = global::Asa.Browser.Controls.Properties.Resources.FloaterMenu;
			this.buttonItemFloater.Name = "buttonItemFloater";
			this.buttonItemFloater.Text = "buttonItem1";
			this.buttonItemFloater.Tooltip = "Floater";
			this.buttonItemFloater.Click += new System.EventHandler(this.OnFloaterClick);
			// 
			// buttonItemMenuExtensionsAddSlide
			// 
			this.buttonItemMenuExtensionsAddSlide.BeginGroup = true;
			this.buttonItemMenuExtensionsAddSlide.Image = global::Asa.Browser.Controls.Properties.Resources.ExtensionsAddSlide;
			this.buttonItemMenuExtensionsAddSlide.Name = "buttonItemMenuExtensionsAddSlide";
			this.buttonItemMenuExtensionsAddSlide.Text = "buttonItem1";
			this.buttonItemMenuExtensionsAddSlide.Tooltip = "Insert Slide";
			this.buttonItemMenuExtensionsAddSlide.Visible = false;
			this.buttonItemMenuExtensionsAddSlide.Click += new System.EventHandler(this.OnMenuExtensionsAddSlideClick);
			// 
			// buttonItemMenuExtensionsAddSlides
			// 
			this.buttonItemMenuExtensionsAddSlides.BeginGroup = true;
			this.buttonItemMenuExtensionsAddSlides.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.buttonItemMenuExtensionsAddSlides.ForeColor = System.Drawing.Color.LightGray;
			this.buttonItemMenuExtensionsAddSlides.Image = global::Asa.Browser.Controls.Properties.Resources.ExtensionsAddSlides;
			this.buttonItemMenuExtensionsAddSlides.Name = "buttonItemMenuExtensionsAddSlides";
			this.buttonItemMenuExtensionsAddSlides.Text = "   test";
			this.buttonItemMenuExtensionsAddSlides.Tooltip = "Add all slides";
			this.buttonItemMenuExtensionsAddSlides.Visible = false;
			this.buttonItemMenuExtensionsAddSlides.Click += new System.EventHandler(this.OnMenuExtensionsAddSlidesClick);
			// 
			// buttonItemMenuExtensionsPrint
			// 
			this.buttonItemMenuExtensionsPrint.BeginGroup = true;
			this.buttonItemMenuExtensionsPrint.Image = global::Asa.Browser.Controls.Properties.Resources.ExtensionsPrint;
			this.buttonItemMenuExtensionsPrint.Name = "buttonItemMenuExtensionsPrint";
			this.buttonItemMenuExtensionsPrint.Tooltip = "Print file";
			this.buttonItemMenuExtensionsPrint.Click += new System.EventHandler(this.OnMenuExtensionsPrintClick);
			// 
			// buttonItemMenuExtensionsAddVideo
			// 
			this.buttonItemMenuExtensionsAddVideo.BeginGroup = true;
			this.buttonItemMenuExtensionsAddVideo.Image = global::Asa.Browser.Controls.Properties.Resources.ExtensionsAddVideo;
			this.buttonItemMenuExtensionsAddVideo.Name = "buttonItemMenuExtensionsAddVideo";
			this.buttonItemMenuExtensionsAddVideo.Text = "buttonItem1";
			this.buttonItemMenuExtensionsAddVideo.Tooltip = "Add this video to your slide";
			this.buttonItemMenuExtensionsAddVideo.Visible = false;
			this.buttonItemMenuExtensionsAddVideo.Click += new System.EventHandler(this.OnMenuExtensionsAddVideoClick);
			// 
			// buttonItemMenuExtensionsDownloadYouTube
			// 
			this.buttonItemMenuExtensionsDownloadYouTube.BeginGroup = true;
			this.buttonItemMenuExtensionsDownloadYouTube.Image = global::Asa.Browser.Controls.Properties.Resources.ExtensionsDownloadYouTube;
			this.buttonItemMenuExtensionsDownloadYouTube.Name = "buttonItemMenuExtensionsDownloadYouTube";
			this.buttonItemMenuExtensionsDownloadYouTube.Text = "buttonItem1";
			this.buttonItemMenuExtensionsDownloadYouTube.Tooltip = "Save a copy of this MP4 file";
			this.buttonItemMenuExtensionsDownloadYouTube.Visible = false;
			this.buttonItemMenuExtensionsDownloadYouTube.Click += new System.EventHandler(this.OnMenuExtensionsDownloadYouTubeClick);
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
            this.buttonItemMenuBrowserChrome,
            this.buttonItemMenuBrowserFirefox,
            this.buttonItemMenuBrowserIE,
            this.buttonItemMenuBrowserEdge,
            this.buttonItemFloater,
            this.buttonItemMenuExtensionsAddSlide,
            this.buttonItemMenuExtensionsAddSlides,
            this.buttonItemMenuExtensionsPrint,
            this.buttonItemMenuExtensionsAddVideo,
            this.buttonItemMenuExtensionsDownloadYouTube,
            this.labelItemMenuWarning,
            this.buttonItemMenuNavigationRefresh,
            this.buttonItemMenuNavigationBack,
            this.buttonItemMenuNavigationForward});
			this.barMain.ItemSpacing = 10;
			this.barMain.Location = new System.Drawing.Point(0, 0);
			this.barMain.Name = "barMain";
			this.barMain.Size = new System.Drawing.Size(1800, 33);
			this.barMain.Stretch = true;
			this.barMain.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.barMain.TabIndex = 1;
			this.barMain.TabStop = false;
			this.barMain.Text = "Main Menu";
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
			this.xtraTabControl.Size = new System.Drawing.Size(1800, 474);
			this.xtraTabControl.TabIndex = 2;
			// 
			// SiteContainerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.xtraTabControl);
			this.Controls.Add(this.barMain);
			this.Name = "SiteContainerControl";
			this.Size = new System.Drawing.Size(1800, 507);
			((System.ComponentModel.ISupportInitialize)(this.barMain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		public DevComponents.DotNetBar.Bar barMain;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuNavigationBack;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuNavigationForward;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuNavigationRefresh;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuBrowserChrome;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuBrowserFirefox;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuBrowserIE;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuBrowserEdge;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsAddSlide;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsAddSlides;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsPrint;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsAddVideo;
		private DevComponents.DotNetBar.ButtonItem buttonItemMenuExtensionsDownloadYouTube;
		private DevComponents.DotNetBar.LabelItem labelItemMenuWarning;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		public DevComponents.DotNetBar.ButtonItem buttonItemFloater;
	}
}
