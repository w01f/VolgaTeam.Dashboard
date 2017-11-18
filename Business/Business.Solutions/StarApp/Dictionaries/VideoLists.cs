using System;
using System.Collections.Generic;
using System.Xml;
using Asa.Business.Solutions.StarApp.Configuration;

namespace Asa.Business.Solutions.StarApp.Dictionaries
{
	public class VideoLists
	{
		public List<SlideHeader> HeadersPartA { get; set; }
		public List<SlideHeader> HeadersPartB { get; set; }
		public List<SlideHeader> HeadersPartC { get; set; }
		public List<SlideHeader> HeadersPartD { get; set; }

		public VideoLists()
		{
			HeadersPartA = new List<SlideHeader>();
			HeadersPartB = new List<SlideHeader>();
			HeadersPartC = new List<SlideHeader>();
			HeadersPartD = new List<SlideHeader>();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataVideoPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataVideoPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/Video");
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

			if (resourceManager.DataVideoPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataVideoPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/Video");
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

			if (resourceManager.DataVideoPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataVideoPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/Video");
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

			if (resourceManager.DataVideoPartDFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataVideoPartDFile.LocalPath);

				var node = document.SelectSingleNode(@"/Video");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "SlideHeader":
							var header = SlideHeader.FromXml(childNode);
							if (!String.IsNullOrEmpty(header.Value))
								HeadersPartD.Add(header);
							break;
					}
				}
			}
		}
	}
}
