using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls.Calendar.SettingsViewers
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CommentViewerControl : UserControl, ICalendarSettingsViewer
    {
        private ConfigurationClasses.MonthCalendarViewSettings _settings = null;

        public string Title
        {
            get
            {
                return "Add a Custom Comment";
            }
        }

        public string FormToggleChangeCaption
        {
            get
            {
                return "Custom Comments";
            }
        }

        public string EditButtonText
        {
            get
            {
                return "Edit Comments";
            }
        }

        public string ApplyForAllText
        {
            get
            {
                return "Show this Exact Same Notes on all slides";
            }
        }

        public bool ShowApplyForAll
        {
            get
            {
                return true;
            }
        }

        public CommentViewerControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public void LoadSettings(ConfigurationClasses.MonthCalendarViewSettings settings)
        {
            _settings = settings;
            if (!string.IsNullOrEmpty(_settings.Comments))
            {
                checkEditUseComment.Checked = true;
                memoEditComment.EditValue = _settings.Comments;
            }
            else
                checkEditUseComment.Checked = false;
        }

        public void SaveSettings()
        {
            if (_settings != null)
            {
                if (checkEditUseComment.Checked && memoEditComment.EditValue != null && !string.IsNullOrEmpty(memoEditComment.EditValue.ToString().Trim()))
                    _settings.Comments = memoEditComment.EditValue.ToString().Trim();
                else
                    _settings.Comments = string.Empty;
            }
        }

        public void ApplySettingsForAll(ConfigurationClasses.MonthCalendarViewSettings[] allSettings)
        {
            if (_settings != null)
            foreach (ConfigurationClasses.MonthCalendarViewSettings settings in allSettings)
                if (_settings != settings)
                    settings.Comments = _settings.Comments;
        }

        private void checkEditUseComment_CheckedChanged(object sender, System.EventArgs e)
        {
            memoEditComment.Enabled = checkEditUseComment.Checked;
            if (!checkEditUseComment.Checked)
                memoEditComment.EditValue = null;
        }
    }
}
