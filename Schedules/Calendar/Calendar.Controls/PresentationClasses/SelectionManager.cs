using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Calendar.Controls.PresentationClasses.Views;
using Asa.Core.Calendar;

namespace Asa.Calendar.Controls.PresentationClasses
{
	public class SelectionManager
	{
		public SelectionManager(IView parentView)
		{
			ParentView = parentView;
			SelectedDays = new List<CalendarDay>();
		}

		public IView ParentView { get; private set; }
		public List<CalendarDay> SelectedDays { get; private set; }

		public event EventHandler<EventArgs> SelectionStateResponse;

		public void ClearSelection()
		{
			foreach (CalendarDay day in SelectedDays)
				ParentView.SelectDay(day, false);
			SelectedDays.Clear();
		}

		public void ProcessSelectionStateRequest()
		{
			if (SelectionStateResponse != null)
				SelectionStateResponse(this, new EventArgs());
		}

		public void SelectDay(CalendarDay day, Keys modifierKeys)
		{
			bool ctrlSelect = (modifierKeys & Keys.Control) == Keys.Control;
			bool shiftSelect = (modifierKeys & Keys.Shift) == Keys.Shift;
			if (!(ctrlSelect | shiftSelect))
				ClearSelection();
			if (shiftSelect)
			{
				CalendarDay prevSelectedDay = SelectedDays.LastOrDefault();
				if (prevSelectedDay != null)
				{
					DateTime minDate = prevSelectedDay.Date > day.Date ? day.Date : prevSelectedDay.Date;
					DateTime maxDate = prevSelectedDay.Date < day.Date ? day.Date : prevSelectedDay.Date;
					foreach (CalendarDay dayToSelect in ParentView.Calendar.CalendarData.Days.Where(x => (x.Date >= minDate && x.Date < maxDate) && !SelectedDays.Contains(x)))
					{
						ParentView.SelectDay(dayToSelect, true);
						SelectedDays.Add(dayToSelect);
					}
				}
			}
			if (!SelectedDays.Contains(day))
			{
				ParentView.SelectDay(day, true);
				SelectedDays.Add(day);
			}
		}
	}
}