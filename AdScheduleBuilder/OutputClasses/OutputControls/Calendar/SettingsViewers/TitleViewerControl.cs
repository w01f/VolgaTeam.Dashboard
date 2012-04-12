using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls.Calendar.SettingsViewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TitleViewerControl : UserControl, ICalendarSettingsViewer
    {
        private ConfigurationClasses.MonthCalendarViewSettings _settings = null;

        public string Title
        {
            get
            {
                return "Calendar Slide Title";
            }
        }

        public string FormToggleChangeCaption
        {
            get
            {
                return "Slide Title";
            }
        }

        public string EditButtonText
        {
            get
            {
                return "Edit Slide Title";
            }
        }

        public string ApplyForAllText
        {
            get
            {
                return "Show this same  Slide Title for all Months in this schedule";
            }
        }

        public bool ShowApplyForAll
        {
            get
            {
                return true;
            }
        }

        public TitleViewerControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public void LoadSettings(ConfigurationClasses.MonthCalendarViewSettings settings)
        {
            _settings = settings;
            textEditTitle.EditValue = _settings.Title;
        }

        public void SaveSettings()
        {
            if (_settings != null)
                _settings.Title = textEditTitle.EditValue.ToString().Trim();
        }

        public void ApplySettingsForAll(ConfigurationClasses.MonthCalendarViewSettings[] allSettings)
        {
            if (_settings != null)
                foreach (ConfigurationClasses.MonthCalendarViewSettings settings in allSettings)
                    if (_settings != settings)
                        settings.Title = _settings.Title;
        }
    }
}
