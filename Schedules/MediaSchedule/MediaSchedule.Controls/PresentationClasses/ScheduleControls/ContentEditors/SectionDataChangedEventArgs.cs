using System;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	public class SectionDataChangedEventArgs : EventArgs
	{
		public bool SnapshotsChanged { get; set; }
		public bool OptionsSetsChanged { get; set; }
	}
}
