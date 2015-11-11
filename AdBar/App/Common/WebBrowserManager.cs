using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DevComponents.DotNetBar;
using Microsoft.Win32;

namespace Asa.Bar.App.Common
{
	class WebBrowserManager
	{
		private static readonly string[] _processedBrowsers =
		{
			"chrome",
			"firefox",
			"opera",
			"iexplore"
		};

		public Dictionary<string, string> AvailableBrowsers { get; private set; }

		public WebBrowserManager()
		{
			AvailableBrowsers = new Dictionary<string, string>();
		}

		public void Load()
		{
			try
			{
				// Check browser availability
				var browsers = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
				if (browsers == null) return;
				foreach (var browserTag in _processedBrowsers)
				{
					foreach (var key in browsers.GetSubKeyNames())
					{
						var tag = browserTag;
						if (!key.ToLower().Contains(tag)) continue;
						var browserKey = browsers.OpenSubKey(key).OpenSubKey(@"shell\open\command");
						if (browserKey == null) continue;
						var browserPath = browserKey.GetValue(null) as String;
						if (browserPath == null) continue;
						var path = browserPath.Replace("\"", "");
						if (!File.Exists(path)) continue;
						if (AvailableBrowsers.ContainsKey(browserTag)) continue;
						AvailableBrowsers.Add(browserTag, path);
					}
				}
			}
			catch { }

			UpdateSelectedBrowserSetting();
		}

		private void UpdateSelectedBrowserSetting()
		{
			if (!String.IsNullOrEmpty(AppManager.Instance.Settings.UserSettings.SelectedBrowser) && 
				AvailableBrowsers.ContainsKey(AppManager.Instance.Settings.UserSettings.SelectedBrowser)) 
				return;
			AppManager.Instance.Settings.UserSettings.SelectedBrowser = AvailableBrowsers.Keys.FirstOrDefault();
			AppManager.Instance.Settings.UserSettings.Save();
		}

		public static IEnumerable<ButtonItem> GetBrowserButtons(SubItemsCollection ribbonItems)
		{
			var buttonItems = new List<ButtonItem>();
			foreach (var item in ribbonItems)
			{

				if (item is ButtonItem && ((ButtonItem)item).Tag != null)
				{
					buttonItems.Add((ButtonItem)item);
				}
				else
				{
					var sub = item as BaseItem;
					if (sub == null) continue;
					if (sub.SubItems.Count > 0)
						buttonItems.AddRange(GetBrowserButtons(sub.SubItems));
				}
			}
			return buttonItems;
		}
	}
}
