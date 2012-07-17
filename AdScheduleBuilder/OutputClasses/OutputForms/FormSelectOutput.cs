using System;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputForms
{
    public enum OutputType
    {
        Preview,
        PowerPoint,
        Email
    }

    public partial class FormSelectOutput : Form
    {
        private string _descriptionExcel = string.Empty;
        private string _descriptionImage = string.Empty;
        private string _descriptionTable = string.Empty;

        private string _helpKey = string.Empty;

        public FormSelectOutput(OutputType outputType)
        {
            InitializeComponent();

            switch (outputType)
            {
                case OutputType.Preview:
                    _descriptionExcel = string.Format("<b>PREVIEW</b> your AD Schedule in PowerPoint as an Excel Grid…{0}{0}Use this format if the Slide Table is unavailable…{0}{0}Sometimes, Excel will Crash if you have an older PC…", Environment.NewLine);
                    _descriptionImage = string.Format("<b>PREVIEW</b> your AD Schedule in PowerPoint as a BitMap Image.{0}{0}Use this format if the Slide Table is unavailable…", Environment.NewLine);
                    _descriptionTable = string.Format("<b>PREVIEW</b> your AD Schedule as a PowerPoint Slide Table.{0}{0}Slide Tables are the best-looking format. Choose this option if available…", Environment.NewLine);
                    this.Text = "Preview Options";
                    buttonXOutput.Text = "  PREVIEW";
                    buttonXOutput.Image = Properties.Resources.PreviewSelector;
                    buttonXOutput.Tooltip = "Preview this schedule";
                    break;
                case OutputType.PowerPoint:
                    _descriptionExcel = string.Format("<b>OUTPUT</b> your AD Schedule in PowerPoint as an Excel Grid…{0}{0}Use this format if the Slide Table is unavailable…{0}{0}Sometimes, Excel will Crash if you have an older PC…", Environment.NewLine);
                    _descriptionImage = string.Format("<b>OUTPUT</b> your AD Schedule in PowerPoint as a BitMap Image.{0}{0}Use this format if the Slide Table is unavailable…", Environment.NewLine);
                    _descriptionTable = string.Format("<b>OUTPUT</b> your AD Schedule to PowerPoint as a Slide Table.{0}{0}Slide Tables are the best-looking format. Choose this option if available…", Environment.NewLine);
                    this.Text = "Slide Output Options";
                    buttonXOutput.Text = "  OUTPUT";
                    buttonXOutput.Image = Properties.Resources.PowerPoint;
                    buttonXOutput.Tooltip = "Output this schedule to your PowerPoint presentation";
                    break;
                case OutputType.Email:
                    _descriptionExcel = string.Format("<b>EMAIL</b> your AD Schedule in PowerPoint as an Excel Grid…{0}{0}Use this format if the Slide Table is unavailable…{0}{0}Sometimes, Excel will Crash if you have an older PC…", Environment.NewLine);
                    _descriptionImage = string.Format("<b>EMAIL</b> your AD Schedule in PowerPoint as a BitMap Image.{0}{0}Use this format if the Slide Table is unavailable…", Environment.NewLine);
                    _descriptionTable = string.Format("<b>EMAIL</b> your AD Schedule as a PowerPoint Slide Table.{0}{0}Slide Tables are the best-looking format. Choose this option if available…", Environment.NewLine);
                    this.Text = "Email Attachment Options";
                    buttonXOutput.Text = "  EMAIL";
                    buttonXOutput.Image = Properties.Resources.Email;
                    buttonXOutput.Tooltip = "Email this schedule as attachment";
                    break;
            }
        }

        private void buttonXOutputType_Click(object sender, System.EventArgs e)
        {
            DevComponents.DotNetBar.ButtonX button = sender as DevComponents.DotNetBar.ButtonX;
            if (button != null && !button.Checked)
            {
                buttonXExcel.Checked = false;
                buttonXGrid.Checked = false;
                buttonXImage.Checked = false;
                buttonXExcel.Image = Properties.Resources.ExcelInactive;
                buttonXGrid.Image = Properties.Resources.TableInactive;
                buttonXImage.Image = Properties.Resources.ImageInactive;
                button.Checked = true;
            }
        }

        private void buttonXOutputType_CheckedChanged(object sender, System.EventArgs e)
        {
            if (buttonXExcel.Checked)
            {
                laOutputTitle.Text = "Excel Grid";
                labelControlDescription.Text = _descriptionExcel;
                buttonXOutput.DialogResult = System.Windows.Forms.DialogResult.No;
                buttonXExcel.Image = Properties.Resources.Excel;
                _helpKey = "excel";
            }
            else if (buttonXGrid.Checked)
            {
                laOutputTitle.Text = "Slide Table";
                labelControlDescription.Text = _descriptionTable;
                buttonXOutput.DialogResult = System.Windows.Forms.DialogResult.Yes;
                buttonXGrid.Image = Properties.Resources.Table;
                _helpKey = "table";
            }
            else if (buttonXImage.Checked)
            {
                laOutputTitle.Text = "Single Image";
                labelControlDescription.Text = _descriptionImage;
                buttonXOutput.DialogResult = System.Windows.Forms.DialogResult.Ignore;
                buttonXImage.Image = Properties.Resources.Image;
                _helpKey = "image";
            }
        }

        private void FormSelectOutput_Load(object sender, System.EventArgs e)
        {
            if (buttonXGrid.Enabled)
                buttonXGrid.Checked = true;
            else
                buttonXImage.Checked = true;

            ConfigurationClasses.RegistryHelper.MainFormHandle = this.Handle;
            ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
        }

        private void pbHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink(_helpKey);
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
