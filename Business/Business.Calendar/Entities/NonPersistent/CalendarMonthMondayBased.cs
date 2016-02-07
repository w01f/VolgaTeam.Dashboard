using System;
using Asa.Business.Calendar.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Calendar.Entities.NonPersistent
{
	public class CalendarMonthMondayBased : CalendarMonth
	{
		[JsonConstructor]
		private CalendarMonthMondayBased() { }

		public CalendarMonthMondayBased(ICalendarContent parent)
			: base(parent)
		{
			OutputData = new CommonCalendarOutputData(this);
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
