using System;
using System.Xml;

namespace Asa.Bar.App.Configuration
{
	public class MaintenanceConfiguration
	{
		public bool MaintenanceEnabled { get; private set; }
		public bool ShowInfo { get; private set; }
		public string InfoText { get; private set; }
		public int InfoDelay { get; private set; }

		public void Load()
		{
			if (!ResourceManager.Instance.MaintenanceConfigFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(ResourceManager.Instance.MaintenanceConfigFile.LocalPath);

			MaintenanceEnabled = !Boolean.Parse(document.SelectSingleNode(@"/maintenance/AfterSync/LaunchMiniBar")?.InnerText ?? "true");
			ShowInfo = Boolean.Parse(document.SelectSingleNode(@"/maintenance/SplashMessage/Enabled")?.InnerText ?? "false");
			InfoText = document.SelectSingleNode(@"/maintenance/SplashMessage/Text")?.InnerText;
			InfoDelay = Int32.Parse(document.SelectSingleNode(@"/maintenance/SplashMessage/CloseAfter")?.InnerText ?? "10");
		}
	}
}
