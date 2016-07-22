using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Calendar.Controls.PresentationClasses.Views;

namespace Asa.Calendar.Controls.PresentationClasses
{
	public class SelectionManager
	{
		public IView ParentView { get; private set; }
		public List<CalendarDay> SelectedDays { get; private set; }

		public event EventHandler<EventArgs> SelectionStateResponse;

		public SelectionManager(IView parentView)
		{
			ParentView = parentView;
			SelectedDays = new List<CalendarDay>();
		}

		public void ClearSelection()
		{
			foreach (var day in SelectedDays)
				ParentView.SelectDay(day, false);
			SelectedDays.Clear();
		}

		public void Release()
		{
			ClearSelection();
			SelectionStateResponse = null;
		}

		public void ProcessSelectionStateRequest()
		{
			SelectionStateResponse?.Invoke(this, new EventArgs());
		}

		public void SelectDay(CalendarDay day, Keys modifierKeys)
		{
			var ctrlSelect = (modifierKeys & Keys.Control) == Keys.Control;
			var shiftSelect = (modifierKeys & Keys.Shift) == Keys.Shift;
			if (!(ctrlSelect | shiftSelect))
				ClearSelection();
			if (shiftSelect)
			{
				var prevSelectedDay = SelectedDays.LastOrDefault();
				if (prevSelectedDay != null)
				{
					var minDate = prevSelectedDay.Date > day.Date ? day.Date : prevSelectedDay.Date;
					var maxDate = prevSelectedDay.Date < day.Date ? day.Date : prevSelectedDay.Date;
					foreach (var dayToSelect in ParentView.Calendar.CalendarContent.Days.Where(x => (x.Date >= minDate && x.Date < maxDate) && !SelectedDays.Contains(x)))
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