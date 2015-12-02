using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Asa.Bar.App.AdSalesResourcesService;
using Asa.Bar.App.Forms;
using Asa.Core.Common;

namespace Asa.Bar.App.Authorization
{
	class AuthManager
	{
		public StorageFile SettingsFile { get; private set; }
		public AuthSettings Settings { get; private set; }

		public void Init()
		{
			SettingsFile = new StorageFile(new[]
			{
				FileStorageManager.LocalFilesFolderName,
				AppProfileManager.Instance.AppName,
				"Credentials.xml"
			});
			SettingsFile.AllocateParentFolder();

			Settings = AuthSettings.Load();
		}

		public void Auth(AuthorizingEventArgs authArgs)
		{
			if (Settings.HasCredentials)
			{
				FormStart.SetTitle("Checking credentials...", "*This should not take long…");
				authArgs.Authorized = IsAuthorized(authArgs.AuthServer, Settings.Login, Settings.GetPassword());
				if (!authArgs.Authorized)
					File.Delete(SettingsFile.LocalPath);
			}
			else
			{
				FormStart.CloseProgress();
				using (var authForm = new FormLogin())
				{
					authForm.SetSiteUrl(authArgs.AuthServer);
					authForm.Logining += (o, e) =>
					{
						e.Accepted = IsAuthorized(authArgs.AuthServer, e.Login, e.Password);
						if (e.Accepted)
						{
							Settings.Login = e.Login;
							Settings.SetPassword(e.Password);
							Settings.Save();
						}
					};
					authArgs.Authorized = authForm.ShowDialog() == DialogResult.OK;
				}
				FormStart.ShowProgress();
			}
		}

		private bool IsAuthorized(string url, string login, string password)
		{
			var authorized = false;
			var thread = new Thread(() =>
			{
				try
				{
					var client = new AdSalesDataControllerService
					{
						Url = String.Format("http://{0}/AdSalesData/quote?ws=1", url)
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
