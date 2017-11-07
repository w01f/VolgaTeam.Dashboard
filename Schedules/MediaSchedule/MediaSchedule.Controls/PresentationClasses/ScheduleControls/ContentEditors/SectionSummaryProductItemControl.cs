using System;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Section.Summary;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.Common;
using Asa.Common.GUI.Summary;
using DevExpress.Skins;

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

			DataChanged += (o, e) => { Data.Commited = true; };
			toolTip.SetToolTip(buttonXImportMedia, String.Format("Import {0} Info", MediaMetaData.Instance.DataTypeString));

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
			layoutControlItemMedia.MaxSize = RectangleHelper.ScaleSize(layoutControlItemMedia.MaxSize, scaleFactor);
			layoutControlItemMedia.MinSize = RectangleHelper.ScaleSize(layoutControlItemMedia.MinSize, scaleFactor);
			layoutControlItemDigital.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDigital.MaxSize, scaleFactor);
			layoutControlItemDigital.MinSize = RectangleHelper.ScaleSize(layoutControlItemDigital.MinSize, scaleFactor);
			layoutControlItemReset.MaxSize = RectangleHelper.ScaleSize(layoutControlItemReset.MaxSize, scaleFactor);
			layoutControlItemReset.MinSize = RectangleHelper.ScaleSize(layoutControlItemReset.MinSize, scaleFactor);
			layoutControlItemDelete.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDelete.MaxSize, scaleFactor);
			layoutControlItemDelete.MinSize = RectangleHelper.ScaleSize(layoutControlItemDelete.MinSize, scaleFactor);
		}

		public virtual void LoadData()
		{
			_loading = true;
			checkEditItem.Checked = Data.ShowValue;
			checkEditDetails.Checked = Data.ShowDescription;
			checkEditMonthly.Checked = Data.ShowMonthly;
			checkEditTotal.Checked = Data.ShowTotal;

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
			simpleLabelItemNumber.Text = String.Format("<size=+4><b>{0}</b></size>", Data.Order + 1);
		}

		public void UpdatePositionButtons()
		{
			layoutControlItemUp.Enabled = !ProductSummaryData.IsFirstInCollection;
			layoutControlItemDown.Enabled = !ProductSummaryData.IsLastInCollection;
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
			checkEditDetails.Checked = true;
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
			checkEditDetails.Checked = true;
			memoEditDetails.EditValue = Data.Description;
			_loading = false;
		}

		private void OnResetItemClick(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(Data.Description))
				if (PopupMessageHelper.Instance.ShowWarningQuestion(
					"Are you SURE you want to DELETE the data for this product?")
					!= DialogResult.Yes) return;

			ProductSummaryData.ResetToDefault();
			checkEditDetails.Checked = false;
			memoEditDetails.EditValue = Data.Description;
		}

		private void ckMonthly_CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemMonthlyValue.Enabled = checkEditMonthly.Checked;
			if (_loading) return;
			spinEditMonthly.Value = checkEditMonthly.Checked ? spinEditMonthly.Value : 0;
			Data.ShowMonthly = checkEditMonthly.Checked;
			InvestmentChanged?.Invoke(this, EventArgs.Empty);
			RaiseDataChanged();
		}

		private void ckTotal_CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTotalValue.Enabled = checkEditTotal.Checked;
			if (_loading) return;
			spinEditTotal.Value = checkEditTotal.Checked ? spinEditTotal.Value : 0;
			Data.ShowTotal = checkEditTotal.Checked;
			InvestmentChanged?.Invoke(this, EventArgs.Empty);
			RaiseDataChanged();
		}

		private void ckItem_CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemTitleValue.Enabled = checkEditItem.Checked;
			if (_loading) return;
			Data.ShowValue = checkEditItem.Checked;
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
			layoutControlItemDetailsValue.Enabled = checkEditDetails.Checked;
			if (_loading) return;
			memoEditDetails.EditValue = checkEditDetails.Checked ? memoEditDetails.EditValue : null;
			Data.ShowDescription = checkEditDetails.Checked;
			if (!checkEditDetails.Checked)
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

		public decimal? OutputMonthlyValue => checkEditMonthly.Checked ? MonthlyValue : null;

		public decimal? OutputTotalValue => checkEditTotal.Checked ? TotalValue : null;

		public bool Complited => Data.ShowValue && !String.IsNullOrEmpty(ItemTitle);

		#endregion
	}
}