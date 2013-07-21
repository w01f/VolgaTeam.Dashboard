using System;

namespace NewBizWiz.Core.AdSchedule
{
	public class ExportEventArgs : EventArgs
	{
		public ExportEventArgs(Schedule sourceSchedule, bool buildAdvanced, bool buildGraphic, bool buildSimple)
		{
			SourceSchedule = sourceSchedule;
			BuildAdvanced = buildAdvanced;
			BuildGraphic = buildGraphic;
			BuildSimple = buildSimple;
		}

		public Schedule SourceSchedule { get; private set; }
		public bool BuildAdvanced { get; private set; }
		public bool BuildGraphic { get; private set; }
		public bool BuildSimple { get; private set; }
	}
}