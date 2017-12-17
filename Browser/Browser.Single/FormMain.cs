using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Browser.Controls.BusinessClasses.Objects;
using Asa.Browser.Single.Configuration;
using Asa.Browser.Single.InteropClasses;
using Asa.Browser.Single.Properties;
using Asa.Common.GUI.Floater;
using Asa.Common.GUI.ToolForms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro.ColorTables;

namespace Asa.Browser.Single
{
	public partial class FormMain : RibbonForm
	{
		private static FormMain _instance;
		private readonly SingleSiteContainerControl _siteContainer;

		public static FormMain Instance => _instance ?? (_instance = new FormMain());

		private FormMain()
		{
			InitializeComponent();

			Shown += (o, e) => _siteContainer.LoadPages();
			Closing += SaveSettings;
			Resize += OnFormResize;

			_siteContainer = new SingleSiteContainerControl();
			_siteContainer.Dock = DockStyle.Fill;
			panelMain.Controls.Add(_siteContainer);
		}

		public void InitForm()
		{
			FormProgress.Init(this);

			LoadSettings();

			Text = AppSettingsManager.Instance.FormText ?? Text;
			Icon = AppSettingsManager.Instance.FormIcon ?? Icon;
			labelItemAppTitle.Text = AppSettingsManager.Instance.StatusBarTitle;
			labelItemUrl.Text = AppSettingsManager.Instance.BaseUrl;

			if (AppSettingsManager.Instance.AccentColor.HasValue)
				styleManager.MetroColorParameters = new MetroColorGeneratorParameters(
					styleManager.MetroColorParameters.CanvasColor,
					AppSettingsManager.Instance.AccentColor.Value);

			if (AppSettingsManager.Instance.StatusBarTextColor.HasValue)
			{
				labelItemAppTitle.ForeColor = AppSettingsManager.Instance.StatusBarTextColor.Value;
				labelItemUrl.ForeColor = AppSettingsManager.Instance.StatusBarTextColor.Value;
			}

			_siteContainer.InitSite(new SiteSettings
			{
				BaseUrl = AppSettingsManager.Instance.BaseUrl,
				EnableMenu = AppSettingsManager.Instance.EnableMenu,
				EnableScroll = AppSettingsManager.Instance.EnableScroll,
			});

			_siteContainer.ButtonNavigationBack.Image = ResourceManager.Instance.BrowserNavigationBack ??
														_siteContainer.ButtonNavigationBack.Image;
			_siteContainer.ButtonNavigationForward.Image = ResourceManager.Instance.BrowserNavigationForward ??
														   _siteContainer.ButtonNavigationForward.Image;
			_siteContainer.ButtonNavigationRefresh.Image = ResourceManager.Instance.BrowserNavigationRefresh ??
														   _siteContainer.ButtonNavigationRefresh.Image;
			_siteContainer.ButtonExternalBrowserChrome.Image = ResourceManager.Instance.BrowserExternalChrome ??
															   _siteContainer.ButtonExternalBrowserChrome.Image;
			_siteContainer.ButtonExternalBrowserFirefox.Image = ResourceManager.Instance.BrowserExternalFirefox ??
																_siteContainer.ButtonExternalBrowserFirefox.Image;
			_siteContainer.ButtonExternalBrowserIE.Image = ResourceManager.Instance.BrowserExternalIE ??
														   _siteContainer.ButtonExternalBrowserIE.Image;
			_siteContainer.ButtonExternalBrowserEdge.Image = ResourceManager.Instance.BrowserExternalEdge ??
															 _siteContainer.ButtonExternalBrowserEdge.Image;
			_siteContainer.ButtonExtensionsAddSlide.Image = ResourceManager.Instance.BrowserPowerPointAddSlide ??
															_siteContainer.ButtonExtensionsAddSlide.Image;
			_siteContainer.ButtonExtensionsAddSlides.Image = ResourceManager.Instance.BrowserPowerPointAddSlides ??
															 _siteContainer.ButtonExtensionsAddSlides.Image;
			_siteContainer.ButtonExtensionsPrint.Image = ResourceManager.Instance.BrowserPowerPointPrint ??
														 _siteContainer.ButtonExtensionsPrint.Image;
			_siteContainer.ButtonExtensionsAddVideo.Image = ResourceManager.Instance.BrowserVideoAdd ??
															_siteContainer.ButtonExtensionsAddVideo.Image;
			_siteContainer.ButtonExtensionsDownloadYouTube.Image = ResourceManager.Instance.BrowserYoutubeAdd ??
																   _siteContainer.ButtonExtensionsDownloadYouTube.Image;
			_siteContainer.buttonItemFloater.Image = ResourceManager.Instance.BrowserFloater ??
			                                         _siteContainer.buttonItemFloater.Image;
			buttonItemUrlEmail.Image = ResourceManager.Instance.BrowserUrlEmail;
			buttonItemUrlCopy.Image = ResourceManager.Instance.BrowserUrlCopy;
		}

		private void OnFormResize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState != FormWindowState.Minimized && f.Tag != FloaterManager.FloatedMarker)
				Opacity = 1;
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			BrowserPowerPointSingleton.Instance.Disconnect();
		}

		private void OnUrlEmailClick(object sender, EventArgs e)
		{
			_siteContainer.EmailUrl();
		}

		private void OnUrlCopyClick(object sender, EventArgs e)
		{
			_siteContainer.CopyUrl();
		}

		#region Form Settings
		private void LoadSettings()
		{
			Width = (Int32)(Screen.PrimaryScreen.Bounds.Width * 0.8);
			Height = (Int32)(Screen.PrimaryScreen.Bounds.Height * 0.8);
			Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;
			Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;

			if (Settings.Default.FormWidth != -1)
			{
				Width = Settings.Default.FormWidth;
				Height = Settings.Default.FormHeight;
			}

			if (Settings.Default.FormTop != -1)
			{
				Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;
				Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;
			}

			if (Settings.Default.FormState != FormWindowState.Normal)
				WindowState = Settings.Default.FormState;
		}

		private void SaveSettings(object sender, CancelEventArgs e)
		{
			if (WindowState == FormWindowState.Minimized) return;
			Settings.Default.FormState = WindowState;

			if (WindowState == FormWindowState.Normal)
			{
				Settings.Default.FormWidth = Width;
				Settings.Default.FormHeight = Height;
				Settings.Default.FormTop = Top;
				Settings.Default.FormLeft = Left;
			}
			Settings.Default.Save();
		}
		#endregion
	}
}
