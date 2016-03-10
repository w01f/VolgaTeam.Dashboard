using Asa.Legacy.Media.Entities.Schedule;

namespace Asa.Legacy.Media.Entities.Section
{
	public class WeekSchedule : ProgramSchedule
	{
		public override int TotalPeriods
		{
			get
			{
				var datesRange = Parent.FlightDateEnd - Parent.FlightDateStart;
				return datesRange.HasValue ? datesRange.Value.Days / 7 + 1 : 0;
			}
		}

		public override ScheduleSection CreateSection()
		{
			return new WeeklySection(this);
		}

		public WeekSchedule(RegularSchedule parent) : base(parent) { }
	}
}
