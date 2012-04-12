using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CommandCentral.TabSalesProForms
{
    class CampaignTimelineManager
    {
        private const string CampaignTimelineSourceFileName = @"Data\!App_Data\app_sales_PRO_Source\Campaign Timeline.xls";
        private const string CampaignTimelineDestinationFileName = @"Data\!App_Data\app_sales_PRO_XML\Campaign Timeline.xml";

        public const string ButtonText = "Campaign\nTimeline";

        public static void ViewDataFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, CampaignTimelineDestinationFileName));
        }

        public static void ViewSourceFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, CampaignTimelineSourceFileName));
        }

        public static void UpdateData()
        {
            List<CommonClasses.SlideHeader> headers = new List<CommonClasses.SlideHeader>();
            List<CommonClasses.Step> steps = new List<CommonClasses.Step>();


            string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, CampaignTimelineSourceFileName));
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

                //Load Steps
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Steps$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            CommonClasses.Step step = new CommonClasses.Step();
                            step.Value = row[0].ToString().Trim();
                            if (dataTable.Columns.Count > 1)
                                if (row[1] != null)
                                {
                                    int temp = -1;
                                    int.TryParse(row[1].ToString(), out temp);
                                    step.Position = temp;
                                }
                            if (!string.IsNullOrEmpty(step.Value))
                                steps.Add(step);
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
                connection.Close();

                //Save XML
                StringBuilder xml = new StringBuilder();
                xml.AppendLine("<CampaignTimeline>");
                foreach (CommonClasses.SlideHeader header in headers)
                {
                    xml.Append(@"<SlideHeader ");
                    xml.Append("Value = \"" + header.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("IsDefault = \"" + header.IsDefault + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (CommonClasses.Step step in steps)
                {
                    xml.Append(@"<Step ");
                    xml.Append("Value = \"" + step.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("Position = \"" + step.Position + "\" ");
                    xml.AppendLine(@"/>");
                }
                xml.AppendLine(@"</CampaignTimeline>");

                string xmlPath = Path.Combine(Application.StartupPath, CampaignTimelineDestinationFileName);
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
