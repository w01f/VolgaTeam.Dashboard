using Asa.Legacy.Media.Entities.Schedule;

namespace Asa.Legacy.Media.Entities.Section
{
	public class MonthlySection : ScheduleSection
	{
		public MonthlySection(ProgramSchedule parent)
			: base(parent)
		{
			SpotType = SpotType.Month;
		}
	}
}
