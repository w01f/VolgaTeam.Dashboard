using System.Drawing;

namespace Asa.Media.Resources
{
    public partial class ResourceContainer
    {
        public Image ProgramScheduleRibbonLogo => Ribbon.Schedule.Resource.New;
        public Image ProgramScheduleNoRecordsLogo => Controls.Schedule.Resource.DefaultSchedule;
        public Image ProgramScheduleNoProgramsLogo => Controls.Schedule.Resource.DefaultTVRadio;
        public Image ProgramScheduleNoDigitalItemsLogo => Controls.Schedule.Resource.DefaultDigital;
        public Image ProgramScheduleNewPopupLogo => Controls.Schedule.Resource.PopupNewSchedule;
        public Image ProgramScheduleRetractableBarColumnsImage => Controls.Schedule.Resource.RetractableBarTVRadioColumns;
        public Image ProgramScheduleRetractableBarTotalsImage => Controls.Schedule.Resource.RetractableBarTVRadioTotals;
        public Image ProgramScheduleRetractableBarDigitalImage => Controls.Schedule.Resource.RetractableBarDigitalInfo;
        public Image ProgramScheduleRetractableBarSummaryImage => Controls.Schedule.Resource.RetractableBarSummaryInfo;
        public Image ProgramScheduleRetractableBarColorsImage => Controls.Schedule.Resource.RetractableBarColors;
    }
}
