using System;
using System.Collections.Generic;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Interfaces;

namespace Asa.Business.Calendar.Entities.NonPersistent
{
	public class CalendarSundayBased : CalendarContent<ISchedule<BaseScheduleSettings>, BaseScheduleSettings>
	{
		public override bool AllowCustomNotes
		{
			get { return true; }
		}

		public override void UpdateDaysCollection()
		{
			UpdateDaysCollection<CalendarDaySundayBased>(DayOfWeek.Sunday, DayOfWeek.Saturday);
		}

		public override void UpdateMonthCollection()
		{
			UpdateMonthCollection<CalendarMonthSundayBased>();
		}

		public override DateTime[][] GetDaysByWeek(DateTime start, DateTime end)
		{
			return GetDaysByWeek(start, end, DayOfWeek.Saturday);
		}

		public override IEnumerable<DateRange> CalculateDateRange(IEnumerable<DateTime> dates)
		{
			return CalculateDateRange(dates, DayOfWeek.Sunday, DayOfWeek.Saturday);
		}

		public override ISchedule<BaseScheduleSettings> Schedule
		{
			get { throw new NotImplementedException(); }
		}
	}
}
