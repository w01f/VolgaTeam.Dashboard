using System;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	public class SettingsChangedEventArgs : EventArgs
	{
		public bool UpdateGridColums { get; set; }
		public ScheduleSettingsType ChangedSettingsType { get; set; }
	}
}
