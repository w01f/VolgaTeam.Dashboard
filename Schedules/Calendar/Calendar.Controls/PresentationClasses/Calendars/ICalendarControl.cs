using CalendarBuilder.BusinessClasses;
using CalendarBuilder.ConfigurationClasses;
using CalendarBuilder.PresentationClasses.DayProperties;
using CalendarBuilder.PresentationClasses.SlideInfo;
using CalendarBuilder.PresentationClasses.Views;

namespace CalendarBuilder.PresentationClasses.Calendars
{
	public interface ICalendarControl
	{
		bool AllowToSave { get; }
		bool SettingsNotSaved { get; set; }

		Calendar CalendarData { get; }
		CalendarSettings CalendarSettings { get; }
		IView SelectedView { get; }
		DayPropertiesWrapper DayProperties { get; }
		SlideInfoWrapper SlideInfo { get; }

		void LeaveCalendar();
		void ShowCalendar();
		bool SaveCalendarData(string scheduleName = "");
		void LoadView();
		void SaveView();
		void Preview();
		void Print();
		void Email();
		void OpenHelp();
		void Splash(bool show);
	}
}