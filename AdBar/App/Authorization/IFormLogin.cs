using System;

namespace Asa.Bar.App.Authorization
{
    public interface IFormLogin
    {
        event EventHandler<LoginEventArgs> Logining;
        void SetSiteUrl(string siteUrl);
    }
}