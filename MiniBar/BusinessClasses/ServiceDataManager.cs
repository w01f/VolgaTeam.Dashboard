using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace MiniBar.BusinessClasses
{
    public class ServiceDataManager
    {
        private static ServiceDataManager _instance = null;
        private string _currentUser = Environment.UserName;

        public List<UserActivity> UserActivities { get; private set; }


        public static ServiceDataManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ServiceDataManager();
                return _instance;
            }
        }

        private ServiceDataManager()
        {
            this.UserActivities = new List<UserActivity>();
        }

        public void LoadData()
        {
            this.UserActivities.Clear();
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.ServiceDataFilePath))
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(ConfigurationClasses.SettingsManager.Instance.ServiceDataFilePath);
                }
                catch
                {
                }

                XmlNode node = document.SelectSingleNode(@"/ServiceData/UserActivities");
                if (node != null)
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        UserActivity userActivity = new UserActivity();
                        userActivity.Deserialize(childNode);
                        if (!string.IsNullOrEmpty(userActivity.UserName))
                            this.UserActivities.Add(userActivity);
                    }
            }
        }

        public void SaveData()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<ServiceData>");
            xml.AppendLine(@"<UserActivities>");
            foreach (UserActivity userActivity in this.UserActivities)
                if (!string.IsNullOrEmpty(userActivity.UserName))
                    xml.AppendLine(userActivity.Serialize());
            xml.AppendLine(@"</UserActivities>");
            xml.AppendLine(@"</ServiceData>");

            using (StreamWriter sw = new StreamWriter(ConfigurationClasses.SettingsManager.Instance.ServiceDataFilePath, false))
            {
                sw.Write(xml);
                sw.Flush();
                sw.Close();
            }
        }

        public void WriteActivity()
        {
            UserActivity userActivity = this.UserActivities.Where(x => x.UserName.Equals(_currentUser)).FirstOrDefault();
            if (userActivity == null)
            {
                userActivity = new UserActivity();
                userActivity.UserName = _currentUser;
                userActivity.FirstLaunch = DateTime.Now;
                userActivity.LastLaunch = userActivity.FirstLaunch;
                this.UserActivities.Add(userActivity);
            }
            else
                userActivity.LastLaunch = DateTime.Now;
            SaveData();
        }
    }

    public class UserActivity
    {
        public string UserName { get; set; }
        public DateTime FirstLaunch { get; set; }
        public DateTime LastLaunch { get; set; }

        public UserActivity()
        {
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            if (!string.IsNullOrEmpty(this.UserName))
            {
                result.Append(@"<UserActivity ");
                result.Append("UserName = \"" + this.UserName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                result.Append("FirstLaunch = \"" + this.FirstLaunch.ToString() + "\" ");
                result.Append("LastLaunch = \"" + this.LastLaunch.ToString() + "\" ");
                result.AppendLine(@"/>");
            }
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            DateTime tempDateTime;

            foreach (XmlAttribute attribute in node.Attributes)
            {
                switch (attribute.Name)
                {
                    case "UserName":
                        this.UserName = attribute.InnerText;
                        break;
                    case "FirstLaunch":
                        if (DateTime.TryParse(attribute.Value, out tempDateTime))
                            this.FirstLaunch = tempDateTime;
                        break;
                    case "LastLaunch":
                        if (DateTime.TryParse(attribute.Value, out tempDateTime))
                            this.LastLaunch = tempDateTime;
                        break;
                }
            }
        }
    }
}
