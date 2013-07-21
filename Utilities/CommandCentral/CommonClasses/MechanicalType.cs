using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandCentral.CommonClasses
{
    class MechanicalType
    {
        public string Name { get; set; }
        public List<MechanicalItem> Items { get; set; }

        public MechanicalType()
        {
            this.Name = string.Empty;
            this.Items = new List<MechanicalItem>();
        }
    }
}
