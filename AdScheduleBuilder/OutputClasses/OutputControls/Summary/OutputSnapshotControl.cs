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
            FormMain.Instance.buttonItemSnapshotOptions.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowOptions;
            xtraTabControlOptions.SelectedTabPageIndex = this.LocalSchedule.ViewSettings.SnapshotViewSettings.SelectedOptionChapterIndex;

            buttonXPercentOfPage.Enabled = BusinessClasses.ListManager.Instance.ShareUnits.Count > 0;
            buttonXAvgAdCost.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgCost;
            buttonXAvgFinalCost.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgFinalCost;
            buttonXAvgPCI.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgPCI;
            buttonXDelivery.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDelivery;
            buttonXDimensions.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDimensions;
            buttonXLogo.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowLogo;
            buttonXPageSize.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPageSize;
            buttonXPercentOfPage.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPercentOfPage & buttonXPercentOfPage.Enabled;
            buttonXReadership.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowReadership;
            buttonXSquare.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowSquare;
            buttonXTotalColorRate.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalColor;
            buttonXTotalDiscounts.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalDiscounts;
            buttonXTotalFinalCost.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalFinalCost;
            buttonXTotalInserts.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalInserts;
            buttonXTotalSquare.Checked = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalSquare;
            _allowToSave = true;
        }

        private void SaveView()
        {
            if (_allowToSave)
            {
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowOptions = FormMain.Instance.buttonItemSnapshotOptions.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.SelectedOptionChapterIndex = xtraTabControlOptions.SelectedTabPageIndex;

                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgCost = buttonXAvgAdCost.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgFinalCost = buttonXAvgFinalCost.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowAvgPCI = buttonXAvgPCI.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDelivery = buttonXDelivery.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowDimensions = buttonXDimensions.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowLogo = buttonXLogo.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPageSize = buttonXPageSize.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowPercentOfPage = buttonXPercentOfPage.Checked & buttonXPercentOfPage.Enabled;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowReadership = buttonXReadership.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowSquare = buttonXSquare.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalColor = buttonXTotalColorRate.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalDiscounts = buttonXTotalDiscounts.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalFinalCost = buttonXTotalFinalCost.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalInserts = buttonXTotalInserts.Checked;
                this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalSquare = buttonXTotalSquare.Checked;
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
            if (buttonXAvgAdCost.Checked)
                count++;
            if (buttonXAvgFinalCost.Checked)
                count++;
            if (buttonXAvgPCI.Checked)
                count++;
            if (buttonXDelivery.Checked)
                count++;
            if (buttonXDimensions.Checked)
                count++;
            if (buttonXPageSize.Checked)
                count++;
            if (buttonXPercentOfPage.Checked)
                count++;
            if (buttonXReadership.Checked)
                count++;
            if (buttonXSquare.Checked)
                count++;
            if (buttonXTotalColorRate.Checked)
                count++;
            if (buttonXTotalDiscounts.Checked)
                count++;
            if (buttonXTotalFinalCost.Checked)
                count++;
            if (buttonXTotalInserts.Checked)
                count++;
            if (buttonXTotalSquare.Checked)
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

        public void buttonItemSnapshotOptions_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
                SaveView();
            splitContainerControl.PanelVisibility = this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowOptions ? DevExpress.XtraEditors.SplitPanelVisibility.Both : DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
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

        #region Options Panel Stuff
        private void xtraTabControlOptions_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (_allowToSave)
                SaveView();
        }

        #region Print
        public void buttonItemSnapshotToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
                SaveView();
            outputSnapshotContainer.UpdateColumns(this.LocalSchedule);
        }

        public void buttonItemSnapshotButton_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonX button = sender as DevComponents.DotNetBar.ButtonX;
            if (button != null && !button.Checked)
            {
                if (AllowShowColumn())
                    button.Checked = true;
                else
                    AppManager.ShowWarning("You already have 5 items enabled");
            }
            else
                button.Checked = false;
        }

        private void pbPrintHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("snapshotnavbar");
        }
        #endregion
        #endregion

        #region Output Staff
        public int OutputFileIndex
        {
            get
            {
                return buttonXLogo.Checked ? 1 : 2;
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
                        adSpecs.Add("Col. Inches: " + publication.SizeOptions.Square.Value.ToString("#,###.00#"));
                    if (this.LocalSchedule.ViewSettings.SnapshotViewSettings.ShowTotalSquare && publication.TotalSquare.HasValue && publication.AdPricingStrategy != BusinessClasses.AdPricingStrategies.SharePage)
                        adSpecs.Add("Total Inches: " + publication.TotalSquare.Value.ToString("#,###.00#"));
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
                    AppManager.ActivateForm(FormMain.Instance.Handle, FormMain.Instance.IsMaximized, false);
                else
                {
                    AppManager.ActivatePowerPoint();
                    AppManager.ActivateMiniBar();
                }
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

        public void Preview()
        {
            using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
            {
                formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
                formProgress.TopMost = true;
                formProgress.Show();
                string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                InteropClasses.PowerPointHelper.Instance.PrepareSnapshotEmail(tempFileName);
                formProgress.Close();
                if (File.Exists(tempFileName))
                    using (OutputForms.FormPreview formPreview = new OutputForms.FormPreview())
                    {
                        formPreview.Text = "Preview this Ad Schedule Snapshot";
                        formPreview.PresentationFile = tempFileName;
                        ConfigurationClasses.RegistryHelper.MainFormHandle = formPreview.Handle;
                        ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                        DialogResult previewResult = formPreview.ShowDialog();
                        ConfigurationClasses.RegistryHelper.MaximizeMainForm = FormMain.Instance.IsMaximized;
                        ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                        if (previewResult != System.Windows.Forms.DialogResult.OK)
                            AppManager.ActivateForm(FormMain.Instance.Handle, true, false);
                        else
                        {
                            AppManager.ActivatePowerPoint();
                            AppManager.ActivateMiniBar();
                        }
                    }
            }
        }
        #endregion

        #region Picture Box Clicks Habdlers
        /// <summary>
        /// Buttonize the PictureBox 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top += 1;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top -= 1;
        }
        #endregion
    }
}
