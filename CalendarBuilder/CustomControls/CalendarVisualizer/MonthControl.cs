using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CalendarBuilder.CustomControls.CalendarVisualizer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class MonthControl : UserControl
    {
        private List<WeekControl> _weekControls = new List<WeekControl>();

        public MonthControl(DayControl[][] weeks)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            pnMain.Resize += new EventHandler(MonthViewControl_Resize);
            foreach (DayControl[] days in weeks)
            {
                WeekControl week = new WeekControl(days);
                _weekControls.Add(week);
                pnData.Controls.Add(week);
                week.BringToFront();
            }
        }

        private void FitHeader()
        {
            double width = this.Width / 7;
            laSunday.Width = (int)width;
            laMonday.Width = (int)width;
            laTuesday.Width = (int)width;
            laWednesday.Width = (int)width;
            laThursday.Width = (int)width;
            laFriday.Width = (int)width;
            laSaturday.Width = this.Width - ((int)width * 6);
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

        public void RefreshData()
        {
            foreach (WeekControl week in _weekControls)
                week.RefreshData();
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
