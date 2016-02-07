using Asa.Business.Calendar.Entities.NonPersistent;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public class MediaCalendarOutputData : CalendarOutputData
	{
		[JsonConstructor]
		private MediaCalendarOutputData() { }

		public MediaCalendarOutputData(CalendarMonth parent)
			: base(parent)
		{
			ApplyForAllCustomComment = false;
			ShowLogo = false;
		}
	}
}
