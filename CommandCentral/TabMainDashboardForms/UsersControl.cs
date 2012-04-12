using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CommandCentral.TabMainDashboard
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UsersControl : UserControl
    {
        private const string UsersSourceFolderName = @"Data\!Main_Dashboard\Users Source";
        private const string UsersSourceFileTemplate = @"UL-*.xls";
        private const string UsersDestinationFileName = @"Data\!Main_Dashboard\Users XML\Users.xml";

        private static UsersControl _instance = null;

        private UsersControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public static UsersControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UsersControl();
                return _instance;
            }
        }

        public void LoadButtons()
        {
            int leftPosition = 10;
            int topPosition = 10;

            pnButtonContainer.Controls.Clear();
            FileInfo[] sourceFiles = (new DirectoryInfo(Path.Combine(Application.StartupPath, UsersSourceFolderName))).GetFiles(UsersSourceFileTemplate);
            laTotalUserLists.Text = string.Format(laTotalUserLists.Text, sourceFiles.Length);
            foreach (FileInfo sourceFile in sourceFiles)
            {
                DevComponents.DotNetBar.ButtonX button = new DevComponents.DotNetBar.ButtonX();
                button.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
                button.Text = sourceFile.Name.Replace(sourceFile.Extension, "").Replace("UL-", "");
                button.Tag = sourceFile;
                button.Click += new EventHandler(buttonXUsers_Click);
                button.Height = 80;
                button.Width = 160;

                button.Left = leftPosition;
                button.Top = topPosition;

                if ((leftPosition + 180 + 160) > pnButtonContainer.Width)
                {
                    leftPosition = 10;
                    topPosition += 100;
                }
                else
                {
                    leftPosition += 180;
                }
                pnButtonContainer.Controls.Add(button);
            }
        }

        public void ViewFile()
        {
            AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, UsersDestinationFileName));
        }

        public void UpdateData()
        {
            List<CommonClasses.User> users = new List<CommonClasses.User>();

            string[] userTypes = new string[] { "Users", "Admins" };
            FileInfo[] sourceFiles = (new DirectoryInfo(Path.Combine(Application.StartupPath, UsersSourceFolderName))).GetFiles(UsersSourceFileTemplate);
            foreach (FileInfo sourceFile in sourceFiles)
            {
                string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", sourceFile.FullName);
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

                    foreach (string userType in userTypes)
                    {

                        //Load Users
                        dataAdapter = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}$]", userType), connection);
                        dataTable = new DataTable();
                        try
                        {
                            dataAdapter.Fill(dataTable);
                            if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    CommonClasses.User user = new CommonClasses.User();
                                    user.Station = sourceFile.Name.Replace(sourceFile.Extension, "").Replace("UL-", "");
                                    user.IsAdmin = userType.Equals(userTypes[0]) ? false : true;

                                    string temp = row[0].ToString().Trim();
                                    if (!string.IsNullOrEmpty(temp))
                                        user.FirstName = temp;

                                    temp = row[1].ToString().Trim();
                                    if (!string.IsNullOrEmpty(temp))
                                        user.LastName = temp;

                                    temp = row[2].ToString().Trim();
                                    if (!string.IsNullOrEmpty(temp))
                                        user.Phone = temp;

                                    temp = row[3].ToString().Trim();
                                    if (!string.IsNullOrEmpty(temp))
                                        user.Email = temp;

                                    if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
                                        users.Add(user);
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
                }
            }

            users.Sort((x, y) =>
            {
                int result = x.Station.CompareTo(y.Station);
                if (result == 0)
                {
                    result = x.FirstName.CompareTo(y.FirstName);
                    if (result == 0)
                    {
                        return x.LastName.CompareTo(y.LastName);
                    }
                    else
                        return result;
                }
                else
                    return result;
            });
            if (users.Count > 0)
            {

                //Save XML
                StringBuilder xml = new StringBuilder();
                xml.AppendLine("<Users>");
                xml.AppendLine("<TotalLists>" + sourceFiles.Length + "</TotalLists>");
                xml.AppendLine("<TotalUsers>" + users.Count + "</TotalUsers>");
                foreach (CommonClasses.User user in users)
                {
                    xml.Append(@"<User ");
                    xml.Append("Station = \"" + user.Station.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("FirstName = \"" + user.FirstName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("LastName = \"" + user.LastName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("Phone = \"" + user.Phone.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("Email = \"" + user.Email.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
                    xml.Append("IsAdmin = \"" + user.IsAdmin + "\" ");
                    xml.AppendLine(@"/>");
                }
                xml.AppendLine(@"</Users>");

                string xmlPath = Path.Combine(Application.StartupPath, UsersDestinationFileName);
                using (StreamWriter sw = new StreamWriter(xmlPath, false))
                {
                    sw.Write(xml.ToString());
                    sw.Flush();
                }
                AppManager.Instance.ShowInformation("Data was updated.");
            }
        }

        private void buttonXUsers_Click(object sender, EventArgs e)
        {
            AppManager.Instance.OpenFile(((FileInfo)((DevComponents.DotNetBar.ButtonX)sender).Tag).FullName);
        }
    }
}
