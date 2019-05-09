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

            using (var form = AppManager.Instance.Settings.GrayConnectConfig.UseGrayConnect ?
                (Form)new FormLoginGrayConnect() :
                new FormLogin())
            {
                var formLogin = (IFormLogin)form;

                formLogin.SetSiteUrl(authArgs.AuthServer);
                formLogin.Logining += (o, e) =>
                {
                    e.Accepted = IsAuthorized(authArgs.AuthServer, e.Login, e.Password);
                    if (e.Accepted)
                    {
                        SiteCredentialsManager.Instance.Settings.Login = e.Login;
                        SiteCredentialsManager.Instance.Settings.SetPassword(e.Password);
                        SiteCredentialsManager.Instance.Settings.Save();
                    }
                };

                authArgs.Authorized = form.ShowDialog() == DialogResult.OK;
            }

            FormStart.ShowProgress();
        }
    }
}
