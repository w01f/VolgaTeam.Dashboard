using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MobileScheduleBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    //public partial class ProductControl : UserControl
    public partial class ProductControl : DevExpress.XtraTab.XtraTabPage
    {
        public BusinessClasses.Product Product { get; set; }
        private bool _allowToSave = false;

        public ProductControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            if ((base.CreateGraphics()).DpiX > 96)
            {
                Font font = new Font(labelControlProduct.Font.FontFamily, labelControlProduct.Font.Size - 4, labelControlProduct.Font.Style);
                labelControlProduct.Font = font;
                labelControlWebsiteLogo.Font = font;
                labelControlComments.Font = font;
                labelControlAdCampaign.Font = font;
                labelControlImpressions.Font = font;
                labelControlInvestment.Font = font;
                labelControlDimensions.Font = new Font(labelControlDimensions.Font.FontFamily, labelControlDimensions.Font.Size - 3, labelControlDimensions.Font.Style);
                font = new Font(labelControlWebsiteDetails.Font.FontFamily, labelControlWebsiteDetails.Font.Size - 2, labelControlWebsiteDetails.Font.Style);
                labelControlActiveDays.Font = font;
                labelControlAdRate.Font = font;
                labelControlFlightDates.Font = font;
                labelControlMonthlyCPM.Font = font;
                labelControlMonthlyImpressions.Font = font;
                labelControlMonthlyInvestment.Font = font;
                labelControlTotalAds.Font = font;
                labelControlTotalCPM.Font = font;
                labelControlTotalImpressions.Font = font;
                labelControlTotalInvestment.Font = font;
                labelControlWebsiteDetails.Font = font;
                checkEditComments.Font = font;
                checkEditDuration.Font = font;
                checkEditMonths.Font = font;
                checkEditWeeks.Font = font;
            }
            spinEditActiveDays.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditActiveDays.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditActiveDays.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditAdRate.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditAdRate.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditAdRate.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditDuration.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditDuration.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditDuration.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditMonthlyImpressions.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditMonthlyImpressions.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditMonthlyImpressions.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditMonthlyInvestment.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditMonthlyInvestment.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditMonthlyInvestment.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditMonthlyCPM.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditMonthlyCPM.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditMonthlyCPM.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditTotalAds.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditTotalAds.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditTotalAds.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditTotalImpressions.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditTotalImpressions.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditTotalImpressions.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditTotalInvestment.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditTotalInvestment.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditTotalInvestment.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditTotalCPM.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditTotalCPM.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditTotalCPM.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            textEditCustomWebsite1.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            textEditCustomWebsite1.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            textEditCustomWebsite1.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            textEditCustomWebsite2.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            textEditCustomWebsite2.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            textEditCustomWebsite2.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            memoEditComments.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            memoEditComments.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            memoEditComments.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            memoEditDescription.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            memoEditDescription.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            memoEditDescription.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);

            AssignCloseActiveEditorsonOutSideClick(this);
        }

        private void AssignCloseActiveEditorsonOutSideClick(Control control)
        {
            if (control.GetType() != typeof(DevExpress.XtraEditors.TextEdit) && control.GetType() != typeof(DevExpress.XtraEditors.MemoEdit) && control.GetType() != typeof(DevExpress.XtraEditors.ComboBoxEdit) && control.GetType() != typeof(DevExpress.XtraEditors.LookUpEdit) && control.GetType() != typeof(DevExpress.XtraEditors.DateEdit) && control.GetType() != typeof(DevExpress.XtraEditors.CheckedListBoxControl) && control.GetType() != typeof(DevExpress.XtraEditors.SpinEdit) && control.GetType() != typeof(DevExpress.XtraEditors.CheckEdit))
            {
                control.Click += new EventHandler(CloseActiveEditorsonOutSideClick);
                foreach (Control childControl in control.Controls)
                    AssignCloseActiveEditorsonOutSideClick(childControl);
            }
        }

        private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
        {
            xtraScrollableControlRight.Focus();
        }

        private void UpdateMonthlyFormula()
        {
            switch (this.Product.Formula)
            {
                case BusinessClasses.FormulaType.CPM:
                    this.Product.MonthlyInvestment = spinEditMonthlyInvestment.EditValue != null ? (double?)spinEditMonthlyInvestment.Value : null;
                    this.Product.MonthlyImpressions = spinEditMonthlyImpressions.EditValue != null ? (double?)spinEditMonthlyImpressions.Value : null;

                    spinEditMonthlyCPM.EditValue = this.Product.MonthlyCPMCalculated;
                    break;
                case BusinessClasses.FormulaType.Investment:
                    this.Product.MonthlyImpressions = spinEditMonthlyImpressions.EditValue != null ? (double?)spinEditMonthlyImpressions.Value : null;
                    this.Product.MonthlyCPM = spinEditMonthlyCPM.EditValue != null && !(this.Product.RateType == BusinessClasses.RateType.CPM && this.Product.DefaultRate.HasValue && (double)spinEditMonthlyCPM.Value == this.Product.DefaultRate) ? (double?)spinEditMonthlyCPM.Value : null;

                    spinEditMonthlyInvestment.EditValue = this.Product.MonthlyInvestmentCalculated;
                    break;
                case BusinessClasses.FormulaType.Impressions:
                    this.Product.MonthlyInvestment = spinEditMonthlyInvestment.EditValue != null ? (double?)spinEditMonthlyInvestment.Value : null;
                    this.Product.MonthlyCPM = spinEditMonthlyCPM.EditValue != null && !(this.Product.RateType == BusinessClasses.RateType.CPM && this.Product.DefaultRate.HasValue && (double)spinEditMonthlyCPM.Value == this.Product.DefaultRate) ? (double?)spinEditMonthlyCPM.Value : null;

                    spinEditMonthlyImpressions.EditValue = this.Product.MonthlyImpressionsCalculated;
                    break;
            }
        }

        private void UpdateTotalFormula()
        {
            switch (this.Product.Formula)
            {
                case BusinessClasses.FormulaType.CPM:
                    this.Product.TotalInvestment = spinEditTotalInvestment.EditValue != null ? (double?)spinEditTotalInvestment.Value : null;
                    this.Product.TotalImpressions = spinEditTotalImpressions.EditValue != null ? (double?)spinEditTotalImpressions.Value : null;

                    spinEditTotalCPM.EditValue = this.Product.TotalCPMCalculated;
                    break;
                case BusinessClasses.FormulaType.Investment:
                    this.Product.TotalImpressions = spinEditTotalImpressions.EditValue != null ? (double?)spinEditTotalImpressions.Value : null;
                    this.Product.TotalCPM = spinEditTotalCPM.EditValue != null && !(this.Product.RateType == BusinessClasses.RateType.CPM && this.Product.DefaultRate.HasValue && (double)spinEditTotalCPM.Value == this.Product.DefaultRate) ? (double?)spinEditTotalCPM.Value : null;

                    spinEditTotalInvestment.EditValue = this.Product.TotalInvestmentCalculated;
                    break;
                case BusinessClasses.FormulaType.Impressions:
                    this.Product.TotalInvestment = spinEditTotalInvestment.EditValue != null ? (double?)spinEditTotalInvestment.Value : null;
                    this.Product.TotalCPM = spinEditTotalCPM.EditValue != null && !(this.Product.RateType == BusinessClasses.RateType.CPM && this.Product.DefaultRate.HasValue && (double)spinEditTotalCPM.Value == this.Product.DefaultRate) ? (double?)spinEditTotalCPM.Value : null;

                    spinEditTotalImpressions.EditValue = this.Product.TotalImpressionsCalculated;
                    break;
            }
        }

        private void LoadLists()
        {
            checkedListBoxControlWebsite.Items.Clear();
            checkedListBoxControlWebsite.Items.AddRange(BusinessClasses.ListManager.Instance.Websites.ToArray());
            comboBoxEditStrengths1.Properties.Items.Clear();
            comboBoxEditStrengths1.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.Strengths.ToArray());
            comboBoxEditStrengths2.Properties.Items.Clear();
            comboBoxEditStrengths2.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.Strengths.ToArray());
        }

        public void LoadValues()
        {
            LoadLists();

            _allowToSave = false;

            this.Text = this.Product.Name.Replace("&", "&&");

            if (!string.IsNullOrEmpty(this.Product.SlideHeader))
                ScheduleBuilderControl.Instance.comboBoxEditSlideHeader.EditValue = this.Product.SlideHeader;
            else
                ScheduleBuilderControl.Instance.comboBoxEditSlideHeader.EditValue = null;
            ScheduleBuilderControl.Instance.labelControlPresentationDate.Text = "Presentation Date: " + this.Product.Parent.PresentationDate.ToString("MM/dd/yy");

            checkedListBoxControlWebsite.UnCheckAll();
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlWebsite.Items)
                if (this.Product.Websites.Contains(item.Value.ToString()))
                    item.CheckState = CheckState.Checked;
            if (checkedListBoxControlWebsite.CheckedItems.Count == 0)
                if (checkedListBoxControlWebsite.Items.Count > 0)
                    checkedListBoxControlWebsite.Items[0].CheckState = CheckState.Checked;
            checkEditCustomWebsite1.Checked = this.Product.ShowCustomWebsite1;
            textEditCustomWebsite1.EditValue = this.Product.CustomWebsite1;
            checkEditCustomWebsite2.Checked = this.Product.ShowCustomWebsite2;
            textEditCustomWebsite2.EditValue = this.Product.CustomWebsite2;
            labelControlProduct.Text = string.Format("{0}{1}", new object[] { !string.IsNullOrEmpty(this.Product.SubCategory) ? (this.Product.SubCategory + " - ") : string.Empty, this.Product.Name });
            labelControlDimensions.Text = "Ad Dimensions: " + this.Product.Dimensions;
            memoEditDescription.EditValue = this.Product.Description;
            spinEditAdRate.EditValue = this.Product.AdRate;
            spinEditMonthlyInvestment.EditValue = this.Product.MonthlyInvestmentCalculated;
            spinEditTotalInvestment.EditValue = this.Product.TotalInvestmentCalculated;
            spinEditMonthlyImpressions.EditValue = this.Product.MonthlyImpressionsCalculated;
            spinEditTotalImpressions.EditValue = this.Product.TotalImpressionsCalculated;
            spinEditMonthlyCPM.EditValue = this.Product.MonthlyCPMCalculated;
            spinEditTotalCPM.EditValue = this.Product.TotalCPMCalculated;
            labelControlFlightDates.Text = this.Product.Parent.FlightDates;
            checkEditDuration.Checked = this.Product.ShowDuration;
            switch (this.Product.DurationType)
            {
                case "Months":
                    checkEditMonths.Checked = true;
                    checkEditWeeks.Checked = false;
                    break;
                case "Weeks":
                    checkEditWeeks.Checked = true;
                    checkEditMonths.Checked = false;
                    break;
            }
            if (this.Product.DurationValue.HasValue)
            {
                spinEditDuration.EditValue = this.Product.DurationValue;
            }
            else
            {
                if (checkEditMonths.Checked)
                    spinEditDuration.EditValue = this.Product.MonthDuraton;
                else if (checkEditWeeks.Checked)
                    spinEditDuration.EditValue = this.Product.WeeksDuration;
            }
            spinEditActiveDays.EditValue = this.Product.ActiveDays;
            spinEditTotalAds.EditValue = this.Product.TotalAds;
            checkEditComments.Checked = this.Product.ShowCommentText;
            memoEditComments.EditValue = this.Product.Comment;
            checkEditStrengths1.Checked = this.Product.ShowStrength1;
            comboBoxEditStrengths1.EditValue = this.Product.Strength1;
            checkEditStrengths2.Checked = this.Product.ShowStrength2;
            comboBoxEditStrengths2.EditValue = this.Product.Strength2;

            UpdateView();
            UpdateDefaultCPM();
            _allowToSave = true;
        }

        public void UpdateDefaultCPM()
        {
            if (this.Product.RateType == BusinessClasses.RateType.CPM && this.Product.Formula != BusinessClasses.FormulaType.CPM)
            {
                spinEditMonthlyCPM.EditValue = !this.Product.MonthlyCPM.HasValue ? this.Product.DefaultRate : this.Product.MonthlyCPM;
                spinEditTotalCPM.EditValue = !this.Product.TotalCPM.HasValue ? this.Product.DefaultRate : this.Product.TotalCPM;
            }
        }

        public void UpdateView()
        {

            ScheduleBuilderControl.Instance.labelControlPresentationDate.Visible = this.Product.ShowPresentationDate;
            if (this.Product.ShowBusinessName && this.Product.ShowDecisionMaker)
            {
                ScheduleBuilderControl.Instance.labelControlAdvertiser.Visible = true;
                ScheduleBuilderControl.Instance.labelControlAdvertiser.Text = "Prepared For: " + this.Product.Parent.BusinessName + "\n\n" + this.Product.Parent.DecisionMaker;
            }
            else if (!this.Product.ShowBusinessName && this.Product.ShowDecisionMaker)
            {
                ScheduleBuilderControl.Instance.labelControlAdvertiser.Visible = true;
                ScheduleBuilderControl.Instance.labelControlAdvertiser.Text = this.Product.Parent.DecisionMaker;
            }
            else if (this.Product.ShowBusinessName && !this.Product.ShowDecisionMaker)
            {
                ScheduleBuilderControl.Instance.labelControlAdvertiser.Visible = true;
                ScheduleBuilderControl.Instance.labelControlAdvertiser.Text = "Prepared For: " + this.Product.Parent.BusinessName;
            }
            else
            {
                ScheduleBuilderControl.Instance.labelControlAdvertiser.Visible = false;
            }

            labelControlProduct.Visible = this.Product.ShowProduct;
            pbProductLogo.Visible = this.Product.ShowProduct;
            labelControlDimensions.Visible = this.Product.ShowDimensions;
            memoEditDescription.Visible = this.Product.ShowDescription;
            pnProduct.Visible = this.Product.ShowProduct | this.Product.ShowDimensions | this.Product.ShowDescription;
            if (this.Product.ShowProduct | this.Product.ShowDimensions | this.Product.ShowDescription)
                pnWebsites.Dock = DockStyle.Bottom;
            else
                pnWebsites.Dock = DockStyle.Top;


            labelControlFlightDates.Visible = this.Product.ShowFlightDates;

            int heightImpressions = pnImpressionsLogo.Height + 25;
            pnImpressions.Visible = this.Product.ShowMonthlyImpressions | this.Product.ShowTotalImpressions;
            pnMonthlyImpressions.Visible = this.Product.ShowMonthlyImpressions;
            labelControlMonthlyCPM.Visible = this.Product.ShowMonthlyCPM;
            spinEditMonthlyCPM.Visible = this.Product.ShowMonthlyCPM;
            if (this.Product.ShowMonthlyImpressions)
            {
                pnMonthlyImpressions.SendToBack();
                heightImpressions += pnMonthlyImpressions.Height;
            }
            pnTotalImpressions.Visible = this.Product.ShowTotalImpressions;
            labelControlTotalCPM.Visible = this.Product.ShowTotalCPM;
            spinEditTotalCPM.Visible = this.Product.ShowTotalCPM;
            if (this.Product.ShowTotalImpressions)
            {
                pnTotalImpressions.BringToFront();
                heightImpressions += pnTotalImpressions.Height;
            }
            pnImpressionsLogo.SendToBack();
            if (this.Product.ShowMonthlyImpressions | this.Product.ShowTotalImpressions)
            {
                pnImpressions.Height = heightImpressions;
                pnImpressions.SendToBack();
            }

            int heightInvestment = pnInvestmentLogo.Height + 25;
            pnInvestment.Visible = this.Product.ShowAdRate | this.Product.ShowMonthlyInvestment | this.Product.ShowTotalInvestment;
            pnAdRate.Visible = this.Product.ShowAdRate;
            if (this.Product.ShowAdRate)
            {
                pnAdRate.SendToBack();
                heightInvestment += pnAdRate.Height;
            }
            pnMonthlyInvestment.Visible = this.Product.ShowMonthlyInvestment;
            if (this.Product.ShowMonthlyInvestment)
                heightInvestment += pnMonthlyInvestment.Height;
            pnTotalInvestment.Visible = this.Product.ShowTotalInvestment;
            if (this.Product.ShowTotalInvestment)
            {
                pnTotalInvestment.BringToFront();
                heightInvestment += pnTotalInvestment.Height;
            }
            pnInvestmentLogo.SendToBack();
            if (this.Product.ShowAdRate | this.Product.ShowMonthlyInvestment | this.Product.ShowTotalInvestment)
            {
                pnInvestment.Height = heightInvestment;
                pnInvestment.SendToBack();
            }

            pnTotalMonth.Visible = this.Product.ShowFlightDates;
            if (this.Product.ShowFlightDates)
                pnTotalMonth.SendToBack();
            pnActiveDays.Visible = this.Product.ShowActiveDays;
            pnTotalAds.Visible = this.Product.ShowTotalAds;
            if (this.Product.ShowTotalAds)
                pnTotalAds.BringToFront();
            pnTotals.Visible = this.Product.ShowFlightDates | this.Product.ShowActiveDays | this.Product.ShowTotalAds;
            if (this.Product.ShowFlightDates | this.Product.ShowActiveDays | this.Product.ShowTotalAds)
                pnTotals.SendToBack();

            pnAdCampaignLogo.Visible = this.Product.ShowFlightDates | this.Product.ShowActiveDays | this.Product.ShowTotalAds;
            if (this.Product.ShowFlightDates | this.Product.ShowActiveDays | this.Product.ShowTotalAds)
                pnAdCampaignLogo.SendToBack();

            pnComments.Visible = this.Product.ShowComments;
            if (this.Product.ShowComments)
                pnComments.BringToFront();

            switch (this.Product.Formula)
            {
                case BusinessClasses.FormulaType.CPM:
                    spinEditMonthlyCPM.Enabled = false;
                    spinEditTotalCPM.Enabled = false;
                    spinEditMonthlyInvestment.Enabled = true;
                    spinEditTotalInvestment.Enabled = true;
                    spinEditMonthlyImpressions.Enabled = true;
                    spinEditTotalImpressions.Enabled = true;
                    break;
                case BusinessClasses.FormulaType.Investment:
                    spinEditMonthlyInvestment.Enabled = false;
                    spinEditTotalInvestment.Enabled = false;
                    spinEditMonthlyCPM.Enabled = true;
                    spinEditTotalCPM.Enabled = true;
                    spinEditMonthlyImpressions.Enabled = true;
                    spinEditTotalImpressions.Enabled = true;
                    break;
                case BusinessClasses.FormulaType.Impressions:
                    spinEditMonthlyImpressions.Enabled = false;
                    spinEditTotalImpressions.Enabled = false;
                    spinEditMonthlyCPM.Enabled = true;
                    spinEditTotalCPM.Enabled = true;
                    spinEditMonthlyInvestment.Enabled = true;
                    spinEditTotalInvestment.Enabled = true;
                    break;
            }
            ScheduleBuilderControl.Instance.pictureBoxFormula.Image = this.Product.ShowCPMButton ? Properties.Resources.InvestmentLogo : Properties.Resources.InvestmentLogoGray;
            ScheduleBuilderControl.Instance.labelControlFormula.Enabled = this.Product.ShowCPMButton;
            ScheduleBuilderControl.Instance.checkEditFormulaCPM.Enabled = this.Product.ShowCPMButton;
            ScheduleBuilderControl.Instance.checkEditFormulaInvestment.Enabled = this.Product.ShowCPMButton;
            ScheduleBuilderControl.Instance.checkEditFormulaImpressions.Enabled = this.Product.ShowCPMButton;
        }

        public void SaveValues()
        {
            if (_allowToSave)
            {
                this.Product.SlideHeader = ScheduleBuilderControl.Instance.comboBoxEditSlideHeader.EditValue != null ? ScheduleBuilderControl.Instance.comboBoxEditSlideHeader.EditValue.ToString() : (ScheduleBuilderControl.Instance.comboBoxEditSlideHeader.Properties.Items.Count > 0 ? ScheduleBuilderControl.Instance.comboBoxEditSlideHeader.Properties.Items[0].ToString() : string.Empty);
                this.Product.Websites.Clear();
                if (FormMain.Instance.buttonItemSchedulesWebsites.Checked)
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlWebsite.Items)
                        if (item.CheckState == CheckState.Checked)
                            this.Product.Websites.Add(item.Value.ToString());
                this.Product.CustomWebsite1 = textEditCustomWebsite1.EditValue != null ? textEditCustomWebsite1.EditValue.ToString() : string.Empty;
                this.Product.CustomWebsite2 = textEditCustomWebsite2.EditValue != null ? textEditCustomWebsite2.EditValue.ToString() : string.Empty;
                this.Product.Description = memoEditDescription.EditValue != null ? memoEditDescription.EditValue.ToString() : string.Empty;
                this.Product.AdRate = spinEditAdRate.EditValue != null ? (double?)spinEditAdRate.Value : null;
                this.Product.MonthlyInvestment = spinEditMonthlyInvestment.EditValue != null ? (double?)spinEditMonthlyInvestment.Value : null;
                this.Product.TotalInvestment = spinEditTotalInvestment.EditValue != null ? (double?)spinEditTotalInvestment.Value : null;
                this.Product.MonthlyImpressions = spinEditMonthlyImpressions.EditValue != null ? (double?)spinEditMonthlyImpressions.Value : null;
                this.Product.TotalImpressions = spinEditTotalImpressions.EditValue != null ? (double?)spinEditTotalImpressions.Value : null;
                this.Product.MonthlyCPM = spinEditMonthlyCPM.EditValue != null && !(this.Product.RateType == BusinessClasses.RateType.CPM && this.Product.DefaultRate.HasValue && (double)spinEditMonthlyCPM.Value == this.Product.DefaultRate) ? (double?)spinEditMonthlyCPM.Value : null;
                this.Product.TotalCPM = spinEditTotalCPM.EditValue != null && !(this.Product.RateType == BusinessClasses.RateType.CPM && this.Product.DefaultRate.HasValue && (double)spinEditTotalCPM.Value == this.Product.DefaultRate) ? (double?)spinEditTotalCPM.Value : null;
                this.Product.DurationValue = spinEditDuration.EditValue != null ? (int?)spinEditDuration.Value : null;
                this.Product.ActiveDays = spinEditActiveDays.EditValue != null ? (int?)spinEditActiveDays.Value : null;
                this.Product.TotalAds = spinEditTotalAds.EditValue != null ? (int?)spinEditTotalAds.Value : null;
                this.Product.Comment = memoEditComments.EditValue != null ? memoEditComments.EditValue.ToString() : string.Empty;
                this.Product.Strength1 = comboBoxEditStrengths1.EditValue != null ? comboBoxEditStrengths1.EditValue.ToString() : string.Empty;
                this.Product.Strength2 = comboBoxEditStrengths2.EditValue != null ? comboBoxEditStrengths2.EditValue.ToString() : string.Empty;
                SaveCheckboxValues();
            }
        }

        public void SaveCheckboxValues()
        {
            if (_allowToSave)
            {
                this.Product.ShowCustomWebsite1 = checkEditCustomWebsite1.Checked;
                this.Product.ShowCustomWebsite2 = checkEditCustomWebsite2.Checked;
                this.Product.ShowDuration = checkEditDuration.Checked;
                if (checkEditMonths.Checked)
                    this.Product.DurationType = "Months";
                else if (checkEditWeeks.Checked)
                    this.Product.DurationType = "Weeks";
                this.Product.ShowCommentText = checkEditComments.Checked;
                this.Product.ShowStrength1 = checkEditStrengths1.Checked;
                this.Product.ShowStrength2 = checkEditStrengths2.Checked;
            }
        }


        public void HideDefaultPanel()
        {
            pnMain.BringToFront();
        }

        private void ckTotalMonth_CheckedChanged(object sender, EventArgs e)
        {
            spinEditDuration.Enabled = checkEditDuration.Checked;
            checkEditMonths.Enabled = checkEditDuration.Checked;
            checkEditWeeks.Enabled = checkEditDuration.Checked;
            SaveCheckboxValues();
            UpdateOutputStatus();
            ScheduleBuilderControl.Instance.SettingsNotSaved = true;
        }

        private void checkEditStrengths1_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditStrengths1.Enabled = checkEditStrengths1.Checked;
            SaveCheckboxValues();
            ScheduleBuilderControl.Instance.SettingsNotSaved = true;
        }

        private void checkEditStrengths2_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditStrengths2.Enabled = checkEditStrengths2.Checked;
            SaveCheckboxValues();
            ScheduleBuilderControl.Instance.SettingsNotSaved = true;
        }

        private void checkEditComments_CheckedChanged(object sender, EventArgs e)
        {
            memoEditComments.Enabled = checkEditComments.Checked;
            SaveCheckboxValues();
            ScheduleBuilderControl.Instance.SettingsNotSaved = true;
        }

        public void WebsiteCheckedChanged()
        {
            checkedListBoxControlWebsite.Enabled = FormMain.Instance.buttonItemSchedulesWebsites.Checked;
            if (!FormMain.Instance.buttonItemSchedulesWebsites.Checked)
            {
                checkedListBoxControlWebsite.UnCheckAll();
                checkEditCustomWebsite1.Checked = FormMain.Instance.buttonItemSchedulesWebsites.Checked;
                checkEditCustomWebsite2.Checked = FormMain.Instance.buttonItemSchedulesWebsites.Checked;
                checkEditCustomWebsite1.Enabled = FormMain.Instance.buttonItemSchedulesWebsites.Checked;
                checkEditCustomWebsite2.Enabled = FormMain.Instance.buttonItemSchedulesWebsites.Checked;
            }
            else
            {
                if (checkedListBoxControlWebsite.Items.Count > 0)
                    checkedListBoxControlWebsite.Items[0].CheckState = CheckState.Checked;
                checkEditCustomWebsite1.Enabled = FormMain.Instance.buttonItemSchedulesWebsites.Checked;
                checkEditCustomWebsite2.Enabled = FormMain.Instance.buttonItemSchedulesWebsites.Checked;
            }
            UpdateOutputStatus();
        }

        private void checkEditCustomWebsite1_CheckedChanged(object sender, EventArgs e)
        {
            textEditCustomWebsite1.Enabled = checkEditCustomWebsite1.Checked;
            SaveCheckboxValues();
        }
        private void checkEditCustomWebsite2_CheckedChanged(object sender, EventArgs e)
        {
            textEditCustomWebsite2.Enabled = checkEditCustomWebsite2.Checked;
            SaveCheckboxValues();
        }

        private void Edit_EditValueChanged(object sender, EventArgs e)
        {
            ScheduleBuilderControl.Instance.SettingsNotSaved = true;
        }

        private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                UpdateMonthlyFormula();
                ScheduleBuilderControl.Instance.SettingsNotSaved = true;
            }
        }

        private void spinEditTotal_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                UpdateTotalFormula();
                ScheduleBuilderControl.Instance.SettingsNotSaved = true;
            }
        }

        private void checkedListBoxControlWebsite_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            ScheduleBuilderControl.Instance.SettingsNotSaved = true;
        }

        private void checkEditMonths_CheckedChanged(object sender, EventArgs e)
        {
            checkEditWeeks.CheckedChanged -= new EventHandler(checkEditWeeks_CheckedChanged);
            checkEditWeeks.Checked = !checkEditMonths.Checked;
            if (!this.Product.DurationValue.HasValue)
            {
                if (checkEditMonths.Checked)
                    spinEditDuration.EditValue = this.Product.MonthDuraton;
                else if (checkEditWeeks.Checked)
                    spinEditDuration.EditValue = this.Product.WeeksDuration;
            }
            SaveCheckboxValues();
            checkEditWeeks.CheckedChanged += new EventHandler(checkEditWeeks_CheckedChanged);
        }

        private void checkEditWeeks_CheckedChanged(object sender, EventArgs e)
        {
            checkEditMonths.CheckedChanged -= new EventHandler(checkEditMonths_CheckedChanged);
            checkEditMonths.Checked = !checkEditWeeks.Checked;
            if (!this.Product.DurationValue.HasValue)
            {
                if (checkEditMonths.Checked)
                    spinEditDuration.EditValue = this.Product.MonthDuraton;
                else if (checkEditWeeks.Checked)
                    spinEditDuration.EditValue = this.Product.WeeksDuration;
            }
            SaveCheckboxValues();
            checkEditMonths.CheckedChanged += new EventHandler(checkEditMonths_CheckedChanged);
        }

        #region Output Staff
        public void UpdateOutputStatus()
        {
            string templateName = this.Product.GetSlideSource();
            FormMain.Instance.buttonItemSchedulesPowerPoint.Enabled = !string.IsNullOrEmpty(templateName);
            FormMain.Instance.buttonItemSchedulesEmail.Enabled = !string.IsNullOrEmpty(templateName);
            if (!string.IsNullOrEmpty(templateName))
            {

                ScheduleBuilderControl.Instance.labelControlOutputStatus.ForeColor = Color.Green;
                ScheduleBuilderControl.Instance.labelControlOutputStatus.Text = "Slide Output AVAILABLE";
            }
            else
            {
                ScheduleBuilderControl.Instance.labelControlOutputStatus.ForeColor = Color.Red;
                ScheduleBuilderControl.Instance.labelControlOutputStatus.Text = "Slide Output DISABLED";
            }
        }
        public void Output()
        {
            SaveValues();
            InteropClasses.PowerPointHelper.Instance.AppendOneSheet(this.Product);
        }
        #endregion
    }
}
