using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace TVScheduleBuilder.BusinessClasses
{
    public class ListManager
    {
        public string ListsFolder { get; set; }

        private const string AdvertisersFileName = @"Advertisers.xml";
        private const string DecisionMakersFileName = @"DecisionMakers.xml";
        private const string TVStrategyFileName = @"TV XML\TV Strategy.xml";

        private static ListManager _instance = new ListManager();

        public List<string> Advertisers { get; set; }
        public List<string> DecisionMakers { get; set; }
        public List<string> SlideHeaders { get; set; }
        public List<string> ClientTypes { get; set; }
        public List<string> Lengths { get; set; }
        public List<string> Demos { get; set; }
        public List<string> CustomDemos { get; set; }
        public List<string> Sources { get; set; }
        public List<string> Times { get; set; }
        public List<string> Days { get; set; }
        public List<Daypart> Dayparts { get; set; }
        public List<Station> Stations { get; set; }
        public List<SourceProgram> SourcePrograms { get; set; }
        public List<string> Statuses { get; set; }

        private ListManager()
        {
            this.ListsFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.Advertisers = new List<string>();
            this.DecisionMakers = new List<string>();
            this.SlideHeaders = new List<string>();
            this.ClientTypes = new List<string>();
            this.Lengths = new List<string>();
            this.Stations = new List<Station>();
            this.Demos = new List<string>();
            this.CustomDemos = new List<string>();
            this.Sources = new List<string>();
            this.Dayparts = new List<Daypart>();
            this.Times = new List<string>();
            this.Days = new List<string>();
            this.SourcePrograms = new List<SourceProgram>();
            this.Statuses = new List<string>();
            LoadLists();
        }

        public static ListManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private void LoadAdvertisers()
        {
            this.Advertisers.Clear();
            string listPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.ListFolder, AdvertisersFileName);
            if (File.Exists(listPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(listPath);

                XmlNode node = document.SelectSingleNode(@"/Advertisers");
                if (node != null)
                {
                    foreach (XmlNode childeNode in node.ChildNodes)
                    {
                        if (!this.Advertisers.Contains(childeNode.InnerText))
                            this.Advertisers.Add(childeNode.InnerText);
                    }
                }
            }
        }

        private void LoadDecisionMakers()
        {
            this.DecisionMakers.Clear();
            string listPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.ListFolder, DecisionMakersFileName);
            if (File.Exists(listPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(listPath);

                XmlNode node = document.SelectSingleNode(@"/DecisionMakers");
                if (node != null)
                {
                    foreach (XmlNode childeNode in node.ChildNodes)
                    {
                        if (!this.DecisionMakers.Contains(childeNode.InnerText))
                            this.DecisionMakers.Add(childeNode.InnerText);
                    }
                }
            }
        }

        private void LoadTVStrategy()
        {
            SourceProgram sourceProgram = null;

            this.SlideHeaders.Clear();
            this.ClientTypes.Clear();
            this.SourcePrograms.Clear();
            this.Lengths.Clear();
            this.Stations.Clear();
            this.Demos.Clear();
            this.CustomDemos.Clear();
            this.Sources.Clear();
            this.Dayparts.Clear();
            this.Times.Clear();
            string listPath = Path.Combine(this.ListsFolder, TVStrategyFileName);
            if (File.Exists(listPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(listPath);

                XmlNode node = document.SelectSingleNode(@"/TVStrategy");
                if (node != null)
                {
                    foreach (XmlNode childeNode in node.ChildNodes)
                    {
                        switch (childeNode.Name)
                        {
                            case "SlideHeader":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!string.IsNullOrEmpty(attribute.Value) && !this.SlideHeaders.Contains(attribute.Value))
                                                this.SlideHeaders.Add(attribute.Value);
                                            break;
                                    }
                                }
                                break;
                            case "ClientType":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!this.ClientTypes.Contains(attribute.Value))
                                                this.ClientTypes.Add(attribute.Value);
                                            break;
                                    }
                                break;
                            case "Daypart":
                                Daypart daypart = new Daypart();
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Name":
                                            daypart.Name = attribute.Value;
                                            break;
                                        case "Code":
                                            daypart.Code = attribute.Value;
                                            break;
                                    }
                                }
                                if (!string.IsNullOrEmpty(daypart.Name))
                                    this.Dayparts.Add(daypart);
                                break;
                            case "CustomDemo":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!this.CustomDemos.Contains(attribute.Value))
                                                this.CustomDemos.Add(attribute.Value);
                                            break;
                                    }
                                break;
                            case "Source":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!this.Sources.Contains(attribute.Value))
                                                this.Sources.Add(attribute.Value);
                                            break;
                                    }
                                break;
                            case "Lenght":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!string.IsNullOrEmpty(attribute.Value) && !this.SlideHeaders.Contains(attribute.Value))
                                                this.Lengths.Add(attribute.Value);
                                            break;
                                    }
                                }
                                break;
                            case "Station":
                                Station station = new Station();
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                {
                                    switch (attribute.Name)
                                    {
                                        case "Name":
                                            station.Name = attribute.Value;
                                            break;
                                        case "Logo":
                                            if (!string.IsNullOrEmpty(attribute.Value))
                                                station.Logo = new Bitmap(new MemoryStream(Convert.FromBase64String(attribute.Value)));
                                            break;
                                    }
                                }
                                if (!string.IsNullOrEmpty(station.Name))
                                    this.Stations.Add(station);
                                break;
                            case "Program":
                                sourceProgram = new SourceProgram();
                                GetProgramProperties(childeNode, ref sourceProgram);
                                if (!string.IsNullOrEmpty(sourceProgram.Name))
                                    this.SourcePrograms.Add(sourceProgram);
                                break;
                            case "Status":
                                foreach (XmlAttribute attribute in childeNode.Attributes)
                                    switch (attribute.Name)
                                    {
                                        case "Value":
                                            if (!this.Statuses.Contains(attribute.Value))
                                                this.Statuses.Add(attribute.Value);
                                            break;
                                    }
                                break;
                        }
                    }
                }
            }
            if (this.SourcePrograms.Count > 0)
            {
                this.Times.AddRange(this.SourcePrograms.Select(x => x.Time).Distinct().ToArray());
                this.Days.AddRange(this.SourcePrograms.Select(x => x.Day).Distinct().ToArray());
            }
            foreach (SourceProgram program in this.SourcePrograms)
                this.Demos.AddRange(program.Demos.Where(x => !this.Demos.Contains(x.Name)).Select(x => x.Name).ToArray());
        }

        private void GetProgramProperties(XmlNode node, ref SourceProgram sourceProgram)
        {
            foreach (XmlAttribute attribute in node.Attributes)
            {
                switch (attribute.Name)
                {
                    case "Name":
                        sourceProgram.Name = attribute.Value;
                        break;
                    case "Station":
                        sourceProgram.Station = attribute.Value;
                        break;
                    case "Daypart":
                        sourceProgram.Daypart = attribute.Value;
                        break;
                    case "Day":
                        sourceProgram.Day = attribute.Value;
                        break;
                    case "Time":
                        sourceProgram.Time = attribute.Value;
                        break;
                }
            }
            foreach (XmlNode childNode in node.ChildNodes)
                switch (childNode.Name)
                {
                    case "Demo":
                        Demo demo = new Demo();
                        foreach (XmlAttribute attribute in childNode.Attributes)
                        {
                            switch (attribute.Name)
                            {
                                case "Name":
                                    demo.Name = attribute.Value;
                                    break;
                                case "Value":
                                    demo.Value = attribute.Value;
                                    break;
                            }
                        }
                        if (!string.IsNullOrEmpty(demo.Name) && !string.IsNullOrEmpty(demo.Value))
                            sourceProgram.Demos.Add(demo);
                        break;
                }
        }

        private void LoadLists()
        {
            LoadAdvertisers();
            LoadDecisionMakers();
            LoadTVStrategy();
        }

        public void SaveAdvertisers()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<Advertisers>");
            foreach (string advertiser in this.Advertisers)
                xml.AppendLine(@"<Advertiser>" + advertiser.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Advertiser>");
            xml.AppendLine(@"</Advertisers>");

            string userConfigurationPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.ListFolder, AdvertisersFileName);
            using (StreamWriter sw = new StreamWriter(userConfigurationPath, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }

        public void SaveDecisionMakers()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<DecisionMakers>");
            foreach (string decisionMaker in this.DecisionMakers)
                xml.AppendLine(@"<DecisionMaker>" + decisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
            xml.AppendLine(@"</DecisionMakers>");

            string userConfigurationPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.ListFolder, DecisionMakersFileName);
            using (StreamWriter sw = new StreamWriter(userConfigurationPath, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }
    }
}
