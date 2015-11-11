using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Asa.CommonGUI.Common;
using Asa.Core.Common;
using Asa.Core.Dashboard;

namespace Asa.Dashboard.TabHomeForms
{
	[ToolboxItem(false)]
	public partial class SimpleSummaryItemControl : UserControl
	{
		private readonly SimpleSummaryItemContainer _parent;
		private int _itemNumber;

		public SimpleSummaryItemControl(SimpleSummaryItemContainer parent)
		{
			InitializeComponent();
			Dock = DockStyle.Top;
			AppManager.Instance.SetClickEventHandler(this);
			_parent = parent;
			if ((CreateGraphics()).DpiX > 96)
			{
				laTotal.Font = new Font(laTotal.Font.FontFamily, laTotal.Font.Size - 2, laTotal.Font.Style);
			}
			if (FormMain.Instance != null)
			{
				textEditItem.MouseUp += TextEditorsHelper.Editor_MouseUp;
				textEditItem.MouseDown += TextEditorsHelper.Editor_MouseDown;
				textEditItem.Enter += TextEditorsHelper.Editor_Enter;
				spinEditMonthly.MouseUp += TextEditorsHelper.Editor_MouseUp;
				spinEditMonthly.MouseDown += TextEditorsHelper.Editor_MouseDown;
				spinEditMonthly.Enter += TextEditorsHelper.Editor_Enter;
				spinEditTotal.MouseUp += TextEditorsHelper.Editor_MouseUp;
				spinEditTotal.MouseDown += TextEditorsHelper.Editor_MouseDown;
				spinEditTotal.Enter += TextEditorsHelper.Editor_Enter;
				memoEditDetails.MouseUp += TextEditorsHelper.Editor_MouseUp;
				memoEditDetails.MouseDown += TextEditorsHelper.Editor_MouseDown;
				memoEditDetails.Enter += TextEditorsHelper.Editor_Enter;
			}
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
			ckItem.Checked = itemState.ShowValue;
			ckDetails.Checked = itemState.ShowDescription;
			ckMonthly.Checked = itemState.ShowMonthly;
			ckTotal.Checked = itemState.ShowTotal;

			textEditItem.EditValue = !string.IsNullOrEmpty(itemState.Value) ? itemState.Value : null;
			memoEditDetails.EditValue = !string.IsNullOrEmpty(itemState.Description) ? itemState.Description : null;
			spinEditMonthly.EditValue = itemState.ShowMonthly ? itemState.Monthly : null;
			spinEditTotal.EditValue = itemState.ShowTotal ? itemState.Total : null;
		}

		private void pbDelete_Click(object sender, EventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you sure you want to delete this product?") == DialogResult.Yes)
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
			if (!TabHomeMainPage.Instance.SlideSimpleSummary.AllowToSave) return;
			spinEditMonthly.EditValue = ckMonthly.Checked ? spinEditMonthly.EditValue : null;
			TabHomeMainPage.Instance.SlideSimpleSummary.SettingsNotSaved = true;
		}

		private void ckTotal_CheckedChanged(object sender, EventArgs e)
		{
			spinEditTotal.Enabled = ckTotal.Checked;
			if (!TabHomeMainPage.Instance.SlideSimpleSummary.AllowToSave) return;
			spinEditTotal.EditValue = ckTotal.Checked ? spinEditTotal.EditValue : null;
			TabHomeMainPage.Instance.SlideSimpleSummary.SettingsNotSaved = true;
		}

		private void ckItem_CheckedChanged(object sender, EventArgs e)
		{
			textEditItem.Enabled = ckItem.Checked;
			if (!TabHomeMainPage.Instance.SlideSimpleSummary.AllowToSave) return;
			TabHomeMainPage.Instance.SlideSimpleSummary.UpdateOutputState();
			TabHomeMainPage.Instance.SlideSimpleSummary.SettingsNotSaved = true;
		}

		private void ckDetails_CheckedChanged(object sender, EventArgs e)
		{
			memoEditDetails.Enabled = ckDetails.Checked;
			if (TabHomeMainPage.Instance.SlideSimpleSummary.AllowToSave)
				TabHomeMainPage.Instance.SlideSimpleSummary.SettingsNotSaved = true;
		}

		private void textEditItem_EditValueChanged(object sender, EventArgs e)
		{
			if (!TabHomeMainPage.Instance.SlideSimpleSummary.AllowToSave) return;
			TabHomeMainPage.Instance.SlideSimpleSummary.UpdateTotalItems();
			TabHomeMainPage.Instance.SlideSimpleSummary.UpdateOutputState();
			TabHomeMainPage.Instance.SlideSimpleSummary.SettingsNotSaved = true;
		}

		private void memoEditDetails_EditValueChanged(object sender, EventArgs e)
		{
			if (!TabHomeMainPage.Instance.SlideSimpleSummary.AllowToSave) return;
			TabHomeMainPage.Instance.SlideSimpleSummary.UpdateTotalItems();
			TabHomeMainPage.Instance.SlideSimpleSummary.SettingsNotSaved = true;
		}

		private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
		{
			if (!TabHomeMainPage.Instance.SlideSimpleSummary.AllowToSave) return;
			TabHomeMainPage.Instance.SlideSimpleSummary.UpdateTotalItems();
			TabHomeMainPage.Instance.SlideSimpleSummary.UpdateTotalValues();
			TabHomeMainPage.Instance.SlideSimpleSummary.SettingsNotSaved = true;
		}

		private void spinEditTotal_EditValueChanged(object sender, EventArgs e)
		{
			if (!TabHomeMainPage.Instance.SlideSimpleSummary.AllowToSave) return;
			TabHomeMainPage.Instance.SlideSimpleSummary.UpdateTotalItems();
			TabHomeMainPage.Instance.SlideSimpleSummary.UpdateTotalValues();
			TabHomeMainPage.Instance.SlideSimpleSummary.SettingsNotSaved = true;
		}

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion

		#region Output Stuff
		public bool ShowMonthly
		{
			get { return ckMonthly.Checked && spinEditMonthly.EditValue != null; }
		}

		public bool ShowTotal
		{
			get { return ckTotal.Checked && spinEditTotal.EditValue != null; }
		}

		public bool ShowValue
		{
			get { return ckItem.Checked; }
		}

		public bool ShowDescription
		{
			get { return ckDetails.Checked; }
		}

		public string ItemTitle
		{
			get { return textEditItem.EditValue != null && ckItem.Checked ? textEditItem.EditValue.ToString() : string.Empty; }
		}

		public string ItemDetail
		{
			get { return memoEditDetails.EditValue != null && ckDetails.Checked ? memoEditDetails.EditValue.ToString() : string.Empty; }
		}

		public decimal? MonthlyValue
		{
			get { return (decimal?)spinEditMonthly.EditValue; }
		}

		public decimal? TotalValue
		{
			get { return (decimal?)spinEditTotal.EditValue; }
		}

		public bool Complited
		{
			get { return !ckItem.Checked || !string.IsNullOrEmpty(ItemTitle); }
		}
		#endregion
	}
}