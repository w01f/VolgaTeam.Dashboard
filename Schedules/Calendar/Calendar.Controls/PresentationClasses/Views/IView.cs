using System;
using NewBizWiz.Calendar.Controls.PresentationClasses.Calendars;
using NewBizWiz.Core.Calendar;

namespace NewBizWiz.Calendar.Controls.PresentationClasses.Views
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