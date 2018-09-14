using System.Drawing;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Cleanslate
{
	public class CleanslateTabInfo : StarTopTabInfo
	{
		public override StarTopTabType TabType => StarTopTabType.Cleanslate;

		public Image HeaderLogo => _resourceManager.GraphicResources?.MainHeader;
		public Image SplashLogo => _resourceManager.GraphicResources?.MainSplash;
		public Image BackgroundLogo => _resourceManager.GraphicResources?.MainBackground;
	}
}