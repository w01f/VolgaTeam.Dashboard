using System;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings
{
	public interface ISettingsDataControl : ISettingsControl
	{
		event EventHandler<SettingsChangedEventArgs> DataChanged;
	}
}
