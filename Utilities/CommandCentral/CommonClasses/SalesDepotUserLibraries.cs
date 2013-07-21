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
        public List<string> LocalLibraries { get; private set; }
        public List<string> RemoteLibraries { get; private set; }
        public bool UseRemoteLibraries { get; set; }

        public SalesDepotUserLibraries()
        {
            this.AppID = Guid.Empty;
            this.UserName = string.Empty;
            this.LocalLibraries = new List<string>();
            this.RemoteLibraries = new List<string>();
        }
    }
}
