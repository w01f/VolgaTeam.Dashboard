using System.Drawing;

namespace Asa.Common.Resources.Media
{
    public partial interface IMediaGraphicResources
    {
        Image OptionsRibbonLogo { get; }
        Image OptionsNoRecordsLogo { get; }
        Image OptionsNoProgramsLogo { get; }
        Image OptionsNoDigitalItemsLogo { get; }
        Image OptionsNewPopupLogo { get; }
        Image OptionsRetractableBarColumnsImage { get; }
        Image OptionsRetractableBarDigitalImage { get; }
        Image OptionsRetractableBarSummaryImage { get; }
        Image OptionsRetractableBarColorsImage { get; }
    }
}
