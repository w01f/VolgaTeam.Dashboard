using System;
using Newtonsoft.Json;

namespace Asa.Business.Calendar.Entities.NonPersistent
{
	public class CalendarDayMondayBased : CalendarDay
	{
		[JsonConstructor]
		private CalendarDayMondayBased() { }

		public CalendarDayMondayBased(CalendarSection parent)
			: base(parent) { }

		public override bool IsMondatBased => true;

		public override int WeekDayIndex
		{
			get
			{
				switch (Date.DayOfWeek)
				{
					case DayOfWeek.Monday:
						return 1;
					case DayOfWeek.Tuesday:
						return 2;
					case DayOfWeek.Wednesday:
						return 3;
					case DayOfWeek.Thursday:
						return 4;
					case DayOfWeek.Friday:
						return 5;
					case DayOfWeek.Saturday:
						return 6;
					case DayOfWeek.Sunday:
						return 7;
					default:
						return 0;
				}
			}
		}
	}
}
