using System;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	public interface ISettingsDataControl : ISettingsControl
	{
		event EventHandler<SettingsChangedEventArgs> DataChanged;
	}
}
