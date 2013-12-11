using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.Calendar.Controls.ToolForms;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Calendar.Controls.PresentationClasses.Views.MonthView
{
	[ToolboxItem(false)]
	public partial class DayControl : UserControl
	{
		private bool _allowToSave;
		private bool _isCopySource;
		private bool _isSelected;
		private Color _colorLight = Color.White;
		private Color _colorDark = Color.LightGray;
		private List<ImageSource> _images = new List<ImageSource>();

		public DayControl(CalendarDay day, IEnumerable<ImageSource> dayImages)
		{
			InitializeComponent();
			Day = day;
			laSmallDayCaption.Text = Day.Date.Day.ToString();
			RefreshData(_colorLight, _colorDark);

			memoEditSimpleComment.Enter += Utilities.Instance.Editor_Enter;
			memoEditSimpleComment.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditSimpleComment.MouseUp += Utilities.Instance.Editor_MouseUp;

			toolStripMenuItemAddNote.Visible = toolStripMenuItemPasteNote.Visible = toolStripSeparator1.Visible = Day.Parent.AllowCustomNotes;

			_images.AddRange(dayImages);
		}

		#region Coomon Methods
		public void RefreshData(Color colorLight, Color colorDark)
		{
			_allowToSave = false;
			_colorLight = colorLight;
			_colorDark = colorDark;
			labelControlData.Text = Day.Summary;
			pbLogo.Image = Day.Logo.XtraTinyImage;
			pbLogo.Visible = Day.Logo.XtraTinyImage != null;
			memoEditSimpleComment.EditValue = Day.Comment1;
			toolStripMenuItemEdit.Visible = true;
			toolStripMenuItemEdit.Enabled = true;
			toolStripMenuItemDelete.Enabled = Day.ContainsData;
			pnCalendarNoteArea.Visible = Day.HasNotes;
			RefreshColor();
			_allowToSave = true;
		}

		public void RefreshColor()
		{
			BackColor = BackColor == Color.Blue || BackColor == Color.Green ? (Day.ContainsData ? Color.Green : Color.Blue) : Color.DarkGray;
			if (!Day.BelongsToSchedules)
			{
				memoEditSimpleComment.BackColor = _colorLight;
				pnCalendarNoteArea.BackColor = _colorLight;
				xtraScrollableControl.BackColor = _colorLight;
				laSmallDayCaption.BackColor = _colorDark;
				laSmallDayCaption.ForeColor = Color.White;
			}
			else if (_isCopySource)
			{
				memoEditSimpleComment.BackColor = Color.FromArgb(192, 255, 192);
				pnCalendarNoteArea.BackColor = Color.FromArgb(192, 255, 192);
				xtraScrollableControl.BackColor = Color.FromArgb(192, 255, 192);
				laSmallDayCaption.BackColor = Color.DarkSeaGreen;
				laSmallDayCaption.ForeColor = Color.Black;
			}
			else
			{
				memoEditSimpleComment.BackColor = Color.White;
				pnCalendarNoteArea.BackColor = Color.White;
				xtraScrollableControl.BackColor = Color.White;
				laSmallDayCaption.BackColor = _colorLight;
				laSmallDayCaption.ForeColor = Color.Black;
			}
		}
		#endregion

		#region Selection Methods
		public void ChangeSelection(bool select)
		{
			_isSelected = select;
			Padding = new Padding(select ? 5 : 1);
			pnCalendarNoteArea.Height = select ? 35 : 40;
			BackColor = _isSelected ? (Day.ContainsData ? Color.Green : Color.Blue) : Color.DarkGray;
			Refresh();
		}

		public void UpdateNoteMenuAccordingSelection(CalendarDay[] selectedDays)
		{
			toolStripMenuItemAddNote.Text = "Add Note";
			toolStripMenuItemAddNote.Enabled = false;
			toolStripMenuItemPasteNote.Text = "Paste Note";
			toolStripMenuItemPasteNote.Enabled = false;
			if (selectedDays.Length <= 1) return;
			var noteDateRange = Day.Parent.CalculateDateRange(selectedDays.Select(x => x.Date).ToArray()).LastOrDefault();
			if (noteDateRange == null) return;
			toolStripMenuItemAddNote.Text = "Add Note " + string.Format("({0}-{1})", new[] { noteDateRange.StartDate.ToString("MM/dd"), noteDateRange.FinishDate.ToString("MM/dd") });
			toolStripMenuItemAddNote.Enabled = !Day.HasNotes;
			toolStripMenuItemPasteNote.Text = "Paste Note " + string.Format("({0}-{1})", new[] { noteDateRange.StartDate.ToString("MM/dd"), noteDateRange.FinishDate.ToString("MM/dd") });
			toolStripMenuItemPasteNote.Enabled = AllowToPasteNote;
		}

		private void Control_Click(object sender, MouseEventArgs e)
		{
			if (!RaiseEvents) return;
			if (e.Button != MouseButtons.Left) return;
			if (!Day.BelongsToSchedules) return;
			if (DaySelected != null)
				DaySelected(this, new SelectDayEventArgs(this, ModifierKeys));
		}

		private void DayControl_MouseDown(object sender, MouseEventArgs e)
		{
			Control_Click(sender, e);
			if (RaiseEvents)
				MultiSelectEnabled = true;
		}

		private void DayControl_MouseUp(object sender, MouseEventArgs e)
		{
			if (RaiseEvents)
				MultiSelectEnabled = false;
		}

		private void DayControl_MouseMove(object sender, MouseEventArgs e)
		{
			if (!RaiseEvents) return;
			if (!MultiSelectEnabled) return;
			if (DayMouseMove != null)
				DayMouseMove(this, e);
		}

		#endregion

		#region Copy\Paste Methods
		public void ChangeCopySource(bool isCopySource)
		{
			_isCopySource = isCopySource;
			RefreshColor();
		}
		#endregion

		#region Common Event Handlers
		private void Control_DoubleClick(object sender, EventArgs e)
		{
			if (!Day.BelongsToSchedules) return;
			xtraScrollableControl.Padding = new Padding(0);
			labelControlData.Visible = false;
			memoEditSimpleComment.Visible = true;
			memoEditSimpleComment.Focus();
			memoEditSimpleComment.SelectAll();
		}
		#endregion

		#region Popupp Menu Event Handlers
		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			if (!Day.BelongsToSchedules)
				e.Cancel = true;
			else if (SelectionStateRequested != null)
				SelectionStateRequested(sender, new EventArgs());
		}

		private void contextMenuStrip_Opened(object sender, EventArgs e)
		{
			if (!_isSelected)
				Control_Click(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
		}

		private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
		{
			if (DayCopied != null)
				DayCopied(sender, new EventArgs());
		}

		private void toolStripMenuItemPaste_Click(object sender, EventArgs e)
		{
			if (DayPasted != null)
				DayPasted(sender, new EventArgs());
		}

		private void toolStripMenuItemEdit_Click(object sender, EventArgs e)
		{
			using (var form = new FormDayProperties(Day))
			{
				form.LoadImages(_images);
				if (form.ShowDialog() != DialogResult.OK) return;
				RefreshData(_colorLight, _colorDark);
				if (DataChanged != null)
					DataChanged(sender, new EventArgs());
			}
		}

		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			if (DayDataDeleted != null)
				DayDataDeleted(sender, new EventArgs());
		}

		private void toolStripMenuItemClone_Click(object sender, EventArgs e)
		{
			if (DayCloned != null)
				DayCloned(sender, new EventArgs());
		}

		private void toolStripMenuItemAddNote_Click(object sender, EventArgs e)
		{
			if (NoteAdded != null)
				NoteAdded(sender, new EventArgs());
		}

		private void toolStripMenuItemPasteNote_Click(object sender, EventArgs e)
		{
			if (NotePasted != null)
				NotePasted(sender, new EventArgs());
		}
		#endregion

		#region Simple Calendar Event Handlers
		private void memoEditSimpleComment_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			Day.Comment1 = memoEditSimpleComment.EditValue != null ? memoEditSimpleComment.EditValue.ToString() : string.Empty;
			RefreshData(_colorLight, _colorDark);
			if (DataChanged != null)
				DataChanged(sender, new EventArgs());
		}

		private void memoEditSimpleComment_Leave(object sender, EventArgs e)
		{
			xtraScrollableControl.Padding = new Padding(3);
			memoEditSimpleComment.Visible = false;
			labelControlData.Visible = true;
		}
		#endregion

		public CalendarDay Day { get; set; }

		public bool RaiseEvents { get; set; }
		public bool AllowToPasteNote { get; set; }
		public bool MultiSelectEnabled { get; set; }
		public event EventHandler<SelectDayEventArgs> DaySelected;
		public event EventHandler<EventArgs> DayCopied;
		public event EventHandler<EventArgs> DayPasted;
		public event EventHandler<EventArgs> DayCloned;
		public event EventHandler<EventArgs> DayDataDeleted;
		public event EventHandler<EventArgs> DataChanged;

		public event EventHandler<EventArgs> SelectionStateRequested;
		public event EventHandler<MouseEventArgs> DayMouseMove;

		public event EventHandler<EventArgs> NoteAdded;
		public event EventHandler<EventArgs> NotePasted;
	}

	public class SelectDayEventArgs : EventArgs
	{
		public SelectDayEventArgs(DayControl selectedDay, Keys modifierKeys)
		{
			SelectedDay = selectedDay;
			ModifierKeys = modifierKeys;
		}

		public DayControl SelectedDay { get; private set; }
		public Keys ModifierKeys { get; private set; }
	}
}