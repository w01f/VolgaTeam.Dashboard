using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

namespace NewBizWiz.Core.Common
{
	public class HelpManager
	{
		private readonly string _contentPath;
		private readonly Dictionary<string, string> _helpLinks = new Dictionary<string, string>();
		private readonly List<string> _browserOrder = new List<string>();

		public HelpManager(string path)
		{
			_contentPath = path;
			LoadHelpLinks();
			LoadBrowserSettings();
		}

		private void LoadHelpLinks()
		{
			_helpLinks.Clear();
			if (!File.Exists(_contentPath)) return;
			var document = new XmlDocument();
			document.Load(_contentPath);
			var node = document.SelectSingleNode(@"/Help");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (!_helpLinks.Keys.Contains(childNode.Name.ToLower()))
					_helpLinks.Add(childNode.Name.ToLower(), childNode.InnerText);
			}
		}

		private void LoadBrowserSettings()
		{
			_browserOrder.Clear();
			var settingsPath = SettingsManager.Instance.HelpBrowserSettingsPath;
			if (!File.Exists(settingsPath)) return;
			var document = new XmlDocument();
			document.Load(settingsPath);
			foreach (var node in document.SelectNodes(@"/BrowserOrder/Browser").OfType<XmlNode>())
				_browserOrder.Add(node.InnerText);
		}

		public void OpenHelpLink(string helpKey)
		{
			helpKey = helpKey.ToLower();
			if (_helpLinks.Keys.Contains(helpKey))
			{
				try
				{
					var process = new Process();
					process.StartInfo.Arguments = _helpLinks[helpKey];
					process.StartInfo.FileName = "iexplore.exe";
					foreach (var browser in _browserOrder)
					{
						if (browser == "Chrome" && Utilities.Instance.ChromeInstalled)
						{
							process.StartInfo.FileName = "chrome.exe";
							break;
						}
						if (browser == "FF" && Utilities.Instance.FirefoxInstalled)
						{
							process.StartInfo.FileName = "firefox.exe";
							break;
						}
					}
					process.Start();
				}
				catch
				{
					Utilities.Instance.ShowWarning("Couldn't open Help link for this page");
				}
			}
			else
				Utilities.Instance.ShowWarning("Help link for this page was not found");
		}

		public void OpenHelpLink(int tabPageNumber)
		{
			var helpKey = string.Format("t{0}", tabPageNumber);
			OpenHelpLink(helpKey);
		}
	}
}