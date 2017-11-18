using System;
using System.Collections.Generic;
using System.Xml;
using Asa.Business.Solutions.StarApp.Configuration;

namespace Asa.Business.Solutions.StarApp.Dictionaries
{
	public class FishingLists
	{
		public List<SlideHeader> HeadersPartA { get; set; }
		public List<SlideHeader> HeadersPartB { get; set; }
		public List<SlideHeader> HeadersPartC { get; set; }

		public FishingLists()
		{
			HeadersPartA = new List<SlideHeader>();
			HeadersPartB = new List<SlideHeader>();
			HeadersPartC = new List<SlideHeader>();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataFishingPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataFishingPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/Fishing");
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

			if (resourceManager.DataFishingPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataFishingPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/Fishing");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "SlideHeader":
							var header = SlideHeader.FromXml(childNode);
							if (!String.IsNullOrEmpty(header.Value))
								HeadersPartB.Add(header);
							break;
					}
				}
			}

			if (resourceManager.DataFishingPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataFishingPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/Fishing");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "SlideHeader":
							var header = SlideHeader.FromXml(childNode);
							if (!String.IsNullOrEmpty(header.Value))
								HeadersPartC.Add(header);
							break;
					}
				}
			}
		}
	}
}
