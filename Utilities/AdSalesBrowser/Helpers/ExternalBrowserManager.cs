using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace AdSalesBrowser.Helpers
{
	class ExternalBrowserManager
	{
		private static readonly string[] ProcessedBrowsers =
		{
			"chrome",
			"firefox",
			"iexplore"
		};

		public static Dictionary<string, string> AvailableBrowsers { get; private set; }

		static ExternalBrowserManager()
		{
			AvailableBrowsers = new Dictionary<string, string>();
		}

		public static void Load()
		{
			try
			{
				// Check browser availability
				var browsers = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
				if (browsers == null) return;
				foreach (var browserTag in ProcessedBrowsers)
				{
					foreach (var key in browsers.GetSubKeyNames())
					{
						var tag = browserTag;
						if (!key.ToLower().Contains(tag)) continue;
						var browserKey = browsers.OpenSubKey(key).OpenSubKey(@"shell\open\command");
						var browserPath = browserKey?.GetValue(null) as String;
						if (browserPath == null) continue;
						var path = browserPath.Replace("\"", "");
						if (!File.Exists(path)) continue;
						if (AvailableBrowsers.ContainsKey(browserTag)) continue;
						AvailableBrowsers.Add(browserTag, path);
					}
				}
			}
			catch { }
		}
	}
}
