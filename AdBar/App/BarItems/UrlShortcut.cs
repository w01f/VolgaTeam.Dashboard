﻿using System;
using System.Diagnostics;
using System.Linq;
using Asa.Bar.App.Configuration;

namespace Asa.Bar.App.BarItems
{
	class UrlShortcut : TabGroupItem
	{
		private string _url;

		protected override LinkType Type => LinkType.Url;

		public UrlShortcut(string configPath) : base(configPath) { }

		protected override void Init(string configContent)
		{
			base.Init(configContent);
			_url = ConfigHelper.GetValuesRegex("<path>(.*)</path>", configContent).FirstOrDefault();
		}

		protected override void OpenLinkInternal()
		{
			var browserPath = AppManager.Instance.Settings.UserSettings.SelectedBrowser;
			try
			{
				if (!String.IsNullOrEmpty(browserPath) &&
					AppManager.Instance.WebBrowserManager.AvailableBrowsers.ContainsKey(AppManager.Instance.Settings.UserSettings.SelectedBrowser))
				{
					if (String.Equals(browserPath,"edge",StringComparison.OrdinalIgnoreCase))
						Process.Start(String.Format(AppManager.Instance.WebBrowserManager.AvailableBrowsers[AppManager.Instance.Settings.UserSettings.SelectedBrowser], _url));
					else
						Process.Start(
							new ProcessStartInfo(
								AppManager.Instance.WebBrowserManager.AvailableBrowsers[AppManager.Instance.Settings.UserSettings.SelectedBrowser],
								_url
							));
				}
				else
					Process.Start(browserPath);
			}
			catch { }
		}
	}
}
