using System;
using System.Drawing;
using System.Linq;
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

        public event EventHandler<EventArgs> SelectionStateRequested;
        public event EventHandler<MouseEventArgs> DayMouseMove;

        public event EventHandler<EventArgs> NoteAdded;
        public event EventHandler<EventArgs> NotePasted;

        public bool AllowToPasteNote { get; set; }
        public bool MultiSelectEnabled { get; set; }

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

        #region Coomon Methods
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
                memoEditSimpleComment.BackColor = Color.LightGray;
                pnCalendarNoteArea.BackColor = Color.LightGray;
                xtraScrollableControl.BackColor = Color.LightGray;
                laSmallDayCaption.BackColor = Color.Gray;
            }
            else if (_isCopySource)
            {
                memoEditSimpleComment.BackColor = Color.FromArgb(192, 255, 192);
                pnCalendarNoteArea.BackColor = Color.FromArgb(192, 255, 192);
                xtraScrollableControl.BackColor = Color.FromArgb(192, 255, 192);
                laSmallDayCaption.BackColor = Color.DarkSeaGreen;
            }
            else
            {
                memoEditSimpleComment.BackColor = Color.AliceBlue;
                pnCalendarNoteArea.BackColor = Color.AliceBlue;
                xtraScrollableControl.BackColor = Color.AliceBlue;
                laSmallDayCaption.BackColor = Color.FromArgb(175, 210, 255);
            }
            pnCalendarNoteArea.Visible = this.Day.HasNotes;
            _allowToSave = true;
        }

        public void Decorate(BusinessClasses.CalendarStyle style)
        {
            _style = style;
            pbLogo.Visible = _style == BusinessClasses.CalendarStyle.Graphic && pbLogo.Image != null;
        }
        #endregion

        #region Selection Methods0
        public void ChangeSelection(bool select)
        {
            _isSelected = select;
            this.Padding = new Padding(select ? 5 : 1);
            pnCalendarNoteArea.Height = select ? 35 : 40;
            this.BackColor = _isSelected ? (this.Day.ContainsData ? Color.Green : Color.Blue) : Color.FromArgb(175, 210, 255);
            this.Refresh();
        }

        public void UpdateNoteMenuAccordingSelection(BusinessClasses.CalendarDay[] selectedDays)
        {
            toolStripMenuItemAddNote.Text = "Add Note";
            toolStripMenuItemAddNote.Enabled = false;
            toolStripMenuItemPasteNote.Text = "Paste Note";
            toolStripMenuItemPasteNote.Enabled = false;
            if (selectedDays.Length > 1)
            {
                BusinessClasses.DateRange noteDateRange = this.Day.Parent.CalculateDateRange(selectedDays.Select(x => x.Date).ToArray()).LastOrDefault();
                if (noteDateRange != null)
                {
                    toolStripMenuItemAddNote.Text = "Add Note " + string.Format("({0}-{1})", new string[] { noteDateRange.StartDate.ToString("MM/dd"), noteDateRange.FinishDate.ToString("MM/dd") });
                    toolStripMenuItemAddNote.Enabled = !this.Day.HasNotes;
                    toolStripMenuItemPasteNote.Text = "Paste Note " + string.Format("({0}-{1})", new string[] { noteDateRange.StartDate.ToString("MM/dd"), noteDateRange.FinishDate.ToString("MM/dd") });
                    toolStripMenuItemPasteNote.Enabled = this.AllowToPasteNote;
                }
            }
        }

        private void Control_Click(object sender, EventArgs e)
        {
            if (this.Day.BelongsToSchedules)
                if (this.DaySelected != null)
                    this.DaySelected(this, new SelectDayEventArgs(this, ModifierKeys));
        }

        private void DayControl_MouseDown(object sender, MouseEventArgs e)
        {
            this.MultiSelectEnabled = true;
        }

        private void DayControl_MouseUp(object sender, MouseEventArgs e)
        {
            this.MultiSelectEnabled = false;
        }

        private void DayControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.MultiSelectEnabled)
                if (this.DayMouseMove != null)
                    this.DayMouseMove(this, e);
        }
        #endregion

        #region Copy\Paste Methods
        public void ChangeCopySource(bool isCopySource)
        {
            _isCopySource = isCopySource;
            if (!this.Day.BelongsToSchedules)
            {
                memoEditSimpleComment.BackColor = Color.LightGray;
                pnCalendarNoteArea.BackColor = Color.LightGray;
                xtraScrollableControl.BackColor = Color.LightGray;
                laSmallDayCaption.BackColor = Color.Gray;
            }
            else if (_isCopySource)
            {
                memoEditSimpleComment.BackColor = Color.FromArgb(192, 255, 192);
                pnCalendarNoteArea.BackColor = Color.FromArgb(192, 255, 192);
                xtraScrollableControl.BackColor = Color.FromArgb(192, 255, 192);
                laSmallDayCaption.BackColor = Color.DarkSeaGreen;
            }
            else
            {
                memoEditSimpleComment.BackColor = Color.AliceBlue;
                pnCalendarNoteArea.BackColor = Color.AliceBlue;
                xtraScrollableControl.BackColor = Color.AliceBlue;
                laSmallDayCaption.BackColor = Color.FromArgb(175, 210, 255);
            }
        }
        #endregion

        #region Common Event Handlers
        private void Control_DoubleClick(object sender, EventArgs e)
        {
            if (this.Day.BelongsToSchedules && (_style == BusinessClasses.CalendarStyle.Advanced || _style == BusinessClasses.CalendarStyle.Graphic))
            {
                if (this.PropertiesRequested != null)
                    this.PropertiesRequested(sender, new EventArgs());
            }
            else if (this.Day.BelongsToSchedules && _style == BusinessClasses.CalendarStyle.Simple)
            {
                memoEditSimpleComment.BringToFront();
                memoEditSimpleComment.Focus();
                memoEditSimpleComment.SelectAll();
                pnCalendarNoteArea.SendToBack();
            }
        }
        #endregion

        #region Popupp Menu Event Handlers
        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.Day.BelongsToSchedules)
                e.Cancel = true;
            else if (this.SelectionStateRequested != null)
                this.SelectionStateRequested(sender, new EventArgs());
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

        private void toolStripMenuItemAddNote_Click(object sender, EventArgs e)
        {
            if (this.NoteAdded != null)
                this.NoteAdded(sender, new EventArgs());
        }

        private void toolStripMenuItemPasteNote_Click(object sender, EventArgs e)
        {
            if (this.NotePasted != null)
                this.NotePasted(sender, new EventArgs());
        }
        #endregion

        #region Simple Calendar Event Handlers
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
            pnCalendarNoteArea.SendToBack();
        }
        #endregion
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
