using Asa.Core.AdSchedule;
using Asa.Core.OnlineSchedule;

namespace Asa.AdSchedule.Controls.PresentationClasses.OutputClasses
{
	public interface ISettingsContainer
	{
		SlideBulletsState SlideBulletsState { get; }
		SlideHeaderState SlideHeaderState { get; }
		DigitalLegend DigitalLegend { get; }
		bool SettingsNotSaved { get; set; }
	}
}