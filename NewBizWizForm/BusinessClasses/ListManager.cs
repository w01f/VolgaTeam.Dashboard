using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace NewBizWizForm.BusinessClasses
{
    class ListManager
    {
        private static ListManager _instance = new ListManager();

        public string ListsFolder { get; set; }

        public string LocalListsFolder { get; set; }

        public Advertisers Advertisers { get; set; }

        public DecisionMakers DecisionMakers { get; set; }

        public Users UsersList { get; set; }

        public CoverLists CoverLists { get; set; }

        #region Home
        public ClientGoalsLists ClientGoalsLists { get; set; }
        public LeadoffStatementLists LeadoffStatementLists { get; set; }
        public TargetCustomersLists TargetCustomersLists { get; set; }
        public SimpleSummaryLists SimpleSummaryLists { get; set; }
        #endregion

        private ListManager()
        {
        }

        public void Init()
        {
            this.ListsFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

            this.LocalListsFolder = Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles)), "AppID-" + ConfigurationClasses.SettingsManager.Instance.AppID.ToString(), @"User_lists");
            if (!Directory.Exists(this.LocalListsFolder))
                Directory.CreateDirectory(this.LocalListsFolder);

            this.Advertisers = new Advertisers();

            this.DecisionMakers = new DecisionMakers();

            this.UsersList = new Users();

            this.CoverLists = new CoverLists();

            #region Home
            this.ClientGoalsLists = new ClientGoalsLists();
            this.LeadoffStatementLists = new LeadoffStatementLists();
            this.TargetCustomersLists = new TargetCustomersLists();
            this.SimpleSummaryLists = new SimpleSummaryLists();
            #endregion
        }

        public static ListManager Instance
        {
            get
            {
                return _instance;
            }
        }
    }

    #region Advertisers
    class Advertisers
    {
        private string _listsFileName;

        public List<string> Titles { get; set; }

        public Advertisers()
        {
            _listsFileName = Path.Combine(ListManager.Instance.LocalListsFolder, "Advertisers.xml");
            this.Titles = new List<string>();
            Load();
        }

        private void Load()
        {
            XmlNode node;
            if (File.Exists(_listsFileName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_listsFileName);

                node = document.SelectSingleNode(@"/Advertisers");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (!string.IsNullOrEmpty(childNode.InnerText))
                            this.Titles.Add(childNode.InnerText);
                    }
                }
            }
        }

        public void Save()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<Advertisers>");
            foreach (string advertiser in this.Titles)
                xml.AppendLine(@"<Advertiser>" + advertiser.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Advertiser>");
            xml.AppendLine(@"</Advertisers>");

            string userConfigurationPath = Path.Combine(Application.StartupPath, _listsFileName);
            using (StreamWriter sw = new StreamWriter(userConfigurationPath, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }
    }
    #endregion

    #region DecisionMakers
    class DecisionMakers
    {
        private string _listsFileName;

        public List<string> Titles { get; set; }

        public DecisionMakers()
        {
            _listsFileName = Path.Combine(ListManager.Instance.LocalListsFolder, "DecisionMakers.xml");
            this.Titles = new List<string>();
            Load();
        }

        private void Load()
        {
            XmlNode node;
            if (File.Exists(_listsFileName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_listsFileName);

                node = document.SelectSingleNode(@"/DecisionMakers");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (!string.IsNullOrEmpty(childNode.InnerText))
                            this.Titles.Add(childNode.InnerText);
                    }
                }
            }
        }

        public void Save()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<DecisionMakers>");
            foreach (string decisionMaker in this.Titles)
                xml.AppendLine(@"<DecisionMaker>" + decisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
            xml.AppendLine(@"</DecisionMakers>");

            string userConfigurationPath = Path.Combine(Application.StartupPath, _listsFileName);
            using (StreamWriter sw = new StreamWriter(userConfigurationPath, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }
    }
    #endregion

    #region Users
    class Users
    {
        private List<User> _users = new List<User>();
        private string _listsFileName;

        public Users()
        {
            _listsFileName = Path.Combine(ListManager.Instance.ListsFolder, "Users XML", "Users.xml");
            Load();
        }

        private void Load()
        {
            bool tempBool;

            XmlNode node;
            if (File.Exists(_listsFileName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_listsFileName);

                node = document.SelectSingleNode(@"/Users");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.Name.Equals("User"))
                        {
                            User user = new User();
                            foreach (XmlAttribute attribute in childNode.Attributes)
                            {
                                switch (attribute.Name)
                                {
                                    case "Station":
                                        user.Station = attribute.Value;
                                        break;
                                    case "FirstName":
                                        user.FirstName = attribute.Value;
                                        break;
                                    case "LastName":
                                        user.LastName = attribute.Value;
                                        break;
                                    case "Phone":
                                        user.Phone = attribute.Value;
                                        break;
                                    case "Email":
                                        user.Email = attribute.Value;
                                        break;
                                    case "IsAdmin":
                                        tempBool = false;
                                        bool.TryParse(attribute.Value, out tempBool);
                                        user.IsAdmin = tempBool;
                                        break;
                                }
                            }
                            _users.Add(user);
                        }
                    }
                }
            }
        }

        public User[] GetUsersByStation(string stationName)
        {
            return _users.ToArray();
        }
    }

    class User
    {
        public string Station { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        public User()
        {
            this.Station = string.Empty;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;
        }
    }
    #endregion

    #region Cover
    class CoverLists
    {
        private string _listsFileName;

        public List<string> Headers { get; set; }
        public List<Quote> Quotes { get; set; }

        public CoverLists()
        {
            _listsFileName = Path.Combine(ListManager.Instance.ListsFolder, "Basic Slides XML", "Add Cover.xml");
            this.Headers = new List<string>();
            this.Quotes = new List<Quote>();
            Load();
        }

        private void Load()
        {
            XmlNode node;
            if (File.Exists(_listsFileName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_listsFileName);

                node = document.SelectSingleNode(@"/CoverSlide");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        switch (childNode.Name)
                        {
                            case "SlideHeader":
                                foreach (XmlAttribute attribute in childNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!string.IsNullOrEmpty(attribute.Value))
                                                this.Headers.Add(attribute.Value);
                                            break;
                                    }
                                }
                                break;
                            case "Quote":
                                Quote quote = new Quote();
                                foreach (XmlAttribute attribute in childNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            quote.Text = attribute.Value;
                                            break;
                                        case "Author":
                                            quote.Author = attribute.Value;
                                            break;
                                    }
                                }
                                this.Quotes.Add(quote);
                                break;
                        }
                    }
                }
            }
        }
    }

    public class Quote
    {
        public string Text { get; set; }
        public string Author { get; set; }

        public Quote()
        {
            this.Text = string.Empty;
            this.Author = string.Empty;
        }

        public bool IsSet
        {
            get
            {
                return !string.IsNullOrEmpty(this.Text + this.Author);
            }
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(@"<Text>" + this.Text.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Text>");
            result.AppendLine(@"<Author>" + this.Author.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Author>");

            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Text":
                        this.Text = childNode.InnerText;
                        break;
                    case "Author":
                        this.Author = childNode.InnerText;
                        break;
                }
            }
        }
    }
    #endregion

    #region Home
    #region Client Goals
    class ClientGoalsLists
    {
        private string _listsFileName;

        public List<string> Headers { get; set; }
        public List<string> Goals { get; set; }

        public ClientGoalsLists()
        {
            _listsFileName = Path.Combine(ListManager.Instance.ListsFolder, "Basic Slides XML", "Needs Analysis.xml");
            this.Headers = new List<string>();
            this.Goals = new List<string>();
            Load();
        }

        private void Load()
        {
            XmlNode node;
            if (File.Exists(_listsFileName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_listsFileName);

                node = document.SelectSingleNode(@"/ClientGoals");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        switch (childNode.Name)
                        {
                            case "SlideHeader":
                                foreach (XmlAttribute attribute in childNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!string.IsNullOrEmpty(attribute.Value))
                                                this.Headers.Add(attribute.Value);
                                            break;
                                    }
                                }
                                break;
                            case "Goal":
                                foreach (XmlAttribute attribute in childNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!string.IsNullOrEmpty(attribute.Value))
                                                this.Goals.Add(attribute.Value);
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region Leadoff Statement
    class LeadoffStatementLists
    {
        private string _listsFileName;

        public List<string> Headers { get; set; }
        public List<string> Statements { get; set; }

        public LeadoffStatementLists()
        {
            _listsFileName = Path.Combine(ListManager.Instance.ListsFolder, "Basic Slides XML", "Intro Slide.xml");
            this.Headers = new List<string>();
            this.Statements = new List<string>();
            Load();
        }

        private void Load()
        {
            XmlNode node;
            if (File.Exists(_listsFileName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_listsFileName);

                node = document.SelectSingleNode(@"/LeadOff");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        switch (childNode.Name)
                        {
                            case "SlideHeader":
                                foreach (XmlAttribute attribute in childNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!string.IsNullOrEmpty(attribute.Value))
                                                this.Headers.Add(attribute.Value);
                                            break;
                                    }
                                }
                                break;
                            case "Statement":
                                foreach (XmlAttribute attribute in childNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!string.IsNullOrEmpty(attribute.Value))
                                                this.Statements.Add(attribute.Value);
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region Target Customers
    class TargetCustomersLists
    {
        private string _listsFileName;

        public List<string> Headers { get; set; }
        public List<string> Demos { get; set; }
        public List<string> HHIs { get; set; }
        public List<string> Geographies { get; set; }

        public TargetCustomersLists()
        {
            _listsFileName = Path.Combine(ListManager.Instance.ListsFolder, "Basic Slides XML", "Target Customer.xml");
            this.Headers = new List<string>();
            this.Demos = new List<string>();
            this.HHIs = new List<string>();
            this.Geographies = new List<string>();
            Load();
        }

        private void Load()
        {
            XmlNode node;
            if (File.Exists(_listsFileName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_listsFileName);

                node = document.SelectSingleNode(@"/TargetCustomers");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        switch (childNode.Name)
                        {
                            case "SlideHeader":
                                foreach (XmlAttribute attribute in childNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!string.IsNullOrEmpty(attribute.Value))
                                                this.Headers.Add(attribute.Value);
                                            break;
                                    }
                                }
                                break;
                            case "Demo":
                                foreach (XmlAttribute attribute in childNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!string.IsNullOrEmpty(attribute.Value))
                                                this.Demos.Add(attribute.Value);
                                            break;
                                    }
                                }
                                break;
                            case "HHI":
                                foreach (XmlAttribute attribute in childNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!string.IsNullOrEmpty(attribute.Value))
                                                this.HHIs.Add(attribute.Value);
                                            break;
                                    }
                                }
                                break;
                            case "Geography":
                                foreach (XmlAttribute attribute in childNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!string.IsNullOrEmpty(attribute.Value))
                                                this.Geographies.Add(attribute.Value);
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region Simple Summary
    class SimpleSummaryLists
    {
        private string _listsFileName;

        public List<string> Headers { get; set; }
        public List<string> Details { get; set; }

        public SimpleSummaryLists()
        {
            _listsFileName = Path.Combine(ListManager.Instance.ListsFolder, "Basic Slides XML", "Closing Summary.xml");
            this.Headers = new List<string>();
            this.Details = new List<string>();
            Load();
        }

        private void Load()
        {
            XmlNode node;
            if (File.Exists(_listsFileName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_listsFileName);

                node = document.SelectSingleNode(@"/SimpleSummary");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        switch (childNode.Name)
                        {
                            case "SlideHeader":
                                foreach (XmlAttribute attribute in childNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!string.IsNullOrEmpty(attribute.Value))
                                                this.Headers.Add(attribute.Value);
                                            break;
                                    }
                                }
                                break;
                            case "Detail":
                                foreach (XmlAttribute attribute in childNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!string.IsNullOrEmpty(attribute.Value))
                                                this.Details.Add(attribute.Value);
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
    #endregion
    #endregion
}
