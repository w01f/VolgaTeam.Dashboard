using System.Drawing;

namespace Asa.Common.Resources.Media
{
    public partial interface IMediaGraphicResources
    {
        Image CalendarNoDataLogo { get; }
        Image CalendarResetImage { get; }
        Image CalendarDataSourceScheduleImage { get; }
        Image CalendarDataSourceSnapshotsImage { get; }
        Image CalendarDataSourceEmptyImage { get; }
        Image CalendarDataCopyImage { get; }
        Image CalendarDataPasteImage { get; }
        Image CalendarDataCloneImage { get; }
        Image CalendarRetractableBarFavoritesImage { get; }
        Image CalendarRetractableBarStyleImage { get; }
        Image CalendarRetractableBarCommentsImage { get; }
    }
}
