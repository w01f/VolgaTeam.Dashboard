using System;
using System.IO;
using System.Windows.Forms;
using AdSalesBrowser.Interops;
using AdSalesBrowser.SalesLibraryExtensions;
using AdSalesBrowser.SalesLibraryExtensions.LinkViewContent;
using Asa.Common.GUI.ToolForms;
using EO.WebBrowser;

namespace AdSalesBrowser.WebPage
{
	public partial class WebKitPage
	{
		private void InitSalesLibraryViewContentExtensions()
		{
			_extensionDownloadView.WebView.DownloadCompleted += OnSlideContentWebViewDownloadCompleted;

			_extensionsManager.LinkViewContentExtension.ContetChanged += OnViewContentChanged;

			_webKit.WebView.RegisterJSExtensionFunction(LinkViewContentExtension.SendLinkDataFunctionName, _extensionsManager.OnJavaScriptCall);
			_webKit.WebView.RegisterJSExtensionFunction(LinkViewContentExtension.ReleaseLinkDataFunctionName, _extensionsManager.OnJavaScriptCall);
			_webKit.WebView.RegisterJSExtensionFunction(LinkViewContentExtension.SwitchDataFunctionName, _extensionsManager.OnJavaScriptCall);
		}

		private void OnViewContentChanged(Object sender, EventArgs e)
		{
			UpdateViewContentState();
		}

		public void UpdateViewContentState()
		{
			FormMain.Instance.ButtonExtensionsAddSlide.Visible = false;
			FormMain.Instance.ButtonExtensionsAddSlides.Visible = false;

			FormMain.Instance.ButtonExtensionsPrint.Visible = false;

			FormMain.Instance.ButtonExtensionsAddVideo.Visible = false;

			FormMain.Instance.LabelExtensionsWarning.Text = String.Empty;

			if (_extensionsManager.LinkViewContentExtension.PowerPointEnabled)
			{
				FormMain.Instance.ButtonExtensionsAddSlide.Visible = true;
				FormMain.Instance.ButtonExtensionsAddSlides.Visible = true;
				FormMain.Instance.ButtonExtensionsPrint.Visible = true;

				PowerPointSingleton.Instance.Connect();
				var slideSettings = PowerPointSingleton.Instance.GetSlideSettings();
				var currentPageContent = _extensionsManager.LinkViewContentExtension.CurrentPowerPointContent;
				if (slideSettings != null && !currentPageContent.IsFitToInsert(slideSettings))
					FormMain.Instance.LabelExtensionsWarning.Text = "Slide Size Conflict: The slides may not insert correctly…";
			}
			if (_extensionsManager.LinkViewContentExtension.PrintEnabled)
			{
				FormMain.Instance.ButtonExtensionsPrint.Visible = true;
			}
			if (_extensionsManager.LinkViewContentExtension.VideoEnabled)
			{
				PowerPointSingleton.Instance.Connect();

				var activePresentation = PowerPointSingleton.Instance.GetActivePresentation();
				var allowVideoInsert = activePresentation != null && File.Exists(activePresentation.FullName);

				FormMain.Instance.ButtonExtensionsAddVideo.Visible = allowVideoInsert;

				if (activePresentation != null && !allowVideoInsert)
					FormMain.Instance.LabelExtensionsWarning.Text = "Save your presentation if you want to add this video…";
			}
			FormMain.Instance.barMain.RecalcLayout();
		}

		public void AddVideo()
		{
			if (!PowerPointManager.Instance.CheckPowerPointRunning(UpdateViewContentState)) return;
			var activePresentation = PowerPointSingleton.Instance.GetActivePresentation();
			if (activePresentation != null && File.Exists(activePresentation.FullName))
			{
				DownloadFile(_extensionsManager.LinkViewContentExtension.CurrentVideoContent.GetMp4Url());
			}
		}

		public void AddSlide()
		{
			if (!PowerPointManager.Instance.CheckPowerPointRunning(UpdateViewContentState)) return;
			DownloadFile(
				_extensionsManager.LinkViewContentExtension.CurrentPageContent.GetPartFileUrl(),
				AfterDownloadAction.Open);
		}

		public void AddSlides()
		{
			if (!PowerPointManager.Instance.CheckPowerPointRunning(UpdateViewContentState)) return;
			DownloadFile(
				_extensionsManager.LinkViewContentExtension.CurrentPageContent.OriginalFileUrl,
				AfterDownloadAction.Open);
		}

		public void Print()
		{
			DownloadFile(
				_extensionsManager.LinkViewContentExtension.CurrentPrintableContent.PrintableFileUrl,
				AfterDownloadAction.Print);
		}

		private bool HandleVideoDownloaded(string filePath)
		{
			PowerPointSingleton.Instance.Connect();
			var activePresentation = PowerPointSingleton.Instance.GetActivePresentation();
			var allowVideoInsert = activePresentation != null && File.Exists(activePresentation.FullName);
			if (allowVideoInsert)
			{
				using (var formComplete = new FormVideoDownloadComplete(filePath))
				{
					var result = formComplete.ShowDialog(FormMain.Instance);
					if (result == DialogResult.Abort)
						AppManager.Instance.ShowFloater(() =>
						{
							FormProgress.ShowProgress();
							FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
							PowerPointSingleton.Instance.InsertVideoIntoActivePresentation(filePath);
							FormProgress.CloseProgress();
						}, null);
					return true;
				}
			}
			return false;
		}

		private void OnSlideContentWebViewDownloadCompleted(object sender, DownloadEventArgs e)
		{
			if (!_extensionsManager.LinkViewContentExtension.ContentEnabled) return;
			switch (_afterDownloadAction)
			{
				case AfterDownloadAction.Print:
					ExtensionsManager.PrintFile(
						e.Item.FullPath,
						(_extensionsManager.LinkViewContentExtension.CurrentPrintableContent.CurrentPage + 1) ?? 1);
					break;
				default:
					AppManager.Instance.ShowFloater(() =>
					{
						if (_extensionsManager.LinkViewContentExtension.CurrentLinkViewContent?.ContentType == LinkContentType.Video)
						{
							FormProgress.ShowProgress();
							FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
							PowerPointSingleton.Instance.InsertVideoIntoActivePresentation(e.Item.FullPath);
							FormProgress.CloseProgress();
						}
						else if (_extensionsManager.LinkViewContentExtension.CurrentLinkViewContent?.ContentType == LinkContentType.PowerPoint)
						{
							FormProgress.ShowProgress();
							FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
							PowerPointSingleton.Instance.AppendSlidesFromFile(e.Item.FullPath);
							FormProgress.CloseProgress();
						}
					}, null);
					break;
			}
		}
	}
}
