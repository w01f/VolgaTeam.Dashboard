using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdSalesBrowser
{
	class AppSettingsManager
	{
		private static readonly AppSettingsManager _instance = new AppSettingsManager();

		public string BaseUrl { get; private set; }
		public bool EnableMenu { get; private set; }
		public bool EnableScroll { get; private set; }
		public bool UseIEEngine { get; private set; }

		public static AppSettingsManager Instance
		{
			get { return _instance; }
		}

		private AppSettingsManager() { }

		public void LoadSettings()
		{
			EnableMenu = true;
			EnableScroll = true;

			var appFileName = Process.GetCurrentProcess().MainModule.FileName;
			var settingsFilePath = Path.ChangeExtension(appFileName, "txt");
			if (!File.Exists(settingsFilePath)) return;
			var settingsLines = File.ReadAllLines(settingsFilePath);
			if (!settingsLines.Any()) return;
			BaseUrl = settingsLines.ElementAtOrDefault(0);
			EnableMenu = (settingsLines.ElementAtOrDefault(1) ?? String.Empty).Contains("nomenu");
			EnableScroll = (settingsLines.ElementAtOrDefault(1) ?? String.Empty).Contains("noscroll");
			UseIEEngine = (settingsLines.ElementAtOrDefault(1) ?? String.Empty).Contains("ie");
		}
	}
}
