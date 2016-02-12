using System.Collections.Generic;
using System.Xml;
using Asa.Business.Dashboard.Configuration;

namespace Asa.Business.Dashboard.Dictionaries
{
	public class CoverLists
	{
		public CoverLists()
		{
			Headers = new List<string>();
			Quotes = new List<Quote>();
			Load();
		}

		public List<string> Headers { get; set; }
		public List<Quote> Quotes { get; set; }

		private void Load()
		{
			var document = new XmlDocument();
			document.Load(ResourceManager.Instance.DataCoverFile.LocalPath);

			var node = document.SelectSingleNode(@"/CoverSlide");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "SlideHeader":
						foreach (XmlAttribute attribute in childNode.Attributes)
						{
							switch (attribute.Name)
							{
								case "Value":
									if (!string.IsNullOrEmpty(attribute.Value))
										Headers.Add(attribute.Value);
									break;
							}
						}
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
