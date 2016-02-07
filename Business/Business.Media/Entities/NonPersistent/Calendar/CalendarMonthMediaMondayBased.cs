using Asa.Business.Calendar.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public class CalendarMonthMediaMondayBased : CalendarMonthMedia
	{
		[JsonConstructor]
		private CalendarMonthMediaMondayBased() { }

		public CalendarMonthMediaMondayBased(ICalendarContent parent) : base(parent) { }
	}
}
