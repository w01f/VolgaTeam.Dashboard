using System;
using DevComponents.DotNetBar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses
{
	public class MonthlyScheduleControl : ScheduleSectionControl
	{
		public MonthlyScheduleControl()
			: base()
		{
			_helpKey = "month";
			laTotalPeriodsTitle.Text = "Total Months:";
			checkEditEmptySports.Text = String.Format(checkEditEmptySports.Text, "Months");
			laScheduleName.Text = "Monthly Schedule";
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
		public override ButtonItem PowerPointButton
		{
			get { return Controller.Instance.MonthlySchedulePowerPoint; }
		}
		public override ButtonItem EmailButton
		{
			get { return Controller.Instance.MonthlyScheduleEmail; }
		}
		public override ButtonItem PreviewButton
		{
			get { return Controller.Instance.MonthlySchedulePreview; }
		}

		public override SlideType SlideType
		{
			get { return MediaMetaData.Instance.DataType == MediaDataType.TV ? SlideType.TVMonthlySchedule : SlideType.RadioMonthlySchedule; }
		}
	}
}
