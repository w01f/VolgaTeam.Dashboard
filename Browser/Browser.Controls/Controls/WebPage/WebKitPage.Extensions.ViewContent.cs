using System;
using System.IO;
using System.Windows.Forms;
using Asa.Browser.Controls.BusinessClasses.Enums;
using Asa.Browser.Controls.BusinessClasses.Helpers;
using Asa.Browser.Controls.BusinessClasses.Objects.LinkViewContent;
using Asa.Browser.Controls.InteropClasses;
using Asa.Browser.Controls.ToolForms;
using Asa.Common.GUI.Floater;
using Asa.Common.GUI.ToolForms;
using EO.WebBrowser;

namespace Asa.Browser.Controls.Controls.WebPage
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
			_siteContainer.ButtonExtensionsAddSlide.Visible = false;
			_siteContainer.ButtonExtensionsAddSlides.Visible = false;

			_siteContainer.ButtonExtensionsPrint.Visible = false;

			_siteContainer.ButtonExtensionsAddVideo.Visible = false;

			_siteContainer.LabelExtensionsWarning.Text = String.Empty;

			if (_extensionsManager.LinkViewContentExtension.PowerPointEnabled)
			{
				_siteContainer.ButtonExtensionsAddSlide.Visible = true;
				_siteContainer.ButtonExtensionsAddSlides.Visible = true;
				_siteContainer.ButtonExtensionsPrint.Visible = true;

				_siteContainer.PowerPointSingleton.Connect();
				var slideSettings = _siteContainer.PowerPointSingleton.GetActiveSlideSettings();
				var currentPageContent = _extensionsManager.LinkViewContentExtension.CurrentPowerPointContent;
				if (slideSettings != null && !currentPageContent.IsFitToInsert(slideSettings))
					_siteContainer.LabelExtensionsWarning.Text = "Slide Size Conflict: The slides may not insert correctly…";
			}
			if (_extensionsManager.LinkViewContentExtension.PrintEnabled)
			{
				_siteContainer.ButtonExtensionsPrint.Visible = true;
			}
			if (_extensionsManager.LinkViewContentExtension.VideoEnabled)
			{
				_siteContainer.PowerPointSingleton.Connect();

				var activePresentation = _siteContainer.PowerPointSingleton.GetActivePresentation();
				var allowVideoInsert = activePresentation != null && File.Exists(activePresentation.FullName);

				_siteContainer.ButtonExtensionsAddVideo.Visible = allowVideoInsert;

				if (activePresentation != null && !allowVideoInsert)
					_siteContainer.LabelExtensionsWarning.Text = "Save your presentation if you want to add this video…";
			}
			_siteContainer.barMain.RecalcLayout();
		}

		public void AddVideo()
		{
			if (!_siteContainer.CheckPowerPointRunning(UpdateViewContentState)) return;
			var activePresentation = _siteContainer.PowerPointSingleton.GetActivePresentation();
			if (activePresentation != null && File.Exists(activePresentation.FullName))
			{
				DownloadFile(_extensionsManager.LinkViewContentExtension.CurrentVideoContent.GetMp4Url());
			}
		}

		public void AddSlide()
		{
			if (!_siteContainer.CheckPowerPointRunning(UpdateViewContentState)) return;
			DownloadFile(
				_extensionsManager.LinkViewContentExtension.CurrentPageContent.GetPartFileUrl(),
				AfterDownloadAction.Open);
		}

		public void AddSlides()
		{
			if (!_siteContainer.CheckPowerPointRunning(UpdateViewContentState)) return;
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
			_siteContainer.PowerPointSingleton.Connect();
			var activePresentation = _siteContainer.PowerPointSingleton.GetActivePresentation();
			var allowVideoInsert = activePresentation != null && File.Exists(activePresentation.FullName);
			if (allowVideoInsert)
			{
				using (var formComplete = new FormVideoDownloadComplete(filePath))
				{
					var result = formComplete.ShowDialog(_siteContainer.MainForm);
					if (result == DialogResult.Abort)
						_siteContainer.ShowFloater(new FloaterRequestedEventArgs
						{
							AfterShow = () =>
							{
								FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
								FormProgress.ShowOutputProgress();
								_siteContainer.PowerPointSingleton.InsertVideoIntoActivePresentation(filePath);
								FormProgress.CloseProgress();
							}
						});
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
					_siteContainer.ShowFloater(new FloaterRequestedEventArgs
					{
						AfterShow = () =>
						{
							if (_extensionsManager.LinkViewContentExtension.CurrentLinkViewContent?.ContentType == LinkContentType.Video)
							{
								FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
								FormProgress.ShowOutputProgress();
								_siteContainer.PowerPointSingleton.InsertVideoIntoActivePresentation(e.Item.FullPath);
								FormProgress.CloseProgress();
							}
							else if (_extensionsManager.LinkViewContentExtension.CurrentLinkViewContent?.ContentType == LinkContentType.PowerPoint)
							{
								FormProgress.SetTitle("Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!");
								FormProgress.ShowOutputProgress();
								_siteContainer.PowerPointSingleton.AppendSlidesFromFile(e.Item.FullPath);
								FormProgress.CloseProgress();
							}
						}
					});
					break;
			}
		}
	}
}
