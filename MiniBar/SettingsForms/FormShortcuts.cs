using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MiniBar.SettingsForms
{
    public partial class FormShortcuts : Form
    {
        public FormShortcuts()
        {
            InitializeComponent();
            laDashboard.Text = ConfigurationClasses.SettingsManager.Instance.DashboardName;
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.DashboardLogoPath) && File.Exists(ConfigurationClasses.SettingsManager.Instance.DashboardIconPath))
                pbDashboard.Image = new Bitmap(ConfigurationClasses.SettingsManager.Instance.DashboardLogoPath);
            laSalesDepot.Text = ConfigurationClasses.SettingsManager.Instance.SalesDepotName;
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.SalesDepotLogoPath) && File.Exists(ConfigurationClasses.SettingsManager.Instance.SalesDepotIconPath))
                pbSalesDepot.Image = new Bitmap(ConfigurationClasses.SettingsManager.Instance.SalesDepotLogoPath);
            foreach (BusinessClasses.NBWApplication application in BusinessClasses.NBWApplicationsManager.Instance.NBWApplications)
            {
                ApplicationDefinitionControl definition = new ApplicationDefinitionControl();
                definition.Application = application;
                definition.UpdateView();
                xtraScrollableControl.Controls.Add(definition);
                definition.BringToFront();
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

        private void pbDashboard_Click(object sender, EventArgs e)
        {
            using (vbAccelerator.Components.Shell.ShellLink shortcut = new vbAccelerator.Components.Shell.ShellLink())
            {
                shortcut.Target = ConfigurationClasses.SettingsManager.Instance.DashboardPath;
                shortcut.WorkingDirectory = Path.GetDirectoryName(ConfigurationClasses.SettingsManager.Instance.DashboardPath);
                shortcut.Description = ConfigurationClasses.SettingsManager.Instance.DashboardName;
                shortcut.DisplayMode = vbAccelerator.Components.Shell.ShellLink.LinkDisplayMode.edmNormal;
                if (File.Exists(ConfigurationClasses.SettingsManager.Instance.DashboardLogoPath) && File.Exists(ConfigurationClasses.SettingsManager.Instance.DashboardIconPath))
                    shortcut.IconPath = ConfigurationClasses.SettingsManager.Instance.DashboardIconPath;
                shortcut.IconIndex = 0;
                shortcut.Save(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), ConfigurationClasses.SettingsManager.Instance.DashboardName + ".lnk"));
            }
        }

        private void pbSalesDepot_Click(object sender, EventArgs e)
        {
            using (vbAccelerator.Components.Shell.ShellLink shortcut = new vbAccelerator.Components.Shell.ShellLink())
            {
                shortcut.Target = ConfigurationClasses.SettingsManager.Instance.SalesDepotExecutablePath;
                shortcut.WorkingDirectory = Path.GetDirectoryName(ConfigurationClasses.SettingsManager.Instance.SalesDepotExecutablePath);
                shortcut.Description = ConfigurationClasses.SettingsManager.Instance.SalesDepotName;
                shortcut.DisplayMode = vbAccelerator.Components.Shell.ShellLink.LinkDisplayMode.edmNormal;
                if (File.Exists(ConfigurationClasses.SettingsManager.Instance.SalesDepotLogoPath) && File.Exists(ConfigurationClasses.SettingsManager.Instance.SalesDepotIconPath))
                    shortcut.IconPath = ConfigurationClasses.SettingsManager.Instance.SalesDepotIconPath;
                shortcut.IconIndex = 0;
                shortcut.Save(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), ConfigurationClasses.SettingsManager.Instance.SalesDepotName + ".lnk"));
            }
        }
    }
}
