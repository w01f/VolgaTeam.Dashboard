using System;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.Shift.Configuration.Approach
{
	public class TabSelectorConfiguration
	{
		public string ContentsTabName { get; private set; }
		public string ContentsTabDescription { get; private set; }
		public int MaxSelectedTabs { get; private set; }

		public static TabSelectorConfiguration FromXml(XmlNode parentNode, string configTag)
		{
			var configuration = Empty();

			var configNode = parentNode?.SelectSingleNode(String.Format("./{0}", configTag));

			if (configNode != null)
			{
				configuration.ContentsTabName = configNode.Attributes?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "Tab0Name", StringComparison.OrdinalIgnoreCase))?.Value;

				configuration.ContentsTabDescription = configNode.Attributes?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "Tab0Label", StringComparison.OrdinalIgnoreCase))?.Value;

				configuration.MaxSelectedTabs = Int32.Parse(configNode.Attributes?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "MaxButtonsSelected",StringComparison.OrdinalIgnoreCase))?.Value ?? "4");
			}

			return configuration;
		}

		public static TabSelectorConfiguration Empty()
		{
			return new TabSelectorConfiguration();
		}
	}
}
