﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MiniBar.BusinessClasses
{
    class MasterWizardManager
    {
        public const string CleanslateFileName = @"{0}\Basic Slides\CleanSlate.ppt";
        public const string GenericCoverFileName = @"{0}\Basic Slides\WizCover2.ppt";
        public const string ContentsFolderPath = @"{0}\Contents Slides";
        public const string ContentsFileName = @"Contents{0}.ppt";
        public const string PageNumbersFileName = @"!pagenumber.ppt";

        public static string MasterWizardsFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Dashboard", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

        private static MasterWizardManager _instance = new MasterWizardManager();
        private MasterWizard _selectedWizard = null;
        public Dictionary<string, MasterWizard> MasterWizards { get; set; }


        private MasterWizardManager()
        {
            this.MasterWizards = new Dictionary<string, MasterWizard>();
            Load();
        }

        public static MasterWizardManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public MasterWizard SelectedWizard
        {
            get
            {
                return _selectedWizard;
            }
            set
            {
                _selectedWizard = value;
            }
        }

        private void Load()
        {
            DirectoryInfo rootFolder = new DirectoryInfo(MasterWizardsFolder);
            if (rootFolder.Exists)
            {
                foreach (DirectoryInfo folder in rootFolder.GetDirectories())
                {
                    MasterWizard masterWizard = new MasterWizard();
                    masterWizard.Name = folder.Name;
                    masterWizard.Folder = folder;
                    masterWizard.Init();
                    if (!masterWizard.Hide)
                        this.MasterWizards.Add(masterWizard.Name, masterWizard);
                }
            }
        }
    }

    public class MasterWizard
    {
        public string Name { get; set; }
        public DirectoryInfo Folder { get; set; }
        public FileInfo LogoFile { get; set; }
        public bool Hide { get; set; }
        public bool Has43 { get; set; }
        public bool Has54 { get; set; }
        public bool Has34 { get; set; }
        public bool Has45 { get; set; }
        public bool Has169 { get; set; }
        public bool HasSlideTemplate43 { get; set; }
        public bool HasSlideTemplate54 { get; set; }
        public bool HasSlideTemplate34 { get; set; }
        public bool HasSlideTemplate45 { get; set; }
        public bool HasSlideTemplate169 { get; set; }

        public string CleanslateFile
        {
            get
            {
                return Path.Combine(this.Folder.FullName, string.Format(MasterWizardManager.CleanslateFileName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string GenericCoverFile
        {
            get
            {
                return Path.Combine(this.Folder.FullName, string.Format(MasterWizardManager.GenericCoverFileName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string PageNumbersFile
        {
            get
            {
                return Path.Combine(this.Folder.FullName, string.Format(MasterWizardManager.ContentsFolderPath, ConfigurationClasses.SettingsManager.Instance.SlideFolder), MasterWizardManager.PageNumbersFileName);
            }
        }

        public string ContentsFolder
        {
            get
            {
                return Path.Combine(this.Folder.FullName, string.Format(MasterWizardManager.ContentsFolderPath, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public MasterWizard()
        {
        }

        public void Init()
        {
            LoadSettings();

            this.Has43 = Directory.Exists(Path.Combine(this.Folder.FullName, "Slides43"));
            this.Has54 = Directory.Exists(Path.Combine(this.Folder.FullName, "Slides54"));
            this.Has34 = Directory.Exists(Path.Combine(this.Folder.FullName, "Slides34"));
            this.Has45 = Directory.Exists(Path.Combine(this.Folder.FullName, "Slides45"));
            this.Has169 = Directory.Exists(Path.Combine(this.Folder.FullName, "Slides169"));
            //this.HasSlideTemplate43 = this.Has43 ? (Directory.GetFiles(Path.Combine(this.Folder.FullName, "Slides43"), @"*.pot*").Length == 1) : false;
            //this.HasSlideTemplate54 = this.Has54 ? (Directory.GetFiles(Path.Combine(this.Folder.FullName, "Slides54"), @"*.pot*").Length == 1) : false;
            //this.HasSlideTemplate34 = this.Has34 ? (Directory.GetFiles(Path.Combine(this.Folder.FullName, "Slides34"), @"*.pot*").Length == 1) : false;
            //this.HasSlideTemplate45 = this.Has45 ? (Directory.GetFiles(Path.Combine(this.Folder.FullName, "Slides45"), @"*.pot*").Length == 1) : false;
            //this.HasSlideTemplate169 = this.Has169 ? (Directory.GetFiles(Path.Combine(this.Folder.FullName, "Slides169"), @"*.pot*").Length == 1) : false;
            this.HasSlideTemplate43 = this.Has43;
            this.HasSlideTemplate54 = this.Has54;
            this.HasSlideTemplate34 = this.Has34;
            this.HasSlideTemplate45 = this.Has45;
            this.HasSlideTemplate169 = this.Has169;
            FileInfo file = new FileInfo(Path.Combine(this.Folder.FullName, this.Folder.Name + ".png"));
            this.LogoFile = file.Exists ? file : null;
        }

        private void LoadSettings()
        {
            XmlNode node;
            bool tempBool = false;

            this.Hide = false;
            string settingsFile = Path.Combine(this.Folder.FullName, "Settings.xml");
            if (File.Exists(settingsFile))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(settingsFile);
                    node = document.SelectSingleNode(@"/settings/hide");
                    if (node != null)
                        if (bool.TryParse(node.InnerText, out tempBool))
                            this.Hide = tempBool;
                }
                catch
                {
                }
            }
        }
    }
}
