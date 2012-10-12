using System;
using System.Windows.Forms;

namespace NewBizWizForm.TabHomeForms.Dashboard
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class DashboardCode14Control : UserControl
    {
        public DashboardCode14Control()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            if (!System.IO.Directory.Exists(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.RadioScheduleSlideFolder) || System.IO.Directory.GetDirectories(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.RadioScheduleSlideFolder).Length == 0)
            {
                pbRadio.Image = Properties.Resources.HomeRadioDisabled;
                pbRadio.Enabled = false;
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

        private void pbRadio_Click(object sender, EventArgs e)
        {
            if (pbRadio.Enabled)
                FormMain.Instance.ribbonTabItemRadio.Select();
        }

        private void pbDigital_Click(object sender, EventArgs e)
        {
            FormMain.Instance.ribbonTabItemDigital.Select();
        }

        private void pbCalendar_Click(object sender, EventArgs e)
        {
            FormMain.Instance.ribbonTabItemCalendar.Select();
        }
    }
}
