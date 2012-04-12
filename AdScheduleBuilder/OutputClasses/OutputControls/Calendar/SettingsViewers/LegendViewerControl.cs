using System.ComponentModel;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls.Calendar.SettingsViewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class LegendViewerControl : UserControl, ICalendarSettingsViewer
    {
        private ConfigurationClasses.MonthCalendarViewSettings _settings = null;

        public string Title
        {
            get
            {
                return "Show Legend Codes";
            }
        }

        public string FormToggleChangeCaption
        {
            get
            {
                return "Slide Legend Codes";
            }
        }

        public string EditButtonText
        {
            get
            {
                return "Edit Legend Codes";
            }
        }

        public string ApplyForAllText
        {
            get
            {
                return "Show only these legend codes for All Month Slides";
            }
        }

        public bool ShowApplyForAll
        {
            get
            {
                return true;
            }
        }

        public LegendViewerControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public void LoadSettings(ConfigurationClasses.MonthCalendarViewSettings settings)
        {
            _settings = settings;
            gridControlLegend.DataSource = new BindingList<ConfigurationClasses.CalendarLegend>(_settings.Legend);
            gridControlLegend.RefreshDataSource();
        }

        public void SaveSettings()
        {
            gridViewLegend.CloseEditor();
        }

        public void ApplySettingsForAll(ConfigurationClasses.MonthCalendarViewSettings[] allSettings)
        {
            if (_settings != null)
                foreach (ConfigurationClasses.MonthCalendarViewSettings settings in allSettings)
                {
                    if (_settings != settings)
                    {
                        settings.Legend.Clear();
                        foreach (ConfigurationClasses.CalendarLegend legend in _settings.Legend)
                            settings.Legend.Add(legend.Clone());
                    }
                }
        }
    }
}
