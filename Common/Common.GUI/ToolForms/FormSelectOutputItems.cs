using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;

namespace Asa.Common.GUI.ToolForms
{
	public partial class FormSelectOutputItems : MetroForm
	{
		public FormSelectOutputItems()
		{
			InitializeComponent();
		}

		private void buttonXSelectAll_Click(object sender, System.EventArgs e)
		{
			checkedListBoxControlOutputItems.CheckAll();
		}

		private void buttonXSelectCurrent_Click(object sender, System.EventArgs e)
		{
			checkedListBoxControlOutputItems.UnCheckAll();
			var currentItem = buttonXSelectCurrent.Tag as CheckedListBoxItem;
			if (currentItem != null)
				currentItem.CheckState = CheckState.Checked;
		}

		private void buttonXSelectNone_Click(object sender, System.EventArgs e)
		{
			checkedListBoxControlOutputItems.UnCheckAll();
		}

		private void checkedListBoxControlProducts_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
		{
			buttonXContinue.Enabled = checkedListBoxControlOutputItems.CheckedItemsCount > 0;
		}
	}
}