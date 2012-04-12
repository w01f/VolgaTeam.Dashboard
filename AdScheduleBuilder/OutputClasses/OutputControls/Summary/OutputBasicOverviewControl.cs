using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class OutputBasicOverviewControl : UserControl, ISummaryOutputControl
    {
        private static OutputBasicOverviewControl _instance;
        private List<PublicationBasicOverviewControl> _tabPages = new List<PublicationBasicOverviewControl>();
        public BusinessClasses.Schedule LocalSchedule { get; set; }
        public bool SettingsNotSaved { get; set; }

        public DevComponents.DotNetBar.SuperTooltipInfo HelpToolTip { get; private set; }

        private OutputBasicOverviewControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.HelpToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me understand how to use the Basic Overview slide", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
            BusinessClasses.ScheduleManager.Instance.SettingsSaved += new EventHandler<BusinessClasses.SavingingEventArgs>((sender, e) =>
            {
                if (sender != this)
                {
                    UpdateOutput(e.QuickSave);
                }
            });
        }

        public static OutputBasicOverviewControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OutputBasicOverviewControl();
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
            laAdvertiser.Text = this.LocalSchedule.BusinessName;
            if (!quickLoad)
            {
                Application.DoEvents();
                xtraTabControlPublications.TabPages.Clear();
                _tabPages.RemoveAll(x => !this.LocalSchedule.Publications.Select(y => y.UniqueID).Contains(x.Publication.UniqueID));
                foreach (BusinessClasses.Publication publication in this.LocalSchedule.Publications)
                {
                    if (!string.IsNullOrEmpty(publication.Name))
                    {
                        PublicationBasicOverviewControl publicationTab = _tabPages.Where(x => x.Publication.UniqueID.Equals(publication.UniqueID)).FirstOrDefault();
                        if (publicationTab == null)
                        {
                            publicationTab = new PublicationBasicOverviewControl();
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
                        PublicationBasicOverviewControl publicationTab = _tabPages.Where(x => x.Publication.UniqueID.Equals(publication.UniqueID)).FirstOrDefault();
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
            BusinessClasses.HelpManager.Instance.OpenHelpLink("basicoverview");
        }

        #region Output Stuff
        public void PrintOutput()
        {
            using (OutputForms.FormSelectPublication form = new OutputForms.FormSelectPublication())
            {
                form.Text = "Basic Overview Slide Output";
                form.pbLogo.Image = Properties.Resources.BasicOverview;
                form.laTitle.Text = "You have Several Publications in this Basic Overview Summary…";
                form.buttonXCurrentPublication.Text = "Send just the Current Publication Overview to my PowerPoint presentation";
                form.buttonXSelectedPublications.Text = "Send all Selected Publications to my PowerPoint presentation";
                foreach (PublicationBasicOverviewControl tabPage in _tabPages)
                    if (tabPage.PageEnabled)
                        form.checkedListBoxControlPublications.Items.Add(tabPage.Publication.UniqueID, tabPage.Publication.Name, CheckState.Checked, true);
                DialogResult result = DialogResult.Yes;
                if (form.checkedListBoxControlPublications.Items.Count > 1)
                {
                    ConfigurationClasses.RegistryHelper.MainFormHandle = form.Handle;
                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                    result = form.ShowDialog();
                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
                    ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                    if (result == DialogResult.Cancel)
                        return;
                }
                using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                {
                    formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
                    formProgress.TopMost = true;
                    formProgress.Show();
                    if (result == DialogResult.Yes)
                        (xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex] as PublicationBasicOverviewControl).PrintOutput();
                    else if (result == DialogResult.No)
                    {
                        foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
                        {
                            if (item.CheckState == CheckState.Checked)
                            {
                                PublicationBasicOverviewControl tabPage = _tabPages.Where(x => x.Publication.UniqueID.Equals(item.Value)).FirstOrDefault();
                                if (tabPage != null)
                                    tabPage.PrintOutput();
                            }
                        }
                    }
                    formProgress.Close();
                }
                using (OutputForms.FormSlideOutput formOutput = new OutputForms.FormSlideOutput())
                {
                    if (formOutput.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        AppManager.ActivateForm(FormMain.Instance.Handle, true, false);
                }
            }
        }

        public void Email()
        {
            using (OutputForms.FormSelectPublication form = new OutputForms.FormSelectPublication())
            {
                form.Text = "Basic Overview Email Output";
                form.pbLogo.Image = Properties.Resources.EmailBig;
                form.laTitle.Text = "You have Several Publications in this Basic Overview Summary…";
                form.buttonXCurrentPublication.Text = "Attach just the Current Publication Overview to my Outlook Email Message";
                form.buttonXSelectedPublications.Text = "Attach all Selected Publications to my Outlook Email Message";
                foreach (PublicationBasicOverviewControl tabPage in _tabPages)
                    if (tabPage.PageEnabled)
                        form.checkedListBoxControlPublications.Items.Add(tabPage.Publication.UniqueID, tabPage.Publication.Name, CheckState.Checked, true);
                DialogResult result = DialogResult.Yes;
                if (form.checkedListBoxControlPublications.Items.Count > 1)
                {
                    ConfigurationClasses.RegistryHelper.MainFormHandle = form.Handle;
                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                    result = form.ShowDialog();
                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
                    ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                    if (result == DialogResult.Cancel)
                        return;
                }
                using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                {
                    formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
                    formProgress.TopMost = true;
                    formProgress.Show();
                    string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                    if (result == DialogResult.Yes)
                        InteropClasses.PowerPointHelper.Instance.PrepareBasicOverviewEmail(tempFileName, new PublicationBasicOverviewControl[] { xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex] as PublicationBasicOverviewControl });
                    else if (result == DialogResult.No)
                    {
                        List<PublicationBasicOverviewControl> emailPages = new List<PublicationBasicOverviewControl>();
                        foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
                        {
                            if (item.CheckState == CheckState.Checked)
                            {
                                PublicationBasicOverviewControl tabPage = _tabPages.Where(x => x.Publication.UniqueID.Equals(item.Value)).FirstOrDefault();
                                if (tabPage != null)
                                    emailPages.Add(tabPage);
                            }
                        }
                        InteropClasses.PowerPointHelper.Instance.PrepareBasicOverviewEmail(tempFileName, emailPages.ToArray());
                    }
                    formProgress.Close();
                    if (File.Exists(tempFileName))
                        using (OutputForms.FormEmail formEmail = new OutputForms.FormEmail())
                        {
                            formEmail.Text = "Email this Basic Overview";
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
        #endregion
    }
}
