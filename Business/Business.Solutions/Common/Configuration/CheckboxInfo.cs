using System;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.Common.Configuration
{
	public class CheckboxInfo
	{
		public string Title { get; private set; }
		public bool Value { get;private set; }
		
		public static CheckboxInfo FromXml(XmlNode node)
		{
			var checkboxInfo = Empty();
			if (node != null)
			{
				var attributes = node.Attributes?.OfType<XmlAttribute>().ToArray() ?? new XmlAttribute[] { };
				foreach (var attribute in attributes)
				{
					switch (attribute.Name)
					{
						case "Title":
							checkboxInfo.Title = attribute.Value;
							break;
						case "DefaultState":
						case "DefaultValue":
							{
								if (Boolean.TryParse(attribute.Value, out var temp))
									checkboxInfo.Value = temp;
							}
							break;
					}
				}
			}
			return checkboxInfo;
		}

		public static CheckboxInfo Empty()
		{
			return new CheckboxInfo();
		}
	}
}
