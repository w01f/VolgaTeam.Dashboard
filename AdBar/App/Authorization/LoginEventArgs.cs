using System;

namespace Asa.Bar.App.Authorization
{
    public class LoginEventArgs : EventArgs
    {
        public string Login { get; private set; }
        public string Password { get; private set; }
        public bool Accepted { get; set; }

        public LoginEventArgs(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
