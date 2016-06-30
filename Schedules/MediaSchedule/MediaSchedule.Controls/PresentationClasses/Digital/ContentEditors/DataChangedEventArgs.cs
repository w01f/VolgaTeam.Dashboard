using System;

namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	public class DataChangedEventArgs : EventArgs
	{
		public DigitalSectionType ChangedSectionType { get; set; }
	}
}
