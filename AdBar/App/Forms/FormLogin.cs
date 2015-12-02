using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Asa.Bar.App.Configuration;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;

namespace Asa.Bar.App.Forms
{
	public partial class FormLogin : MetroForm
	{
		private const string ErrorTextFormat = "<span align=\"center\"><font color=\"#ED1C24\">{0}</font></span>";

		public event EventHandler<LoginEventArgs> Logining;

		public FormLogin()
		{
			InitializeComponent();
		}

		public void SetSiteUrl(string siteUrl)
		{
			Text = String.Format(Text, siteUrl);
			labelXSiteCheck.Text = String.Format(labelXSiteCheck.Text, siteUrl);
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			pnInfo.BackColor = Color.White;

			var supportEmailConfig = new SuportEmailConfig();
			supportEmailConfig.Load();
			labelXForgotPassword.Text = String.Format(labelXForgotPassword.Text, String.Join(";", supportEmailConfig.Emails));
		}

		private void OnUrlLabelClick(object sender, MarkupLinkClickEventArgs e)
		{
			Process.Start(e.HRef);
		}

		private void OnOKClick(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(textBoxXUser.Text) ||
				String.IsNullOrEmpty(textBoxXPassword.Text))
			{
				ShowError("Type user name or email and password");
				return;
			}
			ResetError();
			ShowProgress();

			var loginArgs = new LoginEventArgs(textBoxXUser.Text, textBoxXPassword.Text);
			Logining(this, loginArgs);

			HideProgress();

			if (loginArgs.Accepted)
			{
				DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				ShowError("User name, email or password are wrong");
			}
		}

		private void ShowProgress()
		{
			circularProgress.IsRunning = true;
			circularProgress.Visible = true;
		}

		private void HideProgress()
		{
			circularProgress.Visible = false;
			circularProgress.IsRunning = false;
		}

		private void ShowError(string text)
		{
			labelXErrorText.Text = String.Format(ErrorTextFormat, text);
			labelXErrorText.Visible = true;
		}

		private void ResetError()
		{
			circularProgress.IsRunning = true;
			labelXErrorText.Text = String.Empty;
		}
	}

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
