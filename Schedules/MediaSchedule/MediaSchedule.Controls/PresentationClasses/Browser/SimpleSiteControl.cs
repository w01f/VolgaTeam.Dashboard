using System;
using System.IO;
using System.Windows.Forms;
using Asa.Browser.Controls.BusinessClasses.Objects;
using Asa.Common.GUI.ToolForms;
using EO.WebBrowser;
using EO.WebBrowser.WinForm;

namespace Asa.Media.Controls.PresentationClasses.Browser
{
	public partial class SimpleSiteControl : UserControl, IMediaSite
	{
		private bool _loaded;
		private readonly WebControl _browser;
		private readonly WebControl _childBrowser;

		public SiteSettings SiteSettings { get; }

		public SimpleSiteControl(SiteSettings siteSettings)
		{
			InitializeComponent();

			SiteSettings = siteSettings;

			_childBrowser = new WebControl();
			_childBrowser.WebView = new WebView();
			_childBrowser.WebView.FileDialog += OnProcessFileDialog;
			_childBrowser.WebView.BeforeDownload += OnWebViewBeforeDownload;
			_childBrowser.WebView.DownloadUpdated += OnWebViewDownloadUpdated;
			_childBrowser.WebView.DownloadCompleted += OnWebViewDownloadCompleted;
			_childBrowser.WebView.DownloadCanceled += OnWebViewDownloadCanceled;
			_childBrowser.WebView.CustomUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Essential Objects Chrome/41.0.2272.16 Safari/537.36";
			Controls.Add(_childBrowser);

			_browser = new WebControl();
			_browser.WebView = new WebView();
			_browser.Dock = DockStyle.Fill;
			_browser.WebView.LoadCompleted += OnMainWebViewLoadComplete;
			_browser.WebView.NewWindow += OnMainWebViewNewWindow;
			_browser.WebView.BeforeDownload += OnWebViewBeforeDownload;
			_browser.WebView.CustomUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Essential Objects Chrome/41.0.2272.16 Safari/537.36";
			Controls.Add(_browser);

			_browser.BringToFront();
		}

		public void LoadSite()
		{
			if(_loaded) return;
			FormProgress.SetTitle("Chill-Out for a few seconds...\nLoading Page...");
			FormProgress.ShowProgress();
			Application.DoEvents();
			_browser.WebView.LoadUrl(SiteSettings.BaseUrl);
		}

		private void OnMainWebViewLoadComplete(Object sender, LoadCompletedEventArgs e)
		{
			FormProgress.CloseProgress();
			_loaded = true;
		}

		private void OnMainWebViewNewWindow(object sender, NewWindowEventArgs e)
		{
			_childBrowser.WebView.LoadUrl(e.TargetUrl);
			e.Accepted = false;
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
						if (saveDialog.ShowDialog() != DialogResult.Cancel)
						{
							FormProgress.ShowProgress();
							FormProgress.SetTitle("Downloading…", true);
							FormProgress.SetDetails(Path.GetFileName(saveDialog.FileName));
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
			FormProgress.CloseProgress();
			using (var formComplete = new FormFileDownloadComplete(e.Item.FullPath))
			{
				formComplete.StartPosition=FormStartPosition.CenterParent;
				formComplete.ShowDialog();
			}
		}

		private void OnWebViewDownloadCanceled(Object sender, DownloadEventArgs e)
		{
			FormProgress.CloseProgress();
		}
	}
}
