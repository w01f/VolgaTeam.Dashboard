using Asa.Legacy.Media.Entities.Schedule;

namespace Asa.Legacy.Media.Entities.Section
{
	public class WeeklySection : ScheduleSection
	{
		public WeeklySection(ProgramSchedule parent)
			: base(parent)
		{
			SpotType = SpotType.Week;
		}
	}
}
