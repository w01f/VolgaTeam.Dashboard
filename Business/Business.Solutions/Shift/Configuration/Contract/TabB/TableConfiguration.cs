using System;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.Shift.Configuration.Contract.TabB
{
	public class TableConfiguration
	{
		public string HeaderName { get; private set; }

		public string Column1Name { get; private set; }
		public string Column2Name { get; private set; }
		public string Column3Name { get; private set; }

		public static TableConfiguration FromXml(XmlNode parentNode, string configTag)
		{
			var configuration = Empty();

			var configNode = parentNode?.SelectSingleNode(String.Format("./{0}", configTag));

			if (configNode != null)
			{
				configuration.HeaderName = configNode.Attributes?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "Value", StringComparison.OrdinalIgnoreCase))?.Value;

				configuration.Column1Name = configNode.Attributes?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "Label1", StringComparison.OrdinalIgnoreCase))?.Value;
				configuration.Column2Name = configNode.Attributes?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "Label2", StringComparison.OrdinalIgnoreCase))?.Value;
				configuration.Column3Name = configNode.Attributes?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "Label3", StringComparison.OrdinalIgnoreCase))?.Value;
			}

			return configuration;
		}

		public static TableConfiguration Empty()
		{
			return new TableConfiguration();
		}
	}
}
