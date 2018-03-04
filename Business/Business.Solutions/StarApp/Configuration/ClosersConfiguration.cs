using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class ClosersConfiguration
	{
		public List<ListDataItem> HeadersPartAItems { get; set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }
		public ClipartConfiguration PartAClipart2Configuration { get; private set; }
		public string PartASubHeader1DefaultValue { get; private set; }

		public List<ListDataItem> HeadersPartBItems { get; set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }
		public List<ListDataItem> PartBCombo1Items { get; }
		public List<ListDataItem> PartBCombo2Items { get; }
		public List<ListDataItem> PartBCombo3Items { get; }
		public List<ListDataItem> PartBCombo4Items { get; }
		public string PartBSubHeader1DefaultValue { get; private set; }
		public string PartBSubHeader2DefaultValue { get; private set; }
		public string PartBSubHeader3DefaultValue { get; private set; }

		public List<ListDataItem> HeadersPartCItems { get; set; }
		public ClipartConfiguration PartCClipart1Configuration { get; private set; }
		public ClipartConfiguration PartCClipart2Configuration { get; private set; }
		public string PartCSubHeader1DefaultValue { get; private set; }
		public string PartCSubHeader2DefaultValue { get; private set; }

		public ClosersConfiguration()
		{
			HeadersPartAItems = new List<ListDataItem>();
			PartAClipart1Configuration = new ClipartConfiguration();
			PartAClipart2Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<ListDataItem>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();
			PartBCombo1Items = new List<ListDataItem>();
			PartBCombo2Items = new List<ListDataItem>();
			PartBCombo3Items = new List<ListDataItem>();
			PartBCombo4Items = new List<ListDataItem>();

			HeadersPartCItems = new List<ListDataItem>();
			PartCClipart1Configuration = new ClipartConfiguration();
			PartCClipart2Configuration = new ClipartConfiguration();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataClosersPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataClosersPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP11A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP11AHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartAItems.Add(item);
							}
							break;
						case "CP11ASubheader1":
							PartASubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}

				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP11AClipart1");
				PartAClipart2Configuration = ClipartConfiguration.FromXml(node, "CP11AClipart2");
			}

			if (resourceManager.DataClosersPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataClosersPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP11B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP11BHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartBItems.Add(item);
							}
							break;
						case "CP11BCombo0":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartBCombo1Items.Add(item);
							}
							break;
						case "CP11BCombo1":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartBCombo2Items.Add(item);
							}
							break;
						case "CP11BCombo2":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartBCombo3Items.Add(item);
							}
							break;
						case "CP11BCombo3":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartBCombo4Items.Add(item);
							}
							break;
						case "CP11BSubheader1":
							PartBSubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP11BSubheader2":
							PartBSubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP11BSubheader3":
							PartBSubHeader3DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}

				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP11BClipart1");
				PartBClipart2Configuration = ClipartConfiguration.FromXml(node, "CP11BClipart2");
			}

			if (resourceManager.DataClosersPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataClosersPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP11C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP11CHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartCItems.Add(item);
							}
							break;
						case "CP11CSubheader1":
							PartCSubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP11CSubheader2":
							PartCSubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}

				PartCClipart1Configuration = ClipartConfiguration.FromXml(node, "CP11CClipart1");
				PartCClipart2Configuration = ClipartConfiguration.FromXml(node, "CP11CClipart2");
			}
		}
	}
}
