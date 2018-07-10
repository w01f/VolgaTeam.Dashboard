using System;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using DevComponents.DotNetBar.Metro;
using EO.WebBrowser;

namespace Asa.Solutions.StarApp.PresentationClasses.ClipartEdit
{
	public partial class FormPreviewYouTube : MetroForm
	{
		private readonly YouTubeClipartObject _clipartObject;
		
		public FormPreviewYouTube(YouTubeClipartObject clipartObject)
		{
			_clipartObject = clipartObject;
			InitializeComponent();

			Shown += OnShown;
		}

		private void OnShown(Object sender, EventArgs e)
		{
			circularProgress.IsRunning = true;
			Application.DoEvents();

			webBrowser.WebView = new WebView();
			webBrowser.WebView.CustomUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Essential Objects Chrome/41.0.2272.16 Safari/537.36";
			webBrowser.WebView.NewWindow += OnNewWindow;
			webBrowser.WebView.LoadUrlAndWait(_clipartObject.EmbeddedUrl);
			circularProgress.IsRunning = false;
			Application.DoEvents();

			webBrowser.BringToFront();
			Application.DoEvents();
		}

		private void OnNewWindow(object sender, NewWindowEventArgs e)
		{
		}
	}
}