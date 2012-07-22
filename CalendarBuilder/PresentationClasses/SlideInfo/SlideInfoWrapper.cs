using System;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses.SlideInfo
{
    public class SlideInfoWrapper
    {
        private Calendars.ICalendarControl _parentCalendar = null;
        private DevExpress.XtraBars.Docking.DockPanel _container = null;
        private bool _allowToSave = false;

        public SlideInfoControl ContainedControl { get; private set; }

        public event EventHandler<EventArgs> Shown;
        public event EventHandler<EventArgs> Closed;
        public event EventHandler<EventArgs> DateSaved;

        public bool SettingsNotSaved
        {
            get
            {
                return this.ContainedControl.SettingsNotSaved;
            }
        }

        public SlideInfoWrapper(Calendars.ICalendarControl parentCalendar, DevExpress.XtraBars.Docking.DockPanel container)
        {
            _parentCalendar = parentCalendar;

            _container = container;
            _container.ClosingPanel += new DevExpress.XtraBars.Docking.DockPanelCancelEventHandler(ClosingPanel);
            _container.ClosedPanel += new DevExpress.XtraBars.Docking.DockPanelEventHandler(ClosedPanel);
            _container.DockChanged += new EventHandler(PanelDockChanged);
            _container.DoubleClick += new EventHandler(PanelDoubleClick);


            this.ContainedControl = new SlideInfoControl();
            this.ContainedControl.PropertiesSaved += new EventHandler(PropertiesSaved);
            this.ContainedControl.Closed += new EventHandler(PropertiesClosed);
        }

        #region Container Event Handlers
        private void ClosedPanel(object sender, DevExpress.XtraBars.Docking.DockPanelEventArgs e)
        {
            if (_allowToSave)
                SaveVisibilitySettings();
            if (this.Closed != null)
                this.Closed(this, new EventArgs());
            _parentCalendar.Splash(false);
        }

        private void ClosingPanel(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e)
        {
            _parentCalendar.Splash(true);
            SaveData();
            SaveLocationSettings();
        }

        void PanelDockChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
                SaveLocationSettings();
        }

        private void PanelDoubleClick(object sender, EventArgs e)
        {
            _allowToSave = false;
            if (_container.Dock == DevExpress.XtraBars.Docking.DockingStyle.Float)
            {
                _container.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            }
            else
            {
                _container.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
                if (_parentCalendar.CalendarSettings.SlideInfoFloatLeft != 0 && _parentCalendar.CalendarSettings.SlideInfoFloatTop != 0)
                    _container.FloatLocation = new System.Drawing.Point(_parentCalendar.CalendarSettings.SlideInfoFloatLeft, _parentCalendar.CalendarSettings.SlideInfoFloatTop);
                else
                    _container.FloatLocation = new System.Drawing.Point(200, 200);
            }
            _allowToSave = true;
            SaveLocationSettings();
        }
        #endregion

        #region Contained Control Event Handlers
        private void PropertiesSaved(object sender, EventArgs e)
        {
            if (this.DateSaved != null)
                this.DateSaved(this, new EventArgs());
        }

        private void PropertiesClosed(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Common Methods
        public void LoadVisibilitySettings()
        {
            if (_parentCalendar.CalendarSettings.SlideInfoVisible)
            {
                _parentCalendar.Splash(true);
                Show();
                _parentCalendar.Splash(false);
            }
            else
            {
                _parentCalendar.Splash(true);
                Close();
                _parentCalendar.Splash(false);
            }
            CalendarVisualizer.Instance.SlideInfoButtonItem.Checked = _parentCalendar.CalendarSettings.SlideInfoVisible;
        }

        private void LoadLocationSettings()
        {
            _container.Dock = _parentCalendar.CalendarSettings.SlideInfoDocked ? DevExpress.XtraBars.Docking.DockingStyle.Left : DevExpress.XtraBars.Docking.DockingStyle.Float;
            if (_parentCalendar.CalendarSettings.SlideInfoFloatLeft != 0 && _parentCalendar.CalendarSettings.SlideInfoFloatTop != 0)
                _container.FloatLocation = new System.Drawing.Point(_parentCalendar.CalendarSettings.SlideInfoFloatLeft, _parentCalendar.CalendarSettings.SlideInfoFloatTop);
            else
                _container.FloatLocation = new System.Drawing.Point(200, 200);
        }

        private void SaveVisibilitySettings()
        {
            _parentCalendar.CalendarSettings.SlideInfoVisible = _container.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible;
            ConfigurationClasses.SettingsManager.Instance.ViewSettings.Save();
        }

        private void SaveLocationSettings()
        {
            if (_container.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible)
                _parentCalendar.CalendarSettings.SlideInfoDocked = _container.Dock == DevExpress.XtraBars.Docking.DockingStyle.Left;
            _parentCalendar.CalendarSettings.SlideInfoFloatLeft = _container.FloatLocation.X;
            _parentCalendar.CalendarSettings.SlideInfoFloatTop = _container.FloatLocation.Y;
            ConfigurationClasses.SettingsManager.Instance.ViewSettings.Save();
        }

        public void LoadData(BusinessClasses.CalendarMonth month = null, bool reload = false)
        {
            SaveData(reload: reload);
            if (month == null)
                this.ContainedControl.LoadCurrentMonthData();
            else
                this.ContainedControl.LoadMonth(month);
            _container.Text = this.ContainedControl.MonthTitle;
        }

        public void SaveData(bool reload = false, bool force = false)
        {
            if (this.ContainedControl.SettingsNotSaved && !force)
            {
                string message = reload ? "Calendar data was updated.\nDo you want to reload slide info?" : "Slide Info has changed.\nDo you want to save it?";
                //if (AppManager.ShowWarningQuestion(message) == DialogResult.Yes)
                    this.ContainedControl.SaveData();
            }
            else if (force)
                this.ContainedControl.SaveData(); ;
        }

        public void Show()
        {
            _allowToSave = false;
            _container.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            LoadLocationSettings();
            _allowToSave = true;
            SaveVisibilitySettings();
            if (this.Shown != null)
                this.Shown(this, new EventArgs());
        }

        public void Close(bool allowToSave = true)
        {
            _allowToSave = allowToSave;
            _container.Close();
        }

        public void Decorate(BusinessClasses.CalendarStyle style)
        {
            this.ContainedControl.Decorate(style);
        }
        #endregion
    }
}
