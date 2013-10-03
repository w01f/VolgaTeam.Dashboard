using System;
using System.ComponentModel;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.Dashboard.InteropClasses;
using NewBizWiz.Dashboard.TabHomeForms.Dashboard;
using SettingsManager = NewBizWiz.Core.Dashboard.SettingsManager;

namespace NewBizWiz.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public partial class TabHomeOverviewControl : UserControl
	{
		private static TabHomeOverviewControl _instance;

		public AppManager.SingleParameterDelegate EnableOutput { get; set; }

		private TabHomeOverviewControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);

			Control dashboard = null;
			switch (SettingsManager.Instance.DashboardCode)
			{
				case "newspaper":
					dashboard = new DashboardPrint();
					break;
				case "tv":
					dashboard = new DashboardTV();
					break;
				case "radio":
					dashboard = new DashboardRadio();
					break;
				case "cable":
					dashboard = new DashboardCable();
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

		public void UpdateOutputState()
		{
			bool result = true;
			if (EnableOutput != null)
				EnableOutput(result);
		}

		public void Output()
		{
			AppManager.Instance.ShowFloater(null, () => DashboardPowerPointHelper.Instance.AppendCleanslate());
		}
	}
}