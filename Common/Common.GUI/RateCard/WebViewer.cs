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
							TabControl.Enabled = false;
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
			TabControl.Enabled = true;
			FormProgress.CloseProgress();
			using (var formComplete = new FormDownloadComplete(e.Item.FullPath))
			{
				formComplete.StartPosition=FormStartPosition.CenterScreen;
				formComplete.ShowDialog();
			}
		}

		private void OnWebViewDownloadCanceled(Object sender, DownloadEventArgs e)
		{
			TabControl.Enabled = true;
			FormProgress.CloseProgress();
		}
	}
}
