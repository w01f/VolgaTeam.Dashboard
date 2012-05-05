using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses.Views.MonthView
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class WeekControl : UserControl
    {
        private List<DayControl> _dayControls = new List<DayControl>();
        private List<CalendarNoteControl> _notes = new List<CalendarNoteControl>();

        private int _maxWeekDayIndex = 0;
        private int _minWeekDayIndex = 0;
        private WeekEmptySpaceControl _header = null;
        private WeekEmptySpaceControl _footer = null;

        public WeekControl(DayControl[] days)
        {
            InitializeComponent();
            this.Dock = DockStyle.Top;
            this.Resize += new EventHandler(WeekControl_Resize);

            if (days.Length > 0)
            {
                _dayControls.AddRange(days);
                this.Controls.AddRange(days);
                _maxWeekDayIndex = _dayControls.Max(x => x.Day.WeekDayIndex);
                if (_maxWeekDayIndex < 7)
                {
                    _footer = new WeekEmptySpaceControl();
                    this.Controls.Add(_footer);
                }

                _minWeekDayIndex = _dayControls.Min(x => x.Day.WeekDayIndex);
                if (_maxWeekDayIndex > 1)
                {
                    _header = new WeekEmptySpaceControl();
                    this.Controls.Add(_header);
                }
                FitControls();
            }
        }

        public void AddNotes(CalendarNoteControl[] notes)
        {
            foreach (CalendarNoteControl note in _notes)
                this.Controls.Remove(note);
            _notes.Clear();
            if (notes.Length > 0)
            {
                _notes.AddRange(notes);
                this.Controls.AddRange(notes);
                FitNotes();
            }
        }

        public void RefreshData()
        {
            foreach (DayControl day in _dayControls)
                day.RefreshData();
        }

        private void FitControls()
        {
            FitDays();
            FitNotes();
        }

        private void FitDays()
        {
            double height = this.Height;
            double width = this.Width / 7;

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
                _footer.Width = this.Width - ((int)width * _maxWeekDayIndex);
            }

            foreach (DayControl dayControl in _dayControls)
            {
                dayControl.Top = 0;
                dayControl.Height = (int)height;
                dayControl.Left = (dayControl.Day.WeekDayIndex - 1) * (int)width;
                dayControl.Width = dayControl.Day.WeekDayIndex == 7 ? this.Width - ((int)width * 6) : (int)width;
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
