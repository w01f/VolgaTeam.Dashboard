using System;
using System.Windows.Forms;

namespace NewBizWizForm.TabHomeForms.Dashboard
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class DashboardCode10Control : UserControl
    {
        public DashboardCode10Control()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            if (!System.IO.Directory.Exists(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) || System.IO.Directory.GetDirectories(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length == 0)
            {
                pbTVBig.Image = Properties.Resources.HomeTVBigDisabled;
                pbTVBig.Enabled = false;
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

        private void pbTV_Click(object sender, EventArgs e)
        {
            if (pbTVBig.Enabled)
                FormMain.Instance.ribbonTabItemTV.Select();
        }

        private void pbDigitalBig_Click(object sender, EventArgs e)
        {
            FormMain.Instance.ribbonTabItemDigital.Select();
        }
    }
}
