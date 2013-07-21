using System;
using System.ComponentModel;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.Dashboard.TabHomeForms.Dashboard;
using SettingsManager = NewBizWiz.Core.Dashboard.SettingsManager;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public partial class TabHomeOverviewControl : UserControl
	{
		private static TabHomeOverviewControl _instance;

		private TabHomeOverviewControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);

			Control dashboard = null;
			switch (SettingsManager.Instance.DashboardCode)
			{
				case 1:
					dashboard = new DashboardCode1Control();
					break;
				case 2:
					dashboard = new DashboardCode2Control();
					break;
				case 3:
					dashboard = new DashboardCode3Control();
					break;
				case 4:
					dashboard = new DashboardCode4Control();
					break;
				case 5:
					dashboard = new DashboardCode5Control();
					break;
				case 6:
					dashboard = new DashboardCode6Control();
					break;
				case 7:
					dashboard = new DashboardCode7Control();
					break;
				case 8:
					dashboard = new DashboardCode8Control();
					break;
				case 9:
					dashboard = new DashboardCode9Control();
					break;
				case 10:
					dashboard = new DashboardCode10Control();
					break;
				case 11:
					dashboard = new DashboardCode11Control();
					break;
				case 12:
					dashboard = new DashboardCode12Control();
					break;
				case 13:
					dashboard = new DashboardCode13Control();
					break;
				case 14:
					dashboard = new DashboardCode14Control();
					break;
				case 15:
					dashboard = new DashboardCode15Control();
					break;
				case 16:
					dashboard = new DashboardCode16Control();
					break;
				case 17:
					dashboard = new DashboardCode17Control();
					break;
				case 18:
					dashboard = new DashboardCode18Control();
					break;
				case 30:
					dashboard = new DashboardCode30Control();
					break;
				case 31:
					dashboard = new DashboardCode31Control();
					break;
			}

			if (dashboard != null)
				pnMain.Controls.Add(dashboard);

			pbWatermark.Image = MasterWizardManager.Instance.Watermark;
			pbWatermark.BringToFront();
			laUserName.Text = Environment.UserName;
			pbVersion.Image = MasterWizardManager.Instance.Version;
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