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
            using (vbAccelerator.Components.Shell.ShellLink shortcut = new vbAccelerator.Components.Shell.ShellLink())
            {
                shortcut.Target = this.Application.Executable;
                shortcut.WorkingDirectory = Path.GetDirectoryName(this.Application.Executable);
                shortcut.Description = this.Application.Title.Replace("\n", " ").Replace("\r", string.Empty);
                shortcut.DisplayMode = vbAccelerator.Components.Shell.ShellLink.LinkDisplayMode.edmNormal;
                if (File.Exists(this.Application.Icon))
                    shortcut.IconPath = this.Application.Icon;
                shortcut.IconIndex = 0;
                shortcut.Save(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), this.Application.Title.Replace("\n", " ").Replace("\r", string.Empty) + ".lnk"));
            }
        }
    }
}
