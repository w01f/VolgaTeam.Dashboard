using System;

namespace NewBizWiz.Core.MediaSchedule
{
	public class ScheduleSaveEventArgs : Common.ScheduleSaveEventArgs
	{
		public ScheduleSaveEventArgs(bool quickSave, bool updateDigital, bool calendarTypeChanged)
			: base(quickSave)
		{
			UpdateDigital = updateDigital;
			CalendarTypeChanged = calendarTypeChanged;
		}

		public bool UpdateDigital { get; private set; }

		public bool CalendarTypeChanged { get; private set; }
	}
}
