using Asa.Business.Calendar.Entities.NonPersistent;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public class CalendarMonthMediaMondayBased : CalendarMonthMedia
	{
		[JsonConstructor]
		private CalendarMonthMediaMondayBased() { }

		public CalendarMonthMediaMondayBased(CalendarSection parent) : base(parent) { }
	}
}
