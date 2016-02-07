using System.ComponentModel;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Media.Entities.NonPersistent.Calendar;
using Asa.Calendar.Controls.PresentationClasses.Views.MonthView;
using Asa.Common.Core.Attributes;

namespace Asa.Media.Controls.PresentationClasses.Calendar
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
