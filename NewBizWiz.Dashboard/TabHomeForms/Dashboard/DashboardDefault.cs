using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Asa.Dashboard.TabHomeForms.Dashboard
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