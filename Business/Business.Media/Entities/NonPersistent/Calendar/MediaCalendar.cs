using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.Persistent;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public abstract class MediaCalendar : CalendarContent<MediaSchedule, MediaScheduleSettings>
	{
		protected bool? _isMondayBased;

		public override MediaSchedule Schedule => ((MediaPartition)Parent).Schedule;

		protected override void AfterConstruction()
		{
			base.AfterConstruction();
			_isMondayBased = ScheduleSettings.MondayBased;
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();
			Schedule.CalendarTypeChanged += OnCalendarTypeChanged;
			OnCalendarTypeChanged(this, EventArgs.Empty);
		}

		public override void Dispose()
		{
			Schedule.CalendarTypeChanged -= OnCalendarTypeChanged;
			base.Dispose();
		}

		private void OnCalendarTypeChanged(object sender, EventArgs e)
		{
			if (_isMondayBased.HasValue && _isMondayBased == ScheduleSettings.MondayBased) return;
			_isMondayBased = ScheduleSettings.MondayBased;
			Reset();
		}

		public override void UpdateDaysCollection()
		{
			if (ScheduleSettings.MondayBased)
				UpdateDaysCollection<CalendarDayMondayBased>(ScheduleSettings.StartDayOfWeek, ScheduleSettings.EndDayOfWeek);
			else
				UpdateDaysCollection<CalendarDaySundayBased>(ScheduleSettings.StartDayOfWeek, ScheduleSettings.EndDayOfWeek);
		}

		public override IEnumerable<DateRange> CalculateDateRange(IEnumerable<DateTime> dates)
		{
			return CalculateDateRange(dates, ScheduleSettings.StartDayOfWeek, ScheduleSettings.EndDayOfWeek);
		}

		public override DateTime[][] GetDaysByWeek(DateTime start, DateTime end)
		{
			return GetDaysByWeek(start, end, ScheduleSettings.EndDayOfWeek);
		}

		protected abstract void ApplyDefaultMonthSettings(CalendarMonth targetMonth);

		public override void UpdateMonthCollection()
		{
			if (!ScheduleSettings.FlightDateStart.HasValue || !ScheduleSettings.FlightDateEnd.HasValue)
			{
				Months.Clear();
				return;
			}
			var months = new List<CalendarMonth>();
			var startDate = ScheduleSettings.FlightDateStart.Value;
			var monthTemplates = ScheduleSettings.MondayBased ?
				MediaMetaData.Instance.ListManager.MonthTemplatesMondayBased :
				MediaMetaData.Instance.ListManager.MonthTemplatesSundayBased;
			while (startDate <= ScheduleSettings.FlightDateEnd.Value)
			{
				var monthTemplate = monthTemplates.FirstOrDefault(mt => startDate >= mt.StartDate && startDate <= mt.EndDate);
				if (monthTemplate == null) continue;

				startDate = monthTemplate.Month.Value;
				var month = Months.FirstOrDefault(x => x.Date == startDate);
				if (month == null)
				{
					if (ScheduleSettings.MondayBased)
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

		public override void Reset()
		{
			Days.Clear();
			Months.Clear();
			Notes.Clear();
			AfterConstruction();
		}
	}
}
