using System;
using System.Collections.Generic;
using System.Xml;
using Asa.Business.Solutions.StarApp.Configuration;

namespace Asa.Business.Solutions.StarApp.Dictionaries
{
	public class AudienceLists
	{
		public List<SlideHeader> HeadersPartA { get; set; }
		public List<SlideHeader> HeadersPartB { get; set; }
		public List<SlideHeader> HeadersPartC { get; set; }

		public AudienceLists()
		{
			HeadersPartA = new List<SlideHeader>();
			HeadersPartB = new List<SlideHeader>();
			HeadersPartC = new List<SlideHeader>();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataAudiencePartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataAudiencePartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/Audience");
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

			if (resourceManager.DataAudiencePartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataAudiencePartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/Audience");
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

			if (resourceManager.DataAudiencePartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataAudiencePartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/Audience");
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
