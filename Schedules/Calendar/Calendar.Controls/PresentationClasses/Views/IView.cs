using System;
using Asa.Calendar.Controls.PresentationClasses.Calendars;
using Asa.Core.Calendar;

namespace Asa.Calendar.Controls.PresentationClasses.Views
{
	public interface IView
	{
		ICalendarControl Calendar { get; }
		CopyPasteManager CopyPasteManager { get; }
		bool SettingsNotSaved { get; set; }
		string Title { get; }

		event EventHandler<EventArgs> DataSaved;

		void ChangeMonth(DateTime date);
		void LoadData(bool reload);
		void Save();
		void RefreshData();
		void SelectDay(CalendarDay day, bool selected);

		#region Copy-Paste Methods
		void CopyDay();
		void PasteDay();
		void CloneDay();
		#endregion
	}
}