﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class CoverConfiguration
	{
		public List<ListDataItem> HeaderPartAItems { get; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }
		public string SubHeader1DefaultValue { get; private set; }

		public CoverConfiguration()
		{
			HeaderPartAItems = new List<ListDataItem>();
			PartAClipart1Configuration = new ClipartConfiguration();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataCoverPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCoverPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP01A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP01AHeader":
							{
								var item = ListDataItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeaderPartAItems.Add(item);
							}
							break;
						case "CP01ASubheader1":
							SubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}

				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP01AClipart1");
			}
		}
	}
}
