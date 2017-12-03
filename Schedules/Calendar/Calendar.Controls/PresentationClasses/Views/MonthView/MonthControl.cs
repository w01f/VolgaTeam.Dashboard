using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Media.Entities.NonPersistent.Calendar;
using Asa.Common.Core.Attributes;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using DevExpress.XtraTab;

namespace Asa.Calendar.Controls.PresentationClasses.Views.MonthView
{
	[ToolboxItem(false)]
	//public partial class MonthControl : UserControl
	public partial class MonthControl : XtraTabPage
	{
		private readonly List<CalendarNoteControl> _noteControls = new List<CalendarNoteControl>();
		private readonly List<WeekControl> _weekControls = new List<WeekControl>();
		protected bool WeekSundayStarted { get; set; }

		protected MonthControl()
		{
			InitializeComponent();
			HasData = false;
			pnMain.Resize += OnResize;
			pnHeader.Height = Math.Max(pnHeader.Height, (Int32)(pnHeader.Height * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height * 0.85f));
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

		public void RefreshData(ColorSchema colorSchema)
		{
			foreach (var week in _weekControls)
				week.RefreshData(colorSchema);

			pnMonday.BackColor =
				pnTuesday.BackColor =
					pnWednesday.BackColor =
						pnThursday.BackColor =
							pnFriday.BackColor =
								pnSaturday.BackColor =
									pnSunday.BackColor = colorSchema.LineColor;
			laMonday.BackColor =
				laTuesday.BackColor =
					laWednesday.BackColor =
						laThursday.BackColor =
							laFriday.BackColor =
								laSaturday.BackColor =
									laSunday.BackColor = colorSchema.HeaderBackColor;
			laMonday.ForeColor =
				laTuesday.ForeColor =
					laWednesday.ForeColor =
						laThursday.ForeColor =
							laFriday.ForeColor =
								laSaturday.ForeColor =
									laSunday.ForeColor = colorSchema.HeaderForeColor;
		}

		public void ResizeControls()
		{
			pnEmpty.BringToFront();
			FitWeekControls();
			FitHeader();
			pnMain.BringToFront();
		}

		public void RefreshNotes()
		{
			foreach (var note in _noteControls)
				note.RefreshColor();
		}

		public void Release()
		{
			pnMain.Controls.Clear();

			_noteControls.ForEach(control => control.Release());
			_noteControls.Clear();

			_weekControls.ForEach(control => control.Release());
			_weekControls.Clear();
		}

		public void RaiseEvents(bool enable)
		{
			foreach (var week in _weekControls)
				week.RaiseEvents(enable);
		}

		private void FitHeader()
		{
			double width = Width / 7;
			if (WeekSundayStarted)
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
			if (!_weekControls.Any()) return;
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

		private void OnResize(object sender, EventArgs e)
		{
			ResizeControls();
		}
	}

	[IntendForClass(typeof(CalendarMonthSundayBased))]
	[IntendForClass(typeof(CalendarMonthMediaSundayBased))]
	public class MonthControlSundayBased : MonthControl
	{
		public MonthControlSundayBased()
		{
			WeekSundayStarted = true;
		}
	}

	[IntendForClass(typeof(CalendarMonthMondayBased))]
	[IntendForClass(typeof(CalendarMonthMediaMondayBased))]
	public class MonthControlMondayBased : MonthControl
	{
		public MonthControlMondayBased()
		{
			WeekSundayStarted = false;
		}
	}
}