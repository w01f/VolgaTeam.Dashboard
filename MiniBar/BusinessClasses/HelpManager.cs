using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

namespace MiniBar.BusinessClasses
{
    class HelpManager
    {
        private static HelpManager _instance = new HelpManager();
        private Dictionary<int, string> _helpLinks = new Dictionary<int, string>();

        private HelpManager()
        {
            LoadHelpLinks();
        }

        public static HelpManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private void LoadHelpLinks()
        {
            _helpLinks.Clear();
            XmlNode node;
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.HelpLinksPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(ConfigurationClasses.SettingsManager.Instance.HelpLinksPath);
                node = document.SelectSingleNode(@"/Help");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        int temp = 0;
                        if (int.TryParse(childNode.Name.ToLower().Replace("t", ""), out temp))
                            if (!_helpLinks.Keys.Contains(temp))
                                _helpLinks.Add(temp, childNode.InnerText);

                    }
                }
            }
        }

        public void OpenHelpLink(int tabPageNumber)
        {
            if (_helpLinks.Keys.Contains(tabPageNumber))
            {
                try
                {
                    Process.Start(_helpLinks[tabPageNumber]);
                }
                catch
                {
                    AppManager.Instance.ShowWarning("Couldn't open Help link for this page");
                }
            }
            else
                AppManager.Instance.ShowWarning("Help link for this page was not found");
        }
    }
}
