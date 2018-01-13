using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Calendar.Interfaces;
using Asa.Business.Media.Configuration;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public class CustomDataCalendarSection : MediaCalendarSection
	{
		public override bool AllowCustomNotes => true;

		[JsonConstructor]
		protected CustomDataCalendarSection() { }

		public CustomDataCalendarSection(ICalendarContent parent) : base(parent) { }

		protected override void ApplyDefaultMonthSettings(CalendarMonth targetMonth)
		{
			targetMonth.OutputData.ShowLogo = MediaMetaData.Instance.ListManager.DefaultCustomCalendarSettings.ShowLogo;
			targetMonth.OutputData.ShowBigDate = MediaMetaData.Instance.ListManager.DefaultCustomCalendarSettings.ShowBigDate;
		}
	}
}
