using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace OnlineScheduleBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ProductSummaryControl : UserControl
    {
        private static ProductSummaryControl _instance = null;
        private BusinessClasses.Schedule _localSchedule;
        private bool _allowToSave = true;

        public bool SettingsNotSaved { get; set; }

        public static ProductSummaryControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProductSummaryControl();
                return _instance;
            }
        }

        public static void RemoveInstance()
        {
            try
            {
                _instance.Dispose();
            }
            catch
            {
            }
            finally
            {
                _instance = null;
            }
        }

        public bool AllowToLeaveControl
        {
            get
            {
                bool result = false;
                if (this.SettingsNotSaved)
                {
                    if (AppManager.ShowWarningQuestion("Schedule settings have changed.\nDo you want to save changes?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (SaveSchedule())
                            result = true;
                    }
                }
                else
                    result = true;
                return result;
            }
        }

        private ProductSummaryControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            BusinessClasses.ScheduleManager.Instance.SettingsSaved += new EventHandler<BusinessClasses.SavingingEventArgs>((sender, e) =>
            {
                if (sender != this)
                    LoadSchedule();
            });
            this.SettingsNotSaved = false;
        }

        public void LoadSchedule()
        {
            _localSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();

            comboBoxEditSlideHeader.Properties.Items.Clear();
            comboBoxEditSlideHeader.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.SlideHeaders.ToArray());

            labelControlPresentationDate.Text = "Presentation Date: " + _localSchedule.PresentationDate.ToString("MM/dd/yy");
            labelControlFlightDates.Text = "Online Campaign Dates: " + _localSchedule.FlightDates;
            labelControlAdvertiser.Text = "Prepared for: " + _localSchedule.BusinessName;
            labelControlDecisionMaker.Text = _localSchedule.DecisionMaker;

            labelControlMonthlyImpressions.Text = "Monthly Impressions: " + _localSchedule.MonthlyImpressions.ToString("#,##0");
            labelControlMonthlyInvestment.Text = "Monthly Investment: " + _localSchedule.MonthlyInvestment.ToString("$#,###.00");
            labelControlTotalImpressions.Text = "Total Impressions: " + _localSchedule.TotalImpressions.ToString("#,##0");
            labelControlTotalInvestment.Text = "Total Investment: " + _localSchedule.TotalInvestment.ToString("$#,###.00");

            gridControlProductSummary.DataSource = new BindingList<BusinessClasses.Product>(_localSchedule.Products);

            _allowToSave = false;
            if (!string.IsNullOrEmpty(_localSchedule.ProductSummarySettings.SlideHeader))
                comboBoxEditSlideHeader.EditValue = _localSchedule.ProductSummarySettings.SlideHeader;
            else
                if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
                    comboBoxEditSlideHeader.SelectedIndex = 0;

            checkEditShowTotalsLastSlideSummary.Checked = _localSchedule.ProductSummarySettings.ShowTotalsOnLastOnly;
            checkEditShowTotalsLastSlideSummary.Visible = _localSchedule.Products.Count > 5;

            UpdateToogledButtons();
            UpdateSlideFormat();
            UpdateTotals();
            UpdateGridColumns();
            _allowToSave = true;

            this.SettingsNotSaved = false;
        }

        private bool SaveSchedule(string scheduleName = "")
        {
            if (!string.IsNullOrEmpty(scheduleName))
                _localSchedule.Name = scheduleName;
            BusinessClasses.ScheduleManager.Instance.SaveSchedule(_localSchedule, string.IsNullOrEmpty(scheduleName), this);
            this.SettingsNotSaved = false;
            return true;
        }

        private void UpdateToogledButtons()
        {
            FormMain.Instance.buttonItemProductSummaryActiveDays.Checked = _localSchedule.ProductSummarySettings.ShowActiveDays;
            FormMain.Instance.buttonItemProductSummaryAdRate.Checked = _localSchedule.ProductSummarySettings.ShowAdRate;
            FormMain.Instance.buttonItemProductSummaryCPM.Checked = _localSchedule.ProductSummarySettings.ShowCPM;
            FormMain.Instance.buttonItemProductSummaryDimensions.Checked = _localSchedule.ProductSummarySettings.ShowDimensions;
            FormMain.Instance.buttonItemProductSummaryImpressions.Checked = _localSchedule.ProductSummarySettings.ShowImpressions;
            FormMain.Instance.buttonItemProductSummaryInvestment.Checked = _localSchedule.ProductSummarySettings.ShowInvestment;
            FormMain.Instance.buttonItemProductSummaryMonthlyImpressions.Checked = _localSchedule.ProductSummarySettings.ShowMonthlyImpressions;
            FormMain.Instance.buttonItemProductSummaryMonthlyInvestment.Checked = _localSchedule.ProductSummarySettings.ShowMonthlyInvestment;
            FormMain.Instance.buttonItemProductSummaryTotalAds.Checked = _localSchedule.ProductSummarySettings.ShowTotalAds;
            FormMain.Instance.buttonItemProductSummaryTotalImpressions.Checked = _localSchedule.ProductSummarySettings.ShowTotalImpressions;
            FormMain.Instance.buttonItemProductSummaryTotalInvestment.Checked = _localSchedule.ProductSummarySettings.ShowTotalInvestment;
            FormMain.Instance.buttonItemProductSummaryWebsites.Checked = _localSchedule.ProductSummarySettings.ShowWebsites;
            FormMain.Instance.buttonItemProductSummaryPowerPoint.Enabled = (_localSchedule.Products.Count > 1) && (_localSchedule.ProductSummarySettings.ShowWebsites |
                                                                                                                   _localSchedule.ProductSummarySettings.ShowDimensions |
                                                                                                                   _localSchedule.ProductSummarySettings.ShowActiveDays |
                                                                                                                   _localSchedule.ProductSummarySettings.ShowAdRate |
                                                                                                                   _localSchedule.ProductSummarySettings.ShowTotalAds |
                                                                                                                   _localSchedule.ProductSummarySettings.ShowImpressions |
                                                                                                                   _localSchedule.ProductSummarySettings.ShowInvestment |
                                                                                                                   _localSchedule.ProductSummarySettings.ShowCPM);
            FormMain.Instance.buttonItemProductSummaryEmail.Enabled = FormMain.Instance.buttonItemProductSummaryPowerPoint.Enabled;
        }

        private void UpdateSlideFormat()
        {
            FormMain.Instance.buttonItemProductSummaryPowerPoint.Enabled = (_localSchedule.Products.Count > 1) && (_localSchedule.ProductSummarySettings.ShowWebsites |
                                                                                                                   _localSchedule.ProductSummarySettings.ShowDimensions |
                                                                                                                   _localSchedule.ProductSummarySettings.ShowActiveDays |
                                                                                                                   _localSchedule.ProductSummarySettings.ShowAdRate |
                                                                                                                   _localSchedule.ProductSummarySettings.ShowTotalAds |
                                                                                                                   _localSchedule.ProductSummarySettings.ShowImpressions |
                                                                                                                   _localSchedule.ProductSummarySettings.ShowInvestment |
                                                                                                                   _localSchedule.ProductSummarySettings.ShowCPM);
            FormMain.Instance.buttonItemProductSummaryEmail.Enabled = FormMain.Instance.buttonItemProductSummaryPowerPoint.Enabled;
        }

        private void SaveSlideFormat()
        {
            if (_allowToSave)
            {
                this.SettingsNotSaved = true;
                UpdateSlideFormat();
            }
        }

        private void UpdateTotals()
        {
            labelControlTotalImpressions.Visible = _localSchedule.ProductSummarySettings.ShowTotalImpressions;
            labelControlTotalInvestment.Visible = _localSchedule.ProductSummarySettings.ShowTotalInvestment;
            pnPackageSummaryTotal.Visible = _localSchedule.ProductSummarySettings.ShowTotalImpressions || _localSchedule.ProductSummarySettings.ShowTotalInvestment;
            if (_localSchedule.ProductSummarySettings.ShowTotalImpressions || _localSchedule.ProductSummarySettings.ShowTotalInvestment)
                pnPackageSummaryTotal.BringToFront();

            labelControlMonthlyImpressions.Visible = _localSchedule.ProductSummarySettings.ShowMonthlyImpressions;
            labelControlMonthlyInvestment.Visible = _localSchedule.ProductSummarySettings.ShowMonthlyInvestment;
            pnPackageSummaryMonthly.Visible = _localSchedule.ProductSummarySettings.ShowMonthlyImpressions || _localSchedule.ProductSummarySettings.ShowMonthlyInvestment;
            if (_localSchedule.ProductSummarySettings.ShowMonthlyImpressions || _localSchedule.ProductSummarySettings.ShowMonthlyInvestment)
                pnPackageSummaryMonthly.SendToBack();
        }

        private void SaveTotals()
        {
            if (_allowToSave)
            {
                _localSchedule.ProductSummarySettings.ShowMonthlyImpressions = FormMain.Instance.buttonItemProductSummaryMonthlyImpressions.Checked;
                _localSchedule.ProductSummarySettings.ShowMonthlyInvestment = FormMain.Instance.buttonItemProductSummaryMonthlyInvestment.Checked;
                _localSchedule.ProductSummarySettings.ShowTotalImpressions = FormMain.Instance.buttonItemProductSummaryTotalImpressions.Checked;
                _localSchedule.ProductSummarySettings.ShowTotalInvestment = FormMain.Instance.buttonItemProductSummaryTotalInvestment.Checked;
                this.SettingsNotSaved = true;
                UpdateTotals();
            }
        }

        private void UpdateGridColumns()
        {
            bandedGridColumnMonthlyCPM.Visible = _localSchedule.ProductSummarySettings.ShowCPM;
            bandedGridColumnMonthlyImpressions.Visible = _localSchedule.ProductSummarySettings.ShowImpressions;
            bandedGridColumnMonthlyInvestment.Visible = _localSchedule.ProductSummarySettings.ShowInvestment;
            gridBandMonthly.Visible = _localSchedule.ProductSummarySettings.ShowCPM | _localSchedule.ProductSummarySettings.ShowImpressions | _localSchedule.ProductSummarySettings.ShowInvestment;

            bandedGridColumnTotalCPM.Visible = _localSchedule.ProductSummarySettings.ShowCPM;
            bandedGridColumnTotalImpressions.Visible = _localSchedule.ProductSummarySettings.ShowImpressions;
            bandedGridColumnTotalInvestment.Visible = _localSchedule.ProductSummarySettings.ShowInvestment;
            gridBandTotal.Visible = _localSchedule.ProductSummarySettings.ShowCPM | _localSchedule.ProductSummarySettings.ShowImpressions | _localSchedule.ProductSummarySettings.ShowInvestment;
            FormMain.Instance.buttonItemProductSummaryPowerPoint.Enabled = (_localSchedule.Products.Count > 1) && (_localSchedule.ProductSummarySettings.ShowWebsites |
                                                                                                       _localSchedule.ProductSummarySettings.ShowDimensions |
                                                                                                       _localSchedule.ProductSummarySettings.ShowActiveDays |
                                                                                                       _localSchedule.ProductSummarySettings.ShowAdRate |
                                                                                                       _localSchedule.ProductSummarySettings.ShowTotalAds |
                                                                                                       _localSchedule.ProductSummarySettings.ShowImpressions |
                                                                                                       _localSchedule.ProductSummarySettings.ShowInvestment |
                                                                                                       _localSchedule.ProductSummarySettings.ShowCPM);
            FormMain.Instance.buttonItemProductSummaryEmail.Enabled = FormMain.Instance.buttonItemProductSummaryPowerPoint.Enabled;
        }

        private void SaveGridColumns()
        {
            if (_allowToSave)
            {
                _localSchedule.ProductSummarySettings.ShowCPM = FormMain.Instance.buttonItemProductSummaryCPM.Checked;
                _localSchedule.ProductSummarySettings.ShowImpressions = FormMain.Instance.buttonItemProductSummaryImpressions.Checked;
                _localSchedule.ProductSummarySettings.ShowInvestment = FormMain.Instance.buttonItemProductSummaryInvestment.Checked;
                this.SettingsNotSaved = true;
                UpdateGridColumns();
            }
        }

        private void SavePreviewOption()
        {
            if (_allowToSave)
            {
                _localSchedule.ProductSummarySettings.ShowActiveDays = FormMain.Instance.buttonItemProductSummaryActiveDays.Checked;
                _localSchedule.ProductSummarySettings.ShowAdRate = FormMain.Instance.buttonItemProductSummaryAdRate.Checked;
                _localSchedule.ProductSummarySettings.ShowDimensions = FormMain.Instance.buttonItemProductSummaryDimensions.Checked;
                _localSchedule.ProductSummarySettings.ShowTotalAds = FormMain.Instance.buttonItemProductSummaryTotalAds.Checked;
                _localSchedule.ProductSummarySettings.ShowWebsites = FormMain.Instance.buttonItemProductSummaryWebsites.Checked;
                FormMain.Instance.buttonItemProductSummaryPowerPoint.Enabled = (_localSchedule.Products.Count > 1) && (_localSchedule.ProductSummarySettings.ShowWebsites |
                                                                                                       _localSchedule.ProductSummarySettings.ShowDimensions |
                                                                                                       _localSchedule.ProductSummarySettings.ShowActiveDays |
                                                                                                       _localSchedule.ProductSummarySettings.ShowAdRate |
                                                                                                       _localSchedule.ProductSummarySettings.ShowTotalAds |
                                                                                                       _localSchedule.ProductSummarySettings.ShowImpressions |
                                                                                                       _localSchedule.ProductSummarySettings.ShowInvestment |
                                                                                                       _localSchedule.ProductSummarySettings.ShowCPM);
                FormMain.Instance.buttonItemProductSummaryEmail.Enabled = FormMain.Instance.buttonItemProductSummaryPowerPoint.Enabled;
                this.SettingsNotSaved = true;
            }
        }

        private void bandedGridViewProductSummary_CalcPreviewText(object sender, DevExpress.XtraGrid.Views.Grid.CalcPreviewTextEventArgs e)
        {
            List<string> previewText = new List<string>();
            if (_localSchedule.ProductSummarySettings.ShowDimensions && !string.IsNullOrEmpty(_localSchedule.Products[e.RowHandle].Dimensions))
                previewText.Add("Ad Dimensions: " + _localSchedule.Products[e.RowHandle].Dimensions);
            if (_localSchedule.ProductSummarySettings.ShowWebsites && !string.IsNullOrEmpty(_localSchedule.Products[e.RowHandle].AllWebsites))
                previewText.Add("Websites: " + _localSchedule.Products[e.RowHandle].AllWebsites);
            if (_localSchedule.ProductSummarySettings.ShowTotalAds && _localSchedule.Products[e.RowHandle].TotalAds.HasValue)
                previewText.Add("Total Ads: " + _localSchedule.Products[e.RowHandle].TotalAds.Value.ToString("#,##0"));
            if (_localSchedule.ProductSummarySettings.ShowActiveDays && _localSchedule.Products[e.RowHandle].ActiveDays.HasValue)
                previewText.Add("Active Days: " + _localSchedule.Products[e.RowHandle].ActiveDays.Value.ToString("#,##0"));
            if (_localSchedule.ProductSummarySettings.ShowAdRate && _localSchedule.Products[e.RowHandle].AdRate.HasValue)
                previewText.Add("Ad Rate: " + _localSchedule.Products[e.RowHandle].AdRate.Value.ToString("$#,###.00"));
            e.PreviewText = string.Join(";  ", previewText.ToArray());
            if (string.IsNullOrEmpty(e.PreviewText))
                e.PreviewText = "            ";
        }

        private void PrepareOutput()
        {
            _localSchedule.ProductSummarySettings.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : (comboBoxEditSlideHeader.Properties.Items.Count > 0 ? comboBoxEditSlideHeader.Properties.Items[0].ToString() : string.Empty);
            List<string> temp = new List<string>();
            temp.Clear();
            if (_localSchedule.ProductSummarySettings.ShowMonthlyImpressions)
                temp.Add("Monthly Impressions");
            if (_localSchedule.ProductSummarySettings.ShowTotalImpressions)
                temp.Add("Total Impressions");
            if (_localSchedule.ProductSummarySettings.ShowMonthlyInvestment)
                temp.Add("Monthly Investment");
            if (_localSchedule.ProductSummarySettings.ShowTotalInvestment)
                temp.Add("Total Investment");

            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        if (temp.Count > 0)
                            _localSchedule.ProductSummarySettings.TotalHeader1 = temp[0];
                        else
                            _localSchedule.ProductSummarySettings.TotalHeader1 = string.Empty;
                        break;
                    case 1:
                        if (temp.Count > 1)
                            _localSchedule.ProductSummarySettings.TotalHeader2 = temp[1];
                        else
                            _localSchedule.ProductSummarySettings.TotalHeader2 = string.Empty;
                        break;
                    case 2:
                        if (temp.Count > 2)
                            _localSchedule.ProductSummarySettings.TotalHeader3 = temp[2];
                        else
                            _localSchedule.ProductSummarySettings.TotalHeader3 = string.Empty;
                        break;
                    case 3:
                        if (temp.Count > 3)
                            _localSchedule.ProductSummarySettings.TotalHeader4 = temp[3];
                        else
                            _localSchedule.ProductSummarySettings.TotalHeader4 = string.Empty;
                        break;
                }
            }
            temp.Clear();
            if (_localSchedule.ProductSummarySettings.ShowMonthlyImpressions)
                temp.Add(_localSchedule.MonthlyImpressions.ToString("#,##0"));
            if (_localSchedule.ProductSummarySettings.ShowTotalImpressions)
                temp.Add(_localSchedule.TotalImpressions.ToString("#,##0"));
            if (_localSchedule.ProductSummarySettings.ShowMonthlyInvestment)
                temp.Add(_localSchedule.MonthlyInvestment.ToString("$#,###.00"));
            if (_localSchedule.ProductSummarySettings.ShowTotalInvestment)
                temp.Add(_localSchedule.TotalInvestment.ToString("$#,###.00"));

            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        if (temp.Count > 0)
                            _localSchedule.ProductSummarySettings.TotalValue1 = temp[0];
                        else
                            _localSchedule.ProductSummarySettings.TotalValue1 = string.Empty;
                        break;
                    case 1:
                        if (temp.Count > 1)
                            _localSchedule.ProductSummarySettings.TotalValue2 = temp[1];
                        else
                            _localSchedule.ProductSummarySettings.TotalValue2 = string.Empty;
                        break;
                    case 2:
                        if (temp.Count > 2)
                            _localSchedule.ProductSummarySettings.TotalValue3 = temp[2];
                        else
                            _localSchedule.ProductSummarySettings.TotalValue3 = string.Empty;
                        break;
                    case 3:
                        if (temp.Count > 3)
                            _localSchedule.ProductSummarySettings.TotalValue4 = temp[3];
                        else
                            _localSchedule.ProductSummarySettings.TotalValue4 = string.Empty;
                        break;
                }
            }
        }

        public void buttonItemProductSummaryGridPreviewOptions_CheckedChanged(object sender, EventArgs e)
        {
            SavePreviewOption();
            bandedGridViewProductSummary.RefreshData();
        }

        public void buttonItemProductSummaryGridColumnsOptions_CheckedChanged(object sender, EventArgs e)
        {
            SaveGridColumns();
        }

        public void buttonItemProductSummaryTotalsOptions_CheckedChanged(object sender, EventArgs e)
        {
            SaveTotals();
        }

        public void buttonItemProductSummaryHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("summary");
        }

        public void buttonItemProductSummaryPowerPoint_Click(object sender, EventArgs e)
        {
            using (ToolForms.FormGridType formGridType = new ToolForms.FormGridType())
            {
                DialogResult result = formGridType.ShowDialog();
                if (result != DialogResult.Cancel)
                {
                    PrepareOutput();
                    using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                    {
                        formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
                        formProgress.TopMost = true;
                        formProgress.Show();
                        InteropClasses.PowerPointHelper.Instance.AppendProductSummary(_localSchedule, result == DialogResult.No);
                        formProgress.Close();
                    }
                    using (ToolForms.FormSlideOutput formOutput = new ToolForms.FormSlideOutput())
                    {
                        if (formOutput.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                            AppManager.ActivateForm(FormMain.Instance.Handle, true, false);
                    }
                }
            }
        }

        public void buttonItemProductSummaryEmail_Click(object sender, EventArgs e)
        {
            using (ToolForms.FormGridType formGridType = new ToolForms.FormGridType())
            {
                DialogResult result = formGridType.ShowDialog();
                if (result != DialogResult.Cancel)
                {
                    PrepareOutput();
                    using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                    {
                        formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
                        formProgress.TopMost = true;
                        formProgress.Show();
                        string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                        InteropClasses.PowerPointHelper.Instance.PrepareSummaryEmail(tempFileName, _localSchedule, result == DialogResult.No);
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
            }
        }

        private void checkEditShowTotalsLastSlide_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                _localSchedule.ProductSummarySettings.ShowTotalsOnLastOnly = (sender as DevExpress.XtraEditors.CheckEdit).Checked;
                this.SettingsNotSaved = true;
            }
        }

        private void comboBoxEditSlideHeader_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                _localSchedule.ProductSummarySettings.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : (comboBoxEditSlideHeader.Properties.Items.Count > 0 ? comboBoxEditSlideHeader.Properties.Items[0].ToString() : string.Empty);
                this.SettingsNotSaved = true;
            }
        }

        public void buttonItemProductummarySave_Click(object sender, EventArgs e)
        {
            SaveSchedule();
            AppManager.ShowInformation("Schedule Saved");
        }

        public void buttonItemProductummarySaveAs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = ConfigurationClasses.SettingsManager.Instance.SaveFolder;
                dialog.Title = "Save Schedule As...";
                dialog.Filter = "Schedule Files|*.xml";
                dialog.FileName = _localSchedule.Name + ".xml";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (SaveSchedule(dialog.FileName.Replace(".xml", "")))
                        AppManager.ShowInformation("Schedule was saved");
                }
            }
        }
    }
}
