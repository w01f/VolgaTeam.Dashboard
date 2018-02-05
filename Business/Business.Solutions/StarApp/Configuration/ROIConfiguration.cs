using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class ROIConfiguration
	{
		public List<ListDataItem> HeadersPartAItems { get; set; }
		public string PartASubHeader1DefaultValue { get; private set; }
		public string PartASubHeader2DefaultValue { get; private set; }
		public string PartASubHeader3DefaultValue { get; private set; }
		public string PartASubHeader4DefaultValue { get; private set; }
		public string PartASubHeader5DefaultValue { get; private set; }
		public string PartASubHeader6DefaultValue { get; private set; }
		public string PartASubHeader7DefaultValue { get; private set; }
		public string PartASubHeader8DefaultValue { get; private set; }
		public string PartASubHeader9DefaultValue { get; private set; }
		public string PartASubHeader10DefaultValue { get; private set; }
		public string PartASubHeader11DefaultValue { get; private set; }
		public string PartASubHeader12DefaultValue { get; private set; }
		public string PartASubHeader13DefaultValue { get; private set; }
		public string PartASubHeader14DefaultValue { get; private set; }
		public string PartASubHeader15DefaultValue { get; private set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }
		public ClipartConfiguration PartAClipart2Configuration { get; private set; }
		public ClipartConfiguration PartAClipart3Configuration { get; private set; }

		public List<ListDataItem> HeadersPartBItems { get; set; }
		public string PartBSubHeader1DefaultValue { get; private set; }
		public string PartBSubHeader2DefaultValue { get; private set; }
		public string PartBSubHeader3DefaultValue { get; private set; }
		public string PartBSubHeader4DefaultValue { get; private set; }
		public string PartBSubHeader5DefaultValue { get; private set; }
		public string PartBSubHeader6DefaultValue { get; private set; }
		public string PartBSubHeader7DefaultValue { get; private set; }
		public string PartBSubHeader8DefaultValue { get; private set; }
		public string PartBSubHeader9DefaultValue { get; private set; }
		public string PartBSubHeader10DefaultValue { get; private set; }
		public string PartBSubHeader11DefaultValue { get; private set; }
		public string PartBSubHeader12DefaultValue { get; private set; }
		public string PartBSubHeader13DefaultValue { get; private set; }
		public string PartBSubHeader14DefaultValue { get; private set; }
		public string PartBSubHeader15DefaultValue { get; private set; }
		public string PartBSubHeader16DefaultValue { get; private set; }
		public string PartBSubHeader17DefaultValue { get; private set; }
		public string PartBSubHeader18DefaultValue { get; private set; }
		public string PartBSubHeader19DefaultValue { get; private set; }
		public string PartBSubHeader20DefaultValue { get; private set; }
		public string PartBSubHeader21DefaultValue { get; private set; }
		public string PartBSubHeader22DefaultValue { get; private set; }
		public string PartBSubHeader23DefaultValue { get; private set; }
		public string PartBSubHeader24DefaultValue { get; private set; }
		public string PartBSubHeader25DefaultValue { get; private set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }
		public ClipartConfiguration PartBClipart3Configuration { get; private set; }

		public List<ListDataItem> HeadersPartCItems { get; set; }
		public string PartCSubHeader1DefaultValue { get; private set; }
		public string PartCSubHeader2DefaultValue { get; private set; }
		public string PartCSubHeader3DefaultValue { get; private set; }
		public string PartCSubHeader4DefaultValue { get; private set; }
		public string PartCSubHeader5DefaultValue { get; private set; }
		public string PartCSubHeader6DefaultValue { get; private set; }
		public string PartCSubHeader7DefaultValue { get; private set; }
		public string PartCSubHeader8DefaultValue { get; private set; }
		public string PartCSubHeader9DefaultValue { get; private set; }
		public string PartCSubHeader10DefaultValue { get; private set; }
		public string PartCSubHeader11DefaultValue { get; private set; }
		public string PartCSubHeader12DefaultValue { get; private set; }
		public string PartCSubHeader13DefaultValue { get; private set; }
		public string PartCSubHeader14DefaultValue { get; private set; }
		public string PartCSubHeader15DefaultValue { get; private set; }
		public ClipartConfiguration PartCClipart1Configuration { get; private set; }
		public ClipartConfiguration PartCClipart2Configuration { get; private set; }
		public ClipartConfiguration PartCClipart3Configuration { get; private set; }

		public List<ListDataItem> HeadersPartDItems { get; set; }
		public string PartDSubHeader1DefaultValue { get; private set; }
		public string PartDSubHeader2DefaultValue { get; private set; }
		public string PartDSubHeader3DefaultValue { get; private set; }
		public string PartDSubHeader4DefaultValue { get; private set; }
		public string PartDSubHeader5DefaultValue { get; private set; }
		public string PartDSubHeader6DefaultValue { get; private set; }
		public string PartDSubHeader7DefaultValue { get; private set; }
		public string PartDSubHeader8DefaultValue { get; private set; }
		public string PartDSubHeader9DefaultValue { get; private set; }
		public string PartDSubHeader10DefaultValue { get; private set; }
		public string PartDSubHeader11DefaultValue { get; private set; }
		public string PartDSubHeader12DefaultValue { get; private set; }
		public string PartDSubHeader13DefaultValue { get; private set; }
		public string PartDSubHeader14DefaultValue { get; private set; }
		public string PartDSubHeader15DefaultValue { get; private set; }
		public string PartDSubHeader16DefaultValue { get; private set; }
		public string PartDSubHeader17DefaultValue { get; private set; }
		public ClipartConfiguration PartDClipart1Configuration { get; private set; }
		public ClipartConfiguration PartDClipart2Configuration { get; private set; }
		public ClipartConfiguration PartDClipart3Configuration { get; private set; }

		public ROIConfiguration()
		{
			HeadersPartAItems = new List<ListDataItem>();
			PartAClipart1Configuration = new ClipartConfiguration();
			PartAClipart2Configuration = new ClipartConfiguration();
			PartAClipart3Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<ListDataItem>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();
			PartBClipart3Configuration = new ClipartConfiguration();

			HeadersPartCItems = new List<ListDataItem>();
			PartCClipart1Configuration = new ClipartConfiguration();
			PartCClipart2Configuration = new ClipartConfiguration();
			PartCClipart3Configuration = new ClipartConfiguration();

			HeadersPartDItems = new List<ListDataItem>();
			PartDClipart1Configuration = new ClipartConfiguration();
			PartDClipart2Configuration = new ClipartConfiguration();
			PartDClipart3Configuration = new ClipartConfiguration();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataROIPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataROIPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP06A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP06AHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartAItems.Add(item);
							}
							break;
						case "CP06ASubHeader1":
							PartASubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06ASubHeader2":
							PartASubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06ASubHeader3":
							PartASubHeader3DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06ASubHeader4":
							PartASubHeader4DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06ASubHeader5":
							PartASubHeader5DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06ASubHeader6":
							PartASubHeader6DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06ASubHeader7":
							PartASubHeader7DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06ASubHeader8":
							PartASubHeader8DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06ASubHeader9":
							PartASubHeader9DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06ASubHeader10":
							PartASubHeader10DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06ASubHeader11":
							PartASubHeader11DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06ASubHeader12":
							PartASubHeader12DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06ASubHeader13":
							PartASubHeader13DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06ASubHeader14":
							PartASubHeader14DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06ASubHeader15":
							PartASubHeader15DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}
				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP06AClipart1");
				PartAClipart2Configuration = ClipartConfiguration.FromXml(node, "CP06AClipart2");
				PartAClipart3Configuration = ClipartConfiguration.FromXml(node, "CP06AClipart3");
			}

			if (resourceManager.DataROIPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataROIPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP06B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP06BHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartBItems.Add(item);
							}
							break;
						case "CP06BSubHeader1":
							PartBSubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader2":
							PartBSubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader3":
							PartBSubHeader3DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader4":
							PartBSubHeader4DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader5":
							PartBSubHeader5DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader6":
							PartBSubHeader6DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader7":
							PartBSubHeader7DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader8":
							PartBSubHeader8DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader9":
							PartBSubHeader9DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader10":
							PartBSubHeader10DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader11":
							PartBSubHeader11DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader12":
							PartBSubHeader12DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader13":
							PartBSubHeader13DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader14":
							PartBSubHeader14DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader15":
							PartBSubHeader15DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader16":
							PartBSubHeader16DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader17":
							PartBSubHeader17DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader18":
							PartBSubHeader18DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader19":
							PartBSubHeader19DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader20":
							PartBSubHeader20DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader21":
							PartBSubHeader21DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader22":
							PartBSubHeader22DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader23":
							PartBSubHeader23DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader24":
							PartBSubHeader24DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06BSubHeader25":
							PartBSubHeader25DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}
				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP06BClipart1");
				PartBClipart2Configuration = ClipartConfiguration.FromXml(node, "CP06BClipart2");
				PartBClipart3Configuration = ClipartConfiguration.FromXml(node, "CP06BClipart3");
			}

			if (resourceManager.DataROIPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataROIPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP06C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP06CHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartCItems.Add(item);
							}
							break;
						case "CP06CSubHeader1":
							PartCSubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06CSubHeader2":
							PartCSubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06CSubHeader3":
							PartCSubHeader3DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06CSubHeader4":
							PartCSubHeader4DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06CSubHeader5":
							PartCSubHeader5DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06CSubHeader6":
							PartCSubHeader6DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06CSubHeader7":
							PartCSubHeader7DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06CSubHeader8":
							PartCSubHeader8DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06CSubHeader9":
							PartCSubHeader9DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06CSubHeader10":
							PartCSubHeader10DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06CSubHeader11":
							PartCSubHeader11DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06CSubHeader12":
							PartCSubHeader12DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06CSubHeader13":
							PartCSubHeader13DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06CSubHeader14":
							PartCSubHeader14DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06CSubHeader15":
							PartCSubHeader15DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}
				PartCClipart1Configuration = ClipartConfiguration.FromXml(node, "CP06CClipart1");
				PartCClipart2Configuration = ClipartConfiguration.FromXml(node, "CP06CClipart2");
				PartCClipart3Configuration = ClipartConfiguration.FromXml(node, "CP06CClipart3");
			}

			if (resourceManager.DataROIPartDFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataROIPartDFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP06D");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP06DHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartDItems.Add(item);
							}
							break;
						case "CP06DSubHeader1":
							PartDSubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06DSubHeader2":
							PartDSubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06DSubHeader3":
							PartDSubHeader3DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06DSubHeader4":
							PartDSubHeader4DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06DSubHeader5":
							PartDSubHeader5DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06DSubHeader6":
							PartDSubHeader6DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06DSubHeader7":
							PartDSubHeader7DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06DSubHeader8":
							PartDSubHeader8DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06DSubHeader9":
							PartDSubHeader9DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06DSubHeader10":
							PartDSubHeader10DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06DSubHeader11":
							PartDSubHeader11DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06DSubHeader12":
							PartDSubHeader12DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06DSubHeader13":
							PartDSubHeader13DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06DSubHeader14":
							PartDSubHeader14DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06DSubHeader15":
							PartDSubHeader15DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06DSubHeader16":
							PartDSubHeader16DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP06DSubHeader17":
							PartDSubHeader17DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}
				PartDClipart1Configuration = ClipartConfiguration.FromXml(node, "CP06DClipart1");
				PartDClipart2Configuration = ClipartConfiguration.FromXml(node, "CP06DClipart2");
				PartDClipart3Configuration = ClipartConfiguration.FromXml(node, "CP06DClipart3");
			}
		}
	}
}
