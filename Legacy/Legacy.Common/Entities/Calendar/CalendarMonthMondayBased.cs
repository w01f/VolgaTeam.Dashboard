using System;

namespace Asa.Legacy.Common.Entities.Calendar
{
	public class CalendarMonthMondayBased : CalendarMonth
	{
		public CalendarMonthMondayBased()
		{
			OutputData = new CommonCalendarOutputData();
		}

		public override DateTime Date
		{
			get { return _date; }
			set
			{
				_date = value;
				var temp = value;
				while (temp.DayOfWeek != DayOfWeek.Monday)
					temp = temp.AddDays(-1);
				DaysRangeBegin = temp;

				temp = _date.AddMonths(1).AddDays(-1);
				while (temp.DayOfWeek != DayOfWeek.Sunday)
					temp = temp.AddDays(-1);
				DaysRangeEnd = temp;
			}
		}
	}
}
