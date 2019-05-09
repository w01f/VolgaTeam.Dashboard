using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Browser.Controls.BusinessClasses.Enums;
using Asa.Browser.Controls.BusinessClasses.Helpers;
using Asa.Browser.Controls.BusinessClasses.Objects;
using Asa.Browser.Single.Configuration;
using Asa.Browser.Single.InteropClasses;
using Asa.Browser.Single.Properties;
using Asa.Common.Core.Extensions;
using Asa.Common.GUI.Floater;
using Asa.Common.GUI.ToolForms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro.ColorTables;
using EO.WebBrowser;

namespace Asa.Browser.Single
{
    public partial class FormMain : RibbonForm, IFloaterSupportedForm
    {
        private static FormMain _instance;
        private readonly SingleSiteContainerControl _siteBundleContainer;

        public static FormMain Instance => _instance ?? (_instance = new FormMain());

        private FormMain()
        {
            InitializeComponent();

            Width = (Int32)(Screen.PrimaryScreen.Bounds.Width * 0.8);
            Height = (Int32)(Screen.PrimaryScreen.Bounds.Height * 0.8);
            Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;
            Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;

            Closing += SaveSettings;

            _siteBundleContainer = new SingleSiteContainerControl();
            _siteBundleContainer.Dock = DockStyle.Fill;
            panelMain.Controls.Add(_siteBundleContainer);
        }

        public void InitForm()
        {
            FormProgress.Init(this);

            var threadRunner = new ThreadRunner();
            var webView = threadRunner.CreateWebView();
            webView.Destroy();

            LoadSettings();

            Text = AppSettingsManager.Instance.FormText ?? Text;
            Icon = AppSettingsManager.Instance.FormIcon ?? Icon;

            if (AppSettingsManager.Instance.AccentColor.HasValue)
                styleManager.MetroColorParameters = new MetroColorGeneratorParameters(
                    styleManager.MetroColorParameters.CanvasColor,
                    AppSettingsManager.Instance.AccentColor.Value);

            if (AppSettingsManager.Instance.StatusBarTextColor.HasValue)
            {
                labelItemAppTitle.ForeColor = AppSettingsManager.Instance.StatusBarTextColor.Value;
                labelItemUrl.ForeColor = AppSettingsManager.Instance.StatusBarTextColor.Value;
            }

            _siteBundleContainer.LoadSites(AppSettingsManager.Instance.Sites.OfType<SiteSettings>().ToList());

            ExternalBrowserManager.Load();

            _siteBundleContainer.ButtonNavigationBack.Image = ResourceManager.Instance.BrowserNavigationBack ??
                                                        _siteBundleContainer.ButtonNavigationBack.Image;
            _siteBundleContainer.ButtonNavigationForward.Image = ResourceManager.Instance.BrowserNavigationForward ??
                                                           _siteBundleContainer.ButtonNavigationForward.Image;
            _siteBundleContainer.ButtonNavigationRefresh.Image = ResourceManager.Instance.BrowserNavigationRefresh ??
                                                           _siteBundleContainer.ButtonNavigationRefresh.Image;
            _siteBundleContainer.ButtonExtensionsAddSlide.Image = ResourceManager.Instance.BrowserPowerPointAddSlide ??
                                                            _siteBundleContainer.ButtonExtensionsAddSlide.Image;
            _siteBundleContainer.ButtonExtensionsAddSlides.Image = ResourceManager.Instance.BrowserPowerPointAddSlides ??
                                                             _siteBundleContainer.ButtonExtensionsAddSlides.Image;
            _siteBundleContainer.ButtonExtensionsPrint.Image = ResourceManager.Instance.BrowserPowerPointPrint ??
                                                         _siteBundleContainer.ButtonExtensionsPrint.Image;
            _siteBundleContainer.ButtonExtensionsAddVideo.Image = ResourceManager.Instance.BrowserVideoAdd ??
                                                            _siteBundleContainer.ButtonExtensionsAddVideo.Image;
            _siteBundleContainer.ButtonExtensionsDownloadYouTube.Image = ResourceManager.Instance.BrowserYoutubeAdd ??
                                                                   _siteBundleContainer.ButtonExtensionsDownloadYouTube.Image;

            buttonItemFloater.Image = ResourceManager.Instance.BrowserFloater ??
                                                     buttonItemFloater.Image;

            LoadUrlActionButtons();
        }

        private void LoadUrlActionButtons()
        {
            itemContainerStatusBarActionButtons.SubItems.Clear();

            foreach (var browserTag in ExternalBrowserManager.AvailableBrowsers.Keys)
            {
                var browserButton = new ButtonItem();
                browserButton.Tag = browserTag;
                browserButton.Click += OnExternalBrowserOpenClick;
                Image buttonImage = null;
                switch (browserTag)
                {
                    case ExternalBrowserManager.BrowserChromeTag:
                        buttonImage = ResourceManager.Instance.BrowserExternalChrome;
                        browserButton.Tooltip = "Chrome";
                        break;
                    case ExternalBrowserManager.BrowserFirefoxTag:
                        buttonImage = ResourceManager.Instance.BrowserExternalFirefox;
                        browserButton.Tooltip = "Firefox";
                        break;
                    case ExternalBrowserManager.BrowserIETag:
                        buttonImage = ResourceManager.Instance.BrowserExternalIE;
                        browserButton.Tooltip = "IE";
                        break;
                    case ExternalBrowserManager.BrowserEdgeTag:
                        buttonImage = ResourceManager.Instance.BrowserExternalEdge;
                        browserButton.Tooltip = "Edge";
                        break;
                }
                if (buttonImage != null && buttonImage.Height > 16)
                    buttonImage = buttonImage.Resize(new Size(buttonImage.Width, 16));
                browserButton.Image = buttonImage;
                itemContainerStatusBarActionButtons.SubItems.Add(browserButton);

            }

            var emailUrlButton = new ButtonItem();
            emailUrlButton.BeginGroup = true;
            emailUrlButton.Image = ResourceManager.Instance.BrowserUrlEmail;
            emailUrlButton.Click += OnUrlEmailClick;
            itemContainerStatusBarActionButtons.SubItems.Add(emailUrlButton);

            var copyUrlButton = new ButtonItem();
            copyUrlButton.Image = ResourceManager.Instance.BrowserUrlCopy;
            copyUrlButton.Click += OnUrlCopyClick;
            itemContainerStatusBarActionButtons.SubItems.Add(copyUrlButton);

            itemContainerStatusBarActionButtons.RecalcSize();
            barBottom.RecalcLayout();
        }

        public void ShowAfterFloater()
        {
            if (Tag != FloaterManager.FloatedMarker)
                Opacity = 1;
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            BrowserPowerPointSingleton.Instance.Disconnect();
        }

        private void OnFloaterClick(object sender, EventArgs e)
        {
            _siteBundleContainer.ShowFloater(new FloaterRequestedEventArgs());
        }

        private void OnUrlEmailClick(object sender, EventArgs e)
        {
            _siteBundleContainer.EmailUrl();
        }

        private void OnUrlCopyClick(object sender, EventArgs e)
        {
            _siteBundleContainer.CopyUrl();
        }

        private void OnExternalBrowserOpenClick(object sender, EventArgs e)
        {
            var browserButton = (ButtonItem)sender;
            var browserTag = browserButton.Tag as String;
            ExternalBrowserManager.OpenUrl(browserTag, _siteBundleContainer.SelectedSite?.CurrentUrl);
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
