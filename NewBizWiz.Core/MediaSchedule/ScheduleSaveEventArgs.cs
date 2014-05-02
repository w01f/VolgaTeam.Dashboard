using System;

namespace NewBizWiz.Core.MediaSchedule
{
	public class ScheduleSaveEventArgs : Common.ScheduleSaveEventArgs
	{
		public ScheduleSaveEventArgs(bool quickSave, bool updateDigital)
			: base(quickSave)
		{
			UpdateDigital = updateDigital;
		}

		public bool UpdateDigital { get; set; }
	}
}
