using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Common.Interfaces;
using Asa.Common.GUI.Common;

namespace Asa.Common.GUI.Summary
{
	[ToolboxItem(false)]
	public abstract partial class SummaryProductItemControl : UserControl, ISummaryItemControl
	{
		private bool _loading;
		public CustomSummaryItem Data { get; set; }

		protected ISummaryProduct Product
		{
			get { return (Data as ProductSummaryItem)?.Parent; }
		}

		protected abstract Image ItemLogo { get; }

		public event EventHandler<EventArgs> DataChanged;
		public event EventHandler<EventArgs> InvestmentChanged;
		public event EventHandler<SummaryItemEventArgs> ItemPositionChanged;

		public SummaryProductItemControl()
		{
			InitializeComponent();
			Dock = DockStyle.Top;
			memoEditDetails.MouseUp += TextEditorsHelper.Editor_MouseUp;
			memoEditDetails.MouseDown += TextEditorsHelper.Editor_MouseDown;
			memoEditDetails.Enter += TextEditorsHelper.Editor_Enter;
			DataChanged += (o, e) => { Data.Commited = true; };
		}

		public void LoadData()
		{
			_loading = true;
			pictureBoxLogo.Image = ItemLogo;
			ckItem.Checked = Data.ShowValue;
			ckDetails.Checked = Data.ShowDescription;

			laTitle.Text = Product.SummaryTitle;
			memoEditDetails.EditValue = Data.Description;

			_loading = false;
		}

		public void Release()
		{
			DataChanged = null;
			InvestmentChanged = null;
			ItemPositionChanged = null;
			Data = null;
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
			memoEditDetails.EditValue = ckDetails.Checked ? memoEditDetails.EditValue : Product.SummaryInfo;
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
			get { return Product.SummaryTitle; }
		}

		public abstract string ItemIcon { get; }
		
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
			get { return null; }
		}

		public decimal? OutputTotalValue
		{
			get { return null; }
		}

		public bool Complited
		{
			get { return Data.ShowValue; }
		}
		#endregion
	}
}