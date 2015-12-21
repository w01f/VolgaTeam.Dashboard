using System.IO;
using System.Windows.Forms;
using Asa.Bar.App.Forms;
using Asa.Core.Common;

namespace Asa.Bar.App.Authorization
{
	class AdBarAuthManager : AuthManager
	{
		public override void Auth(AuthorizingEventArgs authArgs)
		{
			base.Auth(authArgs);
			if (authArgs.Authorized) return;
			if (SettingsFile.ExistsLocal())
				File.Delete(SettingsFile.LocalPath);
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
}
