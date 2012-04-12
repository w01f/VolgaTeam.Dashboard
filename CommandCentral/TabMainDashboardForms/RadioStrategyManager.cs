using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CommandCentral.TabMainDashboard
{
    class RadioStrategyManager
    {
        private const string SourceFileName = @"Data\!Main_Dashboard\Radio Source\Radio Strategy.xls";
        private const string DestinationFileName = @"Data\!Main_Dashboard\Radio XML\Radio Strategy.xml";

        public const string ButtonText = "Radio Strategy\nData";

        private static List<CommonClasses.RadioStation> _radioStations = new List<CommonClasses.RadioStation>();

        private static void GetStations(OleDbConnection connection)
        {
            DataTable dataTable;
            try
            {
                _radioStations.Clear();
                dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow row in dataTable.Rows)
                {
                    CommonClasses.RadioStation radioStation = new CommonClasses.RadioStation();
                    radioStation.Name = row["TABLE_NAME"].ToString().Replace("$", "").Replace('"'.ToString(), "'").Replace("'", "");
                    if (!radioStation.Name.Trim().Equals("Headers-Positioning Point") && !radioStation.Name.Trim().Equals("Length"))
                        _radioStations.Add(radioStation);
                }
            }
            catch
            {
            }
        }


        public static void ViewDataFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, DestinationFileName));
        }

        public static void ViewSourceFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, SourceFileName));
        }

        public static void UpdateData()
        {
            List<CommonClasses.SlideHeader> slideHeaders = new List<CommonClasses.SlideHeader>();
            List<string> positioningPoints = new List<string>();
            List<string> lenghts = new List<string>();

            string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=No;IMEX=1"";", Path.Combine(Application.StartupPath, SourceFileName));
            OleDbConnection connection = new OleDbConnection(connnectionString);
            try
            {
                connection.Open();
            }
            catch
            {
                AppManager.Instance.ShowWarning("Couldn't open source file");
                return;
            }

            if (connection.State == ConnectionState.Open)
            {
                OleDbDataAdapter dataAdapter;
                DataTable dataTable;

                //Load Headers And Positioning Points
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Headers-Positioning Point$]", connection);
                dataTable = new DataTable();

                bool loadHeaders = false;
                bool loadPositioningPoint = false;
                slideHeaders.Clear();
                positioningPoints.Clear();

                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (row[0].ToString().Trim().Equals("*Slide Headers"))
                            {
                                loadHeaders = true;
                                loadPositioningPoint = false;
                                continue;
                            }
                            if (row[0].ToString().Trim().Equals("*Positioning Point"))
                            {
                                loadHeaders = false;
                                loadPositioningPoint = true;
                                continue;
                            }

                            if (loadHeaders)
                            {
                                CommonClasses.SlideHeader title = new CommonClasses.SlideHeader();
                                title.Value = row[0].ToString().Trim();
                                if (dataTable.Columns.Count > 1)
                                    if (row[1] != null)
                                        title.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
                                if (!string.IsNullOrEmpty(title.Value))
                                    slideHeaders.Add(title);
                            }

                            if (loadPositioningPoint)
                            {
                                string positioningPoint = row[0].ToString().Trim();
                                if (!string.IsNullOrEmpty(positioningPoint))
                                    positioningPoints.Add(positioningPoint);
                            }
                        }

                    slideHeaders.Sort((x, y) =>
                    {
                        int result = y.IsDefault.CompareTo(x.IsDefault);
                        if (result == 0)
                            result = InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Value, y.Value);
                        return result;
                    });
                    positioningPoints.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x, y));
                }
                catch
                {
                }
                finally
                {
                    dataAdapter.Dispose();
                    dataTable.Dispose();
                }

                //Load Lenghts
                lenghts.Clear();
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Length$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string lenght = row[0].ToString().Trim();
                            if (!string.IsNullOrEmpty(lenght))
                                lenghts.Add(lenght);
                        }
                }
                catch
                {
                }
                finally
                {
                    dataAdapter.Dispose();
                    dataTable.Dispose();
                }

                //Load RadioStations
                GetStations(connection);
                foreach (var radioStation in _radioStations)
                {
                    dataAdapter = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}$]", radioStation.Name), connection);
                    dataTable = new DataTable();

                    bool loadPrograms = false;
                    bool loadDaypartRotations = false;

                    try
                    {
                        dataAdapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                            foreach (DataRow row in dataTable.Rows)
                            {
                                if (row[0].ToString().Trim().Equals("*Program"))
                                {
                                    loadPrograms = true;
                                    loadDaypartRotations = false;
                                    for (int i = 0; i < 40; i++)
                                    {
                                        if (dataTable.Columns.Count > (i + 3))
                                            if (row[i + 3] != null)
                                                if (!string.IsNullOrEmpty(row[i + 3].ToString().Trim()))
                                                {
                                                    CommonClasses.Demo demo = new CommonClasses.Demo();
                                                    demo.Name = string.Format("Demo{0}", i + 1);
                                                    demo.Value = row[i + 3].ToString().Trim();
                                                    radioStation.Demos.Add(demo);
                                                }
                                    }
                                    continue;
                                }
                                if (row[0].ToString().Trim().Equals("*Daypart Rotations"))
                                {
                                    loadPrograms = false;
                                    loadDaypartRotations = true;
                                    continue;
                                }

                                if (loadPrograms)
                                {
                                    CommonClasses.RadioProgram radioProgram = new CommonClasses.RadioProgram();
                                    radioProgram.Name = row[0].ToString().Trim();
                                    if (dataTable.Columns.Count > 1)
                                        if (row[1] != null)
                                            radioProgram.Day = row[1].ToString().Trim();
                                    if (dataTable.Columns.Count > 2)
                                        if (row[2] != null)
                                            radioProgram.Time = row[2].ToString().Trim();
                                    for (int i = 0; i < radioStation.Demos.Count; i++)
                                    {
                                        if (dataTable.Columns.Count > (i + 3))
                                            if (row[i + 3] != null)
                                                radioProgram.DemoValues.Add(row[i + 3].ToString().Trim());
                                    }

                                    if (!string.IsNullOrEmpty(radioProgram.Name))
                                        radioStation.Programs.Add(radioProgram);
                                }

                                if (loadDaypartRotations)
                                {
                                    CommonClasses.DaypartRotation daypartRotation = new CommonClasses.DaypartRotation();
                                    daypartRotation.Name = row[0].ToString().Trim();
                                    if (dataTable.Columns.Count > 1)
                                        if (row[1] != null)
                                            daypartRotation.Day = row[1].ToString().Trim();
                                    if (dataTable.Columns.Count > 2)
                                        if (row[2] != null)
                                            daypartRotation.Time = row[2].ToString().Trim();
                                    for (int i = 0; i < radioStation.Demos.Count; i++)
                                    {
                                        if (dataTable.Columns.Count > (i + 3))
                                            if (row[i + 3] != null)
                                                daypartRotation.DemoValues.Add(row[i + 3].ToString().Trim());
                                    }
                                    if (!string.IsNullOrEmpty(daypartRotation.Name))
                                        radioStation.DaypartRotatiions.Add(daypartRotation);
                                }
                            }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        dataAdapter.Dispose();
                        dataTable.Dispose();
                    }
                }
                connection.Close();

                //Save XML
                StringBuilder xml = new StringBuilder();
                xml.AppendLine("<RadioStrategy>");
                foreach (var header in slideHeaders)
                {
                    xml.Append(@"<SlideHeader ");
                    xml.Append("Value = \"" + header.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("IsDefault = \"" + header.IsDefault + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (var positionPoint in positioningPoints)
                {
                    xml.Append(@"<Statement ");
                    xml.Append("Value = \"" + positionPoint.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (var lenght in lenghts)
                {
                    xml.Append(@"<Lenght ");
                    xml.Append("Value = \"" + lenght.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (var radioStation in _radioStations)
                {
                    xml.Append(@"<RadioStation ");
                    xml.Append("Name = \"" + radioStation.Name.Replace(@"#", ".").Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@">");
                    foreach (var demo in radioStation.Demos)
                    {
                        xml.Append(@"<Demo ");
                        xml.Append("Name = \"" + demo.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                        xml.Append("Value = \"" + demo.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                        xml.AppendLine(@"/>");
                    }
                    foreach (var program in radioStation.Programs)
                    {
                        xml.Append(@"<Program ");
                        xml.Append("Name = \"" + program.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                        xml.Append("Day = \"" + program.Day.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                        xml.Append("Time = \"" + program.Time.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                        for (int i = 0; i < program.DemoValues.Count; i++)
                            xml.Append(radioStation.Demos[i].Name + " = \"" + program.DemoValues[i].Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                        xml.AppendLine(@"/>");
                    }
                    foreach (var daypartRotation in radioStation.DaypartRotatiions)
                    {
                        xml.Append(@"<DaypartRotation ");
                        xml.Append("Name = \"" + daypartRotation.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                        xml.Append("Day = \"" + daypartRotation.Day.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                        xml.Append("Time = \"" + daypartRotation.Time.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                        for (int i = 0; i < daypartRotation.DemoValues.Count; i++)
                            xml.Append(radioStation.Demos[i].Name + " = \"" + daypartRotation.DemoValues[i].Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                        xml.AppendLine(@"/>");
                    }
                    xml.AppendLine(@"</RadioStation>");
                }
                xml.AppendLine(@"</RadioStrategy>");

                string xmlPath = Path.Combine(Application.StartupPath, DestinationFileName);
                using (StreamWriter sw = new StreamWriter(xmlPath, false))
                {
                    sw.Write(xml.ToString());
                    sw.Flush();
                }

                AppManager.Instance.ShowInformation("Data was updated.");
            }
        }
    }
}
