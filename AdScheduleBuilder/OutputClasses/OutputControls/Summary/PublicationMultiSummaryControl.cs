using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class PublicationMultiSummaryControl : DevExpress.XtraTab.XtraTabPage
    //public partial class PublicationMultiSummaryControl : System.Windows.Forms.UserControl
    {
        private bool _allowToSave = false;

        public BusinessClasses.Publication Publication { get; set; }

        public bool SettingsNotSaved
        {
            get
            {
                return OutputMultiSummaryControl.Instance.SettingsNotSaved;
            }
            set
            {
                OutputMultiSummaryControl.Instance.SettingsNotSaved = value;
            }
        }

        public PublicationMultiSummaryControl()
        {
            InitializeComponent();
        }

        private bool AllowToCheck()
        {
            int checkedNumber = 0;
            if (checkEditPageSize.Checked)
                checkedNumber++;
            if (checkEditPercentOfPage.Checked)
                checkedNumber++;
            if (checkEditAvgAdCost.Checked)
                checkedNumber++;
            if (checkEditAvgFinalCost.Checked)
                checkedNumber++;
            if (checkEditAvgPCI.Checked)
                checkedNumber++;
            if (checkEditColor.Checked)
                checkedNumber++;
            if (checkEditSquare.Checked)
                checkedNumber++;
            if (checkEditDimensions.Checked)
                checkedNumber++;
            if (checkEditDiscounts.Checked)
                checkedNumber++;
            if (checkEditMechanicals.Checked)
                checkedNumber++;
            if (checkEditSections.Checked)
                checkedNumber++;
            if (checkEditTotalAds.Checked)
                checkedNumber++;
            if (checkEditTotalSquare.Checked)
                checkedNumber++;
            return checkedNumber < 6;
        }

        public void LoadPublication()
        {
            this.Text = this.Publication.Name.Replace("&", "&&");
            pbLogo.Image = this.Publication.SmallLogo != null ? new Bitmap(this.Publication.SmallLogo) : null;
            checkEditFlightDates.Text = this.Publication.Parent.FlightDateStart.ToString("MM/dd/yy") + " - " + this.Publication.Parent.FlightDateEnd.ToString("MM/dd/yy");
            checkEditName.Text = this.Publication.Name.Replace("&", "&&");
            checkEditTotalAds.Text = "Total Ads: " + this.Publication.TotalInserts.ToString("#,##0");
            checkEditTotalSquare.Text = this.Publication.TotalSquare.HasValue && this.Publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage ? ("Total Column Inches: " + this.Publication.TotalSquare.Value.ToString("#,##0.00")) : string.Empty;
            checkEditTotalSquare.Visible = this.Publication.TotalSquare.HasValue && this.Publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage;
            checkEditPageSize.Text = !string.IsNullOrEmpty(this.Publication.SizeOptions.PageSize) ? ("Page Size: " + this.Publication.SizeOptions.PageSize) : string.Empty;
            checkEditPageSize.Visible = !string.IsNullOrEmpty(this.Publication.SizeOptions.PageSize);
            checkEditPercentOfPage.Text = !string.IsNullOrEmpty(this.Publication.SizeOptions.PercentOfPage) ? (this.Publication.SizeOptions.PercentOfPage + " Share of Page") : string.Empty;
            checkEditPercentOfPage.Visible = !string.IsNullOrEmpty(this.Publication.SizeOptions.PercentOfPage);
            checkEditAvgAdCost.Text = "BW Avg Ad Cost: " + this.Publication.AvgADRate.ToString("$#,##0.00");
            checkEditAvgFinalCost.Text = "Final Avg Ad Cost: " + this.Publication.AvgFinalRate.ToString("$#,##0.00");
            checkEditAvgPCI.Text = this.Publication.AvgPCIRate > 0 ? ("Avg PCI: " + this.Publication.AvgPCIRate.ToString("$#,##0.00")) : string.Empty;
            checkEditAvgPCI.Visible = this.Publication.AvgPCIRate > 0;
            checkEditSquare.Text = this.Publication.SizeOptions.Square.HasValue && this.Publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage ? ("Column Inches: " + this.Publication.SizeOptions.Square.Value.ToString("#,##0.00") + " col. in.") : string.Empty;
            checkEditSquare.Visible = this.Publication.SizeOptions.Square.HasValue && this.Publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage;
            checkEditDimensions.Text = !string.IsNullOrEmpty(this.Publication.SizeOptions.Dimensions) ? ("Dimensions: " + this.Publication.SizeOptions.Dimensions) : string.Empty;
            checkEditDimensions.Visible = !string.IsNullOrEmpty(this.Publication.SizeOptions.Dimensions);
            checkEditDiscounts.Text = "Discounts: " + this.Publication.TotalDiscountRate.ToString("$#,##0.00");
            labelControlSections.Text = "Sections: " + string.Join(", ", this.Publication.Inserts.Where(x => !string.IsNullOrEmpty(x.Section)).Select(x => x.Section).Distinct());

            switch (this.Publication.ColorOption)
            {
                case BusinessClasses.ColorOptions.BlackWhite:
                    checkEditColor.Text = "Black && White";
                    break;
                case BusinessClasses.ColorOptions.SpotColor:
                    checkEditColor.Text = "Spot Color";
                    break;
                case BusinessClasses.ColorOptions.FullColor:
                    checkEditColor.Text = "Full Color";
                    break;
            }
            List<string> dates = new List<string>();
            foreach (BusinessClasses.Insert insert in this.Publication.Inserts)
            {
                if (insert.DateObject != null)
                    dates.Add(insert.Date.ToString("MM/dd/yy"));
            }
            memoEditDates.EditValue = string.Join(", ", dates.ToArray());
            laInvestment.Text = "Investment: " + this.Publication.TotalFinalRate.ToString("$#,##0.00");

            _allowToSave = false;
            checkEditPageSize.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowPageSize & !string.IsNullOrEmpty(this.Publication.SizeOptions.PageSize);
            checkEditPercentOfPage.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowPercentOfPage & !string.IsNullOrEmpty(this.Publication.SizeOptions.PercentOfPage);
            checkEditAvgAdCost.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowAvgAdCost;
            checkEditAvgFinalCost.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowAvgFinalCost;
            checkEditAvgPCI.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowAvgPCI & this.Publication.AvgPCIRate > 0;
            checkEditSquare.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowSquare & this.Publication.SizeOptions.Square.HasValue && this.Publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage;
            checkEditComments.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowComments;
            checkEditDates.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowDates;
            checkEditDimensions.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowDimensions & !string.IsNullOrEmpty(this.Publication.SizeOptions.Dimensions);
            checkEditDiscounts.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowDiscounts;
            checkEditFlightDates.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowFlightDates;
            checkEditInvestment.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowInvestment;
            checkEditLogo.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowLogo;
            checkEditName.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowName;
            checkEditSections.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowSection;
            checkEditColor.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowTotalColor;
            checkEditTotalAds.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowTotalInserts;
            checkEditTotalSquare.Checked = this.Publication.ViewSettings.MultiSummarySettings.ShowTotalSquare & this.Publication.TotalSquare.HasValue && this.Publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage;
            comboBoxEditInvestment.EditValue = this.Publication.ViewSettings.MultiSummarySettings.InvestmentType;
            memoEditComments.EditValue = this.Publication.ViewSettings.MultiSummarySettings.Comments;
            _allowToSave = true;
        }

        private void checkEditInvestment_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditInvestment.Enabled = checkEditInvestment.Checked;
            checkEdit_CheckedChanged(null, null);
        }

        private void checkEditDates_CheckedChanged(object sender, EventArgs e)
        {
            memoEditDates.Enabled = checkEditDates.Checked;
            checkEdit_CheckedChanged(null, null);
        }

        private void checkEditComments_CheckedChanged(object sender, EventArgs e)
        {
            memoEditComments.Enabled = checkEditComments.Checked;
            checkEdit_CheckedChanged(null, null);
        }

        private void checkEditAdItems_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (_allowToSave)
            {
                if ((bool)e.NewValue == true)
                {
                    if (!AllowToCheck())
                    {
                        AppManager.ShowWarning("You may select only up to 6 Ad-Items");
                        e.Cancel = true;
                    }
                }
            }
        }

        private void comboBoxEditInvestment_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                this.Publication.ViewSettings.MultiSummarySettings.InvestmentType = comboBoxEditInvestment.EditValue != null ? comboBoxEditInvestment.EditValue.ToString() : string.Empty;
                this.SettingsNotSaved = true;
            }
        }

        private void memoEditComments_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                this.Publication.ViewSettings.MultiSummarySettings.Comments = memoEditComments.EditValue != null ? memoEditComments.EditValue.ToString() : string.Empty;
                this.SettingsNotSaved = true;
            }
        }

        private void checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                this.Publication.ViewSettings.MultiSummarySettings.ShowPageSize = checkEditPageSize.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowPercentOfPage = checkEditPercentOfPage.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowAvgAdCost = checkEditAvgAdCost.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowAvgFinalCost = checkEditAvgFinalCost.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowAvgPCI = checkEditAvgPCI.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowSquare = checkEditSquare.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowComments = checkEditComments.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowDates = checkEditDates.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowDimensions = checkEditDimensions.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowDiscounts = checkEditDiscounts.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowFlightDates = checkEditFlightDates.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowInvestment = checkEditInvestment.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowLogo = checkEditLogo.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowMechanicals = checkEditMechanicals.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowName = checkEditName.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowSection = checkEditSections.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowTotalColor = checkEditColor.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowTotalInserts = checkEditTotalAds.Checked;
                this.Publication.ViewSettings.MultiSummarySettings.ShowTotalSquare = checkEditTotalSquare.Checked;
                this.SettingsNotSaved = true;
            }
        }

        private void checkEdit_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit cEdit = (DevExpress.XtraEditors.CheckEdit)sender;
            DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo cInfo = (DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo)cEdit.GetViewInfo();
            System.Drawing.Rectangle r = cInfo.CheckInfo.GlyphRect;
            System.Drawing.Rectangle editorRect = new System.Drawing.Rectangle(new System.Drawing.Point(0, 0), cEdit.Size);
            if (!r.Contains(e.Location) && editorRect.Contains(e.Location))
                ((DevExpress.Utils.DXMouseEventArgs)e).Handled = true;
        }
    }
}
