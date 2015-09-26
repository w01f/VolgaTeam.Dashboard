using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Dashboard.TabHomeForms.Dashboard
{
	[ToolboxItem(false)]
	public partial class DashboardDefault : UserControl
	{
		public DashboardDefault()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			if (File.Exists(SettingsManager.Instance.DashboardDefaultLogoPath))
				pbSellerPoint.Image = Image.FromFile(SettingsManager.Instance.DashboardDefaultLogoPath);
		}
	}
}