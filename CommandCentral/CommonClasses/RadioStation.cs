using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandCentral.CommonClasses
{
    class RadioStation
    {
        public string Name { get; set; }
        public List<RadioProgram> Programs { get; set; }
        public List<DaypartRotation> DaypartRotatiions { get; set; }
        public List<Demo> Demos { get; set; }

        public RadioStation()
        {
            this.Name = string.Empty;
            this.Programs = new List<RadioProgram>();
            this.DaypartRotatiions = new List<DaypartRotation>();
            this.Demos = new List<Demo>();
        }
    }
}
