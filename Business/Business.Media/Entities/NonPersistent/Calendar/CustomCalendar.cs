using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Media.Configuration;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public class CustomCalendar : MediaCalendar
	{
		public override bool AllowCustomNotes
		{
			get { return true; }
		}

		protected override void ApplyDefaultMonthSettings(CalendarMonth targetMonth)
		{
			targetMonth.OutputData.ShowLogo = MediaMetaData.Instance.ListManager.DefaultCustomCalendarSettings.ShowLogo;
			targetMonth.OutputData.ShowBigDate = MediaMetaData.Instance.ListManager.DefaultCustomCalendarSettings.ShowBigDate;
		}
	}
}
