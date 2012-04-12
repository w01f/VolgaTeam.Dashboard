using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CalendarBuilder.CustomControls.DayProperties
{
    public partial class DayPropertiesControl : UserControl
    {
        private BusinessClasses.CalendarDay _day = null;

        public bool SettingsNotSaved { get; set; }

        [Browsable(true)]
        [Category("Action")]
        public event EventHandler PropertiesApplied;

        [Browsable(true)]
        [Category("Action")]
        public event EventHandler PropertiesClosed;

        [Browsable(true)]
        [Category("Action")]
        public event DevExpress.XtraNavBar.NavBarGroupEventHandler PropertiesGroupChanged;

        public DayPropertiesControl()
        {
            InitializeComponent();
            navBarControlDayProperties.View = new CustomNavPaneViewInfoRegistrator();
        }

        public void LoadData(BusinessClasses.CalendarDay day)
        {
            _day = day;
            laTitle.Text = _day.Date.ToString("dddd, MMMM d, yyyy");
            digitalPropertiesControl.LoadData(_day);
            newspaperPropertiesControl.LoadData(_day);
            commentControl.LoadData(_day);

            navBarGroupDigital.Expanded = true;
            if (this.PropertiesGroupChanged != null)
                this.PropertiesGroupChanged(navBarControlDayProperties, new DevExpress.XtraNavBar.NavBarGroupEventArgs(navBarGroupDigital));

            this.SettingsNotSaved = false;
        }

        public void SaveData()
        {
            digitalPropertiesControl.SaveData();
            newspaperPropertiesControl.SaveData();
            commentControl.SaveData();
            this.SettingsNotSaved = false;
        }

        private void barLargeButtonItemApply_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
            if (this.PropertiesApplied != null)
                this.PropertiesApplied(sender, e);
        }

        private void barLargeButtonItemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.PropertiesClosed != null)
                this.PropertiesClosed(sender, e);
        }

        private void propertiesControl_PropertiesChanged(object sender, EventArgs e)
        {
            this.SettingsNotSaved = true;
        }

        private void navBarControlDayProperties_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            if (this.PropertiesGroupChanged != null)
                this.PropertiesGroupChanged(sender, e);
        }
    }
}
