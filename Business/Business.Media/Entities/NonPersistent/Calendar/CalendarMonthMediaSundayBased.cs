using Asa.Business.Calendar.Entities.NonPersistent;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public class CalendarMonthMediaSundayBased : CalendarMonthMedia
	{
		[JsonConstructor]
		private CalendarMonthMediaSundayBased() { }
		public CalendarMonthMediaSundayBased(CalendarSection parent) : base(parent) { }
	}
}
