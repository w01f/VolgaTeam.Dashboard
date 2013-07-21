using System.ComponentModel;
using System.Windows.Forms;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.RateCard
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