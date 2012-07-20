using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class OutputCalendarControl : UserControl, ICalendarControl
    {
        private static OutputCalendarControl _instance;
        private List<MonthViewControl> _monthViews = new List<MonthViewControl>();
        private MonthViewControl _selectedMonth = null;
        public BusinessClasses.Schedule LocalSchedule { get; set; }
        public bool AllowToSave { get; set; }
        public List<BusinessClasses.Insert> Inserts { get; set; }
        public bool SettingsNotSaved { get; set; }
        public DevComponents.DotNetBar.SuperTooltipInfo HelpToolTip { get; private set; }

        protected OutputCalendarControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.HelpToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Learn more about the Advertising Calendar", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
            _monthViews = new List<MonthViewControl>();
            this.Inserts = new List<BusinessClasses.Insert>();
            BusinessClasses.ScheduleManager.Instance.SettingsSaved += new EventHandler<BusinessClasses.SavingingEventArgs>((sender, e) =>
            {
                if (sender != this)
                    UpdateOutput(e.QuickSave);
            });
        }

        public static OutputCalendarControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OutputCalendarControl();
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

        private void PrepareMonthViews()
        {
            _monthViews.Clear();
            DateTime startDate = new DateTime(this.LocalSchedule.FlightDateStart.Year, this.LocalSchedule.FlightDateStart.Month, 1);
            DateTime endDate = new DateTime(this.LocalSchedule.FlightDateEnd.Year, this.LocalSchedule.FlightDateEnd.Month, 1);
            MonthViewControl monthView = null;
            ConfigurationClasses.MonthCalendarViewSettings monthSettings = null;
            while (startDate < endDate)
            {
                monthView = new MonthViewControl(this);
                monthSettings = this.LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Where(x => x.Month.Equals(startDate)).FirstOrDefault();
                if (monthSettings == null)
                {
                    monthSettings = new ConfigurationClasses.MonthCalendarViewSettings(this.LocalSchedule.ViewSettings.CalendarViewSettings);
                    monthSettings.Month = startDate;
                    this.LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Add(monthSettings);
                }
                monthView.Settings = monthSettings;
                monthSettings.MonthView = monthView;
                monthView.Init(startDate);
                _monthViews.Add(monthView);
                startDate = startDate.AddMonths(1);
            }
            monthView = new MonthViewControl(this);
            monthSettings = this.LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Where(x => x.Month.Equals(endDate)).FirstOrDefault();
            if (monthSettings == null)
            {
                monthSettings = new ConfigurationClasses.MonthCalendarViewSettings(this.LocalSchedule.ViewSettings.CalendarViewSettings);
                monthSettings.Month = endDate;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Add(monthSettings);
            }
            monthView.Settings = monthSettings;
            monthSettings.MonthView = monthView;
            monthView.Init(endDate);
            _monthViews.Add(monthView);

        }

        private void UpdateMonthSettings()
        {
            foreach (var monthView in _monthViews)
            {
                ConfigurationClasses.MonthCalendarViewSettings monthSettings = this.LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Where(x => x.Month.Equals(monthView.Month)).FirstOrDefault();
                if (monthSettings == null)
                {
                    monthSettings = new ConfigurationClasses.MonthCalendarViewSettings(this.LocalSchedule.ViewSettings.CalendarViewSettings);
                    monthSettings.Month = monthView.Month;
                    this.LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Add(monthSettings);
                }
                monthView.Settings = monthSettings;
                monthSettings.MonthView = monthView;
            }
        }

        public virtual void UpdateMonthView()
        {
            if (this.AllowToSave && comboBoxEditMonthSelector.Properties.Items.Count > 0)
            {
                ShowEmpty();
                pnCalendarView.Controls.Clear();
                DateTime selectedMonth = this.LocalSchedule.ScheduleMonths[comboBoxEditMonthSelector.SelectedIndex];
                _selectedMonth = _monthViews.Where(x => x.Settings.Month.Month.Equals(selectedMonth.Month) && x.Settings.Month.Year.Equals(selectedMonth.Year)).FirstOrDefault();
                if (_selectedMonth == null)
                {
                    _selectedMonth = new MonthViewControl(this);
                    ConfigurationClasses.MonthCalendarViewSettings monthSettings = this.LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Where(x => x.Month.Equals(selectedMonth)).FirstOrDefault();
                    if (monthSettings == null)
                    {
                        monthSettings = new ConfigurationClasses.MonthCalendarViewSettings(this.LocalSchedule.ViewSettings.CalendarViewSettings);
                        monthSettings.Month = selectedMonth;
                        this.LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Add(monthSettings);
                    }
                    _selectedMonth.Settings = monthSettings;
                    monthSettings.MonthView = _selectedMonth;
                    _selectedMonth.Init(selectedMonth);
                    _monthViews.Add(_selectedMonth);
                }
                _selectedMonth.RefreshData();
                pnCalendarView.Controls.Add(_selectedMonth);

                HideEmpty();
                this.Focus();
            }
        }

        public void UpdateToggledOptions()
        {
            if (this.AllowToSave && _selectedMonth != null)
            {
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowAbbreviationOnly = FormMain.Instance.buttonItemCalendarsShowAbbreviation.Checked;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowAdSize = FormMain.Instance.buttonItemCalendarsShowAdSize.Checked;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowPageSize = FormMain.Instance.buttonItemCalendarsShowPageSize.Checked;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowPercentOfPage = FormMain.Instance.buttonItemCalendarsShowPercentOfPage.Checked && BusinessClasses.ListManager.Instance.ShareUnits.Count > 0;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowColor = FormMain.Instance.buttonItemCalendarsShowColor.Checked;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowCost = FormMain.Instance.buttonItemCalendarsShowCost.Checked;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowSection = FormMain.Instance.buttonItemCalendarsShowSection.Checked;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowBigDate = FormMain.Instance.buttonItemCalendarsShowBigDates.Checked;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowLegend = FormMain.Instance.buttonItemCalendarsShowLegend.Checked;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowTitle = FormMain.Instance.buttonItemCalendarsShowTitle.Checked;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowBusinessName = FormMain.Instance.buttonItemCalendarsShowBusinessName.Checked;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowDecisionMaker = FormMain.Instance.buttonItemCalendarsShowDecisionMaker.Checked;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowLogo = FormMain.Instance.buttonItemCalendarsShowLogo.Checked;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowTotalCost = FormMain.Instance.buttonItemCalendarsShowTotalCost.Checked;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowAvgCost = FormMain.Instance.buttonItemCalendarsShowAvgCost.Checked;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowTotalAds = FormMain.Instance.buttonItemCalendarsShowTotalAds.Checked;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowActiveDays = FormMain.Instance.buttonItemCalendarsShowActiveDays.Checked;
                this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowComments = FormMain.Instance.buttonItemCalendarsShowComment.Checked;

                if (FormMain.Instance.buttonItemCalendarsColorBlack.Checked)
                    this.LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "black";
                else if (FormMain.Instance.buttonItemCalendarsColorBlue.Checked)
                    this.LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "blue";
                else if (FormMain.Instance.buttonItemCalendarsColorGray.Checked)
                    this.LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "gray";
                else if (FormMain.Instance.buttonItemCalendarsColorGreen.Checked)
                    this.LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "green";
                else if (FormMain.Instance.buttonItemCalendarsColorOrange.Checked)
                    this.LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "orange";
                else if (FormMain.Instance.buttonItemCalendarsColorTeal.Checked)
                    this.LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "teal";
                _selectedMonth.RefreshData();
                this.SettingsNotSaved = true;
            }
        }

        public void ShowOutputOptions(Calendar.SettingsViewers.ICalendarSettingsViewer settingsViewer)
        {
            if (_selectedMonth != null && settingsViewer != null)
            {
                using (OutputForms.FormCalendarOutputOptions form = new OutputForms.FormCalendarOutputOptions())
                {
                    foreach (var monthView in _monthViews)
                        form.Settings.Add(monthView.Settings.Clone());
                    form.SettingsViewer = settingsViewer;
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        foreach (var monthSettings in form.Settings)
                        {
                            MonthViewControl monthView = _monthViews.Where(x => x.Settings.Month.Equals(monthSettings.Month)).FirstOrDefault();
                            if (monthView != null)
                            {
                                monthView.Settings.Comments = monthSettings.Comments;
                                monthView.Settings.Logo = monthSettings.Logo;
                                monthView.Settings.Title = monthSettings.Title;
                                monthView.Settings.Legend.Clear();
                                monthView.Settings.Legend.AddRange(monthSettings.Legend.ToArray());
                            }
                        }
                        UpdateMonthView();
                        this.SettingsNotSaved = true;
                    }
                }
            }
        }

        public void ShowEmpty()
        {
            pnEmpty.BringToFront();
        }

        public void HideEmpty()
        {
            pnEmpty.SendToBack();
        }

        public void ApplySettings()
        {
            this.AllowToSave = false;
            FormMain.Instance.buttonItemCalendarsShowPercentOfPage.Enabled = BusinessClasses.ListManager.Instance.ShareUnits.Count > 0;

            FormMain.Instance.buttonItemCalendarsShowAbbreviation.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowAbbreviationOnly;
            FormMain.Instance.buttonItemCalendarsShowAdSize.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowAdSize;
            FormMain.Instance.buttonItemCalendarsShowPageSize.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowPageSize;
            FormMain.Instance.buttonItemCalendarsShowPercentOfPage.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowPercentOfPage & FormMain.Instance.buttonItemCalendarsShowPercentOfPage.Enabled;
            FormMain.Instance.buttonItemCalendarsShowColor.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowColor;
            FormMain.Instance.buttonItemCalendarsShowCost.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowCost;
            FormMain.Instance.buttonItemCalendarsShowSection.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowSection;
            FormMain.Instance.buttonItemCalendarsShowBigDates.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowBigDate;
            FormMain.Instance.buttonItemCalendarsShowLegend.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowLegend;
            FormMain.Instance.buttonItemCalendarsShowTitle.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowTitle;
            FormMain.Instance.buttonItemCalendarsShowBusinessName.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowBusinessName;
            FormMain.Instance.buttonItemCalendarsShowDecisionMaker.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowDecisionMaker;
            FormMain.Instance.buttonItemCalendarsShowLogo.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowLogo;
            FormMain.Instance.buttonItemCalendarsShowTotalCost.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowTotalCost;
            FormMain.Instance.buttonItemCalendarsShowAvgCost.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowAvgCost;
            FormMain.Instance.buttonItemCalendarsShowTotalAds.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowTotalAds;
            FormMain.Instance.buttonItemCalendarsShowActiveDays.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowActiveDays;
            FormMain.Instance.buttonItemCalendarsShowComment.Checked = this.LocalSchedule.ViewSettings.CalendarViewSettings.ShowComments;

            FormMain.Instance.buttonItemCalendarsColorBlack.Checked = false;
            FormMain.Instance.buttonItemCalendarsColorBlue.Checked = false;
            FormMain.Instance.buttonItemCalendarsColorGray.Checked = false;
            FormMain.Instance.buttonItemCalendarsColorGreen.Checked = false;
            FormMain.Instance.buttonItemCalendarsColorOrange.Checked = false;
            FormMain.Instance.buttonItemCalendarsColorTeal.Checked = false;
            switch (this.LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor)
            {
                case "black":
                    FormMain.Instance.buttonItemCalendarsColorBlack.Checked = true;
                    break;
                case "blue":
                    FormMain.Instance.buttonItemCalendarsColorBlue.Checked = true;
                    break;
                case "gray":
                    FormMain.Instance.buttonItemCalendarsColorGray.Checked = true;
                    break;
                case "green":
                    FormMain.Instance.buttonItemCalendarsColorGreen.Checked = true;
                    break;
                case "orange":
                    FormMain.Instance.buttonItemCalendarsColorOrange.Checked = true;
                    break;
                case "teal":
                    FormMain.Instance.buttonItemCalendarsColorTeal.Checked = true;
                    break;
            }
            this.AllowToSave = true;
        }

        public void UpdateOutput(bool quickLoad)
        {
            this.LocalSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();
            laScheduleWindow.Text = string.Format("{0} - {1}", new object[] { this.LocalSchedule.FlightDateStart.ToString("MM/dd/yy"), this.LocalSchedule.FlightDateEnd.ToString("MM/dd/yy") });
            laScheduleName.Text = this.LocalSchedule.Name;
            laAdvertiser.Text = this.LocalSchedule.BusinessName + (!string.IsNullOrEmpty(this.LocalSchedule.AccountNumber) ? (" - " + this.LocalSchedule.AccountNumber) : string.Empty);

            if (!quickLoad)
            {
                this.Inserts.Clear();
                foreach (BusinessClasses.Publication publication in this.LocalSchedule.Publications)
                    this.Inserts.AddRange(publication.Inserts.Where(x => x.Date != DateTime.MinValue));

                ApplySettings();

                this.AllowToSave = false;

                PrepareMonthViews();

                comboBoxEditMonthSelector.Properties.Items.Clear();
                comboBoxEditMonthSelector.Properties.Items.AddRange(this.LocalSchedule.ScheduleMonths.Select(x => new DevExpress.XtraEditors.Controls.ImageListBoxItem(x.ToString("MMM, yyyy"), 0)).ToArray());
                if (comboBoxEditMonthSelector.Properties.Items.Count > 0)
                    comboBoxEditMonthSelector.SelectedIndex = 0;

                UpdateMonthView();
                this.AllowToSave = true;
            }
            else
                UpdateMonthSettings();

            this.SettingsNotSaved = false;
        }

        public void OpenHelp()
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("calendars");
        }

        private void comboBoxEditMonthSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMonthView();
        }

        #region Output Staff
        public void PrintOutput()
        {
            if (_selectedMonth != null)
            {
                using (OutputForms.FormSelectPublication form = new OutputForms.FormSelectPublication())
                {
                    form.Text = "Ad Calendar Slide Output";
                    form.pbLogo.Image = Properties.Resources.Calendar;
                    form.laTitle.Text = "You have several Advertising Calendars available for your presentation…";
                    form.buttonXCurrentPublication.Text = string.Format("Send ONLY {0} Calendar Slide to PowerPoint", _selectedMonth.Settings.Month.ToString("MMMM, yyyy"));
                    form.buttonXSelectedPublications.Text = "Send all of the Selected Ad Calendars to PowerPoint";
                    foreach (MonthViewControl monthView in _monthViews.Where(x => this.Inserts.Where(y => y.Date.Year.Equals(x.Month.Year) && y.Date.Month.Equals(x.Month.Month)).Count() > 0))
                        form.checkedListBoxControlPublications.Items.Add(monthView, monthView.Settings.Month.ToString("MMMM, yyyy"), CheckState.Checked, true);
                    ConfigurationClasses.RegistryHelper.MainFormHandle = form.Handle;
                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                    DialogResult result = form.ShowDialog();
                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = FormMain.Instance.IsMaximized;
                    ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                    if (result != DialogResult.Cancel)
                    {
                        using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                        {
                            formProgress.TopMost = true;
                            if (result == DialogResult.Yes)
                            {
                                formProgress.laProgress.Text = "Creating your Calendar Slide…\nThis will take about 30 seconds…";
                                if (this.Inserts.Where(y => y.Date.Year.Equals(_selectedMonth.Month.Year) && y.Date.Month.Equals(_selectedMonth.Month.Month)).Count() == 0)
                                    if (AppManager.ShowWarningQuestion(string.Format("There are no Ads scheduled for {0}.\nDo you still want to send this slide to PowerPoint?", _selectedMonth.Month.ToString("MMMM, yyyy"))) == DialogResult.No)
                                        return;
                                formProgress.Show();
                                this.Enabled = false;
                                _selectedMonth.PrintOutput();
                            }
                            else if (result == DialogResult.No)
                            {
                                formProgress.laProgress.Text = form.checkedListBoxControlPublications.CheckedItems.Count == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Several Calendar slides…\nThis will take a few minutes…";
                                formProgress.Show();
                                this.Enabled = false;
                                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
                                {
                                    if (item.CheckState == CheckState.Checked)
                                    {
                                        MonthViewControl monthView = item.Value as MonthViewControl;
                                        if (monthView != null)
                                            monthView.PrintOutput();
                                    }
                                }
                            }
                            this.Enabled = true;
                            formProgress.Close();
                        }
                        using (OutputForms.FormSlideOutput formOutput = new OutputForms.FormSlideOutput())
                        {
                            if (formOutput.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                                AppManager.ActivateForm(FormMain.Instance.Handle, FormMain.Instance.IsMaximized, false);
                        }
                    }
                }
            }
        }

        public void Email()
        {
            using (OutputForms.FormSelectPublication form = new OutputForms.FormSelectPublication())
            {
                form.Text = "Ad Calendar Email Output";
                form.pbLogo.Image = Properties.Resources.EmailBig;
                form.laTitle.Text = "You have several Advertising Calendars that can be ATTACHED to an email…";
                form.buttonXCurrentPublication.Text = string.Format("Attach just the {0} Calendar Slide to my email message", _selectedMonth.Settings.Month.ToString("MMMM, yyyy"));
                form.buttonXSelectedPublications.Text = "Attach ALL Selected Ad Calendars to my email message";
                foreach (MonthViewControl monthView in _monthViews.Where(x => this.Inserts.Where(y => y.Date.Year.Equals(x.Month.Year) && y.Date.Month.Equals(x.Month.Month)).Count() > 0))
                    form.checkedListBoxControlPublications.Items.Add(monthView, monthView.Settings.Month.ToString("MMMM, yyyy"), CheckState.Checked, true);
                ConfigurationClasses.RegistryHelper.MainFormHandle = form.Handle;
                ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                DialogResult result = form.ShowDialog();
                ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
                ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                if (result != DialogResult.Cancel)
                {
                    using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                    {
                        formProgress.TopMost = true;
                        string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                        if (result == DialogResult.Yes)
                        {
                            formProgress.laProgress.Text = "Creating your Calendar Slide…\nThis will take about 30 seconds…";
                            if (this.Inserts.Where(y => y.Date.Year.Equals(_selectedMonth.Month.Year) && y.Date.Month.Equals(_selectedMonth.Month.Month)).Count() == 0)
                                if (AppManager.ShowWarningQuestion(string.Format("There are no Ads scheduled for {0}.\nDo you still want to Email this slide?", _selectedMonth.Month.ToString("MMMM, yyyy"))) == DialogResult.No)
                                    return;
                            formProgress.Show();
                            this.Enabled = false;
                            InteropClasses.PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, new MonthViewControl[] { _selectedMonth });
                        }
                        else if (result == DialogResult.No)
                        {
                            formProgress.laProgress.Text = form.checkedListBoxControlPublications.CheckedItems.Count == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Several Calendar slides…\nThis will take a few minutes…";
                            formProgress.Show();
                            this.Enabled = false;
                            List<MonthViewControl> emailPages = new List<MonthViewControl>();
                            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
                            {
                                if (item.CheckState == CheckState.Checked)
                                {
                                    MonthViewControl monthView = item.Value as MonthViewControl;
                                    if (monthView != null)
                                        emailPages.Add(monthView);
                                }
                            }
                            InteropClasses.PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, emailPages.ToArray());
                        }
                        this.Enabled = true;
                        formProgress.Close();
                        if (File.Exists(tempFileName))
                            using (OutputForms.FormEmail formEmail = new OutputForms.FormEmail())
                            {
                                formEmail.Text = "Email this Advertising Calendar";
                                formEmail.PresentationFile = tempFileName;
                                ConfigurationClasses.RegistryHelper.MainFormHandle = formEmail.Handle;
                                ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                                formEmail.ShowDialog();
                                ConfigurationClasses.RegistryHelper.MaximizeMainForm = FormMain.Instance.IsMaximized;
                                ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                            }
                    }
                }
            }
        }

        public void Preview()
        {
            using (OutputForms.FormSelectPublication form = new OutputForms.FormSelectPublication())
            {
                form.Text = "Ad Calendar Preview";
                form.pbLogo.Image = Properties.Resources.PreviewCalendar;
                form.laTitle.Text = "You have several Advertising Calendars that can be ATTACHED to an email…";
                form.buttonXCurrentPublication.Text = string.Format("Preview just the {0} Calendar Slide", _selectedMonth.Settings.Month.ToString("MMMM, yyyy"));
                form.buttonXSelectedPublications.Text = "Preview ALL Selected Ad Calendars";
                foreach (MonthViewControl monthView in _monthViews.Where(x => this.Inserts.Where(y => y.Date.Year.Equals(x.Month.Year) && y.Date.Month.Equals(x.Month.Month)).Count() > 0))
                    form.checkedListBoxControlPublications.Items.Add(monthView, monthView.Settings.Month.ToString("MMMM, yyyy"), CheckState.Checked, true);
                ConfigurationClasses.RegistryHelper.MainFormHandle = form.Handle;
                ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                DialogResult result = form.ShowDialog();
                ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
                ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                if (result != DialogResult.Cancel)
                {
                    using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                    {
                        formProgress.TopMost = true;
                        string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                        if (result == DialogResult.Yes)
                        {
                            formProgress.laProgress.Text = "Creating your Calendar Slide…\nThis will take about 30 seconds…";
                            if (this.Inserts.Where(y => y.Date.Year.Equals(_selectedMonth.Month.Year) && y.Date.Month.Equals(_selectedMonth.Month.Month)).Count() == 0)
                                if (AppManager.ShowWarningQuestion(string.Format("There are no Ads scheduled for {0}.\nDo you still want to create this slide?", _selectedMonth.Month.ToString("MMMM, yyyy"))) == DialogResult.No)
                                    return;
                            formProgress.Show();
                            this.Enabled = false;
                            InteropClasses.PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, new MonthViewControl[] { _selectedMonth });
                        }
                        else if (result == DialogResult.No)
                        {
                            formProgress.laProgress.Text = form.checkedListBoxControlPublications.CheckedItems.Count == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Several Calendar slides…\nThis will take a few minutes…";
                            formProgress.Show();
                            this.Enabled = false;
                            List<MonthViewControl> emailPages = new List<MonthViewControl>();
                            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
                            {
                                if (item.CheckState == CheckState.Checked)
                                {
                                    MonthViewControl monthView = item.Value as MonthViewControl;
                                    if (monthView != null)
                                        emailPages.Add(monthView);
                                }
                            }
                            InteropClasses.PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, emailPages.ToArray());
                        }
                        this.Enabled = true;
                        formProgress.Close();
                        if (File.Exists(tempFileName))
                            using (OutputForms.FormPreview formPreview = new OutputForms.FormPreview())
                            {
                                formPreview.Text = "Preview Advertising Calendar";
                                formPreview.PresentationFile = tempFileName;
                                ConfigurationClasses.RegistryHelper.MainFormHandle = formPreview.Handle;
                                ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                                formPreview.ShowDialog();
                                ConfigurationClasses.RegistryHelper.MaximizeMainForm = FormMain.Instance.IsMaximized;
                                ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                            }
                    }
                }
            }
        }
        #endregion
    }
}
