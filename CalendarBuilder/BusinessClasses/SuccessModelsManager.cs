using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace CalendarBuilder.BusinessClasses
{
    public class SuccessModelsManager
    {
        private static SuccessModelsManager _instance = new SuccessModelsManager();

        public List<SuccessModel> SuccessModels { get; set; }

        public static SuccessModelsManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private SuccessModelsManager()
        {
            this.SuccessModels = new List<SuccessModel>();
        }

        public void Load()
        {
            this.SuccessModels.Clear();
            if (Directory.Exists(ConfigurationClasses.SettingsManager.Instance.SuccessModelsPath))
            {
                List<string> fileNames = new List<string>();
                fileNames.AddRange(Directory.GetFiles(ConfigurationClasses.SettingsManager.Instance.SuccessModelsPath, "*.xml"));
                fileNames.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x, y));
                int i = 0;
                foreach (string fileName in fileNames)
                {
                    SuccessModel successModel = new SuccessModel(fileName);
                    successModel.Index = i;
                    if (!string.IsNullOrEmpty(successModel.Name))
                    {
                        this.SuccessModels.Add(successModel);
                        i++;
                    }
                }
            }
        }
    }

    public class SuccessModel
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }

        public SuccessModel(string fileName)
        {
            this.Name = string.Empty;
            this.Link = string.Empty;
            this.Description = string.Empty;
            Load(fileName);
        }

        private void Load(string fileName)
        {
            XmlNode node = null;
            if (File.Exists(fileName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(fileName);
                node = document.SelectSingleNode(@"/Link/LinkText");
                if (node != null)
                    this.Name = node.InnerText.Trim();
                node = document.SelectSingleNode(@"/Link/LinkURL");
                if (node != null)
                    this.Link = node.InnerText.Trim();
                node = document.SelectSingleNode(@"/Link/Overview");
                if (node != null)
                    this.Description = node.InnerText.Trim();
            }
        }
    }
}
