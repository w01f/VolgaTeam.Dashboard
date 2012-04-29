﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses.Views.MonthView
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class DayControl : UserControl
    {
        private BusinessClasses.CalendarStyle _style;
        private bool _allowToSave = false;
        private bool _isSelected = false;
        private bool _isCopySource = false;
 
        public BusinessClasses.CalendarDay Day { get; set; }
        public event EventHandler<SelectDayEventArgs> DaySelected;
        public event EventHandler<EventArgs> PropertiesRequested;
        public event EventHandler<EventArgs> DayCopied;
        public event EventHandler<EventArgs> DayPasted;
        public event EventHandler<EventArgs> DayCloned;
        public event EventHandler<EventArgs> DayDataDeleted;
        public event EventHandler<EventArgs> DataChanged;

        public DayControl(BusinessClasses.CalendarDay day)
        {
            InitializeComponent();
            this.Day = day;
            laSmallDayCaption.Text = this.Day.Date.Day.ToString();
            RefreshData();

            memoEditSimpleComment.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            memoEditSimpleComment.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            memoEditSimpleComment.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
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

        public void RefreshData()
        {
            _allowToSave = false;
            labelControlData.Text = this.Day.Summary;
            pbLogo.Image = this.Day.Logo.XtraTinyImage;
            pbLogo.Visible = _style == BusinessClasses.CalendarStyle.Graphic && this.Day.Logo.XtraTinyImage != null;
            memoEditSimpleComment.EditValue = this.Day.Comment1;
            toolStripMenuItemDelete.Enabled = this.Day.ContainsData;
            this.BackColor = this.BackColor == Color.Blue || this.BackColor == Color.Green ? (this.Day.ContainsData ? Color.Green : Color.Blue) : Color.FromArgb(175, 210, 255);
            if (!this.Day.BelongsToSchedules)
            {
                xtraScrollableControl.BackColor = Color.LightGray;
                laSmallDayCaption.BackColor = Color.Gray;
            }
            else if (_isCopySource)
            {
                xtraScrollableControl.BackColor = Color.LightGreen;
                laSmallDayCaption.BackColor = Color.Green;
            }
            else
            {
                xtraScrollableControl.BackColor = Color.AliceBlue;
                laSmallDayCaption.BackColor = Color.FromArgb(175, 210, 255);
            }
            _allowToSave = true;
        }

        public void ChangeSelection(bool select)
        {
            _isSelected = select;
            this.Padding = new Padding(select ? 5 : 1);
            this.BackColor = _isSelected ? (this.Day.ContainsData ? Color.Green : Color.Blue) : Color.FromArgb(175, 210, 255);
            this.Refresh();
        }

        public void ChangeCopySource(bool isCopySource)
        {
            _isCopySource = isCopySource;
            if (!this.Day.BelongsToSchedules)
            {
                xtraScrollableControl.BackColor = Color.LightGray;
                laSmallDayCaption.BackColor = Color.Gray;
            }
            else if (_isCopySource)
            {
                xtraScrollableControl.BackColor = Color.FromArgb(192, 255, 192);
                laSmallDayCaption.BackColor = Color.DarkSeaGreen;
            }
            else
            {
                xtraScrollableControl.BackColor = Color.AliceBlue;
                laSmallDayCaption.BackColor = Color.FromArgb(175, 210, 255);
            }
        }

        public void Decorate(BusinessClasses.CalendarStyle style)
        {
            _style = style;
            pbLogo.Visible = _style == BusinessClasses.CalendarStyle.Graphic && pbLogo.Image != null;
        }

        private void Control_Click(object sender, EventArgs e)
        {
            if (this.Day.BelongsToSchedules)
                if (this.DaySelected != null)
                    this.DaySelected(sender, new SelectDayEventArgs(this, ModifierKeys));
        }

        private void Control_DoubleClick(object sender, EventArgs e)
        {
            if (this.Day.BelongsToSchedules && _style != BusinessClasses.CalendarStyle.Simple)
            {
                if (this.PropertiesRequested != null)
                    this.PropertiesRequested(sender, new EventArgs());
            }
            else if (this.Day.BelongsToSchedules && _style == BusinessClasses.CalendarStyle.Simple)
            {
                memoEditSimpleComment.BringToFront();
                memoEditSimpleComment.Focus();
                memoEditSimpleComment.SelectAll();
            }
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
            if (this.DayDataDeleted != null)
                this.DayDataDeleted(sender, new EventArgs());
        }

        private void toolStripMenuItemClone_Click(object sender, EventArgs e)
        {
            if (this.DayCloned != null)
                this.DayCloned(sender, new EventArgs());
        }

        private void memoEditSimpleComment_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                this.Day.Comment1 = memoEditSimpleComment.EditValue != null ? memoEditSimpleComment.EditValue.ToString() : string.Empty;
                RefreshData();
                if (this.DataChanged != null)
                    this.DataChanged(sender, new EventArgs());
            }
        }

        private void memoEditSimpleComment_Leave(object sender, EventArgs e)
        {
            xtraScrollableControl.BringToFront();
        }
    }

    public class SelectDayEventArgs : EventArgs
    {
        public DayControl SelectedDay { get; private set; }
        public Keys ModifierKeys { get; private set; }

        public SelectDayEventArgs(DayControl selectedDay, Keys modifierKeys)
        {
            this.SelectedDay = selectedDay;
            this.ModifierKeys = modifierKeys;
        }
    }
}
