using System;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses.DayProperties
{
    public class DayPropertiesWrapper
    {
        private ICalendarControl _parentCalendar = null;
        private DevExpress.XtraBars.Docking.DockPanel _container = null;
        private bool _allowToSave = false;
        
        public DayPropertiesControl ContainedControl { get; private set; }

        public event EventHandler<EventArgs> Shown;
        public event EventHandler<EventArgs> Closed;
        public event EventHandler<EventArgs> DataSaved;

        public bool SettingsNotSaved 
        {
            get
            {
                return this.ContainedControl.SettingsNotSaved;
            } 
        }

        public DayPropertiesWrapper(ICalendarControl parentCalendar, DevExpress.XtraBars.Docking.DockPanel container)
        {
            _parentCalendar = parentCalendar;

            _container = container;
            _container.ClosingPanel += new DevExpress.XtraBars.Docking.DockPanelCancelEventHandler(ClosingPanel);
            _container.ClosedPanel += new DevExpress.XtraBars.Docking.DockPanelEventHandler(ClosedPanel);
            _container.DockChanged += new EventHandler(PanelDockChanged);
            _container.DoubleClick += new EventHandler(PanelDoubleClick);


            this.ContainedControl = new DayPropertiesControl();
            this.ContainedControl.PropertiesSaved += new EventHandler(PropertiesSaved);
            this.ContainedControl.Closed += new EventHandler(PropertiesClosed);
            this.ContainedControl.PropertiesGroupChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(PropertiesGroupChanged);
        }

        #region Container Event Handlers
        private void ClosedPanel(object sender, DevExpress.XtraBars.Docking.DockPanelEventArgs e)
        {
            if (this.Closed != null)
                this.Closed(this, new EventArgs());
        }

        private void ClosingPanel(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e)
        {
            SaveData();
            SaveSettings();
        }

        void PanelDockChanged(object sender, EventArgs e)
        {
            if (_allowToSave)
                SaveSettings();
        }

        private void PanelDoubleClick(object sender, EventArgs e)
        {
            _allowToSave = false;
            if (_container.Dock == DevExpress.XtraBars.Docking.DockingStyle.Float)
            {
                _container.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            }
            else
            {
                _container.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
                if (_parentCalendar.CalendarSettings.DayPropertiesFloatLeft != 0 && _parentCalendar.CalendarSettings.DayPropertiesFloatTop != 0)
                    _container.FloatLocation = new System.Drawing.Point(_parentCalendar.CalendarSettings.DayPropertiesFloatLeft, _parentCalendar.CalendarSettings.DayPropertiesFloatTop);
                else
                    _container.FloatLocation = new System.Drawing.Point(500, 200);
            }
            _allowToSave = true;
            SaveSettings();
        }
        #endregion

        #region Contained Control Event Handlers
        private void PropertiesSaved(object sender, EventArgs e)
        {
            if (this.DataSaved != null)
                this.DataSaved(this, new EventArgs());
        }

        private void PropertiesClosed(object sender, EventArgs e)
        {
            Close();
        }

        private void PropertiesGroupChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            _container.Text = e.Page.Tooltip;
        }
        #endregion

        #region Common Methods
        private void LoadSettings()
        {
            if (_parentCalendar.CalendarSettings.DayPropertiesFloatLeft != 0 && _parentCalendar.CalendarSettings.DayPropertiesFloatTop != 0)
                _container.FloatLocation = new System.Drawing.Point(_parentCalendar.CalendarSettings.DayPropertiesFloatLeft, _parentCalendar.CalendarSettings.DayPropertiesFloatTop);
            else
                _container.FloatLocation = new System.Drawing.Point(500, 200);
            _container.Dock = _parentCalendar.CalendarSettings.DayPropertiesDocked ? DevExpress.XtraBars.Docking.DockingStyle.Right : DevExpress.XtraBars.Docking.DockingStyle.Float;
        }

        private void SaveSettings()
        {
            if (_container.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Visible)
                _parentCalendar.CalendarSettings.DayPropertiesDocked = _container.Dock == DevExpress.XtraBars.Docking.DockingStyle.Right;
            _parentCalendar.CalendarSettings.DayPropertiesFloatLeft = _container.FloatLocation.X;
            _parentCalendar.CalendarSettings.DayPropertiesFloatTop = _container.FloatLocation.Y;
            ConfigurationClasses.SettingsManager.Instance.ViewSettings.Save();
        }

        public void LoadData(BusinessClasses.CalendarDay day = null)
        {
            SaveData();
            if (day == null)
                this.ContainedControl.LoadCurrentDayData();
            else
                this.ContainedControl.LoadData(day);
        }

        public void SaveData(bool force = false)
        {
            if (this.ContainedControl.SettingsNotSaved && !force)
            {
                //if (AppManager.ShowWarningQuestion("Day Properties has changed.\nDo you want to save them") == DialogResult.Yes)
                    this.ContainedControl.SaveData();
            }
            else if (force)
                this.ContainedControl.SaveData();
        }

        public void Show()
        {
            _allowToSave = false;
            _container.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            LoadSettings();
            _allowToSave = true;
            if (this.Shown != null)
                this.Shown(this, new EventArgs());
        }

        public void Close()
        {
            _container.Close();
        }

        public void Decorate(BusinessClasses.CalendarStyle style)
        {
            this.ContainedControl.Decorate(style);
        }
        #endregion
    }
}
