using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Browser.Controls.Controls;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.Floater;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Schedules.Common.Controls.ContentEditors.Helpers;
using DevComponents.DotNetBar;

namespace Asa.Media.Controls.PresentationClasses.Browser
{
	class MediaSiteBundleControl : SiteBundleControl
	{
		public override PowerPointSingletonProcessor PowerPointSingleton
			=> BusinessObjects.Instance.PowerPointManager.Processor;

		public override Form MainForm => Controller.Instance.FormMain;
		public override Image SplashLogo => BusinessObjects.Instance.ImageResourcesManager.BrowserSplash ?? base.SplashLogo;

		public MediaSiteBundleControl()
		{
			buttonItemFloater.Visible = false;

			ButtonNavigationBack.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserNavigationBack ??
										 ButtonNavigationBack.Image;
			ButtonNavigationForward.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserNavigationForward ??
											ButtonNavigationForward.Image;
			ButtonNavigationRefresh.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserNavigationRefresh ??
											ButtonNavigationRefresh.Image;
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

		public override void UpdateMainStatusBarInfo()
		{
			ContentStatusBarManager.Instance.StatusBarMainItemsContainer.SubItems.Clear();

			var titleLabel = new LabelItem();
			titleLabel.Text = BusinessObjects.Instance.BrowserManager.StatusBarTitle;
			if (ContentStatusBarManager.Instance.TextColor.HasValue)
				titleLabel.ForeColor = ContentStatusBarManager.Instance.TextColor.Value;
			ContentStatusBarManager.Instance.StatusBarMainItemsContainer.SubItems.Add(titleLabel);

			if (SelectedSite != null)
			{
				var urlLabel = new LabelItem();
				urlLabel.Text = SelectedSite?.CurrentUrl;
				if (ContentStatusBarManager.Instance.TextColor.HasValue)
					urlLabel.ForeColor = ContentStatusBarManager.Instance.TextColor.Value;
				ContentStatusBarManager.Instance.StatusBarMainItemsContainer.SubItems.Add(urlLabel);
			}

			ContentStatusBarManager.Instance.StatusBarMainItemsContainer.RecalcSize();
			ContentStatusBarManager.Instance.StatusBar.RecalcLayout();
		}
	}
}
