using System;
using System.IO;
using System.Windows.Forms;
using AdSalesBrowser.PowerPoint;
using AdSalesBrowser.SalesLibraryExtensions.SlideContent;
using Asa.Common.GUI.ToolForms;
using EO.WebBrowser;

namespace AdSalesBrowser.WebPage
{
	public partial class WebKitPage
	{
		private void InitSalesLibrarySlideExtensions()
		{
			_extensionDownloadView.WebView.DownloadCompleted += OnSlideContentWebViewDownloadCompleted;

			_extensionsManager.SlideContentExtension.ContetChanged += OnSlideContentContentChanged;

			_webKit.WebView.RegisterJSExtensionFunction(SlideContentExtension.SendLinkDataFunctionName, _extensionsManager.OnJavaScriptCall);
			_webKit.WebView.RegisterJSExtensionFunction(SlideContentExtension.ReleaseLinkDataFunctionName, _extensionsManager.OnJavaScriptCall);
			_webKit.WebView.RegisterJSExtensionFunction(SlideContentExtension.SwitchDataFunctionName, _extensionsManager.OnJavaScriptCall);
		}

		private void OnSlideContentContentChanged(Object sender, EventArgs e)
		{
			UpdateSlideContentState();
		}

		public void UpdateSlideContentState()
		{
			FormMain.Instance.ButtonExtensionsAddSlide.Visible = false;
			FormMain.Instance.ButtonExtensionsAddSlides.Visible = false;
			FormMain.Instance.ButtonExtensionsAddVideo.Visible = false;
			FormMain.Instance.LabelExtensionsWarning.Text = String.Empty;
			if (_extensionsManager.SlideContentExtension.ContentEnabled)
			{
				PowerPointSingleton.Instance.Connect();
				switch (_extensionsManager.SlideContentExtension.CurrentSlideContentLinkData.ContentType)
				{
					case SlideContentType.PowerPoint:
						FormMain.Instance.ButtonExtensionsAddSlide.Visible = true;
						FormMain.Instance.ButtonExtensionsAddSlides.Visible = true;

						var slideSettings = PowerPointSingleton.Instance.GetSlideSettings();
						var powerPointData = (PowerPointData)_extensionsManager.SlideContentExtension.CurrentSlideContentLinkData;
						if (slideSettings != null && !powerPointData.IsFitToInsert(slideSettings))
							FormMain.Instance.LabelExtensionsWarning.Text = "Slide Size Conflict: The slides may not insert correctly…";
						break;
					case SlideContentType.Video:
						var activePresentation = PowerPointSingleton.Instance.GetActivePresentation();
						var allowVideoInsert = activePresentation != null && File.Exists(activePresentation.FullName);
						FormMain.Instance.ButtonExtensionsAddVideo.Visible = allowVideoInsert;
						if (activePresentation != null && !allowVideoInsert)
							FormMain.Instance.LabelExtensionsWarning.Text = "Save your presentation if you want to add this video…";
						break;
				}
			}
			FormMain.Instance.barMain.RecalcLayout();
		}
		
		public void AddVideo()
		{
			if (!PowerPointManager.Instance.CheckPowerPointRunning(UpdateSlideContentState)) return;
			var activePresentation = PowerPointSingleton.Instance.GetActivePresentation();
			if (activePresentation != null && File.Exists(activePresentation.FullName))
			{
				DownloadFile(_extensionsManager.SlideContentExtension.CurrentSlideContentLinkData.GetPartFileUrl());
			}
		}

		public void AddSlide()
		{
			if (!PowerPointManager.Instance.CheckPowerPointRunning(UpdateSlideContentState)) return;
			DownloadFile(_extensionsManager.SlideContentExtension.CurrentSlideContentLinkData.GetPartFileUrl());
		}

		public void AddSlides()
		{
			if (!PowerPointManager.Instance.CheckPowerPointRunning(UpdateSlideContentState)) return;
			DownloadFile(_extensionsManager.SlideContentExtension.CurrentSlideContentLinkData.OriginalFileUrl);
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

		private void OnSlideContentWebViewDownloadCompleted(Object sender, DownloadEventArgs e)
		{
			if(!_extensionsManager.SlideContentExtension.ContentEnabled) return;
			AppManager.Instance.ShowFloater(() =>
			{
				if (_extensionsManager.SlideContentExtension.CurrentSlideContentLinkData?.ContentType == SlideContentType.Video)
				{
					FormProgress.ShowProgress();
					FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
					PowerPointSingleton.Instance.InsertVideoIntoActivePresentation(e.Item.FullPath);
					FormProgress.CloseProgress();
				}
				else if (_extensionsManager.SlideContentExtension.CurrentSlideContentLinkData?.ContentType == SlideContentType.PowerPoint)
				{
					FormProgress.ShowProgress();
					FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
					PowerPointSingleton.Instance.AppendSlidesFromFile(e.Item.FullPath);
					FormProgress.CloseProgress();
				}
			}, null);
		}
	}
}
