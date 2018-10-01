using System;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.Shift.Configuration.Contract.TabA
{
	public class TabSelectorConfiguration
	{
		public string ContentsTabName { get; private set; }
		public string ContentsTabDescription { get; private set; }
		public int MaxSelectedItems { get; private set; }

		public static TabSelectorConfiguration FromXml(XmlNode parentNode, string configTag)
		{
			var configuration = Empty();

			var configNode = parentNode?.SelectSingleNode(String.Format("./{0}", configTag));

			if (configNode != null)
			{
				configuration.ContentsTabName = configNode.Attributes?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "TabName", StringComparison.OrdinalIgnoreCase))?.Value;

				configuration.ContentsTabDescription = configNode.Attributes?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "TabLabel", StringComparison.OrdinalIgnoreCase))?.Value;

				configuration.MaxSelectedItems = Int32.Parse(configNode.Attributes?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "MaxButtonsSelected",StringComparison.OrdinalIgnoreCase))?.Value ?? "8");
			}

			return configuration;
		}

		public static TabSelectorConfiguration Empty()
		{
			return new TabSelectorConfiguration();
		}
	}
}
