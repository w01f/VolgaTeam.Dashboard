using System.Xml;
using Asa.Legacy.Common.Entities.Digital;

namespace Asa.Legacy.Media.Entities.Settings
{
	public class ScheduleBuilderViewSettings
	{
		public ScheduleBuilderViewSettings()
		{
			HomeViewSettings = new HomeViewSettings();
			DigitalPackageSettings = new DigitalPackageSettings();
		}

		public HomeViewSettings HomeViewSettings { get; set; }
		public DigitalPackageSettings DigitalPackageSettings { get; private set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "HomeViewSettings":
						HomeViewSettings.Deserialize(childNode);
						break;
					case "DigitalPackageSettings":
						DigitalPackageSettings.Deserialize(childNode);
						break;
				}
			}
		}
	}
}
