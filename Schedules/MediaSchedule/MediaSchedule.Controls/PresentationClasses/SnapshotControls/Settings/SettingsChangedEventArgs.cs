using System;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings
{
	public class SettingsChangedEventArgs : EventArgs
	{
		public SnapshotSettingsType ChangedSettingsType { get; set; }
	}
}
