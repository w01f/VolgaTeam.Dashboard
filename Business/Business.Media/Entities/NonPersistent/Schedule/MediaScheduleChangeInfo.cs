using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Online.Entities.NonPersistent;

namespace Asa.Business.Media.Entities.NonPersistent.Schedule
{
	public class MediaScheduleChangeInfo : DigitalScheduleChangeInfo
	{
		public bool KeepSpotsWhenDatesChanged { get; set; }
		public bool SpotTypeChanged { get; set; }
		public bool CalendarTypeChanged { get; set; }
		public bool ProgramScheduleChanged { get; set; }
		public bool SnapshotsChanged { get; set; }

		public override void Reset()
		{
			base.Reset();
			SpotTypeChanged = false;
			CalendarTypeChanged = false;
			ProgramScheduleChanged = false;
			SnapshotsChanged = false;
		}

		public override void Merge(BaseScheduleChangeInfo newInfo)
		{
			base.Merge(newInfo);

			var newMediaInfo = (MediaScheduleChangeInfo)newInfo;
			SpotTypeChanged |= newMediaInfo.SpotTypeChanged;
			CalendarTypeChanged |= newMediaInfo.CalendarTypeChanged;
			ProgramScheduleChanged |= newMediaInfo.ProgramScheduleChanged;
			SnapshotsChanged |= newMediaInfo.SnapshotsChanged;
		}
	}
}
