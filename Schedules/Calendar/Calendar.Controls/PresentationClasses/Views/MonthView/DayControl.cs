﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using Asa.Common.Core.Objects.Output;
using Asa.Common.GUI.Common;
using DevComponents.DotNetBar;
using Asa.Calendar.Controls.ToolForms;
using Padding = System.Windows.Forms.Padding;

namespace Asa.Calendar.Controls.PresentationClasses.Views.MonthView
{
	[ToolboxItem(false)]
	public partial class DayControl : UserControl
	{
		public const int HeaderHeight = 25;

		private bool _allowToSave;
		private bool _isCopySource;
		private bool _isSelected;
		private ColorSchema _colorSchema = new ColorSchema();
		private SuperTooltipInfo _tooltip = new SuperTooltipInfo();

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

		public event EventHandler<EventArgs> ImageCopied;
		public event EventHandler<EventArgs> ImagePasted;
		public event EventHandler<EventArgs> ImageDeleted;

		public DayControl(CalendarDay day)
		{
			InitializeComponent();
			Day = day;
			laSmallDayCaption.Text = Day.Date.Day.ToString();
			RefreshData(_colorSchema);

			memoEditSimpleComment.EnableSelectAll();
			toolStripMenuItemAddNote.Visible = toolStripMenuItemPasteNote.Visible = toolStripSeparator1.Visible = Day.Parent.AllowCustomNotes;

			laSmallDayCaption.Height = Math.Max(HeaderHeight, (Int32)(HeaderHeight * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height * 0.85f));
		}

		#region Common Methods
		public void RefreshData(ColorSchema colorSchema)
		{
			_allowToSave = false;
			_colorSchema = colorSchema;
			labelControlData.Text = Day.Summary;
			pbLogo.Image = Day.Logo.TinyImage;
			pbLogo.Visible = Day.Logo.ContainsData;
			pbLogo.Height = Day.Logo.ContainsData ? Day.Logo.TinyImage.Height : 0;
			memoEditSimpleComment.EditValue = Day.Comment;
			toolStripMenuItemEdit.Visible = true;
			toolStripMenuItemEdit.Enabled = true;
			toolStripMenuItemDelete.Enabled = Day.ContainsData;
			pnCalendarNoteArea.Visible = Day.HasNotes;
			RefreshColor();
			UpdateTooltip();
			_allowToSave = true;
		}

		public void Release()
		{
			DaySelected = null;
			DayCopied = null;
			DayPasted = null;
			DayCloned = null;
			DayDataDeleted = null;
			DataChanged = null;

			SelectionStateRequested = null;
			DayMouseMove = null;

			NoteAdded = null;
			NotePasted = null;

			ImageCopied = null;
			ImagePasted = null;
			ImageDeleted = null;

			_tooltip = null;
			Day = null;
		}

		public void RefreshColor()
		{
			BackColor = _isSelected ? (Day.ContainsData ? Color.Green : Color.Blue) : _colorSchema.LineColor;
			if (!Day.BelongsToSchedules)
			{
				memoEditSimpleComment.BackColor = _colorSchema.InactiveBodyColor;
				pnCalendarNoteArea.BackColor = _colorSchema.InactiveBodyColor;
				xtraScrollableControl.BackColor = _colorSchema.InactiveBodyColor;
				laSmallDayCaption.BackColor = _colorSchema.InactiveBackColor;
				laSmallDayCaption.ForeColor = _colorSchema.InactiveForeColor;
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
				memoEditSimpleComment.BackColor = _colorSchema.ActiveBodyColor;
				pnCalendarNoteArea.BackColor = _colorSchema.ActiveBodyColor;
				xtraScrollableControl.BackColor = _colorSchema.ActiveBodyColor;
				laSmallDayCaption.BackColor = _colorSchema.ActiveBackColor;
				laSmallDayCaption.ForeColor = _colorSchema.ActiveForeColor;
			}
		}

		private void UpdateTooltip()
		{
			if (Day.ContainsData)
			{
				_tooltip.HeaderText = Day.Date.ToString(@"dddd, MM/dd/yy");
				_tooltip.BodyText = Day.Summary;
				_tooltip.BodyImage = Day.Logo != null && Day.Logo.ContainsData ? Day.Logo.TinyImage : null;
				superTooltip.SetSuperTooltip(laSmallDayCaption, _tooltip);
				superTooltip.SetSuperTooltip(pnCalendarNoteArea, _tooltip);
				superTooltip.SetSuperTooltip(pbLogo, _tooltip);
				superTooltip.SetSuperTooltip(labelControlData, _tooltip);
				superTooltip.SetSuperTooltip(memoEditSimpleComment, _tooltip);
			}
			else
			{
				superTooltip.SetSuperTooltip(laSmallDayCaption, null);
				superTooltip.SetSuperTooltip(pnCalendarNoteArea, null);
				superTooltip.SetSuperTooltip(pbLogo, _tooltip);
				superTooltip.SetSuperTooltip(labelControlData, null);
				superTooltip.SetSuperTooltip(memoEditSimpleComment, null);
			}
		}
		#endregion

