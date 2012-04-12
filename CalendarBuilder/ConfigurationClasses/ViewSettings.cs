using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CalendarBuilder.ConfigurationClasses
{
    public class ViewSettings
    {
        public string LocalSettingsPath { get; set; }

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

        public ViewSettings()
        {
            this.DayPropertiesDocked = true;
            this.LocalSettingsPath = string.Format(@"{0}\newlocaldirect.com\xml\app\CalendarSettings.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            Load();
        }

        private void Load()
        {
            XmlNode node;
            int tempInt;
            bool tempBool;
            if (File.Exists(this.LocalSettingsPath))
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.LocalSettingsPath);

                #region Day Properties
                node = document.SelectSingleNode(@"/ViewSettings/DayPropertiesDocked");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.DayPropertiesDocked = tempBool;
                node = document.SelectSingleNode(@"/ViewSettings/DayPropertiesFloatLeft");
                if (node != null)
                    if (int.TryParse(node.InnerText, out tempInt))
                        this.DayPropertiesFloatLeft = tempInt;
                node = document.SelectSingleNode(@"/ViewSettings/DayPropertiesFloatTop");
                if (node != null)
                    if (int.TryParse(node.InnerText, out tempInt))
                        this.DayPropertiesFloatTop = tempInt;
                #endregion

                #region Slide Info Properties
                node = document.SelectSingleNode(@"/ViewSettings/SlideInfoVisible");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.SlideInfoVisible = tempBool;
                node = document.SelectSingleNode(@"/ViewSettings/SlideInfoDocked");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.SlideInfoDocked = tempBool;
                node = document.SelectSingleNode(@"/ViewSettings/SlideInfoFloatLeft");
                if (node != null)
                    if (int.TryParse(node.InnerText, out tempInt))
                        this.SlideInfoFloatLeft = tempInt;
                node = document.SelectSingleNode(@"/ViewSettings/SlideInfoFloatTop");
                if (node != null)
                    if (int.TryParse(node.InnerText, out tempInt))
                        this.SlideInfoFloatTop = tempInt;
                #endregion

                #region Grid Properties
                node = document.SelectSingleNode(@"/ViewSettings/GridVisible");
                if (node != null)
                    if (bool.TryParse(node.InnerText, out tempBool))
                        this.GridVisible = tempBool;
                #endregion
            }
        }

        public void Save()
        {
            StringBuilder xml = new StringBuilder();

            xml.AppendLine(@"<ViewSettings>");
            #region Day Properties
            xml.AppendLine(@"<DayPropertiesDocked>" + this.DayPropertiesDocked + @"</DayPropertiesDocked>");
            xml.AppendLine(@"<DayPropertiesFloatLeft>" + this.DayPropertiesFloatLeft + @"</DayPropertiesFloatLeft>");
            xml.AppendLine(@"<DayPropertiesFloatTop>" + this.DayPropertiesFloatTop + @"</DayPropertiesFloatTop>");
            #endregion

            #region Slide Info Properties
            xml.AppendLine(@"<SlideInfoVisible>" + this.SlideInfoVisible + @"</SlideInfoVisible>");
            xml.AppendLine(@"<SlideInfoDocked>" + this.SlideInfoDocked + @"</SlideInfoDocked>");
            xml.AppendLine(@"<SlideInfoFloatLeft>" + this.SlideInfoFloatLeft + @"</SlideInfoFloatLeft>");
            xml.AppendLine(@"<SlideInfoFloatTop>" + this.SlideInfoFloatTop + @"</SlideInfoFloatTop>");
            #endregion

            #region Slide Info Properties
            xml.AppendLine(@"<GridVisible>" + this.GridVisible + @"</GridVisible>");
            #endregion
            xml.AppendLine(@"</ViewSettings>");

            string userConfigurationPath = this.LocalSettingsPath;
            using (StreamWriter sw = new StreamWriter(userConfigurationPath, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }
    }
}
