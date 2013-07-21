using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandCentral.CommonClasses
{
    class MechanicalItem
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public MechanicalItem()
        {
            this.Name = string.Empty;
            this.Value = string.Empty;
        }
    }
}
