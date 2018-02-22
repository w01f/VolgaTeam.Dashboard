using System.Collections.Generic;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class ClosersConfiguration
	{
		public List<ListDataItem> HeadersPartAItems { get; set; }
		public List<ListDataItem> HeadersPartBItems { get; set; }
		public List<ListDataItem> HeadersPartCItems { get; set; }

		public ClosersConfiguration()
		{
			HeadersPartAItems = new List<ListDataItem>();
			HeadersPartBItems = new List<ListDataItem>();
			HeadersPartCItems = new List<ListDataItem>();
		}

		public void Load(ResourceManager resourceManager)
		{
			//if (resourceManager.DataClosersPartAFile.ExistsLocal())
			//{
			//	var document = new XmlDocument();
			//	document.Load(resourceManager.DataClosersPartAFile.LocalPath);

			//	var node = document.SelectSingleNode(@"/Closers");
			//	if (node == null) return;
			//	foreach (XmlNode childNode in node.ChildNodes)
			//	{
			//		switch (childNode.Name)
			//		{
			//			case "SlideHeader":
			//				var header = ListDataItem.FromXml(childNode);
			//				if (!String.IsNullOrEmpty(header.Value))
			//					HeadersPartAItems.Add(header);
			//				break;
			//		}
			//	}
			//}

			//if (resourceManager.DataClosersPartBFile.ExistsLocal())
			//{
			//	var document = new XmlDocument();
			//	document.Load(resourceManager.DataClosersPartBFile.LocalPath);

			//	var node = document.SelectSingleNode(@"/Closers");
			//	if (node == null) return;
			//	foreach (XmlNode childNode in node.ChildNodes)
			//	{
			//		switch (childNode.Name)
			//		{
			//			case "SlideHeader":
			//				var header = ListDataItem.FromXml(childNode);
			//				if (!String.IsNullOrEmpty(header.Value))
			//					HeadersPartBItems.Add(header);
			//				break;
			//		}
			//	}
			//}

			//if (resourceManager.DataClosersPartCFile.ExistsLocal())
			//{
			//	var document = new XmlDocument();
			//	document.Load(resourceManager.DataClosersPartCFile.LocalPath);

			//	var node = document.SelectSingleNode(@"/Closers");
			//	if (node == null) return;
			//	foreach (XmlNode childNode in node.ChildNodes)
			//	{
			//		switch (childNode.Name)
			//		{
			//			case "SlideHeader":
			//				var header = ListDataItem.FromXml(childNode);
			//				if (!String.IsNullOrEmpty(header.Value))
			//					HeadersPartCItems.Add(header);
			//				break;
			//		}
			//	}
			//}
		}
	}
}
