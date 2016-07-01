using System.Xml;
using Asa.Business.Online.Dictionaries;

namespace Asa.Business.Online.Configuration
{
	public class DigitalProductPackageSettings : DigitalPackageSettings
	{
		public override void ResetToDefault()
		{
			var defaultSettings = new XmlDocument();
			defaultSettings.LoadXml(@"<DefaultSettings>" + ListManager.Instance.DefaultDigitalProductPackageSettings.Serialize() + @"</DefaultSettings>");
			Deserialize(defaultSettings.SelectSingleNode(@"/DefaultSettings"));
		}
	}
}
