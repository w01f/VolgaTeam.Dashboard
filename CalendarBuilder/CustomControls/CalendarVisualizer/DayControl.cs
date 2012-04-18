﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalendarBuilder.CustomControls.CalendarVisualizer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class DayControl : UserControl
    {
        private bool _isSelected = false;
        public BusinessClasses.CalendarDay Day { get; set; }
        public event EventHandler<SelectDayEventArgs> DaySelected;
        public event EventHandler<EventArgs> PropertiesRequested;
        public event EventHandler<EventArgs> DayCopied;
        public event EventHandler<EventArgs> DayPasted;
        public event EventHandler<EventArgs> DayCloned;
        public event EventHandler<EventArgs> DayDataDeleted;

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
            toolStripMenuItemDelete.Enabled = this.Day.ContainsData;
            this.BackColor = this.BackColor == Color.Blue || this.BackColor == Color.Green ? (this.Day.ContainsData ? Color.Green : Color.Blue) : Color.AliceBlue;
            if (!this.Day.BelongsToSchedules)
            {
                xtraScrollableControl.BackColor = Color.LightGray;
                laSmallDayCaption.BackColor = Color.Gray;
            }
        }

        public void ChangeSelection(bool select)
        {
            _isSelected = select;
            this.Padding = new Padding(select ? 5 : 0);
            this.BackColor = _isSelected ? (this.Day.ContainsData ? Color.Green : Color.Blue) : Color.AliceBlue;
            this.Refresh();
        }

        private void Control_Click(object sender, EventArgs e)
        {
            if (this.Day.BelongsToSchedules)
                if (this.DaySelected != null)
                    this.DaySelected(sender, new SelectDayEventArgs(this, (ModifierKeys & Keys.Control) == Keys.Control));
        }

        private void Control_DoubleClick(object sender, EventArgs e)
        {
            if (this.Day.BelongsToSchedules)
                if (this.PropertiesRequested != null)
                    this.PropertiesRequested(sender, new SelectDayEventArgs(this, (ModifierKeys & Keys.Control) == Keys.Control));
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.Day.BelongsToSchedules)
                e.Cancel = true;
        }

        private void contextMenuStrip_Opened(object sender, EventArgs e)
        {
            if (!_isSelected)
                Control_Click(null, null);
        }

        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            if (this.DayCopied != null)
                this.DayCopied(sender, new EventArgs());
        }

        private void toolStripMenuItemPaste_Click(object sender, EventArgs e)
        {
            if (this.DayPasted != null)
                this.DayPasted(sender, new EventArgs());
        }

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            this.Day.ClearData();
            RefreshData();
            if (this.DayDataDeleted != null)
                this.DayDataDeleted(sender, new EventArgs());
        }

        private void toolStripMenuItemClone_Click(object sender, EventArgs e)
        {
            if (this.DayCloned != null)
                this.DayCloned(sender, new EventArgs());
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
