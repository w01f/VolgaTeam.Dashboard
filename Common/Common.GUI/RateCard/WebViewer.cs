using System;
using System.IO;
using System.Windows.Forms;
using Asa.Common.GUI.ToolForms;
using DevExpress.XtraTab;
using EO.WebBrowser;
using EO.WebBrowser.WinForm;

namespace Asa.Common.GUI.RateCard
{
	public partial class WebViewer : XtraTabPage, IRateCardViewer
	{
		private readonly WebControl _browser;
		private readonly WebControl _childBrowser;
		public bool Loaded { get; set; }
		public FileInfo File { get; }

		public WebViewer(FileInfo file)
		{
			InitializeComponent();
			File = file;
			Text = Path.GetFileNameWithoutExtension(File.FullName).Replace("&", "&&");

			_childBrowser = new WebControl();
			_childBrowser.WebView = new WebView();
			_childBrowser.WebView.BeforeDownload += OnWebViewBeforeDownload;
			_childBrowser.WebView.CustomUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Essential Objects Chrome/41.0.2272.16 Safari/537.36";
			Controls.Add(_childBrowser);

			_browser = new WebControl();
			_browser.WebView = new WebView();
			_browser.Dock = DockStyle.Fill;
			_browser.WebView.LoadCompleted += OnMainWebViewLoadComplete;
			_browser.WebView.NewWindow += OnMainnWebViewNewWindow;
			_browser.WebView.BeforeDownload += OnWebViewBeforeDownload;
			_browser.WebView.CustomUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Essential Objects Chrome/41.0.2272.16 Safari/537.36";
			Controls.Add(_browser);

			_browser.BringToFront();
		}

		public void ReleaseResources() { }

		public void LoadViewer()
		{
			if (Loaded) return;
			TabControl.Enabled = false;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nLoading Page...");
			FormProgress.ShowProgress();
			Application.DoEvents();
			var url = System.IO.File.ReadAllText(File.FullName).Trim();
			_browser.WebView.LoadUrl(url.Replace(" ", "%20"));
		}

		private void OnMainWebViewLoadComplete(Object sender, LoadCompletedEventArgs e)
		{
			FormProgress.CloseProgress();
			TabControl.Enabled = true;
			Loaded = true;
		}

		private void OnMainnWebViewNewWindow(object sender, NewWindowEventArgs e)
		{
			_childBrowser.WebView.LoadUrl(e.TargetUrl);
			e.Accepted = false;
		}

		private void OnWebViewBeforeDownload(object sender, BeforeDownloadEventArgs e)
		{
			e.FilePath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
				"Downloads",
				Path.GetFileName(e.FilePath));
		}
	}
}
