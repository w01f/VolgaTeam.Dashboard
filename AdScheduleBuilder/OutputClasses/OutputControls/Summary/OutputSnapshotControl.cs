using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class OutputSnapshotControl : UserControl, ISummaryOutputControl
    {
        private static OutputSnapshotControl _instance;
        public BusinessClasses.Schedule LocalSchedule { get; set; }
        private bool _allowToSave = false;

        public bool SettingsNotSaved { get; set; }

        public DevComponents.DotNetBar.SuperTooltipInfo HelpToolTip { get; private set; }

        private OutputSnapshotControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            this.HelpToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me understand how to use the Advertising Snapshot slide", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);

            BusinessClasses.ScheduleManager.Instance.SettingsSaved += new EventHandler<BusinessClasses.SavingingEventArgs>((sender, e) =>
            {
                if (sender != this)
                    UpdateOutput(e.QuickSave);
            });
        }

        public static OutputSnapshotControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OutputSnapshotControl();
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

        private void LoadView()
        {
            _allowToSave = false;
            FormMain.Instance.buttonItemSnapshotPercentOfPage.Enabled = BusinessClasses.ListManager.Instance.ShareUnits.Count > 0;

            FormMain.Instance.buttonItemSnapshotAvgAdCost.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgCost;
            FormMain.Instance.buttonItemSnapshotAvgFinalCost.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgFinalCost;
            FormMain.Instance.buttonItemSnapshotAvgPCI.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgPCI;
            FormMain.Instance.buttonItemSnapshotDelivery.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDelivery;
            FormMain.Instance.buttonItemSnapshotDimensions.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDimensions;
            FormMain.Instance.buttonItemSnapshotLogo.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowLogo;
            FormMain.Instance.buttonItemSnapshotPageSize.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPageSize;
            FormMain.Instance.buttonItemSnapshotPercentOfPage.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPercentOfPage & FormMain.Instance.buttonItemSnapshotPercentOfPage.Enabled;
            FormMain.Instance.buttonItemSnapshotReadership.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowReadership;
            FormMain.Instance.buttonItemSnapshotSquare.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowSquare;
            FormMain.Instance.buttonItemSnapshotTotalColorRate.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalColor;
            FormMain.Instance.buttonItemSnapshotTotalDiscounts.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalDiscounts;
            FormMain.Instance.buttonItemSnapshotTotalFinalCost.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalFinalCost;
            FormMain.Instance.buttonItemSnapshotTotalInserts.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalInserts;
            FormMain.Instance.buttonItemSnapshotTotalSquare.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalSquare;
            _allowToSave = true;
        }

        private void SaveView()
        {
            if (_allowToSave)
            {
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgCost = FormMain.Instance.buttonItemSnapshotAvgAdCost.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgFinalCost = FormMain.Instance.buttonItemSnapshotAvgFinalCost.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgPCI = FormMain.Instance.buttonItemSnapshotAvgPCI.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDelivery = FormMain.Instance.buttonItemSnapshotDelivery.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDimensions = FormMain.Instance.buttonItemSnapshotDimensions.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowLogo = FormMain.Instance.buttonItemSnapshotLogo.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPageSize = FormMain.Instance.buttonItemSnapshotPageSize.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPercentOfPage = FormMain.Instance.buttonItemSnapshotPercentOfPage.Checked & FormMain.Instance.buttonItemSnapshotPercentOfPage.Enabled;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowReadership = FormMain.Instance.buttonItemSnapshotReadership.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowSquare = FormMain.Instance.buttonItemSnapshotSquare.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalColor = FormMain.Instance.buttonItemSnapshotTotalColorRate.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalDiscounts = FormMain.Instance.buttonItemSnapshotTotalDiscounts.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalFinalCost = FormMain.Instance.buttonItemSnapshotTotalFinalCost.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalInserts = FormMain.Instance.buttonItemSnapshotTotalInserts.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalSquare = FormMain.Instance.buttonItemSnapshotTotalSquare.Checked;
                this.SettingsNotSaved = true;
            }
        }

        public void UpdateOutput(bool quickLoad)
        {
            this.LocalSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();
            laScheduleWindow.Text = string.Format("{0} - {1}", new object[] { this.LocalSchedule.FlightDateStart.ToString("MM/dd/yy"), this.LocalSchedule.FlightDateEnd.ToString("MM/dd/yy") });
            laScheduleName.Text = this.LocalSchedule.Name;
            laAdvertiser.Text = this.LocalSchedule.BusinessName + (!string.IsNullOrEmpty(this.LocalSchedule.AccountNumber) ? (" - " + this.LocalSchedule.AccountNumber) : string.Empty);
            if (!quickLoad)
            {
                _allowToSave = false;
                comboBoxEditSchedule.Properties.Items.Clear();
                comboBoxEditSchedule.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.OutputHeaders.ToArray());
                if (string.IsNullOrEmpty(this.LocalSchedule.ViewSettings.SnapshotViewSettings.SlideHeader))
                {
                    if (comboBoxEditSchedule.Properties.Items.Count > 0)
                        comboBoxEditSchedule.SelectedIndex = 0;
                }
                else
                {
                    int index = comboBoxEditSchedule.Properties.Items.IndexOf(this.LocalSchedule.ViewSettings.SnapshotViewSettings.SlideHeader);
                    if (index >= 0)
                        comboBoxEditSchedule.SelectedIndex = index;
                    else
                        comboBoxEditSchedule.SelectedIndex = 0;
                }
                checkEditSchedule.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowSlideHeader;
                checkEditBusinessName.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAdvertiser;
                checkEditDate.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPresentationDate;
                checkEditDecisionMaker.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDecisionMaker;
                checkEditFlightDates.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowFlightDates;
                _allowToSave = true;

                checkEditDate.Text = this.LocalSchedule.PresentationDateObject != null ? this.LocalSchedule.PresentationDate.ToString("MM/dd/yy") : string.Empty;
                checkEditBusinessName.Text = " " + this.LocalSchedule.BusinessName + (!string.IsNullOrEmpty(this.LocalSchedule.AccountNumber) ? (" - " + this.LocalSchedule.AccountNumber) : string.Empty);
                checkEditDecisionMaker.Text = this.LocalSchedule.DecisionMaker;
                checkEditFlightDates.Text = " " + this.LocalSchedule.FlightDateStart.ToString("MM/dd/yy") + " - " + this.LocalSchedule.FlightDateEnd.ToString("MM/dd/yy");

                outputSnapshotContainer.UpdateSnapshots(this.LocalSchedule);

                LoadView();
            }
            this.SettingsNotSaved = false;
        }

        public void OpenHelp()
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("snapshot");
        }

        private bool AllowShowColumn()
        {
            int count = 0;
            if (FormMain.Instance.buttonItemSnapshotAvgAdCost.Checked)
                count++;
            if (FormMain.Instance.buttonItemSnapshotAvgFinalCost.Checked)
                count++;
            if (FormMain.Instance.buttonItemSnapshotAvgPCI.Checked)
                count++;
            if (FormMain.Instance.buttonItemSnapshotDelivery.Checked)
                count++;
            if (FormMain.Instance.buttonItemSnapshotDimensions.Checked)
                count++;
            if (FormMain.Instance.buttonItemSnapshotPageSize.Checked)
                count++;
            if (FormMain.Instance.buttonItemSnapshotPercentOfPage.Checked)
                count++;
            if (FormMain.Instance.buttonItemSnapshotReadership.Checked)
                count++;
            if (FormMain.Instance.buttonItemSnapshotSquare.Checked)
                count++;
            if (FormMain.Instance.buttonItemSnapshotTotalColorRate.Checked)
                count++;
            if (FormMain.Instance.buttonItemSnapshotTotalDiscounts.Checked)
                count++;
            if (FormMain.Instance.buttonItemSnapshotTotalFinalCost.Checked)
                count++;
            if (FormMain.Instance.buttonItemSnapshotTotalInserts.Checked)
                count++;
            if (FormMain.Instance.buttonItemSnapshotTotalSquare.Checked)
                count++;
            return count < 5;
        }

        private void checkEditSchedule_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEditSchedule.Enabled = checkEditSchedule.Checked;
            checkEdit_CheckedChanged(null, null);
        }

        private void comboBoxEditSchedule_EditValueChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.SlideHeader = comboBoxEditSchedule.EditValue != null ? comboBoxEditSchedule.EditValue.ToString() : string.Empty;
                this.SettingsNotSaved = true;
            }
        }

        private void checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAdvertiser = checkEditBusinessName.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDecisionMaker = checkEditDecisionMaker.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowFlightDates = checkEditFlightDates.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPresentationDate = checkEditDate.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowSlideHeader = checkEditSchedule.Checked;
                this.SettingsNotSaved = true;
            }
        }

        public void buttonItemSnapshotToggle_CheckedChanged(object sender, EventArgs e)
        {
            SaveView();
            outputSnapshotContainer.UpdateColumns(this.LocalSchedule);
        }

        public void buttonItemSnapshotButton_Click(object sender, EventArgs e)
        {
            if (!(sender as DevComponents.DotNetBar.ButtonItem).Checked)
            {
                if (AllowShowColumn())
                    (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
                else
                    AppManager.ShowWarning("You already have 5 items enabled");
            }
            else
                (sender as DevComponents.DotNetBar.ButtonItem).Checked = false;
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

        public void SetFocusToScrollbar()
        {
            xtraScrollableControl.Focus();
        }

        #region Output Staff
        public int OutputFileIndex
        {
            get
            {
                return FormMain.Instance.buttonItemSnapshotLogo.Checked ? 1 : 2;
            }
        }

        public string Header
        {
            get
            {
                string result = string.Empty;
                if (comboBoxEditSchedule.EditValue != null && checkEditSchedule.Checked)
                    result = comboBoxEditSchedule.EditValue.ToString();
                return result;
            }
        }

        public string Date
        {
            get
            {
                string result = string.Empty;
                if (checkEditDate.Checked)
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

        public string FlightDates
        {
            get
            {
                string result = string.Empty;
                if (checkEditFlightDates.Checked)
                    result = checkEditFlightDates.Text;
                return result;
            }
        }

        public string[] LogoFiles
        {
            get
            {
                List<string> result = new List<string>();
                foreach (BusinessClasses.Publication publication in this.LocalSchedule.Publications.Where(x => x.Inserts.Count > 0))
                {
                    string fileName = System.IO.Path.GetTempFileName();
                    publication.BigLogo.Save(fileName);
                    result.Add(fileName);
                }
                return result.ToArray();
            }
        }

        public string[] PublicationNames
        {
            get
            {
                List<string> result = new List<string>();
                foreach (BusinessClasses.Publication publication in this.LocalSchedule.Publications.Where(x => x.Inserts.Count > 0))
                    result.Add(publication.Name.Replace("&&", "&"));
                return result.ToArray();
            }
        }

        public string[][] AdSpecs
        {
            get
            {
                List<string[]> result = new List<string[]>();
                foreach (BusinessClasses.Publication publication in this.LocalSchedule.Publications.Where(x => x.Inserts.Count > 0))
                {
                    List<string> adSpecs = new List<string>();
                    if (this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalInserts)
                        adSpecs.Add("Total Ads: " + publication.TotalInserts.ToString("#,##0"));
                    if (this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPageSize && !string.IsNullOrEmpty(publication.SizeOptions.PageSize))
                        adSpecs.Add("Page Size: " + publication.SizeOptions.PageSize);
                    if (this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPercentOfPage && !string.IsNullOrEmpty(publication.SizeOptions.PercentOfPage) && publication.AdPricingStrategy == BusinessClasses.AdPricingStrategies.SharePage)
                        adSpecs.Add(publication.SizeOptions.PercentOfPage + " Share of Page");
                    if (this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDimensions && !string.IsNullOrEmpty(publication.SizeOptions.Dimensions))
                        adSpecs.Add("Dimensions: " + publication.SizeOptions.Dimensions);
                    if (this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowSquare && publication.SizeOptions.Square.HasValue && publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage)
                        adSpecs.Add("Col. Inches: " + publication.SizeOptions.Square.Value.ToString("#,###.00"));
                    if (this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalSquare && publication.TotalSquare.HasValue && publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage)
                        adSpecs.Add("Total Inches: " + publication.TotalSquare.Value.ToString("#,###.00"));
                    if (this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgPCI && publication.AvgPCIRate > 0)
                        adSpecs.Add("Avg PCI: " + publication.AvgPCIRate.ToString("$#,###.00"));
                    if (this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgCost)
                        adSpecs.Add("BW Ad Cost: " + publication.AvgADRate.ToString("$#,###.00"));
                    if (this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgFinalCost)
                        adSpecs.Add("Final Ad Cost: " + publication.AvgFinalRate.ToString("$#,###.00"));
                    if (this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalFinalCost)
                        adSpecs.Add("Investment: " + publication.TotalFinalRate.ToString("$#,###.00"));
                    if (this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalColor)
                        adSpecs.Add("Total Color: " + (publication.TotalColorPricingCalculated > 0 ? publication.TotalColorPricingCalculated.ToString("$#,###.00") : publication.Inserts.FirstOrDefault().ColorPricingObject.ToString()));
                    if (this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalDiscounts)
                        adSpecs.Add("Discounts: " + publication.TotalDiscountRate.ToString("$#,###.00"));
                    if (this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowReadership && publication.DailyReadership != null)
                        adSpecs.Add("Readership: " + publication.DailyReadership.Value.ToString("#,##0"));
                    if (this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDelivery && publication.DailyDelivery != null)
                        adSpecs.Add("Delivery: " + publication.DailyDelivery.Value.ToString("#,##0"));
                    result.Add(adSpecs.ToArray());
                }
                return result.ToArray();
            }
        }

        public void PrintOutput()
        {
            using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
            {
                formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
                formProgress.TopMost = true;
                formProgress.Show();
                InteropClasses.PowerPointHelper.Instance.AppendSnapshot();
                formProgress.Close();
            }
            using (OutputForms.FormSlideOutput formOutput = new OutputForms.FormSlideOutput())
            {
                if (formOutput.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    AppManager.ActivateForm(FormMain.Instance.Handle, true, false);
            }
        }

        public void Email()
        {
            using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
            {
                formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
                formProgress.TopMost = true;
                formProgress.Show();
                string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                InteropClasses.PowerPointHelper.Instance.PrepareSnapshotEmail(tempFileName);
                formProgress.Close();
                if (File.Exists(tempFileName))
                    using (OutputForms.FormEmail formEmail = new OutputForms.FormEmail())
                    {
                        formEmail.Text = "Email this Ad Schedule Snapshot";
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
