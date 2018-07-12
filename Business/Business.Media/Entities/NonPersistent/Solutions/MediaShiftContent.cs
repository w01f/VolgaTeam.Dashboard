using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Entities.NonPersistent.ScheduleResources;
using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Solutions.Shift.Entities.NonPersistent;

namespace Asa.Business.Media.Entities.NonPersistent.Solutions
{
	public class MediaShiftContent : ShiftContent
	{
		public override BaseScheduleSettings ScheduleSettings => ((MediaShiftSolution)Parent).Schedule.Settings;
		public override BaseScheduleResourceContainer ScheduleResources => ((MediaShiftSolution)Parent).Schedule.ResourceContainer;
	}
}