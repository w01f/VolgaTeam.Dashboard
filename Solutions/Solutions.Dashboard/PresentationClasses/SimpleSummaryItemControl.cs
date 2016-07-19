using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Solutions.Dashboard.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;

namespace Asa.Solutions.Dashboard.PresentationClasses
{
	[ToolboxItem(false)]
	public partial class SimpleSummaryItemControl : UserControl
	{
		private bool _allowToSave;
		private readonly SimpleSummaryItemContainer _parent;
		private int _itemNumber;

		public event EventHandler<EventArgs> Changed;

		public SimpleSummaryItemControl(SimpleSummaryItemContainer parent)
		{
			InitializeComponent();
			Dock = DockStyle.Top;
			_parent = parent;
			if ((CreateGraphics()).DpiX > 96)
			{
				laTotal.Font = new Font(laTotal.Font.FontFamily, laTotal.Font.Size - 2, laTotal.Font.Style);
			}
			textEditItem.EnableSelectAll();
			spinEditMonthly.EnableSelectAll();
			spinEditTotal.EnableSelectAll();
			memoEditDetails.EnableSelectAll();

			pbUp.Buttonize();
			pbDown.Buttonize();
			pbDelete.Buttonize();
		}

		public int ItemNumber
		{
			get { return _itemNumber; }
			set
			{
				_itemNumber = value;
				laNumber.Text = _itemNumber.ToString();
			}
		}

		public void LoadSavedState(SimpleSummaryItemState itemState)
		{
			_allowToSave = false;

			ckItem.Checked = itemState.ShowValue;
			ckDetails.Checked = itemState.ShowDescription;
			ckMonthly.Checked = itemState.ShowMonthly;
			ckTotal.Checked = itemState.ShowTotal;

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
			spinEditMonthly.Enabled = ckMonthly.Checked;
			if (!_allowToSave) return;
			spinEditMonthly.EditValue = ckMonthly.Checked ? spinEditMonthly.EditValue : null;
			Changed?.Invoke(this, EventArgs.Empty);
		}

		private void ckTotal_CheckedChanged(object sender, EventArgs e)
		{
			spinEditTotal.Enabled = ckTotal.Checked;
			if (!_allowToSave) return;
			spinEditTotal.EditValue = ckTotal.Checked ? spinEditTotal.EditValue : null;
			Changed?.Invoke(this, EventArgs.Empty);
		}

		private void ckItem_CheckedChanged(object sender, EventArgs e)
		{
			textEditItem.Enabled = ckItem.Checked;
			if (!_allowToSave) return;
			Changed?.Invoke(this, EventArgs.Empty);
		}

		private void ckDetails_CheckedChanged(object sender, EventArgs e)
		{
			memoEditDetails.Enabled = ckDetails.Checked;
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
		public bool ShowMonthly => ckMonthly.Checked && spinEditMonthly.EditValue != null;

		public bool ShowTotal => ckTotal.Checked && spinEditTotal.EditValue != null;

		public bool ShowValue => ckItem.Checked;

		public bool ShowDescription => ckDetails.Checked;

		public string ItemTitle => textEditItem.EditValue != null && ckItem.Checked ? textEditItem.EditValue.ToString() : string.Empty;

		public string ItemDetail => memoEditDetails.EditValue != null && ckDetails.Checked ? memoEditDetails.EditValue.ToString() : string.Empty;

		public decimal? MonthlyValue => (decimal?)spinEditMonthly.EditValue;

		public decimal? TotalValue => (decimal?)spinEditTotal.EditValue;

		public bool Complited => !ckItem.Checked || !string.IsNullOrEmpty(ItemTitle);
		#endregion
	}
}