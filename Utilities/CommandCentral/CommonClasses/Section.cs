using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandCentral.CommonClasses
{
    class Section
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }


        public Section()
        {
            this.Name = string.Empty;
            this.Abbreviation = string.Empty;
        }
    }
}
