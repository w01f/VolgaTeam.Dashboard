using System.Collections.Generic;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NewBizWiz.Calendar.Controls.PresentationClasses.SlideInfo;
using NewBizWiz.Calendar.Controls.PresentationClasses.Views;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;

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
		ButtonItem CopyButton { get; }
		ButtonItem PasteButton { get; }
		ButtonItem CloneButton { get; }

		void AssignCloseActiveEditorsonOutSideClick(Control control);
		void LeaveCalendar();
		void ShowCalendar(bool gridView);
		bool SaveCalendarData(bool byUser, string scheduleName = "");
		void UpdateOutputFunctions();
		void Preview();
		void Print();
		void Email();
		void OpenHelp();
		void Splash(bool show);
		void SaveSettings();
		void TrackActivity(UserActivity activity);
	}
}