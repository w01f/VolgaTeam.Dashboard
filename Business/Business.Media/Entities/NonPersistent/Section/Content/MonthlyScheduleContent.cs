using System;
using Asa.Business.Media.Configuration;

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

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			ApplySettingsForAll = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.UniversalToggles;
		}

		public override ScheduleSection CreateSection()
		{
			return new MonthlySection(this);
		}
	}
}
