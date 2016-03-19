using System;
using System.ComponentModel;
using System.Windows.Forms;
using AdSalesBrowser.Properties;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using EO.WebBrowser;

namespace AdSalesBrowser
{
	public partial class FormMain : MetroForm
	{
		private static FormMain _instance;

		public static FormMain Instance
		{
			get { return _instance ?? (_instance = new FormMain()); }
		}

		private FormMain()
		{
			InitializeComponent();

			Runtime.AddLicense(
				"pfcan53Y+PbooW+mtsPasWmoubPL8q7ZyQkb6Kvc99IfvFuts8PdrmuntcfN" +
				"n6/c9gQU7qe0psLgoVmmwp61n1mXpM0e6KDl5QUg8Z610gLb4IbN1uMjt4/M" +
				"6sXexY+928T9wHa0wMAe6KDl5QUg8Z61kZvnrqXg5/YZ8p61kZt14+30EO2s" +
				"3MKetZ9Zl6TNF+ic3PIEEMidtbvD47ZrqLjE37B1pvD6DuSn6unaD71GgaSx" +
				"y5914+30EO2s3OnP566l4Of2GfKe3MKetZ9Zl6TNDOul5vvPuIlZl6Sxy59Z" +
				"l8DyD+NZ6/0BELxbvNO/7uer5vH2zZ+v3PYEFO6ntKbC4a1pmaTA6YxDl6Sx" +
				"y7to2PD9GvZ3hI6xy59Zs/MDD+SrwPI=");
			Runtime.AllowProprietaryMediaFormats();

			Load += LoadSettings;
			Load += InitApplication; 
			Closing += SaveSettings;
			xtraTabControl.SelectedPageChanged += OnSelectedPageChanged;
			xtraTabControl.CloseButtonClick+=OnWebPageCloseButtonClick;
		}

		private void InitApplication(object sender, EventArgs e)
		{
			AppSettingsManager.Instance.LoadSettings();
			var webPage = CreateWebPage(AppSettingsManager.Instance.BaseUrl);
			((XtraTabPage)webPage).ShowCloseButton = DefaultBoolean.False;
			xtraTabControl.TabPages.Add((XtraTabPage)webPage);
			UpdateTabControlState();

			webPage.Navigate();
		}

		private IWebPage CreateWebPage(string url)
		{
			IWebPage webPage;
			if (AppSettingsManager.Instance.UseIEEngine)
				webPage =  new IEPage(url);
			else
				webPage = new WebKitPage(url);
			((XtraTabPage)webPage).TextChanged += OnWebPageTextChanged;
			webPage.OnNavigateNewPage += OnNavigateNewPage;
			webPage.OnClosePage += OnClosePage;
			return webPage;
		}

		private void RemoveTabPage(XtraTabPage tabPage)
		{
			xtraTabControl.TabPages.Remove(tabPage);
			UpdateTabControlState();
		}

		private void UpdateTabControlState()
		{
			xtraTabControl.ShowTabHeader = xtraTabControl.TabPages.Count > 1?DefaultBoolean.True : DefaultBoolean.False;
		}

		public void SuspendPages()
		{
			xtraTabControl.Enabled = false;
		}

		public void ResumePages()
		{
			xtraTabControl.Enabled = true;
		}

		private void OnNavigateNewPage(object sender, NewPageEventArgs e)
		{
			var webPage = CreateWebPage(e.Url);
			xtraTabControl.TabPages.Add((XtraTabPage)webPage);
			xtraTabControl.SelectedTabPage = (XtraTabPage)webPage;
			UpdateTabControlState();

			webPage.Navigate();
		}

		private void OnWebPageTextChanged(object sender, EventArgs e)
		{
			Text = xtraTabControl.SelectedTabPage.Text;
		}

		private void OnSelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			OnWebPageTextChanged(sender, e);
		}

		private void OnWebPageCloseButtonClick(object sender, EventArgs e)
		{
			var arg = (ClosePageButtonEventArgs)e;
			RemoveTabPage((XtraTabPage)arg.Page);
		}

		private void OnClosePage(object sender, ClosePageEventArgs e)
		{
			RemoveTabPage((XtraTabPage)e.Page);
		}

		#region Form Settings
		private void LoadSettings(object sender, EventArgs e)
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
