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

		public HelpManager(string path)
		{
			_contentPath = path;
			LoadHelpLinks();
		}

		private void LoadHelpLinks()
		{
			_helpLinks.Clear();
			XmlNode node;
			if (File.Exists(_contentPath))
			{
				var document = new XmlDocument();
				document.Load(_contentPath);
				node = document.SelectSingleNode(@"/Help");
				if (node != null)
				{
					foreach (XmlNode childNode in node.ChildNodes)
					{
						if (!_helpLinks.Keys.Contains(childNode.Name.ToLower()))
							_helpLinks.Add(childNode.Name.ToLower(), childNode.InnerText);
					}
				}
			}
		}

		public void OpenHelpLink(string helpKey)
		{
			helpKey = helpKey.ToLower();
			if (_helpLinks.Keys.Contains(helpKey))
			{
				try
				{
					Process.Start(_helpLinks[helpKey]);
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