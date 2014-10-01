using System;
using System.Linq;
using DevComponents.DotNetBar;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.ScheduleControls
{
	public sealed class WeeklyScheduleControl : RegularScheduleSectionControl
	{
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

		public override void LoadSchedule(bool quickLoad)
		{
			base.LoadSchedule(quickLoad);
			if (_localSchedule.SelectedSpotType == SpotType.Week)
			{
				Controller.Instance.UpdateOutputTabs(ScheduleSection.Programs.Any(p => p.TotalSpots > 0));
				Controller.Instance.UpdateCalendarTabs(ScheduleSection.Programs.Any(p => p.TotalSpots > 0));
			}
		}

		public override void CloneProgram(int sourceIndex, bool fullClone)
		{
			base.CloneProgram(sourceIndex, fullClone);
			if (_localSchedule.SelectedSpotType == SpotType.Week)
			{
				Controller.Instance.UpdateOutputTabs(ScheduleSection.Programs.Any(p => p.TotalSpots > 0));
				Controller.Instance.UpdateCalendarTabs(ScheduleSection.Programs.Any(p => p.TotalSpots > 0));
			}
		}

		public override void AddProgram_Click(object sender, EventArgs e)
		{
			base.AddProgram_Click(sender, e);
			if (_localSchedule.SelectedSpotType == SpotType.Week)
			{
				Controller.Instance.UpdateOutputTabs(ScheduleSection.Programs.Any(p => p.TotalSpots > 0));
				Controller.Instance.UpdateCalendarTabs(ScheduleSection.Programs.Any(p => p.TotalSpots > 0));
			}
		}

		public override void DeleteProgram_Click(object sender, EventArgs e)
		{
			base.DeleteProgram_Click(sender, e);
			if (_localSchedule.SelectedSpotType == SpotType.Week)
			{
				Controller.Instance.UpdateOutputTabs(ScheduleSection.Programs.Any(p => p.TotalSpots > 0));
				Controller.Instance.UpdateCalendarTabs(ScheduleSection.Programs.Any(p => p.TotalSpots > 0));
			}
		}

		protected override void ScheduleSection_DataChanged(object sender, EventArgs e)
		{
			base.ScheduleSection_DataChanged(sender, e);
			if (_localSchedule.SelectedSpotType == SpotType.Week)
			{
				Controller.Instance.UpdateOutputTabs(ScheduleSection.Programs.Any(p => p.TotalSpots > 0));
				Controller.Instance.UpdateCalendarTabs(ScheduleSection.Programs.Any(p => p.TotalSpots > 0));
			}
		}
	}
}
