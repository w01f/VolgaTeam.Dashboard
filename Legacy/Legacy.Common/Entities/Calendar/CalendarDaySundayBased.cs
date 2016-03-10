using System;

namespace Asa.Legacy.Common.Entities.Calendar
{
	public class CalendarDaySundayBased : CalendarDay
	{
		public override int WeekDayIndex
		{
			get
			{
				switch (Date.DayOfWeek)
				{
					case DayOfWeek.Sunday:
						return 1;
					case DayOfWeek.Monday:
						return 2;
					case DayOfWeek.Tuesday:
						return 3;
					case DayOfWeek.Wednesday:
						return 4;
					case DayOfWeek.Thursday:
						return 5;
					case DayOfWeek.Friday:
						return 6;
					case DayOfWeek.Saturday:
						return 7;
					default:
						return 0;
				}
			}
		}

		public CalendarDaySundayBased() { }
	}
}
