using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputForms
{
    public partial class FormCalendarOutputOptions : Form
    {
        private ConfigurationClasses.MonthCalendarViewSettings _selectedMonthSettings = null;
        public List<ConfigurationClasses.MonthCalendarViewSettings> Settings { get; private set; }
        public AdScheduleBuilder.OutputClasses.OutputControls.Calendar.SettingsViewers.ICalendarSettingsViewer SettingsViewer { get; set; }

        public FormCalendarOutputOptions()
        {
            InitializeComponent();
            this.Settings = new List<ConfigurationClasses.MonthCalendarViewSettings>();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
                checkEditApplyForAll.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
                buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
                buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
            }
        }

        private void SaveSelectedMonthSettings()
        {
            if (this.SettingsViewer != null)
            {
                this.SettingsViewer.SaveSettings();
                if (checkEditApplyForAll.Checked)
                    this.SettingsViewer.ApplySettingsForAll(this.Settings.ToArray());
            }
        }

        private void FormCalendarNotes_Load(object sender, EventArgs e)
        {
            listBoxControlMonth.Items.Clear();
            listBoxControlMonth.Items.AddRange(this.Settings.Select(x => x.Month.ToString("MMMM, yyyy")).ToArray());
            if (this.Settings.Count > 0)
                listBoxControlMonth.SelectedIndex = 0;
        }

        private void FormCalendarNotes_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSelectedMonthSettings();
        }

        private void listBoxControlMonth_SelectedValueChanged(object sender, EventArgs e)
        {
            SaveSelectedMonthSettings();
            if (listBoxControlMonth.SelectedIndex >= 0 && this.SettingsViewer != null)
            {
                _selectedMonthSettings = this.Settings[listBoxControlMonth.SelectedIndex];
                this.SettingsViewer.LoadSettings(_selectedMonthSettings);
                if (!pnSettingsViewer.Controls.Contains(this.SettingsViewer as Control))
                    pnSettingsViewer.Controls.Add(this.SettingsViewer as Control);
                (this.SettingsViewer as Control).BringToFront();
                laTitle.Text = this.SettingsViewer.Title;
                pnApplyForAll.Visible = this.SettingsViewer.ShowApplyForAll;
                checkEditApplyForAll.Text = this.SettingsViewer.ApplyForAllText;
            }
        }
    }
}