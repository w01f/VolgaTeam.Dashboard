﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Common.Core.Attributes;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.GUI.Common;
using DevExpress.XtraEditors.Controls;

namespace Asa.Calendar.Controls.PresentationClasses.Views.MonthView
{
	[ToolboxItem(false)]
	[IntendForClass(typeof(CommonCalendarNote))]
	public partial class CalendarNoteControl : UserControl
	{
		public const int NoteHeight = 35;


		private readonly bool _allowToSave;

		public CalendarNote CalendarNote { get; private set; }

		public event EventHandler<EventArgs> NoteChanged;
		public event EventHandler<EventArgs> NoteDeleted;
		public event EventHandler<EventArgs> NoteCopied;
		public event EventHandler<EventArgs> NoteCloned;
		public event EventHandler<EventArgs> ColorChanging;

		public CalendarNoteControl(CalendarNote calendarNote)
		{
			InitializeComponent();

			Height = Math.Max(NoteHeight, (Int32)(NoteHeight * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height * 0.85f));

			CalendarNote = calendarNote;

			_allowToSave = false;
			if (CalendarNote.Note != null)
			{
				labelControl.Text = CalendarNote.Note.FormattedText;
				memoEdit.EditValue = CalendarNote.Note.SimpleText;
			}
			_allowToSave = true;

			RefreshColor();

			if (calendarNote.UserAdded)
			{
				OnLabelControlClick(null, EventArgs.Empty);
				calendarNote.UserAdded = false;
			}

			pbClose.Buttonize();
			memoEdit.DisableSelectAll();
		}

		public void Release()
		{
			NoteChanged = null;
			NoteDeleted = null;
			NoteCopied = null;
			NoteCloned = null;
			ColorChanging = null;
			CalendarNote = null;
		}

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

		private void OnMemoEditEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			var newText = memoEdit.EditValue?.ToString();
			if (CalendarNote.Note.SimpleText != newText)
				CalendarNote.Note = !String.IsNullOrEmpty(newText) ? new TextItem(newText, false) : null;
			NoteChanged?.Invoke(this, new EventArgs());
		}

		private void OnMemoEditEditValueChanging(object sender, ChangingEventArgs e)
		{
			textBox.Text = e.NewValue?.ToString() ?? string.Empty;
			if (!_allowToSave) return;
			var linesCount = WinAPIHelper.SendMessage(textBox.Handle, 0x00BA, IntPtr.Zero, IntPtr.Zero);
			if (linesCount <= 2) return;
			textBox.Text = e.OldValue?.ToString() ?? string.Empty;
			e.Cancel = true;
		}

		private void OnMemoEditLeave(object sender, EventArgs e)
		{
			memoEdit.Visible = false;
			labelControl.Text = CalendarNote.Note.FormattedText;
			labelControl.Visible = true;
			labelControl.BringToFront();
		}

		private void OnLabelControlClick(object sender, EventArgs e)
		{
			memoEdit.Visible = true;
			memoEdit.Focus();
			labelControl.Visible = false;
		}

		private void OnCloseClick(object sender, EventArgs e)
		{
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Do you want to delete note?") != DialogResult.Yes) return;
			NoteDeleted?.Invoke(sender, new EventArgs());
		}

		private void OnMenuItemCopyClick(object sender, EventArgs e)
		{
			NoteCopied?.Invoke(sender, new EventArgs());
		}

		private void OnMenuItemCloneClick(object sender, EventArgs e)
		{
			NoteCloned?.Invoke(sender, new EventArgs());
		}

		private void OnMenuItemColorClick(object sender, EventArgs e)
		{
			ColorChanging?.Invoke(sender, new EventArgs());
		}
	}
}