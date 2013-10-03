using System;
using System.IO;
using System.Xml;

namespace NewBizWiz.MiniBar.BusinessClasses
{
	public class ClipartSettings
	{
		public TabNamesEnum TabPage { get; set; }
		public string Name { get; set; }
		public bool Enabled { get; set; }

		public ClipartSettings(string settingsPath)
		{
			Load(settingsPath);
		}

		private void Load(string settingsPath)
		{
			if (!File.Exists(settingsPath)) return;
			var document = new XmlDocument();
			document.Load(settingsPath);

			var node = document.SelectSingleNode(@"/SalesGallery/AppsTab");
			if (node != null)
			{
				TabNamesEnum temp;
				if (Enum.TryParse(node.InnerText, true, out temp))
					TabPage = temp;
			}
			node = document.SelectSingleNode(@"/SalesGallery/Enabled");
			if (node != null)
			{
				bool temp;
				if (Boolean.TryParse(node.InnerText, out temp))
					Enabled = temp;
			}
			node = document.SelectSingleNode(@"/SalesGallery/GroupName");
			if (node != null)
				Name = node.InnerText;
		}
	}
}
