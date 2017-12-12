using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Browser.Controls.BusinessClasses.Enums;
using Asa.Browser.Controls.BusinessClasses.Objects;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Schedules.Common.Controls.ContentEditors.Events;
using Asa.Schedules.Common.Controls.ContentEditors.Helpers;
using Asa.Schedules.Common.Controls.ContentEditors.Interfaces;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;

namespace Asa.Media.Controls.PresentationClasses.Browser
{
	public partial class BrowserContentControl : UserControl, IContentControl
	{
		private ComboBoxEdit _listControl;

		public string Identifier => ContentIdentifiers.Browser;
		public bool IsActive { get; set; }
		public bool RequreScheduleInfo => false;
		public Boolean ShowScheduleInfo => false;
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
			_listControl = Controller.Instance.BrowserSitesCombo;

			LoadSites();

			_listControl.SelectedIndexChanged -= OnSitesListEditValueChanged;
			_listControl.SelectedIndexChanged += OnSitesListEditValueChanged;
		}

		public virtual void ShowControl(ContentOpenEventArgs args = null)
		{
			IsActive = true;

			UpdateMainStatusBarInfo();
			LoadUrlActionButtons();

			if (_listControl.EditValue == null)
				_listControl.SelectedIndex = 0;
		}

		public void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("eo");
		}

		private void UpdateMainStatusBarInfo()
		{
			ContentStatusBarManager.Instance.StatusBarMainItemsContainer.SubItems.Clear();

			var titleLabel = new LabelItem();
			titleLabel.Text = BusinessObjects.Instance.BrowserManager.StatusBarTitle;
			if (ContentStatusBarManager.Instance.TextColor.HasValue)
				titleLabel.ForeColor = ContentStatusBarManager.Instance.TextColor.Value;
			ContentStatusBarManager.Instance.StatusBarMainItemsContainer.SubItems.Add(titleLabel);

			var selectedSiteSettings = _listControl?.EditValue as SiteSettings;
			if (selectedSiteSettings != null)
			{
				var urlLabel = new LabelItem();
				urlLabel.Text = selectedSiteSettings.BaseUrl;
				if (ContentStatusBarManager.Instance.TextColor.HasValue)
					urlLabel.ForeColor = ContentStatusBarManager.Instance.TextColor.Value;
				ContentStatusBarManager.Instance.StatusBarMainItemsContainer.SubItems.Add(urlLabel);
			}

			ContentStatusBarManager.Instance.StatusBarMainItemsContainer.RecalcSize();
			ContentStatusBarManager.Instance.StatusBar.RecalcLayout();
		}

		private void LoadUrlActionButtons()
		{
			ContentStatusBarManager.Instance.StatusBarAdditionalItemsContainer.SubItems.Clear();

			var emailUrlButton = new ButtonItem();
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

		private void LoadSites()
		{
			if (!BusinessObjects.Instance.BrowserManager.Sites.Any()) return;
			_listControl.Properties.Items.Clear();
			_listControl.Properties.Items.AddRange(BusinessObjects.Instance.BrowserManager.Sites);
		}

		private void OnSitesListEditValueChanged(object sender, EventArgs e)
		{
			var comboBox = sender as ComboBoxEdit;
			var selectedSiteSettings = comboBox?.EditValue as SiteSettings;
			if (selectedSiteSettings == null) return;

			UpdateMainStatusBarInfo();

			var siteContainer = Controls.OfType<IMediaSite>().FirstOrDefault(sc => sc.SiteSettings.Id == selectedSiteSettings.Id);
			if (siteContainer == null)
			{
				switch (selectedSiteSettings.SiteType)
				{
					case SiteType.SalesCloud:
						var mediaSiteContainer = new MediaSiteContainer();
						mediaSiteContainer.Dock = DockStyle.Fill;
						Controls.Add(mediaSiteContainer);
						mediaSiteContainer.InitSite(selectedSiteSettings);
						mediaSiteContainer.LoadPages();
						siteContainer = mediaSiteContainer;
						break;
					case SiteType.SimpleSite:
						var simpleSiteControl = new SimpleSiteControl(selectedSiteSettings);
						simpleSiteControl.Dock = DockStyle.Fill;
						Controls.Add(simpleSiteControl);
						simpleSiteControl.LoadSite();
						siteContainer = simpleSiteControl;
						break;
					default:
						throw new ArgumentOutOfRangeException("Undefined site type");
				}
			}
			((Control)siteContainer).BringToFront();
		}

		private void OnUrlEmail(object sender, EventArgs e)
		{
			var selectedSiteSettings = _listControl?.EditValue as SiteSettings;
			if (selectedSiteSettings == null) return;
			var siteContainer = Controls.OfType<IMediaSite>().FirstOrDefault(sc => sc.SiteSettings.Id == selectedSiteSettings.Id);
			siteContainer?.EmailUrl();
		}

		private void OnUrlCopy(object sender, EventArgs e)
		{
			var selectedSiteSettings = _listControl?.EditValue as SiteSettings;
			if (selectedSiteSettings == null) return;
			var siteContainer = Controls.OfType<IMediaSite>().FirstOrDefault(sc => sc.SiteSettings.Id == selectedSiteSettings.Id);
			siteContainer?.CopyUrl();
		}
	}
}
