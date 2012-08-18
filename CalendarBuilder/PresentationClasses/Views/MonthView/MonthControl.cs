using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses.Views.MonthView
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class MonthControl : UserControl
    {
        private bool _weekSundayStarted = true;
        private List<WeekControl> _weekControls = new List<WeekControl>();
        private List<CalendarNoteControl> _noteControls = new List<CalendarNoteControl>();

        public bool HasData { get; private set; }

        public MonthControl(bool weekSundayStarted)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.HasData = false;
            _weekSundayStarted = weekSundayStarted;
            pnMain.Resize += new EventHandler(MonthViewControl_Resize);
        }

        public void AddDays(DayControl[][] weeks)
        {
            _weekControls.Clear();
            foreach (DayControl[] days in weeks)
            {
                WeekControl week = new WeekControl(days);
                _weekControls.Add(week);
                pnData.Controls.Add(week);
                week.BringToFront();
            }
            this.HasData = true;
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

        public void RefreshData()
        {
            foreach (WeekControl week in _weekControls)
                week.RefreshData();
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
            double width = this.Width / 7;
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
                pnSaturday.Width = this.Width - ((int)width * 6);
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
                pnSunday.Width = this.Width - ((int)width * 6);
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
