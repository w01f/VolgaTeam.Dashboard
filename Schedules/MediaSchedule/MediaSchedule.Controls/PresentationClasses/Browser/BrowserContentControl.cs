using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Browser.Controls.BusinessClasses.Enums;
using Asa.Browser.Controls.BusinessClasses.Objects;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Schedules.Common.Controls.ContentEditors.Events;
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
			if (_listControl.EditValue == null)
				_listControl.SelectedIndex = 0;
		}

		public void GetHelp()
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("eo");
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
	}
}
