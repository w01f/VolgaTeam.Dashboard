using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandCentral.CommonClasses
{
    class User
    {
        public string Station { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string SavedPresentationsFolder { get; set; }
        public string SyncTime { get; set; }
        public string SyncStartup { get; set; }
        public bool IsAdmin { get; set; }

        public User()
        {
            this.Station = string.Empty;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;
            this.SavedPresentationsFolder = string.Empty;
            this.SyncStartup = string.Empty;
            this.SyncTime = string.Empty;
        }
    }
}
