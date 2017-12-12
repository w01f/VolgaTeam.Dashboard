using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Browser.Controls.BusinessClasses.Events;
using Asa.Browser.Controls.BusinessClasses.Helpers;
using Asa.Browser.Controls.BusinessClasses.Objects;
using Asa.Browser.Controls.Controls.WebPage;
using Asa.Browser.Controls.Properties;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.Floater;
using DevComponents.DotNetBar;
using DevExpress.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;

namespace Asa.Browser.Controls.Controls
{
	public abstract partial class SiteContainerControl : UserControl
	{
		public SiteSettings SiteSettings { get; private set; }

		public abstract PowerPointSingletonProcessor PowerPointSingleton { get; }
		public abstract Form MainForm { get; }
		public abstract void ShowFloater(FloaterRequestedEventArgs args);
		public abstract bool CheckPowerPointRunning(Action afterRun);

		public virtual Image SplashLogo => Resources.ProgressLogo;

		protected SiteContainerControl()
		{
			InitializeComponent();

			xtraTabControl.SelectedPageChanged += OnSelectedPageChanged;
			xtraTabControl.CloseButtonClick += OnWebPageCloseButtonClick;
		}

		public void InitSite(SiteSettings siteSettings)
		{
			SiteSettings = siteSettings;

			var webPage = CreateWebPage(SiteSettings.BaseUrl);
			webPage.ShowCloseButton = DefaultBoolean.False;
			xtraTabControl.TabPages.Add(webPage);
			UpdateTabControlState();

			ExternalBrowserManager.Load();
			InitExternalBrowserButtons();
		}

		private void OnFloaterClick(object sender, EventArgs e)
		{
			ShowFloater(new FloaterRequestedEventArgs());
		}

		#region Web Page Management

		private WebKitPage SelectedWebPage => xtraTabControl.SelectedTabPage as WebKitPage;

		public void LoadPages()
		{
			foreach (var webPage in xtraTabControl.TabPages.OfType<WebKitPage>())
				webPage.Navigate();
		}

		private WebKitPage CreateWebPage(string url)
		{
			var webPage = new WebKitPage(this, url);
			webPage.OnNavigateNewPage += OnNavigateNewPage;
			webPage.OnClosePage += OnClosePage;
			return webPage;
		}

		private void RemoveTabPage(ClosePageEventArgs args)
		{
			if (args.NeedReleasePage)
				args.Page.Release();
			xtraTabControl.TabPages.Remove(args.Page);
			UpdateTabControlState();
		}

		private void UpdateTabControlState()
		{
			xtraTabControl.ShowTabHeader = xtraTabControl.TabPages.Count > 1 ? DefaultBoolean.True : DefaultBoolean.False;
		}

		public void SuspendPages()
		{
			xtraTabControl.Enabled = false;
		}

		public void ResumePages()
		{
			xtraTabControl.Enabled = true;
		}

		private void OnNavigateNewPage(object sender, NewPageEventArgs e)
		{
			var webPage = CreateWebPage(e.Url);
			xtraTabControl.TabPages.Add(webPage);
			xtraTabControl.SelectedTabPage = webPage;
			UpdateTabControlState();

			webPage.Navigate();
		}

		private void OnSelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			UpdateNavigationButtons();
			UpdateExtensionsState();
			UpdateYouTubeState();
		}

		private void OnWebPageCloseButtonClick(object sender, EventArgs e)
		{
			var args = (ClosePageButtonEventArgs)e;
			RemoveTabPage(new ClosePageEventArgs
			{
				Page = (WebKitPage)args.Page,
				NeedReleasePage = true
			});
		}

		private void OnClosePage(object sender, ClosePageEventArgs e)
		{
			RemoveTabPage(e);
		}
		#endregion

		#region Navigation
		public ButtonItem ButtonNavigationRefresh => buttonItemMenuNavigationRefresh;
		public ButtonItem ButtonNavigationBack => buttonItemMenuNavigationBack;
		public ButtonItem ButtonNavigationForward => buttonItemMenuNavigationForward;

		private void UpdateNavigationButtons()
		{
			SelectedWebPage?.UpdateNavigationButtonsState();
		}

