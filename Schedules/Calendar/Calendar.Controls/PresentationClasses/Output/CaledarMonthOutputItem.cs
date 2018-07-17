using Asa.Business.Calendar.Entities.NonPersistent;

namespace Asa.Calendar.Controls.PresentationClasses.Output
{
	public class CaledarMonthOutputItem
	{
		public CalendarMonth CalendarMonth { get; }
		public bool IsCurrent { get; set; }
		public string DisplayName => CalendarMonth.OutputData.MonthText;

		public CaledarMonthOutputItem(CalendarMonth calendarMonth)
		{
			CalendarMonth = calendarMonth;
		}
	}
}
