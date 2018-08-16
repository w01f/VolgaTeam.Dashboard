using System;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.Shift.Configuration.NeedsSolutions
{
	public class SolutionButtonConfiguration
	{
		public const int DefaultFontSize = 10;

		public int? TopFontSize { get; private set; }
		public int? BottomFontSize { get; private set; }
		public Color TopForeColor { get; private set; }
		public Color BottomForeColor { get; private set; }

		private SolutionButtonConfiguration()
		{
			TopForeColor = Color.Empty;
			BottomForeColor = Color.Empty;
		}

		public static SolutionButtonConfiguration FromXml(XmlNode parentNode, string editorTag = null)
		{
			var configuration = Empty();

			var configNodes = (parentNode?.SelectNodes("./ButtonStyle")?.OfType<XmlNode>() ?? new XmlNode[] { })
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

			configuration.TopFontSize = Int32.Parse(configNode.Attributes
				?.OfType<XmlAttribute>()
				.FirstOrDefault(a => String.Equals(a.Name, "TopFontSize",
					StringComparison.OrdinalIgnoreCase))?.Value ?? DefaultFontSize.ToString());

			configuration.BottomFontSize = Int32.Parse(configNode.Attributes
				?.OfType<XmlAttribute>()
				.FirstOrDefault(a => String.Equals(a.Name, "BottomFontSize",
					StringComparison.OrdinalIgnoreCase))?.Value ?? DefaultFontSize.ToString());

			configuration.TopForeColor = ColorTranslator.FromHtml(configNode.Attributes
				?.OfType<XmlAttribute>()
				.FirstOrDefault(a => String.Equals(a.Name, "TopTextColor",
					StringComparison.OrdinalIgnoreCase))?.Value);

			configuration.BottomForeColor = ColorTranslator.FromHtml(configNode.Attributes
				?.OfType<XmlAttribute>()
				.FirstOrDefault(a => String.Equals(a.Name, "BottomTextColor",
					StringComparison.OrdinalIgnoreCase))?.Value);

			return configuration;
		}

		public static SolutionButtonConfiguration Empty()
		{
			return new SolutionButtonConfiguration();
		}
	}
}
