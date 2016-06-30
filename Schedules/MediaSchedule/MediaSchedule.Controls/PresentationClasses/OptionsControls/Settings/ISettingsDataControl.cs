using System;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Settings
{
	public interface ISettingsDataControl : ISettingsControl
	{
		event EventHandler<SettingsChangedEventArgs> DataChanged;
	}
}
