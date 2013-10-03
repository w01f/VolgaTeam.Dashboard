using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NewBizWiz.MiniBar.BusinessClasses
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

			var rootNode = document.SelectSingleNode(@"/Root");
			if (rootNode == null) return;
			foreach (XmlNode tabNode in rootNode.ChildNodes)
			{
				var tabPage = new TabPage();
				var node = tabNode.SelectSingleNode("Id");
				if (node != null)
				{
					TabNamesEnum temp;
					if (Enum.TryParse(node.InnerText, true, out temp))
						tabPage.Id = temp;
				}
				node = tabNode.SelectSingleNode("Name");
				if (node != null)
					tabPage.Name = node.InnerText;
				node = tabNode.SelectSingleNode("Order");
				if (node != null)
				{
					int temp;
					if (Int32.TryParse(node.InnerText, out temp))
						tabPage.Order = temp;
				}
				node = tabNode.SelectSingleNode("Enabled");
				if (node != null)
				{
					bool temp;
					if (Boolean.TryParse(node.InnerText, out temp))
						tabPage.Enabled = temp;
				}
				node = tabNode.SelectSingleNode("rg1");
				if (node != null)
					tabPage.RibbonGroup1Name = node.InnerText;
				if (tabPage.Id != TabNamesEnum.None)
					TabPages.Add(tabPage);
			}
			TabPages.Sort((x, y) => x.Order.CompareTo(y.Order));
		}
	}

	public class TabPage
	{
		public TabNamesEnum Id { get; set; }
		public string Name { get; set; }
		public int Order { get; set; }
		public bool Enabled { get; set; }
		public string RibbonGroup1Name { get; set; }
	}

	public enum TabNamesEnum
	{
		None,
		PowerPoint,
		Dashboard,
		SalesDepot,
		Apps1,
		Apps2,
		Apps3,
		Apps4,
		Apps5,
		Settings,
		Sync
	}
}