using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommandCentral.TabSalesDepotForms
{
    class SalesDepotAccessRightsManager
    {
        private const string DefaultUserName = "Default";
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

                //Load Default Approved Local Sales Libraries
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [LocalLibraries-ALLUSERS$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 2)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            CommonClasses.SalesDepotUserLibraries approvedLibrariesForUser = approvedLibraries.Where(x => x.AppID.Equals(Guid.Empty) && x.UserName.Equals(DefaultUserName)).FirstOrDefault();
                            if (approvedLibrariesForUser == null)
                            {
                                approvedLibrariesForUser = new CommonClasses.SalesDepotUserLibraries();
                                approvedLibrariesForUser.AppID = Guid.Empty;
                                approvedLibrariesForUser.UserName = DefaultUserName;
                                approvedLibraries.Add(approvedLibrariesForUser);
                            }
                            for (int i = 0; i < dataTable.Columns.Count; i++)
                            {
                                if (row[i] != null)
                                {
                                    string libararyName = dataTable.Columns[i].ColumnName.Trim();
                                    bool approveLibrary = row[i].ToString().Trim().ToLower().Equals("yes");
                                    if (approveLibrary && !string.IsNullOrEmpty(libararyName))
                                        approvedLibrariesForUser.LocalLibraries.Add(libararyName);
                                }
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


                //Load Approved Local Sales Libraries for Custom Users
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [LocalLibraries-EXCEPTIONS$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 2)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            Guid tempGuid;
                            if (Guid.TryParse(row[0].ToString().Trim().ToLower().Replace("appid-", string.Empty), out tempGuid))
                            {
                                string userName = row[1].ToString().Trim();
                                CommonClasses.SalesDepotUserLibraries approvedLibrariesForUser = approvedLibraries.Where(x => x.AppID.Equals(tempGuid) && x.UserName.Equals(userName)).FirstOrDefault();
                                if (approvedLibrariesForUser == null)
                                {
                                    approvedLibrariesForUser = new CommonClasses.SalesDepotUserLibraries();
                                    approvedLibrariesForUser.AppID = tempGuid;
                                    approvedLibrariesForUser.UserName = userName;
                                    approvedLibraries.Add(approvedLibrariesForUser);
                                }
                                for (int i = 2; i < dataTable.Columns.Count; i++)
                                {
                                    if (row[i] != null)
                                    {
                                        string libararyName = dataTable.Columns[i].ColumnName.Trim();
                                        bool approveLibrary = row[i].ToString().Trim().ToLower().Equals("yes");
                                        if (approveLibrary && !string.IsNullOrEmpty(libararyName))
                                            approvedLibrariesForUser.LocalLibraries.Add(libararyName);
                                    }
                                }
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

                //Load Default Approved Remote Sales Libraries
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [RemoteLibraries-ALLUSERS$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 3)
                        foreach (DataRow row in dataTable.Rows)
                        {
                                CommonClasses.SalesDepotUserLibraries approvedLibrariesForUser = approvedLibraries.Where(x => x.AppID.Equals(Guid.Empty) && x.UserName.Equals(DefaultUserName)).FirstOrDefault();
                                if (approvedLibrariesForUser == null)
                                {
                                    approvedLibrariesForUser = new CommonClasses.SalesDepotUserLibraries();
                                    approvedLibrariesForUser.AppID = Guid.Empty;
                                    approvedLibrariesForUser.UserName = DefaultUserName;
                                    approvedLibraries.Add(approvedLibrariesForUser);
                                }
                                approvedLibrariesForUser.UseRemoteLibraries = row[0].ToString().Trim().ToLower().Equals("yes");
                                for (int i = 1; i < dataTable.Columns.Count; i++)
                                {
                                    if (row[i] != null)
                                    {
                                        string libararyName = dataTable.Columns[i].ColumnName.Trim();
                                        bool approveLibrary = row[i].ToString().Trim().ToLower().Equals("yes");
                                        if (approveLibrary && !string.IsNullOrEmpty(libararyName))
                                            approvedLibrariesForUser.RemoteLibraries.Add(libararyName);
                                    }
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

                //Load Approved Remote Sales Libraries for Custom Users
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [RemoteLibraries-EXCEPTIONS$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 3)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            Guid tempGuid;
                            if (Guid.TryParse(row[0].ToString().Trim().ToLower().Replace("appid-", string.Empty), out tempGuid))
                            {
                                string userName = row[1].ToString().Trim();
                                CommonClasses.SalesDepotUserLibraries approvedLibrariesForUser = approvedLibraries.Where(x => x.AppID.Equals(tempGuid) && x.UserName.Equals(userName)).FirstOrDefault();
                                if (approvedLibrariesForUser == null)
                                {
                                    approvedLibrariesForUser = new CommonClasses.SalesDepotUserLibraries();
                                    approvedLibrariesForUser.AppID = tempGuid;
                                    approvedLibrariesForUser.UserName = userName;
                                    approvedLibraries.Add(approvedLibrariesForUser);
                                }
                                approvedLibrariesForUser.UseRemoteLibraries = row[2].ToString().Trim().ToLower().Equals("yes");
                                for (int i = 3; i < dataTable.Columns.Count; i++)
                                {
                                    if (row[i] != null)
                                    {
                                        string libararyName = dataTable.Columns[i].ColumnName.Trim();
                                        bool approveLibrary = row[i].ToString().Trim().ToLower().Equals("yes");
                                        if (approveLibrary && !string.IsNullOrEmpty(libararyName))
                                            approvedLibrariesForUser.RemoteLibraries.Add(libararyName);
                                    }
                                }
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
                    xml.Append("UseRemoteLibraries = \"" + approvedLibrariesForUser.UseRemoteLibraries.ToString() + "\" ");
                    xml.AppendLine(@">");
                    foreach (string library in approvedLibrariesForUser.LocalLibraries)
                        xml.AppendLine(@"<LocalLibrary>" + library.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</LocalLibrary>");
                    foreach (string library in approvedLibrariesForUser.RemoteLibraries)
                        xml.AppendLine(@"<RemoteLibrary>" + library.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</RemoteLibrary>");
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
