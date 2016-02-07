using Asa.Business.Calendar.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public class CalendarMonthMediaSundayBased : CalendarMonthMedia
	{
		[JsonConstructor]
		private CalendarMonthMediaSundayBased() { }
		public CalendarMonthMediaSundayBased(ICalendarContent parent) : base(parent) { }
	}
}
