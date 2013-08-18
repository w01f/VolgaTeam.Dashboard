using System;

namespace NewBizWiz.Core.AdSchedule
{
	public class ExportEventArgs : EventArgs
	{
		public ExportEventArgs(Schedule sourceSchedule)
		{
			SourceSchedule = sourceSchedule;
		}

		public Schedule SourceSchedule { get; private set; }
	}
}