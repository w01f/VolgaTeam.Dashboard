using System.IO;
using System.Windows.Forms;
using Asa.Bar.App.Forms;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Bar.App.Authorization
{
	class AdBarAuthManager : AuthManager
	{
		public override void Auth(AuthorizingEventArgs authArgs)
		{
			base.Auth(authArgs);
			if (authArgs.Authorized) return;
			if (SiteCredentialsManager.Instance.SettingsFile.ExistsLocal())
				File.Delete(SiteCredentialsManager.Instance.SettingsFile.LocalPath);
			FormStart.CloseProgress();
			using (var authForm = new FormLogin())
			{
				authForm.SetSiteUrl(authArgs.AuthServer);
				authForm.Logining += (o, e) =>
				{
					e.Accepted = IsAuthorized(authArgs.AuthServer, e.Login, e.Password);
					if (e.Accepted)
					{
						SiteCredentialsManager.Instance.Settings.Login = e.Login;
						SiteCredentialsManager.Instance.Settings.SetPassword(e.Password);
						SiteCredentialsManager.Instance.Settings.Save();
					}
				};
				authArgs.Authorized = authForm.ShowDialog() == DialogResult.OK;
			}
			FormStart.ShowProgress();
		}
	}
}
