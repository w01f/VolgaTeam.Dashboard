using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MiniBar.SettingsForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ApplicationDefinitionControl : UserControl
    {
        public BusinessClasses.NBWApplication Application { get; set; }

        public ApplicationDefinitionControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Top;
        }

        public void UpdateView()
        {
            pbLogo.Image = this.Application.Image;
            laTitle.Text = this.Application.Title;
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

        private void pbLogo_Click(object sender, EventArgs e)
        {
            this.Application.CreateShortcut();
        }
    }
}
