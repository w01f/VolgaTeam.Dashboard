using System;

namespace Asa.Common.Core.Objects.Output
{
	public class SlideSettingsChangingEventArgs : EventArgs
	{
		public bool Cancel { get; set; }

		public SlideSettingsChangingEventArgs()
		{
			Cancel = false;
		}
	}
}
