using System;
using System.Collections.Generic;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class FishingConfiguration
	{
		public List<ListDataItem> HeadersPartAItems { get; set; }
		public string PartASubHeader1DefaultValue { get; private set; }
		public string PartASubHeader2DefaultValue { get; private set; }
		public string PartASubHeader1Placeholder { get; private set; }
		public string PartASubHeader2Placeholder { get; private set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }

		public List<ListDataItem> HeadersPartBItems { get; set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }

		public List<ListDataItem> HeadersPartCItems { get; set; }
		public string PartCSubHeader1DefaultValue { get; private set; }
		public string PartCSubHeader2DefaultValue { get; private set; }
		public string PartCSubHeader3DefaultValue { get; private set; }
		public string PartCSubHeader1Placeholder { get; private set; }
		public string PartCSubHeader2Placeholder { get; private set; }
		public string PartCSubHeader3Placeholder { get; private set; }

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
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP03AHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartAItems.Add(item);
							break;
						case "CP03ASubheader1":
							if (item.IsPlaceholder)
								PartASubHeader1Placeholder = item.Value;
							else
								PartASubHeader1DefaultValue = item.Value;
							break;
						case "CP03ASubheader2":
							if (item.IsPlaceholder)
								PartASubHeader2Placeholder = item.Value;
							else
								PartASubHeader2DefaultValue = item.Value;
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
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP03BHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartBItems.Add(item);
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
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP03CHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartCItems.Add(item);
							break;
						case "CP03CSubheader1":
							if (item.IsPlaceholder)
								PartCSubHeader1Placeholder = item.Value;
							else
								PartCSubHeader1DefaultValue = item.Value;
							break;
						case "CP03CSubheader2":
							if (item.IsPlaceholder)
								PartCSubHeader2Placeholder = item.Value;
							else
								PartCSubHeader2DefaultValue = item.Value;
							break;
						case "CP03CSubheader3":
							if (item.IsPlaceholder)
								PartCSubHeader3Placeholder = item.Value;
							else
								PartCSubHeader3DefaultValue = item.Value;
							break;
					}
				}
			}
		}
	}
}
