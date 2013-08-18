﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NewBizWiz.Calendar.Controls.PresentationClasses.Views.MonthView
{
	[ToolboxItem(false)]
	public partial class WeekControl : UserControl
	{
		private readonly List<DayControl> _dayControls = new List<DayControl>();
		private readonly WeekEmptySpaceControl _footer;
		private readonly WeekEmptySpaceControl _header;

		private readonly int _maxWeekDayIndex;
		private readonly int _minWeekDayIndex;
		private readonly List<CalendarNoteControl> _notes = new List<CalendarNoteControl>();

		public WeekControl(DayControl[] days)
		{
			InitializeComponent();
			Dock = DockStyle.Top;
			Resize += WeekControl_Resize;

			if (days.Length > 0)
			{
				_dayControls.AddRange(days);
				Controls.AddRange(days);
				_maxWeekDayIndex = _dayControls.Max(x => x.Day.WeekDayIndex);
				if (_maxWeekDayIndex < 7)
				{
					_footer = new WeekEmptySpaceControl();
					Controls.Add(_footer);
				}

				_minWeekDayIndex = _dayControls.Min(x => x.Day.WeekDayIndex);
				if (_maxWeekDayIndex > 1)
				{
					_header = new WeekEmptySpaceControl();
					Controls.Add(_header);
				}
				FitControls();
			}
		}

		public void AddNotes(CalendarNoteControl[] notes)
		{
			foreach (CalendarNoteControl note in _notes)
				Controls.Remove(note);
			_notes.Clear();
			if (notes.Length > 0)
			{
				_notes.AddRange(notes);
				Controls.AddRange(notes);
				FitNotes();
			}
		}

		public void RefreshData(Color colorLight, Color colorDark)
		{
			foreach (DayControl day in _dayControls)
				day.RefreshData(colorLight, colorDark);
		}

		public void RaiseEvents(bool enable)
		{
			foreach (DayControl day in _dayControls)
				day.RaiseEvents = enable;
		}

		private void FitControls()
		{
			FitDays();
			FitNotes();
		}

		private void FitDays()
		{
			double height = Height;
			double width = Width / 7;

			if (_header != null)
			{
				_header.Top = 0;
				_header.Left = 0;
				_header.Height = (int)height;
				_header.Width = ((int)width * (_minWeekDayIndex - 1));
			}

			if (_footer != null)
			{
				_footer.Top = 0;
				_footer.Left = (int)width * _maxWeekDayIndex;
				_footer.Height = (int)height;
				_footer.Width = Width - ((int)width * _maxWeekDayIndex);
			}

			foreach (DayControl dayControl in _dayControls)
			{
				dayControl.Top = 0;
				dayControl.Height = (int)height;
				dayControl.Left = (dayControl.Day.WeekDayIndex - 1) * (int)width;
				dayControl.Width = dayControl.Day.WeekDayIndex == 7 ? Width - ((int)width * 6) : (int)width;
			}
		}

		private void FitNotes()
		{
			foreach (CalendarNoteControl note in _notes)
			{
				DayControl startDay = _dayControls.Where(x => x.Day.Date.Equals(note.CalendarNote.StartDay)).FirstOrDefault();
				DayControl finishDay = _dayControls.Where(x => x.Day.Date.Equals(note.CalendarNote.FinishDay)).FirstOrDefault();
				if (startDay != null && finishDay != null)
				{
					note.Left = startDay.Left + 5;
					note.Top = startDay.Top + 25;
					note.Width = (finishDay.Right - startDay.Left) - 10;
					note.BringToFront();
				}
			}
		}

		private void WeekControl_Resize(object sender, EventArgs e)
		{
			FitControls();
		}
	}
}