using System;
using System.Collections.Generic;
using System.Xml;

namespace Asa.Core.Common
{
	public class TabPageManager
	{
		private readonly StorageFile _contentFile;

		public TabPageManager(StorageFile contentFile)
		{
			_contentFile = contentFile;
			TabPageSettings = new List<TabPageConfig>();
			LoadHelpLinks();
		}

		public List<TabPageConfig> TabPageSettings { get; private set; }

		private void LoadHelpLinks()
		{
			TabPageSettings.Clear();
			if (!_contentFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(_contentFile.LocalPath);
			XmlNode node = document.SelectSingleNode(@"/Root");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var tabPageConfig = new TabPageConfig();
				tabPageConfig.Deserialize(childNode);
				if (tabPageConfig.Visible)
					TabPageSettings.Add(tabPageConfig);
			}
			TabPageSettings.Sort((x, y) => x.Order.CompareTo(y.Order));
		}
	}

	public class TabPageConfig
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public bool Visible { get; set; }
		public int Order { get; set; }

		public void Deserialize(XmlNode node)
		{
			int tempInt;
			bool tempBool;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Id":
						Id = childNode.InnerText;
						break;
					case "Name":
						Name = childNode.InnerText;
						break;
					case "Visible":
						if (Boolean.TryParse(childNode.InnerText, out tempBool))
							Visible = tempBool;
						break;
					case "Order":
						if (Int32.TryParse(childNode.InnerText, out tempInt))
							Order = tempInt;
						break;
				}
			}
		}
	}
}