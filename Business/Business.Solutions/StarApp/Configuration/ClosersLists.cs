using System;
using System.Collections.Generic;
using System.Xml;
using Asa.Business.Solutions.StarApp.Configuration;

namespace Asa.Business.Solutions.StarApp.Dictionaries
{
	public class ClosersLists
	{
		public List<ListDataItem> HeadersPartA { get; set; }
		public List<ListDataItem> HeadersPartB { get; set; }
		public List<ListDataItem> HeadersPartC { get; set; }

		public ClosersLists()
		{
			HeadersPartA = new List<ListDataItem>();
			HeadersPartB = new List<ListDataItem>();
			HeadersPartC = new List<ListDataItem>();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataClosersPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataClosersPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/Closers");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "SlideHeader":
							var header = ListDataItem.FromXml(childNode);
							if (!String.IsNullOrEmpty(header.Value))
								HeadersPartA.Add(header);
							break;
					}
				}
			}

			if (resourceManager.DataClosersPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataClosersPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/Closers");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "SlideHeader":
							var header = ListDataItem.FromXml(childNode);
							if (!String.IsNullOrEmpty(header.Value))
								HeadersPartB.Add(header);
							break;
					}
				}
			}

			if (resourceManager.DataClosersPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataClosersPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/Closers");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "SlideHeader":
							var header = ListDataItem.FromXml(childNode);
							if (!String.IsNullOrEmpty(header.Value))
								HeadersPartC.Add(header);
							break;
					}
				}
			}
		}
	}
}
