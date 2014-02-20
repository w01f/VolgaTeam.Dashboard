using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.Core.AdSchedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class PublicationSnapshotControl : UserControl
	{
		private readonly PrintProduct _printProduct;

		public PublicationSnapshotControl(PrintProduct printProduct)
		{
			InitializeComponent();
			Dock = DockStyle.Top;
			_printProduct = printProduct;
			UpdateData();
		}

		private void UpdateSize()
		{
			RefreshOrder();
			int bottom = 0;
			foreach (Control control in pnMain.Controls)
				if (control.Visible && control.Bottom > bottom)
					bottom = control.Bottom;
			Height = bottom + Padding.Top + Padding.Bottom;
		}

		private void UpdateData()
		{
			laPublicationName.Text = _printProduct.Name;
			if (_printProduct.SmallLogo != null)
				pictureBoxLogo.Image = new Bitmap(_printProduct.SmallLogo);
			else
				pictureBoxLogo.Image = null;
			laSquareValue.Text = _printProduct.SizeOptions.Square.HasValue && _printProduct.AdPricingStrategy != AdPricingStrategies.SharePage ? (_printProduct.SizeOptions.Square.Value.ToString("#,###.00#")) : "N/A";
			laCostValue.Text = _printProduct.AvgADRate.ToString("$#,###.00");
			if (_printProduct.DailyDelivery.HasValue)
				laDeliveryValue.Text = _printProduct.DailyDelivery.Value.ToString("#,##0");
			else
				laDeliveryValue.Text = "0";
			laDimensionsValue.Text = !string.IsNullOrEmpty(_printProduct.SizeOptions.Dimensions) ? (_printProduct.SizeOptions.Dimensions) : "N/A";
			laDiscountsValue.Text = _printProduct.TotalDiscountRate.ToString("$#,###.00");
			laFinalCostValue.Text = _printProduct.AvgFinalRate.ToString("$#,###.00");
			laInvestmentValue.Text = _printProduct.TotalFinalRate.ToString("$#,###.00");
			laPageSizeValue.Text = !String.IsNullOrEmpty(_printProduct.SizeOptions.PageSizeAndGroup) ? _printProduct.SizeOptions.PageSizeAndGroup : "N/A";
			laPercentOfPageValue.Text = !string.IsNullOrEmpty(_printProduct.SizeOptions.PercentOfPage) && _printProduct.AdPricingStrategy == AdPricingStrategies.SharePage ? _printProduct.SizeOptions.PercentOfPage : "N/A";
			laPCIValue.Text = _printProduct.AvgPCIRate > 0 ? _printProduct.AvgPCIRate.ToString("$#,###.00") : "N/A";
			if (_printProduct.DailyReadership.HasValue)
				laReadershipValue.Text = _printProduct.DailyReadership.Value.ToString("#,##0");
			else
				laReadershipValue.Text = "0";
			laTotalColorValue.Text = _printProduct.TotalColorPricingCalculated > 0 ? _printProduct.TotalColorPricingCalculated.ToString("$#,###.00") : _printProduct.Inserts.FirstOrDefault().ColorPricingObject.ToString();
			laTotalSquareValue.Text = _printProduct.TotalSquare.HasValue && _printProduct.AdPricingStrategy != AdPricingStrategies.SharePage ? (_printProduct.TotalSquare.Value.ToString("#,###.00#")) : "N/A";
			laTotalInsertsValue.Text = _printProduct.TotalInserts.ToString("#,##0");
		}

		public void UpdateToggles()
		{
			pnSquare.Visible = Controller.Instance.Summaries.Snapshot.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowSquare;
			pnCost.Visible = Controller.Instance.Summaries.Snapshot.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgCost;
			pnDelivery.Visible = Controller.Instance.Summaries.Snapshot.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDelivery;
			pnDimensions.Visible = Controller.Instance.Summaries.Snapshot.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDimensions;
			pnDiscounts.Visible = Controller.Instance.Summaries.Snapshot.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalDiscounts;
			pnFinalCost.Visible = Controller.Instance.Summaries.Snapshot.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgFinalCost;
			pnInvestment.Visible = Controller.Instance.Summaries.Snapshot.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalFinalCost;
			pictureBoxLogo.Visible = Controller.Instance.Summaries.Snapshot.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowLogo;
			pnPageSize.Visible = Controller.Instance.Summaries.Snapshot.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPageSize;
			pnPercentOfPage.Visible = Controller.Instance.Summaries.Snapshot.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPercentOfPage & ListManager.Instance.ShareUnits.Count > 0;
			pnPCI.Visible = Controller.Instance.Summaries.Snapshot.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgPCI;
			pnReadership.Visible = Controller.Instance.Summaries.Snapshot.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowReadership;
			pnTotalColor.Visible = Controller.Instance.Summaries.Snapshot.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalColor;
			pnTotalSquare.Visible = Controller.Instance.Summaries.Snapshot.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalSquare;
			pnTotalInserts.Visible = Controller.Instance.Summaries.Snapshot.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalInserts;
			UpdateSize();
		}

		private void RefreshOrder()
		{
			pnMain.SuspendLayout();
			pnMain.Visible = false;
			if (pictureBoxLogo.Visible)
				pictureBoxLogo.BringToFront();
			if (pnTotalInserts.Visible)
				pnTotalInserts.BringToFront();
			if (pnPageSize.Visible)
				pnPageSize.BringToFront();
			if (pnPercentOfPage.Visible)
				pnPercentOfPage.BringToFront();
			if (pnDimensions.Visible)
				pnDimensions.BringToFront();
			if (pnSquare.Visible)
				pnSquare.BringToFront();
			if (pnTotalSquare.Visible)
				pnTotalSquare.BringToFront();
			if (pnPCI.Visible)
				pnPCI.BringToFront();
			if (pnCost.Visible)
				pnCost.BringToFront();
			if (pnFinalCost.Visible)
				pnFinalCost.BringToFront();
			if (pnInvestment.Visible)
				pnInvestment.BringToFront();
			if (pnTotalColor.Visible)
				pnTotalColor.BringToFront();
			if (pnDiscounts.Visible)
				pnDiscounts.BringToFront();
			if (pnReadership.Visible)
				pnReadership.BringToFront();
			if (pnDelivery.Visible)
				pnDelivery.BringToFront();
			pnMain.Visible = true;
			pnMain.ResumeLayout();
		}

		private void laPageSizeValue_MouseMove(object sender, MouseEventArgs e)
		{
			Controller.Instance.Summaries.Snapshot.SetFocusToScrollbar();
		}
	}
}