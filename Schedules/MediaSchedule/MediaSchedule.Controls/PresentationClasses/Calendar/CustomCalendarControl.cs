using Asa.Business.Common.Enums;
using Asa.Business.Media.Entities.NonPersistent.Calendar;
using Asa.Common.Core.Helpers;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;

namespace Asa.Media.Controls.PresentationClasses.Calendar
{
	public class CustomCalendarControl : MediaCalendarControl
	{
		public override string Identifier => ContentIdentifiers.CustomCalendar;

		public override RibbonTabItem TabPage => Controller.Instance.TabCalendar2;

		protected override RibbonControl Ribbon => Controller.Instance.Ribbon;

		protected override ImageListBoxControl MonthList => Controller.Instance.Calendar2MonthsList;

		public override ButtonItem CopyButton => Controller.Instance.Calendar2Copy;

		public override ButtonItem PasteButton => Controller.Instance.Calendar2Paste;

		public override ButtonItem CloneButton => Controller.Instance.Calendar2Clone;

		#region BasePartitionEditControl Override
		protected override bool IsContentChanged => EditedContent == null || (ContentUpdateInfo.ChangeInfo.WholeScheduleChanged ||
		                                                                      ContentUpdateInfo.ChangeInfo.ScheduleDatesChanged ||
		                                                                      ContentUpdateInfo.ChangeInfo.CalendarTypeChanged);

		public override void InitControl()
		{
			base.InitControl();
			InitSlideInfo<CalendarSlideInfoControl>();
		}

		protected override void SaveData()
		{
			base.SaveData();
			Schedule.ApplySchedulePartitionContent(
				SchedulePartitionType.CustomCalendar,
				((CustomCalendar)EditedContent).Clone<CustomCalendar, CustomCalendar>());
		}

		public override MediaCalendar GetEditedCalendar()
		{
			return Schedule.GetSchedulePartitionContent<CustomCalendar>(
				SchedulePartitionType.CustomCalendar)
				.Clone<CustomCalendar, CustomCalendar>();
		}

		public override void GetHelp()
		{
			OpenHelp("calendar2");
		}
		#endregion

		#region Output Stuff
		public override void UpdateOutputFunctions()
		{
			var enable = IsOutputEnabled;

			retractableBarControl.Visible = true;
			MonthList.Enabled = true;
			pnTop.Visible = true;
			pnMain.Visible = true;
			pictureBoxNoData.Visible = false;

			Controller.Instance.Calendar1PowerPoint.Enabled = enable;
			Controller.Instance.Calendar1Pdf.Enabled = enable;
			Controller.Instance.Calendar1Preview.Enabled = enable;
			Controller.Instance.Calendar1Email.Enabled = enable;
		}
		#endregion
	}
}
