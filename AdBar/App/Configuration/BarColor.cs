using System;
using System.Drawing;
using System.Xml.Serialization;

namespace Asa.Bar.App.Configuration
{
	[Serializable]
	public struct ColorEx
	{
		public ColorEx(Color color)
			: this()
		{
			Color = color;
		}

		[XmlIgnore]
		public Color Color { get; set; }

		[XmlAttribute]
		public string ColorHtml
		{
			get { return ColorTranslator.ToHtml(Color); }
			set { Color = ColorTranslator.FromHtml(value); }
		}

		public static implicit operator Color(ColorEx colorEx)
		{
			return colorEx.Color;
		}

		public static implicit operator ColorEx(Color color)
		{
			return new ColorEx(color);
		}
	}
}