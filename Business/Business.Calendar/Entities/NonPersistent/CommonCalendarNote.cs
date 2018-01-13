using Newtonsoft.Json;

namespace Asa.Business.Calendar.Entities.NonPersistent
{
	public class CommonCalendarNote : CalendarNote
	{
		[JsonConstructor]
		private CommonCalendarNote() { }
		public CommonCalendarNote(CalendarSection parent) : base(parent) { }
	}
}
