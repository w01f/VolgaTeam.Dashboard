using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class ShareConfiguration
	{
		public List<ListDataItem> HeadersPartAItems { get; set; }
		public List<ListDataItem> PartACombo1Items { get; }
		public List<ListDataItem> PartACombo2Items { get; }
		public List<ListDataItem> PartACombo3Items { get; }
		public List<ListDataItem> PartACombo4Items { get; }
		public string PartASubHeader1DefaultValue { get; private set; }
		public string PartASubHeader2DefaultValue { get; private set; }
		public string PartASubHeader3DefaultValue { get; private set; }
		public string PartASubHeader4DefaultValue { get; private set; }
		public string PartASubHeader5DefaultValue { get; private set; }
		public string PartASubHeader6DefaultValue { get; private set; }
		public string PartASubHeader7DefaultValue { get; private set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }
		public ClipartConfiguration PartAClipart2Configuration { get; private set; }
		public ClipartConfiguration PartAClipart3Configuration { get; private set; }

		public List<ListDataItem> HeadersPartBItems { get; set; }
		public List<ListDataItem> PartBCombo1Items { get; }
		public List<ListDataItem> PartBCombo2Items { get; }
		public string PartBSubHeader1DefaultValue { get; private set; }
		public string PartBSubHeader2DefaultValue { get; private set; }
		public string PartBSubHeader3DefaultValue { get; private set; }
		public string PartBSubHeader4DefaultValue { get; private set; }
		public string PartBSubHeader5DefaultValue { get; private set; }
		public string PartBSubHeader6DefaultValue { get; private set; }
		public string PartBSubHeader7DefaultValue { get; private set; }
		public string PartBSubHeader8DefaultValue { get; private set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }
		public ClipartConfiguration PartBClipart3Configuration { get; private set; }

		public List<ListDataItem> HeadersPartCItems { get; set; }
		public List<ListDataItem> PartCCombo1Items { get; }
		public List<ListDataItem> PartCCombo2Items { get; }
		public List<ListDataItem> PartCCombo3Items { get; }
		public List<ListDataItem> PartCCombo4Items { get; }
		public List<ListDataItem> PartCCombo5Items { get; }
		public List<ListDataItem> PartCCombo6Items { get; }
		public string PartCSubHeader1DefaultValue { get; private set; }
		public string PartCSubHeader2DefaultValue { get; private set; }
		public string PartCSubHeader3DefaultValue { get; private set; }
		public string PartCSubHeader4DefaultValue { get; private set; }
		public ClipartConfiguration PartCClipart1Configuration { get; private set; }
		public ClipartConfiguration PartCClipart2Configuration { get; private set; }
		public ClipartConfiguration PartCClipart3Configuration { get; private set; }

		public List<ListDataItem> HeadersPartDItems { get; set; }
		public List<ListDataItem> PartDCombo1Items { get; }
		public List<ListDataItem> PartDCombo2Items { get; }
		public List<ListDataItem> PartDCombo3Items { get; }
		public string PartDSubHeader1DefaultValue { get; private set; }
		public string PartDSubHeader2DefaultValue { get; private set; }
		public string PartDSubHeader3DefaultValue { get; private set; }
		public string PartDSubHeader4DefaultValue { get; private set; }
		public string PartDSubHeader5DefaultValue { get; private set; }
		public string PartDSubHeader6DefaultValue { get; private set; }
		public string PartDSubHeader7DefaultValue { get; private set; }
		public string PartDSubHeader8DefaultValue { get; private set; }
		public string PartDSubHeader9DefaultValue { get; private set; }
		public ClipartConfiguration PartDClipart1Configuration { get; private set; }
		public ClipartConfiguration PartDClipart2Configuration { get; private set; }
		public ClipartConfiguration PartDClipart3Configuration { get; private set; }

		public List<ListDataItem> HeadersPartEItems { get; set; }
		public List<ListDataItem> PartECombo1Items { get; }
		public List<ListDataItem> PartECombo2Items { get; }
		public List<ListDataItem> PartECombo3Items { get; }
		public List<ListDataItem> PartECombo4Items { get; }
		public string PartESubHeader1DefaultValue { get; private set; }
		public string PartESubHeader2DefaultValue { get; private set; }
		public string PartESubHeader3DefaultValue { get; private set; }
		public string PartESubHeader4DefaultValue { get; private set; }
		public string PartESubHeader5DefaultValue { get; private set; }
		public string PartESubHeader6DefaultValue { get; private set; }
		public string PartESubHeader7DefaultValue { get; private set; }
		public string PartESubHeader8DefaultValue { get; private set; }
		public string PartESubHeader9DefaultValue { get; private set; }
		public string PartESubHeader10DefaultValue { get; private set; }
		public ClipartConfiguration PartEClipart1Configuration { get; private set; }
		public ClipartConfiguration PartEClipart2Configuration { get; private set; }
		public ClipartConfiguration PartEClipart3Configuration { get; private set; }

		public ShareConfiguration()
		{
			HeadersPartAItems = new List<ListDataItem>();
			PartACombo1Items = new List<ListDataItem>();
			PartACombo2Items = new List<ListDataItem>();
			PartACombo3Items = new List<ListDataItem>();
			PartACombo4Items = new List<ListDataItem>();
			PartAClipart1Configuration = new ClipartConfiguration();
			PartAClipart2Configuration = new ClipartConfiguration();
			PartAClipart3Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<ListDataItem>();
			PartBCombo1Items = new List<ListDataItem>();
			PartBCombo2Items = new List<ListDataItem>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();
			PartBClipart3Configuration = new ClipartConfiguration();

			HeadersPartCItems = new List<ListDataItem>();
			PartCCombo1Items = new List<ListDataItem>();
			PartCCombo2Items = new List<ListDataItem>();
			PartCCombo3Items = new List<ListDataItem>();
			PartCCombo4Items = new List<ListDataItem>();
			PartCCombo5Items = new List<ListDataItem>();
			PartCCombo6Items = new List<ListDataItem>();
			PartCClipart1Configuration = new ClipartConfiguration();
			PartCClipart2Configuration = new ClipartConfiguration();
			PartCClipart3Configuration = new ClipartConfiguration();

			HeadersPartDItems = new List<ListDataItem>();
			PartDCombo1Items = new List<ListDataItem>();
			PartDCombo2Items = new List<ListDataItem>();
			PartDCombo3Items = new List<ListDataItem>();
			PartDClipart1Configuration = new ClipartConfiguration();
			PartDClipart2Configuration = new ClipartConfiguration();
			PartDClipart3Configuration = new ClipartConfiguration();

			HeadersPartEItems = new List<ListDataItem>();
			PartECombo1Items = new List<ListDataItem>();
			PartECombo2Items = new List<ListDataItem>();
			PartECombo3Items = new List<ListDataItem>();
			PartECombo4Items = new List<ListDataItem>();
			PartEClipart1Configuration = new ClipartConfiguration();
			PartEClipart2Configuration = new ClipartConfiguration();
			PartEClipart3Configuration = new ClipartConfiguration();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataSharePartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSharePartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP05A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP05AHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartAItems.Add(item);
							}
							break;
						case "CP05ABillionCombo1":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartACombo1Items.Add(item);
							}
							break;
						case "CP05APercentCombo2":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartACombo2Items.Add(item);
							}
							break;
						case "CP05APopulationCombo3":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartACombo3Items.Add(item);
							}
							break;
						case "CP05ASharePointCombo4":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartACombo4Items.Add(item);
							}
							break;
						case "CP05ASubHeader1":
							PartASubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05ASubHeader2":
							PartASubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05ASubHeader3":
							PartASubHeader3DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05ASubHeader4":
							PartASubHeader4DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05ASubHeader5":
							PartASubHeader5DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05ASubHeader6":
							PartASubHeader6DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05ASubHeader7":
							PartASubHeader7DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}

				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP05AClipart1");
				PartAClipart2Configuration = ClipartConfiguration.FromXml(node, "CP05AClipart2");
				PartAClipart3Configuration = ClipartConfiguration.FromXml(node, "CP05AClipart3");
			}

			if (resourceManager.DataSharePartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSharePartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP05B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP05BHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartBItems.Add(item);
							}
							break;
						case "CP05BHouseholdsCombo1":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartBCombo1Items.Add(item);
							}
							break;
						case "CP05BSharePointCombo2":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartBCombo2Items.Add(item);
							}
							break;
						case "CP05BSubHeader1":
							PartBSubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05BSubHeader2":
							PartBSubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05BSubHeader3":
							PartBSubHeader3DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05BSubHeader4":
							PartBSubHeader4DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05BSubHeader5":
							PartBSubHeader5DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05BSubHeader6":
							PartBSubHeader6DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05BSubHeader7":
							PartBSubHeader7DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05BSubHeader8":
							PartBSubHeader8DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}
				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP05BClipart1");
				PartBClipart2Configuration = ClipartConfiguration.FromXml(node, "CP05BClipart2");
				PartBClipart3Configuration = ClipartConfiguration.FromXml(node, "CP05BClipart3");
			}

			if (resourceManager.DataSharePartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSharePartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP05C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP05CHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartCItems.Add(item);
							}
							break;
						case "CP05CBillionCombo1":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartCCombo1Items.Add(item);
							}
							break;
						case "CP05CYearCombo2":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartCCombo2Items.Add(item);
							}
							break;
						case "CP05CGeographyCombo3":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartCCombo3Items.Add(item);
							}
							break;
						case "CP05CPercentCombo4":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartCCombo4Items.Add(item);
							}
							break;
						case "CP05CPopulationCombo5":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartCCombo5Items.Add(item);
							}
							break;
						case "CP05CSharePercentCombo6":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartCCombo6Items.Add(item);
							}
							break;
						case "CP05CSubheader1":
							PartCSubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05CSubheader2":
							PartCSubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05CSubheader3":
							PartCSubHeader3DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05CSubheader4":
							PartCSubHeader4DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}
				PartCClipart1Configuration = ClipartConfiguration.FromXml(node, "CP05CClipart1");
				PartCClipart2Configuration = ClipartConfiguration.FromXml(node, "CP05CClipart2");
				PartCClipart3Configuration = ClipartConfiguration.FromXml(node, "CP05CClipart3");
			}

			if (resourceManager.DataSharePartDFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSharePartDFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP05D");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP05DHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartDItems.Add(item);
							}
							break;
						case "CP05DPercentCombo1":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartDCombo1Items.Add(item);
							}
							break;
						case "CP05DPopulationCombo2":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartDCombo2Items.Add(item);
							}
							break;
						case "CP05DSharePercentCombo6":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartDCombo3Items.Add(item);
							}
							break;
						case "CP05DSubheader1":
							PartDSubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05DSubheader2":
							PartDSubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05DSubHeader3":
							PartDSubHeader3DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05DSubHeader4":
							PartDSubHeader4DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05DSubHeader5":
							PartDSubHeader5DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05DSubHeader6":
							PartDSubHeader6DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05DSubHeader7":
							PartDSubHeader7DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05DSubHeader8":
							PartDSubHeader8DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05DSubHeader9":
							PartDSubHeader9DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}
				PartDClipart1Configuration = ClipartConfiguration.FromXml(node, "CP05DClipart1");
				PartDClipart2Configuration = ClipartConfiguration.FromXml(node, "CP05DClipart2");
				PartDClipart3Configuration = ClipartConfiguration.FromXml(node, "CP05DClipart3");
			}

			if (resourceManager.DataSharePartEFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSharePartEFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP05E");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP05EHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartEItems.Add(item);
							}
							break;
						case "CP05DBillionCombo1":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartECombo1Items.Add(item);
							}
							break;
						case "CP05EPercentCombo2":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartECombo2Items.Add(item);
							}
							break;
						case "CP05EPopulationCombo3":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartECombo3Items.Add(item);
							}
							break;
						case "CP05ESharePercentCombo3":
						{
							var item = ListDataItem.FromXml(childNode);
							if (!String.IsNullOrEmpty(item.Value))
								PartECombo4Items.Add(item);
						}
							break;
						case "CP05ESubheader1":
							PartESubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05ESubheader2":
							PartESubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05ESubheader3":
							PartESubHeader3DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05ESubheader4":
							PartESubHeader4DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05ESubheader5":
							PartESubHeader5DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05ESubheader6":
							PartESubHeader6DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05ESubheader7":
							PartESubHeader7DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05ESubheader8":
							PartESubHeader8DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05ESubheader9":
							PartESubHeader9DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP05ESubheader10":
							PartESubHeader10DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}
				PartEClipart1Configuration = ClipartConfiguration.FromXml(node, "CP05EClipart1");
				PartEClipart2Configuration = ClipartConfiguration.FromXml(node, "CP05EClipart2");
				PartEClipart3Configuration = ClipartConfiguration.FromXml(node, "CP05EClipart3");
			}
		}
	}
}
