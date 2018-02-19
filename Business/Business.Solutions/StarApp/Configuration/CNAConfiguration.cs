using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class CNAConfiguration
	{
		public List<ListDataItem> HeadersPartAItems { get; set; }
		public string PartASubHeader1DefaultValue { get; private set; }
		public string PartASubHeader2DefaultValue { get; private set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }

		public List<ListDataItem> HeadersPartBItems { get; set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }

		public CNAConfiguration()
		{
			HeadersPartAItems = new List<ListDataItem>();
			PartAClipart1Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<ListDataItem>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataCNAPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCNAPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP02A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP02AHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartAItems.Add(item);
							}
							break;
						case "CP02ASubheader1":
							PartASubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP02ASubheader2":
							PartASubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}

				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP02AClipart1");
			}

			if (resourceManager.DataCNAPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCNAPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP02B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP02BHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartBItems.Add(item);
							}
							break;
					}
				}

				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP02BClipart1");
				PartBClipart2Configuration = ClipartConfiguration.FromXml(node, "CP02BClipart2");
			}
		}
	}
}
