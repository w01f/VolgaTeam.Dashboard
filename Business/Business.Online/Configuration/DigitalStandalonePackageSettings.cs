using System.Xml;
using Asa.Business.Online.Dictionaries;

namespace Asa.Business.Online.Configuration
{
	class DigitalStandalonePackageSettings : DigitalPackageSettings
	{
		public override void ResetToDefault()
		{
			var defaultSettings = new XmlDocument();
			defaultSettings.LoadXml(@"<DefaultSettings>" + ListManager.Instance.DefaultDigitalStandalonePackageSettings.Serialize() + @"</DefaultSettings>");
			Deserialize(defaultSettings.SelectSingleNode(@"/DefaultSettings"));
	}
	}
}
