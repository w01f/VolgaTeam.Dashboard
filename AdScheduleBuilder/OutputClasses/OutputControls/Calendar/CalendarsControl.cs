using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CalendarsControl : UserControl
    {
        private static CalendarsControl _instance;
        private ICalendarControl _selectedOutput = null;

        private CalendarsControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public static CalendarsControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CalendarsControl();
                return _instance;
            }
        }

        public bool AllowToLeaveControl
        {
            get
            {
                bool result = false;
                if (_selectedOutput != null && _selectedOutput.SettingsNotSaved)
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

        private Calendar.SettingsViewers.ICalendarSettingsViewer GetSettingsViwerAccordingToggledButton(DevComponents.DotNetBar.ButtonItem toggledButton)
        {
            Calendar.SettingsViewers.ICalendarSettingsViewer result = null;
            if (toggledButton == FormMain.Instance.buttonItemCalendarsShowTitle)
                result = new Calendar.SettingsViewers.TitleViewerControl();
            else if (toggledButton == FormMain.Instance.buttonItemCalendarsShowComment)
                result = new Calendar.SettingsViewers.CommentViewerControl();
            else if (toggledButton == FormMain.Instance.buttonItemCalendarsShowLegend)
                result = new Calendar.SettingsViewers.LegendViewerControl();
            else if (toggledButton == FormMain.Instance.buttonItemCalendarsShowLogo)
                result = new Calendar.SettingsViewers.LogoViewerControl();
            else if (toggledButton == FormMain.Instance.buttonItemCalendarsShowBusinessName)
                result = new Calendar.SettingsViewers.BusinessNameViewerControl();
            else if (toggledButton == FormMain.Instance.buttonItemCalendarsShowActiveDays)
                result = new Calendar.SettingsViewers.TotalDaysViewerControl();
            else if (toggledButton == FormMain.Instance.buttonItemCalendarsShowAvgCost)
                result = new Calendar.SettingsViewers.AvgCostViewerControl();
            else if (toggledButton == FormMain.Instance.buttonItemCalendarsShowDecisionMaker)
                result = new Calendar.SettingsViewers.DecisionMakerViewerControl();
            else if (toggledButton == FormMain.Instance.buttonItemCalendarsShowTotalAds)
                result = new Calendar.SettingsViewers.TotalAdsViewerControl();
            else if (toggledButton == FormMain.Instance.buttonItemCalendarsShowTotalCost)
                result = new Calendar.SettingsViewers.TotalCostViewerControl();
            return result;
        }

        public void UpdatePageAccordingToggledButton()
        {
            _selectedOutput = OutputControls.OutputCalendarControl.Instance;
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
                FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemCalendarsHelp, _selectedOutput.HelpToolTip);
                _selectedOutput.ApplySettings();
                _selectedOutput.UpdateMonthView();
                (_selectedOutput as Control).BringToFront();
                pnMain.BringToFront();
            }
            else
            {
                pnEmpty.BringToFront();
                FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemCalendarsHelp, null);
            }
        }

        public void comboBoxEditCalendar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedOutput != null)
            {
                _selectedOutput.UpdateMonthView();
            }
        }

        public void buttonItemCalendarsToggledAdditional_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem button = sender as DevComponents.DotNetBar.ButtonItem;
            if (button != null && _selectedOutput != null)
            {
                Calendar.SettingsViewers.ICalendarSettingsViewer settingsViewer = GetSettingsViwerAccordingToggledButton(button);
                if (button.Checked)
                {
                    using (OutputClasses.OutputForms.FormCalendarToggleChange form = new OutputForms.FormCalendarToggleChange())
                    {
                        form.Text = settingsViewer.FormToggleChangeCaption;
                        form.buttonXEdit.Text = settingsViewer.EditButtonText;
                        DialogResult formResult = form.ShowDialog();
                        if (formResult == DialogResult.Yes)
                            button.Checked = false;
                        else if (formResult == DialogResult.No)
                        {
                            _selectedOutput.ShowOutputOptions(settingsViewer);
                        }
                    }
                }
                else
                {
                    button.Checked = true;
                    _selectedOutput.ShowOutputOptions(settingsViewer);
                }
            }
        }

        public void buttonItemCalendarsToggled_CheckedChanged(object sender, EventArgs e)
        {
            if (_selectedOutput != null)
                _selectedOutput.UpdateToggledOptions();
        }

        public void buttonItemCalendarsToggledSize_CheckedChanged(object sender, EventArgs e)
        {
            if (_selectedOutput != null)
            {
                if (_selectedOutput.AllowToSave)
                {
                    _selectedOutput.AllowToSave = false;
                    if (sender == FormMain.Instance.buttonItemCalendarsShowAdSize)
                    {
                        if (FormMain.Instance.buttonItemCalendarsShowAdSize.Checked && (FormMain.Instance.buttonItemCalendarsShowPageSize.Checked || FormMain.Instance.buttonItemCalendarsShowPercentOfPage.Checked))
                        {
                            FormMain.Instance.buttonItemCalendarsShowPageSize.Checked = false;
                            FormMain.Instance.buttonItemCalendarsShowPercentOfPage.Checked = false;
                        }
                    }
                    else if (sender == FormMain.Instance.buttonItemCalendarsShowPageSize)
                    {
                        if (FormMain.Instance.buttonItemCalendarsShowPageSize.Checked && (FormMain.Instance.buttonItemCalendarsShowAdSize.Checked || FormMain.Instance.buttonItemCalendarsShowPercentOfPage.Checked))
                        {
                            FormMain.Instance.buttonItemCalendarsShowAdSize.Checked = false;
                            FormMain.Instance.buttonItemCalendarsShowPercentOfPage.Checked = false;
                        }
                    }
                    else if (sender == FormMain.Instance.buttonItemCalendarsShowPercentOfPage)
                    {
                        if (FormMain.Instance.buttonItemCalendarsShowPercentOfPage.Checked && (FormMain.Instance.buttonItemCalendarsShowAdSize.Checked || FormMain.Instance.buttonItemCalendarsShowPageSize.Checked))
                        {
                            FormMain.Instance.buttonItemCalendarsShowAdSize.Checked = false;
                            FormMain.Instance.buttonItemCalendarsShowPageSize.Checked = false;
                        }
                    }
                    _selectedOutput.AllowToSave = true;
                }
            }
        }

        public void buttonItemCalendarsThemeColor_Click(object sender, EventArgs e)
        {
            if (_selectedOutput != null)
            {
                _selectedOutput.AllowToSave = false;
                FormMain.Instance.buttonItemCalendarsColorBlack.Checked = false;
                FormMain.Instance.buttonItemCalendarsColorBlue.Checked = false;
                FormMain.Instance.buttonItemCalendarsColorGray.Checked = false;
                FormMain.Instance.buttonItemCalendarsColorGreen.Checked = false;
                FormMain.Instance.buttonItemCalendarsColorOrange.Checked = false;
                FormMain.Instance.buttonItemCalendarsColorTeal.Checked = false;
                _selectedOutput.AllowToSave = true;
                (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
            }
        }

        public void buttonItemCalendarsPreview_Click(object sender, EventArgs e)
        {
            if (_selectedOutput != null)
                _selectedOutput.Preview();
        }

        public void buttonItemCalendarsPowerPoint_Click(object sender, EventArgs e)
        {
            if (_selectedOutput != null)
                _selectedOutput.PrintOutput();
        }

        public void buttonItemCalendarsEmail_Click(object sender, EventArgs e)
        {
            if (_selectedOutput != null)
                _selectedOutput.Email();
        }

        public void buttonItemCalendarSave_Click(object sender, EventArgs e)
        {
            SaveSchedule();
            AppManager.ShowInformation("Schedule Saved");
        }

        public void buttonItemCalendarSaveAs_Click(object sender, EventArgs e)
        {
            if (_selectedOutput != null)
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.InitialDirectory = ConfigurationClasses.SettingsManager.Instance.SaveFolder;
                    dialog.Title = "Save Schedule As...";
                    dialog.Filter = "Schedule Files|*.xml";
                    dialog.FileName = _selectedOutput.LocalSchedule.Name + ".xml";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        SaveSchedule(dialog.FileName.Replace(".xml", ""));
                        AppManager.ShowInformation("Schedule Saved");
                    }
                }
        }

        public void buttonItemCalendarsHelp_Click(object sender, EventArgs e)
        {
            if (_selectedOutput != null)
                _selectedOutput.OpenHelp();
        }
    }

    public interface ICalendarControl
    {
        BusinessClasses.Schedule LocalSchedule { get; set; }
        bool SettingsNotSaved { get; set; }
        DevComponents.DotNetBar.SuperTooltipInfo HelpToolTip { get; }
        bool AllowToSave { get; set; }
        List<BusinessClasses.Insert> Inserts { get; }

        void ApplySettings();
        void UpdateMonthView();
        void UpdateToggledOptions();
        void ShowOutputOptions(Calendar.SettingsViewers.ICalendarSettingsViewer settingsViewer);
        void UpdateOutput(bool quickLoad);
        void PrintOutput();
        void Email();
        void Preview();
        void OpenHelp();
    }
}
