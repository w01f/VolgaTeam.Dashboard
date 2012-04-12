using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CommandCentral.TabMainDashboard
{
    class TargetCustomerManager
    {
        private const string TargetCustomerSourceFileName = @"Data\!Main_Dashboard\Basic Slides Source\Target Customer.xls";
        private const string TargetCustomerDestinationFileName = @"Data\!Main_Dashboard\Basic Slides XML\Target Customer.xml";


        public const string ButtonText = "Target\nCustomer";

        public static void ViewDataFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, TargetCustomerDestinationFileName));
        }

        public static void ViewSourceFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, TargetCustomerSourceFileName));
        }

        public static void UpdateData()
        {
            List<CommonClasses.SlideHeader> headers = new List<CommonClasses.SlideHeader>();
            List<string> demos = new List<string>();
            List<string> hhis = new List<string>();
            List<string> geographies = new List<string>();


            string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, TargetCustomerSourceFileName));
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

                //Load Headers
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Headers$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            CommonClasses.SlideHeader title = new CommonClasses.SlideHeader();
                            title.Value = row[0].ToString().Trim();
                            if (dataTable.Columns.Count > 1)
                                if (row[1] != null)
                                    title.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
                            if (!string.IsNullOrEmpty(title.Value))
                                headers.Add(title);
                        }
                    headers.Sort((x, y) =>
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

                //Load Demos
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Demos$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string demo = row[0].ToString().Trim();
                            if (!string.IsNullOrEmpty(demo))
                                demos.Add(demo);
                        }
                    demos.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x, y));
                }
                catch
                {
                }
                finally
                {
                    dataAdapter.Dispose();
                    dataTable.Dispose();
                }

                //Load HHI
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [HHI$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string hhi = row[0].ToString().Trim();
                            if (!string.IsNullOrEmpty(hhi))
                                hhis.Add(hhi);
                        }
                    hhis.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x, y));
                }
                catch
                {
                }
                finally
                {
                    dataAdapter.Dispose();
                    dataTable.Dispose();
                }
                //Load Geography
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Geography$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string geography = row[0].ToString().Trim();
                            if (!string.IsNullOrEmpty(geography))
                                geographies.Add(geography);
                        }
                    geographies.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x, y));
                }
                catch
                {
                }
                finally
                {
                    dataAdapter.Dispose();
                    dataTable.Dispose();
                }
                connection.Close();

                //Save XML
                StringBuilder xml = new StringBuilder();
                xml.AppendLine("<TargetCustomers>");
                foreach (CommonClasses.SlideHeader header in headers)
                {
                    xml.Append(@"<SlideHeader ");
                    xml.Append("Value = \"" + header.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("IsDefault = \"" + header.IsDefault + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (string demo in demos)
                {
                    xml.Append(@"<Demo ");
                    xml.Append("Value = \"" + demo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (string hhi in hhis)
                {
                    xml.Append(@"<HHI ");
                    xml.Append("Value = \"" + hhi.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (string geography in geographies)
                {
                    xml.Append(@"<Geography ");
                    xml.Append("Value = \"" + geography.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                xml.AppendLine(@"</TargetCustomers>");

                string xmlPath = Path.Combine(Application.StartupPath, TargetCustomerDestinationFileName);
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
