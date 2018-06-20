using System;
using System.Collections.Generic;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class CoverConfiguration
	{
		public List<ListDataItem> HeaderPartAItems { get; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }
		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }

		public CoverConfiguration()
		{
			HeaderPartAItems = new List<ListDataItem>();
			PartAClipart1Configuration = new ClipartConfiguration();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataCoverPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCoverPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP01A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP01AHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeaderPartAItems.Add(item);
							break;
						case "CP01ASubheader1":
							if (item.IsPlaceholder)
								SubHeader1Placeholder = item.Value;
							else
								SubHeader1DefaultValue = item.Value;
							break;
					}
				}

				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP01AClipart1");
			}
		}
	}
}
