using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace MiniBarLoader.ConfigurationClasses
{
    class SettingsManager
    {
        private static SettingsManager _instance = new SettingsManager();
        private string _minibarSettingsFile = string.Empty;
        private string _minibarAppSettingsFile = string.Empty;
        public bool OwnControl { get; set; }
        public bool AutoRunNormal { get; set; }
        public bool AutoRunHidden { get; set; }
        public bool AutoRunFloat { get; set; }
        public string MinibarPath { get; set; }

        private SettingsManager()
        {
            _minibarSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\MinibarSettings.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.MinibarPath = string.Format(@"{0}\newlocaldirect.com\app\Minibar\MiniBar.exe", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
        }

        public static SettingsManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public void LoadMinibarSettings()
        {
            this.OwnControl = false;
            this.AutoRunNormal = true;
            this.AutoRunHidden = false;
            this.AutoRunFloat = false;

            XmlNode node;
            bool tempBool;
            if (File.Exists(_minibarSettingsFile))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(_minibarSettingsFile);
                }
                catch
                { 
                }

                node = document.SelectSingleNode(@"/MinibarSettings/OwnControl");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.OwnControl = tempBool;
            }

            if (this.OwnControl)
                _minibarAppSettingsFile = string.Format(@"{0}\newlocaldirect.com\app\Minibar\MinibarAppSettings.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            else
                _minibarAppSettingsFile = string.Format(@"{0}\newlocaldirect.com\xml\app\MinibarAppSettings.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            if (File.Exists(_minibarAppSettingsFile))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(_minibarAppSettingsFile);
                }
                catch
                { 
                }

                node = document.SelectSingleNode(@"/MinibarSettings/AutoRunNormal");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.AutoRunNormal = tempBool;
                node = document.SelectSingleNode(@"/MinibarSettings/AutoRunHidden");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.AutoRunHidden = tempBool;
                node = document.SelectSingleNode(@"/MinibarSettings/AutoRunFloat");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.AutoRunFloat = tempBool;
            }
        }
    }
}
