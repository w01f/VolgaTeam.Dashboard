using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Business.Solutions.Dashboard.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using DevExpress.Skins;

namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	[ToolboxItem(false)]
	public partial class SimpleSummaryItemControl : UserControl
	{
		private bool _allowToSave = true;
		private readonly SimpleSummaryItemContainer _parent;
		private int _itemNumber;

		public event EventHandler<EventArgs> Changed;

		public SimpleSummaryItemControl(SimpleSummaryItemContainer parent)
		{
			InitializeComponent();
			Dock = DockStyle.Top;
			_parent = parent;
			textEditItem.EnableSelectAll();
			spinEditMonthly.EnableSelectAll();
			spinEditTotal.EnableSelectAll();
			memoEditDetails.EnableSelectAll();


			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);

			simpleLabelItemNumber.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemNumber.MaxSize, scaleFactor);
			simpleLabelItemNumber.MinSize = RectangleHelper.ScaleSize(simpleLabelItemNumber.MinSize, scaleFactor);
			layoutControlItemUp.MaxSize = RectangleHelper.ScaleSize(layoutControlItemUp.MaxSize, scaleFactor);
			layoutControlItemUp.MinSize = RectangleHelper.ScaleSize(layoutControlItemUp.MinSize, scaleFactor);
			layoutControlItemDown.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDown.MaxSize, scaleFactor);
			layoutControlItemDown.MinSize = RectangleHelper.ScaleSize(layoutControlItemDown.MinSize, scaleFactor);
			layoutControlItemTitleToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTitleToggle.MaxSize, scaleFactor);
			layoutControlItemTitleToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemTitleToggle.MinSize, scaleFactor);
			layoutControlItemTitleValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTitleValue.MaxSize, scaleFactor);
			layoutControlItemTitleValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemTitleValue.MinSize, scaleFactor);
			layoutControlItemMonthlyToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemMonthlyToggle.MaxSize, scaleFactor);
			layoutControlItemMonthlyToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemMonthlyToggle.MinSize, scaleFactor);
			layoutControlItemMonthlyValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemMonthlyValue.MaxSize, scaleFactor);
			layoutControlItemMonthlyValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemMonthlyValue.MinSize, scaleFactor);
			layoutControlItemTotalToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTotalToggle.MaxSize, scaleFactor);
			layoutControlItemTotalToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemTotalToggle.MinSize, scaleFactor);
			layoutControlItemTotalValue.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTotalValue.MaxSize, scaleFactor);
			layoutControlItemTotalValue.MinSize = RectangleHelper.ScaleSize(layoutControlItemTotalValue.MinSize, scaleFactor);
			layoutControlItemDetailsToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDetailsToggle.MaxSize, scaleFactor);
			layoutControlItemDetailsToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemDetailsToggle.MinSize, scaleFactor);
			layoutControlItemDelete.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDelete.MaxSize, scaleFactor);
			layoutControlItemDelete.MinSize = RectangleHelper.ScaleSize(layoutControlItemDelete.MinSize, scaleFactor);
		}

		public int ItemNumber
		{
			get => _itemNumber;
			set
			{
				_itemNumber = value;
				simpleLabelItemNumber.Text = String.Format("<size=+4><b>{0}</b></size>", _itemNumber);
			}
		}

		public void LoadSavedState(SimpleSummaryItemState itemState)
		{
			_allowToSave = false;

			checkEditItem.Checked = itemState.ShowValue;
			checkEditDetails.Checked = itemState.ShowDescription;
			checkEditMonthly.Checked = itemState.ShowMonthly;
			checkEditTotal.Checked = itemState.ShowTotal;

			textEditItem.EditValue = !string.IsNullOrEmpty(itemState.Value) ? itemState.Value : null;
			memoEditDetails.EditValue = !string.IsNullOrEmpty(itemState.Description) ? itemState.Description : null;
			spinEditMonthly.EditValue = itemState.ShowMonthly ? itemState.Monthly : null;
			spinEditTotal.EditValue = itemState.ShowTotal ? itemState.Total : null;

			_allowToSave = true;
		}

		private void pbDelete_Click(object sender, EventArgs e)
		{
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Are you sure you want to delete this product?") == DialogResult.Yes)
				_parent.DeleteItem(_itemNumber);
		}

		private void pbUp_Click(object sender, EventArgs e)
		{
			_parent.UpItem(_itemNumber);
		}

		private void pbDown_Click(object sender, EventArgs e)
		{
			_parent.DownItem(_itemNumber);
		}

		private void ckMonthly_CheckedChanged(object sender, EventArgs e)
		{
			spinEditMonthly.Enabled = checkEditMonthly.Checked;
			if (!_allowToSave) return;
			spinEditMonthly.EditValue = checkEditMonthly.Checked ? spinEditMonthly.EditValue : null;
			Changed?.Invoke(this, EventArgs.Empty);
		}

		private void ckTotal_CheckedChanged(object sender, EventArgs e)
		{
			spinEditTotal.Enabled = checkEditTotal.Checked;
			if (!_allowToSave) return;
			spinEditTotal.EditValue = checkEditTotal.Checked ? spinEditTotal.EditValue : null;
			Changed?.Invoke(this, EventArgs.Empty);
		}

		private void ckItem_CheckedChanged(object sender, EventArgs e)
		{
			textEditItem.Enabled = checkEditItem.Checked;
			if (!_allowToSave) return;
			Changed?.Invoke(this, EventArgs.Empty);
		}

		private void ckDetails_CheckedChanged(object sender, EventArgs e)
		{
			memoEditDetails.Enabled = checkEditDetails.Checked;
			if (_allowToSave)
				Changed?.Invoke(this, EventArgs.Empty);
		}

		private void textEditItem_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			Changed?.Invoke(this, EventArgs.Empty);
		}

		private void memoEditDetails_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			Changed?.Invoke(this, EventArgs.Empty);
		}

		private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			Changed?.Invoke(this, EventArgs.Empty);
		}

		private void spinEditTotal_EditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			Changed?.Invoke(this, EventArgs.Empty);
		}

		#region Output Stuff
		public bool ShowMonthly => checkEditMonthly.Checked && spinEditMonthly.EditValue != null;

		public bool ShowTotal => checkEditTotal.Checked && spinEditTotal.EditValue != null;

		public bool ShowValue => checkEditItem.Checked;

		public bool ShowDescription => checkEditDetails.Checked;

		public string ItemTitle => textEditItem.EditValue != null && checkEditItem.Checked ? textEditItem.EditValue.ToString() : string.Empty;

		public string ItemDetail => memoEditDetails.EditValue != null && checkEditDetails.Checked ? memoEditDetails.EditValue.ToString() : string.Empty;

		public decimal? MonthlyValue => (decimal?)spinEditMonthly.EditValue;

		public decimal? TotalValue => (decimal?)spinEditTotal.EditValue;

		public bool Complited => !checkEditItem.Checked || !string.IsNullOrEmpty(ItemTitle);
		#endregion
	}
}