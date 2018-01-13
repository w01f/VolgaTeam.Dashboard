using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Business.Calendar.Interfaces;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Interfaces;

namespace Asa.Business.Calendar.Entities.NonPersistent
{
	public abstract class CalendarContent<TSchedule, TScheduleSettings> : BaseSchedulePartitionContent<TSchedule, TScheduleSettings>, ICalendarContent
		where TSchedule : ISchedule<TScheduleSettings>
		where TScheduleSettings : IBaseScheduleSettings
	{
		public IBaseScheduleSettings Settings => ScheduleSettings;

		public List<CalendarSection> Sections { get; }

		protected CalendarContent()
		{
			Sections = new List<CalendarSection>();
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();

			foreach (var scheduleSection in Sections)
				scheduleSection.Parent = this;
		}

		public override void Dispose()
		{
			Sections.ForEach(s => s.Dispose());
			Sections.Clear();

			base.Dispose();
		}

		public abstract IEnumerable<DateRange> CalculateDateRange(IEnumerable<DateTime> dates);

		public abstract DateTime[][] GetDaysByWeek(DateTime start, DateTime end);

		protected IEnumerable<DateRange> CalculateDateRange(IEnumerable<DateTime> dates, DayOfWeek startDay, DayOfWeek endDay)
		{
			var result = new List<DateRange>();
			var selectedDates = new List<DateTime>();
			selectedDates.AddRange(dates);
			selectedDates.Sort();

			var firstSelectedDate = selectedDates.FirstOrDefault();
			var lastSelectedDate = selectedDates.LastOrDefault();
			var firstWeekday = firstSelectedDate;
			while (firstWeekday.DayOfWeek != startDay)
				firstWeekday = firstWeekday.AddDays(-1);
			var lastWeekday = firstSelectedDate;
			while (lastWeekday.DayOfWeek != endDay)
				lastWeekday = lastWeekday.AddDays(1);

			while (firstWeekday < lastSelectedDate)
			{
				var range = new DateRange();
				if (firstWeekday >= firstSelectedDate && lastWeekday <= lastSelectedDate)
				{
					range.StartDate = firstWeekday;
					range.FinishDate = lastWeekday;
				}
				else if (firstWeekday <= firstSelectedDate && lastWeekday >= lastSelectedDate)
				{
					range.StartDate = firstSelectedDate;
					range.FinishDate = lastSelectedDate;
				}
				else if (firstWeekday <= firstSelectedDate && lastWeekday >= firstSelectedDate)
				{
					range.StartDate = firstSelectedDate;
					range.FinishDate = lastWeekday;
				}
				else if (firstWeekday <= lastSelectedDate && lastWeekday >= lastSelectedDate)
				{
					range.StartDate = firstWeekday;
					range.FinishDate = lastSelectedDate;
				}
				result.Add(range);
				firstWeekday = firstWeekday.AddDays(7);
				lastWeekday = lastWeekday.AddDays(7);
			}
			return result;
		}

		protected DateTime[][] GetDaysByWeek(DateTime start, DateTime end, DayOfWeek borderDay)
		{
			var weeks = new List<DateTime[]>();
			var week = new List<DateTime>();
			while (!(start > end))
			{
				week.Add(start);
				if (start.DayOfWeek == borderDay)
				{
					weeks.Add(week.ToArray());
					week.Clear();
				}
				start = start.AddDays(1);
			}
			if (week.Count > 0)
				weeks.Add(week.ToArray());
			return weeks.ToArray();
		}
	}
}
