using System;
using System.Windows.Forms;

namespace NewBizWizForm.TabHomeForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TabHomeOverviewControl : UserControl
    {
        private static TabHomeOverviewControl _instance;

        private TabHomeOverviewControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);

            Control dashboard = null;
            switch (ConfigurationClasses.SettingsManager.Instance.DashboardCode)
            { 
                case 1:
                    dashboard = new Dashboard.DashboardCode1Control();
                    break;
                case 2:
                    dashboard = new Dashboard.DashboardCode2Control();
                    break;
                case 3:
                    dashboard = new Dashboard.DashboardCode3Control();
                    break;
                case 4:
                    dashboard = new Dashboard.DashboardCode4Control();
                    break;
                case 5:
                    dashboard = new Dashboard.DashboardCode5Control();
                    break;
                case 6:
                    dashboard = new Dashboard.DashboardCode6Control();
                    break;
                case 7:
                    dashboard = new Dashboard.DashboardCode7Control();
                    break;
                case 8:
                    dashboard = new Dashboard.DashboardCode8Control();
                    break;
                case 9:
                    dashboard = new Dashboard.DashboardCode9Control();
                    break;
                case 10:
                    dashboard = new Dashboard.DashboardCode10Control();
                    break;
                case 11:
                    dashboard = new Dashboard.DashboardCode11Control();
                    break;
                case 12:
                    dashboard = new Dashboard.DashboardCode12Control();
                    break;
                case 13:
                    dashboard = new Dashboard.DashboardCode13Control();
                    break;
                case 14:
                    dashboard = new Dashboard.DashboardCode14Control();
                    break;
                case 15:
                    dashboard = new Dashboard.DashboardCode15Control();
                    break;
                case 16:
                    dashboard = new Dashboard.DashboardCode16Control();
                    break;
                case 17:
                    dashboard = new Dashboard.DashboardCode17Control();
                    break;
                case 30:
                    dashboard = new Dashboard.DashboardCode30Control();
                    break;
            }

            if (dashboard != null)
                pnMain.Controls.Add(dashboard);
            
            pbWatermark.Image = BusinessClasses.MasterWizardManager.Instance.Watermark;
            pbWatermark.BringToFront();
            laUserName.Text = Environment.UserName;
            pbVersion.Image = BusinessClasses.MasterWizardManager.Instance.Version;
        }

        public static TabHomeOverviewControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TabHomeOverviewControl();
                return _instance;
            }
        }
    }
}
