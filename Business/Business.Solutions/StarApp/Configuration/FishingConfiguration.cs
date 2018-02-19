using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class FishingConfiguration
	{
		public List<ListDataItem> HeadersPartAItems { get; set; }
		public string PartASubHeader1DefaultValue { get; private set; }
		public string PartASubHeader2DefaultValue { get; private set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }

		public List<ListDataItem> HeadersPartBItems { get; set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }

		public List<ListDataItem> HeadersPartCItems { get; set; }
		public string PartCSubHeader1DefaultValue { get; private set; }
		public string PartCSubHeader2DefaultValue { get; private set; }
		public string PartCSubHeader3DefaultValue { get; private set; }

		public FishingConfiguration()
		{
			HeadersPartAItems = new List<ListDataItem>();
			PartAClipart1Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<ListDataItem>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();

			HeadersPartCItems = new List<ListDataItem>();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataFishingPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataFishingPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP03A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP03AHeader":
							{
								var header = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(header.Value))
									HeadersPartAItems.Add(header);
							}
							break;
						case "CP03ASubheader1":
							PartASubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP03ASubheader2":
							PartASubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}

				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP03AClipart1");
			}

			if (resourceManager.DataFishingPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataFishingPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP03B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP03BHeader":
							{
								var header = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(header.Value))
									HeadersPartBItems.Add(header);
							}
							break;
					}
				}

				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP03BClipart1");
				PartBClipart2Configuration = ClipartConfiguration.FromXml(node, "CP03BClipart2");
			}

			if (resourceManager.DataFishingPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataFishingPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP03C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP03CHeader":
							{
								var header = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(header.Value))
									HeadersPartCItems.Add(header);
							}
							break;
						case "CP03CSubheader1":
							PartCSubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP03CSubheader2":
							PartCSubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP03CSubheader3":
							PartCSubHeader3DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}
			}
		}
	}
}
