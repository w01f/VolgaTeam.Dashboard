namespace Asa.Business.Media.Entities.NonPersistent.Section.Content
{
	public class WeeklyScheduleContent : ProgramScheduleContent
	{
		public override int TotalPeriods
		{
			get
			{
				var datesRange = ScheduleSettings.FlightDateEnd - ScheduleSettings.FlightDateStart;
				return datesRange.HasValue ? datesRange.Value.Days / 7 + 1 : 0;
			}
		}

		public override ScheduleSection CreateSection()
		{
			return new WeeklySection(this);
		}
	}
}
