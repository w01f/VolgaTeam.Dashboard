using System.Windows.Forms;
using Asa.Business.Calendar.Configuration;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Calendar.Interfaces;
using Asa.Common.Core.Objects.Output;
using DevComponents.DotNetBar;
using Asa.Calendar.Controls.PresentationClasses.SlideInfo;
using Asa.Calendar.Controls.PresentationClasses.Views;

namespace Asa.Calendar.Controls.PresentationClasses.Calendars
{
	public interface ICalendarControl
	{
		bool AllowToSave { get; }
		bool SettingsNotSaved { get; set; }

		ICalendarContent CalendarContent { get; }
		CalendarSettings CalendarSettings { get; }
		CalendarSection ActiveCalendarSection { get; }
		IView CalendarView { get; }
		SlideInfoWrapper SlideInfo { get; }
		ButtonItem CopyButton { get; }
		ButtonItem PasteButton { get; }
		ButtonItem CloneButton { get; }

		void AssignCloseActiveEditorsonOutSideClick(Control control);
		ColorSchema GetColorSchema(string colorName);
		void UpdateDataManagementAndOutputFunctions();
		void OpenHelp(string key);
		void Splash(bool show);
		void SaveSettings();
	}
}