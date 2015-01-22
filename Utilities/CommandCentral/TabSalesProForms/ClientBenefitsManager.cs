using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CommandCentral.Entities.Common;

namespace CommandCentral.TabSalesProForms
{
    class ClientBenefitsManager
    {
        private const string ClientBenefitsSourceFileName = @"Data\!App_Data\app_sales_PRO_Source\Client Benefits.xls";
        private const string ClientBenefitsDestinationFileName = @"Data\!App_Data\app_sales_PRO_XML\Client Benefits.xml";

        public const string ButtonText = "Client\nBenefits";

        public static void ViewDataFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, ClientBenefitsDestinationFileName));
        }

        public static void ViewSourceFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, ClientBenefitsSourceFileName));
        }

        public static void UpdateData()
        {
            List<SlideHeader> headers = new List<SlideHeader>();
            List<string> benefits = new List<string>();


            string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, ClientBenefitsSourceFileName));
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
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Slide Headers$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            SlideHeader title = new SlideHeader();
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

                //Load Benefits
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Benefits$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string benefit = row[0].ToString().Trim();
                            if (!string.IsNullOrEmpty(benefit))
                                benefits.Add(benefit);
                        }
                    benefits.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x, y));
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
                xml.AppendLine("<ClientBenefits>");
                foreach (SlideHeader header in headers)
                {
                    xml.Append(@"<SlideHeader ");
                    xml.Append("Value = \"" + header.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("IsDefault = \"" + header.IsDefault + "\" ");
                    xml.AppendLine(@"/>");
                }
                foreach (string benefit in benefits)
                {
                    xml.Append(@"<Benefit ");
                    xml.Append("Value = \"" + benefit.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@"/>");
                }
                xml.AppendLine(@"</ClientBenefits>");

                string xmlPath = Path.Combine(Application.StartupPath, ClientBenefitsDestinationFileName);
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
