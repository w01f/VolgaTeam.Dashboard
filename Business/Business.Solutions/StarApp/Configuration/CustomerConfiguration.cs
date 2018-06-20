﻿using System;
using System.Collections.Generic;
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
		public string PartBSubHeader1Placeholder { get; private set; }
		public string PartBSubHeader2Placeholder { get; private set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }

		public List<ListDataItem> HeadersPartCItems { get; set; }
		public string PartCSubHeader1DefaultValue { get; private set; }
		public string PartCSubHeader2DefaultValue { get; private set; }
		public string PartCSubHeader3DefaultValue { get; private set; }
		public string PartCSubHeader1Placeholder { get; private set; }
		public string PartCSubHeader2Placeholder { get; private set; }
		public string PartCSubHeader3Placeholder { get; private set; }

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
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP04AHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartAItems.Add(item);
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
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP04BHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartBItems.Add(item);
							break;
						case "CP04BSubheader1":
							if (item.IsPlaceholder)
								PartBSubHeader1Placeholder = item.Value;
							else
								PartBSubHeader1DefaultValue = item.Value;
							break;
						case "CP04BSubheader2":
							if (item.IsPlaceholder)
								PartBSubHeader2Placeholder = item.Value;
							else
								PartBSubHeader2DefaultValue = item.Value;
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
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP04CHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartCItems.Add(item);
							break;
						case "CP04CSubheader1":
							if (item.IsPlaceholder)
								PartCSubHeader1Placeholder = item.Value;
							else
								PartCSubHeader1DefaultValue = item.Value;
							break;
						case "CP04CSubheader2":
							if (item.IsPlaceholder)
								PartCSubHeader2Placeholder = item.Value;
							else
								PartCSubHeader2DefaultValue = item.Value;
							break;
						case "CP04CSubheader3":
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
