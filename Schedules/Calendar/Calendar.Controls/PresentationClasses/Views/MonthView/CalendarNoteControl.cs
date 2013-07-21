using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses.Views.MonthView
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CalendarNoteControl : UserControl
    {
        private bool _allowToSave = false;
        public BusinessClasses.CalendarNote CalendarNote { get; private set; }

        public event EventHandler<EventArgs> NoteChanged;
        public event EventHandler<EventArgs> NoteDeleted;
        public event EventHandler<EventArgs> NoteCopied;
        public event EventHandler<EventArgs> NoteCloned;
        public event EventHandler<EventArgs> ColorChanging;

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

        public CalendarNoteControl(BusinessClasses.CalendarNote calendarNote)
        {
            InitializeComponent();
            this.CalendarNote = calendarNote;

            _allowToSave = false;
            memoEdit.EditValue = this.CalendarNote.Note;
            _allowToSave = true;

            RefreshColor();

            memoEdit.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            memoEdit.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            memoEdit.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
        }

        public void RefreshColor()
        {
            this.BackColor = this.CalendarNote.BackgroundColor;
            memoEdit.BackColor = this.CalendarNote.BackgroundColor;
            memoEdit.ForeColor = this.CalendarNote.ForeColor;
        }

        private void memoEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                this.CalendarNote.Note = memoEdit.EditValue != null ? memoEdit.EditValue.ToString() : string.Empty;
                if (this.NoteChanged != null)
                    this.NoteChanged(sender, new EventArgs());
            }
        }

        private void memoEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            textBox.Text = e.NewValue != null ? e.NewValue.ToString() : string.Empty;
            if (_allowToSave)
            {
                int linesCount = InteropClasses.WinAPIHelper.SendMessage(textBox.Handle, 0x00BA, IntPtr.Zero, IntPtr.Zero);
                if (linesCount > 2)
                {
                    textBox.Text = e.OldValue != null ? e.OldValue.ToString() : string.Empty;
                    e.Cancel = true;
                }
            }
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            if (AppManager.ShowWarningQuestion("Do you want to delete note?") == DialogResult.Yes)
                if (this.NoteDeleted != null)
                    this.NoteDeleted(sender, new EventArgs());
        }

        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            if (this.NoteCopied != null)
                this.NoteCopied(sender, new EventArgs());
        }

        private void toolStripMenuItemClone_Click(object sender, EventArgs e)
        {
            if (this.NoteCloned != null)
                this.NoteCloned(sender, new EventArgs());
        }

        private void toolStripMenuItemColor_Click(object sender, EventArgs e)
        {
            if (this.ColorChanging != null)
                this.ColorChanging(sender, new EventArgs());
        }

        #region Picture Box Clicks Habdlers
        /// <summary>
        /// Buttonize the PictureBox 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top += 1;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top -= 1;
        }
        #endregion
    }
}
