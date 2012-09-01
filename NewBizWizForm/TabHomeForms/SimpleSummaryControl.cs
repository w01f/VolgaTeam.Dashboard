using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NewBizWizForm.TabHomeForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class SimpleSummaryControl : UserControl
    {
        private static SimpleSummaryControl _instance;
        public AppManager.SingleParameterDelegate EnableOutput { get; set; }
        public AppManager.SingleParameterDelegate EnableSavedFiles { get; set; }

        public bool AllowToSave { get; set; }
        public bool SettingsNotSaved { get; set; }

        private SimpleSummaryControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laDescription.Font = new Font(laDescription.Font.FontFamily, laDescription.Font.Size - 3, laDescription.Font.Style);
                ckAdvertiser.Font = new Font(ckAdvertiser.Font.FontFamily, ckAdvertiser.Font.Size - 2, ckAdvertiser.Font.Style);
                comboBoxEditAdvertiser.Font = new Font(comboBoxEditAdvertiser.Font.FontFamily, comboBoxEditAdvertiser.Font.Size - 2, comboBoxEditAdvertiser.Font.Style);
                ckDecisionMaker.Font = new Font(ckDecisionMaker.Font.FontFamily, ckDecisionMaker.Font.Size - 2, ckDecisionMaker.Font.Style);
                comboBoxEditDecisionMaker.Font = new Font(comboBoxEditDecisionMaker.Font.FontFamily, comboBoxEditDecisionMaker.Font.Size - 2, comboBoxEditDecisionMaker.Font.Style);
                ckDate.Font = new Font(ckDate.Font.FontFamily, ckDate.Font.Size - 2, ckDate.Font.Style);
                dateEditDate.Font = new Font(dateEditDate.Font.FontFamily, dateEditDate.Font.Size - 2, dateEditDate.Font.Style);
                ckFlightDates.Font = new Font(ckFlightDates.Font.FontFamily, ckFlightDates.Font.Size - 2, ckFlightDates.Font.Style);
                dateEditFligtDatesStart.Font = new Font(dateEditFligtDatesStart.Font.FontFamily, dateEditFligtDatesStart.Font.Size - 2, dateEditFligtDatesStart.Font.Style);
                dateEditFligtDatesEnd.Font = new Font(dateEditFligtDatesEnd.Font.FontFamily, dateEditFligtDatesEnd.Font.Size - 2, dateEditFligtDatesEnd.Font.Style);
                laFlightDatesStart.Font = new Font(laFlightDatesStart.Font.FontFamily, laFlightDatesStart.Font.Size - 2, laFlightDatesStart.Font.Style);
                laFlightDatesEnd.Font = new Font(laFlightDatesEnd.Font.FontFamily, laFlightDatesEnd.Font.Size - 2, laFlightDatesEnd.Font.Style);
                ckPreparedFor.Font = new Font(ckPreparedFor.Font.FontFamily, ckPreparedFor.Font.Size - 3, ckPreparedFor.Font.Style);
                laAdvertiserTag.Font = new Font(laAdvertiserTag.Font.FontFamily, laAdvertiserTag.Font.Size - 2, laAdvertiserTag.Font.Style);
                ckCampaign.Font = new Font(ckCampaign.Font.FontFamily, ckCampaign.Font.Size - 2, ckCampaign.Font.Style);
                laCampaignTag.Font = new Font(laCampaignTag.Font.FontFamily, laCampaignTag.Font.Size - 2, laCampaignTag.Font.Style);
                ckMonthlyInvestment.Font = new Font(ckMonthlyInvestment.Font.FontFamily, ckMonthlyInvestment.Font.Size - 2, ckMonthlyInvestment.Font.Style);
                laMonthlyInvestment.Font = new Font(laMonthlyInvestment.Font.FontFamily, laMonthlyInvestment.Font.Size - 2, laMonthlyInvestment.Font.Style);
                laMonthlyInvestmentTag.Font = new Font(laMonthlyInvestmentTag.Font.FontFamily, laMonthlyInvestmentTag.Font.Size - 2, laMonthlyInvestmentTag.Font.Style);
                ckTotalInvestment.Font = new Font(ckTotalInvestment.Font.FontFamily, ckTotalInvestment.Font.Size - 2, ckTotalInvestment.Font.Style);
                laTotalInvestment.Font = new Font(laTotalInvestment.Font.FontFamily, laTotalInvestment.Font.Size - 2, laTotalInvestment.Font.Style);
                laTotalInvestmentTag.Font = new Font(laTotalInvestmentTag.Font.FontFamily, laTotalInvestmentTag.Font.Size - 2, laTotalInvestmentTag.Font.Style);
                ckEnableTotalsEdit.Font = new Font(ckEnableTotalsEdit.Font.FontFamily, ckEnableTotalsEdit.Font.Size - 2, ckEnableTotalsEdit.Font.Style);
                ckPresentationDate.Font = new Font(ckPresentationDate.Font.FontFamily, ckPresentationDate.Font.Size - 2, ckPresentationDate.Font.Style);
                laPresentationDateTag.Font = new Font(laPresentationDateTag.Font.FontFamily, laPresentationDateTag.Font.Size - 2, laPresentationDateTag.Font.Style);
                ckSignatureLine.Font = new Font(ckSignatureLine.Font.FontFamily, ckSignatureLine.Font.Size - 2, ckSignatureLine.Font.Style);
                laSignatureLineTag.Font = new Font(laSignatureLineTag.Font.FontFamily, laSignatureLineTag.Font.Size - 2, laSignatureLineTag.Font.Style);
                laDetails.Font = new Font(laDetails.Font.FontFamily, laDetails.Font.Size - 3, laDetails.Font.Style);
                laMonthly.Font = new Font(laMonthly.Font.FontFamily, laMonthly.Font.Size - 3, laMonthly.Font.Style);
                laTotal.Font = new Font(laTotal.Font.FontFamily, laTotal.Font.Size - 3, laTotal.Font.Style);
                buttonXSavedFiles.Font = new Font(buttonXSavedFiles.Font.FontFamily, buttonXSavedFiles.Font.Size - 2, buttonXSavedFiles.Font.Style);
            }
            comboBoxEditSlideHeader.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            comboBoxEditSlideHeader.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            comboBoxEditSlideHeader.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            comboBoxEditAdvertiser.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            comboBoxEditAdvertiser.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            comboBoxEditAdvertiser.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            comboBoxEditDecisionMaker.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            comboBoxEditDecisionMaker.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            comboBoxEditDecisionMaker.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditMonthly.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditMonthly.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditMonthly.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            spinEditTotal.MouseUp += new MouseEventHandler(FormMain.Instance.Editor_MouseUp);
            spinEditTotal.MouseDown += new MouseEventHandler(FormMain.Instance.Editor_MouseDown);
            spinEditTotal.Enter += new EventHandler(FormMain.Instance.Editor_Enter);
            tabControlPanelAdvertiser.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Right;
            tabControlPanelDetails.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Right;
            tabControlPanelSlideOutput.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Right;
        }

        public static SimpleSummaryControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SimpleSummaryControl();
                return _instance;
            }
        }

        public void UpdateTotalItems()
        {
            laTotalItems.Text = string.Format("Total Items: {0}", simpleSummaryItemContainer.ItemsCount);
        }

        public void UpdateTotalValues()
        {
            laMonthlyInvestmentTag.Text = (simpleSummaryItemContainer.TotalMonthlyValue.HasValue ? simpleSummaryItemContainer.TotalMonthlyValue.Value : 0).ToString("$#,##0.00");
            pnMonthInvestments.Visible = simpleSummaryItemContainer.ShowMonthlyTotal;
            laTotalInvestmentTag.Text = (simpleSummaryItemContainer.TotalTotalValue.HasValue ? simpleSummaryItemContainer.TotalTotalValue.Value : 0).ToString("$#,##0.00");
            pnTotalInvestments.Visible = simpleSummaryItemContainer.ShowTotalTotal;
        }

        public void UpdateOutputTotalValues()
        {
            laMonthly.Visible = simpleSummaryItemContainer.TotalMonthlyValue.HasValue;
            laTotal.Visible = simpleSummaryItemContainer.TotalTotalValue.HasValue;
            if (!ckEnableTotalsEdit.Checked)
            {
                spinEditMonthly.Value = (decimal)simpleSummaryItemContainer.OutputTotalMonthlyValue;
                spinEditTotal.Value = (decimal)simpleSummaryItemContainer.OutputTotalTotalValue;
            }
        }

        public void UpdateSavedFilesState()
        {
            if (EnableSavedFiles != null)
                EnableSavedFiles(ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.AllowToLoad());
            buttonXSavedFiles.Enabled = ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.AllowToLoad();
        }

        private void LoadSavedState()
        {
            this.AllowToSave = false;

            if (string.IsNullOrEmpty(ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.SlideHeader))
            {
                if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
                    comboBoxEditSlideHeader.SelectedIndex = 0;
            }
            else
            {
                int index = comboBoxEditSlideHeader.Properties.Items.IndexOf(ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.SlideHeader);
                if (index >= 0)
                    comboBoxEditSlideHeader.SelectedIndex = index;
                else
                    comboBoxEditSlideHeader.SelectedIndex = 0;
            }

            ckAdvertiser.Checked = ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowAdvertiser;
            ckPreparedFor.Checked = ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowAdvertiser;
            comboBoxEditAdvertiser.EditValue = string.IsNullOrEmpty(ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.Advertiser) ? null : ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.Advertiser;

            ckDecisionMaker.Checked = ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowDecisionMaker;
            ckSignatureLine.Checked = ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowDecisionMaker;
            comboBoxEditDecisionMaker.EditValue = string.IsNullOrEmpty(ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.DecisionMaker) ? null : ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.DecisionMaker;

            ckDate.Checked = ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowPresentationDate;
            ckPresentationDate.Checked = ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowPresentationDate;
            if (ckDate.Checked)
                dateEditDate.EditValue = ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.PresentationDate != DateTime.MinValue ? (object)ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.PresentationDate : null;
            else
                dateEditDate.EditValue = null;

            ckFlightDates.Checked = ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowFlightDates;
            ckCampaign.Checked = ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowFlightDates;
            if (ckFlightDates.Checked)
            {
                dateEditFligtDatesStart.EditValue = ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.FlightDatesStart != DateTime.MinValue ? (object)ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.FlightDatesStart : null;
                dateEditFligtDatesEnd.EditValue = ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.FlightDatesEnd != DateTime.MinValue ? (object)ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.FlightDatesEnd : null;
            }
            else
            {
                dateEditFligtDatesStart.EditValue = null;
                dateEditFligtDatesEnd.EditValue = null;
            }

            ckMonthlyInvestment.Checked = ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowMonthly;
            ckTotalInvestment.Checked = ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowTotal;

            ckEnableTotalsEdit.Checked = ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.EnableTotalsEdit;
            if (ckEnableTotalsEdit.Checked)
            {
                spinEditMonthly.Value = (decimal)ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.MonthlyValue;
                spinEditTotal.Value = (decimal)ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.TotalValue;
            }

            simpleSummaryItemContainer.LoadItems();

            this.AllowToSave = true;
            this.SettingsNotSaved = false;

            UpdateTotalItems();
            UpdateSavedFilesState();

            tabControl_SelectedTabChanged(null, null);
        }

        private void SaveState()
        {
            simpleSummaryItemContainer.SaveItems();

            ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.SlideHeader = comboBoxEditSlideHeader.EditValue != null ? comboBoxEditSlideHeader.EditValue.ToString() : null;
            ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowAdvertiser = ckAdvertiser.Checked & ckPreparedFor.Checked;
            ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.Advertiser = comboBoxEditAdvertiser.EditValue != null ? comboBoxEditAdvertiser.EditValue.ToString() : string.Empty;

            ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowDecisionMaker = ckDecisionMaker.Checked & ckSignatureLine.Checked;
            ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.DecisionMaker = comboBoxEditDecisionMaker.EditValue != null ? comboBoxEditDecisionMaker.EditValue.ToString() : string.Empty; ckDate.Checked = ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowPresentationDate;

            ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowPresentationDate = ckPresentationDate.Checked & ckDate.Checked;
            ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.PresentationDate = dateEditDate.EditValue != null ? dateEditDate.DateTime : DateTime.MinValue;

            ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowFlightDates = ckFlightDates.Checked & ckCampaign.Checked;
            ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.FlightDatesStart = dateEditFligtDatesStart.EditValue != null ? dateEditFligtDatesStart.DateTime : DateTime.MinValue;
            ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.FlightDatesEnd = dateEditFligtDatesEnd.EditValue != null ? dateEditFligtDatesEnd.DateTime : DateTime.MinValue;

            ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowMonthly = ckMonthlyInvestment.Checked;
            ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.ShowTotal = ckTotalInvestment.Checked;
            ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.EnableTotalsEdit = ckEnableTotalsEdit.Checked;
            ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.MonthlyValue = (double)spinEditMonthly.Value;
            ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.TotalValue = (double)spinEditTotal.Value;

            this.AllowToSave = true;
            this.SettingsNotSaved = false;
        }

        public void LoadFromFile()
        {
            using (FormSavedSimpleSummary form = new FormSavedSimpleSummary())
            {
                if (form.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(form.SelectedFile))
                {
                    ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.Load(form.SelectedFile);
                    LoadSavedState();
                }
            }
        }

        private void CoverControl_Load(object sender, EventArgs e)
        {
            simpleSummaryItemContainer.OutputContainer = simpleSummaryOutputContainer;

            comboBoxEditSlideHeader.Properties.Items.Clear();
            comboBoxEditSlideHeader.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.SimpleSummaryLists.Headers);
            if (comboBoxEditSlideHeader.Properties.Items.Count > 0)
                comboBoxEditSlideHeader.SelectedIndex = 0;

            comboBoxEditAdvertiser.Properties.Items.Clear();
            comboBoxEditAdvertiser.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.Advertisers.Titles);

            comboBoxEditDecisionMaker.Properties.Items.Clear();
            comboBoxEditDecisionMaker.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.DecisionMakers.Titles);

            FormMain.Instance.FormClosed += new FormClosedEventHandler((sender1, e1) =>
            {
                if (this.SettingsNotSaved)
                {
                    SaveState();
                    ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.Save();
                }
            });

            LoadSavedState();
        }

        private void buttonXSavedFiles_Click(object sender, EventArgs e)
        {
            LoadFromFile();
        }

        private void buttonXAddItem_Click(object sender, EventArgs e)
        {
            simpleSummaryItemContainer.AddItem();
            if (simpleSummaryItemContainer.ItemsCount >= 20)
                buttonXAddItem.Enabled = false;
            UpdateTotalItems();
            this.SettingsNotSaved = true;
        }

        public void tabControl_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            laTotalItems.Visible = false;
            buttonXAddItem.Visible = false;

            ckPreparedFor.Visible = false;
            laAdvertiserTag.Visible = false;
            ckCampaign.Visible = false;
            laCampaignTag.Visible = false;
            pnTotals.Visible = false;
            ckPresentationDate.Visible = false;
            laPresentationDateTag.Visible = false;
            ckSignatureLine.Visible = false;
            laSignatureLineTag.Visible = false;
            ckEnableTotalsEdit.Visible = false;
            pbTotals.Visible = false;
            ckMonthlyInvestment.Visible = false;
            spinEditMonthly.Visible = false;
            ckTotalInvestment.Visible = false;
            spinEditTotal.Visible = false;
            pnHeader.Visible = false;
            simpleSummaryOutputContainer.Visible = false;
            pnOutputWarning.Visible = false;


            laDescription.Visible = false;

            if (EnableOutput != null)
                EnableOutput(false);

            if (tabControlSimpleSummary.SelectedTab.Equals(tabItemAdvertiser))
            {
                laDescription.Visible = true;
            }
            else if (tabControlSimpleSummary.SelectedTab.Equals(tabItemCampaign))
            {
                laDescription.Visible = true;
            }
            else if (tabControlSimpleSummary.SelectedTab.Equals(tabItemPaymentDetails))
            {
                laTotalItems.Visible = true;
                buttonXAddItem.Visible = true;
                pnTotals.Visible = true;
                UpdateTotalValues();
            }
            else if (tabControlSimpleSummary.SelectedTab.Equals(tabItemSlideOutput))
            {
                bool itemsComplited = simpleSummaryItemContainer.ItemsComplited;
                pnHeader.Visible = itemsComplited;
                simpleSummaryOutputContainer.Visible = itemsComplited;
                pnOutputWarning.Visible = !itemsComplited;
                ckPreparedFor.Visible = itemsComplited;
                laAdvertiserTag.Visible = itemsComplited;
                ckCampaign.Visible = itemsComplited;
                laCampaignTag.Visible = itemsComplited;
                ckPresentationDate.Visible = itemsComplited;
                laPresentationDateTag.Visible = itemsComplited;
                ckSignatureLine.Visible = itemsComplited;
                laSignatureLineTag.Visible = itemsComplited;
                ckEnableTotalsEdit.Visible = itemsComplited;
                pbTotals.Visible = itemsComplited;
                ckMonthlyInvestment.Visible = itemsComplited;
                spinEditMonthly.Visible = itemsComplited;
                ckTotalInvestment.Visible = itemsComplited;
                spinEditTotal.Visible = itemsComplited;
                UpdateOutputTotalValues();
                simpleSummaryItemContainer.HideTotals();
                simpleSummaryItemContainer.HideDescription();
                laAdvertiserTag.Text = this.Advertiser;
                laCampaignTag.Text = this.CampaignDates;
                laPresentationDateTag.Text = this.PresentationDate;
                laSignatureLineTag.Text = this.DecisionMaker;

                if (EnableOutput != null)
                    EnableOutput(itemsComplited);
            }
        }

        private void ckAdvertiser_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditAdvertiser.Enabled = ckAdvertiser.Checked;
            if (this.AllowToSave)
                this.SettingsNotSaved = true;
        }

        private void ckDecisionMaker_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditDecisionMaker.Enabled = ckDecisionMaker.Checked;
            if (this.AllowToSave)
                this.SettingsNotSaved = true;
        }

        private void ckDate_CheckedChanged(object sender, EventArgs e)
        {
            dateEditDate.Enabled = ckDate.Checked;
            if (this.AllowToSave)
                this.SettingsNotSaved = true;
        }

        private void ckFlightDates_CheckedChanged(object sender, EventArgs e)
        {
            dateEditFligtDatesStart.Enabled = ckFlightDates.Checked;
            dateEditFligtDatesEnd.Enabled = ckFlightDates.Checked;
            if (this.AllowToSave)
                this.SettingsNotSaved = true;
        }

        private void dateEditFligtDatesStart_EditValueChanged(object sender, EventArgs e)
        {
            dateEditFligtDatesEnd.Properties.NullDate = dateEditFligtDatesStart.DateTime;
            if (this.AllowToSave)
                this.SettingsNotSaved = true;
        }

        private void ckEnableTotals_CheckedChanged(object sender, EventArgs e)
        {
            spinEditMonthly.Properties.ReadOnly = !ckEnableTotalsEdit.Checked;
            spinEditTotal.Properties.ReadOnly = !ckEnableTotalsEdit.Checked;
            spinEditMonthly.Enabled = ckEnableTotalsEdit.Checked;
            spinEditTotal.Enabled = ckEnableTotalsEdit.Checked;
            UpdateOutputTotalValues();
            if (this.AllowToSave)
                this.SettingsNotSaved = true;
        }

        private void spinEditMonthly_EditValueChanged(object sender, EventArgs e)
        {
            if (this.AllowToSave && ckEnableTotalsEdit.Checked)
                this.SettingsNotSaved = true;
        }


        private void checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (this.AllowToSave)
                this.SettingsNotSaved = true;
        }

        private void edit_EditValueChanged(object sender, EventArgs e)
        {
            if (this.AllowToSave)
                this.SettingsNotSaved = true;
        }
        #region Output Staff
        public int ItemsCount
        {
            get
            {
                return simpleSummaryItemContainer.ItemTitles.Length;
            }
        }

        public string Title
        {
            get
            {
                return comboBoxEditSlideHeader.EditValue == null ? string.Empty : comboBoxEditSlideHeader.EditValue.ToString();
            }
        }

        public string Advertiser
        {
            get
            {
                if (ckAdvertiser.Checked && ckPreparedFor.Checked)
                    return comboBoxEditAdvertiser.EditValue == null ? string.Empty : comboBoxEditAdvertiser.EditValue.ToString();
                else
                    return string.Empty;
            }
        }

        public string DecisionMaker
        {
            get
            {
                if (ckDecisionMaker.Checked && ckSignatureLine.Checked)
                    return comboBoxEditDecisionMaker.EditValue == null ? string.Empty : comboBoxEditDecisionMaker.EditValue.ToString();
                else
                    return string.Empty;
            }
        }

        public string PresentationDate
        {
            get
            {
                if (ckDate.Checked && ckPresentationDate.Checked)
                    return dateEditDate.EditValue != null ? !dateEditDate.DateTime.Equals(DateTime.MinValue) ? dateEditDate.DateTime.ToString("MMMM dd, yyyy") : string.Empty : string.Empty;
                else
                    return string.Empty;
            }
        }

        public string CampaignDates
        {
            get
            {
                if (ckFlightDates.Checked && ckCampaign.Checked)
                    return (dateEditFligtDatesStart.EditValue != null ? !dateEditFligtDatesStart.DateTime.Equals(DateTime.MinValue) ? dateEditFligtDatesStart.DateTime.ToString("M/d/yyyy") : string.Empty : string.Empty) +
                        (dateEditFligtDatesEnd.EditValue != null ? !dateEditFligtDatesEnd.DateTime.Equals(DateTime.MinValue) ? " - " + dateEditFligtDatesEnd.DateTime.ToString("M/d/yyyy") : string.Empty : string.Empty);
                else
                    return string.Empty;
            }
        }

        public string[] ItemTitles
        {
            get
            {
                return simpleSummaryItemContainer.ItemTitles;
            }
        }

        public string[] ItemDetails
        {
            get
            {
                return simpleSummaryItemContainer.ItemDetails;
            }
        }

        public string[] MonthlyValues
        {
            get
            {
                return simpleSummaryItemContainer.OutputMonthlyValues;
            }
        }

        public string[] TotalValues
        {
            get
            {
                return simpleSummaryItemContainer.OutputTotalValues;
            }
        }

        public string TotalMonthlyValue
        {
            get
            {
                return ckMonthlyInvestment.Checked ? (ckEnableTotalsEdit.Checked ? (double)spinEditMonthly.Value : simpleSummaryItemContainer.OutputTotalMonthlyValue).ToString("$#,##0.00") : string.Empty;
            }
        }

        public string TotalTotalValue
        {
            get
            {
                return ckTotalInvestment.Checked ? (ckEnableTotalsEdit.Checked ? (double)spinEditTotal.Value : simpleSummaryItemContainer.OutputTotalTotalValue).ToString("$#,##0.00") : string.Empty;
            }
        }

        public bool ShowMonhlyHeader
        {
            get
            {
                return simpleSummaryItemContainer.TotalMonthlyValue.HasValue;
            }
        }

        public bool ShowTotalHeader
        {
            get
            {
                return simpleSummaryItemContainer.TotalTotalValue.HasValue;
            }
        }

        public void Output()
        {
            if (!BusinessClasses.ListManager.Instance.Advertisers.Titles.Contains(this.Advertiser) && !string.IsNullOrEmpty(this.Advertiser))
            {
                BusinessClasses.ListManager.Instance.Advertisers.Titles.Add(this.Advertiser);
                BusinessClasses.ListManager.Instance.Advertisers.Save();
            }

            if (!BusinessClasses.ListManager.Instance.DecisionMakers.Titles.Contains(this.DecisionMaker) && !string.IsNullOrEmpty(this.DecisionMaker))
            {
                BusinessClasses.ListManager.Instance.DecisionMakers.Titles.Add(this.DecisionMaker);
                BusinessClasses.ListManager.Instance.DecisionMakers.Save();
            }

            if (this.SettingsNotSaved)
            {
                SaveState();
                ConfigurationClasses.ViewSettingsManager.Instance.SimpleSummaryState.Save();
                UpdateSavedFilesState();
            }

            InteropClasses.PowerPointHelper.Instance.AppendSimpleSummary();
        }
        #endregion
    }
}
