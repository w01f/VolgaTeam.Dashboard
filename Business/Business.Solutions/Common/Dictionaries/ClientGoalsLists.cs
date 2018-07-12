using System.Collections.Generic;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Common.Dictionaries
{
	public class ClientGoalsLists
	{
		public List<ListDataItem> Headers { get; set; }
		public List<ListDataItem> Goals { get; set; }

		public ClientGoalsLists()
		{
			Headers = new List<ListDataItem>();
			Goals = new List<ListDataItem>();
		}

		public void Load(StorageFile dataFile)
		{
			var document = new XmlDocument();
			document.Load(dataFile.LocalPath);

			var node = document.SelectSingleNode(@"/ClientGoals");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "SlideHeader":
						Headers.Add(ListDataItem.FromXml(childNode));
						break;
					case "Goal":
						Goals.Add(ListDataItem.FromXml(childNode));
						break;
				}
			}
		}
	}
}
