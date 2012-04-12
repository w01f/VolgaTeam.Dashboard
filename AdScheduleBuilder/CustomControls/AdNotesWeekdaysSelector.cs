using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AdScheduleBuilder.CustomControls
{
    public partial class AdNotesWeekdaysSelector : UserControl
    {
        public AdNotesWeekdaysSelector()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                ckEnableWeekdays.Font = new Font(ckEnableWeekdays.Font.FontFamily, ckEnableWeekdays.Font.Size - 2, ckEnableWeekdays.Font.Style);
                buttonXClearAll.Font = new Font(buttonXClearAll.Font.FontFamily, buttonXClearAll.Font.Size - 2, buttonXClearAll.Font.Style);
                buttonXFriday.Font = new Font(buttonXFriday.Font.FontFamily, buttonXFriday.Font.Size - 2, buttonXFriday.Font.Style);
                buttonXMonday.Font = new Font(buttonXMonday.Font.FontFamily, buttonXMonday.Font.Size - 2, buttonXMonday.Font.Style);
                buttonXSaturday.Font = new Font(buttonXSaturday.Font.FontFamily, buttonXSaturday.Font.Size - 2, buttonXSaturday.Font.Style);
                buttonXSelectAll.Font = new Font(buttonXSelectAll.Font.FontFamily, buttonXSelectAll.Font.Size - 2, buttonXSelectAll.Font.Style);
                buttonXSunday.Font = new Font(buttonXSunday.Font.FontFamily, buttonXSunday.Font.Size - 2, buttonXSunday.Font.Style);
                buttonXThursday.Font = new Font(buttonXThursday.Font.FontFamily, buttonXThursday.Font.Size - 2, buttonXThursday.Font.Style);
                buttonXTuesday.Font = new Font(buttonXTuesday.Font.FontFamily, buttonXTuesday.Font.Size - 2, buttonXTuesday.Font.Style);
                buttonXWednesday.Font = new Font(buttonXWednesday.Font.FontFamily, buttonXWednesday.Font.Size - 2, buttonXWednesday.Font.Style);
            }
        }

        public DayOfWeek[] SelectedDays
        {
            get
            {
                List<DayOfWeek> result = new List<DayOfWeek>();
                if (ckEnableWeekdays.Checked)
                {
                    if (buttonXMonday.Checked)
                        result.Add(DayOfWeek.Monday);
                    if (buttonXTuesday.Checked)
                        result.Add(DayOfWeek.Tuesday);
                    if (buttonXWednesday.Checked)
                        result.Add(DayOfWeek.Wednesday);
                    if (buttonXThursday.Checked)
                        result.Add(DayOfWeek.Thursday);
                    if (buttonXFriday.Checked)
                        result.Add(DayOfWeek.Friday);
                    if (buttonXSaturday.Checked)
                        result.Add(DayOfWeek.Saturday);
                    if (buttonXSunday.Checked)
                        result.Add(DayOfWeek.Sunday);
                }
                return result.ToArray();
            }
        }

        private void ckCommentAll_CheckedChanged(object sender, System.EventArgs e)
        {
            buttonXClearAll.Enabled = ckEnableWeekdays.Checked;
            buttonXFriday.Enabled = ckEnableWeekdays.Checked;
            buttonXMonday.Enabled = ckEnableWeekdays.Checked;
            buttonXSaturday.Enabled = ckEnableWeekdays.Checked;
            buttonXSelectAll.Enabled = ckEnableWeekdays.Checked;
            buttonXSunday.Enabled = ckEnableWeekdays.Checked;
            buttonXThursday.Enabled = ckEnableWeekdays.Checked;
            buttonXTuesday.Enabled = ckEnableWeekdays.Checked;
            buttonXWednesday.Enabled = ckEnableWeekdays.Checked;
        }

        private void buttonXSelectAll_Click(object sender, System.EventArgs e)
        {
            buttonXFriday.Checked = true;
            buttonXMonday.Checked = true;
            buttonXSaturday.Checked = true;
            buttonXSunday.Checked = true;
            buttonXThursday.Checked = true;
            buttonXTuesday.Checked = true;
            buttonXWednesday.Checked = true;
        }

        private void buttonXClearAll_Click(object sender, System.EventArgs e)
        {
            buttonXFriday.Checked = false;
            buttonXMonday.Checked = false;
            buttonXSaturday.Checked = false;
            buttonXSunday.Checked = false;
            buttonXThursday.Checked = false;
            buttonXTuesday.Checked = false;
            buttonXWednesday.Checked = false;
        }
    }
}
