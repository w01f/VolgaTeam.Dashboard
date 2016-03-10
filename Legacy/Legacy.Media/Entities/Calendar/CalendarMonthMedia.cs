using System;
using Asa.Legacy.Common.Entities.Calendar;

namespace Asa.Legacy.Media.Entities.Calendar
{
	public abstract class CalendarMonthMedia : CalendarMonth
	{
		protected CalendarMonthMedia()
		{
			OutputData = new MediaCalendarOutputData();
		}

		public override DateTime Date
		{
			get { return _date; }
			set { _date = value; }
		}
	}
}
