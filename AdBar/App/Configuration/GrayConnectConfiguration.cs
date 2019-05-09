using System.IO;
using System.Xml;

namespace Asa.Bar.App.Configuration
{
    public class GrayConnectConfiguration
    {
        public bool UseGrayConnect { get; private set; }

        public string FormTitle { get; private set; }

        public string Step1Title { get; private set; }
        public string Step1Url { get; private set; }
        public string Step1UrlDescription { get; private set; }

        public string Step2Title { get; private set; }
        public string Step2Url { get; private set; }
        public string Step2UrlDescription { get; private set; }

        public string Step3Title { get; private set; }
        public string Step3Url { get; private set; }
        public string Step3UrlDescription { get; private set; }

        public string Step4Title { get; private set; }
        public string Step4Url { get; private set; }
        public string Step4UrlDescription { get; private set; }

        public string MainSiteUrl { get; private set; }
        public string GrayConnectUrl { get; private set; }

        public void Load()
        {
            var settingsFilePath = Path.Combine(ResourceManager.Instance.AppRootFolderPath, "gray_connect.xml");

            if (!File.Exists(settingsFilePath)) return;

            UseGrayConnect = true;

            var document = new XmlDocument();
            document.Load(settingsFilePath);

            FormTitle = document.SelectSingleNode(@"//Config/TopLabel")?.InnerText;

            Step1Title = document.SelectSingleNode(@"//Config/Step1Label/Text")?.InnerText;
            Step1Url = document.SelectSingleNode(@"//Config/Step1Label/URL")?.InnerText;
            Step1UrlDescription = document.SelectSingleNode(@"//Config/Step1Label/HyperLinkText")?.InnerText;

            Step2Title = document.SelectSingleNode(@"//Config/Step2Label/Text")?.InnerText;
            Step2Url = document.SelectSingleNode(@"//Config/Step2Label/URL")?.InnerText;
            Step2UrlDescription = document.SelectSingleNode(@"//Config/Step2Label/HyperLinkText")?.InnerText;

            Step3Title = document.SelectSingleNode(@"//Config/Step3Label/Text")?.InnerText;
            Step3Url = document.SelectSingleNode(@"//Config/Step3Label/URL")?.InnerText;
            Step3UrlDescription = document.SelectSingleNode(@"//Config/Step3Label/HyperLinkText")?.InnerText;

            Step4Title = document.SelectSingleNode(@"//Config/Step4Label/Text")?.InnerText;
            Step4Url = document.SelectSingleNode(@"//Config/Step4Label/URL")?.InnerText;
            Step4UrlDescription = document.SelectSingleNode(@"//Config/Step4Label/HyperLinkText")?.InnerText;

            MainSiteUrl = document.SelectSingleNode(@"//Config/BottomLeftURLs/LeftURL")?.InnerText;
            GrayConnectUrl = document.SelectSingleNode(@"//Config/BottomLeftURLs/RightURL")?.InnerText;
        }
    }
}
