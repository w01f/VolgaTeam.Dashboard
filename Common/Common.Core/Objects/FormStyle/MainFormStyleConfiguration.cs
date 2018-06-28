using System.Drawing;
using System.Xml;

namespace Asa.Common.Core.Objects.FormStyle
{
	public class MainFormStyleConfiguration
	{
		public Color? AccentColor { get; set; }
		public Color? StatusBarTextColor { get; set; }
		public Color? ToggleSelectedColor { get; set; }
		public Color? ToggleHoverColor { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "AccentColor":
						AccentColor = ColorTranslator.FromHtml(childNode.InnerText);
						break;
					case "StatusBarTextColor":
						StatusBarTextColor = ColorTranslator.FromHtml(childNode.InnerText);
						break;
					case "ToggleSelectedColor":
						ToggleSelectedColor = ColorTranslator.FromHtml(childNode.InnerText);
						break;
					case "ToggleHoverColor":
						ToggleHoverColor = ColorTranslator.FromHtml(childNode.InnerText);
						break;
				}
			}
		}
	}
}
