using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Asa.Browser.Controls.BusinessClasses.Objects;
using Asa.Browser.Controls.Controls;
using Asa.Browser.Single.Configuration;
using Asa.Browser.Single.InteropClasses;
using Asa.Browser.Single.Properties;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.Floater;
using DevComponents.DotNetBar;

namespace Asa.Browser.Single
{
    class SingleSiteContainerControl : SiteBundleControl
    {
        private Image _splashLogo = Resources.ProgressLogo;
        private Image _floaterLogo = Resources.FloaterLogo;

        public override PowerPointSingletonProcessor PowerPointSingleton => new BrowserPowerPointSingleton();
        public override Form MainForm => FormMain.Instance;
        public override Image SplashLogo => _splashLogo;

        public SingleSiteContainerControl()
        {
            comboBoxItemSites.SelectedIndexChanged += OnSelectedSiteChanged;
        }

        private void OnSelectedSiteChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBoxItem;
            if (!(comboBox?.SelectedItem is SingleSiteSettings selectedSiteSettings)) return;

            var splashLogoPath = Path.Combine(AppSettingsManager.Instance.AppFolderPath, $"splash_{selectedSiteSettings.BrowserId}.png");
            _splashLogo = File.Exists(splashLogoPath) ? Image.FromFile(splashLogoPath) : Resources.ProgressLogo;

            var floaterLogoPath = Path.Combine(AppSettingsManager.Instance.AppFolderPath, $"floater_{selectedSiteSettings.BrowserId}.png");
            _floaterLogo = File.Exists(floaterLogoPath) ? Image.FromFile(floaterLogoPath) : Resources.FloaterLogo;

            FormMain.Instance.labelItemAppTitle.Text = selectedSiteSettings.StatusBarTitle;
            FormMain.Instance.labelItemUrl.Text = selectedSiteSettings.BaseUrl;

            FormMain.Instance.itemContainerStatusBarActionButtons.RecalcSize();
            FormMain.Instance.barBottom.RecalcLayout();
        }

        public override void ShowFloater(FloaterRequestedEventArgs args)
        {
            AppManager.Instance.ShowFloater(_floaterLogo, args.AfterShow, args.AfterBack);
        }

        public override bool CheckPowerPointRunning(Action afterRun)
        {
            return BrowserPowerPointSingleton.Instance.CheckPowerPointRunning(_floaterLogo, afterRun);
        }

        public override void UpdateMainStatusBarInfo() { }
    }
}
