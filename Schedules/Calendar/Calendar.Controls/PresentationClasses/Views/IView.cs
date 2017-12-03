using System;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Calendar.Controls.PresentationClasses.Calendars;
using Asa.Calendar.Controls.PresentationClasses.Views.MonthView;

namespace Asa.Calendar.Controls.PresentationClasses.Views
{
	public interface IView
	{
		ICalendarControl Calendar { get; }
		CopyPasteManager CopyPasteManager { get; }
		bool SettingsNotSaved { get; set; }
		string Title { get; }

		MonthControl SelectedMonth { get; }
		CalendarMonth SelectedMonthData { get; }

		event EventHandler<EventArgs> DataSaved;
		event EventHandler<EventArgs> SelectedMonthChanged;

		void LoadData();
		void Save();
		void RefreshData();
		void Release();
		void SelectDay(CalendarDay day, bool selected);

		#region Copy-Paste Methods
		void CopyDay();
		void PasteDay();
		void CloneDay();
		#endregion

	}
}