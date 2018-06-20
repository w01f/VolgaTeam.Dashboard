using System;
using System.Collections.Generic;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class SolutionConfiguration
	{
		public List<ListDataItem> HeadersPartAItems { get; set; }
		public string PartASubHeader1DefaultValue { get; private set; }
		public string PartASubHeader1Placeholder { get; private set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }

		public List<ListDataItem> HeadersPartBItems { get; set; }
		public string PartBSubHeader1DefaultValue { get; private set; }
		public string PartBSubHeader1Placeholder { get; private set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }
		public ClipartConfiguration PartBClipart3Configuration { get; private set; }

		public List<ListDataItem> HeadersPartCItems { get; set; }
		public string PartCSubHeader1DefaultValue { get; private set; }
		public string PartCSubHeader2DefaultValue { get; private set; }
		public string PartCSubHeader1Placeholder { get; private set; }
		public string PartCSubHeader2Placeholder { get; private set; }
		public ClipartConfiguration PartCClipart1Configuration { get; private set; }
		public ClipartConfiguration PartCClipart2Configuration { get; private set; }

		public List<ListDataItem> HeadersPartDItems { get; set; }
		public string PartDSubHeader1DefaultValue { get; private set; }
		public string PartDSubHeader1Placeholder { get; private set; }
		public ClipartConfiguration PartDClipart1Configuration { get; private set; }

		public SolutionConfiguration()
		{
			HeadersPartAItems = new List<ListDataItem>();
			PartAClipart1Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<ListDataItem>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();
			PartBClipart3Configuration = new ClipartConfiguration();

			HeadersPartCItems = new List<ListDataItem>();
			PartCClipart1Configuration = new ClipartConfiguration();
			PartCClipart2Configuration = new ClipartConfiguration();

			HeadersPartDItems = new List<ListDataItem>();
			PartDClipart1Configuration = new ClipartConfiguration();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataSolutionPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP10A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP10AHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartAItems.Add(item);
							break;
						case "CP10ASubheader1":
							if (item.IsPlaceholder)
								PartASubHeader1Placeholder = item.Value;
							else
								PartASubHeader1DefaultValue = item.Value;
							break;
					}
				}
				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP10AClipart1");
			}

			if (resourceManager.DataSolutionPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP10B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP10BHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartBItems.Add(item);
							break;
						case "CP10BSubheader1":
							if (item.IsPlaceholder)
								PartBSubHeader1Placeholder = item.Value;
							else
								PartBSubHeader1DefaultValue = item.Value;
							break;
					}
				}
				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP10BClipart1");
				PartBClipart2Configuration = ClipartConfiguration.FromXml(node, "CP10BClipart2");
				PartBClipart3Configuration = ClipartConfiguration.FromXml(node, "CP10BClipart3");
			}

			if (resourceManager.DataSolutionPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP10C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP10CHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartCItems.Add(item);
							break;
						case "CP10CSubheader1":
							if (item.IsPlaceholder)
								PartCSubHeader1Placeholder = item.Value;
							else
								PartCSubHeader1DefaultValue = item.Value;
							break;
						case "CP10CSubheader2":
							if (item.IsPlaceholder)
								PartCSubHeader2Placeholder = item.Value;
							else
								PartCSubHeader2DefaultValue = item.Value;
							break;
					}
				}
				PartCClipart1Configuration = ClipartConfiguration.FromXml(node, "CP10CClipart1");
				PartCClipart2Configuration = ClipartConfiguration.FromXml(node, "CP10CClipart2");
			}

			if (resourceManager.DataSolutionPartDFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionPartDFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP10D");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP10DHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartDItems.Add(item);
							break;
						case "CP10DSubheader1":
							if (item.IsPlaceholder)
								PartDSubHeader1Placeholder = item.Value;
							else
								PartDSubHeader1DefaultValue = item.Value;
							break;
					}
				}
				PartDClipart1Configuration = ClipartConfiguration.FromXml(node, "CP10DClipart1");
			}
		}
	}
}
