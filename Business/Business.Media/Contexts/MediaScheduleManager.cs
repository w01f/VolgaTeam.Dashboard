using Asa.Business.Common.Contexts;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.Persistent;

namespace Asa.Business.Media.Contexts
{
	public class MediaScheduleManager : ScheduleManager<MediaSchedule, MediaScheduleSettings, MediaContext, MediaContext>
	{
		public override MediaContext SchedulesContainer => Context;
	}
}
