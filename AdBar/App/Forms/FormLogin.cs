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
			if ((CreateGraphics()).DpiX > 96)
			{
				labelXErrorText.Font = new Font(labelXErrorText.Font.FontFamily, labelXErrorText.Font.Size - 2, labelXErrorText.Font.Style);
				labelXForgotPassword.Font = new Font(labelXForgotPassword.Font.FontFamily, labelXForgotPassword.Font.Size - 2, labelXForgotPassword.Font.Style);
				labelXMainTitle.Font = new Font(labelXMainTitle.Font.FontFamily, labelXMainTitle.Font.Size - 2, labelXMainTitle.Font.Style);
				labelXPasswordDescription.Font = new Font(labelXPasswordDescription.Font.FontFamily, labelXPasswordDescription.Font.Size - 2, labelXPasswordDescription.Font.Style);
				labelXPasswordTitle.Font = new Font(labelXPasswordTitle.Font.FontFamily, labelXPasswordTitle.Font.Size - 2, labelXPasswordTitle.Font.Style);
				labelXSiteCheck.Font = new Font(labelXSiteCheck.Font.FontFamily, labelXSiteCheck.Font.Size - 2, labelXSiteCheck.Font.Style);
				labelXUserDescription.Font = new Font(labelXUserDescription.Font.FontFamily, labelXUserDescription.Font.Size - 2, labelXUserDescription.Font.Style);
				labelXUserTitle.Font = new Font(labelXUserTitle.Font.FontFamily, labelXUserTitle.Font.Size - 2, labelXUserTitle.Font.Style);
			}
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

		private void OnEnterKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Return)
				OnOKClick(sender, e);
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
