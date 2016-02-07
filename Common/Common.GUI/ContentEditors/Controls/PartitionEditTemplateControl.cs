using System.ComponentModel;
using System.Windows.Forms;

namespace Asa.Common.GUI.ContentEditors.Controls
{
	[ToolboxItem(false)]
	public partial class PartitionEditTemplateControl : UserControl
	{
		public PartitionEditTemplateControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}
	}
}
