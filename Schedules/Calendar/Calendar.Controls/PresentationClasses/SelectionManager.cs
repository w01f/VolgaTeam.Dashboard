using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses
{
    public class SelectionManager
    {
        public Views.IView ParentView { get; private set; }
        public List<BusinessClasses.CalendarDay> SelectedDays { get; private set; }

        public event EventHandler<EventArgs> SelectionStateResponse;

        public SelectionManager(Views.IView parentView)
        {
            this.ParentView = parentView;
            this.SelectedDays = new List<BusinessClasses.CalendarDay>();
        }

        public void ClearSelection()
        {
            foreach (BusinessClasses.CalendarDay day in this.SelectedDays)
                this.ParentView.SelectDay(day, false);
            this.SelectedDays.Clear();
        }

        public void ProcessSelectionStateRequest()
        {
            if (this.SelectionStateResponse != null)
                this.SelectionStateResponse(this, new EventArgs());
        }

        public void SelectDay(BusinessClasses.CalendarDay day, Keys modifierKeys)
        {
            bool ctrlSelect = (modifierKeys & Keys.Control) == Keys.Control;
            bool shiftSelect = (modifierKeys & Keys.Shift) == Keys.Shift;
            if (!(ctrlSelect | shiftSelect))
                ClearSelection();
            if (shiftSelect)
            {
                BusinessClasses.CalendarDay prevSelectedDay = this.SelectedDays.LastOrDefault();
                if (prevSelectedDay != null)
                {
                    DateTime minDate = prevSelectedDay.Date > day.Date ? day.Date : prevSelectedDay.Date;
                    DateTime maxDate = prevSelectedDay.Date < day.Date ? day.Date : prevSelectedDay.Date;
                    foreach (BusinessClasses.CalendarDay dayToSelect in this.ParentView.Calendar.CalendarData.Days.Where(x => (x.Date >= minDate && x.Date < maxDate) && !this.SelectedDays.Contains(x)))
                    {
                        this.ParentView.SelectDay(dayToSelect, true);
                        this.SelectedDays.Add(dayToSelect);
                    }
                }
            }
            if (!this.SelectedDays.Contains(day))
            {
                this.ParentView.SelectDay(day, true);
                this.SelectedDays.Add(day);
            }
            this.ParentView.Calendar.DayProperties.LoadData(day);
        }
    }
}
