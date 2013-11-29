using DevComponents.DotNetBar;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses
{
	public class WeeklyScheduleControl : ScheduleSectionControl
	{
		public WeeklyScheduleControl()
			: base()
		{
			laTotalPeriodsTitle.Text = "Total Weeks:";
		}
		public override ScheduleSection ScheduleSection
		{
			get { return _localSchedule.WeeklySchedule; }
		}
		public override ButtonItem OptionsButton
		{
			get { return Controller.Instance.WeeklyScheduleOptions; }
		}
		public override ButtonItem ThemeButton
		{
			get { return Controller.Instance.WeeklyScheduleTheme; }
		}
	}
}
