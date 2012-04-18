using System;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class SummariesControl : UserControl
    {
        private static SummariesControl _instance;
        private ISummaryOutputControl _selectedOutput = null;

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

        private void UncheckOutputOptions()
        {
            FormMain.Instance.buttonItemSummariesBasicOverview.Checked = false;
            FormMain.Instance.buttonItemSummariesMultiSummary.Checked = false;
            FormMain.Instance.buttonItemSummariesSnapshot.Checked = false;
            FormMain.Instance.itemContainerSnapshotButtonsGroup1.Visible = false;
            FormMain.Instance.itemContainerSnapshotButtonsGroup2.Visible = false;
            FormMain.Instance.itemContainerSnapshotButtonsGroup3.Visible = false;
            FormMain.Instance.ribbonBarSummariesSnapshot.RecalcLayout();
            FormMain.Instance.ribbonPanelSummaries.PerformLayout();
        }

        public void UpdatePageAccordingToggledButton()
        {
            _selectedOutput = null;
            if (FormMain.Instance.buttonItemSummariesBasicOverview != null && FormMain.Instance.buttonItemSummariesBasicOverview.Checked)
                _selectedOutput = OutputControls.OutputBasicOverviewControl.Instance;
            else if (FormMain.Instance.buttonItemSummariesMultiSummary != null && FormMain.Instance.buttonItemSummariesMultiSummary.Checked)
                _selectedOutput = OutputControls.OutputMultiSummaryControl.Instance;
            else if (FormMain.Instance.buttonItemSummariesSnapshot != null && FormMain.Instance.buttonItemSummariesSnapshot.Checked)
                _selectedOutput = OutputControls.OutputSnapshotControl.Instance;
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
                FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemSummariesHelp, _selectedOutput.HelpToolTip);
            }
            else
            {
                pnEmpty.BringToFront();
                FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemSummariesHelp, null);
            }
        }

        public void buttonItemSummariesBasicOverview_Click(object sender, EventArgs e)
        {
            if (!(sender as DevComponents.DotNetBar.ButtonItem).Checked)
                if (this.AllowToLeaveControl)
                {
                    UncheckOutputOptions();
                    FormMain.Instance.buttonItemSummariesBasicOverview.Checked = true;
                    UpdatePageAccordingToggledButton();
                }
        }

        public void buttonItemSummariesMultiSummary_Click(object sender, EventArgs e)
        {
            if (!(sender as DevComponents.DotNetBar.ButtonItem).Checked)
                if (this.AllowToLeaveControl)
                {
                    UncheckOutputOptions();
                    FormMain.Instance.buttonItemSummariesMultiSummary.Checked = true;
                    UpdatePageAccordingToggledButton();
                }
        }

        public void buttonItemSummariesSnapshot_Click(object sender, EventArgs e)
        {
            if (!(sender as DevComponents.DotNetBar.ButtonItem).Checked)
                if (this.AllowToLeaveControl)
                {
                    UncheckOutputOptions();
                    FormMain.Instance.buttonItemSummariesSnapshot.Checked = true;
                    FormMain.Instance.itemContainerSnapshotButtonsGroup1.Visible = true;
                    FormMain.Instance.itemContainerSnapshotButtonsGroup2.Visible = true;
                    FormMain.Instance.itemContainerSnapshotButtonsGroup3.Visible = true;
                    FormMain.Instance.ribbonBarSummariesSnapshot.RecalcLayout();
                    FormMain.Instance.ribbonPanelSummaries.PerformLayout();
                    UpdatePageAccordingToggledButton();
                }
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

        public void buttonItemSummariesHelp_Click(object sender, EventArgs e)
        {
            _selectedOutput.OpenHelp();
        }
    }

    public interface ISummaryOutputControl
    {
        BusinessClasses.Schedule LocalSchedule { get; set; }
        bool SettingsNotSaved { get; set; }
        DevComponents.DotNetBar.SuperTooltipInfo HelpToolTip { get; }
        void UpdateOutput(bool quickLoad);
        void PrintOutput();
        void Email();
        void OpenHelp();
    }
}
