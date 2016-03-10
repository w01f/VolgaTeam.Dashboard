using System.Collections.Generic;
using System.Xml;

namespace Asa.Legacy.Common.Entities.Common
{
	public class TextGroup : ITextItem
	{
		public string Separator { get; private set; }
		public string BorderLeft { get; private set; }
		public string BorderRight { get; private set; }
		public List<ITextItem> Items { get; private set; }

		public TextGroup()
		{
			Items = new List<ITextItem>();
			Separator = " ";
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Separator":
						Separator = childNode.InnerText;
						break;
					case "BorderLeft":
						BorderLeft = childNode.InnerText;
						break;
					case "BorderRight":
						BorderRight = childNode.InnerText;
						break;
					case "TextItem":
						{
							var textItem = new TextItem();
							textItem.Deserialize(childNode);
							Items.Add(textItem);
						}
						break;
					case "TextGroup":
						{
							var textItem = new TextGroup();
							textItem.Deserialize(childNode);
							Items.Add(textItem);
						}
						break;
				}
			}
		}
	}
}
