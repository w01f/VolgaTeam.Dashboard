using System.Collections.Generic;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Dashboard.Dictionaries
{
	public class CoverLists
	{
		public List<ListDataItem> Headers { get; set; }
		public List<Quote> Quotes { get; set; }

		public CoverLists()
		{
			Headers = new List<ListDataItem>();
			Quotes = new List<Quote>();
		}

		public void Load(StorageFile dataFile)
		{
			var document = new XmlDocument();
			document.Load(dataFile.LocalPath);

			var node = document.SelectSingleNode(@"/CoverSlide");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "SlideHeader":
						Headers.Add(ListDataItem.FromXml(childNode));
						break;
					case "Quote":
						var quote = new Quote();
						foreach (XmlAttribute attribute in childNode.Attributes)
						{
							switch (attribute.Name)
							{
								case "Value":
									quote.Text = attribute.Value;
									break;
								case "Author":
									quote.Author = attribute.Value;
									break;
							}
						}
						Quotes.Add(quote);
						break;
				}
			}
		}
	}
}
