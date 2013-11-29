using System;

namespace NewBizWiz.Core.Common
{
	public class ScheduleSaveEventArgs : EventArgs
	{
		public ScheduleSaveEventArgs(bool quickSave)
		{
			QuickSave = quickSave;
		}

		public bool QuickSave { get; set; }
	}
}
