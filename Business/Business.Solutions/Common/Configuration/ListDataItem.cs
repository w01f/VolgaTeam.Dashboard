using System;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.Common.Configuration
{
	public class ListDataItem
	{
		public string Value { get; set; }
		public bool IsDefault { get; set; }
		public bool IsPlaceholder { get; set; }

		public override string ToString()
		{
			return Value;
		}

		public static ListDataItem FromXml(XmlNode node)
		{
			var listDataItem = new ListDataItem();
			if (node != null)
			{
				var attributes = node.Attributes?.OfType<XmlAttribute>().ToArray() ?? new XmlAttribute[] { };
				foreach (var attribute in attributes)
				{
					switch (attribute.Name)
					{
						case "Value":
							listDataItem.Value = attribute.Value;
							break;
						case "IsDefault":
							{
								if (Boolean.TryParse(attribute.Value, out var temp))
									listDataItem.IsDefault = temp;
							}
							break;
						case "IsPlaceholder":
							{
								if (Boolean.TryParse(attribute.Value, out var temp))
									listDataItem.IsPlaceholder = temp;
							}
							break;
					}
				}
			}
			return listDataItem;
		}

		public static ListDataItem FromString(string value)
		{
			var listDataItem = new ListDataItem();
			listDataItem.Value = value;
			return listDataItem;
		}

		public static ListDataItem Clone(ListDataItem source)
		{
			var listDataItem = new ListDataItem();
			listDataItem.Value = source.Value;
			listDataItem.IsDefault = source.IsDefault;
			listDataItem.IsPlaceholder = source.IsPlaceholder;
			return listDataItem;
		}
	}
}
