using System.Collections.Generic;
using System.Linq;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Calendar.Interfaces;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.Persistent;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public abstract class MediaCalendarSection : CalendarSection
	{
		public MediaSchedule MediaSchedule => ((MediaCalendar)Parent).Schedule;
		public MediaScheduleSettings MediaScheduleSettings => MediaSchedule.Settings;

		[JsonConstructor]
		protected MediaCalendarSection() { }

		protected MediaCalendarSection(ICalendarContent parent) : base(parent) { }

		protected abstract void ApplyDefaultMonthSettings(CalendarMonth targetMonth);

		public override void UpdateDaysCollection()
		{
			if (MediaScheduleSettings.MondayBased)
				UpdateDaysCollection<CalendarDayMondayBased>(MediaScheduleSettings.StartDayOfWeek, MediaScheduleSettings.EndDayOfWeek);
			else
				UpdateDaysCollection<CalendarDaySundayBased>(MediaScheduleSettings.StartDayOfWeek, MediaScheduleSettings.EndDayOfWeek);
		}

		public override void UpdateMonthCollection()
		{
			if (!MediaScheduleSettings.FlightDateStart.HasValue || !MediaScheduleSettings.FlightDateEnd.HasValue)
			{
				Months.Clear();
				return;
			}
			var months = new List<CalendarMonth>();
			var startDate = MediaScheduleSettings.FlightDateStart.Value;
			var monthTemplates = MediaScheduleSettings.MondayBased ?
				MediaMetaData.Instance.ListManager.MonthTemplatesMondayBased :
				MediaMetaData.Instance.ListManager.MonthTemplatesSundayBased;
			while (startDate <= MediaScheduleSettings.FlightDateEnd.Value)
			{
				var monthTemplate = monthTemplates.FirstOrDefault(mt => startDate >= mt.StartDate && startDate <= mt.EndDate);
				if (monthTemplate == null) continue;

				startDate = monthTemplate.Month.Value;
				var month = Months.FirstOrDefault(x => x.Date == startDate);
				if (month == null)
				{
					if (MediaScheduleSettings.MondayBased)
						month = new CalendarMonthMediaMondayBased(this);
					else
						month = new CalendarMonthMediaSundayBased(this);
					ApplyDefaultMonthSettings(month);
				}
				month.Date = monthTemplate.Month.Value;
				month.DaysRangeBegin = monthTemplate.StartDate.Value;
				month.DaysRangeEnd = monthTemplate.EndDate.Value;
				month.Days.Clear();
				month.Days.AddRange(Days.Where(x => x.Date >= month.DaysRangeBegin && x.Date <= month.DaysRangeEnd));
				months.Add(month);
				startDate = startDate.AddMonths(1);
			}
			Months.Clear();
			Months.AddRange(months);
		}
	}
}
