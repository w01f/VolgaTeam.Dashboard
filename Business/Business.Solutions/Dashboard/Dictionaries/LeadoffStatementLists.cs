using System.Collections.Generic;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Dashboard.Dictionaries
{
	public class LeadoffStatementLists
	{
		public List<ListDataItem> Headers { get; set; }
		public List<ListDataItem> Statements { get; set; }

		public LeadoffStatementLists()
		{
			Headers = new List<ListDataItem>();
			Statements = new List<ListDataItem>();
		}

		public void Load(StorageFile dataFile)
		{
			var document = new XmlDocument();
			document.Load(dataFile.LocalPath);

			var node = document.SelectSingleNode(@"/LeadOff");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "SlideHeader":
						Headers.Add(ListDataItem.FromXml(childNode));
						break;
					case "Statement":
						Statements.Add(ListDataItem.FromXml(childNode));
						break;
				}
			}
		}
	}
}
