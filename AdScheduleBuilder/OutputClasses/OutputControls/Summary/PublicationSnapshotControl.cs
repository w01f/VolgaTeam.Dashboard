using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class PublicationSnapshotControl : UserControl
    {
        private BusinessClasses.Publication _publication = null;

        public PublicationSnapshotControl(BusinessClasses.Publication publication)
        {
            InitializeComponent();
            this.Dock = DockStyle.Top;
            _publication = publication;
            UpdateData();
        }

        private void UpdateSize()
        {
            RefreshOrder();
            int bottom = 0;
            foreach (Control control in pnMain.Controls)
                if (control.Visible && control.Bottom > bottom)
                    bottom = control.Bottom;
            this.Height = bottom + this.Padding.Top + this.Padding.Bottom;
        }

        private void UpdateData()
        {
            laPublicationName.Text = _publication.Name;
            if (_publication.SmallLogo != null)
                pictureBoxLogo.Image = new Bitmap(_publication.SmallLogo);
            else
                pictureBoxLogo.Image = null;
            laSquareValue.Text = _publication.SizeOptions.Square.HasValue && _publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage ? (_publication.SizeOptions.Square.Value.ToString("#,###.00#")) : "N/A";
            laCostValue.Text = _publication.AvgADRate.ToString("$#,###.00");
            if (_publication.DailyDelivery.HasValue)
                laDeliveryValue.Text = _publication.DailyDelivery.Value.ToString("#,##0");
            else
                laDeliveryValue.Text = "0";
            laDimensionsValue.Text = !string.IsNullOrEmpty(_publication.SizeOptions.Dimensions) ? (_publication.SizeOptions.Dimensions) : "N/A";
            laDiscountsValue.Text = _publication.TotalDiscountRate.ToString("$#,###.00");
            laFinalCostValue.Text = _publication.AvgFinalRate.ToString("$#,###.00");
            laInvestmentValue.Text = _publication.TotalFinalRate.ToString("$#,###.00");
            laPageSizeValue.Text = !string.IsNullOrEmpty(_publication.SizeOptions.PageSize) ? _publication.SizeOptions.PageSize : "N/A";
            laPercentOfPageValue.Text = !string.IsNullOrEmpty(_publication.SizeOptions.PercentOfPage) && _publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.SharePage ? _publication.SizeOptions.PercentOfPage : "N/A";
            laPCIValue.Text = _publication.AvgPCIRate > 0 ? _publication.AvgPCIRate.ToString("$#,###.00") : "N/A";
            if (_publication.DailyReadership.HasValue)
                laReadershipValue.Text = _publication.DailyReadership.Value.ToString("#,##0");
            else
                laReadershipValue.Text = "0";
            laTotalColorValue.Text = _publication.TotalColorPricingCalculated > 0 ? _publication.TotalColorPricingCalculated.ToString("$#,###.00") : _publication.Inserts.FirstOrDefault().ColorPricingObject.ToString();
            laTotalSquareValue.Text = _publication.TotalSquare.HasValue && _publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage ? (_publication.TotalSquare.Value.ToString("#,###.00#")) : "N/A";
            laTotalInsertsValue.Text = _publication.TotalInserts.ToString("#,##0");
        }

        public void UpdateToggles()
        {
            pnSquare.Visible = OutputSnapshotControl.Instance.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowSquare;
            pnCost.Visible = OutputSnapshotControl.Instance.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgCost;
            pnDelivery.Visible = OutputSnapshotControl.Instance.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDelivery;
            pnDimensions.Visible = OutputSnapshotControl.Instance.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDimensions;
            pnDiscounts.Visible = OutputSnapshotControl.Instance.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalDiscounts;
            pnFinalCost.Visible = OutputSnapshotControl.Instance.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgFinalCost;
            pnInvestment.Visible = OutputSnapshotControl.Instance.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalFinalCost;
            pictureBoxLogo.Visible = OutputSnapshotControl.Instance.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowLogo;
            pnPageSize.Visible = OutputSnapshotControl.Instance.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPageSize;
            pnPercentOfPage.Visible = OutputSnapshotControl.Instance.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPercentOfPage & BusinessClasses.ListManager.Instance.ShareUnits.Count > 0;
            pnPCI.Visible = OutputSnapshotControl.Instance.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgPCI;
            pnReadership.Visible = OutputSnapshotControl.Instance.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowReadership;
            pnTotalColor.Visible = OutputSnapshotControl.Instance.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalColor;
            pnTotalSquare.Visible = OutputSnapshotControl.Instance.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalSquare;
            pnTotalInserts.Visible = OutputSnapshotControl.Instance.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalInserts;
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
            OutputControls.OutputSnapshotControl.Instance.SetFocusToScrollbar();
        }
    }
}
