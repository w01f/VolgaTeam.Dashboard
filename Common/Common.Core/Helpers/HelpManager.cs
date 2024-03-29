﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Enums;

namespace Asa.Common.Core.Helpers
{
	public class HelpManager
	{
		private readonly Dictionary<string, string> _helpLinks = new Dictionary<string, string>();
		private readonly List<string> _browserOrder = new List<string>();

		public static string GetFileName()
		{
			switch (AppProfileManager.Instance.AppType)
			{
				case AppTypeEnum.Dashboard:
					return "DashboardHelp.xml";
				case AppTypeEnum.TVSchedule:
					return "TVHelp.xml";
				case AppTypeEnum.RadioSchedule:
					return "RadioHelp.xml";
				case AppTypeEnum.PrintSchedule:
					return "AdScheduleHelp.xml";
			}
			throw new InvalidEnumArgumentException("Help file not found for app");
		}

		public void LoadHelpLinks()
		{
			_helpLinks.Clear();
			var document = new XmlDocument();
			document.Load(ResourceManager.Instance.HelpFile.LocalPath);
			var node = document.SelectSingleNode(@"/Help");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (!_helpLinks.Keys.Contains(childNode.Name.ToLower()))
					_helpLinks.Add(childNode.Name.ToLower(), childNode.InnerText);
			}

			LoadBrowserSettings();
		}

		private void LoadBrowserSettings()
		{
			_browserOrder.Clear();
			var document = new XmlDocument();
			document.Load(ResourceManager.Instance.HelpBrowserFile.LocalPath);
			foreach (var node in document.SelectNodes(@"/BrowserOrder/Browser").OfType<XmlNode>())
				_browserOrder.Add(node.InnerText);
		}

		public void OpenHelpLink(string helpKey)
		{
			helpKey = helpKey.ToLower();
			if (!_helpLinks.Keys.Contains(helpKey)) return;
			try
			{
				var process = new Process();
				process.StartInfo.Arguments = _helpLinks[helpKey];
				process.StartInfo.FileName = "iexplore.exe";
				foreach (var browser in _browserOrder)
				{
					if (browser == "Chrome" && BrowserHelper.ChromeInstalled)
					{
						process.StartInfo.FileName = "chrome.exe";
						break;
					}
					if (browser == "FF" && BrowserHelper.FirefoxInstalled)
					{
						process.StartInfo.FileName = "firefox.exe";
						break;
					}
				}
				process.Start();
			}
			catch
			{
			}
		}

		public void OpenHelpLink(int tabPageNumber)
		{
			var helpKey = string.Format("t{0}", tabPageNumber);
			OpenHelpLink(helpKey);
		}
	}
}
