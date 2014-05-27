using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.Calendar.Controls.BusinessClasses;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using Pabo.Calendar;

namespace NewBizWiz.Calendar.Controls.ToolForms
{
	public partial class FormCloneNote : Form
	{
		private readonly DateTime _flightDateEnd;
		private readonly DateTime _flightDateStart;
		private readonly List<DateRange> _selectedRanges = new List<DateRange>();
		private readonly CalendarNote _sourceNote;
		private DateTime? _selectedDate;

		public FormCloneNote(CalendarNote sourceNote, DateTime flightDateStart, DateTime flightDateEnd)
		{
			InitializeComponent();
			_sourceNote = sourceNote;
			_flightDateStart = flightDateStart;
			_flightDateEnd = flightDateEnd;
			labelControlFlightDates.Text = string.Format(labelControlFlightDates.Text, string.Format("{0} - {1}", new object[] { _flightDateStart.ToString("M/d/yy"), _flightDateEnd.ToString("M/d/yy") }));
			laClonedNote.Text = _sourceNote.Note.SimpleText;
			monthCalendarClone.ActiveMonth.Month = _sourceNote.StartDay.Month;
			monthCalendarClone.ActiveMonth.Year = _sourceNote.StartDay.Year;
			monthCalendarClone.Header.TextColor = Color.Black;

			UpdateTotals();

			if ((base.CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 4, laTitle.Font.Style);
				labelControlTooltip.Font = new Font(labelControlTooltip.Font.FontFamily, labelControlTooltip.Font.Size - 2, labelControlTooltip.Font.Style);
				labelControlFlightDates.Font = new Font(labelControlFlightDates.Font.FontFamily, labelControlFlightDates.Font.Size - 2, labelControlFlightDates.Font.Style);
				labelControlClonedNumber.Font = new Font(labelControlClonedNumber.Font.FontFamily, labelControlClonedNumber.Font.Size - 2, labelControlClonedNumber.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
				buttonXClearAll.Font = new Font(buttonXClearAll.Font.FontFamily, buttonXClearAll.Font.Size - 2, buttonXClearAll.Font.Style);
				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
			}
		}

		public DateRange[] SelectedRanges
		{
			get { return _selectedRanges.ToArray(); }
		}

		private void UpdateSelectedDates()
		{
			gridControlDays.DataSource = new BindingList<DateRange>(_selectedRanges.ToArray());
			monthCalendarClone.Refresh();
			UpdateTotals();
		}

		private void UpdateTotals()
		{
			labelControlClonedNumber.Text = string.Format("Cloned Notes: <b>{0}</b>", _selectedRanges.Count.ToString());
		}

		private void repositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			_selectedRanges.RemoveAt(gridViewDays.GetDataSourceRowIndex(gridViewDays.FocusedRowHandle));
			UpdateSelectedDates();
		}

		private void monthCalendarClone_DayQueryInfo(object sender, DayQueryInfoEventArgs e)
		{
			if (_sourceNote.StartDay <= e.Date && _sourceNote.FinishDay >= e.Date)
			{
				e.Info.BackColor1 = Color.Green;
				e.Info.TextColor = Color.Black;
				e.Info.DateColor = Color.Black;
				e.OwnerDraw = true;
			}
			if (_selectedRanges.Where(x => x.StartDate <= e.Date && x.FinishDate >= e.Date).Count() > 0)
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

		private void buttonXClearAll_Click(object sender, EventArgs e)
		{
			_selectedRanges.Clear();
			UpdateSelectedDates();
		}

		private void monthCalendarClone_DayClick(object sender, DayClickEventArgs e)
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
								if (_selectedRanges.Where(x => (x.StartDate <= temp && x.FinishDate >= temp)).Count() == 0)
								{
									DateTime startDate = _selectedDate.Value < temp ? _selectedDate.Value : temp;
									DateTime finishDate = _selectedDate.Value > temp ? _selectedDate.Value : temp;
									if (startDate < _sourceNote.StartDay && finishDate > _sourceNote.FinishDay)
										startDate = _sourceNote.FinishDay.AddDays(1);
									var _rangesToDelete = new List<DateRange>();
									_rangesToDelete.AddRange(_selectedRanges.Where(x => x.StartDate >= startDate && x.FinishDate <= finishDate));
									foreach (DateRange rande in _rangesToDelete)
										_selectedRanges.Remove(rande);
									_selectedRanges.AddRange(_sourceNote.Parent.CalculateDateRange(new[] { startDate, finishDate }));
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
						Utilities.Instance.ShowWarning("Pick a date that is in your Schedule Window…");
				}
			}
			else
			{
				_selectedDate = null;
			}
		}

		private void pbHelp_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("ninjanotesclone");
		}

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion
	}
}