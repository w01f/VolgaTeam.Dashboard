using System;
using Asa.Business.Calendar.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Calendar.Entities.NonPersistent
{
	public class CalendarMonthSundayBased : CalendarMonth
	{
		[JsonConstructor]
		private CalendarMonthSundayBased() { }

		public CalendarMonthSundayBased(ICalendarContent parent)
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
				DaysRangeBegin = _date;
				DaysRangeEnd = _date.AddMonths(1).AddDays(-1);
			}
		}
	}
}
