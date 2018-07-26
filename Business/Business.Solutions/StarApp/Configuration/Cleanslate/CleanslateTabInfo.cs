using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Cleanslate
{
	public class CleanslateTabInfo : StarTopTabInfo
	{
		public override StarTopTabType TabType => StarTopTabType.Cleanslate;

		public Image HeaderLogo { get; private set; }
		public Image SplashLogo { get; private set; }

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			HeaderLogo = resourceManager.LogoCleanslateHeaderFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoCleanslateHeaderFile.LocalPath)
				: null;
			SplashLogo = resourceManager.LogoCleanslateSplashFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoCleanslateSplashFile.LocalPath)
				: null;
		}
	}
}