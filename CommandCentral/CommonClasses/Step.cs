using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandCentral.CommonClasses
{
    public class Step
    {
        public string Value { get; set; }
        public int Position { get; set; }

        public Step()
        {
            this.Value = string.Empty;
            this.Position = -1;
        }
    }
}
