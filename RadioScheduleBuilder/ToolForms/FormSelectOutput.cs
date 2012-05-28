﻿using System;
using System.Windows.Forms;

namespace RadioScheduleBuilder.ToolForms
{
    public partial class FormSelectOutput : Form
    {
        private string _descriptionExcel = string.Empty;
        private string _descriptionImage = string.Empty;
        private string _descriptionTable = string.Empty;
        private string _descriptionSlideMaster = string.Empty;
        private string _descriptionGroupedObjects = string.Empty;

        public bool IsEmailOutput { get; set; }
        public int ExcelBasedSlideCount { get; set; }
        public int TableBasedSlideCount { get; set; }
        public int TagsBasedSlideCount { get; set; }

        public FormSelectOutput()
        {
            InitializeComponent();
        }

        private void buttonXOutputType_Click(object sender, System.EventArgs e)
        {
            buttonXExcel.Checked = false;
            buttonXGrid.Checked = false;
            buttonXGroupedObjects.Checked = false;
            buttonXImage.Checked = false;
            buttonXSlideMaster.Checked = false;
            (sender as DevComponents.DotNetBar.ButtonX).Checked = true;
        }

        private void buttonXOutputType_CheckedChanged(object sender, System.EventArgs e)
        {
            if (buttonXExcel.Checked)
            {
                laOutputTitle.Text = "Excel Grid";
                laOutputDescription.Text = _descriptionExcel;
                labelControlSlidesCount.Text = string.Format("Estimated Slides: <b>{0}</b>", this.ExcelBasedSlideCount.ToString());
                buttonXOutput.DialogResult = System.Windows.Forms.DialogResult.No;
            }
            else if (buttonXGrid.Checked)
            {
                laOutputTitle.Text = "Slide Table";
                laOutputDescription.Text = _descriptionTable;
                labelControlSlidesCount.Text = string.Format("Estimated Slides: <b>{0}</b>", this.TableBasedSlideCount.ToString());
                buttonXOutput.DialogResult = System.Windows.Forms.DialogResult.Yes;
            }
            else if (buttonXGroupedObjects.Checked)
            {
                laOutputTitle.Text = "Grouped Objects";
                laOutputDescription.Text = _descriptionGroupedObjects;
                labelControlSlidesCount.Text = string.Format("Estimated Slides: <b>{0}</b>", this.TagsBasedSlideCount.ToString());
                buttonXOutput.DialogResult = System.Windows.Forms.DialogResult.Abort;
            }
            else if (buttonXImage.Checked)
            {
                laOutputTitle.Text = "Single Image";
                laOutputDescription.Text = _descriptionImage;
                labelControlSlidesCount.Text = string.Format("Estimated Slides: <b>{0}</b>", this.ExcelBasedSlideCount.ToString());
                buttonXOutput.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            }
            else if (buttonXSlideMaster.Checked)
            {
                laOutputTitle.Text = "Slide Master";
                laOutputDescription.Text = _descriptionSlideMaster;
                labelControlSlidesCount.Text = string.Format("Estimated Slides: <b>{0}</b>", this.TagsBasedSlideCount.ToString());
                buttonXOutput.DialogResult = System.Windows.Forms.DialogResult.Retry;
            }
        }

        private void FormSelectOutput_Load(object sender, System.EventArgs e)
        {
            if (this.IsEmailOutput)
            {
                _descriptionExcel = string.Format("Email your Radio Schedule as an Excel Grid on a PowerPoint slide.{0}{0}The schedule is created in Excel, and then the Excel Grid is pasted to the slide. Sometimes this process will crash Excel. It is a little “buggy”…{0}{0}These slides usually generate in just a few seconds.", Environment.NewLine);
                _descriptionImage = string.Format("Email your Radio Schedule as a single image on a PowerPoint slide.{0}{0}The schedule is created in Excel, and then pasted as an IMAGE to your slide. The image is easy to re-size on the slide.{0}{0}These slides usually generate in just a few seconds.", Environment.NewLine);
                _descriptionTable = string.Format("Email your Radio Schedule as a PowerPoint Table.{0}{0}Slide Tables are usually recommended for relatively small Spot Schedules.{0}{0}These slides usually generate in just a few seconds.", Environment.NewLine);
                _descriptionSlideMaster = string.Format("Email your Radio Schedule on a PowerPoint slide master.{0}{0}The Schedule will be LOCKED into position on the slide master, and it fills up most of the slide.{0}{0}These slides usually generate in less than a minute or so.", Environment.NewLine);
                _descriptionGroupedObjects = string.Format("Email your Radio Schedule as Grouped Objects on a PowerPoint slide.{0}{0}All of the individual textboxes and images that make up the schedule are grouped together before they output to the slide.{0}{0}These slides usually generate in less than a minute or so.", Environment.NewLine);
                this.Text = "Email Attachment Options";
                buttonXOutput.Text = "Send to Email";
            }
            else
            {
                _descriptionExcel = string.Format("Send your Radio Schedule to the PowerPoint slide as an Excel Grid.{0}{0}The schedule is created in Excel, and then the Excel Grid is pasted to the slide. Sometimes this process will crash Excel. It is a little “buggy”…{0}{0}These slides usually generate in just a few seconds.", Environment.NewLine);
                _descriptionImage = string.Format("Send your Radio Schedule to the PowerPoint slide as single image.{0}{0}The schedule is created in Excel, and then pasted as an IMAGE to your slide. The image is easy to re-size on the slide.{0}{0}These slides usually generate in just a few seconds.", Environment.NewLine);
                _descriptionTable = string.Format("Send your Radio Schedule to PowerPoint as a Table.{0}{0}Slide Tables are usually recommended for relatively small Spot Schedules.{0}{0}These slides usually generate in just a few seconds.", Environment.NewLine);
                _descriptionSlideMaster = string.Format("Send your Radio Schedule to PowerPoint as a Unique Slide Master.{0}{0}The Schedule will be LOCKED into position on the slide master, and it fills up most of the slide.{0}{0}These slides usually generate in less than a minute or so.", Environment.NewLine);
                _descriptionGroupedObjects = string.Format("Send your Radio Schedule to the PowerPoint slide as a Grouped Object.{0}{0}All of the individual textboxes and images that make up the schedule are grouped together before they output to the slide.{0}{0}These slides usually generate in less than a minute or so.", Environment.NewLine);
                this.Text = "Slide Output Options";
                buttonXOutput.Text = "Send to PowerPoint";
            }

            if (buttonXGrid.Enabled)
                buttonXGrid.Checked = true;
            else
                buttonXSlideMaster.Checked = true;
        }
    }
}