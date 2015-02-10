using System;
using System.Linq;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.Calendar
{
	public class BroadcastCalendarControl : MediaCalendarControl
	{
		public override Core.Calendar.Calendar CalendarData
		{
			get { return _localSchedule.BroadcastCalendar; }
		}

		public override ImageListBoxControl MonthList
		{
			get { return Controller.Instance.Calendar1MonthsList; }
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

		public override void LoadCalendar(bool quickLoad)
		{
			base.LoadCalendar(quickLoad);
			if (!_localSchedule.Section.Programs.Any()) return;
			if (!quickLoad)
				CalendarData.UpdateNotesCollection();
		}

		public override void Help_Click(object sender, EventArgs e)
		{
			OpenHelp("calendar");
		}

		public override void UpdateOutputFunctions()
		{
			base.UpdateOutputFunctions();
			var schedueSection = _localSchedule.Section;
			var enable = schedueSection.Programs.Any();
			MonthList.Enabled = enable;
			pnTop.Visible = enable;
			pnMain.Visible = enable;
			pictureBoxNoData.Image = Properties.Resources.CalendarDisabled;
			pictureBoxNoData.Visible = !enable;
			pictureBoxNoData.BringToFront();
		}
	}
}
