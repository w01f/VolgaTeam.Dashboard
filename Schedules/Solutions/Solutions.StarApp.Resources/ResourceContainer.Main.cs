using System.Drawing;
using Asa.Common.Resources.Solutions.StarApp;

namespace Asa.Solutions.StarApp.Resources
{
	public partial class ResourceContainer : IStarAppGraphicResources
	{
		public Image MainHeader => Resources.Main.Resource.Header;
		public Image MainSplash => Resources.Main.Resource.Logo;
		public Image MainBackground => Resources.Main.Resource.Background;
	}
}
