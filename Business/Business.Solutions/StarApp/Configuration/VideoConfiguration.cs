using System;
using System.Collections.Generic;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class VideoConfiguration
	{
		public List<ListDataItem> HeadersPartAItems { get; set; }
		public string PartASubHeader1DefaultValue { get; private set; }
		public string PartASubHeader1Placeholder { get; private set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }

		public List<ListDataItem> HeadersPartBItems { get; set; }
		public string PartBSubHeader1DefaultValue { get; private set; }
		public string PartBSubHeader1Placeholder { get; private set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }

		public List<ListDataItem> HeadersPartCItems { get; set; }
		public string PartCSubHeader1DefaultValue { get; private set; }
		public string PartCSubHeader1Placeholder { get; private set; }
		public ClipartConfiguration PartCClipart1Configuration { get; private set; }

		public List<ListDataItem> HeadersPartDItems { get; set; }
		public string PartDSubHeader1DefaultValue { get; private set; }
		public string PartDSubHeader1Placeholder { get; private set; }
		public ClipartConfiguration PartDClipart1Configuration { get; private set; }

		public VideoConfiguration()
		{
			HeadersPartAItems = new List<ListDataItem>();
			PartAClipart1Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<ListDataItem>();
			PartBClipart1Configuration = new ClipartConfiguration();

			HeadersPartCItems = new List<ListDataItem>();
			PartCClipart1Configuration = new ClipartConfiguration();

			HeadersPartDItems = new List<ListDataItem>();
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
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP08AHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartAItems.Add(item);
							break;
						case "CP08ASubHeader1":
							if (item.IsPlaceholder)
								PartASubHeader1Placeholder = item.Value;
							else
								PartASubHeader1DefaultValue = item.Value;
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
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP08BHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartBItems.Add(item);
							break;
						case "CP08BSubheader1":
							if (item.IsPlaceholder)
								PartBSubHeader1Placeholder = item.Value;
							else
								PartBSubHeader1DefaultValue = item.Value;
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
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP08CHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartCItems.Add(item);
							break;
						case "CP08CSubheader1":
							if (item.IsPlaceholder)
								PartCSubHeader1Placeholder = item.Value;
							else
								PartCSubHeader1DefaultValue = item.Value;
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
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP08DHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartDItems.Add(item);
							break;
						case "CP08DSubheader1":
							if (item.IsPlaceholder)
								PartDSubHeader1Placeholder = item.Value;
							else
								PartDSubHeader1DefaultValue = item.Value;
							break;
					}
				}

				PartDClipart1Configuration = ClipartConfiguration.FromXml(node, "CP08DClipart1");
			}
		}
	}
}
