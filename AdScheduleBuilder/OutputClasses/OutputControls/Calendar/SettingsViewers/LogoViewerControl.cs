using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls.Calendar.SettingsViewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class LogoViewerControl : UserControl, ICalendarSettingsViewer
    {
        private ConfigurationClasses.MonthCalendarViewSettings _settings = null;

        public string Title
        {
            get
            {
                return "Show Logo on the slide";
            }
        }

        public string FormToggleChangeCaption
        {
            get
            {
                return "Calendar Slide Logos";
            }
        }

        public string EditButtonText
        {
            get
            {
                return "Change Slide Logos";
            }
        }

        public string ApplyForAllText
        {
            get
            {
                return "Show this Logo on all Slides";
            }
        }

        public bool ShowApplyForAll
        {
            get
            {
                return true;
            }
        }

        public LogoViewerControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public void LoadSettings(ConfigurationClasses.MonthCalendarViewSettings settings)
        {
            _settings = settings;
            pictureEditLogo.Image = _settings.Logo;
        }

        public void SaveSettings()
        {
            if (pictureEditLogo.Image != null && _settings != null)
                _settings.Logo = pictureEditLogo.Image;
        }

        public void ApplySettingsForAll(ConfigurationClasses.MonthCalendarViewSettings[] allSettings)
        {
            if (_settings != null)
            foreach (ConfigurationClasses.MonthCalendarViewSettings settings in allSettings)
                if (_settings != settings)
                {
                    if (_settings.Logo != null)
                        settings.Logo = _settings.Logo.Clone() as Image;
                    else
                        settings.Logo = null;
                }
        }

        private void pictureEditLogo_Click(object sender, System.EventArgs e)
        {
            using (ToolForms.FormImageGallery form = new ToolForms.FormImageGallery())
            {
                if (form.ShowDialog() == DialogResult.OK && form.SelectedSource != null && form.SelectedSource.BigLogo != null)
                {
                    pictureEditLogo.Image = new System.Drawing.Bitmap(form.SelectedSource.BigLogo);
                }
            }
        }
    }
}
