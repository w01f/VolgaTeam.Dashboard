using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class OutputMultiSummaryControl : UserControl, ISummaryOutputControl
    {
        private static OutputMultiSummaryControl _instance;
        private List<PublicationMultiSummaryControl> _tabPages = new List<PublicationMultiSummaryControl>();
        private bool _allowToSave = false;
        public BusinessClasses.Schedule LocalSchedule { get; set; }
        public bool SettingsNotSaved { get; set; }

        public DevComponents.DotNetBar.SuperTooltipInfo HelpToolTip { get; private set; }

        private OutputMultiSummaryControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            this.HelpToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Learn more about the Multi-Publication Analysis", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);

            BusinessClasses.ScheduleManager.Instance.SettingsSaved += new EventHandler<BusinessClasses.SavingingEventArgs>((sender, e) =>
            {
                if (sender != this)
                    UpdateOutput(e.QuickSave);
            });
        }

        public static OutputMultiSummaryControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OutputMultiSummaryControl();
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

        public void UpdateOutput(bool quickLoad)
        {
            this.LocalSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();
            laScheduleWindow.Text = string.Format("{0} - {1}", new object[] { this.LocalSchedule.FlightDateStart.ToString("MM/dd/yy"), this.LocalSchedule.FlightDateEnd.ToString("MM/dd/yy") });
            laScheduleName.Text = this.LocalSchedule.Name;
            laAdvertiser.Text = this.LocalSchedule.BusinessName + (!string.IsNullOrEmpty(this.LocalSchedule.AccountNumber) ? (" - " + this.LocalSchedule.AccountNumber) : string.Empty);
            if (!quickLoad)
            {
                checkEditDate.Text = this.LocalSchedule.PresentationDateObject != null ? this.LocalSchedule.PresentationDate.ToString("MM/dd/yy") : string.Empty;
                checkEditBusinessName.Text = " " + this.LocalSchedule.BusinessName + (!string.IsNullOrEmpty(this.LocalSchedule.AccountNumber) ? (" - " + this.LocalSchedule.AccountNumber) : string.Empty);
                checkEditDecisionMaker.Text = " " + this.LocalSchedule.DecisionMaker;
                checkEditFlightDates.Text = " " + this.LocalSchedule.FlightDateStart.ToString("MM/dd/yy") + " - " + this.LocalSchedule.FlightDateEnd.ToString("MM/dd/yy");

                _allowToSave = false;
                comboBoxEditSchedule.Properties.Items.Clear();
                comboBoxEditSchedule.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.OutputHeaders.ToArray());
                if (string.IsNullOrEmpty(this.LocalSchedule.ViewSettings.MultiSummaryViewSettings.SlideHeader))
                {
                    if (comboBoxEditSchedule.Properties.Items.Count > 0)
                        comboBoxEditSchedule.SelectedIndex = 0;
                }
                else
                {
                    int index = comboBoxEditSchedule.Properties.Items.IndexOf(this.LocalSchedule.ViewSettings.MultiSummaryViewSettings.SlideHeader);
                    if (index >= 0)
                        comboBoxEditSchedule.SelectedIndex = index;
                    else
                        comboBoxEditSchedule.SelectedIndex = 0;
                }
                checkEditSchedule.Checked = this.LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowSlideHeader;
                checkEditBusinessName.Checked = this.LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowAdvertiser;
                checkEditDate.Checked = this.LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowPresentationDate;
                checkEditDecisionMaker.Checked = this.LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowDecisionMaker;
                checkEditFlightDates.Checked = this.LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowFlightDates;
                rbOnePerSlide.Checked = this.LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowOnePublicationPerSlide;
                rbTwoPerSlide.Checked = !this.LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowOnePublicationPerSlide;
                _allowToSave = true;

                xtraTabControlPublications.SuspendLayout();
                Application.DoEvents();
                xtraTabControlPublications.TabPages.Clear();
                _tabPages.RemoveAll(x => !this.LocalSchedule.Publications.Select(y => y.UniqueID).Contains(x.Publication.UniqueID));
                foreach (BusinessClasses.Publication publication in this.LocalSchedule.Publications)
                {
                    if (!string.IsNullOrEmpty(publication.Name))
                    {
                        PublicationMultiSummaryControl publicationTab = _tabPages.Where(x => x.Publication.UniqueID.Equals(publication.UniqueID)).FirstOrDefault();
                        if (publicationTab == null)
                        {
                            publicationTab = new PublicationMultiSummaryControl();
                            _tabPages.Add(publicationTab);
                            Application.DoEvents();
                        }
                        publicationTab.Publication = publication;
                        publicationTab.PageEnabled = publication.Inserts.Count > 0;
                        publicationTab.LoadPublication();
                        Application.DoEvents();
                    }
                }
                _tabPages.Sort((x, y) => x.Publication.Index.CompareTo(y.Publication.Index));
                xtraTabControlPublications.TabPages.AddRange(_tabPages.ToArray());
                Application.DoEvents();
                xtraTabControlPublications.ResumeLayout();
            }
            else
            {
                foreach (BusinessClasses.Publication publication in this.LocalSchedule.Publications)
                {
                    if (!string.IsNullOrEmpty(publication.Name))
                    {
                        PublicationMultiSummaryControl publicationTab = _tabPages.Where(x => x.Publication.UniqueID.Equals(publication.UniqueID)).FirstOrDefault();
                        if (publicationTab != null)
                        {
                            publicationTab.Publication = publication;
                            publicationTab.PageEnabled = publication.Inserts.Count > 0;
                        }
                        Application.DoEvents();
                    }
                }
            }
            this.SettingsNotSaved = false;
        }

        public void OpenHelp()
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("analysis");
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
                this.LocalSchedule.ViewSettings.MultiSummaryViewSettings.SlideHeader = comboBoxEditSchedule.EditValue != null ? comboBoxEditSchedule.EditValue.ToString() : string.Empty;
                this.SettingsNotSaved = true;
            }
        }

        private void checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
            {
                this.LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowAdvertiser = checkEditBusinessName.Checked;
                this.LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowDecisionMaker = checkEditDecisionMaker.Checked;
                this.LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowFlightDates = checkEditFlightDates.Checked;
                this.LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowPresentationDate = checkEditDate.Checked;
                this.LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowSlideHeader = checkEditSchedule.Checked;
                this.LocalSchedule.ViewSettings.MultiSummaryViewSettings.ShowOnePublicationPerSlide = rbOnePerSlide.Checked;
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

        #region Output Stuff
        public int OutputFileIndex
        {
            get
            {
                return rbOnePerSlide.Checked ? 1 : 2;
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

        public string[] FlightDates2
        {
            get
            {
                List<string> result = new List<string>();
                foreach (PublicationMultiSummaryControl publication in xtraTabControlPublications.TabPages.Where(x => x.PageEnabled))
                    result.Add(publication.checkEditFlightDates.Checked ? publication.checkEditFlightDates.Text : string.Empty);
                return result.ToArray();
            }
        }

        public string[] LogoFiles
        {
            get
            {
                List<string> result = new List<string>();
                foreach (PublicationMultiSummaryControl publication in xtraTabControlPublications.TabPages.Where(x => x.PageEnabled))
                    if (publication.checkEditLogo.Checked && publication.pbLogo.Image != null)
                    {
                        string fileName = System.IO.Path.GetTempFileName();
                        publication.pbLogo.Image.Save(fileName);
                        result.Add(fileName);
                    }
                    else
                        result.Add(string.Empty);
                return result.ToArray();
            }
        }

        public string[] PublicationNames1
        {
            get
            {
                List<string> result = new List<string>();
                foreach (PublicationMultiSummaryControl publication in xtraTabControlPublications.TabPages.Where(x => x.PageEnabled))
                    result.Add(publication.checkEditName.Checked && publication.checkEditLogo.Checked ? publication.checkEditName.Text.Replace("&&", "&") : string.Empty);
                return result.ToArray();
            }
        }

        public string[] PublicationNames2
        {
            get
            {
                List<string> result = new List<string>();
                foreach (PublicationMultiSummaryControl publication in xtraTabControlPublications.TabPages.Where(x => x.PageEnabled))
                    result.Add(publication.checkEditName.Checked && !publication.checkEditLogo.Checked ? publication.checkEditName.Text.Replace("&&", "&") : string.Empty);
                return result.ToArray();
            }
        }

        public string[] Investments
        {
            get
            {
                List<string> result = new List<string>();
                foreach (PublicationMultiSummaryControl publication in xtraTabControlPublications.TabPages.Where(x => x.PageEnabled))
                    result.Add(publication.checkEditInvestment.Checked ? (publication.comboBoxEditInvestment.EditValue.ToString() + " " + publication.laInvestment.Text) : string.Empty);
                return result.ToArray();
            }
        }

        public string[] RunDates
        {
            get
            {
                List<string> result = new List<string>();
                foreach (PublicationMultiSummaryControl publication in xtraTabControlPublications.TabPages.Where(x => x.PageEnabled))
                {
                    string runDates = publication.checkEditDates.Checked ? publication.memoEditDates.EditValue.ToString() : string.Empty;
                    runDates += (publication.checkEditComments.Checked && publication.memoEditComments.EditValue != null && !string.IsNullOrEmpty(publication.memoEditComments.Text.Trim()) ? ((!string.IsNullOrEmpty(runDates) ? " - " : string.Empty) + publication.memoEditComments.Text) : string.Empty);
                    result.Add(runDates);
                }

                return result.ToArray();
            }
        }

        public string[][] AdSpecs
        {
            get
            {
                List<string[]> result = new List<string[]>();
                foreach (PublicationMultiSummaryControl publication in xtraTabControlPublications.TabPages.Where(x => x.PageEnabled))
                {
                    List<string> adSpecs = new List<string>();
                    if (publication.checkEditTotalAds.Checked)
                        adSpecs.Add(publication.checkEditTotalAds.Text);
                    if (publication.checkEditTotalSquare.Checked && !string.IsNullOrEmpty(publication.checkEditTotalSquare.Text))
                        adSpecs.Add(publication.checkEditTotalSquare.Text);
                    if (publication.checkEditSquare.Checked && !string.IsNullOrEmpty(publication.checkEditSquare.Text))
                        adSpecs.Add(publication.checkEditSquare.Text);
                    if (publication.checkEditDimensions.Checked && !string.IsNullOrEmpty(publication.checkEditDimensions.Text))
                        adSpecs.Add(publication.checkEditDimensions.Text);
                    if (publication.checkEditPageSize.Checked && !string.IsNullOrEmpty(publication.checkEditPageSize.Text))
                        adSpecs.Add(publication.checkEditPageSize.Text);
                    if (publication.checkEditPercentOfPage.Checked && !string.IsNullOrEmpty(publication.checkEditPercentOfPage.Text))
                        adSpecs.Add(publication.checkEditPercentOfPage.Text);
                    if (publication.checkEditColor.Checked)
                        adSpecs.Add(publication.checkEditColor.Text.Replace("&&", "&"));
                    if (publication.checkEditAvgPCI.Checked)
                        adSpecs.Add(publication.checkEditAvgPCI.Text);
                    if (publication.checkEditAvgAdCost.Checked)
                        adSpecs.Add(publication.checkEditAvgAdCost.Text);
                    if (publication.checkEditAvgFinalCost.Checked)
                        adSpecs.Add(publication.checkEditAvgFinalCost.Text);
                    if (publication.checkEditDiscounts.Checked)
                        adSpecs.Add(publication.checkEditDiscounts.Text);
                    if (publication.checkEditMechanicals.Checked)
                        adSpecs.Add(publication.checkEditMechanicals.Text);
                    if (publication.checkEditSections.Checked)
                        adSpecs.Add(publication.labelControlSections.Text);
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
                InteropClasses.PowerPointHelper.Instance.AppendMultiSummary();
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
                InteropClasses.PowerPointHelper.Instance.PrepareMultiSummaryEmail(tempFileName);
                formProgress.Close();
                if (File.Exists(tempFileName))
                    using (OutputForms.FormEmail formEmail = new OutputForms.FormEmail())
                    {
                        formEmail.Text = "Email this Multi-Publication Analysis";
                        formEmail.PresentationFile = tempFileName;
                        ConfigurationClasses.RegistryHelper.MainFormHandle = formEmail.Handle;
                        ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                        formEmail.ShowDialog();
                        ConfigurationClasses.RegistryHelper.MaximizeMainForm = FormMain.Instance.IsMaximized;
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
                InteropClasses.PowerPointHelper.Instance.PrepareMultiSummaryEmail(tempFileName);
                formProgress.Close();
                if (File.Exists(tempFileName))
                    using (OutputForms.FormPreview formPreview = new OutputForms.FormPreview())
                    {
                        formPreview.Text = "Preview Multi-Publication Analysis";
                        formPreview.PresentationFile = tempFileName;
                        ConfigurationClasses.RegistryHelper.MainFormHandle = formPreview.Handle;
                        ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                        if (formPreview.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                            AppManager.ActivateForm(FormMain.Instance.Handle, true, false);
                        else
                        {
                            AppManager.ActivatePowerPoint();
                            AppManager.ActivateMiniBar();
                        }
                        ConfigurationClasses.RegistryHelper.MaximizeMainForm = FormMain.Instance.IsMaximized;
                        ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                    }
            }
        }
        #endregion
    }
}
