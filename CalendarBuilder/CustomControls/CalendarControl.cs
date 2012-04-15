using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CalendarBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CalendarControl : UserControl
    {
        private static CalendarControl _instance = null;
        private BusinessClasses.Calendar _localCalendar;
        private CalendarVisualizer.CalendarVisualizer _visualizer = new CalendarVisualizer.CalendarVisualizer();

        public bool AllowToSave { get; set; }
        public bool SettingsNotSaved { get; set; }

        private CalendarControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            if ((base.CreateGraphics()).DpiX > 96)
            {
            }
            BusinessClasses.CalendarManager.Instance.SettingsSaved += new EventHandler<BusinessClasses.SavingingEventArgs>((sender, e) =>
            {
                if (sender != this)
                    LoadCalendar(e.QuickSave);
            });

            AssignCloseActiveEditorsonOutSideClick(FormMain.Instance.ribbonControl);
            AssignCloseActiveEditorsonOutSideClick(pnTop);
        }

        public static CalendarControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CalendarControl();
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
                    if (AppManager.ShowWarningQuestion("Calendar settings have changed.\nDo you want to save changes?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (SaveCalendar())
                            result = true;
                    }
                }
                else
                    result = true;
                SaveSlideInfoState();
                bool temp = this.AllowToSave;
                this.AllowToSave = false;
                dockPanelSlideInfo.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                this.AllowToSave = temp;
                return result;
            }
        }

        #region Common Methods
        public void Splash(bool show)
        {
            if (show)
                pnEmpty.BringToFront();
            else
                pnMain.BringToFront();
        }

        private void AssignCloseActiveEditorsonOutSideClick(Control control)
        {
            if (control.GetType() != typeof(CalendarVisualizer.DayControl)
                && control != FormMain.Instance.listBoxControlCalendar)
            {
                control.Click += new EventHandler(CloseActiveEditorsonOutSideClick);
                foreach (Control childControl in control.Controls)
                    AssignCloseActiveEditorsonOutSideClick(childControl);
            }
        }

        private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
        {
            if (_visualizer != null)
                _visualizer.ClearSelection();
            Splash(true);
            ChangeDayPropertiesVisibility(false);
            Splash(false);
        }

        public void LoadCalendar(bool quickLoad)
        {
            this.AllowToSave = false;

            _localCalendar = BusinessClasses.CalendarManager.Instance.GetLocalCalendar();

            laAdvertiser.Text = _localCalendar.BusinessName;
            laCalendarWindow.Text = _localCalendar.FlightDateStart.HasValue && _localCalendar.FlightDateEnd.HasValue ? string.Format("{0} - {1}", new object[] { _localCalendar.FlightDateStart.Value.ToString("MM/dd/yy"), _localCalendar.FlightDateEnd.Value.ToString("MM/dd/yy") }) : string.Empty;
            laCalendarName.Text = _localCalendar.Name;

            _visualizer.Init(_localCalendar, xtraScrollableControlMain, quickLoad);

            if (!quickLoad)
            {
                FormMain.Instance.listBoxControlCalendar.Items.Clear();
                FormMain.Instance.listBoxControlCalendar.Items.AddRange(_localCalendar.Months.Select(x => new DevExpress.XtraEditors.Controls.ImageListBoxItem(x.StartDate.ToString("MMM, yyyy"), 0)).ToArray());
                if (FormMain.Instance.listBoxControlCalendar.Items.Count > 0)
                    FormMain.Instance.listBoxControlCalendar.SelectedIndex = 0;

                foreach (CalendarVisualizer.MonthControl month in _visualizer.Months.Values)
                    AssignCloseActiveEditorsonOutSideClick(month);
            }
            this.AllowToSave = true;
            imageListBoxEditCalendar_SelectedIndexChanged(null, null);
            this.SettingsNotSaved = false;
        }

        private bool SaveCalendar(string scheduleName = "")
        {
            if (!string.IsNullOrEmpty(scheduleName))
                _localCalendar.Name = scheduleName;
            BusinessClasses.CalendarManager.Instance.SaveCalendar(_localCalendar, true, this);
            LoadCalendar(true);
            laCalendarName.Text = _localCalendar.Name;
            this.SettingsNotSaved = false;
            return true;
        }
        #endregion

        #region Common Event Handlers
        private void CalendarControl_Load(object sender, EventArgs e)
        {
            AssignCloseActiveEditorsonOutSideClick(FormMain.Instance.ribbonControl);
            AssignCloseActiveEditorsonOutSideClick(pnTop);
        }

        public void imageListBoxEditCalendar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_visualizer != null && FormMain.Instance.listBoxControlCalendar.SelectedIndex >= 0 && this.AllowToSave)
            {
                Splash(true);

                ChangeDayPropertiesVisibility(false);
                _visualizer.ShowMonth(_localCalendar.Months[FormMain.Instance.listBoxControlCalendar.SelectedIndex].StartDate);
                Splash(false);
            }
        }
        #endregion

        #region Day Properties Methods and Event Handlers
        private void SaveDayPropertiesState()
        {
            if (this.AllowToSave)
            {
                ConfigurationClasses.SettingsManager.Instance.ViewSettings.DayPropertiesDocked = dockPanelDayProperties.Dock == DevExpress.XtraBars.Docking.DockingStyle.Right;
                ConfigurationClasses.SettingsManager.Instance.ViewSettings.DayPropertiesFloatLeft = dockPanelDayProperties.FloatLocation.X;
                ConfigurationClasses.SettingsManager.Instance.ViewSettings.DayPropertiesFloatTop = dockPanelDayProperties.FloatLocation.Y;
                ConfigurationClasses.SettingsManager.Instance.ViewSettings.Save();
            }
        }

        public void ApplyDayProperties()
        {
            if (dayPropertiesControl.SettingsNotSaved)
            {
                if (AppManager.ShowWarningQuestion("Day Properties has changed.\nDo you want to save them") == DialogResult.Yes)
                {
                    dayPropertiesControl.SaveData();
                }
            }
            LoadSlideInfoData(reload: true);
            LoadGridData(reload: true);
            _visualizer.RefreshData();
            this.SettingsNotSaved = true;
        }

        public void ChangeDayPropertiesVisibility(bool show)
        {
            if (show && dockPanelDayProperties.Visibility != DevExpress.XtraBars.Docking.DockVisibility.Visible)
            {
                bool temp = this.AllowToSave;
                this.AllowToSave = false;
                dockPanelDayProperties.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                if (ConfigurationClasses.SettingsManager.Instance.ViewSettings.DayPropertiesFloatLeft != 0 && ConfigurationClasses.SettingsManager.Instance.ViewSettings.DayPropertiesFloatTop != 0)
                    dockPanelDayProperties.FloatLocation = new System.Drawing.Point(ConfigurationClasses.SettingsManager.Instance.ViewSettings.DayPropertiesFloatLeft, ConfigurationClasses.SettingsManager.Instance.ViewSettings.DayPropertiesFloatTop);
                else
                    dockPanelDayProperties.FloatLocation = new System.Drawing.Point(500, 200);
                dockPanelDayProperties.Dock = ConfigurationClasses.SettingsManager.Instance.ViewSettings.DayPropertiesDocked ? DevExpress.XtraBars.Docking.DockingStyle.Right : DevExpress.XtraBars.Docking.DockingStyle.Float;
                this.AllowToSave = temp;
            }
            else if (!show && dockPanelDayProperties.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible)
            {
                SaveDayPropertiesState();
                dockPanelDayProperties.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            }
        }

        private void dayPropertiesControl_PropertiesClosed(object sender, EventArgs e)
        {
            dockPanelDayProperties_ClosingPanel(null, null);
            ChangeDayPropertiesVisibility(false);
        }

        private void dayPropertiesControl_PropertiesApplied(object sender, EventArgs e)
        {
            ApplyDayProperties();
        }

        private void dockManager_Sizing(object sender, DevExpress.XtraBars.Docking.SizingEventArgs e)
        {
            if (e.Panel.Name.Equals("dockPanelDayProperties") && (e.NewSize.Width < 300 || e.NewSize.Height < 650))
                e.Cancel = true;
        }

        private void dockPanelDayProperties_ClosingPanel(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e)
        {
            ApplyDayProperties();
            dayPropertiesControl.SettingsNotSaved = false;
            SaveDayPropertiesState();
        }

        private void dockPanelDayProperties_DockChanged(object sender, EventArgs e)
        {
            SaveDayPropertiesState();
        }

        private void dockPanelDayProperties_DoubleClick(object sender, EventArgs e)
        {
            if (dockPanelDayProperties.Dock == DevExpress.XtraBars.Docking.DockingStyle.Float)
            {
                dockPanelDayProperties.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            }
            else
            {
                dockPanelDayProperties.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
                if (ConfigurationClasses.SettingsManager.Instance.ViewSettings.DayPropertiesFloatLeft != 0 && ConfigurationClasses.SettingsManager.Instance.ViewSettings.DayPropertiesFloatTop != 0)
                    dockPanelDayProperties.FloatLocation = new System.Drawing.Point(ConfigurationClasses.SettingsManager.Instance.ViewSettings.DayPropertiesFloatLeft, ConfigurationClasses.SettingsManager.Instance.ViewSettings.DayPropertiesFloatTop);
                else
                    dockPanelDayProperties.FloatLocation = new System.Drawing.Point(500, 200);
            }
        }
        private void dayPropertiesControl_PropertiesGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            dockPanelDayProperties.Text = e.Group.Hint;
        }
        #endregion

        #region Slide Info Methods and Event Handlers
        public void LoadSlideInfoState()
        {
            bool temp = this.AllowToSave;
            this.AllowToSave = false;
            FormMain.Instance.buttonItemCalendarSlideInfo.Checked = ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoVisible;
            UpdateSlideInfoAreaAccordingOptions();
            this.AllowToSave = temp;
        }

        public void UpdateSlideInfoAreaAccordingOptions()
        {
            dockPanelSlideInfo.Visibility = ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoVisible ? DevExpress.XtraBars.Docking.DockVisibility.Visible : DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            dockPanelSlideInfo.Dock = ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoDocked ? DevExpress.XtraBars.Docking.DockingStyle.Left : DevExpress.XtraBars.Docking.DockingStyle.Float;
            if (ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoFloatLeft != 0 && ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoFloatTop != 0)
                dockPanelSlideInfo.FloatLocation = new System.Drawing.Point(ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoFloatLeft, ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoFloatTop);
            else
                dockPanelSlideInfo.FloatLocation = new System.Drawing.Point(200, 200);
        }

        private void SaveSlideInfoState()
        {
            if (this.AllowToSave)
            {
                ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoVisible = dockPanelSlideInfo.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible;
                ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoDocked = dockPanelSlideInfo.Dock == DevExpress.XtraBars.Docking.DockingStyle.Left;
                ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoFloatLeft = dockPanelSlideInfo.FloatLocation.X;
                ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoFloatTop = dockPanelSlideInfo.FloatLocation.Y;
                ConfigurationClasses.SettingsManager.Instance.ViewSettings.Save();
            }
        }

        public void LoadSlideInfoData(BusinessClasses.CalendarMonth month = null, bool reload = false)
        {
            if (slideInfoControl.SettingsNotSaved)
            {
                string message = reload ? "Calendar data was updated.\nDo you want to reload slide info?" : "Slide Info has changed.\nDo you want to save it?";
                if (AppManager.ShowWarningQuestion(message) == DialogResult.Yes)
                {
                    slideInfoControl.SaveData();
                    this.SettingsNotSaved = true;
                }
            }
            if (month != null)
            {
                slideInfoControl.LoadMonth(month);
            }
            else
            {
                slideInfoControl.LoadCurrentMonthData();
            }
        }

        private void slideInfoControl_PropertiesApplied(object sender, EventArgs e)
        {
            this.SettingsNotSaved = true;
        }

        private void slideInfoControl_PropertiesClosed(object sender, EventArgs e)
        {
            dockPanelSlideInfo_ClosedPanel(null, null);
        }

        private void dockPanelSlideInfo_ClosedPanel(object sender, DevExpress.XtraBars.Docking.DockPanelEventArgs e)
        {
            SaveSlideInfoState();
            bool temp = this.AllowToSave;
            this.AllowToSave = false;
            FormMain.Instance.buttonItemCalendarSlideInfo.Checked = false;
            this.AllowToSave = temp;
        }

        private void dockPanelSlideInfo_DockChanged(object sender, EventArgs e)
        {
            SaveSlideInfoState();
        }

        private void dockPanelSlideInfo_DoubleClick(object sender, EventArgs e)
        {
            if (dockPanelSlideInfo.Dock == DevExpress.XtraBars.Docking.DockingStyle.Float)
            {
                dockPanelSlideInfo.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            }
            else
            {
                dockPanelSlideInfo.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
                if (ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoFloatLeft != 0 && ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoFloatTop != 0)
                    dockPanelSlideInfo.FloatLocation = new System.Drawing.Point(ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoFloatLeft, ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoFloatTop);
                else
                    dockPanelSlideInfo.FloatLocation = new System.Drawing.Point(200, 200);
            }
        }

        public void buttonItemCalendarSlideInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.AllowToSave)
            {
                ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoVisible = FormMain.Instance.buttonItemCalendarSlideInfo.Checked;
                if (!ConfigurationClasses.SettingsManager.Instance.ViewSettings.SlideInfoVisible)
                    dockPanelSlideInfo.Close();
                else
                {
                    bool temp = this.AllowToSave;
                    this.AllowToSave = false;
                    UpdateSlideInfoAreaAccordingOptions();
                    this.AllowToSave = temp;
                    SaveSlideInfoState();
                    LoadSlideInfoData();
                }
            }
        }

        #endregion

        #region Grid Methods and Event Handlers
        private void LoadGridState()
        {
            bool temp = this.AllowToSave;
            this.AllowToSave = false;
            FormMain.Instance.buttonItemCalendarGrid.Checked = ConfigurationClasses.SettingsManager.Instance.ViewSettings.GridVisible;
            UpdateGridAreaAccordingOptions();
            this.AllowToSave = temp;
        }

        private void UpdateGridAreaAccordingOptions()
        {
            if (ConfigurationClasses.SettingsManager.Instance.ViewSettings.GridVisible)
            {
                gridControl.BringToFront();
                xtraScrollableControlMain.SendToBack();
            }
            else
            {
                xtraScrollableControlMain.BringToFront();
                gridControl.SendToBack();
            }

        }

        private void SaveGridState()
        {
            ConfigurationClasses.SettingsManager.Instance.ViewSettings.GridVisible = FormMain.Instance.buttonItemCalendarGrid.Checked;
            ConfigurationClasses.SettingsManager.Instance.ViewSettings.Save();
        }

        public void LoadGridData(BusinessClasses.CalendarMonth month = null, bool reload = false)
        {
            if (gridControl.SettingsNotSaved)
            {
                string message = reload ? "Calendar data was updated.\nDo you want to reload grid?" : "Grid data has changed.\nDo you want to save it?";
                if (AppManager.ShowWarningQuestion(message) == DialogResult.Yes)
                {
                    gridControl.SaveData();
                    LoadSlideInfoData(reload: true);
                    _visualizer.RefreshData();
                    this.SettingsNotSaved = true;
                }
                gridControl.SettingsNotSaved = false;
            }
            if (month != null)
            {
                gridControl.LoadMonth(month);
            }
            else
            {
                gridControl.LoadCurrentMonthData();
            }
        }

        public void ApplyGridData()
        {
            if (gridControl.SettingsNotSaved)
            {
                if (AppManager.ShowWarningQuestion("Grid data has changed.\nDo you want to save it?") == DialogResult.Yes)
                {
                    gridControl.SaveData();
                }
                gridControl.SettingsNotSaved = false;
            }
            LoadSlideInfoData(reload: true);
            _visualizer.RefreshData();
            this.SettingsNotSaved = true;
        }

        private void gridControl_PropertiesApplied(object sender, EventArgs e)
        {
            ApplyGridData();
        }

        private void gridControl_PropertiesClosed(object sender, EventArgs e)
        {
            ApplyGridData();
            FormMain.Instance.buttonItemCalendarGrid.Checked = false;
        }

        public void buttonItemCalendarGrid_CheckedChanged(object sender, EventArgs e)
        {
            if (this.AllowToSave)
            {
                SaveGridState();
                UpdateGridAreaAccordingOptions();
                LoadGridData();
            }
        }
        #endregion

        #region Ribbon Operations Events
        public void buttonItemScheduleHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("Home");
        }

        public void buttonItemScheduleSave_Click(object sender, EventArgs e)
        {
            if (SaveCalendar())
                AppManager.ShowInformation("Calendar Saved");
        }

        public void buttonItemScheduleSaveAs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = ConfigurationClasses.SettingsManager.Instance.SaveFolder;
                dialog.Title = "Save Calendar As...";
                dialog.Filter = "Calendar Files|*.xml";
                dialog.FileName = _localCalendar.Name + ".xml";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (SaveCalendar(dialog.FileName.Replace(".xml", "")))
                        AppManager.ShowInformation("Calendar was saved");
                }
            }
        }
        #endregion

        #region Output Staff
        public void buttonItemWeeklySchedulePowerPoint_Click(object sender, EventArgs e)
        {
            if (FormMain.Instance.listBoxControlCalendar.SelectedIndex >= 0)
            {
                BusinessClasses.CalendarMonth _selectedMonth = _localCalendar.Months[FormMain.Instance.listBoxControlCalendar.SelectedIndex];
                using (ToolForms.FormSelectPublication form = new ToolForms.FormSelectPublication())
                {
                    form.Text = "Ad Calendar Slide Output";
                    form.pbLogo.Image = Properties.Resources.Calendar;
                    form.laTitle.Text = "You have several Calendars available for your presentation…";
                    form.buttonXCurrentPublication.Text = string.Format("Send ONLY {0} Calendar Slide to PowerPoint", _selectedMonth.StartDate.ToString("MMMM, yyyy"));
                    form.buttonXSelectedPublications.Text = "Send all of the Selected Calendars to PowerPoint";
                    foreach (BusinessClasses.CalendarMonth month in _localCalendar.Months.Where(y=>y.Days.Where(z=>z.ContainsData).Count()>0))
                        form.checkedListBoxControlMonths.Items.Add(month, month.StartDate.ToString("MMMM, yyyy"), CheckState.Checked, true);
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
                            if (result == DialogResult.Yes)
                            {
                                formProgress.laProgress.Text = "Creating your Calendar Slide…\nThis will take about 30 seconds…";
                                if (_selectedMonth.Days.Where(x=>x.ContainsData).Count() == 0)
                                    if (AppManager.ShowWarningQuestion(string.Format("There are no records for {0}.\nDo you still want to send this slide to PowerPoint?", _selectedMonth.StartDate.ToString("MMMM, yyyy"))) == DialogResult.No)
                                        return;
                                formProgress.Show();
                                this.Enabled = false;
                                InteropClasses.PowerPointHelper.Instance.AppendCalendar(_selectedMonth.OutputData);
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
                                AppManager.ActivateForm(FormMain.Instance.Handle, true, false);
                        }
                    }
                }
            }
        }

        public void buttonItemWeeklyScheduleEmail_Click(object sender, EventArgs e)
        {
            if (FormMain.Instance.listBoxControlCalendar.SelectedIndex >= 0)
            {
                BusinessClasses.CalendarMonth _selectedMonth = _localCalendar.Months[FormMain.Instance.listBoxControlCalendar.SelectedIndex];

                using (ToolForms.FormSelectPublication form = new ToolForms.FormSelectPublication())
                {
                    form.Text = "Ad Calendar Email Output";
                    form.pbLogo.Image = Properties.Resources.EmailBig;
                    form.laTitle.Text = "You have several Calendars that can be ATTACHED to an email…";
                    form.buttonXCurrentPublication.Text = string.Format("Attach just the {0} Calendar Slide to my email message", _selectedMonth.StartDate.ToString("MMMM, yyyy"));
                    form.buttonXSelectedPublications.Text = "Attach ALL Selected Calendars to my email message";
                    foreach (BusinessClasses.CalendarMonth month in _localCalendar.Months.Where(y => y.Days.Where(z => z.ContainsData).Count() > 0))
                        form.checkedListBoxControlMonths.Items.Add(month, month.StartDate.ToString("MMMM, yyyy"), CheckState.Checked, true);
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
                                if (_selectedMonth.Days.Where(x => x.ContainsData).Count() == 0)
                                    if (AppManager.ShowWarningQuestion(string.Format("There are no records for {0}.\nDo you still want to Email this slide?", _selectedMonth.StartDate.ToString("MMMM, yyyy"))) == DialogResult.No)
                                        return;
                                formProgress.Show();
                                this.Enabled = false;
                                InteropClasses.PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, new BusinessClasses.CalendarOutputData[] { _selectedMonth.OutputData });
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
                                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
                                    ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                                }
                        }
                    }
                }
            }
        }
        #endregion
    }
}


