using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CommandCentral.TabSalesDepotForms
{
    class SalesDepotAccessRightsManager
    {
        private const string SalesDepotAccessRightsSourceFileName = @"Data\!Main_Dashboard\SalesLibraryAccess Source\ApprovedLibraries.xls";
        private const string SalesDepotAccessRightsDestinationFileName = @"Data\!Main_Dashboard\SalesLibraryAccess XML\ApprovedLibraries.xml";

        public const string ButtonText = "Sales Depot\nAccess Rights";

        public static void ViewDataFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, SalesDepotAccessRightsDestinationFileName));
        }

        public static void ViewSourceFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, SalesDepotAccessRightsSourceFileName));
        }

        public static void UpdateData()
        {
            List<CommonClasses.SalesDepotUserLibraries> approvedLibraries = new List<CommonClasses.SalesDepotUserLibraries>();

            string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, SalesDepotAccessRightsSourceFileName));
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

                //Load Approved Sales Libraries
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [SalesLibraries$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 2)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            CommonClasses.SalesDepotUserLibraries approvedLibrariesForUser = new CommonClasses.SalesDepotUserLibraries();
                            Guid tempGuid;
                            if (Guid.TryParse(row[0].ToString().Trim().ToLower().Replace("appid-", string.Empty), out tempGuid))
                            {
                                approvedLibrariesForUser.AppID = tempGuid;
                                approvedLibrariesForUser.UserName = row[1].ToString().Trim();
                                for (int i = 2; i < dataTable.Columns.Count; i++)
                                    if (row[i] != null && !string.IsNullOrEmpty(row[i].ToString().Trim()))
                                        approvedLibrariesForUser.Libraries.Add(row[i].ToString().Trim());
                                approvedLibraries.Add(approvedLibrariesForUser);
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

                connection.Close();

                //Save XML
                StringBuilder xml = new StringBuilder();
                xml.AppendLine("<ApprovedLibraries>");
                foreach (CommonClasses.SalesDepotUserLibraries approvedLibrariesForUser in approvedLibraries)
                {
                    xml.Append(@"<User ");
                    xml.Append("Name = \"" + approvedLibrariesForUser.UserName.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("AppID = \"" + approvedLibrariesForUser.AppID.ToString() + "\" ");
                    xml.AppendLine(@">");
                    foreach (string library in approvedLibrariesForUser.Libraries)
                        xml.AppendLine(@"<Library>" + library.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Library>");
                    xml.AppendLine(@"</User>");
                }
                xml.AppendLine(@"</ApprovedLibraries>");

                string xmlPath = Path.Combine(Application.StartupPath, SalesDepotAccessRightsDestinationFileName);
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
