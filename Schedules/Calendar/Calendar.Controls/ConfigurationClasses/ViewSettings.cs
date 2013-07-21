using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CalendarBuilder.ConfigurationClasses
{
    public class LocalSettings
    {
        public string LocalSettingsPath { get; set; }
        public CalendarSettings AdvancedCalendarSettings { get; private set; }
        public CalendarSettings GraphicCalendarSettings { get; private set; }
        public CalendarSettings SimpleCalendarSettings { get; private set; }

        public LocalSettings()
        {
            this.LocalSettingsPath = string.Format(@"{0}\newlocaldirect.com\xml\app\CalendarSettings.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            this.AdvancedCalendarSettings = new CalendarSettings();
            this.GraphicCalendarSettings = new CalendarSettings();
            this.SimpleCalendarSettings = new CalendarSettings();
            Load();
        }

        private void Load()
        {
            XmlNode node;
            if (File.Exists(this.LocalSettingsPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.LocalSettingsPath);

                node = document.SelectSingleNode(@"/LocalSettings/AdvancedCalendarSettings");
                if (node != null)
                    this.AdvancedCalendarSettings.Deserialize(node);

                node = document.SelectSingleNode(@"/LocalSettings/GraphicCalendarSettings");
                if (node != null)
                    this.GraphicCalendarSettings.Deserialize(node);

                node = document.SelectSingleNode(@"/LocalSettings/SimpleCalendarSettings");
                if (node != null)
                    this.SimpleCalendarSettings.Deserialize(node);
            }
        }

        public void Save()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<LocalSettings>");
            xml.AppendLine(@"<AdvancedCalendarSettings>" + this.AdvancedCalendarSettings.Serialize() + @"</AdvancedCalendarSettings>");
            xml.AppendLine(@"<GraphicCalendarSettings>" + this.GraphicCalendarSettings.Serialize() + @"</GraphicCalendarSettings>");
            xml.AppendLine(@"<SimpleCalendarSettings>" + this.SimpleCalendarSettings.Serialize() + @"</SimpleCalendarSettings>");
            xml.AppendLine(@"</LocalSettings>");

            string userConfigurationPath = this.LocalSettingsPath;
            using (StreamWriter sw = new StreamWriter(userConfigurationPath, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }
    }

    public class CalendarSettings
    {
        #region Day Properties
        public bool DayPropertiesDocked { get; set; }
        public int DayPropertiesFloatLeft { get; set; }
        public int DayPropertiesFloatTop { get; set; }
        #endregion

        #region Slide Info
        public bool SlideInfoVisible { get; set; }
        public bool SlideInfoDocked { get; set; }
        public int SlideInfoFloatLeft { get; set; }
        public int SlideInfoFloatTop { get; set; }
        #endregion

        #region Grid
        public bool GridVisible { get; set; }
        #endregion

        public CalendarSettings()
        {
            this.DayPropertiesDocked = true;
            this.SlideInfoDocked = true;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            #region Day Properties
            result.AppendLine(@"<DayPropertiesDocked>" + this.DayPropertiesDocked + @"</DayPropertiesDocked>");
            result.AppendLine(@"<DayPropertiesFloatLeft>" + this.DayPropertiesFloatLeft + @"</DayPropertiesFloatLeft>");
            result.AppendLine(@"<DayPropertiesFloatTop>" + this.DayPropertiesFloatTop + @"</DayPropertiesFloatTop>");
            #endregion

            #region Slide Info Properties
            result.AppendLine(@"<SlideInfoVisible>" + this.SlideInfoVisible + @"</SlideInfoVisible>");
            result.AppendLine(@"<SlideInfoDocked>" + this.SlideInfoDocked + @"</SlideInfoDocked>");
            result.AppendLine(@"<SlideInfoFloatLeft>" + this.SlideInfoFloatLeft + @"</SlideInfoFloatLeft>");
            result.AppendLine(@"<SlideInfoFloatTop>" + this.SlideInfoFloatTop + @"</SlideInfoFloatTop>");
            #endregion

            #region Slide Info Properties
            result.AppendLine(@"<GridVisible>" + this.GridVisible + @"</GridVisible>");
            #endregion
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            bool tempBool = false;
            int tempInt;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    #region Day Properties
                    case "DayPropertiesDocked":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.DayPropertiesDocked = tempBool;
                        break;
                    case "DayPropertiesFloatLeft":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DayPropertiesFloatLeft = tempInt;
                        break;
                    case "DayPropertiesFloatTop":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.DayPropertiesFloatTop = tempInt;
                        break;
                    #endregion

                    #region Slide Info Properties
                    case "SlideInfoVisible":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.SlideInfoVisible = tempBool;
                        break;
                    case "SlideInfoDocked":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.SlideInfoDocked = tempBool;
                        break;
                    case "SlideInfoFloatLeft":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.SlideInfoFloatLeft = tempInt;
                        break;
                    case "SlideInfoFloatTop":
                        if (int.TryParse(childNode.InnerText, out tempInt))
                            this.SlideInfoFloatTop = tempInt;
                        break;
                    #endregion

                    #region Grid Properties
                    case "GridVisible":
                        if (bool.TryParse(childNode.InnerText, out tempBool))
                            this.GridVisible = tempBool;
                        break;
                    #endregion
                }
            }
        }
    }
}
