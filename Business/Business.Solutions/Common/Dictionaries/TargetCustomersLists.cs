using System.Collections.Generic;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Common.Dictionaries
{
	public class TargetCustomersLists
	{
		public List<ListDataItem> Headers { get; set; }
		public List<ListDataItem> Demos { get; set; }
		public List<ListDataItem> HHIs { get; set; }
		public List<ListDataItem> Geographies { get; set; }

		public List<ListDataItem> CombinedList { get; set; }

		public TargetCustomersLists()
		{
			Headers = new List<ListDataItem>();
			Demos = new List<ListDataItem>();
			HHIs = new List<ListDataItem>();
			Geographies = new List<ListDataItem>();
			CombinedList = new List<ListDataItem>();
		}

		public void Load(StorageFile dataFile)
		{
			var document = new XmlDocument();
			document.Load(dataFile.LocalPath);

			var node = document.SelectSingleNode(@"/TargetCustomers");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "SlideHeader":
						Headers.Add(ListDataItem.FromXml(childNode));
						break;
					case "Demo":
						Demos.Add(ListDataItem.FromXml(childNode));
						break;
					case "HHI":
						HHIs.Add(ListDataItem.FromXml(childNode));
						break;
					case "Geography":
						Geographies.Add(ListDataItem.FromXml(childNode));
						break;
				}
			}

			CombinedList.AddRange(Demos);
			CombinedList.AddRange(HHIs);
			CombinedList.AddRange(Geographies);
		}
	}
}
