using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using ListManager = NewBizWiz.Core.Dashboard.ListManager;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.Summary
{
	[ToolboxItem(false)]
	public partial class SummaryInputItemControl : UserControl
	{
		private bool _loading;
		public SummaryItem Data { get; set; }

		public event EventHandler<EventArgs> ItemDeleted;
		public event EventHandler<EventArgs> ItemUp;
		public event EventHandler<EventArgs> ItemDown;
		public event EventHandler<EventArgs> DataChanged;
		public event EventHandler<EventArgs> InvestmentChanged;

		public SummaryInputItemControl()
		{
			InitializeComponent();
			Dock = DockStyle.Top;
			comboBoxEditItem.Properties.Items.Clear();
			comboBoxEditItem.Properties.Items.AddRange(ListManager.Instance.SimpleSummaryLists.Details);

			comboBoxEditItem.MouseUp += Utilities.Instance.Editor_MouseUp;
			comboBoxEditItem.MouseDown += Utilities.Instance.Editor_MouseDown;
			comboBoxEditItem.Enter += Utilities.Instance.Editor_Enter;
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
			OutputItem = new SummaryOutputItemControl();
			DataChanged += (o, e) => { Data.Commited = true; };
		}

		public SummaryOutputItemControl OutputItem { get; set; }

		public void LoadData()
		{
			_loading = true;
			ckItem.Checked = Data.ShowValue;
			ckDetails.Checked = Data.ShowDescription;
			ckMonthly.Checked = Data.ShowMonthly;
			ckTotal.Checked = Data.ShowTotal;

			comboBoxEditItem.EditValue = Data.Value;
			memoEditDetails.EditValue = Data.Description;
			spinEditMonthly.EditValue = Data.Monthly;
			spinEditTotal.EditValue = Data.Total;

			UpdateItemNumber();
			_loading = false;
		}

		public void UpdateItemNumber()
		{
			laNumber.Text = (Data.Order + 1).ToString();
		}

		public void UpdateOutputItem()
		{
			OutputItem.MonthlyVisible = ckMonthly.Checked && spinEditMonthly.EditValue != null;
			OutputItem.TotalVisible = ckTotal.Checked && spinEditTotal.EditValue != null;
			OutputItem.ItemVisible = ckItem.Checked;
			OutputItem.DetailsVisible = ckDetails.Checked;
			OutputItem.MonthlyValue = spinEditMonthly.Value.ToString("$#,##0.00");
			OutputItem.ToatlValue = spinEditTotal.Value.ToString("$#,##0.00");
			if (comboBoxEditItem.EditValue != null)
				OutputItem.ItemValue = comboBoxEditItem.EditValue.ToString();
			else
				OutputItem.ItemValue = string.Empty;
			if (!String.IsNullOrEmpty(ItemDetail))
			{
				OutputItem.DetailsValue = memoEditDetails.EditValue.ToString().Replace(Environment.NewLine, "; ");
				OutputItem.pnDetails.Visible = true;
			}
			else
			{
				OutputItem.DetailsValue = string.Empty;
				OutputItem.pnDetails.Visible = false;
			}
			OutputItem.Height = (String.IsNullOrEmpty(ItemDetail) ? OutputItem.pnHeader.Height : (OutputItem.pnHeader.Height + OutputItem.pnDetails.Height)) + 40;
		}

		private void pbDelete_Click(object sender, EventArgs e)
		{
			if (ItemDeleted != null)
				ItemDeleted(this, EventArgs.Empty);
			UpdateItemNumber();
		}

		private void pbUp_Click(object sender, EventArgs e)
		{
			if (ItemUp != null)
				ItemUp(this, EventArgs.Empty);
			UpdateItemNumber();
		}

		private void pbDown_Click(object sender, EventArgs e)
		{
			if (ItemDown != null)
				ItemDown(this, EventArgs.Empty);
			UpdateItemNumber();
		}

		private void ckMonthly_CheckedChanged(object sender, EventArgs e)
		{
			spinEditMonthly.Enabled = ckMonthly.Checked;
			if (!_loading)
			{
				spinEditMonthly.Value = ckMonthly.Checked ? spinEditMonthly.Value : 0;
				Data.ShowMonthly = ckMonthly.Checked;
				if (InvestmentChanged != null)
					InvestmentChanged(this, EventArgs.Empty);
				if (DataChanged != null)
					DataChanged(this, EventArgs.Empty);
			}
		}

		private void ckTotal_CheckedChanged(object sender, EventArgs e)
		{
			spinEditTotal.Enabled = ckTotal.Checked;
			if (!_loading)
			{
				spinEditTotal.Value = ckTotal.Checked ? spinEditTotal.Value : 0;
				Data.ShowTotal = ckTotal.Checked;
				if (InvestmentChanged != null)
					InvestmentChanged(this, EventArgs.Empty);
				if (DataChanged != null)
					DataChanged(this, EventArgs.Empty);
			}
		}

		private void ckItem_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxEditItem.Enabled = ckItem.Checked;
			if (!_loading)
			{
				comboBoxEditItem.EditValue = ckItem.Checked ? comboBoxEditItem.EditValue : null;
				Data.ShowValue = ckItem.Checked;
				if (DataChanged != null)
					DataChanged(this, EventArgs.Empty);
			}
		}

		private void ckDetails_CheckedChanged(object sender, EventArgs e)
		{
			memoEditDetails.Enabled = ckDetails.Checked;
			if (!_loading)
			{
				memoEditDetails.EditValue = ckDetails.Checked ? memoEditDetails.EditValue : null;
				Data.ShowDescription = ckDetails.Checked;
				if (DataChanged != null)
					DataChanged(this, EventArgs.Empty);
			}
		}

		private void comboBoxEditItem_EditValueChanged(object sender, EventArgs e)
		{
			if (!_loading)
			{
				Data.Value = comboBoxEditItem.EditValue != null ? comboBoxEditItem.EditValue.ToString() : null;
				if (InvestmentChanged != null)
					InvestmentChanged(this, EventArgs.Empty);
				if (DataChanged != null)
					DataChanged(this, EventArgs.Empty);
			}
		}

		private void memoEditDetails_EditValueChanged(object sender, EventArgs e)
		{
			if (!_loading)
			{
				Data.Description = memoEditDetails.EditValue != null ? memoEditDetails.EditValue.ToString() : null;
				if (DataChanged != null)
					DataChanged(this, EventArgs.Empty);
			}
		}

		private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
		{
			if (!_loading)
			{
				Data.Monthly = spinEditMonthly.Value;
				if (InvestmentChanged != null)
					InvestmentChanged(this, EventArgs.Empty);
				if (DataChanged != null)
					DataChanged(this, EventArgs.Empty);
			}
		}

		private void spinEditTotal_EditValueChanged(object sender, EventArgs e)
		{
			if (!_loading)
			{
				Data.Total = spinEditTotal.Value;
				if (InvestmentChanged != null)
					InvestmentChanged(this, EventArgs.Empty);
				if (DataChanged != null)
					DataChanged(this, EventArgs.Empty);
			}
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
			get { return Data.ShowMonthly; }
		}

		public bool ShowTotal
		{
			get { return Data.ShowTotal; }
		}

		public bool ShowValueOutput
		{
			get { return Data.ShowValue & OutputItem.ItemChecked; }
		}

		public bool ShowDescriptionOutput
		{
			get { return Data.ShowDescription & OutputItem.DetailsChecked; }
		}

		public bool ShowMonthlyOutput
		{
			get { return ShowMonthly & OutputItem.MonthlyChecked; }
		}

		public bool ShowTotalOutput
		{
			get { return ShowTotal & OutputItem.TotalChecked; }
		}

		public string ItemTitle
		{
			get { return Data.Value; }
		}

		public string OutputItemTitle
		{
			get { return !String.IsNullOrEmpty(ItemTitle) && ShowValueOutput ? ItemTitle : String.Empty; }
		}

		public string ItemDetail
		{
			get { return !String.IsNullOrEmpty(Data.Description) && Data.ShowDescription ? Data.Description : String.Empty; }
		}

		public string ItemDetailOutput
		{
			get { return !String.IsNullOrEmpty(ItemDetail) && OutputItem.DetailsChecked ? ItemDetail : String.Empty; }
		}

		public decimal MonthlyValue
		{
			get { return Data.ShowMonthly ? Data.Monthly : 0; }
		}

		public decimal TotalValue
		{
			get { return Data.ShowTotal ? Data.Total : 0; }
		}

		public decimal? OutputMonthlyValue
		{
			get { return ckMonthly.Checked && OutputItem.MonthlyChecked ? MonthlyValue : (decimal?)null; }
		}

		public decimal? OutputTotalValue
		{
			get { return ckTotal.Checked &&  OutputItem.TotalChecked ? TotalValue : (decimal?)null; }
		}

		public bool Complited
		{
			get { return Data.ShowValue && !String.IsNullOrEmpty(ItemTitle); }
		}
		#endregion
	}
}