using System;

namespace Asa.Media.Controls.PresentationClasses.Digital.Settings
{
	public class SettingsChangedEventArgs : EventArgs
	{
		public DigitalSettingsType ChangedSettingsType { get; set; }
	}
}
