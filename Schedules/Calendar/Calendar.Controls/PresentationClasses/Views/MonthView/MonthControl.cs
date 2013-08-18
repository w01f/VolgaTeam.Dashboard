﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NewBizWiz.Calendar.Controls.PresentationClasses.Views.MonthView
{
	[ToolboxItem(false)]
	public partial class MonthControl : UserControl
	{
		private readonly List<CalendarNoteControl> _noteControls = new List<CalendarNoteControl>();
		private readonly List<WeekControl> _weekControls = new List<WeekControl>();
		private readonly bool _weekSundayStarted = true;

		public MonthControl(bool weekSundayStarted = true)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			HasData = false;
			_weekSundayStarted = weekSundayStarted;
			pnMain.Resize += MonthViewControl_Resize;
		}

		public bool HasData { get; private set; }

		public void AddDays(DayControl[][] weeks)
		{
			_weekControls.Clear();
			foreach (var days in weeks)
			{
				var week = new WeekControl(days);
				_weekControls.Add(week);
				pnData.Controls.Add(week);
				week.BringToFront();
			}
			HasData = true;
		}

		public void AddNotes(CalendarNoteControl[][] notes)
		{
			_noteControls.Clear();
			for (int i = 0; i < _weekControls.Count; i++)
				if (i < notes.Length)
				{
					_weekControls[i].AddNotes(notes[i]);
					_noteControls.AddRange(notes[i]);
				}
		}

		public void RefreshData(Color colorLight, Color colorDark)
		{
			foreach (WeekControl week in _weekControls)
				week.RefreshData(colorLight, colorDark);
		}

		public void RefreshNotes()
		{
			foreach (CalendarNoteControl note in _noteControls)
				note.RefreshColor();
		}

		public void RaiseEvents(bool enable)
		{
			foreach (WeekControl week in _weekControls)
				week.RaiseEvents(enable);
		}

		private void FitHeader()
		{
			double width = Width / 7;
			if (_weekSundayStarted)
			{
				pnSunday.BringToFront();
				pnMonday.BringToFront();
				pnTuesday.BringToFront();
				pnWednesday.BringToFront();
				pnThursday.BringToFront();
				pnFriday.BringToFront();
				pnSaturday.BringToFront();
				pnSunday.Width = (int)width;
				pnMonday.Width = (int)width;
				pnTuesday.Width = (int)width;
				pnWednesday.Width = (int)width;
				pnThursday.Width = (int)width;
				pnFriday.Width = (int)width;
				pnSaturday.Width = Width - ((int)width * 6);
			}
			else
			{
				pnMonday.BringToFront();
				pnTuesday.BringToFront();
				pnWednesday.BringToFront();
				pnThursday.BringToFront();
				pnFriday.BringToFront();
				pnSaturday.BringToFront();
				pnSunday.BringToFront();
				pnMonday.Width = (int)width;
				pnTuesday.Width = (int)width;
				pnWednesday.Width = (int)width;
				pnThursday.Width = (int)width;
				pnFriday.Width = (int)width;
				pnSaturday.Width = (int)width;
				pnSunday.Width = Width - ((int)width * 6);
			}
		}

		private void FitWeekControls()
		{
			double height = pnData.Height / _weekControls.Count;
			for (int i = 0; i < _weekControls.Count; i++)
			{
				if (i == (_weekControls.Count - 1))
					_weekControls[i].Height = pnData.Height - ((int)height * i);
				else
					_weekControls[i].Height = (int)height;
				_weekControls[i].Refresh();
			}
		}

		private void MonthViewControl_Resize(object sender, EventArgs e)
		{
			pnEmpty.BringToFront();
			FitWeekControls();
			FitHeader();
			pnMain.BringToFront();
		}
	}
}