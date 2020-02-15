using System.Drawing;

namespace Asa.Common.Resources.Media
{
    public partial interface IMediaGraphicResources
    {
        Image ProgramScheduleRibbonLogo { get; }
        Image ProgramScheduleNoRecordsLogo { get; }
        Image ProgramScheduleNoProgramsLogo { get; }
        Image ProgramScheduleNoDigitalItemsLogo { get; }
        Image ProgramScheduleNewPopupLogo { get; }
        Image ProgramScheduleRetractableBarColumnsImage { get; }
        Image ProgramScheduleRetractableBarTotalsImage { get; }
        Image ProgramScheduleRetractableBarDigitalImage { get; }
        Image ProgramScheduleRetractableBarSummaryImage { get; }
        Image ProgramScheduleRetractableBarColorsImage { get; }
    }
}
