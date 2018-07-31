using System;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.Common.Configuration
{
	public class TextEditorConfiguration
	{
		public const int DefaultFontSize = 10;

		public int? FontSize { get; private set; }
		public Color ForeColor { get; private set; }
		public Color BackColor { get; private set; }

		private TextEditorConfiguration()
		{
			ForeColor = Color.Empty;
			BackColor = Color.Empty;
		}

		public static TextEditorConfiguration FromXml(XmlNode parentNode)
		{
			var configuration = Empty();

			var configNode = parentNode?.SelectSingleNode("./ControlStyle");
			if (configNode?.Attributes == null)
				return configuration;

			configuration.FontSize = Int32.Parse(configNode.Attributes
				.OfType<XmlAttribute>()
				.FirstOrDefault(a => String.Equals(a.Name, "FontSize",
					StringComparison.OrdinalIgnoreCase))?.Value ?? "10");

			configuration.ForeColor = ColorTranslator.FromHtml(configNode.Attributes
				.OfType<XmlAttribute>()
				.FirstOrDefault(a => String.Equals(a.Name, "TextColor",
					StringComparison.OrdinalIgnoreCase))?.Value);

			configuration.BackColor = ColorTranslator.FromHtml(configNode.Attributes
				.OfType<XmlAttribute>()
				.FirstOrDefault(a => String.Equals(a.Name, "BackgroundColor",
					StringComparison.OrdinalIgnoreCase))?.Value);

			return configuration;
		}

		public static TextEditorConfiguration Empty()
		{
			return new TextEditorConfiguration();
		}
	}
}
