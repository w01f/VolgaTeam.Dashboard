using Asa.Business.Media.Configuration;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Content
{
	public class WeeklyScheduleContent : ProgramScheduleContent
	{
		public override int TotalPeriods
		{
			get
			{
				var datesRange = ScheduleSettings.FlightDateEnd - ScheduleSettings.FlightDateStart;
				return datesRange?.Days / 7 + 1 ?? 0;
			}
		}

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			ApplySettingsForAll = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.UniversalToggles;
		}

		public override ScheduleSection CreateSection()
		{
			return new WeeklySection(this);
		}
	}
}
