using System.Drawing;

namespace Asa.Media.Resources
{
    public partial class ResourceContainer
    {
        public Image SnapshotsRibbonLogo => Ribbon.Snapshots.Resource.New;
        public Image SnapshotsNoRecordsLogo => Controls.Snapshots.Resource.DefaultSnapshot;
        public Image SnapshotsNoProgramsLogo => Controls.Snapshots.Resource.DefaultProgram;
        public Image SnapshotsNoDigitalItemsLogo => Controls.Snapshots.Resource.DefaultDigital;
        public Image SnapshotsNewPopupLogo => Controls.Snapshots.Resource.PopupSnapshotNew;
        public Image SnapshotsRetractableBarColumnsImage => Controls.Snapshots.Resource.RetractableBarColumns;
        public Image SnapshotsRetractableBarDigitalImage => Controls.Snapshots.Resource.RetractableBarDigitalInfo;
        public Image SnapshotsRetractableBarSummaryImage => Controls.Snapshots.Resource.RetractableBarSummaryInfo;
        public Image SnapshotsRetractableBarActiveWeeksImage => Controls.Snapshots.Resource.RetractableBarActiveWeeks;
        public Image SnapshotsRetractableBarColorsImage => Controls.Snapshots.Resource.RetractableBarColors;
    }
}
