using System;
using System.IO;
using System.Windows.Forms;
using AdSalesBrowser.SalesLibraryExtensions;
using AdSalesBrowser.SalesLibraryExtensions.FileLinks;
using Asa.Common.GUI.ToolForms;
using EO.WebBrowser;
using EO.WebBrowser.WinForm;

namespace AdSalesBrowser.WebPage
{
	public partial class WebKitPage
	{
		private WebControl _extensionDownloadView;
		private ExtensionsManager _extensionsManager;

		private void InitExtensions()
		{
			_extensionDownloadView = new WebControl();
			_extensionDownloadView.WebView = new WebView();
			_extensionDownloadView.WebView.ShouldForceDownload += OnExtensionWebViewShouldForceDownload;
			_extensionDownloadView.WebView.BeforeDownload += OnExtensionWebViewBeforeDownload;
			_extensionDownloadView.WebView.DownloadCompleted += OnExtensionsWebViewDownloadCompleted;
			_extensionDownloadView.WebView.DownloadUpdated += OnWebViewDownloadUpdated;
			_extensionDownloadView.WebView.DownloadCanceled += OnWebViewDownloadCanceled;
			_extensionDownloadView.WebView.LoadFailed += OnExtensionWebViewLoadFailed;
			Controls.Add(_extensionDownloadView);

			_extensionsManager = new ExtensionsManager(_startUrl);
			_webKit.WebView.RegisterJSExtensionFunction(ExtensionsManager.ActivateFunctionName, _extensionsManager.OnJavaScriptCall);
			_webKit.WebView.RegisterJSExtensionFunction(LinkOpenExtension.OpenLinkFunctionName, _extensionsManager.OnJavaScriptCall);

			InitSalesLibrarySlideExtensions();
			InitSalesLibraryLinkOpenExtensions();
		}

		private void DownloadFile(string url)
		{
			FormDownloadProgress.ShowProgress(FormMain.Instance);
			FormDownloadProgress.SetTitle("Downloading…");
			FormMain.Instance.SuspendPages();
			Application.DoEvents();
			_extensionDownloadView.WebView.LoadUrl(url.Replace(@"SalesLibraries/SalesLibraries", "SalesLibraries"));
		}

		private void OnExtensionWebViewBeforeDownload(Object sender, BeforeDownloadEventArgs e)
		{
			e.ShowDialog = false;
			e.FilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(e.FilePath));
		}

		private void OnExtensionWebViewShouldForceDownload(object sender, ShouldForceDownloadEventArgs e)
		{
			e.ForceDownload = true;
		}

		private void OnExtensionWebViewLoadFailed(object sender, LoadFailedEventArgs e)
		{
			if (e.ErrorCode == ErrorCode.ProceedAsDownload) return;
			FormMain.Instance.ResumePages();
			FormDownloadProgress.CloseProgress();
		}

		private void OnExtensionsWebViewDownloadCompleted(Object sender, DownloadEventArgs e)
		{
			FormMain.Instance.ResumePages();
			FormDownloadProgress.CloseProgress();
		}
	}
}