		private void OnMenuNavigationBackClick(object sender, EventArgs e)
		{
			SelectedWebPage?.NavigateBack();
		}

		private void OnMenuNavigationForwardClick(object sender, EventArgs e)
		{
			SelectedWebPage?.NavigateForward();
		}

		private void OnMenuNavigationRefreshClick(object sender, EventArgs e)
		{
			SelectedWebPage?.Refresh();
		}
		#endregion

		#region External Web Browsers
		private ButtonItem[] _externalBrowserButtons;
		public ButtonItem ButtonExternalBrowserChrome => buttonItemMenuBrowserChrome;
		public ButtonItem ButtonExternalBrowserFirefox => buttonItemMenuBrowserFirefox;
		public ButtonItem ButtonExternalBrowserIE => buttonItemMenuBrowserIE;
		public ButtonItem ButtonExternalBrowserEdge => buttonItemMenuBrowserEdge;

		private void InitExternalBrowserButtons()
		{
			_externalBrowserButtons = new[]
			{
				buttonItemMenuBrowserChrome,
				buttonItemMenuBrowserFirefox,
				buttonItemMenuBrowserIE,
				buttonItemMenuBrowserEdge
			};
			foreach (var browserButton in _externalBrowserButtons)
			{
				var browserTag = browserButton.Tag as String;
				if (ExternalBrowserManager.AvailableBrowsers.ContainsKey(browserTag))
					browserButton.Visible = true;
				else
					browserButton.Visible = false;
			}
		}

		private void OnExternalBrowserOpenClick(object sender, EventArgs e)
		{
			var browserButton = (ButtonItem)sender;
			var browserTag = browserButton.Tag as String;
			ExternalBrowserManager.OpenUrl(browserTag, SelectedWebPage?.CurrentUrl);
		}
		#endregion

		#region Url Details
		public void CopyUrl()
		{
			try
			{
				Clipboard.SetText(SelectedWebPage?.CurrentUrl ?? "empty");
				PopupMessageHelper.Instance.ShowInformation("Url successfully copied");
			}
			catch
			{
				PopupMessageHelper.Instance.ShowWarning("Url is not loaded");
			}
		}

		public void EmailUrl()
		{
			try
			{
				Process.Start(Uri.EscapeUriString(String.Format("mailto:{0}?Body={1}", String.Empty, SelectedWebPage?.CurrentUrl ?? "Empty")));
			}
			catch { }
		}
		#endregion

		#region Slide Content Extension
		public ButtonItem ButtonExtensionsAddSlide => buttonItemMenuExtensionsAddSlide;
		public ButtonItem ButtonExtensionsAddSlides => buttonItemMenuExtensionsAddSlides;
		public ButtonItem ButtonExtensionsPrint => buttonItemMenuExtensionsPrint;
		public ButtonItem ButtonExtensionsAddVideo => buttonItemMenuExtensionsAddVideo;
		public LabelItem LabelExtensionsWarning => labelItemMenuWarning;

		private void UpdateExtensionsState()
		{
			SelectedWebPage?.UpdateViewContentState();
		}

		private void OnMenuExtensionsAddSlideClick(object sender, EventArgs e)
		{
			SelectedWebPage?.AddSlide();
		}

		private void OnMenuExtensionsAddSlidesClick(object sender, EventArgs e)
		{
			SelectedWebPage?.AddSlides();
		}

		private void OnMenuExtensionsPrintClick(object sender, EventArgs e)
		{
			SelectedWebPage?.Print();
		}

		private void OnMenuExtensionsAddVideoClick(object sender, EventArgs e)
		{
			SelectedWebPage?.AddVideo();
		}
		#endregion

		#region YouTube Extensions
		public ButtonItem ButtonExtensionsDownloadYouTube => buttonItemMenuExtensionsDownloadYouTube;

		private void UpdateYouTubeState()
		{
			SelectedWebPage?.UpdateYouTubeState();
		}

		private void OnMenuExtensionsDownloadYouTubeClick(object sender, EventArgs e)
		{
			SelectedWebPage?.DownloadYouTube();
		}
		#endregion
	}
}
