using System;
using DevComponents.DotNetBar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.ScheduleControls
{
	public class MonthlyScheduleControl : ScheduleSectionControl
	{
		public MonthlyScheduleControl()
			: base()
		{
			_helpKey = SpotTitle.ToLower();
			laTotalPeriodsTitle.Text = String.Format("Total {0}s:", SpotTitle);
			checkEditEmptySports.Text = String.Format(checkEditEmptySports.Text, String.Format("{0}s:", SpotTitle));
		}

		protected override sealed string SpotTitle
		{
			get { return "Month"; }
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
		public override RibbonBar QuarterBar
		{
			get { return Controller.Instance.MonthlyScheduleQuarterBar; }
		}
		public override ButtonItem QuarterButton
		{
			get { return Controller.Instance.MonthlyScheduleQuarterButton; }
		}
		public override SlideType SlideType
		{
			get { return MediaMetaData.Instance.DataType == MediaDataType.TV ? SlideType.TVMonthlySchedule : SlideType.RadioMonthlySchedule; }
		}
	}
}
