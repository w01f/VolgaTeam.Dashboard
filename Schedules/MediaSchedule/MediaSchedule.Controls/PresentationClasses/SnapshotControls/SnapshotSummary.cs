using System.ComponentModel;
using DevExpress.Utils;
using DevExpress.XtraTab;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.SnapshotControls
{
	[ToolboxItem(false)]
	//public partial class SnapshotSummary : UserControl
	public sealed partial class SnapshotSummary : XtraTabPage
	{
		public SnapshotSummary()
		{
			InitializeComponent();
			ShowCloseButton = DefaultBoolean.False;
			Text = "Product Summary";
		}
	}
}
