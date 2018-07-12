using System.Collections.Generic;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Dashboard.Dictionaries
{
	public class SimpleSummaryLists
	{
		public List<ListDataItem> Headers { get; set; }
		public List<string> Details { get; set; }

		public SimpleSummaryLists()
		{
			Headers = new List<ListDataItem>();
			Details = new List<string>();
		}

		public void Load(StorageFile dataFile)
		{
			var document = new XmlDocument();
			document.Load(dataFile.LocalPath);

			var node = document.SelectSingleNode(@"/SimpleSummary");
			if (node != null)
			{
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "SlideHeader":
							Headers.Add(ListDataItem.FromXml(childNode));
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
