using System;
using System.Windows.Forms;
using AdSalesBrowser.Configuration;
using AdSalesBrowser.Helpers;
using AdSalesBrowser.Properties;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Floater;
using EO.WebBrowser;

namespace AdSalesBrowser
{
	public class AppManager
	{
		private readonly FloaterManager _floater = new FloaterManager();

		public static AppManager Instance { get; } = new AppManager();
		private AppManager()
		{
			Runtime.AddLicense(
				"pfcan53Y+PbooW+mtsPasWmoubPL8q7ZyQkb6Kvc99IfvFuts8PdrmuntcfN" +
				"n6/c9gQU7qe0psLgoVmmwp61n1mXpM0e6KDl5QUg8Z610gLb4IbN1uMjt4/M" +
				"6sXexY+928T9wHa0wMAe6KDl5QUg8Z61kZvnrqXg5/YZ8p61kZt14+30EO2s" +
				"3MKetZ9Zl6TNF+ic3PIEEMidtbvD47ZrqLjE37B1pvD6DuSn6unaD71GgaSx" +
				"y5914+30EO2s3OnP566l4Of2GfKe3MKetZ9Zl6TNDOul5vvPuIlZl6Sxy59Z" +
				"l8DyD+NZ6/0BELxbvNO/7uer5vH2zZ+v3PYEFO6ntKbC4a1pmaTA6YxDl6Sx" +
				"y7to2PD9GvZ3hI6xy59Zs/MDD+SrwPI=");
			Runtime.AllowProprietaryMediaFormats();
		}

		public void RunApplication()
		{
			AppSettingsManager.Instance.LoadSettings();
			ExternalBrowserManager.Load();
			FormMain.Instance.InitForm();
			Application.Run(FormMain.Instance);
		}

		public void ShowFloater(Action afterShow)
		{
			ShowFloater(FormMain.Instance, new FloaterRequestedEventArgs
			{
				AfterShow = afterShow
			});
		}

		public void ShowFloater(Form sender, FloaterRequestedEventArgs e)
		{
			var afterBack = new Action<bool>(b =>
			{
				Utilities.ActivateForm(FormMain.Instance.Handle, b, false);
				Utilities.ActivateTaskbar();
			});
			_floater.ShowFloater(sender ?? FormMain.Instance, e.Logo ?? AppSettingsManager.Instance.FloaterLogo, e.AfterShow, null, afterBack);
		}
	}
}
