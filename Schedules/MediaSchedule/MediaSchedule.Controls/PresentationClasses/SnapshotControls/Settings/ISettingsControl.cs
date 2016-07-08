using Asa.Common.GUI.RetractableBar;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings
{
	public interface ISettingsControl
	{
		int Order { get; }
		bool IsAvailable { get; }
		ButtonInfo BarButton { get; }
		SnapshotSettingsType SettingsType { get; }
	}
}
