using System;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Content
{
	public class MonthlyScheduleContent : ProgramScheduleContent
	{
		public override int TotalPeriods
		{
			get
			{
				if (!ScheduleSettings.FlightDateEnd.HasValue || !ScheduleSettings.FlightDateStart.HasValue) return 0;
				return Math.Abs((ScheduleSettings.FlightDateEnd.Value.Month - ScheduleSettings.FlightDateStart.Value.Month) + 12 * 
					(ScheduleSettings.FlightDateEnd.Value.Year - ScheduleSettings.FlightDateStart.Value.Year)) + 1;
			}
		}

		public override ScheduleSection CreateSection()
		{
			return new MonthlySection(this);
		}
	}
}
