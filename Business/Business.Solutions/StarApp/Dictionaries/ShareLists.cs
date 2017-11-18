using System;
using System.Collections.Generic;
using System.Xml;
using Asa.Business.Solutions.StarApp.Configuration;

namespace Asa.Business.Solutions.StarApp.Dictionaries
{
	public class ShareLists
	{
		public List<SlideHeader> HeadersPartA { get; set; }
		public List<SlideHeader> HeadersPartB { get; set; }
		public List<SlideHeader> HeadersPartC { get; set; }
		public List<SlideHeader> HeadersPartD { get; set; }
		public List<SlideHeader> HeadersPartE { get; set; }

		public ShareLists()
		{
			HeadersPartA = new List<SlideHeader>();
			HeadersPartB = new List<SlideHeader>();
			HeadersPartC = new List<SlideHeader>();
			HeadersPartD = new List<SlideHeader>();
			HeadersPartE = new List<SlideHeader>();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataSharePartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSharePartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/Share");
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

			if (resourceManager.DataSharePartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSharePartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/Share");
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

			if (resourceManager.DataSharePartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSharePartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/Share");
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

			if (resourceManager.DataSharePartDFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSharePartDFile.LocalPath);

				var node = document.SelectSingleNode(@"/Share");
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

			if (resourceManager.DataSharePartEFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSharePartEFile.LocalPath);

				var node = document.SelectSingleNode(@"/Share");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "SlideHeader":
							var header = SlideHeader.FromXml(childNode);
							if (!String.IsNullOrEmpty(header.Value))
								HeadersPartE.Add(header);
							break;
					}
				}
			}
		}
	}
}
