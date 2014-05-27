using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;

namespace NewBizWiz.Calendar.Controls.PresentationClasses.Views.MonthView
{
	[ToolboxItem(false)]
	[IntendForClass(typeof(CommonCalendarNote))]
	public partial class CalendarNoteControl : UserControl
	{
		private readonly bool _allowToSave;

		public CalendarNoteControl(CalendarNote calendarNote)
		{
			InitializeComponent();
			CalendarNote = calendarNote;

			_allowToSave = false;
			labelControl.Text = CalendarNote.Note.FormattedText;
			memoEdit.EditValue = CalendarNote.Note.SimpleText;
			_allowToSave = true;

			RefreshColor();

			memoEdit.Enter += Utilities.Instance.Editor_Enter;
			memoEdit.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEdit.MouseUp += Utilities.Instance.Editor_MouseUp;
		}

		public CalendarNote CalendarNote { get; private set; }

		public event EventHandler<EventArgs> NoteChanged;
		public event EventHandler<EventArgs> NoteDeleted;
		public event EventHandler<EventArgs> NoteCopied;
		public event EventHandler<EventArgs> NoteCloned;
		public event EventHandler<EventArgs> ColorChanging;

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var rect = e.ClipRectangle.Top == 0 ?
				new Rectangle(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, Height) :
				new Rectangle(e.ClipRectangle.Left, 0, e.ClipRectangle.Width, e.ClipRectangle.Bottom);
			for (int i = 0; i < 1; i++)
			{
				ControlPaint.DrawBorder(e.Graphics, rect, Color.DarkGray, ButtonBorderStyle.Solid);
				rect.X = rect.X + 1;
				rect.Y = rect.Y + 1;
				rect.Width = rect.Width - 2;
				rect.Height = rect.Height - 2;
			}
		}

		public void RefreshColor()
		{
			BackColor = CalendarNote.BackgroundColor;
			memoEdit.BackColor = CalendarNote.BackgroundColor;
			memoEdit.ForeColor = CalendarNote.ForeColor;
			labelControl.BackColor = CalendarNote.BackgroundColor;
			labelControl.ForeColor = CalendarNote.ForeColor;
		}

		private void memoEdit_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			var newText = memoEdit.EditValue != null ? memoEdit.EditValue.ToString() : null;
			if (CalendarNote.Note.SimpleText != newText)
				CalendarNote.Note = !String.IsNullOrEmpty(newText) ? new TextItem(newText, false) : null;
			if (NoteChanged != null)
				NoteChanged(this, new EventArgs());
		}

		private void memoEdit_EditValueChanging(object sender, ChangingEventArgs e)
		{
			textBox.Text = e.NewValue != null ? e.NewValue.ToString() : string.Empty;
			if (!_allowToSave) return;
			var linesCount = WinAPIHelper.SendMessage(textBox.Handle, 0x00BA, IntPtr.Zero, IntPtr.Zero);
			if (linesCount <= 2) return;
			textBox.Text = e.OldValue != null ? e.OldValue.ToString() : string.Empty;
			e.Cancel = true;
		}

		private void memoEdit_Leave(object sender, EventArgs e)
		{
			memoEdit.Visible = false;
			labelControl.Text = CalendarNote.Note.FormattedText;
			labelControl.Visible = true;
		}

		private void labelControl_Click(object sender, EventArgs e)
		{
			memoEdit.Visible = true;
			memoEdit.Focus();
			labelControl.Visible = false;
		}

		private void pbClose_Click(object sender, EventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Do you want to delete note?") != DialogResult.Yes) return;
			if (NoteDeleted != null)
				NoteDeleted(sender, new EventArgs());
		}

		private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
		{
			if (NoteCopied != null)
				NoteCopied(sender, new EventArgs());
		}

		private void toolStripMenuItemClone_Click(object sender, EventArgs e)
		{
			if (NoteCloned != null)
				NoteCloned(sender, new EventArgs());
		}

		private void toolStripMenuItemColor_Click(object sender, EventArgs e)
		{
			if (ColorChanging != null)
				ColorChanging(sender, new EventArgs());
		}

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion
	}
}