using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommandCentral.TabMainDashboard
{
    class RadioStrategyManager
    {
        private const string SourceFileName = @"Data\!Main_Dashboard\Radio Source\Radio Strategy.xls";
        private const string DestinationFileName = @"Data\!Main_Dashboard\Radio XML\Radio Strategy.xml";
        private const string ImageSourceFolder = @"Data\!Main_Dashboard\Radio Source\Radio Images";

        public const string ButtonText = "Radio Strategy\nData";

        private static List<CommonClasses.NameCodePair> _dayparts = new List<CommonClasses.NameCodePair>();
        private static List<CommonClasses.RadioProgram> _programs = new List<CommonClasses.RadioProgram>();

        private static void GetDayparts(OleDbConnection connection)
        {
            DataTable dataTable;
            try
            {
                _dayparts.Clear();
                dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow row in dataTable.Rows)
                {
                    CommonClasses.NameCodePair daypart = new CommonClasses.NameCodePair();
                    daypart.Name = row["TABLE_NAME"].ToString().Replace("$", "").Replace('"'.ToString(), "'").Replace("'", "");

                    if (!daypart.Name.Trim().Equals("Headers-Positioning Point") && !daypart.Name.Trim().Equals("Length") && !daypart.Name.Trim().Equals("Dayparts") && !daypart.Name.Trim().Equals("Stations") && !daypart.Name.Trim().Equals("Client Type") && !daypart.Name.Trim().Equals("Custom Demos") && !daypart.Name.Trim().Equals("Sources") && !daypart.Name.Trim().Equals("File-Status"))
                        _dayparts.Add(daypart);
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
            List<string> clientTypes = new List<string>();
            List<string> customDemos = new List<string>();
            List<string> sources = new List<string>();
            List<CommonClasses.SlideHeader> statuses = new List<CommonClasses.SlideHeader>();
            List<CommonClasses.Station> stations = new List<CommonClasses.Station>();

            string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, SourceFileName));
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

                bool loadHeaders = true;
                bool loadPositioningPoint = false;
                slideHeaders.Clear();
                positioningPoints.Clear();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
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
                            result = 1;
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

                //Load Statuses
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [File-Status$]", connection);
                dataTable = new DataTable();
                statuses.Clear();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            CommonClasses.SlideHeader status = new CommonClasses.SlideHeader();
                            status.Value = row[0].ToString().Trim();
                            if (dataTable.Columns.Count > 1)
                                if (row[1] != null)
                                    status.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
                            if (!string.IsNullOrEmpty(status.Value))
                                statuses.Add(status);
                        }

                    statuses.Sort((x, y) =>
                    {
                        int result = y.IsDefault.CompareTo(x.IsDefault);
                        if (result == 0)
                            result = InteropClasses.WinAPIHelper.StrCmpLogicalW(x.Value, y.Value);
                        return result;
                    });
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

                //Load Client Types
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Client Type$]", connection);
                dataTable = new DataTable();
                clientTypes.Clear();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string clientType = row[0].ToString().Trim();
                            if (!string.IsNullOrEmpty(clientType))
                                clientTypes.Add(clientType);
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

                //Load Dayparts
                GetDayparts(connection);
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Dayparts$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 1)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string code = row[1].ToString().Trim();
                            CommonClasses.NameCodePair daypart = _dayparts.Where(x => x.Name.Equals(code)).FirstOrDefault();
                            if (daypart != null)
                                daypart.Code = row[0].ToString().Trim();
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

                //Load Custom Demos
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Custom Demos$]", connection);
                dataTable = new DataTable();
                customDemos.Clear();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string customDemo = row[0].ToString().Trim();
                            if (!string.IsNullOrEmpty(customDemo))
                                customDemos.Add(customDemo);
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

                //Load Sources
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Sources$]", connection);
                dataTable = new DataTable();
                sources.Clear();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string source = row[0].ToString().Trim();
                            if (!string.IsNullOrEmpty(source))
                                sources.Add(source);
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

                //Load Stations
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Stations$]", connection);
                dataTable = new DataTable();
                stations.Clear();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 1)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            CommonClasses.Station station = new CommonClasses.Station();
                            station.Name = row[0].ToString().Trim();
                            string filePath = Path.Combine(Application.StartupPath, ImageSourceFolder, row[1].ToString().Trim());
                            if (File.Exists(filePath))
                                station.Logo = new Bitmap(filePath);
                            if (!string.IsNullOrEmpty(station.Name))
                                stations.Add(station);
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

                //Load Radio Programs
                _programs.Clear();
                foreach (var daypart in _dayparts)
                {
                    dataAdapter = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}$]", daypart.Name), connection);
                    dataTable = new DataTable();
                    try
                    {
                        dataAdapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0 && dataTable.Columns.Count >= 4)
                            foreach (DataRow row in dataTable.Rows)
                            {
                                CommonClasses.RadioProgram program = new CommonClasses.RadioProgram();
                                program.Station = row[0].ToString().Trim();
                                program.Name = row[1].ToString().Trim();
                                program.Day = row[2].ToString().Trim();
                                program.Time = row[3].ToString().Trim();
                                program.Daypart = daypart.Code;
                                for (int i = 4; i < 44; i++)
                                {
                                    if (dataTable.Columns.Count > i)
                                        if (row[i] != null)
                                        {
                                            CommonClasses.Demo demo = new CommonClasses.Demo();
                                            demo.Name = dataTable.Columns[i].ColumnName;
                                            demo.Value = row[i].ToString().Trim();
                                            if (!string.IsNullOrEmpty(demo.Name) && !string.IsNullOrEmpty(demo.Value))
                                                program.Demos.Add(demo);
                                        }
                                }
                                if (!string.IsNullOrEmpty(program.Name))
                                    _programs.Add(program);
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
                foreach (var status in statuses)
                {
                    xml.Append(@"<Status ");
                    xml.Append("Value = \"" + status.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
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
                foreach (var daypart in _dayparts)
                {
                    xml.Append(@"<Daypart ");
                    xml.Append("Name = \"" + daypart.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("Code = \"" + daypart.Code.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
                foreach (var station in stations)
                {
                    xml.Append(@"<Station ");
                    xml.Append("Name = \"" + station.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("Logo = \"" + Convert.ToBase64String((byte[])converter.ConvertTo(station.Logo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (var clientType in clientTypes)
                {
                    xml.Append(@"<ClientType ");
                    xml.Append("Value = \"" + clientType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (var customDemo in customDemos)
                {
                    xml.Append(@"<CustomDemo ");
                    xml.Append("Value = \"" + customDemo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (var source in sources)
                {
                    xml.Append(@"<Source ");
                    xml.Append("Value = \"" + source.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (var program in _programs)
                {
                    xml.Append(@"<Program ");
                    xml.Append("Name = \"" + program.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("Station = \"" + program.Station.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("Daypart = \"" + program.Daypart.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("Day = \"" + program.Day.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("Time = \"" + program.Time.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@">");
                    foreach (var demo in program.Demos)
                    {
                        xml.Append(@"<Demo ");
                        xml.Append("Name = \"" + demo.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                        xml.Append("Value = \"" + demo.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                        xml.AppendLine(@"/>");
                    }
                    xml.AppendLine(@"</Program>");
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
