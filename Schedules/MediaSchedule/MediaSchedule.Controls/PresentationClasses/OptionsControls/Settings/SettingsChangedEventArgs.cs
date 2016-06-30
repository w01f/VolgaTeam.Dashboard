using System;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Settings
{
	public class SettingsChangedEventArgs : EventArgs
	{
		public OptionSettingsType ChangedSettingsType { get; set; }
	}
}
