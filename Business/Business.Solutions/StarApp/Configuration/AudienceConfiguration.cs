using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class AudienceConfiguration
	{
		public List<SlideHeader> HeadersPartAItems { get; set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }
		public ClipartConfiguration PartAClipart2Configuration { get; private set; }

		public List<SlideHeader> HeadersPartBItems { get; set; }
		public string PartBSubHeader1DefaultValue { get; private set; }
		public string PartBSubHeader2DefaultValue { get; private set; }
		public string PartBSubHeader3DefaultValue { get; private set; }
		public string PartBSubHeader4DefaultValue { get; private set; }
		public string PartBSubHeader5DefaultValue { get; private set; }
		public string PartBSubHeader6DefaultValue { get; private set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }
		public ClipartConfiguration PartBClipart3Configuration { get; private set; }

		public List<SlideHeader> HeadersPartCItems { get; set; }
		public List<ComboboxItem> PartCCombo1Items { get; }
		public ClipartConfiguration PartCClipart1Configuration { get; private set; }
		public ClipartConfiguration PartCClipart2Configuration { get; private set; }
		public ClipartConfiguration PartCClipart3Configuration { get; private set; }
		public ClipartConfiguration PartCClipart4Configuration { get; private set; }

		public AudienceConfiguration()
		{
			HeadersPartAItems = new List<SlideHeader>();
			PartAClipart1Configuration = new ClipartConfiguration();
			PartAClipart2Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<SlideHeader>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();
			PartBClipart3Configuration = new ClipartConfiguration();

			HeadersPartCItems = new List<SlideHeader>();
			PartCCombo1Items = new List<ComboboxItem>();
			PartCClipart1Configuration = new ClipartConfiguration();
			PartCClipart2Configuration = new ClipartConfiguration();
			PartCClipart3Configuration = new ClipartConfiguration();
			PartCClipart4Configuration = new ClipartConfiguration();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataAudiencePartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataAudiencePartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP09A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP09AHeader":
							{
								var item = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartAItems.Add(item);
							}
							break;
					}
				}

				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP09AClipart1");
				PartAClipart2Configuration = ClipartConfiguration.FromXml(node, "CP09AClipart2");
			}

			if (resourceManager.DataAudiencePartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataAudiencePartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP09B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP09BHeader":
							{
								var item = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartBItems.Add(item);
							}
							break;
						case "CP09BSubHeader1":
							PartBSubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP09BSubHeader2":
							PartBSubHeader2DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP09BSubHeader3":
							PartBSubHeader3DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP09BSubHeader4":
							PartBSubHeader4DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP09BSubHeader5":
							PartBSubHeader5DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
						case "CP09BSubHeader6":
							PartBSubHeader6DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}

				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP09BClipart1");
				PartBClipart2Configuration = ClipartConfiguration.FromXml(node, "CP09BClipart2");
				PartBClipart3Configuration = ClipartConfiguration.FromXml(node, "CP09BClipart3");
			}

			if (resourceManager.DataAudiencePartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataAudiencePartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP09C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP09CHeader":
							{
								var item = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartCItems.Add(item);
							}
							break;
					}
				}

				PartCClipart1Configuration = ClipartConfiguration.FromXml(node, "CP09CClipart1");
				PartCClipart2Configuration = ClipartConfiguration.FromXml(node, "CP09CClipart2");
				PartCClipart3Configuration = ClipartConfiguration.FromXml(node, "CP09CClipart3");
				PartCClipart4Configuration = ClipartConfiguration.FromXml(node, "CP09CClipart4");
			}
		}
	}
}
