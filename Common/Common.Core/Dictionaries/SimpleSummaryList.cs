﻿using System.Collections.Generic;
using System.Xml;
using Asa.Common.Core.Configuration;

namespace Asa.Common.Core.Dictionaries
{
	public class SimpleSummaryLists
	{
		public SimpleSummaryLists()
		{
			Headers = new List<string>();
			Details = new List<string>();
		}

		public List<string> Headers { get; set; }
		public List<string> Details { get; set; }

		public void Load()
		{
			var document = new XmlDocument();
			document.Load(ResourceManager.Instance.DataSimpleSummaryFile.LocalPath);

			var node = document.SelectSingleNode(@"/SimpleSummary");
			if (node != null)
			{
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "SlideHeader":
							foreach (XmlAttribute attribute in childNode.Attributes)
							{
								switch (attribute.Name)
								{
									case "Value":
										if (!string.IsNullOrEmpty(attribute.Value))
											Headers.Add(attribute.Value);
										break;
								}
							}
							break;
						case "Detail":
							foreach (XmlAttribute attribute in childNode.Attributes)
							{
								switch (attribute.Name)
								{
									case "Value":
										if (!string.IsNullOrEmpty(attribute.Value))
											Details.Add(attribute.Value);
										break;
								}
							}
							break;
					}
				}
			}
		}
	}
}
