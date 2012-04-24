using System;
using System.Windows.Forms;

namespace NewBizWizForm.TabHomeForms.Dashboard
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class DashboardCode31Control : UserControl
    {
        public DashboardCode31Control()
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

        private void pbClientSolutionBig_Click(object sender, EventArgs e)
        {
            FormMain.Instance.buttonItemHomeCover_Click(null, null);
        }

        private void pbSalesDepotBig_Click(object sender, EventArgs e)
        {
            AppManager.Instance.RunSalesDepot();
            FormMain.Instance.Close();
        }
    }
}
