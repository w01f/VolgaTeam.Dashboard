using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NewBizWiz.Core.Common;

namespace NewBizWiz.CommonGUI.Summary
{
	[ToolboxItem(false)]
	public partial class SummaryCustomItemControl : UserControl, ISummaryItemControl
	{
		private bool _loading;
		public CustomSummaryItem Data { get; set; }

		public event EventHandler<EventArgs> DataChanged;
		public event EventHandler<EventArgs> InvestmentChanged;
		public event EventHandler<SummaryItemEventArgs> ItemPositionChanged;
		public event EventHandler<SummaryItemEventArgs> ItemDeleted;

		public SummaryCustomItemControl()
		{
			InitializeComponent();
			Dock = DockStyle.Top;
			textEditItem.MouseUp += Utilities.Instance.Editor_MouseUp;
			textEditItem.MouseDown += Utilities.Instance.Editor_MouseDown;
			textEditItem.Enter += Utilities.Instance.Editor_Enter;
			spinEditMonthly.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditMonthly.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditMonthly.Enter += Utilities.Instance.Editor_Enter;
			spinEditTotal.MouseUp += Utilities.Instance.Editor_MouseUp;
			spinEditTotal.MouseDown += Utilities.Instance.Editor_MouseDown;
			spinEditTotal.Enter += Utilities.Instance.Editor_Enter;
			memoEditDetails.MouseUp += Utilities.Instance.Editor_MouseUp;
			memoEditDetails.MouseDown += Utilities.Instance.Editor_MouseDown;
			memoEditDetails.Enter += Utilities.Instance.Editor_Enter;

			if ((CreateGraphics()).DpiX > 96)
			{
				laTotal.Font = new Font(laTotal.Font.FontFamily, laTotal.Font.Size - 2, laTotal.Font.Style);
			}
			DataChanged += (o, e) => { Data.Commited = true; };
		}

		public void LoadData()
		{
			_loading = true;
			ckItem.Checked = Data.ShowValue;
			ckDetails.Checked = Data.ShowDescription;
			ckMonthly.Checked = Data.ShowMonthly;
			ckTotal.Checked = Data.ShowTotal;

			textEditItem.EditValue = Data.Value;
			memoEditDetails.EditValue = Data.Description;
			spinEditMonthly.EditValue = Data.Monthly;
			spinEditTotal.EditValue = Data.Total;

			UpdateNumber();

			_loading = false;
		}

		public void UpdateNumber()
		{
			laNumber.Text = (Data.Order + 1).ToString();			
		}

		private void pbDelete_Click(object sender, EventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Are you sure you want to delete this product?") == DialogResult.Yes)
			if (ItemDeleted != null)
				ItemDeleted(this, new SummaryItemEventArgs(this));
		}

		private void pbUp_Click(object sender, EventArgs e)
		{
			Data.Order -= (decimal)1.5;
			if (ItemPositionChanged != null)
				ItemPositionChanged(this, new SummaryItemEventArgs(this));
		}

		private void pbDown_Click(object sender, EventArgs e)
		{
			Data.Order += (decimal)1.5;
			if (ItemPositionChanged != null)
				ItemPositionChanged(this, new SummaryItemEventArgs(this));
		}

		private void ckMonthly_CheckedChanged(object sender, EventArgs e)
		{
			spinEditMonthly.Enabled = ckMonthly.Checked;
			laMonthly.Enabled = ckMonthly.Checked;
			if (_loading) return;
			spinEditMonthly.Value = ckMonthly.Checked ? spinEditMonthly.Value : 0;
			Data.ShowMonthly = ckMonthly.Checked;
			if (InvestmentChanged != null)
				InvestmentChanged(this, EventArgs.Empty);
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void ckTotal_CheckedChanged(object sender, EventArgs e)
		{
			spinEditTotal.Enabled = ckTotal.Checked;
			laTotal.Enabled = ckTotal.Checked;
			if (_loading) return;
			spinEditTotal.Value = ckTotal.Checked ? spinEditTotal.Value : 0;
			Data.ShowTotal = ckTotal.Checked;
			if (InvestmentChanged != null)
				InvestmentChanged(this, EventArgs.Empty);
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void ckItem_CheckedChanged(object sender, EventArgs e)
		{
			textEditItem.Enabled = ckItem.Checked;
			if (_loading) return;
			Data.ShowValue = ckItem.Checked;
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void textEditItem_EditValueChanged(object sender, EventArgs e)
		{
			if (_loading) return;
			Data.Value = textEditItem.EditValue as String;
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void ckDetails_CheckedChanged(object sender, EventArgs e)
		{
			memoEditDetails.Enabled = ckDetails.Checked;
			if (_loading) return;
			memoEditDetails.EditValue = ckDetails.Checked ? memoEditDetails.EditValue : null;
			Data.ShowDescription = ckDetails.Checked;
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void memoEditDetails_EditValueChanged(object sender, EventArgs e)
		{
			if (_loading) return;
			Data.Description = memoEditDetails.EditValue as String;
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
		{
			if (_loading) return;
			Data.Monthly = spinEditMonthly.Value;
			if (InvestmentChanged != null)
				InvestmentChanged(this, EventArgs.Empty);
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void spinEditTotal_EditValueChanged(object sender, EventArgs e)
		{
			if (_loading) return;
			Data.Total = spinEditTotal.Value;
			if (InvestmentChanged != null)
				InvestmentChanged(this, EventArgs.Empty);
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		#region Output Stuff
		public bool ShowMonthly
		{
			get { return Data.ShowMonthly; }
		}

		public bool ShowTotal
		{
			get { return Data.ShowTotal; }
		}

		public bool ShowValueOutput
		{
			get { return Data.ShowValue; }
		}

		public bool ShowDescriptionOutput
		{
			get { return Data.ShowDescription; }
		}

		public bool ShowMonthlyOutput
		{
			get { return ShowMonthly; }
		}

		public bool ShowTotalOutput
		{
			get { return ShowTotal; }
		}

		public string ItemTitle
		{
			get { return !String.IsNullOrEmpty(Data.Value) && Data.ShowValue ? Data.Value : String.Empty; }
		}

		public string ItemIcon
		{
			get { return String.Empty; }
		}

		public string OutputItemTitle
		{
			get { return !String.IsNullOrEmpty(ItemTitle) && ShowValueOutput ? ItemTitle : String.Empty; }
		}

		private string ItemDetail
		{
			get { return !String.IsNullOrEmpty(Data.Description) && Data.ShowDescription ? Data.Description : String.Empty; }
		}

		public string ItemDetailOutput
		{
			get { return !String.IsNullOrEmpty(ItemDetail) ? ItemDetail : String.Empty; }
		}

		public decimal? MonthlyValue
		{
			get { return Data.ShowMonthly ? Data.Monthly : null; }
		}

		public decimal? TotalValue
		{
			get { return Data.ShowTotal ? Data.Total : null; }
		}

		public decimal? OutputMonthlyValue
		{
			get { return ckMonthly.Checked ? MonthlyValue : null; }
		}

		public decimal? OutputTotalValue
		{
			get { return ckTotal.Checked ? TotalValue : null; }
		}

		public bool Complited
		{
			get { return Data.ShowValue && !String.IsNullOrEmpty(ItemTitle); }
		}
		#endregion

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
	}
}