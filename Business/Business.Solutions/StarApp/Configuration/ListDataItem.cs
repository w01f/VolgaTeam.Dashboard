using System;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class ListDataItem
	{
		public string Value { get; set; }
		public bool IsDefault { get; set; }

		public ListDataItem()
		{
			Value = string.Empty;
			IsDefault = false;
		}

		public override string ToString()
		{
			return Value;
		}

		public static ListDataItem FromXml(XmlNode node)
		{
			var listDataItem = new ListDataItem();
			foreach (XmlAttribute attribute in node.Attributes)
			{
				switch (attribute.Name)
				{
					case "Value":
						listDataItem.Value = attribute.Value;
						break;
					case "IsDefault":
						if (Boolean.TryParse(attribute.Value, out var temp))
							listDataItem.IsDefault = temp;
						break;
				}
			}
			return listDataItem;
		}
	}
}
