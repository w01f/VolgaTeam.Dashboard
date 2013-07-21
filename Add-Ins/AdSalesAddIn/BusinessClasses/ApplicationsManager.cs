using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Xml;

namespace AdSalesAddIn.BusinessClasses
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
        public string OfficeRibbonLabel { get; set; }
        public string OfficeRibbonDescription { get; set; }
        public string Executable { get; set; }
        public Image Image { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
        public bool UseSlideTemplates { get; set; }
        public string SlideTemplatesPath { get; set; }

        public Microsoft.Office.Tools.Ribbon.RibbonButton AppButton { get; private set; }

        public NBWApplication(DirectoryInfo rootFolder)
        {
            this.Title = string.Empty;
            this.OfficeRibbonLabel = string.Empty;
            this.OfficeRibbonDescription = string.Empty;
            this.Executable = string.Empty;
            this.Order = 9999;
            this.RootFolder = rootFolder;
            this.SlideTemplatesPath = string.Empty;
            if (this.RootFolder.Exists)
            {
                LoadManifest();
                this.IsConfigured = !string.IsNullOrEmpty(this.Title) && !string.IsNullOrEmpty(this.Executable) && this.Image != null && this.Order != 999;
            }

            this.AppButton = Globals.Factory.GetRibbonFactory().CreateRibbonButton();
            this.AppButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.AppButton.Tag = this.Executable;
            this.AppButton.Label = this.OfficeRibbonLabel;
            this.AppButton.Description = this.OfficeRibbonDescription;
            this.AppButton.ScreenTip = this.OfficeRibbonLabel;
            this.AppButton.SuperTip = this.OfficeRibbonDescription; 
            this.AppButton.ShowImage = true;
            this.AppButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AppButton_Click);

            bool isHighDPI = BusinessClasses.CommonMethods.IsHighDPI();
            string logoPath = Path.Combine(this.RootFolder.FullName, isHighDPI ? ConfigurationClasses.SettingsManager.HighDPIRibbonImageFileName : ConfigurationClasses.SettingsManager.RegularDPIRibbonImageFileName);
            if (File.Exists(logoPath))
                this.AppButton.Image = new Bitmap(logoPath);
        }

        private void AppButton_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
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
                node = document.SelectSingleNode(@"/Manifest/OfficeRibbonLabel");
                if (node != null)
                    this.OfficeRibbonLabel = node.InnerText;
                node = document.SelectSingleNode(@"/Manifest/OfficeRibbonDescription");
                if (node != null)
                    this.OfficeRibbonDescription = node.InnerText;
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
