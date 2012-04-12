using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CalendarBuilder.CustomControls.CalendarVisualizer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class DayControl : UserControl
    {
        public BusinessClasses.CalendarDay Day { get; set; }
        public event EventHandler<SelectDayEventArgs> DaySelected;
        public event EventHandler<EventArgs> PropertiesRequested;

        public DayControl(BusinessClasses.CalendarDay day)
        {
            InitializeComponent();
            this.Day = day;
            laSmallDayCaption.Text = this.Day.Date.Day.ToString();
            RefreshData();
        }

        public void RefreshData()
        {
            labelControlData.Text = this.Day.Summary;
            this.BackColor = this.BackColor == Color.Blue || this.BackColor == Color.Green ? (this.Day.ContainsData ? Color.Green : Color.Blue) : Color.AliceBlue;
        }

        public void ChangeSelection(bool select)
        {
            this.Padding = new Padding(select ? 5 : 0);
            this.BackColor = select ? (this.Day.ContainsData ? Color.Green : Color.Blue) : Color.AliceBlue;
            this.Refresh();
        }

        private void Control_Click(object sender, EventArgs e)
        {
            if (this.DaySelected != null)
                this.DaySelected(sender, new SelectDayEventArgs(this, (ModifierKeys & Keys.Control) == Keys.Control));
        }

        private void Control_DoubleClick(object sender, EventArgs e)
        {
            if (this.PropertiesRequested != null)
                this.PropertiesRequested(sender, new SelectDayEventArgs(this, (ModifierKeys & Keys.Control) == Keys.Control));
        }
    }

    public class SelectDayEventArgs : EventArgs
    {
        public DayControl SelectedDay { get; private set; }
        public bool MultiSelect { get; private set; }

        public SelectDayEventArgs(DayControl selectedDay, bool multiSelect)
        {
            this.SelectedDay = selectedDay;
            this.MultiSelect = multiSelect;
        }
    }
}
