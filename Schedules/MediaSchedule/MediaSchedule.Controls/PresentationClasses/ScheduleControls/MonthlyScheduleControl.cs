using System;
using System.Linq;
using DevComponents.DotNetBar;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.ScheduleControls
{
	public class MonthlyScheduleControl : RegularScheduleSectionControl
	{
		public override ButtonItem ThemeButton
		{
			get { return Controller.Instance.MonthlyScheduleTheme; }
		}
		public override ButtonItem PowerPointButton
		{
			get { return Controller.Instance.MonthlySchedulePowerPoint; }
		}
		public override ButtonItem PdfButton
		{
			get { return Controller.Instance.MonthlySchedulePdf; }
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

		public override void LoadSchedule(bool quickLoad)
		{
			base.LoadSchedule(quickLoad);
			if (_localSchedule.SelectedSpotType == SpotType.Month)
			{
				Controller.Instance.UpdateOutputTabs(ScheduleSection.Programs.Any(p => p.TotalSpots > 0));
			}
		}

		public override void CloneProgram(int sourceIndex, bool fullClone)
		{
			base.CloneProgram(sourceIndex, fullClone);
			if (_localSchedule.SelectedSpotType == SpotType.Month)
			{
				Controller.Instance.UpdateOutputTabs(ScheduleSection.Programs.Any(p => p.TotalSpots > 0));
			}
		}

		public override void AddProgram_Click(object sender, EventArgs e)
		{
			base.AddProgram_Click(sender, e);
			if (_localSchedule.SelectedSpotType == SpotType.Month)
			{
				Controller.Instance.UpdateOutputTabs(ScheduleSection.Programs.Any(p => p.TotalSpots > 0));
			}
		}

		public override void DeleteProgram_Click(object sender, EventArgs e)
		{
			base.DeleteProgram_Click(sender, e);
			if (_localSchedule.SelectedSpotType == SpotType.Month)
			{
				Controller.Instance.UpdateOutputTabs(ScheduleSection.Programs.Any(p => p.TotalSpots > 0));
			}
		}

		protected override void ScheduleSection_DataChanged(object sender, EventArgs e)
		{
			base.ScheduleSection_DataChanged(sender, e);
			if (_localSchedule.SelectedSpotType == SpotType.Month)
			{
				Controller.Instance.UpdateOutputTabs(ScheduleSection.Programs.Any(p => p.TotalSpots > 0));
			}
		}
	}
}
