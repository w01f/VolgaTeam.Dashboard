using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.Summary
{
	[ToolboxItem(false)]
	public partial class SummaryInputItemControl : UserControl
	{
		private bool _loading;
		public SummaryItem Data { get; set; }

		public event EventHandler<EventArgs> DataChanged;
		public event EventHandler<EventArgs> InvestmentChanged;

		public SummaryInputItemControl()
		{
			InitializeComponent();
			Dock = DockStyle.Top;
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
			pictureBoxLogo.Image = Data.Parent is PrintProduct ? Properties.Resources.SummaryPrint : Properties.Resources.SummaryDigital;
			ckItem.Checked = Data.ShowValue;
			ckDetails.Checked = Data.ShowDescription;
			ckMonthly.Checked = Data.ShowMonthly;
			ckTotal.Checked = Data.ShowTotal;

			laTitle.Text = Data.Parent.SummaryTitle;
			memoEditDetails.EditValue = Data.Description;
			spinEditMonthly.EditValue = Data.Monthly;
			spinEditTotal.EditValue = Data.Total;

			_loading = false;
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
			laTitle.Enabled = ckItem.Checked;
			if (_loading) return;
			Data.ShowValue = ckItem.Checked;
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void ckDetails_CheckedChanged(object sender, EventArgs e)
		{
			memoEditDetails.Enabled = ckDetails.Checked;
			if (_loading) return;
			memoEditDetails.EditValue = ckDetails.Checked ? memoEditDetails.EditValue : Data.Parent.SummaryInfo;
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
			get { return Data.Parent.SummaryTitle; }
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
			get { return ckMonthly.Checked ? MonthlyValue : (decimal?)null; }
		}

		public decimal? OutputTotalValue
		{
			get { return ckTotal.Checked ? TotalValue : (decimal?)null; }
		}

		public bool Complited
		{
			get { return Data.ShowValue && !String.IsNullOrEmpty(ItemTitle); }
		}
		#endregion
	}
}