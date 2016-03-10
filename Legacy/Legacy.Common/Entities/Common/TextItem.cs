using System;
using System.Xml;

namespace Asa.Legacy.Common.Entities.Common
{
	public class TextItem : ITextItem
	{
		public string Text { get; private set; }
		public bool IsBold { get; private set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Text":
						Text = childNode.InnerText;
						break;
					case "IsBold":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								IsBold = temp;
						}
						break;
				}
			}
		}
	}
}
