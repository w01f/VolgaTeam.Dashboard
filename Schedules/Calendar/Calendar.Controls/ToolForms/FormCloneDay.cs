using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using Pabo.Calendar;

namespace Asa.Calendar.Controls.ToolForms
{
	public partial class FormCloneDay : MetroForm
	{
		private readonly CalendarDay _day;
		private readonly DateTime _flightDateEnd;
		private readonly DateTime _flightDateStart;
		private readonly List<DateItem> _selectedDates = new List<DateItem>();

		public Action OnHelpClick { get; set; }

		public FormCloneDay(CalendarDay day, DateTime flightDateStart, DateTime flightDateEnd)
		{
			InitializeComponent();
			_day = day;
			_flightDateStart = flightDateStart;
			_flightDateEnd = flightDateEnd;
			simpleLabelItemFlightDates.Text = String.Format(simpleLabelItemFlightDates.Text, String.Format("{0} - {1}", new object[] { _flightDateStart.ToString("M/d/yy"), _flightDateEnd.ToString("M/d/yy") }));
			simpleLabelItemTitle.Text = String.Format("<size=+14><b>{0:dddd, MM/dd/yy}</b></size>", _day.Date);
			checkEditHighlightWeekdays.Text = String.Format(checkEditHighlightWeekdays.Text, _day.Date.ToString("dddd"));
			buttonXAddAllWeekdays.Text = String.Format(buttonXAddAllWeekdays.Text, _day.Date.ToString("dddd"));
			monthCalendarClone.ActiveMonth.Month = _day.Date.Month;
			monthCalendarClone.ActiveMonth.Year = _day.Date.Year;
			monthCalendarClone.Header.TextColor = Color.Black;
			if (_day.IsMondatBased)
				monthCalendarClone.FirstDayOfWeek = 2;

			UpdateTotals();

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MaxSize, scaleFactor);
			layoutControlItemLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MinSize, scaleFactor);
			layoutControlItemHelp.MaxSize = RectangleHelper.ScaleSize(layoutControlItemHelp.MaxSize, scaleFactor);
			layoutControlItemHelp.MinSize = RectangleHelper.ScaleSize(layoutControlItemHelp.MinSize, scaleFactor);
			layoutControlItemSelectAllDays.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSelectAllDays.MaxSize, scaleFactor);
			layoutControlItemSelectAllDays.MinSize = RectangleHelper.ScaleSize(layoutControlItemSelectAllDays.MinSize, scaleFactor);
			layoutControlItemSelectFirstDays.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSelectFirstDays.MaxSize, scaleFactor);
			layoutControlItemSelectFirstDays.MinSize = RectangleHelper.ScaleSize(layoutControlItemSelectFirstDays.MinSize, scaleFactor);
			layoutControlItemAddAllWeekdays.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAddAllWeekdays.MaxSize, scaleFactor);
			layoutControlItemAddAllWeekdays.MinSize = RectangleHelper.ScaleSize(layoutControlItemAddAllWeekdays.MinSize, scaleFactor);
			layoutControlItemClearAll.MaxSize = RectangleHelper.ScaleSize(layoutControlItemClearAll.MaxSize, scaleFactor);
			layoutControlItemClearAll.MinSize = RectangleHelper.ScaleSize(layoutControlItemClearAll.MinSize, scaleFactor);
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, scaleFactor);
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, scaleFactor);
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, scaleFactor);
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, scaleFactor);
		}

		public DateTime[] SelectedDates
		{
			get { return _selectedDates.Select(x => x.Date).ToArray(); }
		}

		private void UpdateSelectedDates()
		{
			gridControlDays.DataSource = new BindingList<DateItem>(_selectedDates.ToArray());
			monthCalendarClone.Refresh();
			UpdateTotals();
		}

		private void UpdateTotals()
		{
			simpleLabelItemClonedNumber.Text = string.Format("<size=+2>Cloned Days: <b>{0}</b></size>", _selectedDates.Count);
		}

		private void AddSelectedDate(DateTime selectedDate)
		{
			var dateItem = new DateItem();
			dateItem.Date = selectedDate;
			dateItem.BackColor1 = Color.Blue;
			if (!_selectedDates.Select(x => x.Date).Contains(selectedDate))
				_selectedDates.Add(dateItem);
			UpdateSelectedDates();
		}

		private void OnDayGridButtonEditButtonClick(object sender, ButtonPressedEventArgs e)
		{
			_selectedDates.RemoveAt(gridViewDays.GetDataSourceRowIndex(gridViewDays.FocusedRowHandle));
			UpdateSelectedDates();
		}

		private void OnCloneCalendarDayQueryInfo(object sender, DayQueryInfoEventArgs e)
		{
			if (_selectedDates.Select(x => x.Date).Contains(e.Date))
			{
				e.Info.BackColor1 = Color.Blue;
				e.Info.TextColor = Color.White;
				e.Info.DateColor = Color.White;
				e.OwnerDraw = true;
			}
			else if (e.Date == _day.Date)
			{
				e.Info.BackColor1 = Color.Green;
				e.Info.TextColor = Color.White;
				e.Info.DateColor = Color.White;
				e.OwnerDraw = true;
			}
			else if (e.Date.DayOfWeek == _day.Date.DayOfWeek && checkEditHighlightWeekdays.Checked && (e.Date >= _flightDateStart && e.Date <= _flightDateEnd))
			{
				e.Info.BoldedDate = true;
				e.OwnerDraw = true;
			}
			else if (!(e.Date >= _flightDateStart && e.Date <= _flightDateEnd))
			{
				e.Info.TextColor = Color.Gray;
				e.Info.DateColor = Color.Gray;
				e.OwnerDraw = true;
				e.OwnerDraw = true;
			}
		}

		private void OnClearAllClick(object sender, EventArgs e)
		{
			_selectedDates.Clear();
			UpdateSelectedDates();
		}

		private void OnAddAllWeekdaysClick(object sender, EventArgs e)
		{
			DateTime startDate = _flightDateStart;
			while (!startDate.DayOfWeek.Equals(_day.Date.DayOfWeek))
				startDate = startDate.AddDays(1);
			while (startDate <= _flightDateEnd)
			{
				if (startDate != _day.Date)
					AddSelectedDate(startDate.Date);
				startDate = startDate.AddDays(7);
			}
		}

		private void OnSelectAllDaysClick(object sender, EventArgs e)
		{
			DateTime startDate = _flightDateStart;
			while (startDate <= _flightDateEnd)
			{
				if (startDate != _day.Date)
					AddSelectedDate(startDate.Date);
				startDate = startDate.AddDays(1);
			}
		}

		private void OnSelectFirstDaysClick(object sender, EventArgs e)
		{
			DateTime startDate = _flightDateStart;
			while (startDate <= _flightDateEnd)
			{
				if (startDate != _day.Date && (startDate.Day == 1 || startDate.Equals(_flightDateStart)))
					AddSelectedDate(startDate.Date);
				startDate = startDate.AddDays(1);
			}
		}

		private void OnHighlightWeekdaysCheckedChanged(object sender, EventArgs e)
		{
			monthCalendarClone.Refresh();
		}

		private void OnCloneCalendarDayClick(object sender, DayClickEventArgs e)
		{
			DateTime temp;
			if (DateTime.TryParse(e.Date, out temp))
			{
				if (temp >= _flightDateStart && temp <= _flightDateEnd)
					AddSelectedDate(temp);
				else if (temp < _flightDateStart || temp > _flightDateEnd)
					PopupMessageHelper.Instance.ShowWarning("Pick a date that is in your Schedule Window…");
			}
		}

		private void OnHelpButtonClick(object sender, EventArgs e)
		{
			OnHelpClick?.Invoke();
		}
	}
}