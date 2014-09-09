using System;
using System.Linq;
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

		public override void LoadSchedule(bool quickLoad)
		{
			base.LoadSchedule(quickLoad);
			if (_localSchedule.SelectedSpotType == SpotType.Month)
			{
				Controller.Instance.UpdateOutputTabs(_localSchedule.MonthlySchedule.Programs.Any(p => p.TotalSpots > 0));
				Controller.Instance.UpdateCalendarTabs(false);
			}
		}

		public override void CloneProgram(int sourceIndex, bool fullClone)
		{
			base.CloneProgram(sourceIndex, fullClone);
			if (_localSchedule.SelectedSpotType == SpotType.Month)
			{
				Controller.Instance.UpdateOutputTabs(_localSchedule.MonthlySchedule.Programs.Any(p => p.TotalSpots > 0));
				Controller.Instance.UpdateCalendarTabs(false);
			}
		}

		public override void AddProgram_Click(object sender, EventArgs e)
		{
			base.AddProgram_Click(sender, e);
			if (_localSchedule.SelectedSpotType == SpotType.Month)
			{
				Controller.Instance.UpdateOutputTabs(_localSchedule.MonthlySchedule.Programs.Any(p => p.TotalSpots > 0));
				Controller.Instance.UpdateCalendarTabs(false);
			}
		}

		public override void DeleteProgram_Click(object sender, EventArgs e)
		{
			base.DeleteProgram_Click(sender, e);
			if (_localSchedule.SelectedSpotType == SpotType.Month)
			{
				Controller.Instance.UpdateOutputTabs(_localSchedule.MonthlySchedule.Programs.Any(p => p.TotalSpots > 0));
				Controller.Instance.UpdateCalendarTabs(false);
			}
		}

		protected override void ScheduleSection_DataChanged(object sender, EventArgs e)
		{
			base.ScheduleSection_DataChanged(sender, e);
			if (_localSchedule.SelectedSpotType == SpotType.Month)
			{
				Controller.Instance.UpdateOutputTabs(_localSchedule.MonthlySchedule.Programs.Any(p => p.TotalSpots > 0));
				Controller.Instance.UpdateCalendarTabs(false);
			}
		}
	}
}
