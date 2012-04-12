using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

namespace Reset.BusinessClasses
{
    class HelpManager
    {
        private static HelpManager _instance = new HelpManager();
        private Dictionary<string, string> _helpLinks = new Dictionary<string, string>();

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
            if (File.Exists(AppManager.Instance.HelpLinksPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(AppManager.Instance.HelpLinksPath);
                node = document.SelectSingleNode(@"/Help");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (!_helpLinks.Keys.Contains(childNode.Name))
                            _helpLinks.Add(childNode.Name, childNode.InnerText);

                    }
                }
            }
        }

        public void OpenHelpLink(string helpKey)
        {
            if (_helpLinks.Keys.Contains(helpKey))
            {
                try
                {
                    Process.Start(_helpLinks[helpKey]);
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
