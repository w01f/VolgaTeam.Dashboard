using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using AdSalesBrowser.Properties;

namespace AdSalesBrowser.Helpers
{
	class AppSettingsManager
	{
		public string BaseUrl { get; private set; }
		public bool EnableMenu { get; private set; }
		public bool EnableScroll { get; private set; }

		public Image SplashLogo { get; private set; }

		public static AppSettingsManager Instance { get; } = new AppSettingsManager();

		private AppSettingsManager() { }

		public void LoadSettings()
		{
			EnableMenu = true;
			EnableScroll = true;

			var appFileName = Process.GetCurrentProcess().MainModule.FileName;
			var appFolderPath = Path.GetDirectoryName(appFileName);
			var settingsFilePath = Path.ChangeExtension(appFileName, "txt");
			if (!File.Exists(settingsFilePath)) return;
			var settingsLines = File.ReadAllLines(settingsFilePath);
			if (!settingsLines.Any()) return;
			BaseUrl = settingsLines.ElementAtOrDefault(0);
			EnableMenu = (settingsLines.ElementAtOrDefault(1) ?? String.Empty).Contains("nomenu");
			EnableScroll = (settingsLines.ElementAtOrDefault(1) ?? String.Empty).Contains("noscroll");

			var splashLogoPath = Path.Combine(appFolderPath, "splash.png");
			SplashLogo = File.Exists(splashLogoPath) ? Image.FromFile(splashLogoPath) : Resources.ProgressLogo;
		}
	}
}
