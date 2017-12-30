using System;
using System.Collections.Generic;

namespace CommandCentral.BusinessClasses.Entities.SalesDepot
{
	internal class SalesDepotUserLibraries
	{
		public Guid AppID { get; set; }
		public string UserName { get; set; }
		public List<string> LocalLibraries { get; }
		public List<string> RemoteLibraries { get; }
		public bool UseRemoteLibraries { get; set; }

		public SalesDepotUserLibraries()
		{
			AppID = Guid.Empty;
			UserName = string.Empty;
			LocalLibraries = new List<string>();
			RemoteLibraries = new List<string>();
		}
	}
}