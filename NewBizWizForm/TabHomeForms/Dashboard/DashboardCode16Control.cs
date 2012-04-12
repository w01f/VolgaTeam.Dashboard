using System;
using System.Windows.Forms;

namespace NewBizWizForm.TabHomeForms.Dashboard
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class DashboardCode16Control : UserControl
    {
        public DashboardCode16Control()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
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

        private void pbNewspaper_Click(object sender, EventArgs e)
        {
            FormMain.Instance.ribbonTabItemNewspaper.Select();
        }

        private void pbDigital_Click(object sender, EventArgs e)
        {
            FormMain.Instance.ribbonTabItemDigital.Select();
        }

        private void pbCalendar_Click(object sender, EventArgs e)
        {
            FormMain.Instance.ribbonTabItemCalendar.Select();
        }

        private void pbTV_Click(object sender, EventArgs e)
        {
            FormMain.Instance.ribbonTabItemTV.Select();
        }
    }
}
