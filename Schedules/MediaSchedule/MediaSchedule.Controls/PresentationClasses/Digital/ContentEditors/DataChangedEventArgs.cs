using System;

namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	public class DataChangedEventArgs : EventArgs
	{
		public DigitalEditorType ChangedEditorType { get; set; }
	}
}
