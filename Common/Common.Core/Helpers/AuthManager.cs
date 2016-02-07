using System;
using System.Threading;
using System.Windows.Forms;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Common.Core.Helpers
{
	public class AuthManager
	{
		public StorageFile SettingsFile { get; private set; }
		public AuthSettings Settings { get; private set; }

		public void Init()
		{
			SettingsFile = new StorageFile(new[]
			{
				FileStorageManager.LocalFilesFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"Credentials.xml"
			});
			SettingsFile.AllocateParentFolder();

			Settings = AuthSettings.Load(SettingsFile);
		}

		public virtual void Auth(AuthorizingEventArgs authArgs)
		{
			authArgs.Authorized = Settings.HasCredentials &&
				(authArgs.LightCheck ||
					FileStorageManager.Instance.UseLocalMode ||
					IsAuthorized(authArgs.AuthServer, Settings.Login, Settings.GetPassword()));
		}

		protected bool IsAuthorized(string url, string login, string password)
		{
			var authorized = false;
			var thread = new Thread(() =>
			{
				try
				{
					var client = new AdSalesDataControllerService.AdSalesDataControllerService
					{
						Url = String.Format("{0}/AdSalesData/quote?ws=1", url)
					};
					var sessionKey = client.getSessionKey(login, password);
					authorized = !String.IsNullOrEmpty(sessionKey);
				}
				catch { }
			});
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			return authorized;
		}
	}
}
