using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Solutions.Dashboard.Entities.NonPersistent;

namespace Asa.Business.Media.Entities.NonPersistent.Solutions
{
	public class MediaDashboardContent : DashboardContent
	{
		public override BaseScheduleSettings ScheduleSettings => ((MediaDashboardSolution)Parent).Schedule.Settings;
	}
}
