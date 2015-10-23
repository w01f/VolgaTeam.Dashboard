using System.ComponentModel;
using System.Windows.Forms;
using Asa.Calendar.Controls.PresentationClasses.Views.MonthView;
using Asa.Core.Calendar;
using Asa.Core.Common;
using Asa.Core.MediaSchedule;

namespace Asa.MediaSchedule.Controls.PresentationClasses
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
