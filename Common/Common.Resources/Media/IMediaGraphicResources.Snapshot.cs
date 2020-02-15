using System.Drawing;

namespace Asa.Common.Resources.Media
{
    public partial interface IMediaGraphicResources
    {
        Image SnapshotsRibbonLogo { get; }
        Image SnapshotsNoRecordsLogo { get; }
        Image SnapshotsNoProgramsLogo { get; }
        Image SnapshotsNoDigitalItemsLogo { get; }
        Image SnapshotsNewPopupLogo { get; }
        Image SnapshotsRetractableBarColumnsImage { get; }
        Image SnapshotsRetractableBarDigitalImage { get; }
        Image SnapshotsRetractableBarSummaryImage { get; }
        Image SnapshotsRetractableBarActiveWeeksImage { get; }
        Image SnapshotsRetractableBarColorsImage { get; }
    }
}
