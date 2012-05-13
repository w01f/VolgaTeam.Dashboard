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

        private void FitHeader()
        {
            double width = this.Width / 7;
            if (_weekSundayStarted)
            {
                laSunday.BringToFront();
                laMonday.BringToFront();
                laTuesday.BringToFront();
                laWednesday.BringToFront();
                laThursday.BringToFront();
                laFriday.BringToFront();
                laSaturday.BringToFront();
                laSunday.Width = (int)width;
                laMonday.Width = (int)width;
                laTuesday.Width = (int)width;
                laWednesday.Width = (int)width;
                laThursday.Width = (int)width;
                laFriday.Width = (int)width;
                laSaturday.Width = this.Width - ((int)width * 6);
            }
            else
            {
                laMonday.BringToFront();
                laTuesday.BringToFront();
                laWednesday.BringToFront();
                laThursday.BringToFront();
                laFriday.BringToFront();
                laSaturday.BringToFront();
                laSunday.BringToFront();
                laMonday.Width = (int)width;
                laTuesday.Width = (int)width;
                laWednesday.Width = (int)width;
                laThursday.Width = (int)width;
                laFriday.Width = (int)width;
                laSaturday.Width = (int)width;
                laSunday.Width = this.Width - ((int)width * 6);
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

        private void WeekDayTitle_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect;
            if (e.ClipRectangle.Top == 0)
                rect = new Rectangle(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, this.Height);
            else
                rect = new Rectangle(e.ClipRectangle.Left, 0, e.ClipRectangle.Width, e.ClipRectangle.Bottom);
            for (int i = 0; i < 1; i++)
            {
                ControlPaint.DrawBorder(e.Graphics, rect, Color.DarkGray, ButtonBorderStyle.Solid);
                rect.X = rect.X + 1;
                rect.Y = rect.Y + 1;
                rect.Width = rect.Width - 2;
                rect.Height = rect.Height - 2;
            }
        }
    }
}
