using System;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class ComboboxItem
	{
		public string Value { get; set; }
		public bool IsDefault { get; set; }

		public ComboboxItem()
		{
			Value = String.Empty;
			IsDefault = false;
		}

		public override string ToString()
		{
			return Value;
		}

		public static ComboboxItem FromXml(XmlNode node)
		{
			var comboboxItem = new ComboboxItem();
			foreach (XmlAttribute attribute in node.Attributes)
			{
				switch (attribute.Name)
				{
					case "Value":
						comboboxItem.Value = attribute.Value;
						break;
					case "IsDefault":
						bool temp;
						if (Boolean.TryParse(attribute.Value, out temp))
							comboboxItem.IsDefault = temp;
						break;
				}
			}
			return comboboxItem;
		}
	}
}
