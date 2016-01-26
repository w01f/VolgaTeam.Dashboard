using System.Windows.Forms;
using DevComponents.DotNetBar;
using Asa.Calendar.Controls.PresentationClasses.SlideInfo;
using Asa.Calendar.Controls.PresentationClasses.Views;
using Asa.Core.Calendar;
using Asa.Core.Common;

namespace Asa.Calendar.Controls.PresentationClasses.Calendars
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
		ColorSchema GetColorSchema(string colorName);
		void UpdateOutputFunctions();
		void Preview();
		void Print();
		void Email();
		void OpenHelp(string key);
		void Splash(bool show);
		void SaveSettings();
	}
}