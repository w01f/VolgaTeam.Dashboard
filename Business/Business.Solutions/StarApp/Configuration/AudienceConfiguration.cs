using System;
using System.Collections.Generic;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class AudienceConfiguration
	{
		public List<ListDataItem> HeadersPartAItems { get; set; }
		public string PartASubHeader1DefaultValue { get; private set; }
		public string PartASubHeader2DefaultValue { get; private set; }
		public string PartASubHeader1Placeholder { get; private set; }
		public string PartASubHeader2Placeholder { get; private set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }
		public ClipartConfiguration PartAClipart2Configuration { get; private set; }

		public List<ListDataItem> HeadersPartBItems { get; set; }
		public string PartBSubHeader1DefaultValue { get; private set; }
		public string PartBSubHeader2DefaultValue { get; private set; }
		public string PartBSubHeader3DefaultValue { get; private set; }
		public string PartBSubHeader4DefaultValue { get; private set; }
		public string PartBSubHeader5DefaultValue { get; private set; }
		public string PartBSubHeader6DefaultValue { get; private set; }
		public string PartBSubHeader1Placeholder { get; private set; }
		public string PartBSubHeader2Placeholder { get; private set; }
		public string PartBSubHeader3Placeholder { get; private set; }
		public string PartBSubHeader4Placeholder { get; private set; }
		public string PartBSubHeader5Placeholder { get; private set; }
		public string PartBSubHeader6Placeholder { get; private set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }
		public ClipartConfiguration PartBClipart3Configuration { get; private set; }

		public List<ListDataItem> HeadersPartCItems { get; set; }
		public List<ListDataItem> PartCCombo1Items { get; }
		public ClipartConfiguration PartCClipart1Configuration { get; private set; }
		public ClipartConfiguration PartCClipart2Configuration { get; private set; }
		public ClipartConfiguration PartCClipart3Configuration { get; private set; }
		public ClipartConfiguration PartCClipart4Configuration { get; private set; }

		public AudienceConfiguration()
		{
			HeadersPartAItems = new List<ListDataItem>();
			PartAClipart1Configuration = new ClipartConfiguration();
			PartAClipart2Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<ListDataItem>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();
			PartBClipart3Configuration = new ClipartConfiguration();

			HeadersPartCItems = new List<ListDataItem>();
			PartCCombo1Items = new List<ListDataItem>();
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
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP09AHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartAItems.Add(item);
							break;
						case "CP09ASubheader1":
							if (item.IsPlaceholder)
								PartASubHeader1Placeholder = item.Value;
							else
								PartASubHeader1DefaultValue = item.Value;
							break;
						case "CP09ASubheader2":
							if (item.IsPlaceholder)
								PartASubHeader2Placeholder = item.Value;
							else
								PartASubHeader2DefaultValue = item.Value;
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
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP09BHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartBItems.Add(item);
							break;
						case "CP09BSubheader1":
							if (item.IsPlaceholder)
								PartBSubHeader1Placeholder = item.Value;
							else
								PartBSubHeader1DefaultValue = item.Value;
							break;
						case "CP09BSubheader2":
							if (item.IsPlaceholder)
								PartBSubHeader2Placeholder = item.Value;
							else
								PartBSubHeader2DefaultValue = item.Value;
							break;
						case "CP09BSubheader3":
							if (item.IsPlaceholder)
								PartBSubHeader3Placeholder = item.Value;
							else
								PartBSubHeader3DefaultValue = item.Value;
							break;
						case "CP09BSubheader4":
							if (item.IsPlaceholder)
								PartBSubHeader4Placeholder = item.Value;
							else
								PartBSubHeader4DefaultValue = item.Value;
							break;
						case "CP09BSubheader5":
							if (item.IsPlaceholder)
								PartBSubHeader5Placeholder = item.Value;
							else
								PartBSubHeader5DefaultValue = item.Value;
							break;
						case "CP09BSubheader6":
							if (item.IsPlaceholder)
								PartBSubHeader6Placeholder = item.Value;
							else
								PartBSubHeader6DefaultValue = item.Value;
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
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP09CHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartCItems.Add(item);
							break;
						case "CP09CCombo1":
							if (!String.IsNullOrEmpty(item.Value))
								PartCCombo1Items.Add(item);
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
