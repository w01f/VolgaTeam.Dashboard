using System.Linq;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public class CustomCalendar : MediaCalendar
	{
		protected override void InitSections()
		{
			if (Sections.Any()) return;
			Sections.Add(new CustomDataCalendarSection(this));
			foreach (var calendarSection in Sections)
				calendarSection.AfterConstraction();
		}
	}
}
