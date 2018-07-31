using DevExpress.XtraEditors.Controls;
using System;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.Common.Configuration
{
	public class ClipartConfiguration
	{
		public ContentAlignment Alignment { get; private set; }
		public PictureSizeMode SizeMode { get; private set; }

		public ClipartConfiguration()
		{
			Alignment = ContentAlignment.MiddleCenter;
			SizeMode = PictureSizeMode.Squeeze;
		}

		public static ClipartConfiguration FromXml(XmlNode containerNode, string clipartTag)
		{
			var configuration = new ClipartConfiguration();
			var horizontalAlignmentValue = containerNode
				.SelectNodes(@"ClipartAlignment").OfType<XmlNode>().FirstOrDefault(n => String.Equals(n.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "ClipartTag"))?.Value, clipartTag, StringComparison.OrdinalIgnoreCase))
				?.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Horizontal"))?.Value?.ToLower();
			var verticalAlignmentValue = containerNode
				.SelectNodes(@"ClipartAlignment").OfType<XmlNode>().FirstOrDefault(n => String.Equals(n.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "ClipartTag"))?.Value, clipartTag, StringComparison.OrdinalIgnoreCase))
				?.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Vertical"))?.Value?.ToLower();
			switch (horizontalAlignmentValue)
			{
				case "left":
					switch (verticalAlignmentValue)
					{
						case "top":
							configuration.Alignment = ContentAlignment.TopLeft;
							break;
						case "middle":
							configuration.Alignment = ContentAlignment.MiddleLeft;
							break;
						case "bottom":
							configuration.Alignment = ContentAlignment.BottomLeft;
							break;
					}
					break;
				case "middle":
					switch (verticalAlignmentValue)
					{
						case "top":
							configuration.Alignment = ContentAlignment.TopCenter;
							break;
						case "middle":
							configuration.Alignment = ContentAlignment.MiddleCenter;
							break;
						case "bottom":
							configuration.Alignment = ContentAlignment.BottomCenter;
							break;
					}
					break;
				case "right":
					switch (verticalAlignmentValue)
					{
						case "top":
							configuration.Alignment = ContentAlignment.TopRight;
							break;
						case "middle":
							configuration.Alignment = ContentAlignment.MiddleRight;
							break;
						case "bottom":
							configuration.Alignment = ContentAlignment.BottomRight;
							break;
					}
					break;
			}

			var sizeModeValue = containerNode
				.SelectNodes(@"ClipartAlignment").OfType<XmlNode>().FirstOrDefault(n => String.Equals(n.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "ClipartTag"))?.Value, clipartTag, StringComparison.OrdinalIgnoreCase))
				?.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "SizeMode"))?.Value?.ToLower();
			switch (sizeModeValue)
			{
				case "stretch":
					configuration.SizeMode = PictureSizeMode.Stretch;
					break;
				default:
					configuration.SizeMode = PictureSizeMode.Squeeze;
					break;
			}
			return configuration;
		}
	}
}
