using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace TVScheduleBuilder.ToolForms
{
    public partial class FormSelectOutput : Form
    {
        private string _descriptionExcel = string.Empty;
        private string _descriptionImage = string.Empty;
        private string _descriptionTable = string.Empty;
        private string _descriptionSlideMaster = string.Empty;
        private string _descriptionGroupedObjects = string.Empty;

        private string _helpKey = string.Empty;

        public bool IsEmailOutput { get; set; }
        public int ExcelBasedSlideCount { get; set; }
        public int TableBasedSlideCount { get; set; }
        public int TagsBasedSlideCount { get; set; }

        public string TemplatePath
        {
            get
            {
                if (layoutViewThemes.FocusedRowHandle >= 0 && layoutViewThemes.FocusedRowHandle < BusinessClasses.ThemeManager.Instance.Themes.Count)
                    return BusinessClasses.ThemeManager.Instance.Themes[layoutViewThemes.GetDataSourceRowIndex(layoutViewThemes.FocusedRowHandle)].FilePath;
                else
                    return string.Empty;
            }
        }

        public FormSelectOutput()
        {
            InitializeComponent();

            gridControlThemes.DataSource = BusinessClasses.ThemeManager.Instance.Themes;

            BusinessClasses.Theme selectedTheme = BusinessClasses.ThemeManager.Instance.Themes.FirstOrDefault(x => x.FilePath.Equals(ConfigurationClasses.SettingsManager.Instance.SelectedTemplatePath));
            int selectedTemplateIndex = 0;
            if (selectedTheme != null)
                selectedTemplateIndex = BusinessClasses.ThemeManager.Instance.Themes.IndexOf(selectedTheme);
            if (BusinessClasses.ThemeManager.Instance.Themes.Count > 0)
                layoutViewThemes.FocusedRowHandle = selectedTemplateIndex;
            layoutViewThemes.FocusedRowChanged+=new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(layoutViewThemes_FocusedRowChanged);
        }

        private void buttonXOutputType_Click(object sender, System.EventArgs e)
        {
            DevComponents.DotNetBar.ButtonX button = sender as DevComponents.DotNetBar.ButtonX;
            if (button != null && !button.Checked)
            {
                buttonXExcel.Checked = false;
                buttonXGrid.Checked = false;
                buttonXGroupedObjects.Checked = false;
                buttonXImage.Checked = false;
                buttonXSlideMaster.Checked = false;
                buttonXExcel.Image = Properties.Resources.ExcelInactive;
                buttonXGrid.Image = Properties.Resources.GridInactive;
                buttonXGroupedObjects.Image = Properties.Resources.TextGroupedInactive;
                buttonXImage.Image = Properties.Resources.ImageInactive;
                buttonXSlideMaster.Image = Properties.Resources.SlideMasterInactive;
                button.Checked = true;
            }
        }

        private void buttonXOutputType_CheckedChanged(object sender, System.EventArgs e)
        {
            if (xtraTabControl.SelectedTabPage == xtraTabPageFormat)
            {
                if (buttonXExcel.Checked)
                {
                    laOutputTitle.Text = "Excel Grid";
                    laOutputDescription.Text = _descriptionExcel;
                    labelControlSlidesCount.Text = string.Format("Estimated Slides: <b>{0}</b>", this.ExcelBasedSlideCount.ToString());
                    buttonXOutput.DialogResult = System.Windows.Forms.DialogResult.No;
                    buttonXExcel.Image = Properties.Resources.Excel;
                    _helpKey = "outputgrid";
                }
                else if (buttonXGrid.Checked)
                {
                    laOutputTitle.Text = "Slide Table";
                    laOutputDescription.Text = _descriptionTable;
                    labelControlSlidesCount.Text = string.Format("Estimated Slides: <b>{0}</b>", this.TableBasedSlideCount.ToString());
                    buttonXOutput.DialogResult = System.Windows.Forms.DialogResult.Yes;
                    buttonXGrid.Image = Properties.Resources.Grid;
                    _helpKey = "outputtable";
                }
                else if (buttonXGroupedObjects.Checked)
                {
                    laOutputTitle.Text = "Grouped Objects";
                    laOutputDescription.Text = _descriptionGroupedObjects;
                    labelControlSlidesCount.Text = string.Format("Estimated Slides: <b>{0}</b>", this.TagsBasedSlideCount.ToString());
                    buttonXOutput.DialogResult = System.Windows.Forms.DialogResult.Abort;
                    buttonXGroupedObjects.Image = Properties.Resources.TextGrouped;
                    _helpKey = "outputgrouped";
                }
                else if (buttonXImage.Checked)
                {
                    laOutputTitle.Text = "Single Image";
                    laOutputDescription.Text = _descriptionImage;
                    labelControlSlidesCount.Text = string.Format("Estimated Slides: <b>{0}</b>", this.ExcelBasedSlideCount.ToString());
                    buttonXOutput.DialogResult = System.Windows.Forms.DialogResult.Ignore;
                    buttonXImage.Image = Properties.Resources.Image;
                    _helpKey = "outputimage";
                }
                else if (buttonXSlideMaster.Checked)
                {
                    laOutputTitle.Text = "Slide Master";
                    laOutputDescription.Text = _descriptionSlideMaster;
                    labelControlSlidesCount.Text = string.Format("Estimated Slides: <b>{0}</b>", this.TagsBasedSlideCount.ToString());
                    buttonXOutput.DialogResult = System.Windows.Forms.DialogResult.Retry;
                    buttonXSlideMaster.Image = Properties.Resources.SlideMaster;
                    _helpKey = "outputmaster";
                }
            }
            else
            {
                laOutputTitle.Text = "Slide Theme";
                _helpKey = "theme";
            }
        }

        private void FormSelectOutput_Load(object sender, System.EventArgs e)
        {
            xtraTabPageTheme.PageEnabled = BusinessClasses.ThemeManager.Instance.Themes.Count > 0;

            if (this.IsEmailOutput)
            {
                _descriptionExcel = string.Format("Email your Television Schedule as an Excel Grid on a PowerPoint slide.{0}{0}The schedule is created in Excel, and then the Excel Grid is pasted to the slide. Sometimes this process will crash Excel. It is a little “buggy”…{0}{0}These slides usually generate in just a few seconds.", Environment.NewLine);
                _descriptionImage = string.Format("Email your Television Schedule as a single image on a PowerPoint slide.{0}{0}The schedule is created in Excel, and then pasted as an IMAGE to your slide. The image is easy to re-size on the slide.{0}{0}These slides usually generate in just a few seconds.", Environment.NewLine);
                _descriptionTable = string.Format("Email your Television Schedule as a PowerPoint Table.{0}{0}Slide Tables are usually recommended for relatively small Spot Schedules.{0}{0}These slides usually generate in just a few seconds.", Environment.NewLine);
                _descriptionSlideMaster = string.Format("Email your Television Schedule on a PowerPoint slide master.{0}{0}The Schedule will be LOCKED into position on the slide master, and it fills up most of the slide.{0}{0}These slides usually generate in less than a minute or so.", Environment.NewLine);
                _descriptionGroupedObjects = string.Format("Email your Television Schedule as Grouped Objects on a PowerPoint slide.{0}{0}All of the individual textboxes and images that make up the schedule are grouped together before they output to the slide.{0}{0}These slides usually generate in less than a minute or so.", Environment.NewLine);
                this.Text = "Email Attachment Options";
                buttonXOutput.Text = "Send to Email";
                buttonXOutput.Image = Properties.Resources.Email;
                buttonXPreview.Visible = false;
                buttonXOutput.Location = buttonXPreview.Location;
                buttonXOutput.Tooltip = "Email this schedule as attachment";
            }
            else
            {
                _descriptionExcel = string.Format("Send your Television Schedule to the PowerPoint slide as an Excel Grid.{0}{0}The schedule is created in Excel, and then the Excel Grid is pasted to the slide. Sometimes this process will crash Excel. It is a little “buggy”…{0}{0}These slides usually generate in just a few seconds.", Environment.NewLine);
                _descriptionImage = string.Format("Send your Television Schedule to the PowerPoint slide as single image.{0}{0}The schedule is created in Excel, and then pasted as an IMAGE to your slide. The image is easy to re-size on the slide.{0}{0}These slides usually generate in just a few seconds.", Environment.NewLine);
                _descriptionTable = string.Format("Send your Television Schedule to PowerPoint as a Table.{0}{0}Slide Tables are usually recommended for relatively small Spot Schedules.{0}{0}These slides usually generate in just a few seconds.", Environment.NewLine);
                _descriptionSlideMaster = string.Format("Send your Television Schedule to PowerPoint as a Unique Slide Master.{0}{0}The Schedule will be LOCKED into position on the slide master, and it fills up most of the slide.{0}{0}These slides usually generate in less than a minute or so.", Environment.NewLine);
                _descriptionGroupedObjects = string.Format("Send your Television Schedule to the PowerPoint slide as a Grouped Object.{0}{0}All of the individual textboxes and images that make up the schedule are grouped together before they output to the slide.{0}{0}These slides usually generate in less than a minute or so.", Environment.NewLine);
                this.Text = "Slide Output Options";
                buttonXOutput.Text = "Slide Output";
                buttonXOutput.Image = Properties.Resources.PowerPoint;
                buttonXOutput.Tooltip = "Output this schedule to your PowerPoint presentation";
                buttonXPreview.Visible = true;
            }

            if (buttonXGrid.Enabled)
                buttonXGrid.Checked = true;
            else if (buttonXSlideMaster.Enabled)
                buttonXSlideMaster.Checked = true;
            else if (buttonXGroupedObjects.Enabled)
                buttonXGroupedObjects.Checked = true;
            else if (buttonXImage.Enabled)
                buttonXImage.Checked = true;
            else if (buttonXExcel.Enabled)
                buttonXExcel.Checked = true;

            ConfigurationClasses.RegistryHelper.MainFormHandle = this.Handle;
            ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
        }

        private void pbHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink(_helpKey);
        }

        private void layoutViewThemes_CustomFieldValueStyle(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewFieldValueStyleEventArgs e)
        {
            if ((e.State & DevExpress.XtraGrid.Views.Base.GridRowCellState.Focused) == DevExpress.XtraGrid.Views.Base.GridRowCellState.Focused)
                e.Appearance.BackColor = SystemColors.Highlight;
        }

        private void xtraTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            buttonXOutputType_CheckedChanged(null, null);
            if (e.Page == xtraTabPageTheme)
                gridControlThemes.Focus();
        }

        private void layoutViewThemes_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (layoutViewThemes.FocusedRowHandle >= 0)
            {
                BusinessClasses.Theme selectedTheme = BusinessClasses.ThemeManager.Instance.Themes[layoutViewThemes.GetDataSourceRowIndex(layoutViewThemes.FocusedRowHandle)];
                ConfigurationClasses.SettingsManager.Instance.SelectedTemplatePath = selectedTheme.FilePath;
                ConfigurationClasses.SettingsManager.Instance.SaveApplicationSettings();
            }
        }

        #region Picture Box Clicks Habdlers
        /// <summary>
        /// Buttonize the PictureBox 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top += 1;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top -= 1;
        }
        #endregion
    }
}
