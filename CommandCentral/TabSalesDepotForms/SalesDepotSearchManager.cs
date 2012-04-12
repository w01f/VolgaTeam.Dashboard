using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommandCentral.TabSalesDepotForms
{
    class SalesDepotSearchManager
    {
        private const string SalesDepotSearchSourceFileName = @"Data\!Main_Dashboard\SDSearch Source\SDSearch.xls";
        private const string SalesDepotSearchDestinationFileName = @"Data\!Main_Dashboard\SDSearch XML\SDSearch.xml";

        public const string ButtonText = "Sales Depot\nSearch";

        private static List<CommonClasses.SearchGroup> _categories = new List<CommonClasses.SearchGroup>();

        public static void ViewDataFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, SalesDepotSearchDestinationFileName));
        }

        public static void ViewSourceFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, SalesDepotSearchSourceFileName));
        }

        private static void GetCategories(OleDbConnection connection)
        {
            DataTable dataTable;
            try
            {
                _categories.Clear();
                dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow row in dataTable.Rows)
                {
                    CommonClasses.SearchGroup searchGroup = new CommonClasses.SearchGroup();
                    searchGroup.Name = row["TABLE_NAME"].ToString().Replace("$", "").Replace('"'.ToString(), "'").Replace("'", "");

                    if (!searchGroup.Name.Trim().Equals("Categories"))
                        _categories.Add(searchGroup);
                }
            }
            catch
            {
            }
        }

        public static void UpdateData()
        {
            List<CommonClasses.SearchGroup> searchGroups = new List<CommonClasses.SearchGroup>();

            string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, SalesDepotSearchSourceFileName));
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

                //Load Groups
                GetCategories(connection);
                dataAdapter = new OleDbDataAdapter("SELECT * FROM [Categories$]", connection);
                dataTable = new DataTable();
                try
                {
                    dataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 1 && dataTable.Columns.Count > 1)
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string groupName = row[0].ToString().Trim();
                            CommonClasses.SearchGroup searchGroup = _categories.Where(x => x.Name.Equals(groupName)).FirstOrDefault();
                            if (searchGroup != null)
                                searchGroup.Description = row[1].ToString().Trim();
                            searchGroups.Add(searchGroup);
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

                //Load Tags
                foreach (CommonClasses.SearchGroup group in searchGroups)
                {
                    group.Tags.Clear();
                    dataAdapter = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}$]", group.Name), connection);
                    dataTable = new DataTable();
                    try
                    {
                        dataAdapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                            foreach (DataRow row in dataTable.Rows)
                            {
                                string value = row[0].ToString().Trim();
                                if (!string.IsNullOrEmpty(value))
                                    group.Tags.Add(value);
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
                xml.AppendLine("<SDSearch>");
                foreach (CommonClasses.SearchGroup group in searchGroups)
                {
                    xml.Append(@"<Category ");
                    xml.Append("Name = \"" + group.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("Description = \"" + group.Description.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
                    xml.AppendLine(@">");
                    foreach (string tag in group.Tags)
                    {
                        xml.Append(@"<Tag ");
                        xml.Append("Value = \"" + tag.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
                        xml.AppendLine(@"/>");
                    }
                    xml.AppendLine(@"</Category>");
                }
                xml.AppendLine(@"</SDSearch>");

                string xmlPath = Path.Combine(Application.StartupPath, SalesDepotSearchDestinationFileName);
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
