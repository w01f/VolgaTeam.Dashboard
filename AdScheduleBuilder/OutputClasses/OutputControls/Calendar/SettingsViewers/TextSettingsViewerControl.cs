using System.Linq;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls.Calendar.SettingsViewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TextSettingsViewerControl : UserControl, ICalendarSettingsViewer
    {
        protected ConfigurationClasses.MonthCalendarViewSettings _settings = null;

        public virtual string Title
        {
            get
            {
                return string.Empty;
            }
        }

        public virtual string FormToggleChangeCaption
        {
            get
            {
                return string.Empty;
            }
        }

        public string EditButtonText
        {
            get
            {
                return "View this Item";
            }
        }

        public string ApplyForAllText
        {
            get
            {
                return string.Empty;
            }
        }

        public bool ShowApplyForAll
        {
            get
            {
                return false;
            }
        }

        public TextSettingsViewerControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public void LoadSettings(ConfigurationClasses.MonthCalendarViewSettings settings)
        {
            _settings = settings;
            LoadValue();
        }

        public void SaveSettings()
        {
        }

        public void ApplySettingsForAll(ConfigurationClasses.MonthCalendarViewSettings[] allSettings)
        {
        }

        public virtual void LoadValue()
        {
        }
    }

    [System.ComponentModel.ToolboxItem(false)]
    public class DateViewerControl : TextSettingsViewerControl
    {
        public override string Title
        {
            get
            {
                return "Calendar Month Title";
            }
        }

        public override string FormToggleChangeCaption
        {
            get
            {
                return "Calendar Month Title";
            }
        }

        public override void LoadValue()
        {
            laValue.Text = _settings.Month.ToString("MMMM, yy");
        }
    }

    [System.ComponentModel.ToolboxItem(false)]
    public class BusinessNameViewerControl : TextSettingsViewerControl
    {
        public override string Title
        {
            get
            {
                return "Advertiser Name";
            }
        }

        public override string FormToggleChangeCaption
        {
            get
            {
                return "Advertiser Name";
            }
        }

        public override void LoadValue()
        {
            laValue.Text = "Business Name: " + (_settings.MonthView.ParentCalendar.LocalSchedule.BusinessName + (!string.IsNullOrEmpty(_settings.MonthView.ParentCalendar.LocalSchedule.AccountNumber) ? (" - " + _settings.MonthView.ParentCalendar.LocalSchedule.AccountNumber) : string.Empty));
        }
    }

    [System.ComponentModel.ToolboxItem(false)]
    public class DecisionMakerViewerControl : TextSettingsViewerControl
    {
        public override string Title
        {
            get
            {
                return "Decisionmaker Name";
            }
        }

        public override string FormToggleChangeCaption
        {
            get
            {
                return "Decision Maker";
            }
        }

        public override void LoadValue()
        {
            laValue.Text = "Decisionmaker Name: " + _settings.MonthView.ParentCalendar.LocalSchedule.DecisionMaker;
        }
    }

    [System.ComponentModel.ToolboxItem(false)]
    public class TotalCostViewerControl : TextSettingsViewerControl
    {
        public override string Title
        {
            get
            {
                return "Monthly Investment";
            }
        }

        public override string FormToggleChangeCaption
        {
            get
            {
                return "Monthly Investment";
            }
        }

        public override void LoadValue()
        {
            laValue.Text = "Monthly Investment: " + _settings.MonthView.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).Select(x => x.FinalRate).Sum().ToString("$#,###.00");
        }
    }

    [System.ComponentModel.ToolboxItem(false)]
    public class AvgCostViewerControl : TextSettingsViewerControl
    {
        public override string Title
        {
            get
            {
                return "Monthly Average Ad Rate";
            }
        }

        public override string FormToggleChangeCaption
        {
            get
            {
                return "Average Rate";
            }
        }

        public override void LoadValue()
        {
            laValue.Text = "Average Ad Rate: " + (_settings.MonthView.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).Count() > 0 ? _settings.MonthView.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).Select(x => x.FinalRate).Average().ToString("$#,###.00") : "$.00");
        }
    }

    [System.ComponentModel.ToolboxItem(false)]
    public class TotalAdsViewerControl : TextSettingsViewerControl
    {
        public override string Title
        {
            get
            {
                return "Monthly Ads Scheduled";
            }
        }

        public override string FormToggleChangeCaption
        {
            get
            {
                return "Monthly Ads Scheduled";
            }
        }

        public override void LoadValue()
        {
            laValue.Text = "Total Ads: " + _settings.MonthView.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).Count().ToString();
        }
    }

    [System.ComponentModel.ToolboxItem(false)]
    public class TotalDaysViewerControl : TextSettingsViewerControl
    {
        public override string Title
        {
            get
            {
                return "Active Days";
            }
        }

        public override string FormToggleChangeCaption
        {
            get
            {
                return "Active Days";
            }
        }

        public override void LoadValue()
        {
            laValue.Text = "Active Days: " + _settings.MonthView.ParentCalendar.Inserts.Where(x => x.Date.Month == _settings.Month.Month && x.Date.Year == _settings.Month.Year).GroupBy(x => x.Date).Count().ToString();
        }
    }
}
