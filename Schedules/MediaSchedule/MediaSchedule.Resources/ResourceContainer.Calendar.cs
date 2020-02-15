using System.Drawing;

namespace Asa.Media.Resources
{
    public partial class ResourceContainer
    {
        public Image CalendarNoDataLogo => Controls.Calendar.Resource.DefaultCalendar;
        public Image CalendarResetImage => Ribbon.Calendar.Resource.Reset;
        public Image CalendarDataSourceScheduleImage => Ribbon.Calendar.Resource.ImportFromSchedule;
        public Image CalendarDataSourceSnapshotsImage => Ribbon.Calendar.Resource.ImportFromSnapshots;
        public Image CalendarDataSourceEmptyImage => Ribbon.Calendar.Resource.Custom;
        public Image CalendarDataCopyImage => Ribbon.Calendar.Resource.Copy;
        public Image CalendarDataPasteImage => Ribbon.Calendar.Resource.Paste;
        public Image CalendarDataCloneImage => Ribbon.Calendar.Resource.Clone;
        public Image CalendarRetractableBarFavoritesImage => Controls.Calendar.Resource.RetractableBarGallery;
        public Image CalendarRetractableBarStyleImage => Controls.Calendar.Resource.RetractableBarStyle;
        public Image CalendarRetractableBarCommentsImage => Controls.Calendar.Resource.RetractableBarComments;
    }
}
