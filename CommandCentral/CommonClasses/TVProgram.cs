using System.Collections.Generic;

namespace CommandCentral.CommonClasses
{
    class TVProgram
    {
        public string Name { get; set; }
        public string Station { get; set; }
        public string Daypart { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public List<Demo> Demos { get; set; }

        public TVProgram()
        {
            this.Name = string.Empty;
            this.Station = string.Empty;
            this.Daypart = string.Empty;
            this.Day = string.Empty;
            this.Time = string.Empty;
            this.Demos = new List<Demo>();
        }
    }
}
