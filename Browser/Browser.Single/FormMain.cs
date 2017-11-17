using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Browser.Controls.BusinessClasses.Objects;
using Asa.Browser.Single.Configuration;
using Asa.Browser.Single.InteropClasses;
using Asa.Browser.Single.Properties;
using Asa.Common.GUI.Floater;
using Asa.Common.GUI.ToolForms;

namespace Asa.Browser.Single
{
	public partial class FormMain : Form
	{
		private static FormMain _instance;
		private readonly SingleSiteContainerControl _siteContainer;

		public static FormMain Instance => _instance ?? (_instance = new FormMain());

		private FormMain()
		{
			InitializeComponent();
			Shown += (o, e) => _siteContainer.LoadPages();
			Closing += SaveSettings;
			Resize += OnFormResize;

			_siteContainer = new SingleSiteContainerControl();
			_siteContainer.Dock = DockStyle.Fill;
			Controls.Add(_siteContainer);
		}

		public void InitForm()
		{
			FormProgress.Init(this);

			LoadSettings();

			Text = AppSettingsManager.Instance.FormText ?? Text;
			Icon = AppSettingsManager.Instance.FormIcon ?? Icon;

			_siteContainer.InitSite(new SiteSettings
			{
				BaseUrl = AppSettingsManager.Instance.BaseUrl,
				EnableMenu = AppSettingsManager.Instance.EnableMenu,
				EnableScroll = AppSettingsManager.Instance.EnableScroll,
			});
		}

		private void OnFormResize(object sender, EventArgs e)
		{
			var f = sender as Form;
			if (f.WindowState != FormWindowState.Minimized && f.Tag != FloaterManager.FloatedMarker)
				Opacity = 1;
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			BrowserPowerPointSingleton.Instance.Disconnect();
		}

		#region Form Settings
		private void LoadSettings()
		{
			Width = Settings.Default.FormWidth;
			Height = Settings.Default.FormHeight;

			if (Settings.Default.FormTop != -1)
			{
				Top = Settings.Default.FormTop;
				Left = Settings.Default.FormLeft;
			}

			if (Settings.Default.FormState != FormWindowState.Normal)
				WindowState = Settings.Default.FormState;
		}

		private void SaveSettings(object sender, CancelEventArgs e)
		{
			if (WindowState == FormWindowState.Minimized) return;
			Settings.Default.FormState = WindowState;

			if (WindowState == FormWindowState.Normal)
			{
				Settings.Default.FormWidth = Width;
				Settings.Default.FormHeight = Height;
				Settings.Default.FormTop = Top;
				Settings.Default.FormLeft = Left;
			}
			Settings.Default.Save();
		}
		#endregion
	}
}
