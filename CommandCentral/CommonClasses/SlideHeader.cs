using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandCentral.CommonClasses
{
    public class SlideHeader
    {
        public string Value { get; set; }
        public bool IsDefault { get; set; }

        public SlideHeader()
        {
            this.Value = string.Empty;
            this.IsDefault = false;
        }
    }
}
