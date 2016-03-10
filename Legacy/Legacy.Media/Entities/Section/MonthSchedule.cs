using System;
using Asa.Legacy.Media.Entities.Schedule;

namespace Asa.Legacy.Media.Entities.Section
{
	public class MonthSchedule : ProgramSchedule
	{
		public override int TotalPeriods
		{
			get
			{
				if (!Parent.FlightDateEnd.HasValue || !Parent.FlightDateStart.HasValue) return 0;
				return Math.Abs((Parent.FlightDateEnd.Value.Month - Parent.FlightDateStart.Value.Month) + 12 * (Parent.FlightDateEnd.Value.Year - Parent.FlightDateStart.Value.Year)) + 1;
			}
		}

		public MonthSchedule(RegularSchedule parent) : base(parent) { }

		public override ScheduleSection CreateSection()
		{
			return new MonthlySection(this);
		}
	}
}
