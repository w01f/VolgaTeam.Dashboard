using System;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class SummariesControl : UserControl
    {
        private static SummariesControl _instance;
        private ISummaryOutputControl _selectedOutput = null;

        #region Operation Buttons
        public DevComponents.DotNetBar.ButtonItem HelpButtonItem { get; set; }
        #endregion

        private SummariesControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public static SummariesControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SummariesControl();
                return _instance;
            }
        }

        public bool AllowToLeaveControl
        {
            get
            {
                bool result = false;
                if (_selectedOutput.SettingsNotSaved)
                {
                    SaveSchedule();
                    result = true;
                }
                else
                    result = true;
                return result;
            }
        }

        private void SaveSchedule(string newName = "")
        {
            if (_selectedOutput != null)
            {
                if (!string.IsNullOrEmpty(newName))
                    _selectedOutput.LocalSchedule.Name = newName;
                _selectedOutput.SettingsNotSaved = false;
                _selectedOutput.LocalSchedule.ViewSettings.SaveDefaultViewSettings();
                BusinessClasses.ScheduleManager.Instance.SaveSchedule(_selectedOutput.LocalSchedule, true, _selectedOutput as Control);
                _selectedOutput.UpdateOutput(true);
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

        public void SelectSummary(SummaryType summaryType)
        {
            switch (summaryType)
            {
                case SummaryType.Overview:
                    _selectedOutput = OutputControls.OutputBasicOverviewControl.Instance;
                    this.HelpButtonItem = FormMain.Instance.buttonItemOverviewHelp;
                    break;
                case SummaryType.MultiSummary:
                    _selectedOutput = OutputControls.OutputMultiSummaryControl.Instance;
                    this.HelpButtonItem = FormMain.Instance.buttonItemMultiSummaryHelp;
                    break;
                case SummaryType.Snapshot:
                    _selectedOutput = OutputControls.OutputSnapshotControl.Instance;
                    this.HelpButtonItem = FormMain.Instance.buttonItemSnapshotHelp;
                    break;
                default:
                    _selectedOutput = null;
                    break;
            }

            if (_selectedOutput != null)
            {
                if (!pnMain.Controls.Contains(_selectedOutput as Control))
                {
                    Application.DoEvents();
                    pnEmpty.BringToFront();
                    Application.DoEvents();
                    pnMain.Controls.Add(_selectedOutput as Control);
                    Application.DoEvents();
                    pnMain.BringToFront();
                    Application.DoEvents();
                }
                (_selectedOutput as Control).BringToFront();
                FormMain.Instance.superTooltip.SetSuperTooltip(this.HelpButtonItem, _selectedOutput.HelpToolTip);
            }
            else
            {
                pnEmpty.BringToFront();
                FormMain.Instance.superTooltip.SetSuperTooltip(this.HelpButtonItem, null);
            }
        }

        public void buttonItemSummariesPreview_Click(object sender, EventArgs e)
        {
            _selectedOutput.Preview();
        }

        public void buttonItemSummariesPowerPoint_Click(object sender, EventArgs e)
        {
            _selectedOutput.PrintOutput();
        }

        public void buttonItemSummariesEmail_Click(object sender, EventArgs e)
        {
            _selectedOutput.Email();
        }

        public void buttonItemSummariesSave_Click(object sender, EventArgs e)
        {
            SaveSchedule();
            AppManager.ShowInformation("Schedule Saved");
        }

        public void buttonItemSummariesSaveAs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = ConfigurationClasses.SettingsManager.Instance.SaveFolder;
                dialog.Title = "Save Schedule As...";
                dialog.Filter = "Schedule Files|*.xml";
                dialog.FileName = _selectedOutput.LocalSchedule.Name + ".xml";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SaveSchedule(dialog.FileName.Replace(".xml", ""));
                    AppManager.ShowInformation("Schedule was saved");
                }
            }
        }

        public void buttonItemSummariesReset_Click(object sender, EventArgs e)
        {
            _selectedOutput.ResetToDefault();
            SaveSchedule();
        }

        public void buttonItemSummariesHelp_Click(object sender, EventArgs e)
        {
            _selectedOutput.OpenHelp();
        }
    }

    public enum SummaryType
    {
        Overview,
        MultiSummary,
        Snapshot
    }

    public interface ISummaryOutputControl
    {
        BusinessClasses.Schedule LocalSchedule { get; set; }
        bool SettingsNotSaved { get; set; }
        DevComponents.DotNetBar.SuperTooltipInfo HelpToolTip { get; }
        void UpdateOutput(bool quickLoad);
        void ResetToDefault();
        void PrintOutput();
        void Email();
        void Preview();
        void OpenHelp();
    }
}
