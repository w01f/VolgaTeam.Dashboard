using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandCentral.CommonClasses
{
    class SalesDepotUserLibraries
    {
        public Guid AppID { get; set; }
        public string UserName { get; set; }
        public List<string> Libraries { get; private set; }

        public SalesDepotUserLibraries()
        {
            this.AppID = Guid.Empty;
            this.UserName = string.Empty;
            this.Libraries = new List<string>();
        }
    }
}
