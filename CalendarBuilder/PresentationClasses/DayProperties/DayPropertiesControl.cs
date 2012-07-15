using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses.DayProperties
{
    public partial class DayPropertiesControl : UserControl
    {
        private BusinessClasses.CalendarStyle _style;
        public BusinessClasses.CalendarDay Day { get; set; }

        public bool SettingsNotSaved { get; set; }

        [Browsable(true)]
        [Category("Action")]
        public event EventHandler PropertiesSaved;

        [Browsable(true)]
        [Category("Action")]
        public event EventHandler Closed;

        [Browsable(true)]
        [Category("Action")]
        public event DevExpress.XtraTab.TabPageChangedEventHandler PropertiesGroupChanged;

        public DayPropertiesControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public void LoadData(BusinessClasses.CalendarDay day)
        {
            this.Day = day;
            LoadCurrentDayData();

            xtraTabPageDigital.PageVisible = _style == BusinessClasses.CalendarStyle.Advanced && this.Day.Parent.Schedule.ShowDigital;
            xtraTabPageNewspaper.PageVisible = _style == BusinessClasses.CalendarStyle.Advanced && this.Day.Parent.Schedule.ShowNewspaper;
            xtraTabPageTV.PageVisible = _style == BusinessClasses.CalendarStyle.Advanced;
            xtraTabPageRadio.PageVisible = _style == BusinessClasses.CalendarStyle.Advanced;
            xtraTabPageTV.PageEnabled = this.Day.Parent.Schedule.ShowTV;
            xtraTabPageRadio.PageEnabled = this.Day.Parent.Schedule.ShowRadio;
            xtraTabPageLogo.PageVisible = _style == BusinessClasses.CalendarStyle.Graphic;

            if (this.PropertiesGroupChanged != null)
                this.PropertiesGroupChanged(xtraTabControl, new DevExpress.XtraTab.TabPageChangedEventArgs(null, xtraTabControl.SelectedTabPage));
        }

        public void LoadCurrentDayData()
        {
            if (this.Day != null)
            {
                xtraTabPageDigital.Tooltip = "Digital Info: " + this.Day.Date.ToString("dddd, MMMM d, yyyy");
                xtraTabPageNewspaper.Tooltip = "Newspaper Info: " + this.Day.Date.ToString("dddd, MMMM d, yyyy");
                xtraTabPageTV.Tooltip = "TV Info: " + this.Day.Date.ToString("dddd, MMMM d, yyyy");
                xtraTabPageRadio.Tooltip = "Radio Info: " + this.Day.Date.ToString("dddd, MMMM d, yyyy");
                xtraTabPageComment.Tooltip = "Comment: " + this.Day.Date.ToString("dddd, MMMM d, yyyy");
                xtraTabPageLogo.Tooltip = "Logo: " + this.Day.Date.ToString("dddd, MMMM d, yyyy");
                digitalPropertiesControl.LoadData(this.Day);
                newspaperPropertiesControl.LoadData(this.Day);
                commentControl.LoadData(this.Day);
                logoControl.LoadData(this.Day);
                this.SettingsNotSaved = false;
            }
        }

        public void SaveData()
        {
            digitalPropertiesControl.SaveData();
            newspaperPropertiesControl.SaveData();
            tvPropertiesControl.SaveData();
            radioPropertiesControl.SaveData();
            commentControl.SaveData();
            logoControl.SaveData();
            this.SettingsNotSaved = false;

            if (this.PropertiesSaved != null)
                this.PropertiesSaved(this, new EventArgs());
        }

        public void Decorate(BusinessClasses.CalendarStyle style)
        {
            _style = style;
        }

        private void barLargeButtonItemApply_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
        }

        private void barLargeButtonItemDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Day.ClearData();
            LoadCurrentDayData();
            this.SettingsNotSaved = true;
        }

        private void barLargeButtonItemClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.Closed != null)
                this.Closed(sender, e);
        }

        private void propertiesControl_PropertiesChanged(object sender, EventArgs e)
        {
            this.SettingsNotSaved = true;
        }

        private void xtraTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (this.PropertiesGroupChanged != null)
                this.PropertiesGroupChanged(sender, e);
        }
    }
}
