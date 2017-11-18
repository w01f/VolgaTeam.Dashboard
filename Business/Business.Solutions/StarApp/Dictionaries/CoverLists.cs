using System;
using System.Collections.Generic;
using System.Xml;
using Asa.Business.Solutions.StarApp.Configuration;

namespace Asa.Business.Solutions.StarApp.Dictionaries
{
	public class CoverLists
	{
		public List<SlideHeader> HeadersPartA { get; set; }

		public CoverLists()
		{
			HeadersPartA = new List<SlideHeader>();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataCoverPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCoverPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/Cover");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "SlideHeader":
							var header = SlideHeader.FromXml(childNode);
							if (!String.IsNullOrEmpty(header.Value))
								HeadersPartA.Add(header);
							break;
					}
				}
			}
		}
	}
}
