using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace MobileScheduleBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ProductBundleControl : UserControl
    {
        private static ProductBundleControl _instance = null;
        private BusinessClasses.Schedule _localSchedule;
        private bool _allowToSave = true;

        public bool SettingsNotSaved { get; set; }

        public static ProductBundleControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProductBundleControl();
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

        private ProductBundleControl()
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

        private void ProductBundleControl_Load(object sender, EventArgs e)
        {
            spinEditMonthlyImpressions.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditMonthlyImpressions.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditMonthlyImpressions.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditMonthlyInvestment.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditMonthlyInvestment.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditMonthlyInvestment.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditTotalImpressions.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditTotalImpressions.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditTotalImpressions.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditTotalInvestment.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditTotalInvestment.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditTotalInvestment.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
        }

        private void PrepareOutput()
        {
            _localSchedule.ProductBundleSettings.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : (comboBoxEditSlideHeader.Properties.Items.Count > 0 ? comboBoxEditSlideHeader.Properties.Items[0].ToString() : string.Empty);
            List<string> temp = new List<string>();
            temp.Clear();
            if (_localSchedule.ProductBundleSettings.ShowMonthlyImpressions)
                temp.Add("Monthly Impressions");
            if (_localSchedule.ProductBundleSettings.ShowTotalImpressions)
                temp.Add("Total Impressions");
            if (_localSchedule.ProductBundleSettings.ShowMonthlyInvestment)
                temp.Add("Monthly Investment");
            if (_localSchedule.ProductBundleSettings.ShowTotalInvestment)
                temp.Add("Total Investment");

            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        if (temp.Count > 0)
                            _localSchedule.ProductBundleSettings.TotalHeader1 = temp[0];
                        else
                            _localSchedule.ProductBundleSettings.TotalHeader1 = string.Empty;
                        break;
                    case 1:
                        if (temp.Count > 1)
                            _localSchedule.ProductBundleSettings.TotalHeader2 = temp[1];
                        else
                            _localSchedule.ProductBundleSettings.TotalHeader2 = string.Empty;
                        break;
                    case 2:
                        if (temp.Count > 2)
                            _localSchedule.ProductBundleSettings.TotalHeader3 = temp[2];
                        else
                            _localSchedule.ProductBundleSettings.TotalHeader3 = string.Empty;
                        break;
                    case 3:
                        if (temp.Count > 3)
                            _localSchedule.ProductBundleSettings.TotalHeader4 = temp[3];
                        else
                            _localSchedule.ProductBundleSettings.TotalHeader4 = string.Empty;
                        break;
                }
            }
            temp.Clear();
            if (_localSchedule.ProductBundleSettings.ShowMonthlyImpressions)
                temp.Add(spinEditMonthlyImpressions.Value.ToString("#,##0"));
            if (_localSchedule.ProductBundleSettings.ShowMonthlyInvestment)
                temp.Add(spinEditMonthlyInvestment.Value.ToString("$#,###.00"));
            if (_localSchedule.ProductBundleSettings.ShowTotalImpressions)
                temp.Add(spinEditTotalImpressions.Value.ToString("#,##0"));
            if (_localSchedule.ProductBundleSettings.ShowTotalInvestment)
                temp.Add(spinEditTotalInvestment.Value.ToString("$#,###.00"));

            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        if (temp.Count > 0)
                            _localSchedule.ProductBundleSettings.TotalValue1 = temp[0];
                        else
                            _localSchedule.ProductBundleSettings.TotalValue1 = string.Empty;
                        break;
                    case 1:
                        if (temp.Count > 1)
                            _localSchedule.ProductBundleSettings.TotalValue2 = temp[1];
                        else
                            _localSchedule.ProductBundleSettings.TotalValue2 = string.Empty;
                        break;
                    case 2:
                        if (temp.Count > 2)
                            _localSchedule.ProductBundleSettings.TotalValue3 = temp[2];
                        else
                            _localSchedule.ProductBundleSettings.TotalValue3 = string.Empty;
                        break;
                    case 3:
                        if (temp.Count > 3)
                            _localSchedule.ProductBundleSettings.TotalValue4 = temp[3];
                        else
                            _localSchedule.ProductBundleSettings.TotalValue4 = string.Empty;
                        break;
                }
            }

            temp.Clear();
            if (_localSchedule.ProductBundleSettings.ShowMonthlyImpressions && _localSchedule.ProductBundleSettings.ShowMonthlyInvestment)
            {
                temp.Add(checkEditMonthlyCPM.Checked ? checkEditMonthlyCPM.Text : string.Empty);
                temp.Add(string.Empty);
            }
            else if (_localSchedule.ProductBundleSettings.ShowMonthlyImpressions || _localSchedule.ProductBundleSettings.ShowMonthlyInvestment)
                temp.Add(string.Empty);
            if (_localSchedule.ProductBundleSettings.ShowTotalImpressions && _localSchedule.ProductBundleSettings.ShowTotalInvestment)
            {
                temp.Add(checkEditTotalCPM.Checked ? checkEditTotalCPM.Text : string.Empty);
                temp.Add(string.Empty);
            }
            else if (_localSchedule.ProductBundleSettings.ShowTotalImpressions || _localSchedule.ProductBundleSettings.ShowTotalInvestment)
                temp.Add(string.Empty);

            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        if (temp.Count > 0)
                            _localSchedule.ProductBundleSettings.TotalCPM1 = temp[0];
                        else
                            _localSchedule.ProductBundleSettings.TotalCPM1 = string.Empty;
                        break;
                    case 1:
                        if (temp.Count > 1)
                            _localSchedule.ProductBundleSettings.TotalCPM2 = temp[1];
                        else
                            _localSchedule.ProductBundleSettings.TotalCPM2 = string.Empty;
                        break;
                    case 2:
                        if (temp.Count > 2)
                            _localSchedule.ProductBundleSettings.TotalCPM3 = temp[2];
                        else
                            _localSchedule.ProductBundleSettings.TotalCPM3 = string.Empty;
                        break;
                    case 3:
                        if (temp.Count > 3)
                            _localSchedule.ProductBundleSettings.TotalCPM4 = temp[3];
                        else
                            _localSchedule.ProductBundleSettings.TotalCPM4 = string.Empty;
                        break;
                }
            }
        }

        public void LoadSchedule()
        {
            _localSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();

            comboBoxEditSlideHeader.Properties.Items.Clear();
            comboBoxEditSlideHeader.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.SlideHeaders.ToArray());

            labelControlPresentationDate.Text = "Presentation Date: " + _localSchedule.PresentationDate.ToString("MM/dd/yy");
            labelControlFlightDates.Text = "Mobile Campaign Dates: " + _localSchedule.FlightDates;
            labelControlAdvertiser.Text = "Prepared for: " + _localSchedule.BusinessName;
            labelControlDecisionMaker.Text = _localSchedule.DecisionMaker;

            gridControlProductBundle.DataSource = new BindingList<BusinessClasses.Product>(_localSchedule.Products);

            _allowToSave = false;
            if (!string.IsNullOrEmpty(_localSchedule.ProductBundleSettings.SlideHeader))
                comboBoxEditSlideHeader.EditValue = _localSchedule.ProductBundleSettings.SlideHeader;
            else
                if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
                    comboBoxEditSlideHeader.SelectedIndex = 0;

            spinEditMonthlyImpressions.EditValue = _localSchedule.ProductBundleSettings.TotalMonthlyImpressions;
            spinEditMonthlyInvestment.EditValue = _localSchedule.ProductBundleSettings.TotalMonthlyInvestments;
            spinEditTotalImpressions.EditValue = _localSchedule.ProductBundleSettings.TotalImpressions;
            spinEditTotalInvestment.EditValue = _localSchedule.ProductBundleSettings.TotalInvestments;
            double investment = (double)spinEditMonthlyInvestment.Value;
            double impressions = (double)spinEditMonthlyImpressions.Value;
            checkEditMonthlyCPM.Text = "CPM: " + (impressions != 0 ? ((investment / impressions) * 1000) : 0).ToString("$#,###.00");
            investment = (double)spinEditTotalInvestment.Value;
            impressions = (double)spinEditTotalImpressions.Value;
            checkEditTotalCPM.Text = "CPM: " + (impressions != 0 ? ((investment / impressions) * 1000) : 0).ToString("$#,###.00");
            checkEditMonthlyCPM.Checked = _localSchedule.ProductBundleSettings.ShowMonthlyCPM;
            checkEditTotalCPM.Checked = _localSchedule.ProductBundleSettings.ShowTotalCPM;

            checkEditShowTotalsLastSlideBundle.Checked = _localSchedule.ProductBundleSettings.ShowTotalsOnLastOnly;
            checkEditShowTotalsLastSlideBundle.Visible = _localSchedule.Products.Count > 5;

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
            FormMain.Instance.buttonItemProductBundleActiveDays.Checked = _localSchedule.ProductBundleSettings.ShowActiveDays;
            FormMain.Instance.buttonItemProductBundleAdRate.Checked = _localSchedule.ProductBundleSettings.ShowAdRate;
            FormMain.Instance.buttonItemProductBundleDimensions.Checked = _localSchedule.ProductBundleSettings.ShowDimensions;
            FormMain.Instance.buttonItemProductBundleMonthlyImpressions.Checked = _localSchedule.ProductBundleSettings.ShowMonthlyImpressions;
            FormMain.Instance.buttonItemProductBundleMonthlyInvestment.Checked = _localSchedule.ProductBundleSettings.ShowMonthlyInvestment;
            FormMain.Instance.buttonItemProductBundleTotalAds.Checked = _localSchedule.ProductBundleSettings.ShowTotalAds;
            FormMain.Instance.buttonItemProductBundleTotalImpressions.Checked = _localSchedule.ProductBundleSettings.ShowTotalImpressions;
            FormMain.Instance.buttonItemProductBundleTotalInvestment.Checked = _localSchedule.ProductBundleSettings.ShowTotalInvestment;
            FormMain.Instance.buttonItemProductBundleWebsites.Checked = _localSchedule.ProductBundleSettings.ShowWebsites;
            FormMain.Instance.buttonItemProductBundlePowerPoint.Enabled = (_localSchedule.Products.Count > 1) && (_localSchedule.ProductBundleSettings.ShowWebsites |
                                                                                                                   _localSchedule.ProductBundleSettings.ShowDimensions |
                                                                                                                   _localSchedule.ProductBundleSettings.ShowActiveDays |
                                                                                                                   _localSchedule.ProductBundleSettings.ShowAdRate |
                                                                                                                   _localSchedule.ProductBundleSettings.ShowTotalAds);
            FormMain.Instance.buttonItemProductBundleEmail.Enabled = FormMain.Instance.buttonItemProductBundlePowerPoint.Enabled;
        }

        private void UpdateSlideFormat()
        {
            _allowToSave = false;
            FormMain.Instance.buttonItemProductBundleInvestment.Checked = false;
            FormMain.Instance.buttonItemProductBundleImpressions.Checked = false;
            _allowToSave = true;
            FormMain.Instance.buttonItemProductBundleCPM.Checked = false;
            FormMain.Instance.buttonItemProductBundlePowerPoint.Enabled = (_localSchedule.Products.Count > 1) && (_localSchedule.ProductBundleSettings.ShowWebsites |
                                                                                                                   _localSchedule.ProductBundleSettings.ShowDimensions |
                                                                                                                   _localSchedule.ProductBundleSettings.ShowActiveDays |
                                                                                                                   _localSchedule.ProductBundleSettings.ShowAdRate |
                                                                                                                   _localSchedule.ProductBundleSettings.ShowTotalAds);
            FormMain.Instance.buttonItemProductBundleEmail.Enabled = FormMain.Instance.buttonItemProductBundlePowerPoint.Enabled;
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
            labelControlPackageBundleMonthlyImpressions.Visible = _localSchedule.ProductBundleSettings.ShowMonthlyImpressions;
            spinEditMonthlyImpressions.Visible = _localSchedule.ProductBundleSettings.ShowMonthlyImpressions;
            labelControlPackageBundleMonthlyInvestment.Visible = _localSchedule.ProductBundleSettings.ShowMonthlyInvestment;
            spinEditMonthlyInvestment.Visible = _localSchedule.ProductBundleSettings.ShowMonthlyInvestment;
            if (_localSchedule.ProductBundleSettings.ShowMonthlyImpressions && _localSchedule.ProductBundleSettings.ShowMonthlyInvestment)
            {
                checkEditMonthlyCPM.Visible = true;
            }
            else
            {
                checkEditMonthlyCPM.Checked = false;
                checkEditMonthlyCPM.Visible = false;
            }
            pnPackageBundleMonthly.Visible = _localSchedule.ProductBundleSettings.ShowMonthlyImpressions | _localSchedule.ProductBundleSettings.ShowMonthlyInvestment;

            labelControlPackageBundleTotalImpressions.Visible = _localSchedule.ProductBundleSettings.ShowTotalImpressions;
            spinEditTotalImpressions.Visible = _localSchedule.ProductBundleSettings.ShowTotalImpressions;
            labelControlPackageBundleTotalInvestment.Visible = _localSchedule.ProductBundleSettings.ShowTotalInvestment;
            spinEditTotalInvestment.Visible = _localSchedule.ProductBundleSettings.ShowTotalInvestment;
            if (_localSchedule.ProductBundleSettings.ShowTotalImpressions && _localSchedule.ProductBundleSettings.ShowTotalInvestment)
            {
                checkEditTotalCPM.Visible = true;
            }
            else
            {
                checkEditTotalCPM.Checked = false;
                checkEditTotalCPM.Visible = false;
            }
            pnPackageBundleTotal.Visible = _localSchedule.ProductBundleSettings.ShowTotalImpressions | _localSchedule.ProductBundleSettings.ShowTotalInvestment;
            pnPackageBundleTotal.BringToFront();
        }

        private void SaveTotals()
        {
            if (_allowToSave)
            {
                _localSchedule.ProductBundleSettings.ShowMonthlyImpressions = FormMain.Instance.buttonItemProductBundleMonthlyImpressions.Checked;
                _localSchedule.ProductBundleSettings.ShowMonthlyInvestment = FormMain.Instance.buttonItemProductBundleMonthlyInvestment.Checked;
                _localSchedule.ProductBundleSettings.ShowTotalImpressions = FormMain.Instance.buttonItemProductBundleTotalImpressions.Checked;
                _localSchedule.ProductBundleSettings.ShowTotalInvestment = FormMain.Instance.buttonItemProductBundleTotalInvestment.Checked;
                this.SettingsNotSaved = true;
                UpdateTotals();
            }
        }

        private void UpdateGridColumns()
        {
            FormMain.Instance.buttonItemProductBundlePowerPoint.Enabled = (_localSchedule.Products.Count > 1) && (_localSchedule.ProductBundleSettings.ShowWebsites |
                                                                                                       _localSchedule.ProductBundleSettings.ShowDimensions |
                                                                                                       _localSchedule.ProductBundleSettings.ShowActiveDays |
                                                                                                       _localSchedule.ProductBundleSettings.ShowAdRate |
                                                                                                       _localSchedule.ProductBundleSettings.ShowTotalAds);
            FormMain.Instance.buttonItemProductBundleEmail.Enabled = FormMain.Instance.buttonItemProductBundlePowerPoint.Enabled;
        }

        private void SaveGridColumns()
        {
            if (_allowToSave)
            {
                this.SettingsNotSaved = true;
                UpdateGridColumns();
            }
        }

        private void SavePreviewOption()
        {
            if (_allowToSave)
            {
                _localSchedule.ProductBundleSettings.ShowActiveDays = FormMain.Instance.buttonItemProductBundleActiveDays.Checked;
                _localSchedule.ProductBundleSettings.ShowAdRate = FormMain.Instance.buttonItemProductBundleAdRate.Checked;
                _localSchedule.ProductBundleSettings.ShowDimensions = FormMain.Instance.buttonItemProductBundleDimensions.Checked;
                _localSchedule.ProductBundleSettings.ShowTotalAds = FormMain.Instance.buttonItemProductBundleTotalAds.Checked;
                _localSchedule.ProductBundleSettings.ShowWebsites = FormMain.Instance.buttonItemProductBundleWebsites.Checked;
                FormMain.Instance.buttonItemProductBundlePowerPoint.Enabled = (_localSchedule.Products.Count > 1) && (_localSchedule.ProductBundleSettings.ShowWebsites |
                                                                                                       _localSchedule.ProductBundleSettings.ShowDimensions |
                                                                                                       _localSchedule.ProductBundleSettings.ShowActiveDays |
                                                                                                       _localSchedule.ProductBundleSettings.ShowAdRate |
                                                                                                       _localSchedule.ProductBundleSettings.ShowTotalAds);
                FormMain.Instance.buttonItemProductBundleEmail.Enabled = FormMain.Instance.buttonItemProductBundlePowerPoint.Enabled;
                this.SettingsNotSaved = true;
            }
        }

        private void bandedGridViewProductBundle_CalcPreviewText(object sender, DevExpress.XtraGrid.Views.Grid.CalcPreviewTextEventArgs e)
        {
            List<string> previewText = new List<string>();
            if (_localSchedule.ProductBundleSettings.ShowDimensions && !string.IsNullOrEmpty(_localSchedule.Products[e.RowHandle].Dimensions))
                previewText.Add("Ad Dimensions: " + _localSchedule.Products[e.RowHandle].Dimensions);
            if (_localSchedule.ProductBundleSettings.ShowWebsites && !string.IsNullOrEmpty(_localSchedule.Products[e.RowHandle].AllWebsites))
                previewText.Add("Websites: " + _localSchedule.Products[e.RowHandle].AllWebsites);
            if (_localSchedule.ProductBundleSettings.ShowTotalAds && _localSchedule.Products[e.RowHandle].TotalAds.HasValue)
                previewText.Add("Total Ads: " + _localSchedule.Products[e.RowHandle].TotalAds.Value.ToString("#,##0"));
            if (_localSchedule.ProductBundleSettings.ShowActiveDays && _localSchedule.Products[e.RowHandle].ActiveDays.HasValue)
                previewText.Add("Active Days: " + _localSchedule.Products[e.RowHandle].ActiveDays.Value.ToString("#,##0"));
            if (_localSchedule.ProductBundleSettings.ShowAdRate && _localSchedule.Products[e.RowHandle].AdRate.HasValue)
                previewText.Add("Ad Rate: " + _localSchedule.Products[e.RowHandle].AdRate.Value.ToString("$#,###.00"));
            e.PreviewText = string.Join(";  ", previewText.ToArray());
            if (string.IsNullOrEmpty(e.PreviewText))
                e.PreviewText = "            ";
        }

        public void buttonItemProductBundleGridPreviewOptions_CheckedChanged(object sender, EventArgs e)
        {
            SavePreviewOption();
            bandedGridViewProductBundle.RefreshData();
        }

        public void buttonItemProductBundleGridColumnsOptions_CheckedChanged(object sender, EventArgs e)
        {
            SaveGridColumns();
        }

        public void buttonItemProductBundleTotalsOptions_CheckedChanged(object sender, EventArgs e)
        {
            SaveTotals();
        }

        public void buttonItemProductBundleHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("bundle");
        }

        public void buttonItemProductBundlePowerPoint_Click(object sender, EventArgs e)
        {
            PrepareOutput();
            using (ToolForms.FormGridType formGridType = new ToolForms.FormGridType())
            {
                DialogResult result = formGridType.ShowDialog();
                if (result != DialogResult.Cancel)
                {
                    using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                    {
                        formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
                        formProgress.TopMost = true;
                        formProgress.Show();
                        InteropClasses.PowerPointHelper.Instance.AppendProductBundle(_localSchedule, result == DialogResult.No);
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

        public void buttonItemProductBundleEmail_Click(object sender, EventArgs e)
        {
            PrepareOutput();
            using (ToolForms.FormGridType formGridType = new ToolForms.FormGridType())
            {
                DialogResult result = formGridType.ShowDialog();
                if (result != DialogResult.Cancel)
                {
                    using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                    {
                        formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
                        formProgress.TopMost = true;
                        formProgress.Show();
                        string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                        InteropClasses.PowerPointHelper.Instance.PrepareBundleEmail(tempFileName, _localSchedule, result == DialogResult.No);
                        formProgress.Close();
                        if (File.Exists(tempFileName))
                            using (ToolForms.FormEmail formEmail = new ToolForms.FormEmail())
                            {
                                formEmail.Text = "Email this Mobile Schedule";
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

        private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {

                double investment = (double)spinEditMonthlyInvestment.Value;
                double impressions = (double)spinEditMonthlyImpressions.Value;
                checkEditMonthlyCPM.Text = "CPM: " + (impressions != 0 ? ((investment / impressions) * 1000) : 0).ToString("$#,###.00");

                _localSchedule.ProductBundleSettings.TotalMonthlyImpressions = spinEditMonthlyImpressions.EditValue != null ? (double?)spinEditMonthlyImpressions.Value : null;
                _localSchedule.ProductBundleSettings.TotalMonthlyInvestments = spinEditMonthlyInvestment.EditValue != null ? (double?)spinEditMonthlyInvestment.Value : null;
                this.SettingsNotSaved = true;
            }
        }

        private void spinEditTotalImpressions_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                double investment = (double)spinEditTotalInvestment.Value;
                double impressions = (double)spinEditTotalImpressions.Value;
                checkEditTotalCPM.Text = "CPM: " + (impressions != 0 ? ((investment / impressions) * 1000) : 0).ToString("$#,###.00");

                _localSchedule.ProductBundleSettings.TotalImpressions = spinEditTotalImpressions.EditValue != null ? (double?)spinEditTotalImpressions.Value : null;
                _localSchedule.ProductBundleSettings.TotalInvestments = spinEditTotalInvestment.EditValue != null ? (double?)spinEditTotalInvestment.Value : null;
                this.SettingsNotSaved = true;
            }
        }

        private void checkEditShowTotalsLastSlide_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                _localSchedule.ProductBundleSettings.ShowTotalsOnLastOnly = (sender as DevExpress.XtraEditors.CheckEdit).Checked;
                this.SettingsNotSaved = true;
            }
        }

        private void comboBoxEditSlideHeader_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                _localSchedule.ProductBundleSettings.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : (comboBoxEditSlideHeader.Properties.Items.Count > 0 ? comboBoxEditSlideHeader.Properties.Items[0].ToString() : string.Empty);
                this.SettingsNotSaved = true;
            }
        }

        private void checkEditCPM_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                _localSchedule.ProductBundleSettings.ShowMonthlyCPM = checkEditMonthlyCPM.Checked;
                _localSchedule.ProductBundleSettings.ShowTotalCPM = checkEditTotalCPM.Checked;
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
            using (ToolForms.FormNewSchedule from = new ToolForms.FormNewSchedule())
            {
                from.Text = "Save Schedule";
                from.laLogo.Text = "Please set a new name for your Schedule:";
                if (from.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(from.ScheduleName))
                    {
                        if (SaveSchedule(from.ScheduleName))
                            AppManager.ShowInformation("Schedule was saved");
                    }
                    else
                    {
                        AppManager.ShowWarning("Schedule Name can't be empty");
                    }
                }
            }
        }
    }
}
