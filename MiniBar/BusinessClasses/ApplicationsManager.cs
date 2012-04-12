using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

namespace MiniBar.BusinessClasses
{
    public class NBWApplicationsManager
    {
        private static NBWApplicationsManager _instance = new NBWApplicationsManager();
        public List<NBWApplication> NBWApplications { get; set; }

        public static NBWApplicationsManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private NBWApplicationsManager()
        {
            this.NBWApplications = new List<NBWApplication>();
            DirectoryInfo nbwApplicationsRoot = new DirectoryInfo(ConfigurationClasses.SettingsManager.Instance.NBWApplicationsRootPath);
            if (nbwApplicationsRoot.Exists)
            {
                foreach (DirectoryInfo nbwApplicationRoot in nbwApplicationsRoot.GetDirectories())
                {
                    NBWApplication nbwApplication = new NBWApplication(nbwApplicationRoot);
                    if (nbwApplication.IsConfigured)
                        this.NBWApplications.Add(nbwApplication);
                }
                this.NBWApplications.Sort((x, y) => x.Order.CompareTo(y.Order));
            }
        }
    }

    public class NBWApplication
    {
        public bool IsConfigured { get; set; }
        public DirectoryInfo RootFolder { get; set; }
        public string Title { get; set; }
        public string Executable { get; set; }
        public Image Image { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
        public bool UseSlideTemplates { get; set; }
        public string SlideTemplatesPath { get; set; }

        public DevComponents.DotNetBar.LabelItem AppLabel { get; private set; }
        public DevComponents.DotNetBar.ButtonItem AppButton { get; set; }

        public NBWApplication(DirectoryInfo rootFolder)
        {
            this.Title = string.Empty;
            this.Executable = string.Empty;
            this.Order = 9999;
            this.RootFolder = rootFolder;
            this.SlideTemplatesPath = string.Empty;
            if (this.RootFolder.Exists)
            {
                LoadManifest();
                this.IsConfigured = !string.IsNullOrEmpty(this.Title) && !string.IsNullOrEmpty(this.Executable) && this.Image != null && this.Order != 999;
            }

            this.AppLabel = new DevComponents.DotNetBar.LabelItem();
            this.AppLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AppLabel.Text = this.Title;
            this.AppLabel.Click += new EventHandler(AppButton_Click);
            this.AppLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(AppButton_MouseDown);

            this.AppButton = new DevComponents.DotNetBar.ButtonItem();
            this.AppButton.Image = this.Image;
            this.AppButton.ImagePaddingHorizontal = 8;
            this.AppButton.SubItemsExpandWidth = 14;
            this.AppButton.Tag = this.Executable;
            this.AppButton.Click += new EventHandler(AppButton_Click);
            this.AppButton.MouseDown += new System.Windows.Forms.MouseEventHandler(AppButton_MouseDown);
        }

        void AppButton_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!this.AppButton.Enabled)
                System.Windows.Forms.MessageBox.Show("This App REQUIRES a Different slide background…\nChoose a different slide background on the Minibar PowerPoint tab…", "This App REQUIRES a Different slide background…", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
        }

        private void AppButton_Click(object sender, EventArgs e)
        {
            string executablePath = this.Executable;
            if (File.Exists(this.Executable))
                Process.Start(this.Executable);
        }


        public void LoadManifest()
        {
            XmlNode node;
            if (File.Exists(Path.Combine(this.RootFolder.FullName, ConfigurationClasses.SettingsManager.NBWApplicationManifestFileName)))
            {
                XmlDocument document = new XmlDocument();
                document.Load(Path.Combine(this.RootFolder.FullName, ConfigurationClasses.SettingsManager.NBWApplicationManifestFileName));
                node = document.SelectSingleNode(@"/Manifest/Title");
                if (node != null)
                    this.Title = node.InnerText;
                node = document.SelectSingleNode(@"/Manifest/Executable");
                if (node != null)
                    if (File.Exists(Path.Combine(this.RootFolder.FullName, node.InnerText)))
                        this.Executable = Path.Combine(this.RootFolder.FullName, node.InnerText);
                node = document.SelectSingleNode(@"/Manifest/Image");
                if (node != null)
                {
                    if (File.Exists(Path.Combine(this.RootFolder.FullName, node.InnerText)))
                        this.Image = new Bitmap(Path.Combine(this.RootFolder.FullName, node.InnerText));
                }
                node = document.SelectSingleNode(@"/Manifest/Icon");
                if (node != null)
                    if (File.Exists(Path.Combine(this.RootFolder.FullName, node.InnerText)))
                        this.Icon = Path.Combine(this.RootFolder.FullName, node.InnerText);
                node = document.SelectSingleNode(@"/Manifest/SlideTemplatesPath");
                if (node != null)
                {
                    this.UseSlideTemplates = true;
                    this.SlideTemplatesPath = node.InnerText;
                }
                node = document.SelectSingleNode(@"/Manifest/Order");
                if (node != null)
                {
                    int tempInt = 9999;
                    int.TryParse(node.InnerText, out tempInt);
                    this.Order = tempInt;
                }
            }
        }
    }
}
