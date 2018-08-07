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
		public Color DropdownForeColor { get; private set; }
		public Color BackColor { get; private set; }

		private TextEditorConfiguration()
		{
			ForeColor = Color.Empty;
			DropdownForeColor = Color.Empty;
			BackColor = Color.Empty;
		}

		public static TextEditorConfiguration FromXml(XmlNode parentNode, string editorTag = null)
		{
			var configuration = Empty();

			var configNodes = (parentNode?.SelectNodes("./ControlStyle")?.OfType<XmlNode>() ?? new XmlNode[] { })
				.Where(node => node.Attributes != null)
				.ToList();

			XmlNode configNode = null;
			if (!String.IsNullOrEmpty(editorTag))
			{
				configNode = configNodes.FirstOrDefault(node => node.Attributes.OfType<XmlAttribute>().Any(a =>
					String.Equals(a.Name, "Control", StringComparison.OrdinalIgnoreCase) &&
					String.Equals(a.Value, editorTag, StringComparison.OrdinalIgnoreCase)));
			}
			if (configNode == null)
				configNode = configNodes.FirstOrDefault(node => !node.Attributes.OfType<XmlAttribute>().Any(a =>
					String.Equals(a.Name, "Control", StringComparison.OrdinalIgnoreCase)));

			if (configNode == null)
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

			configuration.DropdownForeColor = ColorTranslator.FromHtml(configNode.Attributes
				.OfType<XmlAttribute>()
				.FirstOrDefault(a => String.Equals(a.Name, "DropdownColor",
					StringComparison.OrdinalIgnoreCase))?.Value);

			return configuration;
		}

		public static TextEditorConfiguration Empty()
		{
			return new TextEditorConfiguration();
		}
	}
}
