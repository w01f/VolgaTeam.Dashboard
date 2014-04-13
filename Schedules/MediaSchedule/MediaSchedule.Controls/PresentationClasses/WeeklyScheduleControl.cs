using System;
using DevComponents.DotNetBar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses
{
	public class WeeklyScheduleControl : ScheduleSectionControl
	{
		public WeeklyScheduleControl()
			: base()
		{
			_helpKey = "week";
			laTotalPeriodsTitle.Text = "Total Weeks:";
			checkEditEmptySports.Text = String.Format(checkEditEmptySports.Text, "Weeks");
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
		public override ButtonItem PowerPointButton
		{
			get { return Controller.Instance.WeeklySchedulePowerPoint; }
		}
		public override ButtonItem EmailButton
		{
			get { return Controller.Instance.WeeklyScheduleEmail; }
		}
		public override ButtonItem PreviewButton
		{
			get { return Controller.Instance.WeeklySchedulePreview; }
		}
		public override ButtonItem QuarterButton
		{
			get { return Controller.Instance.WeeklyScheduleQuarter; }
		}
		public override SlideType SlideType
		{
			get { return MediaMetaData.Instance.DataType == MediaDataType.TV ? SlideType.TVWeeklySchedule : SlideType.RadioWeeklySchedule; }
		}
	}
}
