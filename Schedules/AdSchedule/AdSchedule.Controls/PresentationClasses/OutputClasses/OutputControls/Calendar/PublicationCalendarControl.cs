using System;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar
{
	public class PublicationCalendarControl: AdCalendarControl
	{
		public override Core.Calendar.Calendar CalendarData
		{
			get { return _localSchedule.PublicationCalendar; }
		}

		public override ImageListBoxControl MonthList
		{
			get { return Controller.Instance.Calendar1MonthList; }
		}

		public override ButtonItem PreviewButton
		{
			get { return Controller.Instance.Calendar1Preview; }
		}

		public override ButtonItem EmailButton
		{
			get { return Controller.Instance.Calendar1Email; }
		}

		public override ButtonItem PowerPointButton
		{
			get { return Controller.Instance.Calendar1PowerPoint; }
		}

		public override ButtonItem PdfButton
		{
			get { return Controller.Instance.Calendar1Pdf; }
		}

		public override ButtonItem ThemeButton
		{
			get { throw new NotImplementedException(); }
		}

		public override ButtonItem CopyButton
		{
			get { return Controller.Instance.Calendar1Copy; }
		}

		public override ButtonItem PasteButton
		{
			get { return Controller.Instance.Calendar1Paste; }
		}

		public override ButtonItem CloneButton
		{
			get { return Controller.Instance.Calendar1Clone; }
		}

		protected override RibbonTabItem CalendarTab
		{
			get { return Controller.Instance.TabCalendar1; }
		}

		public override void Help_Click(object sender, EventArgs e)
		{
			OpenHelp("calendar");
		}

		public PublicationCalendarControl()
		{
			InitSlideInfo<PublicationSlideInfoControl>();
		}
	}
}
