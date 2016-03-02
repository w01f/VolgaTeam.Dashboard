using System;
using System.IO;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraTab;
using EO.WebBrowser;
using EO.WebBrowser.WinForm;

namespace AdSalesBrowser
{
	public sealed partial class WebKitPage : XtraTabPage, IWebPage
	//public sealed partial class WebKitPage : UserControl, IWebPage
	{
		private readonly WebControl _webKit;
		private readonly string _url;

		public event EventHandler<NewPageEventArgs> OnNavigateNewPage;
		public event EventHandler<ClosePageEventArgs> OnClosePage;

		public WebKitPage()
		{
			InitializeComponent();
			_webKit = new WebControl();
			_webKit.Dock = DockStyle.Fill;
			Controls.Add(_webKit);
			_webKit.WebView = new WebView();
			_webKit.WebView.TitleChanged += OnWebViewTitleChanged;
			_webKit.WebView.LoadCompleted += OnWebViewLoadCompleted;
			_webKit.WebView.LoadFailed += OnWebViewLoadFailed;
			_webKit.WebView.IsLoadingChanged += OnWebViewIsLoadingChanged;
			_webKit.WebView.BeforeContextMenu += OnWebViewBeforeContextMenu;
			_webKit.WebView.NewWindow += OnWebViewNewWindow;
			_webKit.WebView.LaunchUrl += OnWebViewLaunchUrl;
			_webKit.WebView.BeforeDownload += OnWebViewBeforeDownload;
			_webKit.WebView.CustomUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Essential Objects Chrome/41.0.2272.16 Safari/537.36";
			Text = _webKit.WebView.Title;
		}

		public WebKitPage(string url)
			: this()
		{
			_url = url;
		}

		public void Navigate()
		{
			circularProgressWebpage.Visible = true;
			circularProgressWebpage.IsRunning = true;
			circularProgressWebpage.BringToFront();
			ResizeProgressBar();
			Application.DoEvents();
			_webKit.WebView.LoadUrl(_url);
		}

		private void ResizeProgressBar()
		{
			var p = ClientRectangle.GetCenter();
			var tmp = circularProgressWebpage.ClientRectangle.GetCenter();
			p.Offset(-tmp.X, -tmp.Y);
			circularProgressWebpage.Location = p;
		}

		private void OnWebViewTitleChanged(object sender, EventArgs eventArgs)
		{
			Text = ((WebView)sender).Title;
		}

		private void OnWebViewBeforeContextMenu(object sender, BeforeContextMenuEventArgs e)
		{
			if (!AppSettingsManager.Instance.EnableMenu)
				e.Menu.Items.Clear();
		}

		private void OnWebViewIsLoadingChanged(object sender, EventArgs e)
		{
			if (_webKit.WebView.IsLoading) return;
			circularProgressWebpage.Visible = false;
			circularProgressWebpage.IsRunning = false;
		}

		private void OnWebViewLoadFailed(object sender, LoadFailedEventArgs e)
		{
			if (ShowCloseButton != DefaultBoolean.False && OnClosePage != null)
				OnClosePage(this, new ClosePageEventArgs() { Page = this });
			else
			{
				circularProgressWebpage.Visible = false;
				circularProgressWebpage.IsRunning = false;
			}
		}

		private void OnWebViewLoadCompleted(object sender, LoadCompletedEventArgs e)
		{
			circularProgressWebpage.Visible = false;
		}

		private void OnWebViewNewWindow(object sender, NewWindowEventArgs e)
		{
			if (OnNavigateNewPage != null)
				OnNavigateNewPage(this, new NewPageEventArgs() { Url = e.TargetUrl });
			e.Accepted = false;
		}
		private void OnWebViewLaunchUrl(object sender, LaunchUrlEventArgs e)
		{
			e.UseOSHandler = true;
		}
		private void OnWebViewBeforeDownload(object sender, BeforeDownloadEventArgs e)
		{
			e.FilePath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
				"Downloads",
				Path.GetFileName(e.FilePath));
		}

		private void WebPage_Resize(object sender, EventArgs e)
		{
			ResizeProgressBar();
		}
	}
}
