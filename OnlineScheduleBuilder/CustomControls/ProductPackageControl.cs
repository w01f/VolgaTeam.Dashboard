using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OnlineScheduleBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ProductPackageControl : UserControl
    {
        public BusinessClasses.ProductPackage ProductPackage { get; set; }
        private bool _allowToSave = false;

        public ProductPackageControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            if ((base.CreateGraphics()).DpiX > 96)
            {
                Font font = new Font(labelControlProduct.Font.FontFamily, labelControlProduct.Font.Size - 4, labelControlProduct.Font.Style);
                labelControlProduct.Font = font;
                labelControlWebsitelogo.Font = font;
                labelControlComments.Font = font;
                labelControlAdCampaign.Font = font;
                labelControlImpressions.Font = font;
                labelControlInvestment.Font = font;
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
            switch (this.ProductPackage.Formula)
            {
                case BusinessClasses.FormulaType.CPM:
                    this.ProductPackage.MonthlyInvestment = spinEditMonthlyInvestment.EditValue != null ? (double?)spinEditMonthlyInvestment.Value : null;
                    this.ProductPackage.MonthlyImpressions = spinEditMonthlyImpressions.EditValue != null ? (double?)spinEditMonthlyImpressions.Value : null;

                    spinEditMonthlyCPM.EditValue = this.ProductPackage.MonthlyCPMCalculated;
                    break;
                case BusinessClasses.FormulaType.Investment:
                    this.ProductPackage.MonthlyImpressions = spinEditMonthlyImpressions.EditValue != null ? (double?)spinEditMonthlyImpressions.Value : null;
                    this.ProductPackage.MonthlyCPM = spinEditMonthlyCPM.EditValue != null ? (double?)spinEditMonthlyCPM.Value : null;

                    spinEditMonthlyInvestment.EditValue = this.ProductPackage.MonthlyInvestmentCalculated;
                    break;
                case BusinessClasses.FormulaType.Impressions:
                    this.ProductPackage.MonthlyInvestment = spinEditMonthlyInvestment.EditValue != null ? (double?)spinEditMonthlyInvestment.Value : null;
                    this.ProductPackage.MonthlyCPM = spinEditMonthlyCPM.EditValue != null ? (double?)spinEditMonthlyCPM.Value : null;

                    spinEditMonthlyImpressions.EditValue = this.ProductPackage.MonthlyImpressionsCalculated;
                    break;
            }
        }

        private void UpdateTotalFormula()
        {
            switch (this.ProductPackage.Formula)
            {
                case BusinessClasses.FormulaType.CPM:
                    this.ProductPackage.TotalInvestment = spinEditTotalInvestment.EditValue != null ? (double?)spinEditTotalInvestment.Value : null;
                    this.ProductPackage.TotalImpressions = spinEditTotalImpressions.EditValue != null ? (double?)spinEditTotalImpressions.Value : null;

                    spinEditTotalCPM.EditValue = this.ProductPackage.TotalCPMCalculated;
                    break;
                case BusinessClasses.FormulaType.Investment:
                    this.ProductPackage.TotalImpressions = spinEditTotalImpressions.EditValue != null ? (double?)spinEditTotalImpressions.Value : null;
                    this.ProductPackage.TotalCPM = spinEditTotalCPM.EditValue != null ? (double?)spinEditTotalCPM.Value : null;

                    spinEditTotalInvestment.EditValue = this.ProductPackage.TotalInvestmentCalculated;
                    break;
                case BusinessClasses.FormulaType.Impressions:
                    this.ProductPackage.TotalInvestment = spinEditTotalInvestment.EditValue != null ? (double?)spinEditTotalInvestment.Value : null;
                    this.ProductPackage.TotalCPM = spinEditTotalCPM.EditValue != null ? (double?)spinEditTotalCPM.Value : null;

                    spinEditTotalImpressions.EditValue = this.ProductPackage.TotalImpressionsCalculated;
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

            this.Text = this.ProductPackage.Name.Replace("&", "&&");

            if (!string.IsNullOrEmpty(this.ProductPackage.SlideHeader))
                WebPackageControl.Instance.comboBoxEditSlideHeader.EditValue = this.ProductPackage.SlideHeader;
            else
                WebPackageControl.Instance.comboBoxEditSlideHeader.EditValue = null;
            WebPackageControl.Instance.labelControlPresentationDate.Text = "Presentation Date: " + this.ProductPackage.Parent.PresentationDate.ToString("MM/dd/yy");

            checkedListBoxControlWebsite.UnCheckAll();
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlWebsite.Items)
                if (this.ProductPackage.Websites.Contains(item.Value.ToString()))
                    item.CheckState = CheckState.Checked;
            if (checkedListBoxControlWebsite.CheckedItems.Count == 0)
                if (checkedListBoxControlWebsite.Items.Count > 0)
                    checkedListBoxControlWebsite.Items[0].CheckState = CheckState.Checked;
            checkEditCustomWebsite1.Checked = this.ProductPackage.ShowCustomWebsite1;
            textEditCustomWebsite1.EditValue = this.ProductPackage.CustomWebsite1;
            checkEditCustomWebsite2.Checked = this.ProductPackage.ShowCustomWebsite2;
            textEditCustomWebsite2.EditValue = this.ProductPackage.CustomWebsite2;
            labelControlProduct.Text = this.ProductPackage.Name;
            memoEditDescription.EditValue = this.ProductPackage.Description;
            spinEditAdRate.EditValue = this.ProductPackage.AdRate;
            spinEditMonthlyInvestment.EditValue = this.ProductPackage.MonthlyInvestmentCalculated;
            spinEditTotalInvestment.EditValue = this.ProductPackage.TotalInvestmentCalculated;
            spinEditMonthlyImpressions.EditValue = this.ProductPackage.MonthlyImpressionsCalculated;
            spinEditTotalImpressions.EditValue = this.ProductPackage.TotalImpressionsCalculated;
            spinEditMonthlyCPM.EditValue = this.ProductPackage.MonthlyCPMCalculated;
            spinEditTotalCPM.EditValue = this.ProductPackage.TotalCPMCalculated;
            labelControlFlightDates.Text = this.ProductPackage.Parent.FlightDates;
            checkEditDuration.Checked = this.ProductPackage.ShowDuration;
            switch (this.ProductPackage.DurationType)
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
            if (this.ProductPackage.DurationValue.HasValue)
            {
                spinEditDuration.EditValue = this.ProductPackage.DurationValue;
            }
            else
            {
                if (checkEditMonths.Checked)
                    spinEditDuration.EditValue = this.ProductPackage.MonthDuraton;
                else if (checkEditWeeks.Checked)
                    spinEditDuration.EditValue = this.ProductPackage.WeeksDuration;
            }
            spinEditActiveDays.EditValue = this.ProductPackage.ActiveDays;
            spinEditTotalAds.EditValue = this.ProductPackage.TotalAds;
            checkEditComments.Checked = this.ProductPackage.ShowCommentText;
            memoEditComments.EditValue = this.ProductPackage.Comment;
            checkEditStrengths1.Checked = this.ProductPackage.ShowStrength1;
            comboBoxEditStrengths1.EditValue = this.ProductPackage.Strength1;
            checkEditStrengths2.Checked = this.ProductPackage.ShowStrength2;
            comboBoxEditStrengths2.EditValue = this.ProductPackage.Strength2;

            UpdateView();
            _allowToSave = true;
        }

        public void UpdateView()
        {

            WebPackageControl.Instance.labelControlPresentationDate.Visible = this.ProductPackage.ShowPresentationDate;
            if (this.ProductPackage.ShowBusinessName && this.ProductPackage.ShowDecisionMaker)
            {
                WebPackageControl.Instance.labelControlAdvertiser.Visible = true;
                WebPackageControl.Instance.labelControlAdvertiser.Text = "Prepared For: " + this.ProductPackage.Parent.BusinessName + "\n\n" + this.ProductPackage.Parent.DecisionMaker;
            }
            else if (!this.ProductPackage.ShowBusinessName && this.ProductPackage.ShowDecisionMaker)
            {
                WebPackageControl.Instance.labelControlAdvertiser.Visible = true;
                WebPackageControl.Instance.labelControlAdvertiser.Text = this.ProductPackage.Parent.DecisionMaker;
            }
            else if (this.ProductPackage.ShowBusinessName && !this.ProductPackage.ShowDecisionMaker)
            {
                WebPackageControl.Instance.labelControlAdvertiser.Visible = true;
                WebPackageControl.Instance.labelControlAdvertiser.Text = "Prepared For: " + this.ProductPackage.Parent.BusinessName;
            }
            else
            {
                WebPackageControl.Instance.labelControlAdvertiser.Visible = false;
            }

            labelControlProduct.Visible = true;
            pbProductLogo.Visible = true;
            memoEditDescription.Visible = true;
            pnProduct.Visible = true;
            pnWebsites.Dock = DockStyle.Bottom;

            labelControlFlightDates.Visible = this.ProductPackage.ShowFlightDates;

            int heightImpressions = pnImpressionsLogo.Height + 25;
            pnImpressions.Visible = this.ProductPackage.ShowMonthlyImpressions | this.ProductPackage.ShowTotalImpressions;
            pnMonthlyImpressions.Visible = this.ProductPackage.ShowMonthlyImpressions;
            labelControlMonthlyCPM.Visible = this.ProductPackage.ShowMonthlyCPM;
            spinEditMonthlyCPM.Visible = this.ProductPackage.ShowMonthlyCPM;
            if (this.ProductPackage.ShowMonthlyImpressions)
            {
                pnMonthlyImpressions.SendToBack();
                heightImpressions += pnMonthlyImpressions.Height;
            }
            pnTotalImpressions.Visible = this.ProductPackage.ShowTotalImpressions;
            labelControlTotalCPM.Visible = this.ProductPackage.ShowTotalCPM;
            spinEditTotalCPM.Visible = this.ProductPackage.ShowTotalCPM;
            if (this.ProductPackage.ShowTotalImpressions)
            {
                pnTotalImpressions.BringToFront();
                heightImpressions += pnTotalImpressions.Height;
            }
            pnImpressionsLogo.SendToBack();
            if (this.ProductPackage.ShowMonthlyImpressions | this.ProductPackage.ShowTotalImpressions)
            {
                pnImpressions.Height = heightImpressions;
                pnImpressions.SendToBack();
            }

            int heightInvestment = pnInvestmentLogo.Height + 25;
            pnInvestment.Visible = this.ProductPackage.ShowAdRate | this.ProductPackage.ShowMonthlyInvestment | this.ProductPackage.ShowTotalInvestment;
            pnAdRate.Visible = this.ProductPackage.ShowAdRate;
            if (this.ProductPackage.ShowAdRate)
            {
                pnAdRate.SendToBack();
                heightInvestment += pnAdRate.Height;
            }
            pnMonthlyInvestment.Visible = this.ProductPackage.ShowMonthlyInvestment;
            if (this.ProductPackage.ShowMonthlyInvestment)
                heightInvestment += pnMonthlyInvestment.Height;
            pnTotalInvestment.Visible = this.ProductPackage.ShowTotalInvestment;
            if (this.ProductPackage.ShowTotalInvestment)
            {
                pnTotalInvestment.BringToFront();
                heightInvestment += pnTotalInvestment.Height;
            }
            pnInvestmentLogo.SendToBack();
            if (this.ProductPackage.ShowAdRate | this.ProductPackage.ShowMonthlyInvestment | this.ProductPackage.ShowTotalInvestment)
            {
                pnInvestment.Height = heightInvestment;
                pnInvestment.SendToBack();
            }

            pnTotalMonth.Visible = this.ProductPackage.ShowFlightDates;
            if (this.ProductPackage.ShowFlightDates)
                pnTotalMonth.SendToBack();
            pnActiveDays.Visible = this.ProductPackage.ShowActiveDays;
            pnTotalAds.Visible = this.ProductPackage.ShowTotalAds;
            if (this.ProductPackage.ShowTotalAds)
                pnTotalAds.BringToFront();
            pnTotals.Visible = this.ProductPackage.ShowFlightDates | this.ProductPackage.ShowActiveDays | this.ProductPackage.ShowTotalAds;
            if (this.ProductPackage.ShowFlightDates | this.ProductPackage.ShowActiveDays | this.ProductPackage.ShowTotalAds)
                pnTotals.SendToBack();

            pnAdCampaignLogo.Visible = this.ProductPackage.ShowFlightDates | this.ProductPackage.ShowActiveDays | this.ProductPackage.ShowTotalAds;
            if (this.ProductPackage.ShowFlightDates | this.ProductPackage.ShowActiveDays | this.ProductPackage.ShowTotalAds)
                pnAdCampaignLogo.SendToBack();

            pnComments.Visible = this.ProductPackage.ShowComments;
            if (this.ProductPackage.ShowComments)
                pnComments.BringToFront();

            switch (this.ProductPackage.Formula)
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
            WebPackageControl.Instance.pictureBoxFormula.Image = this.ProductPackage.ShowCPMButton ? Properties.Resources.InvestmentLogo : Properties.Resources.InvestmentLogoGray;
            WebPackageControl.Instance.labelControlFormula.Enabled = this.ProductPackage.ShowCPMButton;
            WebPackageControl.Instance.checkEditFormulaCPM.Enabled = this.ProductPackage.ShowCPMButton;
            WebPackageControl.Instance.checkEditFormulaInvestment.Enabled = this.ProductPackage.ShowCPMButton;
            WebPackageControl.Instance.checkEditFormulaImpressions.Enabled = this.ProductPackage.ShowCPMButton;
        }

        public void SaveValues()
        {
            if (_allowToSave)
            {
                this.ProductPackage.SlideHeader = WebPackageControl.Instance.comboBoxEditSlideHeader.EditValue != null ? WebPackageControl.Instance.comboBoxEditSlideHeader.EditValue.ToString() : (WebPackageControl.Instance.comboBoxEditSlideHeader.Properties.Items.Count > 0 ? WebPackageControl.Instance.comboBoxEditSlideHeader.Properties.Items[0].ToString() : string.Empty);
                this.ProductPackage.Websites.Clear();
                if (FormMain.Instance.buttonItemWebPackageWebsites.Checked)
                    foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in checkedListBoxControlWebsite.Items)
                        if (item.CheckState == CheckState.Checked)
                            this.ProductPackage.Websites.Add(item.Value.ToString());
                this.ProductPackage.CustomWebsite1 = textEditCustomWebsite1.EditValue != null ? textEditCustomWebsite1.EditValue.ToString() : string.Empty;
                this.ProductPackage.CustomWebsite2 = textEditCustomWebsite2.EditValue != null ? textEditCustomWebsite2.EditValue.ToString() : string.Empty;
                this.ProductPackage.Description = memoEditDescription.EditValue != null ? memoEditDescription.EditValue.ToString() : string.Empty;
                this.ProductPackage.AdRate = spinEditAdRate.EditValue != null ? (double?)spinEditAdRate.Value : null;
                this.ProductPackage.MonthlyInvestment = spinEditMonthlyInvestment.EditValue != null ? (double?)spinEditMonthlyInvestment.Value : null;
                this.ProductPackage.TotalInvestment = spinEditTotalInvestment.EditValue != null ? (double?)spinEditTotalInvestment.Value : null;
                this.ProductPackage.MonthlyImpressions = spinEditMonthlyImpressions.EditValue != null ? (double?)spinEditMonthlyImpressions.Value : null;
                this.ProductPackage.TotalImpressions = spinEditTotalImpressions.EditValue != null ? (double?)spinEditTotalImpressions.Value : null;
                this.ProductPackage.MonthlyCPM = spinEditMonthlyCPM.EditValue != null ? (double?)spinEditMonthlyCPM.Value : null;
                this.ProductPackage.TotalCPM = spinEditTotalCPM.EditValue != null ? (double?)spinEditTotalCPM.Value : null;
                this.ProductPackage.DurationValue = spinEditDuration.EditValue != null ? (int?)spinEditDuration.Value : null;
                this.ProductPackage.ActiveDays = spinEditActiveDays.EditValue != null ? (int?)spinEditActiveDays.Value : null;
                this.ProductPackage.TotalAds = spinEditTotalAds.EditValue != null ? (int?)spinEditTotalAds.Value : null;
                this.ProductPackage.Comment = memoEditComments.EditValue != null ? memoEditComments.EditValue.ToString() : string.Empty;
                this.ProductPackage.Strength1 = comboBoxEditStrengths1.EditValue != null ? comboBoxEditStrengths1.EditValue.ToString() : string.Empty;
                this.ProductPackage.Strength2 = comboBoxEditStrengths2.EditValue != null ? comboBoxEditStrengths2.EditValue.ToString() : string.Empty;
                SaveCheckboxValues();
            }
        }

        public void SaveCheckboxValues()
        {
            if (_allowToSave)
            {
                this.ProductPackage.ShowCustomWebsite1 = checkEditCustomWebsite1.Checked;
                this.ProductPackage.ShowCustomWebsite2 = checkEditCustomWebsite2.Checked;
                this.ProductPackage.ShowDuration = checkEditDuration.Checked;
                if (checkEditMonths.Checked)
                    this.ProductPackage.DurationType = "Months";
                else if (checkEditWeeks.Checked)
                    this.ProductPackage.DurationType = "Weeks";
                this.ProductPackage.ShowCommentText = checkEditComments.Checked;
                this.ProductPackage.ShowStrength1 = checkEditStrengths1.Checked;
                this.ProductPackage.ShowStrength2 = checkEditStrengths2.Checked;
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
            WebPackageControl.Instance.UpdateOutputStatus();
            WebPackageControl.Instance.SettingsNotSaved = true;
        }

        private void checkEditStrengths1_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditStrengths1.Enabled = checkEditStrengths1.Checked;
            SaveCheckboxValues();
            WebPackageControl.Instance.SettingsNotSaved = true;
        }

        private void checkEditStrengths2_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditStrengths2.Enabled = checkEditStrengths2.Checked;
            SaveCheckboxValues();
            WebPackageControl.Instance.SettingsNotSaved = true;
        }

        private void checkEditComments_CheckedChanged(object sender, EventArgs e)
        {
            memoEditComments.Enabled = checkEditComments.Checked;
            SaveCheckboxValues();
            WebPackageControl.Instance.SettingsNotSaved = true;
        }

        public void WebsiteCheckedChanged()
        {
            checkedListBoxControlWebsite.Enabled = FormMain.Instance.buttonItemWebPackageWebsites.Checked;
            if (!FormMain.Instance.buttonItemWebPackageWebsites.Checked)
            {
                checkedListBoxControlWebsite.UnCheckAll();
                checkEditCustomWebsite1.Checked = FormMain.Instance.buttonItemWebPackageWebsites.Checked;
                checkEditCustomWebsite2.Checked = FormMain.Instance.buttonItemWebPackageWebsites.Checked;
                checkEditCustomWebsite1.Enabled = FormMain.Instance.buttonItemWebPackageWebsites.Checked;
                checkEditCustomWebsite2.Enabled = FormMain.Instance.buttonItemWebPackageWebsites.Checked;
            }
            else
            {
                if (checkedListBoxControlWebsite.Items.Count > 0)
                    checkedListBoxControlWebsite.Items[0].CheckState = CheckState.Checked;
                checkEditCustomWebsite1.Enabled = FormMain.Instance.buttonItemWebPackageWebsites.Checked;
                checkEditCustomWebsite2.Enabled = FormMain.Instance.buttonItemWebPackageWebsites.Checked;
            }
            WebPackageControl.Instance.UpdateOutputStatus();
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
            WebPackageControl.Instance.SettingsNotSaved = true;
        }

        private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                UpdateMonthlyFormula();
                WebPackageControl.Instance.SettingsNotSaved = true;
            }
        }

        private void spinEditTotal_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                UpdateTotalFormula();
                WebPackageControl.Instance.SettingsNotSaved = true;
            }
        }

        private void checkedListBoxControlWebsite_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            WebPackageControl.Instance.SettingsNotSaved = true;
        }

        private void checkEditMonths_CheckedChanged(object sender, EventArgs e)
        {
            checkEditWeeks.CheckedChanged -= new EventHandler(checkEditWeeks_CheckedChanged);
            checkEditWeeks.Checked = !checkEditMonths.Checked;
            if (!this.ProductPackage.DurationValue.HasValue)
            {
                if (checkEditMonths.Checked)
                    spinEditDuration.EditValue = this.ProductPackage.MonthDuraton;
                else if (checkEditWeeks.Checked)
                    spinEditDuration.EditValue = this.ProductPackage.WeeksDuration;
            }
            SaveCheckboxValues();
            checkEditWeeks.CheckedChanged += new EventHandler(checkEditWeeks_CheckedChanged);
        }

        private void checkEditWeeks_CheckedChanged(object sender, EventArgs e)
        {
            checkEditMonths.CheckedChanged -= new EventHandler(checkEditMonths_CheckedChanged);
            checkEditMonths.Checked = !checkEditWeeks.Checked;
            if (!this.ProductPackage.DurationValue.HasValue)
            {
                if (checkEditMonths.Checked)
                    spinEditDuration.EditValue = this.ProductPackage.MonthDuraton;
                else if (checkEditWeeks.Checked)
                    spinEditDuration.EditValue = this.ProductPackage.WeeksDuration;
            }
            SaveCheckboxValues();
            checkEditMonths.CheckedChanged += new EventHandler(checkEditMonths_CheckedChanged);
        }

        #region Output Staff
        public void Output()
        {
            SaveValues();
            using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
            {
                formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
                formProgress.TopMost = true;
                formProgress.Show();
                InteropClasses.PowerPointHelper.Instance.AppendOneSheetPackage(this.ProductPackage);
                formProgress.Close();
            }
            using (ToolForms.FormSlideOutput formOutput = new ToolForms.FormSlideOutput())
            {
                if (formOutput.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    AppManager.ActivateForm(FormMain.Instance.Handle, true, false);
            }
        }

        public void Email()
        {
            SaveValues();
            using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
            {
                formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
                formProgress.TopMost = true;
                formProgress.Show();
                string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                InteropClasses.PowerPointHelper.Instance.PreparePackageEmail(tempFileName, this.ProductPackage);
                formProgress.Close();
                if (File.Exists(tempFileName))
                    using (ToolForms.FormEmail formEmail = new ToolForms.FormEmail())
                    {
                        formEmail.Text = "Email this Online Schedule";
                        formEmail.PresentationFile = tempFileName;
                        ConfigurationClasses.RegistryHelper.MainFormHandle = formEmail.Handle;
                        ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                        formEmail.ShowDialog();
                        ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
                        ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                    }
            }
        }
        #endregion
    }
}
