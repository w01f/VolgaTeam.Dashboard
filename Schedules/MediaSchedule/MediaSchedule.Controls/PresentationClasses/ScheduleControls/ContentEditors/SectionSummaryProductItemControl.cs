using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Section.Summary;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Summary;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	[ToolboxItem(false)]
	public partial class SectionSummaryProductItemControl : UserControl, ISummaryItemControl
	{
		private bool _loading;
		public CustomSummaryItem Data { get; set; }
		private ProductInfoSummaryItem ProductSummaryData => (ProductInfoSummaryItem)Data;

		public event EventHandler<EventArgs> DataChanged;
		public event EventHandler<EventArgs> InvestmentChanged;
		public event EventHandler<SummaryItemEventArgs> ItemPositionChanged;
		public event EventHandler<SummaryItemEventArgs> ItemDeleted;

		public SectionSummaryProductItemControl()
		{
			InitializeComponent();
			Dock = DockStyle.Top;
			textEditItem.EnableSelectAll();
			spinEditMonthly.EnableSelectAll();
			spinEditTotal.EnableSelectAll();

			if ((CreateGraphics()).DpiX > 96)
			{
				laTotal.Font = new Font(laTotal.Font.FontFamily, laTotal.Font.Size - 2, laTotal.Font.Style);
			}
			DataChanged += (o, e) => { Data.Commited = true; };
			toolTip.SetToolTip(buttonXImportMedia, String.Format("Import {0} Info", MediaMetaData.Instance.DataTypeString));
		}

		public virtual void LoadData()
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
			UpdatePositionButtons();

			_loading = false;
		}

		public void Release()
		{
			DataChanged = null;
			InvestmentChanged = null;
			ItemPositionChanged = null;
			ItemDeleted = null;
			Data = null;
		}

		protected virtual void RaiseDescriptionChanged()
		{
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		protected void RaiseDataChanged()
		{
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		public void UpdateNumber()
		{
			laNumber.Text = (Data.Order + 1).ToString();

		}

		public void UpdatePositionButtons()
		{
			buttonXUp.Enabled = !ProductSummaryData.IsFirstInCollection;
			buttonXDown.Enabled = !ProductSummaryData.IsLastInCollection;
		}

		private void OnDeleteItemClick(object sender, EventArgs e)
		{
			if (PopupMessageHelper.Instance.ShowWarningQuestion("Are you sure you want to delete this product?") == DialogResult.Yes)
				ItemDeleted?.Invoke(this, new SummaryItemEventArgs(this));
		}

		private void OnUpItemClick(object sender, EventArgs e)
		{
			Data.Order -= (decimal)1.5;
			ItemPositionChanged?.Invoke(this, new SummaryItemEventArgs(this));
		}

		private void OnDownItemClick(object sender, EventArgs e)
		{
			Data.Order += (decimal)1.5;
			ItemPositionChanged?.Invoke(this, new SummaryItemEventArgs(this));
		}

		private void OnImportDigitalClick(object sender, EventArgs e)
		{
			if (PopupMessageHelper.Instance.ShowWarningQuestion(
				String.Format("Are you SURE you want to Write Over this data{0}With Digital Marketing Info?", Environment.NewLine))
				!= DialogResult.Yes) return;

			ProductSummaryData.DataSourceType = SummaryItemDataSourceType.Digital;
			ProductSummaryData.Synchronize();

			_loading = true;
			ckDetails.Checked = true;
			memoEditDetails.EditValue = Data.Description;
			_loading = false;
		}

		private void OnImportMediaClick(object sender, EventArgs e)
		{
			if (PopupMessageHelper.Instance.ShowWarningQuestion(
				String.Format("Are you SURE you want to Write Over this data{1}With {0} Schedule Info?", MediaMetaData.Instance.DataTypeString, Environment.NewLine))
				!= DialogResult.Yes) return;

			ProductSummaryData.DataSourceType = SummaryItemDataSourceType.Media;
			ProductSummaryData.Synchronize();

			_loading = true;
			ckDetails.Checked = true;
			memoEditDetails.EditValue = Data.Description;
			_loading = false;
		}

		private void OnResetItemClick(object sender, EventArgs e)
		{
			if(!String.IsNullOrEmpty(Data.Description))
				if (PopupMessageHelper.Instance.ShowWarningQuestion(
					"Are you SURE you want to DELETE the data for this product?")
					!= DialogResult.Yes) return;

			ProductSummaryData.ResetToDefault();
			ckDetails.Checked = false;
			memoEditDetails.EditValue = Data.Description;
		}

		private void ckMonthly_CheckedChanged(object sender, EventArgs e)
		{
			spinEditMonthly.Enabled = ckMonthly.Checked;
			laMonthly.Enabled = ckMonthly.Checked;
			if (_loading) return;
			spinEditMonthly.Value = ckMonthly.Checked ? spinEditMonthly.Value : 0;
			Data.ShowMonthly = ckMonthly.Checked;
			InvestmentChanged?.Invoke(this, EventArgs.Empty);
			RaiseDataChanged();
		}

		private void ckTotal_CheckedChanged(object sender, EventArgs e)
		{
			spinEditTotal.Enabled = ckTotal.Checked;
			laTotal.Enabled = ckTotal.Checked;
			if (_loading) return;
			spinEditTotal.Value = ckTotal.Checked ? spinEditTotal.Value : 0;
			Data.ShowTotal = ckTotal.Checked;
			InvestmentChanged?.Invoke(this, EventArgs.Empty);
			RaiseDataChanged();
		}

		private void ckItem_CheckedChanged(object sender, EventArgs e)
		{
			textEditItem.Enabled = ckItem.Checked;
			if (_loading) return;
			Data.ShowValue = ckItem.Checked;
			RaiseDescriptionChanged();
		}

		private void textEditItem_EditValueChanged(object sender, EventArgs e)
		{
			if (_loading) return;
			Data.Value = textEditItem.EditValue as String;
			RaiseDescriptionChanged();
		}

		private void ckDetails_CheckedChanged(object sender, EventArgs e)
		{
			memoEditDetails.Enabled = ckDetails.Checked;
			if (_loading) return;
			memoEditDetails.EditValue = ckDetails.Checked ? memoEditDetails.EditValue : null;
			Data.ShowDescription = ckDetails.Checked;
			if (!ckDetails.Checked)
				ProductSummaryData.DataSourceType = SummaryItemDataSourceType.None;
			RaiseDescriptionChanged();
		}

		private void memoEditDetails_EditValueChanged(object sender, EventArgs e)
		{
			if (_loading) return;
			ProductSummaryData.DataSourceType = SummaryItemDataSourceType.None;
			Data.Description = memoEditDetails.EditValue as String;
			RaiseDescriptionChanged();
		}

		private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
		{
			if (_loading) return;
			Data.Monthly = spinEditMonthly.Value;
			InvestmentChanged?.Invoke(this, EventArgs.Empty);
			RaiseDataChanged();
		}

		private void spinEditTotal_EditValueChanged(object sender, EventArgs e)
		{
			if (_loading) return;
			Data.Total = spinEditTotal.Value;
			InvestmentChanged?.Invoke(this, EventArgs.Empty);
			RaiseDataChanged();
		}

		#region Output Stuff
		public bool ShowMonthly => Data.ShowMonthly;

		public bool ShowTotal => Data.ShowTotal;

		public bool ShowValueOutput => Data.ShowValue;

		public bool ShowDescriptionOutput => Data.ShowDescription;

		public bool ShowMonthlyOutput => ShowMonthly;

		public bool ShowTotalOutput => ShowTotal;

		public string ItemTitle => !String.IsNullOrEmpty(Data.Value) && Data.ShowValue ? Data.Value : String.Empty;

		public string ItemIcon => String.Empty;

		public string OutputItemTitle => !String.IsNullOrEmpty(ItemTitle) && ShowValueOutput ? ItemTitle : String.Empty;

		private string ItemDetail => !String.IsNullOrEmpty(Data.Description) && Data.ShowDescription ? Data.Description : String.Empty;

		public string ItemDetailOutput => !String.IsNullOrEmpty(ItemDetail) ? ItemDetail : String.Empty;

		public decimal? MonthlyValue => Data.ShowMonthly ? Data.Monthly : null;

		public decimal? TotalValue => Data.ShowTotal ? Data.Total : null;

		public decimal? OutputMonthlyValue => ckMonthly.Checked ? MonthlyValue : null;

		public decimal? OutputTotalValue => ckTotal.Checked ? TotalValue : null;

		public bool Complited => Data.ShowValue && !String.IsNullOrEmpty(ItemTitle);

		#endregion
	}
}