using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class VideoConfiguration
	{
		public List<SlideHeader> HeadersPartAItems { get; set; }
		public string PartASubHeader1DefaultValue { get; private set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }

		public List<SlideHeader> HeadersPartBItems { get; set; }
		public string PartBSubHeader1DefaultValue { get; private set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }

		public List<SlideHeader> HeadersPartCItems { get; set; }
		public ClipartConfiguration PartCClipart1Configuration { get; private set; }

		public List<SlideHeader> HeadersPartDItems { get; set; }
		public ClipartConfiguration PartDClipart1Configuration { get; private set; }

		public VideoConfiguration()
		{
			HeadersPartAItems = new List<SlideHeader>();
			PartAClipart1Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<SlideHeader>();
			PartBClipart1Configuration = new ClipartConfiguration();

			HeadersPartCItems = new List<SlideHeader>();
			PartCClipart1Configuration = new ClipartConfiguration();

			HeadersPartDItems = new List<SlideHeader>();
			PartDClipart1Configuration = new ClipartConfiguration();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataVideoPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataVideoPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP08A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP08AHeader":
							{
								var item = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartAItems.Add(item);
							}
							break;
						case "CP08ASubHeader1":
							PartASubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}

				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP08AClipart1");
			}

			if (resourceManager.DataVideoPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataVideoPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP08B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP08BHeader":
							{
								var item = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartBItems.Add(item);
							}
							break;
						case "CP08BSubheader1":
							PartBSubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}

				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP08BClipart1");
			}

			if (resourceManager.DataVideoPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataVideoPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP08C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP08CHeader":
							{
								var item = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartCItems.Add(item);
							}
							break;
					}
				}

				PartCClipart1Configuration = ClipartConfiguration.FromXml(node, "CP08CClipart1");
			}

			if (resourceManager.DataVideoPartDFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataVideoPartDFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP08D");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP08DHeader":
							{
								var item = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartDItems.Add(item);
							}
							break;
					}
				}

				PartDClipart1Configuration = ClipartConfiguration.FromXml(node, "CP08DClipart1");
			}
		}
	}
}
