using System;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;

namespace Asa.Common.GUI.ToolForms
{
	public partial class FormSelectOutputItems : MetroForm
	{
		public FormSelectOutputItems()
		{
			InitializeComponent();

			checkedListBoxControlOutputItems.ItemHeight = (Int32)(checkedListBoxControlOutputItems.ItemHeight * Utilities.GetScaleFactor(CreateGraphics().DpiX).Height);
			layoutControlItemSelectAll.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSelectAll.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSelectAll.MinSize = RectangleHelper.ScaleSize(layoutControlItemSelectAll.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSelectCurrent.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSelectCurrent.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSelectCurrent.MinSize = RectangleHelper.ScaleSize(layoutControlItemSelectCurrent.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSelectNone.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSelectNone.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSelectNone.MinSize = RectangleHelper.ScaleSize(layoutControlItemSelectNone.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
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