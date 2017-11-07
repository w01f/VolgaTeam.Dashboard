﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CommandCentral.Entities.Dashboard;
using DevComponents.DotNetBar;

namespace CommandCentral.TabMainDashboardForms
{
	[ToolboxItem(false)]
	public partial class UsersControl : UserControl
	{
		private const string UsersSourceFolderName = @"Data\!Main_Dashboard\Users Source";
		private const string UsersSourceFileTemplate = @"UL-*.xlsx";
		private const string UsersDestinationFileName = @"Data\!Main_Dashboard\Users XML\Users.xml";

		private static UsersControl _instance;

		private UsersControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
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
			laTotalUserLists.Text = String.Format(laTotalUserLists.Text, sourceFiles.Length);
			foreach (FileInfo sourceFile in sourceFiles)
			{
				var button = new ButtonX();
				button.ColorTable = eButtonColor.OrangeWithBackground;
				button.Text = sourceFile.Name.Replace(sourceFile.Extension, "").Replace("UL-", "");
				button.TextColor = Color.Black;
				button.Tag = sourceFile;
				button.Click += buttonXUsers_Click;
				button.Height = 80;
				button.Width = 160;
				button.Style = eDotNetBarStyle.StyleManagerControlled;

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
			var users = new List<User>();

			var sourceFiles = (new DirectoryInfo(Path.Combine(Application.StartupPath, UsersSourceFolderName))).GetFiles(UsersSourceFileTemplate);
			foreach (var sourceFile in sourceFiles)
			{
				string connnectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1"";", sourceFile.FullName);
				var connection = new OleDbConnection(connnectionString);
				try
				{
					connection.Open();
				}
				catch (Exception ex)
				{
					AppManager.Instance.ShowWarning("Couldn't open source file");
					return;
				}
				if (connection.State != ConnectionState.Open) continue;

				var sheetName = string.Empty;
				var dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
				foreach (DataRow row in dataTable.Rows)
				{
					sheetName = row["TABLE_NAME"].ToString().Replace("$", "").Replace('"'.ToString(), "'").Replace("'", "");
					break;
				}

				//Load Users
				var dataAdapter = new OleDbDataAdapter(String.Format("SELECT * FROM [{0}$]", sheetName), connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							var user = new User();
							user.Station = sourceFile.Name.Replace(sourceFile.Extension, "").Replace("UL-", "");

							string temp = row[0].ToString().Trim();
							if (!String.IsNullOrEmpty(temp))
								user.FirstName = temp;

							temp = row[1].ToString().Trim();
							if (!String.IsNullOrEmpty(temp))
								user.LastName = temp;

							temp = row[2].ToString().Trim();
							if (!String.IsNullOrEmpty(temp))
								user.Email = temp;

							temp = row[3].ToString().Trim();
							if (!String.IsNullOrEmpty(temp))
								user.Phone = temp;

							temp = row[5].ToString().Trim();
							if (!String.IsNullOrEmpty(temp))
								user.IsAdmin = temp.Equals("yes", StringComparison.OrdinalIgnoreCase);

							if (!String.IsNullOrEmpty(user.FirstName) && !String.IsNullOrEmpty(user.LastName))
								users.Add(user);
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				connection.Close();
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
					return result;
				}
				return result;
			});
			if (users.Count > 0)
			{
				//Save XML
				var xml = new StringBuilder();
				xml.AppendLine("<Users>");
				xml.AppendLine("<TotalLists>" + sourceFiles.Length + "</TotalLists>");
				xml.AppendLine("<TotalUsers>" + users.Count + "</TotalUsers>");
				foreach (User user in users)
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

				var xmlPath = Path.Combine(Application.StartupPath, UsersDestinationFileName);
				using (var sw = new StreamWriter(xmlPath, false))
				{
					sw.Write(xml.ToString());
					sw.Flush();
				}

				ProductionFilesUpdateHelper.UpdateProductionFies(xmlPath);

				AppManager.Instance.ShowInformation("Data was updated.");
			}
		}

		private void buttonXUsers_Click(object sender, EventArgs e)
		{
			AppManager.Instance.OpenFile(((FileInfo)((ButtonX)sender).Tag).FullName);
		}
	}
}