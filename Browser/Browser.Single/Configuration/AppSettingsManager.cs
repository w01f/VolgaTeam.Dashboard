using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using Asa.Browser.Controls.BusinessClasses.Enums;
using Asa.Browser.Controls.BusinessClasses.Objects;

namespace Asa.Browser.Single.Configuration
{
    class AppSettingsManager
    {
        public string AppFileName { get; }
        public string AppFolderPath { get; }
        public Icon FormIcon { get; private set; }
        public string FormText { get; private set; }

        public Color? AccentColor { get; set; }
        public Color? StatusBarTextColor { get; set; }

        public List<SingleSiteSettings> Sites { get; } = new List<SingleSiteSettings>();

        public static AppSettingsManager Instance { get; } = new AppSettingsManager();

        private AppSettingsManager()
        {
            AppFileName = Process.GetCurrentProcess().MainModule.FileName;
            AppFolderPath = Path.GetDirectoryName(AppFileName);
        }

        public void LoadSettings()
        {
            var iconPath = Path.Combine(AppFolderPath, "icon.ico");
            FormIcon = File.Exists(iconPath) ? new Icon(iconPath) : null;

            var settingsFilePath = Path.ChangeExtension(AppFileName, "xml");
            if (!File.Exists(settingsFilePath)) return;
            var document = new XmlDocument();
            document.Load(settingsFilePath);

            FormText = document.SelectSingleNode(@"//Root/Title")?.InnerText;

            var colorValue = document.SelectSingleNode(@"//Root/Style/AccentColor")?.InnerText;
            if (!String.IsNullOrEmpty(colorValue))
                AccentColor = ColorTranslator.FromHtml(colorValue);

            colorValue = document.SelectSingleNode(@"//Root/Style/StatusBarTextColor")?.InnerText;
            if (!String.IsNullOrEmpty(colorValue))
                StatusBarTextColor = ColorTranslator.FromHtml(colorValue);

            foreach (var browserNode in document.SelectNodes(@"//Root/Browser").OfType<XmlNode>())
            {
                var browserId = browserNode.SelectSingleNode(@"./Id")?.InnerText;
                var statusBarTitle = browserNode.SelectSingleNode(@"./Footer")?.InnerText ?? "Sales Cloud";

                foreach (var siteNode in browserNode.SelectNodes(@"./Site").OfType<XmlNode>())
                {
                    var siteSettings = new SingleSiteSettings();

                    siteSettings.BrowserId = browserId;
                    siteSettings.StatusBarTitle = statusBarTitle;

                    switch (siteNode.SelectSingleNode("./Type")?.InnerText.ToLower())
                    {
                        case "website":
                            siteSettings.SiteType = SiteType.SimpleSite;
                            break;
                        case "salescloud":
                            siteSettings.SiteType = SiteType.SalesCloud;
                            break;
                        default:
                            siteSettings.SiteType = SiteType.SalesCloud;
                            break;
                    }
                    siteSettings.BaseUrl = siteNode.SelectSingleNode("./Url")?.InnerText;
                    siteSettings.Title = siteNode.SelectSingleNode("./ComboName")?.InnerText ?? siteSettings.BaseUrl;
                    if (!String.IsNullOrEmpty(siteSettings.BaseUrl))
                        Sites.Add(siteSettings);
                }
            }
        }
    }
}
