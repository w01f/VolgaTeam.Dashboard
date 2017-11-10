using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using Pabo.Calendar;
using DateRange = Asa.Business.Common.Entities.NonPersistent.Common.DateRange;

namespace Asa.Calendar.Controls.ToolForms
{
	public partial class FormCloneNote: MetroForm
	{
		private readonly DateTime _flightDateEnd;
		private readonly DateTime _flightDateStart;
		private readonly List<DateRange> _selectedRanges = new List<DateRange>();
		private readonly CalendarNote _sourceNote;
		private DateTime? _selectedDate;

		public Action OnHelpClick { get; set; }

		public FormCloneNote(CalendarNote sourceNote, DateTime flightDateStart, DateTime flightDateEnd)
		{
			InitializeComponent();
			_sourceNote = sourceNote;
			_flightDateStart = flightDateStart;
			_flightDateEnd = flightDateEnd;
			simpleLabelItemFlightDates.Text = String.Format(simpleLabelItemFlightDates.Text, String.Format("{0} - {1}", new object[] { _flightDateStart.ToString("M/d/yy"), _flightDateEnd.ToString("M/d/yy") }));
			simpleLabelItemClonedNote.Text = _sourceNote.Note.SimpleText;
			monthCalendarClone.ActiveMonth.Month = _sourceNote.StartDay.Month;
			monthCalendarClone.ActiveMonth.Year = _sourceNote.StartDay.Year;
			monthCalendarClone.Header.TextColor = Color.Black;

			UpdateTotals();

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MaxSize, scaleFactor);
			layoutControlItemLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MinSize, scaleFactor);
			layoutControlItemHelp.MaxSize = RectangleHelper.ScaleSize(layoutControlItemHelp.MaxSize, scaleFactor);
			layoutControlItemHelp.MinSize = RectangleHelper.ScaleSize(layoutControlItemHelp.MinSize, scaleFactor);
			layoutControlItemClearAll.MaxSize = RectangleHelper.ScaleSize(layoutControlItemClearAll.MaxSize, scaleFactor);
			layoutControlItemClearAll.MinSize = RectangleHelper.ScaleSize(layoutControlItemClearAll.MinSize, scaleFactor);
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, scaleFactor);
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, scaleFactor);
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, scaleFactor);
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, scaleFactor);
		}

		public DateRange[] SelectedRanges => _selectedRanges.ToArray();

		private void UpdateSelectedDates()
		{
			gridControlDays.DataSource = new BindingList<DateRange>(_selectedRanges.ToArray());
			monthCalendarClone.Refresh();
			UpdateTotals();
		}

		private void UpdateTotals()
		{
			simpleLabelItemClonedNumber.Text = string.Format("<size=+2>Cloned Notes: <b>{0}</b></size>", _selectedRanges.Count.ToString());
		}

		private void OnDayGridButtonEditButtonClick(object sender, ButtonPressedEventArgs e)
		{
			_selectedRanges.RemoveAt(gridViewDays.GetDataSourceRowIndex(gridViewDays.FocusedRowHandle));
			UpdateSelectedDates();
		}

		private void OnCloneCalendarDayQueryInfo(object sender, DayQueryInfoEventArgs e)
		{
			if (_sourceNote.StartDay <= e.Date && _sourceNote.FinishDay >= e.Date)
			{
				e.Info.BackColor1 = Color.Green;
				e.Info.TextColor = Color.Black;
				e.Info.DateColor = Color.Black;
				e.OwnerDraw = true;
			}
			if (_selectedRanges.Count(x => x.StartDate <= e.Date && x.FinishDate >= e.Date) > 0)
			{
				e.Info.BackColor1 = Color.Blue;
				e.Info.TextColor = Color.White;
				e.Info.DateColor = Color.White;
				e.OwnerDraw = true;
			}
			else if (_selectedDate.HasValue && e.Date.Equals(_selectedDate.Value))
			{
				e.Info.BackColor1 = Color.Orange;
				e.Info.TextColor = Color.White;
				e.Info.DateColor = Color.White;
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
			_selectedRanges.Clear();
			UpdateSelectedDates();
		}

		private void OnCloneCalendarDayClick(object sender, DayClickEventArgs e)
		{
			if ((ModifierKeys & Keys.Shift) == Keys.Shift || (ModifierKeys & Keys.Control) == Keys.Control)
			{
				DateTime temp;
				if (DateTime.TryParse(e.Date, out temp))
				{
					if (temp >= _flightDateStart && temp <= _flightDateEnd)
					{
						if (!(temp >= _sourceNote.StartDay && temp <= _sourceNote.FinishDay))
						{
							if (_selectedDate.HasValue)
							{
								if (!_selectedRanges.Any(x => x.StartDate <= temp && x.FinishDate >= temp))
								{
									var startDate = _selectedDate.Value < temp ? _selectedDate.Value : temp;
									var finishDate = _selectedDate.Value > temp ? _selectedDate.Value : temp;
									if (startDate < _sourceNote.StartDay && finishDate > _sourceNote.FinishDay)
										startDate = _sourceNote.FinishDay.AddDays(1);
									var _rangesToDelete = new List<DateRange>();
									_rangesToDelete.AddRange(_selectedRanges.Where(x => x.StartDate >= startDate && x.FinishDate <= finishDate));
									foreach (DateRange rande in _rangesToDelete)
										_selectedRanges.Remove(rande);
									_selectedRanges.AddRange(_sourceNote.ParentCalendar.CalculateDateRange(new[] { startDate, finishDate }));
									UpdateSelectedDates();
									_selectedDate = null;
								}
							}
							else
							{
								_selectedDate = temp;
							}
						}
					}
					else
						PopupMessageHelper.Instance.ShowWarning("Pick a date that is in your Schedule Window…");
				}
			}
			else
			{
				_selectedDate = null;
			}
		}

		private void OnHelpButtonClick(object sender, EventArgs e)
		{
			OnHelpClick?.Invoke();
		}
	}
}