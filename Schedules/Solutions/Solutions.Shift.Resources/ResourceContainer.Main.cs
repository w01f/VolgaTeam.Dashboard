using System.Drawing;
using Asa.Common.Resources.Solutions.Shift;

namespace Asa.Solutions.Shift.Resources
{
	public partial class ResourceContainer : IShiftGraphicResources
	{
		public Image MainHeader => Resources.Main.Resource.Header;
		public Image MainSplash => Resources.Main.Resource.Splash;
		public Image MainBackgroound => Resources.Main.Resource.Background;
	}
}
