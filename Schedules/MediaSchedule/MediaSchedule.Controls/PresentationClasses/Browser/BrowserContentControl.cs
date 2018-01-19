using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Browser.Controls.BusinessClasses.Helpers;
using Asa.Common.Core.Extensions;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Schedules.Common.Controls.ContentEditors.Events;
using Asa.Schedules.Common.Controls.ContentEditors.Helpers;
using Asa.Schedules.Common.Controls.ContentEditors.Interfaces;
using DevComponents.DotNetBar;

namespace Asa.Media.Controls.PresentationClasses.Browser
{
	public partial class BrowserContentControl : UserControl, IContentControl
	{
		private MediaSiteBundleControl _siteBundleControl;

		public string Identifier => ContentIdentifiers.Browser;
		public bool IsActive { get; set; }
		public bool RequreScheduleInfo => false;
		public bool ShowScheduleInfo => false;
		public bool RibbonAlwaysCollapsed => true;
		public RibbonTabItem TabPage => Controller.Instance.TabBrowser;

		public BrowserContentControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void InitMetaData()
		{
			TabPage.Tag = Identifier;
		}

		public virtual void InitControl()
		{
			if (BusinessObjects.Instance.BrowserManager.Sites.Any())
			{
				_siteBundleControl = new MediaSiteBundleControl();
				_siteBundleControl.Dock = DockStyle.Fill;
				Controls.Add(_siteBundleControl);

				_siteBundleControl.LoadSites(BusinessObjects.Instance.BrowserManager.Sites);

				ExternalBrowserManager.Load();
			}
		}

		public void ShowControl(ContentOpenEventArgs args = null)
		{
			IsActive = true;
			if (!BusinessObjects.Instance.BrowserManager.Sites.Any()) return;

			_siteBundleControl.UpdateMainStatusBarInfo();
			LoadUrlActionButtons();
		}

		public void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("eo");
		}

		private void LoadUrlActionButtons()
		{
			ContentStatusBarManager.Instance.StatusBarAdditionalItemsContainer.SubItems.Clear();

			foreach (var browserTag in ExternalBrowserManager.AvailableBrowsers.Keys)
			{
				var browserButton = new ButtonItem();
				browserButton.Tag = browserTag;
				browserButton.Click += OnExternalBrowserOpenClick;
				Image buttonImage = null;
				switch (browserTag)
				{
					case ExternalBrowserManager.BrowserChromeTag:
						buttonImage = BusinessObjects.Instance.ImageResourcesManager.BrowserExternalChrome;
						browserButton.Tooltip = "Chrome";
						break;
					case ExternalBrowserManager.BrowserFirefoxTag:
						buttonImage = BusinessObjects.Instance.ImageResourcesManager.BrowserExternalFirefox;
						browserButton.Tooltip = "Firefox";
						break;
					case ExternalBrowserManager.BrowserIETag:
						buttonImage = BusinessObjects.Instance.ImageResourcesManager.BrowserExternalIE;
						browserButton.Tooltip = "IE";
						break;
					case ExternalBrowserManager.BrowserEdgeTag:
						buttonImage = BusinessObjects.Instance.ImageResourcesManager.BrowserExternalEdge;
						browserButton.Tooltip = "Edge";
						break;
				}
				if (buttonImage != null && buttonImage.Height > 16)
					buttonImage = buttonImage.Resize(new Size(buttonImage.Width, 16));
				browserButton.Image = buttonImage;
				ContentStatusBarManager.Instance.StatusBarAdditionalItemsContainer.SubItems.Add(browserButton);
			}

			var emailUrlButton = new ButtonItem();
			emailUrlButton.BeginGroup = true;
			emailUrlButton.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserUrlEmail;
			emailUrlButton.Click += OnUrlEmail;
			ContentStatusBarManager.Instance.StatusBarAdditionalItemsContainer.SubItems.Add(emailUrlButton);

			var copyUrlButton = new ButtonItem();
			copyUrlButton.Image = BusinessObjects.Instance.ImageResourcesManager.BrowserUrlCopy;
			copyUrlButton.Click += OnUrlCopy;
			ContentStatusBarManager.Instance.StatusBarAdditionalItemsContainer.SubItems.Add(copyUrlButton);

			ContentStatusBarManager.Instance.StatusBarAdditionalItemsContainer.RecalcSize();
			ContentStatusBarManager.Instance.StatusBar.RecalcLayout();
		}

		private void OnUrlEmail(object sender, EventArgs e)
		{
			_siteBundleControl.EmailUrl();
		}

		private void OnUrlCopy(object sender, EventArgs e)
		{
			_siteBundleControl.CopyUrl();
		}

		private void OnExternalBrowserOpenClick(object sender, EventArgs e)
		{
			var browserButton = (ButtonItem)sender;
			var browserTag = browserButton.Tag as String;
			ExternalBrowserManager.OpenUrl(browserTag, _siteBundleControl.SelectedSite?.CurrentUrl);
		}
	}
}
