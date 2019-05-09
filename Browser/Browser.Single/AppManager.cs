using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Browser.Single.Configuration;
using Asa.Browser.Single.Properties;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Floater;

namespace Asa.Browser.Single
{
    public class AppManager
    {
        private readonly FloaterManager _floater = new FloaterManager();

        public static AppManager Instance { get; } = new AppManager();
        private AppManager() { }

        public void RunApplication()
        {
            LicenseHelper.Register();
            AppSettingsManager.Instance.LoadSettings();
            ResourceManager.Instance.LoadResources();
            FormMain.Instance.InitForm();
            PopupMessageHelper.Instance.Title = FormMain.Instance.Text;
            PopupMessageHelper.Instance.MainForm = FormMain.Instance;
            Application.Run(FormMain.Instance);
        }

        public void ShowFloater(Image floaterLogo, Action afterShow, Action afterBack)
        {
            ShowFloater(FormMain.Instance, new FloaterRequestedEventArgs
            {
                Logo = floaterLogo,
                AfterShow = afterShow,
                AfterBack = afterBack
            });
        }

        public void ShowFloater(IFloaterSupportedForm sender, FloaterRequestedEventArgs e)
        {
            var afterBack = new Action<bool>(b =>
            {
                e.AfterBack?.Invoke();
                Utilities.ActivateForm(FormMain.Instance.Handle, b, false);
                Utilities.ActivateTaskbar();
            });
            _floater.ShowFloater(sender ?? FormMain.Instance, FormMain.Instance.Text, e.Logo ?? Resources.FloaterLogo, e.AfterShow, null, afterBack);
        }
    }
}
