using System;
using DevComponents.DotNetBar;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses
{
	public class MonthlyScheduleControl : ScheduleSectionControl
	{
		public MonthlyScheduleControl()
			: base()
		{
			laTotalPeriodsTitle.Text = "Total Months:";
			checkEditEmptySports.Text = String.Format(checkEditEmptySports.Text, "Months");
		}

		public override ScheduleSection ScheduleSection
		{
			get { return _localSchedule.MonthlySchedule; }
		}
		public override ButtonItem OptionsButton
		{
			get { return Controller.Instance.MonthlyScheduleOptions; }
		}
		public override ButtonItem ThemeButton
		{
			get { return Controller.Instance.MonthlyScheduleTheme; }
		}
	}
}
