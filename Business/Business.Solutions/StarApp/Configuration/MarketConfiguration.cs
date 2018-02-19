using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class MarketConfiguration
	{
		public List<ListDataItem> HeadersPartAItems { get; set; }
		public string PartASubHeader1DefaultValue { get; private set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }

		public List<ListDataItem> HeadersPartBItems { get; set; }
		public string PartBSubHeader1DefaultValue { get; private set; }
		public string PartBSubHeader2DefaultValue { get; private set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }
		public ClipartConfiguration PartBClipart3Configuration { get; private set; }
		public ClipartConfiguration PartBClipart4Configuration { get; private set; }
		public ClipartConfiguration PartBClipart5Configuration { get; private set; }

		public List<ListDataItem> HeadersPartCItems { get; set; }
		public List<ListDataItem> PartCCombo1Items { get; }
		public ClipartConfiguration PartCClipart1Configuration { get; private set; }
		public ClipartConfiguration PartCClipart2Configuration { get; private set; }
		public ClipartConfiguration PartCClipart3Configuration { get; private set; }
		public ClipartConfiguration PartCClipart4Configuration { get; private set; }

		public MarketConfiguration()
		{
			HeadersPartAItems = new List<ListDataItem>();
			PartAClipart1Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<ListDataItem>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();
			PartBClipart3Configuration = new ClipartConfiguration();
			PartBClipart4Configuration = new ClipartConfiguration();
			PartBClipart5Configuration = new ClipartConfiguration();

			HeadersPartCItems = new List<ListDataItem>();
			PartCCombo1Items = new List<ListDataItem>();
			PartCClipart1Configuration = new ClipartConfiguration();
			PartCClipart2Configuration = new ClipartConfiguration();
			PartCClipart3Configuration = new ClipartConfiguration();
			PartCClipart4Configuration = new ClipartConfiguration();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataMarketPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataMarketPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP07A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP07AHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartAItems.Add(item);
							}
							break;
						case "CP07ASubheader1":
							PartASubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}

				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP07AClipart1");
			}

			if (resourceManager.DataMarketPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataMarketPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP07B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP07BHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartBItems.Add(item);
							}
							break;
						case "CP07BSubheader1":
							PartBSubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP07BSubheader2":
							PartBSubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}

				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP07BClipart1");
				PartBClipart2Configuration = ClipartConfiguration.FromXml(node, "CP07BClipart2");
				PartBClipart3Configuration = ClipartConfiguration.FromXml(node, "CP07BClipart3");
				PartBClipart4Configuration = ClipartConfiguration.FromXml(node, "CP07BClipart4");
				PartBClipart5Configuration = ClipartConfiguration.FromXml(node, "CP07BClipart5");
			}

			if (resourceManager.DataMarketPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataMarketPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP07C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP07CHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartCItems.Add(item);
							}
							break;
						case "CP07CCombo1":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartCCombo1Items.Add(item);
							}
							break;
					}
				}

				PartCClipart1Configuration = ClipartConfiguration.FromXml(node, "CP07CClipart1");
				PartCClipart2Configuration = ClipartConfiguration.FromXml(node, "CP07CClipart2");
				PartCClipart3Configuration = ClipartConfiguration.FromXml(node, "CP07CClipart3");
				PartCClipart4Configuration = ClipartConfiguration.FromXml(node, "CP07CClipart4");
			}
		}
	}
}
