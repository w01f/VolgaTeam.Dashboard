using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandCentral.CommonClasses
{
    class DaypartRotation
    {
        public string Name { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public List<string> DemoValues { get; set; }

        public DaypartRotation()
        {
            this.Name = string.Empty;
            this.Day = string.Empty;
            this.Time = string.Empty;
            this.DemoValues = new List<string>();
        }
    }
}
