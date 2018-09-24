using System;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors
{
	public class SlideDescriptionChangedEventArgs : EventArgs
	{
		public SlideDescription SlideDescription { get; set; }

		public SlideDescriptionChangedEventArgs()
		{
			SlideDescription = new SlideDescription();
		}
	}
}
