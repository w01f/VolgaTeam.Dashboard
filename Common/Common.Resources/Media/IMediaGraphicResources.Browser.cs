using System.Drawing;

namespace Asa.Common.Resources.Media
{
    public partial interface IMediaGraphicResources
    {
        Image BrowserNavigationBack { get; }
        Image BrowserNavigationForward { get; }
        Image BrowserNavigationRefresh { get; }
        Image BrowserExternalChrome { get; }
        Image BrowserExternalFirefox { get; }
        Image BrowserExternalIE { get; }
        Image BrowserExternalEdge { get; }
        Image BrowserPowerPointAddSlide { get; }
        Image BrowserPowerPointAddSlides { get; }
        Image BrowserPowerPointPrint { get; }
        Image BrowserVideoAdd { get; }
        Image BrowserYoutubeAdd { get; }
        Image BrowserUrlCopy { get; }
        Image BrowserUrlEmail { get; }
    }
}
