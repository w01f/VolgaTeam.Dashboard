using System;
using System.Collections.Generic;

namespace CommandCentral.Entities.SalesDepot
{
	internal class SalesDepotUserLibraries
	{
		public SalesDepotUserLibraries()
		{
			AppID = Guid.Empty;
			UserName = string.Empty;
			LocalLibraries = new List<string>();
			RemoteLibraries = new List<string>();
		}

		public Guid AppID { get; set; }
		public string UserName { get; set; }
		public List<string> LocalLibraries { get; private set; }
		public List<string> RemoteLibraries { get; private set; }
		public bool UseRemoteLibraries { get; set; }
	}
}