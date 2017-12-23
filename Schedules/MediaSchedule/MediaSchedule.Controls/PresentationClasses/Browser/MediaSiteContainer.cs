using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Browser.Controls.Controls;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.Floater;
using Asa.Media.Controls.BusinessClasses.Managers;

namespace Asa.Media.Controls.PresentationClasses.Browser
{
	class MediaSiteContainer : SiteContainerControl, IMediaSite
	{
		public override PowerPointSingletonProcessor PowerPointSingleton
			=> BusinessObjects.Instance.PowerPointManager.Processor;

		public override Form MainForm => Controller.Instance.FormMain;
		public override Image SplashLogo => BusinessObjects.Instance.ImageResourcesManager.BrowserSplash ?? base.SplashLogo;

		public MediaSiteContainer()
		{
			buttonItemFloater.Visible = false;

			ButtonNavigationBack.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserNavigationBack ??
										 ButtonNavigationBack.Image;
			ButtonNavigationForward.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserNavigationForward ??
										 ButtonNavigationForward.Image;
			ButtonNavigationRefresh.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserNavigationRefresh ??
										 ButtonNavigationRefresh.Image;
			ButtonExternalBrowserChrome.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserExternalChrome ??
										 ButtonExternalBrowserChrome.Image;
			ButtonExternalBrowserFirefox.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserExternalFirefox ??
												ButtonExternalBrowserFirefox.Image;
			ButtonExternalBrowserIE.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserExternalIE ??
												ButtonExternalBrowserIE.Image;
			ButtonExternalBrowserEdge.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserExternalEdge ??
												ButtonExternalBrowserEdge.Image;
			ButtonExtensionsAddSlide.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserPowerPointAddSlide ??
												ButtonExtensionsAddSlide.Image;
			ButtonExtensionsAddSlides.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserPowerPointAddSlides ??
											 ButtonExtensionsAddSlides.Image;
			ButtonExtensionsPrint.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserPowerPointPrint ??
											 ButtonExtensionsPrint.Image;
			ButtonExtensionsAddVideo.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserVideoAdd ??
											 ButtonExtensionsAddVideo.Image;
			ButtonExtensionsDownloadYouTube.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserYoutubeAdd ??
											 ButtonExtensionsDownloadYouTube.Image;
		}

		public override void ShowFloater(FloaterRequestedEventArgs args)
		{
			Controller.Instance.ShowFloater(args.AfterShow);
		}

		public override bool CheckPowerPointRunning(Action afterRun)
		{
			return Controller.Instance.CheckPowerPointRunning(afterRun);
		}
	}
}
