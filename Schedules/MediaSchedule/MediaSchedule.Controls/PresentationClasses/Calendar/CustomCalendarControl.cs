using DevComponents.DotNetBar;
using DevExpress.XtraEditors;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.Calendar
{
	public class CustomCalendarControl : MediaCalendarControl
	{
		public override Core.Calendar.Calendar CalendarData
		{
			get { return _localSchedule.CustomCalendar; }
		}

		public override ImageListBoxControl MonthList
		{
			get { return Controller.Instance.Calendar2MonthsList; }
		}

		public override ButtonItem PreviewButton
		{
			get { return Controller.Instance.Calendar2Preview; }
		}

		public override ButtonItem EmailButton
		{
			get { return Controller.Instance.Calendar2Email; }
		}

		public override ButtonItem PowerPointButton
		{
			get { return Controller.Instance.Calendar2PowerPoint; }
		}

		public override ButtonItem CopyButton
		{
			get { return Controller.Instance.Calendar2Copy; }
		}

		public override ButtonItem PasteButton
		{
			get { return Controller.Instance.Calendar2Paste; }
		}

		public override ButtonItem CloneButton
		{
			get { return Controller.Instance.Calendar2Clone; }
		}

		protected override RibbonTabItem CalendarTab
		{
			get { return Controller.Instance.TabCalendar2; }
		}
	}
}
