using System;
using System.Windows.Forms;
using DevExpress.XtraTab;

namespace AdSalesBrowser
{
	public sealed partial class IEPage : XtraTabPage, IWebPage
	{
		private readonly WebBrowser _webBrowserIE;
		private readonly string _url;

		public event EventHandler<NewPageEventArgs> OnNavigateNewPage;
		public event EventHandler<ClosePageEventArgs> OnClosePage;

		public IEPage()
		{
			InitializeComponent();
			Text = "Web Page";
			_webBrowserIE = new WebBrowser();
			_webBrowserIE.DocumentCompleted += OnDocumentCompleted;
			Controls.Add(_webBrowserIE);
			_webBrowserIE.Dock = DockStyle.Fill;
			_webBrowserIE.IsWebBrowserContextMenuEnabled = AppSettingsManager.Instance.EnableMenu;
			_webBrowserIE.ScrollBarsEnabled = AppSettingsManager.Instance.EnableScroll;
			_webBrowserIE.ScriptErrorsSuppressed = true;
			circularProgressWebpage.Visible = false;
			circularProgressWebpage.Parent = _webBrowserIE;
		}

		public IEPage(string url)
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
			_webBrowserIE.Navigate(_url);
		}

		private void ResizeProgressBar()
		{
			var p = ClientRectangle.GetCenter();
			var tmp = circularProgressWebpage.ClientRectangle.GetCenter();
			p.Offset(-tmp.X, -tmp.Y);
			circularProgressWebpage.Location = p;
		}

		private void OnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			circularProgressWebpage.Visible = false;
			circularProgressWebpage.IsRunning = false;
		}

		private void WebPage_Resize(object sender, EventArgs e)
		{
			ResizeProgressBar();
		}
	}
}