		#region Selection Methods
		public void ChangeSelection(bool select)
		{
			_isSelected = select;
			Padding = new Padding(select ? 5 : 1);
			pnCalendarNoteArea.Height = Math.Max(CalendarNoteControl.NoteHeight, (Int32)(CalendarNoteControl.NoteHeight * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height * 0.85f) + (select ? 0 : 5));
			RefreshColor();
			Refresh();
		}

		public void UpdateNoteMenuAccordingSelection(IEnumerable<CalendarDay> selectedDays)
		{
			toolStripMenuItemAddNote.Text = "Add Note";
			toolStripMenuItemAddNote.Enabled = false;
			toolStripMenuItemPasteNote.Text = "Paste Note";
			toolStripMenuItemPasteNote.Enabled = false;
			if (selectedDays.Count() <= 1) return;
			var noteDateRange = Day.Parent.Parent.CalculateDateRange(selectedDays.Select(x => x.Date).ToArray()).LastOrDefault();
			if (noteDateRange == null) return;
			toolStripMenuItemAddNote.Text = "Add Note " + string.Format("({0}-{1})", new[] { noteDateRange.StartDate.Value.ToString("MM/dd"), noteDateRange.FinishDate.Value.ToString("MM/dd") });
			toolStripMenuItemAddNote.Enabled = !Day.HasNotes;
			toolStripMenuItemPasteNote.Text = "Paste Note " + string.Format("({0}-{1})", new[] { noteDateRange.StartDate.Value.ToString("MM/dd"), noteDateRange.FinishDate.Value.ToString("MM/dd") });
			toolStripMenuItemPasteNote.Enabled = AllowToPasteNote;
		}

		private void Control_Click(object sender, MouseEventArgs e)
		{
			if (!RaiseEvents) return;
			if (e.Button != MouseButtons.Left) return;
			if (!Day.BelongsToSchedules) return;
			DaySelected?.Invoke(this, new SelectDayEventArgs(this, ModifierKeys));
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
			DayMouseMove?.Invoke(this, e);
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

		private void DayControl_DragOver(object sender, DragEventArgs e)
		{
			if (!Day.BelongsToSchedules)
				e.Effect = DragDropEffects.None;
			else
				e.Effect = DragDropEffects.Copy;
		}

		private void DayControl_DragDrop(object sender, DragEventArgs e)
		{
			var imageSource = e.Data.GetData(typeof(ImageSource)) as ImageSource;
			if (imageSource == null) return;
			Day.Logo = imageSource.Clone<ImageSource, ImageSource>();
			RefreshData(_colorSchema);
			DataChanged?.Invoke(this, new EventArgs());
		}
		#endregion

		#region Popup Menu Event Handlers
		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			if (!Day.BelongsToSchedules)
				e.Cancel = true;
			else SelectionStateRequested?.Invoke(sender, new EventArgs());
			toolStripMenuItemCopyImage.Enabled = Day.Logo.ContainsData;
			toolStripMenuItemDeleteImage.Enabled = Day.Logo.ContainsData;
			var clipBoardImage = ClipboardHelper.GetPngFormClipboard();
			toolStripMenuItemPasteImage.Enabled = clipBoardImage != null || Clipboard.ContainsText(TextDataFormat.Html);
		}

		private void contextMenuStrip_Opened(object sender, EventArgs e)
		{
			if (!_isSelected)
				Control_Click(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
		}

		private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
		{
			DayCopied?.Invoke(sender, new EventArgs());
		}

		private void toolStripMenuItemPaste_Click(object sender, EventArgs e)
		{
			DayPasted?.Invoke(sender, new EventArgs());
		}

		private void toolStripMenuItemEdit_Click(object sender, EventArgs e)
		{
			using (var form = new FormDayProperties(Day))
			{
				if (form.ShowDialog() != DialogResult.OK) return;
				RefreshData(_colorSchema);
				DataChanged?.Invoke(this, new EventArgs());
			}
		}

		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			DayDataDeleted?.Invoke(sender, new EventArgs());
		}

		private void toolStripMenuItemClone_Click(object sender, EventArgs e)
		{
			DayCloned?.Invoke(sender, new EventArgs());
		}

		private void toolStripMenuItemAddNote_Click(object sender, EventArgs e)
		{
			NoteAdded?.Invoke(sender, new EventArgs());
		}

		private void toolStripMenuItemPasteNote_Click(object sender, EventArgs e)
		{
			NotePasted?.Invoke(sender, new EventArgs());
		}

		private void toolStripMenuItemCopyImage_Click(object sender, EventArgs e)
		{
			ImageCopied?.Invoke(this, new EventArgs());
		}

		private void toolStripMenuItemPasteImage_Click(object sender, EventArgs e)
		{
			ImagePasted?.Invoke(this, new EventArgs());
		}

		private void toolStripMenuItemDeleteImage_Click(object sender, EventArgs e)
		{
			ImageDeleted?.Invoke(this, new EventArgs());
		}
		#endregion

		#region Simple Calendar Event Handlers
		private void memoEditSimpleComment_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			Day.Comment = memoEditSimpleComment.EditValue as String;
			RefreshData(_colorSchema);
			DataChanged?.Invoke(this, new EventArgs());
		}

		private void memoEditSimpleComment_Leave(object sender, EventArgs e)
		{
			xtraScrollableControl.Padding = new Padding(3);
			memoEditSimpleComment.Visible = false;
			labelControlData.Visible = true;
		}
		#endregion
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