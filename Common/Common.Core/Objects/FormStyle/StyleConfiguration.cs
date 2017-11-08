using System.Drawing;
using System.Xml;

namespace Asa.Common.Core.Objects.FormStyle
{
	public class StyleConfiguration
	{
		public Color? AccentColor { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "AccentColor":
						AccentColor = ColorTranslator.FromHtml(node.InnerText);
						break;
				}
			}
		}
	}
}
