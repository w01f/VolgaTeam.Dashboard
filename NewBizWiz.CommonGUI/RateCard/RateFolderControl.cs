using System.ComponentModel;
using System.Windows.Forms;

namespace Asa.CommonGUI.RateCard
{
	[ToolboxItem(false)]
	public partial class RateFolderControl : UserControl
	{
		public RateFolderControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}