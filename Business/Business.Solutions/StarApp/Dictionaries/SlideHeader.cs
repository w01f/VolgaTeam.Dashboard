using System;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Dictionaries
{
	public class SlideHeader
	{
		public SlideHeader()
		{
			Value = string.Empty;
			IsDefault = false;
		}

		public string Value { get; set; }
		public bool IsDefault { get; set; }

		public override string ToString()
		{
			return Value;
		}

		public static SlideHeader FromXml(XmlNode node)
		{
			var header = new SlideHeader();
			foreach (XmlAttribute attribute in node.Attributes)
			{
				switch (attribute.Name)
				{
					case "Value":
						header.Value = attribute.Value;
						break;
					case "IsDefault":
						bool temp;
						if (Boolean.TryParse(attribute.Value, out temp))
							header.IsDefault = temp;
						break;
				}
			}
			return header;
		}
	}
}
