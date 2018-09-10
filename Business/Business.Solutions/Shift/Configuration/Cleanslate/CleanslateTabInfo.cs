using System.Drawing;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Cleanslate
{
	public class CleanslateTabInfo : ShiftTopTabInfo
	{
		public Image HeaderLogo => _resourceManager.GraphicResources?.MainHeader;
		public Image SplashLogo => _resourceManager.GraphicResources?.MainSplash;
		public Image BackgroundLogo => _resourceManager.GraphicResources?.MainBackgroound;

		public CleanslateTabInfo() : base(ShiftTopTabType.Cleanslate) { }
	}
}
