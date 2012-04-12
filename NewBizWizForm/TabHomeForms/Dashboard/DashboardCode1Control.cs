using System;
using System.Windows.Forms;

namespace NewBizWizForm.TabHomeForms.Dashboard
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class DashboardCode1Control : UserControl
    {
        public DashboardCode1Control()
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

        private void pbOnline_Click(object sender, EventArgs e)
        {
            FormMain.Instance.ribbonTabItemOnline.Select();
        }

        private void pbMobile_Click(object sender, EventArgs e)
        {
            FormMain.Instance.ribbonTabItemMobile.Select();
        }
    }
}
