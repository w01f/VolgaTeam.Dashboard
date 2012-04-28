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
                FitDayControls();
            }
        }

        private void FitDayControls()
        {
            double height = this.Height;
            double width = this.Width/ 7;

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

        public void RefreshData()
        {
            foreach (DayControl day in _dayControls)
                day.RefreshData();
        }

        void WeekControl_Resize(object sender, EventArgs e)
        {
            FitDayControls();
        }
    }
}
