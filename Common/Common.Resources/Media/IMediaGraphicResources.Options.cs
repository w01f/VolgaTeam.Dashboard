using System.Drawing;

namespace Asa.Common.Resources.Media
{
    public partial interface IMediaGraphicResources
    {
        Icon MainAppIcon { get; }
        Image MainAppRibbonLogo { get; }
        Image FloaterLogo { get; }
        Image ToggleSwitchSkinElement { get; }
    }
}
