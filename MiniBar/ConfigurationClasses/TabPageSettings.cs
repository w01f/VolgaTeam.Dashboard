using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace MiniBar.ConfigurationClasses
{
	public class TabPageSettings
	{
		public const string UndefinedName = "Undefinded";
		private readonly string _settingsPath;

		public TabPageSettings()
		{
			TabPages = new List<TabPage>();
			_settingsPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\tab_names.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			Load();
		}

		public List<TabPage> TabPages { get; private set; }

		private void Load()
		{
			if (!File.Exists(_settingsPath)) return;
			var document = new XmlDocument();
			document.Load(_settingsPath);

			var node = document.SelectSingleNode(@"/Root");
			if (node != null)
				foreach (XmlNode tabNode in node.ChildNodes)
				{
					var tabPage = new TabPage();
					var nodeId = tabNode.SelectSingleNode("Id");
					if (nodeId != null)
					{
						TabNamesEnum temp;
						if (Enum.TryParse(nodeId.InnerText, true, out temp))
							tabPage.Id = temp;
					}
					var nodeName = tabNode.SelectSingleNode("Name");
					if (nodeName != null)
						tabPage.Name = nodeName.InnerText;
					var nodeEnabled = tabNode.SelectSingleNode("Enabled");
					if (nodeEnabled != null)
					{
						bool temp;
						if (bool.TryParse(nodeEnabled.InnerText, out temp))
							tabPage.Enabled = temp;
					}
					if (tabPage.Id != TabNamesEnum.None)
						TabPages.Add(tabPage);
				}
		}
	}

	public class TabPage
	{
		public TabNamesEnum Id { get; set; }
		public string Name { get; set; }
		public bool Enabled { get; set; }
	}

	public enum TabNamesEnum
	{
		None,
		PowerPoint,
		Dashboard,
		SalesDepot,
		Apps,
		Clipart,
		PDF,
		Tools,
		Settings,
		iPad,
		Sync
	}
}