using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses.Views
{
    public interface IView
    {
        Calendars.ICalendarControl Calendar { get; }
        CopyPasteManager CopyPasteManager { get; }
        bool SettingsNotSaved { get; set; }

        event EventHandler<EventArgs> DataSaved;

        void ChangeMonth(DateTime date);
        void LoadData(bool reload);
        void Save();
        void RefreshData();
        void SelectDay(BusinessClasses.CalendarDay day, bool selected);
        void Decorate(BusinessClasses.CalendarStyle style);

        #region Copy-Paste Methods
        void CopyDay();
        void PasteDay();
        void CloneDay();
        #endregion
    }
}
