using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using AdSalesBrowser.Configuration;
using AdSalesBrowser.Helpers;
using AdSalesBrowser.Interops;
using AdSalesBrowser.Properties;
using AdSalesBrowser.WebPage;
using Asa.Common.GUI.Floater;
using Asa.Common.GUI.ToolForms;
using DevComponents.DotNetBar;
using DevExpress.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;

namespace AdSalesBrowser
{
	public partial class FormMain : Form
	{
		private static FormMain _instance;

		public static FormMain Instance => _instance ?? (_instance = new FormMain());

		private FormMain()
		{
			InitializeComponent();
			Shown += (o, e) => LoadPages();
			Closing += SaveSettings;
			Resize += OnFormResize;
			xtraTabControl.SelectedPageChanged += OnSelectedPageChanged;
			xtraTabControl.CloseButtonClick += OnWebPageCloseButtonClick;
		}

		public void InitForm()
		{
			FormProgress.Init(this);

			LoadSettings();

			Text = AppSettingsManager.Instance.FormText ?? Text;
			Icon = AppSettingsManager.Instance.FormIcon ?? Icon;

			InitExternalBrowserButtons();

			var webPage = CreateWebPage(AppSettingsManager.Instance.BaseUrl);
			((XtraTabPage)webPage).ShowCloseButton = DefaultBoolean.False;
			xtraTabControl.TabPages.Add((XtraTabPage)webPage);
			UpdateTabControlState();
		}

		private void OnFormResize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState != FormWindowState.Minimized && f.Tag != FloaterManager.FloatedMarker)
				Opacity = 1;
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			PowerPointSingleton.Instance.Disconnect();
		}

		private void OnFloaterClick(object sender, EventArgs e)
		{
			AppManager.Instance.ShowFloater(() => { }, null);
		}

		#region Web Page Management

		private WebKitPage SelectedWebPage => xtraTabControl.SelectedTabPage as WebKitPage;

		private void LoadPages()
		{
			foreach (var webPage in xtraTabControl.TabPages.OfType<WebKitPage>())
				webPage.Navigate();
		}

		private WebKitPage CreateWebPage(string url)
		{
			var webPage = new WebKitPage(url);
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
			xtraTabControl.TabPages.Add((XtraTabPage)webPage);
			xtraTabControl.SelectedTabPage = (XtraTabPage)webPage;
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
		public ButtonItem ButtonNavigationBack => buttonItemMenuNavigationBack;
		public ButtonItem ButtonNavigationForward => buttonItemMenuNavigationForward;

		private void UpdateNavigationButtons()
		{
			SelectedWebPage?.UpdateNavigationButtonsState();
		}

		private void buttonItemMenuNavigationBack_Click(object sender, EventArgs e)
		{
			SelectedWebPage?.NavigateBack();
		}

		private void buttonItemMenuNavigationForward_Click(object sender, EventArgs e)
		{
			SelectedWebPage?.NavigateForward();
		}

		private void buttonItemMenuNavigationRefresh_Click(object sender, EventArgs e)
		{
			SelectedWebPage?.Refresh();
		}
		#endregion

		#region External Web Browsers
		private ButtonItem[] _externalBrowserButtons;

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
		private void buttonItemDetails_Click(object sender, EventArgs e)
		{
			using (var form = new FormUrlDetails(SelectedWebPage?.CurrentUrl))
			{
				form.ShowDialog(this);
			}
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

		private void buttonItemMenuExtensionsAddSlide_Click(object sender, EventArgs e)
		{
			SelectedWebPage?.AddSlide();
		}

		private void buttonItemMenuExtensionsAddSlides_Click(object sender, EventArgs e)
		{
			SelectedWebPage?.AddSlides();
		}

		private void buttonItemMenuExtensionsPrint_Click(object sender, EventArgs e)
		{
			SelectedWebPage?.Print();
		}

		private void buttonItemMenuExtensionsAddVideo_Click(object sender, EventArgs e)
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

		private void buttonItemMenuExtensionsDownloadYouTube_Click(object sender, EventArgs e)
		{
			SelectedWebPage?.DownloadYouTube();
		}
		#endregion

		#region Form Settings
		private void LoadSettings()
		{
			Width = Settings.Default.FormWidth;
			Height = Settings.Default.FormHeight;

			if (Settings.Default.FormTop != -1)
			{
				Top = Settings.Default.FormTop;
				Left = Settings.Default.FormLeft;
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
