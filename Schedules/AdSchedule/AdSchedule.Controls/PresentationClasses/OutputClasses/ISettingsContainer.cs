using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.OnlineSchedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses
{
	public interface ISettingsContainer
	{
		SlideBulletsState SlideBulletsState { get; }
		SlideHeaderState SlideHeaderState { get; }
		DigitalLegend DigitalLegend { get; }
		bool SettingsNotSaved { get; set; }
	}
}