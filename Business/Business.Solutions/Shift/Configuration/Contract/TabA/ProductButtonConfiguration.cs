using System;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.Shift.Configuration.Contract.TabA
{
	public class ProductButtonConfiguration
	{
		public const int DefaultFontSize = 10;

		public int? FontSize { get; private set; }
		public Color ForeColor { get; private set; }
		public Color BackgroundColor { get; private set; }

		private ProductButtonConfiguration()
		{
			ForeColor = Color.Empty;
			BackgroundColor = Color.Empty;
		}

		public static ProductButtonConfiguration FromXml(XmlNode parentNode, string editorTag = null)
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
				?.OfType<XmlAttribute>()
				.FirstOrDefault(a => String.Equals(a.Name, "FontSize",
					StringComparison.OrdinalIgnoreCase))?.Value ?? DefaultFontSize.ToString());

			configuration.ForeColor = ColorTranslator.FromHtml(configNode.Attributes
				?.OfType<XmlAttribute>()
				.FirstOrDefault(a => String.Equals(a.Name, "TextColor",
					StringComparison.OrdinalIgnoreCase))?.Value);

			var backColorValue = configNode.Attributes
				?.OfType<XmlAttribute>()
				.FirstOrDefault(a => String.Equals(a.Name, "BackgroundColor",
					StringComparison.OrdinalIgnoreCase))?.Value;
			if (!String.IsNullOrWhiteSpace(backColorValue))
				configuration.BackgroundColor = ColorTranslator.FromHtml(backColorValue);

			return configuration;
		}

		public static ProductButtonConfiguration Empty()
		{
			return new ProductButtonConfiguration();
		}
	}
}
