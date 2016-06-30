using Asa.Common.GUI.RetractableBar;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Settings
{
	public interface ISettingsControl
	{
		int Order { get; }
		bool IsAvailable { get; }
		ButtonInfo BarButton { get; }
		OptionSettingsType SettingsType { get; }
	}
}
