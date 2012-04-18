using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    public partial class PublicationBasicOverviewControl : DevExpress.XtraTab.XtraTabPage
    //public partial class PublicationBasicOverviewControl : System.Windows.Forms.UserControl
    {
        private bool _allowToSave = false;
        public BusinessClasses.Publication Publication { get; set; }

        public bool SettingsNotSaved
        {
            get
            {
                return OutputBasicOverviewControl.Instance.SettingsNotSaved;
            }
            set
            {
                OutputBasicOverviewControl.Instance.SettingsNotSaved = value;
            }
        }

        public PublicationBasicOverviewControl()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laAdSize.Font = new Font(laAdSize.Font.FontFamily, laAdSize.Font.Size - 3, laAdSize.Font.Style);
                laDates.Font = new Font(laDates.Font.FontFamily, laDates.Font.Size - 3, laDates.Font.Style);
                laInvestmentDetails.Font = new Font(laInvestmentDetails.Font.FontFamily, laInvestmentDetails.Font.Size - 3, laInvestmentDetails.Font.Style);
                laTotalAds.Font = new Font(laTotalAds.Font.FontFamily, laTotalAds.Font.Size - 3, laTotalAds.Font.Style);
                checkEditAvgADRate.Font = new Font(checkEditAvgADRate.Font.FontFamily, checkEditAvgADRate.Font.Size - 2, checkEditAvgADRate.Font.Style);
                checkEditAvgPCIRate.Font = new Font(checkEditAvgPCIRate.Font.FontFamily, checkEditAvgPCIRate.Font.Size - 2, checkEditAvgPCIRate.Font.Style);
                checkEditBusinessName.Font = new Font(checkEditBusinessName.Font.FontFamily, checkEditBusinessName.Font.Size - 2, checkEditBusinessName.Font.Style);
                checkEditStandartColor.Font = new Font(checkEditStandartColor.Font.FontFamily, checkEditStandartColor.Font.Size - 2, checkEditStandartColor.Font.Style);
                checkEditStandartDimensions.Font = new Font(checkEditStandartDimensions.Font.FontFamily, checkEditStandartDimensions.Font.Size - 2, checkEditStandartDimensions.Font.Style);
                checkEditDate.Font = new Font(checkEditDate.Font.FontFamily, checkEditDate.Font.Size - 2, checkEditDate.Font.Style);
                checkEditDecisionMaker.Font = new Font(checkEditDecisionMaker.Font.FontFamily, checkEditDecisionMaker.Font.Size - 2, checkEditDecisionMaker.Font.Style);
                checkEditFlightDates.Font = new Font(checkEditFlightDates.Font.FontFamily, checkEditFlightDates.Font.Size - 2, checkEditFlightDates.Font.Style);
                checkEditFlightDates2.Font = new Font(checkEditFlightDates2.Font.FontFamily, checkEditFlightDates2.Font.Size - 2, checkEditFlightDates2.Font.Style);
                checkEditStandartMechanicals.Font = new Font(checkEditStandartMechanicals.Font.FontFamily, checkEditStandartMechanicals.Font.Size - 2, checkEditStandartMechanicals.Font.Style);
                checkEditName.Font = new Font(checkEditName.Font.FontFamily, checkEditName.Font.Size - 3, checkEditName.Font.Style);
                checkEditStandartPageSize.Font = new Font(checkEditStandartPageSize.Font.FontFamily, checkEditStandartPageSize.Font.Size - 2, checkEditStandartPageSize.Font.Style);
                checkEditTotalAds.Font = new Font(checkEditTotalAds.Font.FontFamily, checkEditTotalAds.Font.Size - 2, checkEditTotalAds.Font.Style);
                checkEditStandartSquare.Font = new Font(checkEditStandartSquare.Font.FontFamily, checkEditStandartSquare.Font.Size - 2, checkEditStandartSquare.Font.Style);
                checkEditTotalCost.Font = new Font(checkEditTotalCost.Font.FontFamily, checkEditTotalCost.Font.Size - 2, checkEditTotalCost.Font.Style);
                checkEditTotalDiscounts.Font = new Font(checkEditTotalDiscounts.Font.FontFamily, checkEditTotalDiscounts.Font.Size - 2, checkEditTotalDiscounts.Font.Style);
                checkEditTotalSquare.Font = new Font(checkEditTotalSquare.Font.FontFamily, checkEditTotalSquare.Font.Size - 2, checkEditTotalSquare.Font.Style);
            }
        }

        public void LoadPublication()
        {
            this.Text = this.Publication.Name.Replace("&", "&&");
            pbLogo.Image = this.Publication.SmallLogo != null ? new Bitmap(this.Publication.SmallLogo) : null;
            checkEditName.Text = this.Publication.Name.Replace("&", "&&");

            checkEditStandartDimensions.Text = !string.IsNullOrEmpty(this.Publication.SizeOptions.Dimensions) ? this.Publication.SizeOptions.Dimensions : string.Empty;
            checkEditStandartDimensions.Visible = !string.IsNullOrEmpty(this.Publication.SizeOptions.Dimensions);
            checkEditSharePageDimensions.Text = !string.IsNullOrEmpty(this.Publication.SizeOptions.Dimensions) ? this.Publication.SizeOptions.Dimensions : string.Empty;
            checkEditSharePageDimensions.Visible = !string.IsNullOrEmpty(this.Publication.SizeOptions.Dimensions);
            checkEditStandartSquare.Text = this.Publication.SizeOptions.Square.HasValue ? (this.Publication.SizeOptions.Square.Value.ToString("#,##0.00") + " col. in.") : string.Empty;
            checkEditStandartSquare.Visible = this.Publication.SizeOptions.Square.HasValue;
            checkEditTotalSquare.Text = this.Publication.TotalSquare.HasValue && this.Publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage ? ("Total Inches: " + this.Publication.TotalSquare.Value.ToString("#,##0.00")) : string.Empty;
            checkEditTotalSquare.Visible = this.Publication.TotalSquare.HasValue && this.Publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage;
            checkEditStandartPageSize.Visible = !string.IsNullOrEmpty(this.Publication.SizeOptions.PageSize);
            checkEditStandartPageSize.Text = !string.IsNullOrEmpty(this.Publication.SizeOptions.PageSize) ? this.Publication.SizeOptions.PageSize : string.Empty;
            checkEditSharePagePageSize.Visible = !string.IsNullOrEmpty(this.Publication.SizeOptions.PageSize);
            checkEditSharePagePageSize.Text = !string.IsNullOrEmpty(this.Publication.SizeOptions.PageSize) ? this.Publication.SizeOptions.PageSize : string.Empty;
            checkEditStandartMechanicals.Visible = false;// !string.IsNullOrEmpty(this.Publication.SizeOptions.Mechanicals);
            checkEditStandartMechanicals.Text = string.Empty;// !string.IsNullOrEmpty(this.Publication.SizeOptions.Mechanicals) ? ("Mechanicals: " + this.Publication.SizeOptions.Mechanicals) : string.Empty;
            checkEditSharePagePercentOfPage.Visible = !string.IsNullOrEmpty(this.Publication.SizeOptions.PercentOfPage);
            checkEditSharePagePercentOfPage.Text = !string.IsNullOrEmpty(this.Publication.SizeOptions.PercentOfPage) ? (this.Publication.SizeOptions.PercentOfPage + " Share of Page") : string.Empty;
            pnAdSizeStandart.Visible = this.Publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage;
            pnAdSizeSharePage.Visible = this.Publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.SharePage;

            checkEditAvgADRate.Text = "Avg Ad Rate: " + this.Publication.AvgADRate.ToString("$#,##0.00");
            checkEditAvgPCIRate.Text = this.Publication.AvgPCIRate > 0 ? ("Avg PCI: " + this.Publication.AvgPCIRate.ToString("$#,##0.00")) : string.Empty;
            checkEditAvgPCIRate.Visible = this.Publication.AvgPCIRate > 0;
            checkEditBusinessName.Text = this.Publication.Parent.BusinessName + (!string.IsNullOrEmpty(this.Publication.Parent.AccountNumber) ? (" - " + this.Publication.Parent.AccountNumber) : string.Empty);

            switch (this.Publication.ColorOption)
            {
                case BusinessClasses.ColorOptions.BlackWhite:
                    checkEditStandartColor.Text = "Black && White";
                    checkEditSharePageColor.Text = "Black && White";
                    break;
                case BusinessClasses.ColorOptions.SpotColor:
                    checkEditStandartColor.Text = "Spot Color";
                    checkEditSharePageColor.Text = "Spot Color";
                    break;
                case BusinessClasses.ColorOptions.FullColor:
                    checkEditStandartColor.Text = "Full Color";
                    checkEditSharePageColor.Text = "Full Color";
                    break;
            }

            checkEditDate.Text = this.Publication.Parent.PresentationDateObject != null ? this.Publication.Parent.PresentationDate.ToString("MM/dd/yy") : string.Empty;

            List<string> dates = new List<string>();
            foreach (BusinessClasses.Insert insert in this.Publication.Inserts)
            {
                if (insert.DateObject != null)
                    dates.Add(insert.Date.ToString("MM/dd/yy"));
            }
            memoEditDates.EditValue = string.Join(", ", dates.ToArray());

            checkEditDecisionMaker.Text = this.Publication.Parent.DecisionMaker;
            checkEditFlightDates.Text = "   " + this.Publication.Parent.FlightDateStart.ToString("MM/dd/yy") + " - " + this.Publication.Parent.FlightDateEnd.ToString("MM/dd/yy");
            checkEditFlightDates2.Text = this.Publication.Parent.FlightDateStart.ToString("MM/dd/yy") + " - " + this.Publication.Parent.FlightDateEnd.ToString("MM/dd/yy");
            checkEditTotalAds.Text = "Total Ads: " + this.Publication.TotalInserts.ToString("#,##0");
            checkEditTotalCost.Text = "Total Cost: " + this.Publication.TotalFinalRate.ToString("$#,##0.00");
            checkEditTotalDiscounts.Text = "Total Discounts: " + this.Publication.TotalDiscountRate.ToString("$#,##0.00");

            _allowToSave = false;
            checkEditTotalDiscounts.Checked = this.Publication.TotalDiscountRate > 0;


            comboBoxEditSchedule.Properties.Items.Clear();
            comboBoxEditSchedule.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.OutputHeaders.ToArray());
            if (string.IsNullOrEmpty(this.Publication.ViewSettings.BasicOverviewSettings.SlideHeader))
            {
                if (comboBoxEditSchedule.Properties.Items.Count > 0)
                    comboBoxEditSchedule.SelectedIndex = 0;
            }
            else
            {
                int index = comboBoxEditSchedule.Properties.Items.IndexOf(this.Publication.ViewSettings.BasicOverviewSettings.SlideHeader);
                if (index >= 0)
                    comboBoxEditSchedule.SelectedIndex = index;
                else
                    comboBoxEditSchedule.SelectedIndex = 0;
            }
            textEditRunDatesComment.EditValue = this.Publication.ViewSettings.BasicOverviewSettings.Comments;

            checkEditStandartPageSize.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowPageSize;
            checkEditSharePagePageSize.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowPageSize;
            checkEditSharePagePercentOfPage.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowPercentOfPage;
            checkEditBusinessName.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowAdvertiser;
            checkEditAvgADRate.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowAvgAdCost;
            checkEditAvgPCIRate.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowAvgPCI;
            checkEditStandartColor.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowColor;
            checkEditSharePageColor.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowColor;
            checkEditStandartDimensions.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowDimensions;
            checkEditSharePageDimensions.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowDimensions;
            checkEditRunDatesComment.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowComments;
            checkEditDates.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowDates;
            checkEditDecisionMaker.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowDecisionMaker;
            checkEditTotalDiscounts.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowDiscounts && this.Publication.TotalDiscountRate > 0;
            checkEditFlightDates.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowFlightDates;
            checkEditFlightDates2.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowFlightDates2;
            checkEditTotalCost.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowInvestment;
            checkEditLogo.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowLogo;
            checkEditStandartMechanicals.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowMechanicals;
            checkEditName.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowName;
            checkEditDate.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowPresentationDate;
            checkEditSchedule.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowSlideHeader;
            checkEditStandartSquare.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowSquare;
            checkEditTotalAds.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowTotalInserts;
            checkEditTotalSquare.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowTotalSquare;
            checkEditAdSizePicture.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowAdSizeDetails;
            checkEditTotalAdsPicture.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowTotalDetails;
            checkEditInvestmentDetailsPicture.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowInvestmentDetails;
            checkEditDatesPicture.Checked = this.Publication.ViewSettings.BasicOverviewSettings.ShowDateDetails;
            _allowToSave = true;
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

        #region Check Event Handlers
        private void checkEditTextEdit_CheckedChanged(object sender, EventArgs e)
        {
            textEditRunDatesComment.Enabled = checkEditRunDatesComment.Checked;
            checkEdit_CheckedChanged(null, null);
        }

        private void checkEditSchedule_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditSchedule.Enabled = checkEditSchedule.Checked;
            checkEdit_CheckedChanged(null, null);
        }

        private void checkEditDates_CheckedChanged(object sender, EventArgs e)
        {
            memoEditDates.Enabled = checkEditDates.Checked;
            checkEdit_CheckedChanged(null, null);
        }

        private void checkEditAdSizePicture_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkEditTotalAdsPicture_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkEditInvestmentDetailsPicture_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkEditDatesPicture_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                this.Publication.ViewSettings.BasicOverviewSettings.ShowPageSize = this.Publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.SharePage ? checkEditSharePagePageSize.Checked : checkEditStandartPageSize.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowPercentOfPage = checkEditSharePagePercentOfPage.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowAdSizeDetails = checkEditAdSizePicture.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowAdvertiser = checkEditBusinessName.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowAvgAdCost = checkEditAvgADRate.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowAvgPCI = checkEditAvgPCIRate.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowColor = this.Publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.SharePage ? checkEditSharePageColor.Checked : checkEditStandartColor.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowDimensions = this.Publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.SharePage ? checkEditSharePageDimensions.Checked : checkEditStandartDimensions.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowComments = checkEditRunDatesComment.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowDateDetails = checkEditDatesPicture.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowDates = checkEditDates.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowDecisionMaker = checkEditDecisionMaker.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowDiscounts = checkEditTotalDiscounts.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowFlightDates = checkEditFlightDates.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowFlightDates2 = checkEditFlightDates2.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowInvestment = checkEditTotalCost.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowInvestmentDetails = checkEditInvestmentDetailsPicture.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowLogo = checkEditLogo.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowMechanicals = checkEditStandartMechanicals.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowName = checkEditName.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowPresentationDate = checkEditDate.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowSlideHeader = checkEditSchedule.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowSquare = checkEditStandartSquare.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowTotalDetails = checkEditTotalAdsPicture.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowTotalInserts = checkEditTotalAds.Checked;
                this.Publication.ViewSettings.BasicOverviewSettings.ShowTotalSquare = checkEditTotalSquare.Checked;
                this.SettingsNotSaved = true;
            }
        }
        private void comboBoxEditSchedule_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                this.Publication.ViewSettings.BasicOverviewSettings.SlideHeader = comboBoxEditSchedule.EditValue != null ? comboBoxEditSchedule.EditValue.ToString() : string.Empty;
                this.SettingsNotSaved = true;
            }
        }

        private void textEditRunDatesComment_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                this.Publication.ViewSettings.BasicOverviewSettings.Comments = textEditRunDatesComment.EditValue != null ? textEditRunDatesComment.EditValue.ToString() : string.Empty;
                this.SettingsNotSaved = true;
            }
        }
        #endregion

        #region Output Staff
        public string Header
        {
            get
            {
                string result = string.Empty;
                if (checkEditSchedule.Checked)
                    result = comboBoxEditSchedule.EditValue.ToString();
                return result;
            }
        }

        public string LogoFile
        {
            get
            {
                string result = string.Empty;
                if (checkEditLogo.Checked)
                {
                    result = System.IO.Path.GetTempFileName();
                    pbLogo.Image.Save(result);
                }
                return result;
            }
        }

        public string PresentationName1
        {
            get
            {
                string result = string.Empty;
                if (checkEditName.Checked && checkEditLogo.Checked)
                    result = this.Text;
                return result.Replace("&&", "&");
            }
        }

        public string PresentationDate1
        {
            get
            {
                string result = string.Empty;
                if (checkEditDate.Checked && checkEditLogo.Checked)
                    result = checkEditDate.Text;
                return result;
            }
        }

        public string PresentationName2
        {
            get
            {
                string result = string.Empty;
                if (checkEditName.Checked && checkEditLogo.Checked)
                    result = this.Text;
                return result.Replace("&&", "&");
            }
        }

        public string PresentationDate2
        {
            get
            {
                string result = string.Empty;
                if (checkEditDate.Checked && checkEditLogo.Checked)
                    result = checkEditDate.Text;
                return result;
            }
        }

        public string BusinessName
        {
            get
            {
                string result = string.Empty;
                if (checkEditBusinessName.Checked)
                    result = checkEditBusinessName.Text;
                return result;
            }
        }

        public string DecisionMaker
        {
            get
            {
                string result = string.Empty;
                if (checkEditDecisionMaker.Checked)
                    result = checkEditDecisionMaker.Text;
                return result;
            }
        }

        public string FlightDates1
        {
            get
            {
                string result = string.Empty;
                if (checkEditFlightDates.Checked)
                    result = checkEditFlightDates.Text;
                return result;
            }
        }

        public string FlightDates2
        {
            get
            {
                string result = string.Empty;
                if (checkEditFlightDates2.Checked)
                    result = checkEditFlightDates2.Text;
                return result;
            }
        }

        public string[] AdSpecs
        {
            get
            {
                List<string> values = new List<string>();
                if (this.Publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.SharePage)
                {
                    if (checkEditSharePagePercentOfPage.Checked && !string.IsNullOrEmpty(checkEditSharePagePercentOfPage.Text))
                        values.Add(checkEditSharePagePercentOfPage.Text);
                    if (checkEditSharePageColor.Checked)
                        values.Add(checkEditSharePageColor.Text.Replace("&&", "&"));
                    if (checkEditSharePagePageSize.Checked && !string.IsNullOrEmpty(this.Publication.SizeOptions.PageSize))
                        values.Add(checkEditSharePagePageSize.Text);
                    if (checkEditSharePageDimensions.Checked && !string.IsNullOrEmpty(checkEditSharePageDimensions.Text))
                        values.Add(checkEditSharePageDimensions.Text);
                }
                else
                {
                    if (checkEditStandartSquare.Checked && !string.IsNullOrEmpty(checkEditStandartSquare.Text))
                        values.Add(checkEditStandartSquare.Text);
                    if (checkEditStandartPageSize.Checked && !string.IsNullOrEmpty(this.Publication.SizeOptions.PageSize))
                        values.Add(checkEditStandartPageSize.Text);
                    if (checkEditStandartDimensions.Checked && !string.IsNullOrEmpty(checkEditStandartDimensions.Text))
                        values.Add(checkEditStandartDimensions.Text);
                    //if (checkEditStandartMechanicals.Checked && !string.IsNullOrEmpty(this.Publication.SizeOptions.Mechanicals))
                    //    values.Add(checkEditStandartMechanicals.Text);
                    if (checkEditStandartColor.Checked)
                        values.Add(checkEditStandartColor.Text.Replace("&&", "&"));
                }
                return values.ToArray();
            }
        }

        public string[] AdSummaries
        {
            get
            {
                List<string> values = new List<string>();
                if (checkEditTotalAds.Checked)
                    values.Add(checkEditTotalAds.Text);
                if (checkEditTotalSquare.Checked && !string.IsNullOrEmpty(checkEditTotalSquare.Text))
                    values.Add(checkEditTotalSquare.Text);
                return values.ToArray();
            }
        }

        public string[] InvestmentDetails
        {
            get
            {
                List<string> values = new List<string>();
                if (checkEditAvgADRate.Checked)
                    values.Add(checkEditAvgADRate.Text);
                if (checkEditAvgPCIRate.Checked && !string.IsNullOrEmpty(checkEditAvgPCIRate.Text))
                    values.Add(checkEditAvgPCIRate.Text);
                if (checkEditTotalDiscounts.Checked)
                    values.Add(checkEditTotalDiscounts.Text);
                if (checkEditTotalCost.Checked)
                    values.Add(checkEditTotalCost.Text);
                return values.ToArray();
            }
        }

        public string RunDates
        {
            get
            {
                string result = string.Empty;
                if (checkEditDates.Checked)
                    result = memoEditDates.EditValue.ToString();
                if (checkEditRunDatesComment.Checked && textEditRunDatesComment.EditValue != null && !string.IsNullOrEmpty(textEditRunDatesComment.EditValue.ToString()))
                {
                    if (!string.IsNullOrEmpty(result))
                        result += " - ";
                    result += textEditRunDatesComment.Text;
                }
                return result;
            }
        }

        public string OutputFileIndex
        {
            get
            {
                int index = 0;

                if (checkEditAdSizePicture.Checked &&
                    checkEditTotalAdsPicture.Checked &&
                    checkEditInvestmentDetailsPicture.Checked &&
                    checkEditDatesPicture.Checked)
                    index = 1;
                else if (checkEditAdSizePicture.Checked &&
                    checkEditTotalAdsPicture.Checked &&
                    checkEditInvestmentDetailsPicture.Checked &&
                    !checkEditDatesPicture.Checked)
                    index = 2;
                else if (checkEditAdSizePicture.Checked &&
                    !checkEditTotalAdsPicture.Checked &&
                    checkEditInvestmentDetailsPicture.Checked &&
                    checkEditDatesPicture.Checked)
                    index = 3;
                else if (checkEditAdSizePicture.Checked &&
                    checkEditTotalAdsPicture.Checked &&
                    !checkEditInvestmentDetailsPicture.Checked &&
                    checkEditDatesPicture.Checked)
                    index = 4;
                else if (checkEditAdSizePicture.Checked &&
                    checkEditTotalAdsPicture.Checked &&
                    !checkEditInvestmentDetailsPicture.Checked &&
                    !checkEditDatesPicture.Checked)
                    index = 5;
                else if (checkEditAdSizePicture.Checked &&
                    !checkEditTotalAdsPicture.Checked &&
                    checkEditInvestmentDetailsPicture.Checked &&
                    !checkEditDatesPicture.Checked)
                    index = 6;
                else if (checkEditAdSizePicture.Checked &&
                    !checkEditTotalAdsPicture.Checked &&
                    !checkEditInvestmentDetailsPicture.Checked &&
                    checkEditDatesPicture.Checked)
                    index = 7;
                else if (!checkEditAdSizePicture.Checked &&
                    checkEditTotalAdsPicture.Checked &&
                    checkEditInvestmentDetailsPicture.Checked &&
                    checkEditDatesPicture.Checked)
                    index = 8;
                else if (!checkEditAdSizePicture.Checked &&
                    checkEditTotalAdsPicture.Checked &&
                    checkEditInvestmentDetailsPicture.Checked &&
                    !checkEditDatesPicture.Checked)
                    index = 9;
                else if (!checkEditAdSizePicture.Checked &&
                    checkEditTotalAdsPicture.Checked &&
                    !checkEditInvestmentDetailsPicture.Checked &&
                    checkEditDatesPicture.Checked)
                    index = 10;
                else if (!checkEditAdSizePicture.Checked &&
                    !checkEditTotalAdsPicture.Checked &&
                    checkEditInvestmentDetailsPicture.Checked &&
                    checkEditDatesPicture.Checked)
                    index = 11;
                else if (checkEditAdSizePicture.Checked &&
                    !checkEditTotalAdsPicture.Checked &&
                    !checkEditInvestmentDetailsPicture.Checked &&
                    !checkEditDatesPicture.Checked)
                    index = 12;
                else if (!checkEditAdSizePicture.Checked &&
                    checkEditTotalAdsPicture.Checked &&
                    !checkEditInvestmentDetailsPicture.Checked &&
                    !checkEditDatesPicture.Checked)
                    index = 13;
                else if (!checkEditAdSizePicture.Checked &&
                    !checkEditTotalAdsPicture.Checked &&
                    checkEditInvestmentDetailsPicture.Checked &&
                    !checkEditDatesPicture.Checked)
                    index = 14;
                else if (!checkEditAdSizePicture.Checked &&
                    !checkEditTotalAdsPicture.Checked &&
                    !checkEditInvestmentDetailsPicture.Checked &&
                    checkEditDatesPicture.Checked)
                    index = 15;

                return index.ToString();
            }
        }

        public void PrintOutput()
        {
            InteropClasses.PowerPointHelper.Instance.AppendBasicOverview(this);
        }
        #endregion
    }
}
