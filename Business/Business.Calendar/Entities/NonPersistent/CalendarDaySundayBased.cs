using System;
using Asa.Business.Calendar.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Calendar.Entities.NonPersistent
{
	public class CalendarDaySundayBased: CalendarDay
	{
		[JsonConstructor]
		private CalendarDaySundayBased() { }

		public CalendarDaySundayBased(ICalendarContent parent)
			: base(parent) { }

		public override bool IsMondatBased
		{
			get { return false; }
		}

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
	}
}
