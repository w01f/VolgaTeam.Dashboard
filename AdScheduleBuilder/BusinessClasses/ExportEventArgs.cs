using System;

namespace AdScheduleBuilder.BusinessClasses
{
    public class ExportEventArgs : EventArgs
    {
        public Schedule SourceSchedule { get; private set; }
        public bool BuildAdvanced { get; private set; }
        public bool BuildGraphic { get; private set; }
        public bool BuildSimple { get; private set; }

        public ExportEventArgs(Schedule sourceSchedule, bool buildAdvanced, bool buildGraphic, bool buildSimple)
        {
            this.SourceSchedule = sourceSchedule;
            this.BuildAdvanced = buildAdvanced;
            this.BuildGraphic = buildGraphic;
            this.BuildSimple = buildSimple;
        }
    }
}
