using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NewBizWiz.Dashboard.TabHomeForms.Dashboard
{
	[ToolboxItem(false)]
	public partial class DashboardDefault : UserControl
	{
		public DashboardDefault()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}