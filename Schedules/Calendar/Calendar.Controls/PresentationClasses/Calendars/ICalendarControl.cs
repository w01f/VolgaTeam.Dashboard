using NewBizWiz.Calendar.Controls.PresentationClasses.SlideInfo;
using NewBizWiz.Calendar.Controls.PresentationClasses.Views;
using NewBizWiz.Core.Calendar;

namespace NewBizWiz.Calendar.Controls.PresentationClasses.Calendars
{
	public interface ICalendarControl
	{
		bool AllowToSave { get; }
		bool SettingsNotSaved { get; set; }

		Core.Calendar.Calendar CalendarData { get; }
		CalendarSettings CalendarSettings { get; }
		IView SelectedView { get; }
		SlideInfoWrapper SlideInfo { get; }

		void LeaveCalendar();
		void ShowCalendar(bool gridView);
		bool SaveCalendarData(string scheduleName = "");
		void UpdateOutputFunctions();
		void Preview();
		void Print();
		void Email();
		void OpenHelp();
		void Splash(bool show);
	}
}