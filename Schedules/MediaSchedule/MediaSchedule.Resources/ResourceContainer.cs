using System.Drawing;
using Asa.Common.Resources.Media;
using Asa.Common.Resources.Solutions;

namespace Asa.Media.Resources
{
    public partial class ResourceContainer : IMediaGraphicResources, ISolutionsResourceManager
    {
        public Icon MainAppIcon => Common.Resource.FormIcon;
        public Image MainAppRibbonLogo => Common.Resource.AppLogo;
        public Image FloaterLogo => Common.Resource.BrandingImage;
        public Image ToggleSwitchSkinElement => Common.Resource.ToggleSwitch;
    }
}
