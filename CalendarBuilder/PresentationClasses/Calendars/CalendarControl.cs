using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses.Calendars
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CalendarControl : UserControl, ICalendarControl
    {
        protected BusinessClasses.Schedule _localSchedule = null;
        protected BusinessClasses.CalendarStyle _calendarStyle;

        public Views.MonthView.MonthViewControl MonthView { get; private set; }
        public Views.GridView.GridViewControl GridView { get; private set; }
        public Views.IView SelectedView { get; private set; }

        public DayProperties.DayPropertiesWrapper DayProperties { get; private set; }
        public SlideInfo.SlideInfoWrapper SlideInfo { get; private set; }

        public bool AllowToSave { get; set; }
        public bool SettingsNotSaved { get; set; }

        public BusinessClasses.Calendar CalendarData
        {
            get
            {
                switch (_calendarStyle)
                {
                    case BusinessClasses.CalendarStyle.Advanced:
                        return _localSchedule.AdvancedCalendar;
                    case BusinessClasses.CalendarStyle.Graphic:
                        return _localSchedule.GraphicCalendar;
                    case BusinessClasses.CalendarStyle.Simple:
                        return _localSchedule.SimpleCalendar;
                    default:
                        return null;
                }
            }
        }

        public ConfigurationClasses.CalendarSettings CalendarSettings
        {
            get
            {
                switch (_calendarStyle)
                {
                    case BusinessClasses.CalendarStyle.Advanced:
                        return ConfigurationClasses.SettingsManager.Instance.ViewSettings.AdvancedCalendarSettings;
                    case BusinessClasses.CalendarStyle.Graphic:
                        return ConfigurationClasses.SettingsManager.Instance.ViewSettings.GraphicCalendarSettings;
                    case BusinessClasses.CalendarStyle.Simple:
                        return ConfigurationClasses.SettingsManager.Instance.ViewSettings.SimpleCalendarSettings;
                    default:
                        return null;
                }

            }
        }

        public CalendarControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            if ((base.CreateGraphics()).DpiX > 96)
            {
            }
            BusinessClasses.ScheduleManager.Instance.SettingsSaved += new EventHandler<BusinessClasses.SavingingEventArgs>((sender, e) =>
            {
                if (sender != this)
                {
                    LoadCalendar(e.QuickSave);
                }
            });

            Splash(true);

            #region Month View Initialization
            this.MonthView = new Views.MonthView.MonthViewControl(this);
            this.MonthView.DataSaved += new EventHandler<EventArgs>((sender, e) =>
            {
                this.GridView.RefreshData();
                this.SettingsNotSaved = true;
            });
            #endregion

            #region Grid  View Initialization
            this.GridView = new Views.GridView.GridViewControl(this);
            this.GridView.DataSaved += new EventHandler<EventArgs>((sender, e) =>
            {
                this.MonthView.RefreshData();
                this.SettingsNotSaved = true;
            });
            pnMain.Controls.Add(this.MonthView);
            pnMain.Controls.Add(this.GridView);
            #endregion

            #region Day Properties Initialization
            this.DayProperties = new DayProperties.DayPropertiesWrapper(this, dockPanelDayProperties);
            CalendarVisualizer.AssignCloseActiveEditorsonOutSideClick(this.DayProperties.ContainedControl);
            dockPanelDayProperties.Controls.Add(this.DayProperties.ContainedControl);
            this.DayProperties.Shown += new EventHandler<EventArgs>((sender, e) =>
            {
                this.SlideInfo.Close();
            });
            this.DayProperties.Closed += new EventHandler<EventArgs>((sender, e) =>
            {
            });
            this.DayProperties.DataSaved += new EventHandler<EventArgs>((sender, e) =>
            {
                this.MonthView.RefreshData();
                this.GridView.RefreshData();
                this.SlideInfo.LoadData(reload: true);
                this.SettingsNotSaved = true;
            });
            #endregion

            #region Slide Info Initialization
            this.SlideInfo = new SlideInfo.SlideInfoWrapper(this, dockPanelSlideInfo);
            CalendarVisualizer.AssignCloseActiveEditorsonOutSideClick(this.SlideInfo.ContainedControl);
            dockPanelSlideInfo.Controls.Add(this.SlideInfo.ContainedControl);
            this.SlideInfo.Shown += new EventHandler<EventArgs>((sender, e) =>
            {
                this.DayProperties.Close();
                bool temp = this.AllowToSave;
                this.AllowToSave = false;
                CalendarVisualizer.Instance.SlideInfoButtonItem.Checked = true;
                this.AllowToSave = temp;
            });
            this.SlideInfo.Closed += new EventHandler<EventArgs>((sender, e) =>
            {
                bool temp = this.AllowToSave;
                this.AllowToSave = false;
                CalendarVisualizer.Instance.SlideInfoButtonItem.Checked = false;
                this.AllowToSave = temp;
            });
            this.SlideInfo.DateSaved += new EventHandler<EventArgs>((sender, e) =>
            {
                this.SettingsNotSaved = true;
            });
            #endregion
        }

        #region Common Methods
        public void Splash(bool show)
        {
            if (show)
            {
                pnEmpty.BringToFront();
            }
            else
            {
                pnMain.BringToFront();
            }
        }

        public void LeaveCalendar()
        {
            this.SlideInfo.Close(false);
            if (this.SettingsNotSaved || (this.SelectedView != null && this.SelectedView.SettingsNotSaved) || this.DayProperties.SettingsNotSaved || this.SlideInfo.SettingsNotSaved)
                SaveCalendarData();
        }

        public void ShowCalendar()
        {
            this.AllowToSave = false;
            CalendarVisualizer.Instance.MonthsListBoxControl.Items.Clear();
            CalendarVisualizer.Instance.MonthsListBoxControl.Items.AddRange(this.CalendarData.Months.Select(x => new DevExpress.XtraEditors.Controls.ImageListBoxItem(x.Date.ToString("MMM, yyyy"), 0)).ToArray());
            if (CalendarVisualizer.Instance.MonthsListBoxControl.Items.Count > 0)
                CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex = 0;
            CalendarVisualizer.Instance.MonthViewButtonItem.Checked = !this.CalendarSettings.GridVisible;
            CalendarVisualizer.Instance.GridViewButtonItem.Checked = this.CalendarSettings.GridVisible;
            LoadView();
            this.SlideInfo.LoadData(month: this.CalendarData.Months[CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex]);
            this.SlideInfo.LoadVisibilitySettings();
            this.AllowToSave = true;
        }

        public void LoadCalendar(bool quickLoad)
        {
            _localSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();

            laAdvertiser.Text = this.CalendarData.Schedule.BusinessName;
            laCalendarWindow.Text = this.CalendarData.Schedule.FlightDateStart.HasValue && this.CalendarData.Schedule.FlightDateEnd.HasValue ? string.Format("{0} - {1}", new object[] { this.CalendarData.Schedule.FlightDateStart.Value.ToString("MM/dd/yy"), this.CalendarData.Schedule.FlightDateEnd.Value.ToString("MM/dd/yy") }) : string.Empty;
            laCalendarName.Text = this.CalendarData.Schedule.Name;

            this.MonthView.LoadData(quickLoad);
            this.GridView.LoadData(quickLoad);

            this.MonthView.Decorate(_calendarStyle);
            this.GridView.Decorate(_calendarStyle);
            this.DayProperties.Decorate(_calendarStyle);
            this.SlideInfo.Decorate(_calendarStyle);

            this.DayProperties.Close();

            this.SettingsNotSaved = false;
        }

        public bool SaveCalendarData(string scheduleName = "")
        {
            this.SelectedView.Save();
            this.SlideInfo.SaveData(force: true);
            this.DayProperties.SaveData(force: true);
            if (!string.IsNullOrEmpty(scheduleName))
                _localSchedule.Name = scheduleName;
            BusinessClasses.ScheduleManager.Instance.SaveSchedule(_localSchedule, true, this);
            LoadCalendar(true);
            laCalendarName.Text = this.CalendarData.Schedule.Name;
            this.SettingsNotSaved = false;
            return true;
        }
        #endregion

        #region View Methods
        public void LoadView()
        {
            bool temp = this.AllowToSave;
            this.AllowToSave = false;
            if (this.SelectedView != null)
            {
                if (this.SelectedView.SettingsNotSaved)
                    this.SelectedView.Save();
                this.SelectedView.CopyPasteManager.ResetCopy();
                this.SelectedView.CopyPasteManager.ResetPaste();
            }
            if (this.CalendarSettings.GridVisible)
            {
                this.SelectedView = this.GridView;
                this.GridView.BringToFront();
            }
            else
            {
                this.SelectedView = this.MonthView;
                this.MonthView.BringToFront();
            }
            this.Splash(true);
            this.SelectedView.ChangeMonth(this.CalendarData.Months[CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex].Date);
            this.Splash(false);
            this.AllowToSave = temp;
        }

        public void SaveView()
        {
            this.SelectedView.Save();
            this.CalendarSettings.GridVisible = CalendarVisualizer.Instance.GridViewButtonItem.Checked;
            ConfigurationClasses.SettingsManager.Instance.ViewSettings.Save();
            LoadView();
        }
        #endregion

        #region Output Staff
        public void Print()
        {
            if (CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex >= 0)
            {
                BusinessClasses.CalendarMonth selectedMonth = this.CalendarData.Months[CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex];
                foreach (BusinessClasses.CalendarMonth month in this.CalendarData.Months)
                    month.OutputData.PrepareNotes();
                using (ToolForms.FormSelectCalendar form = new ToolForms.FormSelectCalendar())
                {
                    form.Text = "Ad Calendar Slide Output";
                    form.pbLogo.Image = Properties.Resources.Calendar;
                    form.laTitle.Text = "You have several Calendars available for your presentation…";
                    form.buttonXCurrentPublication.Text = string.Format("Send ONLY {0} Calendar Slide to PowerPoint", selectedMonth.Date.ToString("MMMM, yyyy"));
                    form.buttonXSelectedPublications.Text = "Send all of the Selected Calendars to PowerPoint";
                    foreach (BusinessClasses.CalendarMonth month in this.CalendarData.Months.Where(y => y.Days.Where(z => z.ContainsData).Count() > 0 || y.OutputData.Notes.Count > 0))
                        form.checkedListBoxControlMonths.Items.Add(month, month.Date.ToString("MMMM, yyyy"), CheckState.Checked, true);
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
                                if (selectedMonth.Days.Where(x => x.ContainsData).Count() == 0 && selectedMonth.OutputData.Notes.Count == 0)
                                    if (AppManager.ShowWarningQuestion(string.Format("There are no records for {0}.\nDo you still want to send this slide to PowerPoint?", selectedMonth.Date.ToString("MMMM, yyyy"))) == DialogResult.No)
                                        return;
                                formProgress.Show();
                                this.Enabled = false;
                                InteropClasses.PowerPointHelper.Instance.AppendCalendar(selectedMonth.OutputData);
                            }
                            else if (result == DialogResult.No)
                            {
                                formProgress.laProgress.Text = form.checkedListBoxControlMonths.CheckedItems.Count == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Several Calendar slides…\nThis will take a few minutes…";
                                formProgress.Show();
                                this.Enabled = false;
                                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in form.checkedListBoxControlMonths.Items)
                                {
                                    if (item.CheckState == CheckState.Checked)
                                    {
                                        BusinessClasses.CalendarMonth month = item.Value as BusinessClasses.CalendarMonth;
                                        if (month != null)
                                            InteropClasses.PowerPointHelper.Instance.AppendCalendar(month.OutputData);

                                    }
                                }
                            }
                            this.Enabled = true;
                            formProgress.Close();
                        }
                        using (ToolForms.FormSlideOutput formOutput = new ToolForms.FormSlideOutput())
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
                }
            }
        }

        public void Email()
        {
            if (CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex >= 0)
            {
                BusinessClasses.CalendarMonth selectedMonth = this.CalendarData.Months[CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex];
                foreach (BusinessClasses.CalendarMonth month in this.CalendarData.Months)
                    month.OutputData.PrepareNotes();
                using (ToolForms.FormSelectCalendar form = new ToolForms.FormSelectCalendar())
                {
                    form.Text = "Ad Calendar Email Output";
                    form.pbLogo.Image = Properties.Resources.EmailBig;
                    form.laTitle.Text = "You have several Calendars that can be ATTACHED to an email…";
                    form.buttonXCurrentPublication.Text = string.Format("Attach just the {0} Calendar Slide to my email message", selectedMonth.Date.ToString("MMMM, yyyy"));
                    form.buttonXSelectedPublications.Text = "Attach ALL Selected Calendars to my email message";
                    foreach (BusinessClasses.CalendarMonth month in this.CalendarData.Months.Where(y => y.Days.Where(z => z.ContainsData).Count() > 0 || y.OutputData.Notes.Count > 0))
                        form.checkedListBoxControlMonths.Items.Add(month, month.Date.ToString("MMMM, yyyy"), CheckState.Checked, true);
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
                            string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                            if (result == DialogResult.Yes)
                            {
                                formProgress.laProgress.Text = "Creating your Calendar Slide…\nThis will take about 30 seconds…";
                                if (selectedMonth.Days.Where(x => x.ContainsData).Count() == 0)
                                    if (AppManager.ShowWarningQuestion(string.Format("There are no records for {0}.\nDo you still want to Email this slide?", selectedMonth.Date.ToString("MMMM, yyyy"))) == DialogResult.No)
                                        return;
                                formProgress.Show();
                                this.Enabled = false;
                                InteropClasses.PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, new BusinessClasses.CalendarOutputData[] { selectedMonth.OutputData });
                            }
                            else if (result == DialogResult.No)
                            {
                                formProgress.laProgress.Text = form.checkedListBoxControlMonths.CheckedItems.Count == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Several Calendar slides…\nThis will take a few minutes…";
                                formProgress.Show();
                                this.Enabled = false;
                                List<BusinessClasses.CalendarOutputData> emailPages = new List<BusinessClasses.CalendarOutputData>();
                                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in form.checkedListBoxControlMonths.Items)
                                {
                                    if (item.CheckState == CheckState.Checked)
                                    {
                                        BusinessClasses.CalendarMonth month = item.Value as BusinessClasses.CalendarMonth;
                                        if (month != null)
                                            emailPages.Add(month.OutputData);
                                    }
                                }
                                InteropClasses.PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, emailPages.ToArray());
                            }
                            this.Enabled = true;
                            formProgress.Close();
                            if (File.Exists(tempFileName))
                                using (ToolForms.FormEmail formEmail = new ToolForms.FormEmail())
                                {
                                    formEmail.Text = "Email this Calendar";
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
        }

        public void Preview()
        {
            if (CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex >= 0)
            {
                BusinessClasses.CalendarMonth selectedMonth = this.CalendarData.Months[CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex];
                foreach (BusinessClasses.CalendarMonth month in this.CalendarData.Months)
                    month.OutputData.PrepareNotes();
                using (ToolForms.FormSelectCalendar form = new ToolForms.FormSelectCalendar())
                {
                    form.Text = "Ad Calendar Preview";
                    form.pbLogo.Image = Properties.Resources.Preview;
                    form.laTitle.Text = "You have several Calendars available for preview…";
                    form.buttonXCurrentPublication.Text = string.Format("Preview just the {0} Calendar Slide", selectedMonth.Date.ToString("MMMM, yyyy"));
                    form.buttonXSelectedPublications.Text = "Preview ALL Selected Calendars";
                    foreach (BusinessClasses.CalendarMonth month in this.CalendarData.Months.Where(y => y.Days.Where(z => z.ContainsData).Count() > 0 || y.OutputData.Notes.Count > 0))
                        form.checkedListBoxControlMonths.Items.Add(month, month.Date.ToString("MMMM, yyyy"), CheckState.Checked, true);
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
                            string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                            if (result == DialogResult.Yes)
                            {
                                formProgress.laProgress.Text = "Creating your Calendar Slide…\nThis will take about 30 seconds…";
                                if (selectedMonth.Days.Where(x => x.ContainsData).Count() == 0)
                                    if (AppManager.ShowWarningQuestion(string.Format("There are no records for {0}.\nDo you still want to Email this slide?", selectedMonth.Date.ToString("MMMM, yyyy"))) == DialogResult.No)
                                        return;
                                formProgress.Show();
                                this.Enabled = false;
                                InteropClasses.PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, new BusinessClasses.CalendarOutputData[] { selectedMonth.OutputData });
                            }
                            else if (result == DialogResult.No)
                            {
                                formProgress.laProgress.Text = form.checkedListBoxControlMonths.CheckedItems.Count == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Several Calendar slides…\nThis will take a few minutes…";
                                formProgress.Show();
                                this.Enabled = false;
                                List<BusinessClasses.CalendarOutputData> emailPages = new List<BusinessClasses.CalendarOutputData>();
                                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in form.checkedListBoxControlMonths.Items)
                                {
                                    if (item.CheckState == CheckState.Checked)
                                    {
                                        BusinessClasses.CalendarMonth month = item.Value as BusinessClasses.CalendarMonth;
                                        if (month != null)
                                            emailPages.Add(month.OutputData);
                                    }
                                }
                                InteropClasses.PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, emailPages.ToArray());
                            }
                            this.Enabled = true;
                            formProgress.Close();
                            if (File.Exists(tempFileName))
                                using (ToolForms.FormPreview formPreview = new ToolForms.FormPreview())
                                {
                                    formPreview.Text = "Preview this Calendar";
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
                }
            }
        }
        #endregion

        #region Other Ribbon Operations
        public void OpenHelp()
        {
            switch (_calendarStyle)
            {
                case BusinessClasses.CalendarStyle.Advanced:
                    BusinessClasses.HelpManager.Instance.OpenHelpLink(this.SelectedView.GetType() == typeof(Views.GridView.GridViewControl) ? "nerdgrid" : "nerdcal");
                    break;
                case BusinessClasses.CalendarStyle.Graphic:
                    BusinessClasses.HelpManager.Instance.OpenHelpLink(this.SelectedView.GetType() == typeof(Views.GridView.GridViewControl) ? "coolgrid" : "coolcal");
                    break;
                case BusinessClasses.CalendarStyle.Simple:
                    BusinessClasses.HelpManager.Instance.OpenHelpLink(this.SelectedView.GetType() == typeof(Views.GridView.GridViewControl) ? "easygrid" : "easycal");
                    break;
            }
        }
        #endregion

        #region Common Event Handlers
        private void dockManager_Sizing(object sender, DevExpress.XtraBars.Docking.SizingEventArgs e)
        {
            if ((e.Panel.Name.Equals("dockPanelDayProperties") || e.Panel.Name.Equals("dockPanelSlideInfo")) && (e.NewSize.Width < 300 || e.NewSize.Height < 650))
                e.Cancel = true;
        }
        #endregion
    }
}


