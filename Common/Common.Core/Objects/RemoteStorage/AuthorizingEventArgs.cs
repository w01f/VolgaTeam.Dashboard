using System;

namespace Asa.Common.Core.Objects.RemoteStorage
{
	public class AuthorizingEventArgs : EventArgs
	{
		public bool Authorized { get; set; }
		public string AuthServer { get; private set; }
		public bool LightCheck { get; set; }

		public AuthorizingEventArgs(string authService)
		{
			Authorized = true;
			AuthServer = authService;
		}
	}
}
