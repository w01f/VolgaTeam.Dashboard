using Newtonsoft.Json;

namespace Asa.Business.Calendar.Entities.NonPersistent
{
	public class CommonCalendarOutputData : CalendarOutputData
	{
		[JsonConstructor]
		private CommonCalendarOutputData() { }
		public CommonCalendarOutputData(CalendarMonth parent) : base(parent) { }
	}
}
