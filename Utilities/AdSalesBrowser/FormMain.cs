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
				"MEaBpLHLn3Xj7fQQ7azc6c/nrqXg5/YZ8p7cwp61n1mXpM0M66Xm+8+4iVmX" +
				"pLHLn1mXwPIP41nr/QEQvFu807/u56vm8fbNn6/c9gQU7qe0psLgrWmZpMDp" +
				"jEOXpLHLu2jY8P0a9neEjrHLn1mz8wMP5KvA8vcan53Y+PbooW+mtsPasWmo" +
				"ubPL9Z7p9/oa7XaZtcbNn2i1kZvLn1mXwAQU5qfY+AYd5Hfg8/LW5azp/sEi" +
				"6HrL6OXs7YKt8+nsvHazswQU5qfY+AYd5HeEjs3a66La6f8e5HeEjnXj7fQQ" +
				"7azcwp61n1mXpM0X6Jzc8gQQyJ21u8PjtmuouMTfsHWm8PoO5Kfq6doPvQ==");

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
