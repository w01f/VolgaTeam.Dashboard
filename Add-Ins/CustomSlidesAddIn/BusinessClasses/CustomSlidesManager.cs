using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;

namespace CustomSlidesAddIn.BusinessClasses
{
    public class CustomSlidesManager
    {
        private static CustomSlidesManager _instance = new CustomSlidesManager();

        public List<CustomSlide> CustomSlides43 { get; set; }
        public List<CustomSlide> CustomSlides54 { get; set; }
        public List<CustomSlide> CustomSlides169 { get; set; }
        public List<CustomSlide> CustomSlides34 { get; set; }
        public List<CustomSlide> CustomSlides45 { get; set; }

        public static CustomSlidesManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private CustomSlidesManager()
        {
            this.CustomSlides43 = new List<CustomSlide>();
            this.CustomSlides54 = new List<CustomSlide>();
            this.CustomSlides169 = new List<CustomSlide>();
            this.CustomSlides34 = new List<CustomSlide>();
            this.CustomSlides45 = new List<CustomSlide>();

            DirectoryInfo sizeFolder = new DirectoryInfo(Path.Combine(ConfigurationClasses.SettingsManager.Instance.CustomSlidesRootPath, ConfigurationClasses.SettingsManager.Slide43Folder));
            if (sizeFolder.Exists)
            {
                List<DirectoryInfo> customSlideFolders = new List<DirectoryInfo>();
                customSlideFolders.AddRange(sizeFolder.GetDirectories());
                customSlideFolders.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
                foreach (DirectoryInfo customSlideFolder in customSlideFolders)
                {
                    CustomSlide customSlide = new CustomSlide(customSlideFolder);
                    if (customSlide.IsConfigured)
                        this.CustomSlides43.Add(customSlide);
                }
            }

            sizeFolder = new DirectoryInfo(Path.Combine(ConfigurationClasses.SettingsManager.Instance.CustomSlidesRootPath, ConfigurationClasses.SettingsManager.Slide54Folder));
            if (sizeFolder.Exists)
            {
                List<DirectoryInfo> customSlideFolders = new List<DirectoryInfo>();
                customSlideFolders.AddRange(sizeFolder.GetDirectories());
                customSlideFolders.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
                foreach (DirectoryInfo customSlideFolder in customSlideFolders)
                {
                    CustomSlide customSlide = new CustomSlide(customSlideFolder);
                    if (customSlide.IsConfigured)
                        this.CustomSlides54.Add(customSlide);
                }
            }

            sizeFolder = new DirectoryInfo(Path.Combine(ConfigurationClasses.SettingsManager.Instance.CustomSlidesRootPath, ConfigurationClasses.SettingsManager.Slide169Folder));
            if (sizeFolder.Exists)
            {
                List<DirectoryInfo> customSlideFolders = new List<DirectoryInfo>();
                customSlideFolders.AddRange(sizeFolder.GetDirectories());
                customSlideFolders.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
                foreach (DirectoryInfo customSlideFolder in customSlideFolders)
                {
                    CustomSlide customSlide = new CustomSlide(customSlideFolder);
                    if (customSlide.IsConfigured)
                        this.CustomSlides169.Add(customSlide);
                }
            }

            sizeFolder = new DirectoryInfo(Path.Combine(ConfigurationClasses.SettingsManager.Instance.CustomSlidesRootPath, ConfigurationClasses.SettingsManager.Slide34Folder));
            if (sizeFolder.Exists)
            {
                List<DirectoryInfo> customSlideFolders = new List<DirectoryInfo>();
                customSlideFolders.AddRange(sizeFolder.GetDirectories());
                customSlideFolders.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
                foreach (DirectoryInfo customSlideFolder in customSlideFolders)
                {
                    CustomSlide customSlide = new CustomSlide(customSlideFolder);
                    if (customSlide.IsConfigured)
                        this.CustomSlides34.Add(customSlide);
                }
            }

            sizeFolder = new DirectoryInfo(Path.Combine(ConfigurationClasses.SettingsManager.Instance.CustomSlidesRootPath, ConfigurationClasses.SettingsManager.Slide45Folder));
            if (sizeFolder.Exists)
            {
                List<DirectoryInfo> customSlideFolders = new List<DirectoryInfo>();
                customSlideFolders.AddRange(sizeFolder.GetDirectories());
                customSlideFolders.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Name, y.Name));
                foreach (DirectoryInfo customSlideFolder in customSlideFolders)
                {
                    CustomSlide customSlide = new CustomSlide(customSlideFolder);
                    if (customSlide.IsConfigured)
                        this.CustomSlides45.Add(customSlide);
                }
            }
        }
    }

    public class CustomSlide
    {
        public bool IsConfigured { get; set; }
        public DirectoryInfo RootFolder { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SlideSource { get; set; }

        public Microsoft.Office.Tools.Ribbon.RibbonDropDownItem CustomSlideButton { get; private set; }

        public CustomSlide(DirectoryInfo rootFolder)
        {
            this.Title = string.Empty;
            this.Description = string.Empty;
            this.SlideSource = string.Empty;
            this.RootFolder = rootFolder;
            this.SlideSource = Path.Combine(this.RootFolder.FullName, ConfigurationClasses.SettingsManager.CustomSlideSourceFileName);
            if (this.RootFolder.Exists)
            {
                LoadSlideSettings();
                this.IsConfigured = !string.IsNullOrEmpty(this.Title) && !string.IsNullOrEmpty(this.SlideSource);
            }

            this.CustomSlideButton = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
            this.CustomSlideButton.Tag = this.SlideSource;
            this.CustomSlideButton.Label = this.Title;
            this.CustomSlideButton.ScreenTip = this.Title;
            this.CustomSlideButton.SuperTip = this.Description;

            string logoPath = Path.Combine(this.RootFolder.FullName, ConfigurationClasses.SettingsManager.Instance.HighDPI ? ConfigurationClasses.SettingsManager.HighDPIRibbonImageFileName : ConfigurationClasses.SettingsManager.RegularDPIRibbonImageFileName);
            if (File.Exists(logoPath))
                this.CustomSlideButton.Image = new Bitmap(logoPath);
        }

        public void LoadSlideSettings()
        {
            XmlNode node;
            if (File.Exists(Path.Combine(this.RootFolder.FullName, ConfigurationClasses.SettingsManager.CustomSlideConfigFileName)))
            {
                XmlDocument document = new XmlDocument();
                document.Load(Path.Combine(this.RootFolder.FullName, ConfigurationClasses.SettingsManager.CustomSlideConfigFileName));
                node = document.SelectSingleNode(@"/menutext/title");
                if (node != null)
                    this.Title = node.InnerText;
                node = document.SelectSingleNode(@"/menutext/description");
                if (node != null)
                    this.Description = node.InnerText;
            }
        }
    }
}
