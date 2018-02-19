using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class CustomerConfiguration
	{
		public List<ListDataItem> HeadersPartAItems { get; set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }
		public ClipartConfiguration PartAClipart2Configuration { get; private set; }

		public List<ListDataItem> HeadersPartBItems { get; set; }
		public string PartBSubHeader1DefaultValue { get; private set; }
		public string PartBSubHeader2DefaultValue { get; private set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }

		public List<ListDataItem> HeadersPartCItems { get; set; }
		public string PartCSubHeader1DefaultValue { get; private set; }
		public string PartCSubHeader2DefaultValue { get; private set; }
		public string PartCSubHeader3DefaultValue { get; private set; }

		public CustomerConfiguration()
		{
			HeadersPartAItems = new List<ListDataItem>();
			PartAClipart1Configuration = new ClipartConfiguration();
			PartAClipart2Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<ListDataItem>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();

			HeadersPartCItems = new List<ListDataItem>();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataCustomerPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCustomerPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP04A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP04AHeader":
							{
								var header = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(header.Value))
									HeadersPartAItems.Add(header);
							}
							break;
					}
				}


				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP04AClipart1");
				PartAClipart2Configuration = ClipartConfiguration.FromXml(node, "CP04AClipart2");
			}

			if (resourceManager.DataCustomerPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCustomerPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP04B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP04BHeader":
							{
								var header = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(header.Value))
									HeadersPartBItems.Add(header);
							}
							break;
						case "CP04BSubheader1":
							PartBSubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP04BSubheader2":
							PartBSubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}

				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP04BClipart1");
				PartBClipart2Configuration = ClipartConfiguration.FromXml(node, "CP04BClipart2");
			}

			if (resourceManager.DataCustomerPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCustomerPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP04C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP04CHeader":
							{
								var header = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(header.Value))
									HeadersPartCItems.Add(header);
							}
							break;
						case "CP04CSubheader1":
							PartCSubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP04CSubheader2":
							PartCSubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP04CSubheader3":
							PartCSubHeader3DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}
			}
		}
	}
}
