using System;
using System.Collections.Generic;
using System.Xml;

namespace Asa.Legacy.Common.Entities.Digital
{
	public class ProductInfo
	{
		public ProductInfoType Type { get; set; }
		public bool Selected { get; set; }
		public string Group { get; set; }
		public List<string> Phrases { get; private set; }
		public string UserValue { get; set; }

		public ProductInfo()
		{
			Phrases = new List<string>();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "Type":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								Type = (ProductInfoType)temp;
						}
						break;
					case "Selected":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								Selected = temp;
						}
						break;
					case "Group":
						Group = childNode.InnerText;
						break;
					case "Phrase":
						Phrases.Add(childNode.InnerText);
						break;
					case "UserValue":
						UserValue = childNode.InnerText;
						break;
				}
		}
	}
}
