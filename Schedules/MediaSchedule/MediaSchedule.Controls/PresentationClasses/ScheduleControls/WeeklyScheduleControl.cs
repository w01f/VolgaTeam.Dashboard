using System;
using System.Linq;
using DevComponents.DotNetBar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.ScheduleControls
{
	public sealed class WeeklyScheduleControl : ScheduleSectionControl
	{
		public WeeklyScheduleControl()
			: base()
		{
			_helpKey = SpotTitle.ToLower();
			laTotalPeriodsTitle.Text = String.Format("Total {0}s:", SpotTitle);
			checkEditEmptySports.Text = String.Format(checkEditEmptySports.Text, String.Format("{0}s:", SpotTitle));
		}

		protected override string SpotTitle
		{
			get { return "Week"; }
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
		public override RibbonBar QuarterBar
		{
			get { return Controller.Instance.WeeklyScheduleQuarterBar; }
		}
		public override ButtonItem QuarterButton
		{
			get { return Controller.Instance.WeeklyScheduleQuarterButton; }
		}
		public override SlideType SlideType
		{
			get { return MediaMetaData.Instance.DataType == MediaDataType.TV ? SlideType.TVWeeklySchedule : SlideType.RadioWeeklySchedule; }
		}

		public override void LoadSchedule(bool quickLoad)
		{
			base.LoadSchedule(quickLoad);
			Controller.Instance.UpdateOutputTabs(_localSchedule.WeeklySchedule.Programs.Any(p => p.TotalSpots > 0));
		}

		public override void CloneProgram(int sourceIndex, bool fullClone)
		{
			base.CloneProgram(sourceIndex, fullClone);
			Controller.Instance.UpdateOutputTabs(_localSchedule.WeeklySchedule.Programs.Any(p => p.TotalSpots > 0));
		}

		public override void AddProgram_Click(object sender, EventArgs e)
		{
			base.AddProgram_Click(sender, e);
			Controller.Instance.UpdateOutputTabs(_localSchedule.WeeklySchedule.Programs.Any(p => p.TotalSpots > 0));
		}

		public override void DeleteProgram_Click(object sender, EventArgs e)
		{
			base.DeleteProgram_Click(sender, e);
			Controller.Instance.UpdateOutputTabs(_localSchedule.WeeklySchedule.Programs.Any(p => p.TotalSpots > 0));
		}

		protected override void ScheduleSection_DataChanged(object sender, EventArgs e)
		{
			base.ScheduleSection_DataChanged(sender, e);
			Controller.Instance.UpdateOutputTabs(_localSchedule.WeeklySchedule.Programs.Any(p => p.TotalSpots > 0));
		}
	}
}
