using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using AdSalesBrowser.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.ToolForms;
using DevExpress.Utils;
using DevExpress.XtraTab;
using EO.WebBrowser;
using EO.WebBrowser.WinForm;

namespace AdSalesBrowser.WebPage
{
	public sealed partial class WebKitPage : XtraTabPage
	//public sealed partial class WebKitPage : UserControl
	{
		private readonly WebControl _webKit;
		private readonly string _url;

		private PictureBox[] _externalBrowserButtons;

		public event EventHandler<NewPageEventArgs> OnNavigateNewPage;
		public event EventHandler<ClosePageEventArgs> OnClosePage;

		public WebKitPage()
		{
			InitializeComponent();
			_webKit = new WebControl();
			_webKit.Dock = DockStyle.Fill;
			Controls.Add(_webKit);
			_webKit.WebView = new WebView();
			_webKit.WebView.CustomUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Essential Objects Chrome/41.0.2272.16 Safari/537.36";
			Text = _webKit.WebView.Title;

			pbProgressLogo.Image = AppSettingsManager.Instance.SplashLogo;

			InitSiteLoading();
			InitDownloading();
			_webKit.WebView.JSExtInvoke += OnJavaScriptCall;
			InitExternalBrowserButtons();
		}

		public WebKitPage(string url) : this()
		{
			_url = url;
		}

		#region Site Loading
		public void Navigate()
		{
			ResizeProgressBar();
			pnProgress.BringToFront();
			circularProgress.IsRunning = true;

			Application.DoEvents();
			Application.DoEvents();
			_webKit.WebView.LoadUrl(_url);
		}

		private void InitSiteLoading()
		{
			_webKit.WebView.TitleChanged += OnWebViewTitleChanged;
			_webKit.WebView.LoadCompleted += OnWebViewLoadCompleted;
			_webKit.WebView.LoadFailed += OnWebViewLoadFailed;
			_webKit.WebView.BeforeContextMenu += OnWebViewBeforeContextMenu;
			_webKit.WebView.NewWindow += OnWebViewNewWindow;
			_webKit.WebView.LaunchUrl += OnWebViewLaunchUrl;
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

		private void OnWebViewLoadFailed(object sender, LoadFailedEventArgs e)
		{
			if (ShowCloseButton != DefaultBoolean.False && OnClosePage != null)
				OnClosePage(this, new ClosePageEventArgs() { Page = this });
			else
			{
				circularProgress.IsRunning = false;
				pnProgress.SendToBack();
				_webKit.BringToFront();
			}
		}

		private void OnWebViewLoadCompleted(object sender, LoadCompletedEventArgs e)
		{
			circularProgress.IsRunning = false;
			pnProgress.SendToBack();
			_webKit.BringToFront();
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
		#endregion

		#region Download handling
		private void InitDownloading()
		{
			_webKit.WebView.FileDialog += OnProcessFileDialog;
			_webKit.WebView.BeforeDownload += OnWebViewBeforeDownload;
			_webKit.WebView.DownloadUpdated += OnWebViewDownloadUpdated;
			_webKit.WebView.DownloadCompleted += OnWebViewDownloadCompleted;
			_webKit.WebView.DownloadCanceled += OnWebViewDownloadCanceled;
		}

		private void OnProcessFileDialog(Object sender, FileDialogEventArgs e)
		{
			switch (e.Mode)
			{
				case FileDialogMode.Save:
					using (var saveDialog = new SaveFileDialog())
					{
						saveDialog.Title = e.Title;
						saveDialog.Filter = e.Filter;
						saveDialog.FileName = e.DefaultFileName;
						if (saveDialog.ShowDialog(FormMain.Instance) != DialogResult.Cancel)
						{
							FormProgress.ShowProgress();
							FormProgress.SetTitle("Downloading…", true);
							FormProgress.SetDetails(Path.GetFileName(saveDialog.FileName));
							FormMain.Instance.SuspendPages();
							Application.DoEvents();
							e.Continue(saveDialog.FileName);
						}
						else
							e.Cancel();
					}
					break;
			}
			e.Handled = true;
		}

		private void OnWebViewBeforeDownload(object sender, BeforeDownloadEventArgs e)
		{
			e.FilePath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
				"Downloads",
				Path.GetFileName(e.FilePath));
		}

		private void OnWebViewDownloadUpdated(Object sender, DownloadEventArgs e)
		{
			FormProgress.SetDetails(String.Format("{0} - {1}%", Path.GetFileName(e.Item.FullPath), e.Item.PercentageComplete));
			Application.DoEvents();
		}

		private void OnWebViewDownloadCompleted(Object sender, DownloadEventArgs e)
		{
			FormMain.Instance.ResumePages();
			FormProgress.CloseProgress();
			using (var formComplete = new FormDownloadComplete(e.Item.FullPath))
			{
				formComplete.ShowDialog(FormMain.Instance);
			}
		}

		private void OnWebViewDownloadCanceled(Object sender, DownloadEventArgs e)
		{
			FormMain.Instance.ResumePages();
			FormProgress.CloseProgress();
		}
		#endregion

		#region External Web Browsers
		private void InitExternalBrowserButtons()
		{
			_externalBrowserButtons = new[] { pbChrome, pbFirefox, pbIExplorer };
			foreach (var browserButton in _externalBrowserButtons)
			{
				var browserTag = browserButton.Tag as String;
				if (ExternalBrowserManager.AvailableBrowsers.ContainsKey(browserTag))
				{
					browserButton.Enabled = true;
					browserButton.Buttonize();
				}
				else
					browserButton.Enabled = false;
			}
		}

		private void OnExternalBrowserOpenClick(object sender, EventArgs e)
		{
			var browserButton = (PictureBox)sender;
			var browserTag = browserButton.Tag as String;
			var browserPath = ExternalBrowserManager.AvailableBrowsers[browserTag];
			try
			{
				Process.Start(browserPath, _webKit.WebView.Url);
			}
			catch { }
		}
		#endregion

		#region Splash Processing
		private void ResizeProgressBar()
		{
			var padding = (Width - 420) / 2;
			pnProgress.Padding = new Padding(padding, 50, padding, 0);
		}

		private void OnWebPageResize(object sender, EventArgs e)
		{
			ResizeProgressBar();
		}
		#endregion

		private void OnJavaScriptCall(Object sender, JSExtInvokeArgs e)
		{
			switch (e.FunctionName)
			{
				case "activateEOEngine":
					MessageBox.Show("Handled");
					break;
			}
		}
	}
}
