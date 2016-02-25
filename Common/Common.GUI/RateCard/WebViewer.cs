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
		public bool Loaded { get; set; }
		public FileInfo File { get; }

		public WebViewer(FileInfo file)
		{
			InitializeComponent();
			File = file;
			Text = Path.GetFileNameWithoutExtension(File.FullName).Replace("&", "&&");

			_browser = new WebControl();
			Controls.Add(_browser);
			_browser.WebView = new WebView();
			_browser.Dock = DockStyle.Fill;
			_browser.WebView.LoadCompleted += WebView_LoadComplete;
		}

		public void ReleaseResources(){}

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

		private void WebView_LoadComplete(Object sender, LoadCompletedEventArgs e)
		{
			FormProgress.CloseProgress();
			TabControl.Enabled = true;
			Loaded = true;
		}
	}
}
