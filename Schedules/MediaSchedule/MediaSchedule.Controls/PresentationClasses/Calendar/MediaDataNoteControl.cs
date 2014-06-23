using System.ComponentModel;
using System.Windows.Forms;
using NewBizWiz.Calendar.Controls.PresentationClasses.Views.MonthView;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses
{
	[ToolboxItem(false)]
	[IntendForClass(typeof(MediaDataNote))]
	public class MediaDataNoteControl : CalendarNoteControl
	{
		public MediaDataNoteControl(CalendarNote calendarNote)
			: base(calendarNote)
		{
			memoEdit.Width += pbClose.Width;
			labelControl.Width = memoEdit.Width;
			textBox.Width = memoEdit.Width;
			pbClose.Visible = false;
			toolStripMenuItemClone.Visible = false;
			toolStripMenuItemCopy.Visible = false;
		}
	}
}
