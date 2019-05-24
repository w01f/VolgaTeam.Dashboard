using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Asa.Bar.App.Authorization;
using Asa.Bar.App.Configuration;
using DevComponents.DotNetBar;

namespace Asa.Bar.App.Forms
{
    public partial class FormLoginGrayConnect : Form, IFormLogin
    {
        private const string ErrorTextFormat = "<span align=\"center\"><font color=\"#ED1C24\">{0}</font></span>";

        public event EventHandler<LoginEventArgs> Logining;

        public FormLoginGrayConnect()
        {
            InitializeComponent();

            var logoPath = Path.Combine(ResourceManager.Instance.AppRootFolderPath, "app_logo.png");
            if (File.Exists(logoPath))
            {
                var tempPath = Path.GetTempFileName();
                File.Copy(logoPath, tempPath, true);
                pictureBoxMainLogo.Image = Image.FromFile(tempPath);
            }

            labelXMainTitle.Text = String.Format(labelXMainTitle.Text, AppManager.Instance.Settings.GrayConnectConfig.FormTitle);

            labelXUserTitle.Text = String.Format(labelXUserTitle.Text, AppManager.Instance.Settings.GrayConnectConfig.LoginTitle);
            labelXUserDescription.Text = String.Format(labelXUserDescription.Text, AppManager.Instance.Settings.GrayConnectConfig.LoginDescription);

            labelXPasswordTitle.Text = String.Format(labelXPasswordTitle.Text, AppManager.Instance.Settings.GrayConnectConfig.PasswordTitle);
            labelXPasswordDescription.Text = String.Format(labelXPasswordDescription.Text, AppManager.Instance.Settings.GrayConnectConfig.PasswordDescription);

            labelXLoginStepsDescription.Text = String.Format(labelXLoginStepsDescription.Text, 
                AppManager.Instance.Settings.GrayConnectConfig.Step1Title, 
                AppManager.Instance.Settings.GrayConnectConfig.Step1Url, 
                AppManager.Instance.Settings.GrayConnectConfig.Step1UrlDescription, 
                AppManager.Instance.Settings.GrayConnectConfig.Step2Title, 
                AppManager.Instance.Settings.GrayConnectConfig.Step2Url, 
                AppManager.Instance.Settings.GrayConnectConfig.Step2UrlDescription, 
                AppManager.Instance.Settings.GrayConnectConfig.Step3Title, 
                AppManager.Instance.Settings.GrayConnectConfig.Step3Url, 
                AppManager.Instance.Settings.GrayConnectConfig.Step3UrlDescription, 
                AppManager.Instance.Settings.GrayConnectConfig.Step4Title, 
                AppManager.Instance.Settings.GrayConnectConfig.Step4Url, 
                AppManager.Instance.Settings.GrayConnectConfig.Step4UrlDescription);

            buttonXMainSite.Visible = AppManager.Instance.Settings.GrayConnectConfig.MainSiteVisible;
            buttonXMainSite.Text = String.Format(buttonXMainSite.Text, AppManager.Instance.Settings.GrayConnectConfig.MainSiteTitle);

            buttonXGrayConnect.Visible = AppManager.Instance.Settings.GrayConnectConfig.GrayConnectVisible;
            buttonXGrayConnect.Text = String.Format(buttonXGrayConnect.Text, AppManager.Instance.Settings.GrayConnectConfig.GrayConnectTitle);

            if ((CreateGraphics()).DpiX > 96)
            {
                labelXErrorText.Font = new Font(labelXErrorText.Font.FontFamily, labelXErrorText.Font.Size - 2, labelXErrorText.Font.Style);
                labelXMainTitle.Font = new Font(labelXMainTitle.Font.FontFamily, labelXMainTitle.Font.Size - 2, labelXMainTitle.Font.Style);
                labelXPasswordDescription.Font = new Font(labelXPasswordDescription.Font.FontFamily, labelXPasswordDescription.Font.Size - 2, labelXPasswordDescription.Font.Style);
                labelXPasswordTitle.Font = new Font(labelXPasswordTitle.Font.FontFamily, labelXPasswordTitle.Font.Size - 2, labelXPasswordTitle.Font.Style);
                labelXLoginStepsDescription.Font = new Font(labelXLoginStepsDescription.Font.FontFamily, labelXLoginStepsDescription.Font.Size - 2, labelXLoginStepsDescription.Font.Style);
                labelXUserDescription.Font = new Font(labelXUserDescription.Font.FontFamily, labelXUserDescription.Font.Size - 2, labelXUserDescription.Font.Style);
                labelXUserTitle.Font = new Font(labelXUserTitle.Font.FontFamily, labelXUserTitle.Font.Size - 2, labelXUserTitle.Font.Style);
                buttonXMainSite.Font = new Font(buttonXMainSite.Font.FontFamily, buttonXMainSite.Font.Size - 2, buttonXMainSite.Font.Style);
                buttonXGrayConnect.Font = new Font(buttonXGrayConnect.Font.FontFamily, buttonXGrayConnect.Font.Size - 2, buttonXGrayConnect.Font.Style);
                buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
                buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
            }
        }

        public void SetSiteUrl(string siteUrl)
        {
            Text = String.Format(Text, siteUrl);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            pnInfo.BackColor = Color.White;

            var supportEmailConfig = new SuportEmailConfig();
            supportEmailConfig.Load();
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

        private void OnMainSiteClick(object sender, EventArgs e)
        {
            Process.Start(AppManager.Instance.Settings.GrayConnectConfig.MainSiteUrl);
        }

        private void OnGrayConnectClick(object sender, EventArgs e)
        {
            Process.Start(AppManager.Instance.Settings.GrayConnectConfig.GrayConnectUrl);
        }
    }
}
