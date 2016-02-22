using Asa.Common.GUI.RetractableBar;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	public interface ISettingsControl
	{
		int Order { get; }
		bool IsAvailable { get; }
		ButtonInfo BarButton { get; }
		ScheduleSettingsType SettingsType { get; }
	}
}
