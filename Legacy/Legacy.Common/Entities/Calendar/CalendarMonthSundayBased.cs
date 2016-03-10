using System;

namespace Asa.Legacy.Common.Entities.Calendar
{
	public class CalendarMonthSundayBased : CalendarMonth
	{
		public CalendarMonthSundayBased()
		{
			OutputData = new CommonCalendarOutputData();
		}

		public override DateTime Date
		{
			get { return _date; }
			set
			{
				_date = value;
				DaysRangeBegin = _date;
				DaysRangeEnd = _date.AddMonths(1).AddDays(-1);
			}
		}
	}
}
