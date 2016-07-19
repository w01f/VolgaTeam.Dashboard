using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.Persistent;

namespace Asa.Business.Media.Entities.NonPersistent.Common
{
	public abstract class MediaSolutionContent : BaseScheduleSolutionContent<MediaSchedule, MediaScheduleSettings>
	{
		public override MediaSchedule Schedule => ((MediaPartition)Parent).Schedule;
	}
}
