using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommandCentral.Entities.SalesDepot;

namespace CommandCentral.TabSalesDepotForms
{
	internal class SalesDepotSearchManager
	{
		private const string SalesDepotSearchSourceFileName = @"Data\!Main_Dashboard\SDSearch Source\SDSearch.xls";
		private const string SalesDepotSearchDestinationFileName = @"Data\!Main_Dashboard\SDSearch XML\SDSearch.xml";

		public const string ButtonText = "Sales Depot\nSearch";

		private static readonly List<SearchGroup> _categories = new List<SearchGroup>();

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
					var searchGroup = new SearchGroup();
					searchGroup.Name = row["TABLE_NAME"].ToString().Replace("$", "").Replace('"'.ToString(), "'").Replace("'", "").Replace("#", ".");

					if (!searchGroup.Name.Trim().Equals("Categories") && !searchGroup.Name.Trim().Equals("Settings"))
						_categories.Add(searchGroup);
				}
			}
			catch { }
		}

		public static void UpdateData()
		{
			var searchGroups = new List<SearchGroup>();
			var maxTags = 0;
			var tagCount = false;

			string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, SalesDepotSearchSourceFileName));
			var connection = new OleDbConnection(connnectionString);
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
				bool tempBool;
				int tempInt;

				//Load Groups
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Settings$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					foreach (DataRow row in dataTable.Rows)
					{
						if (row[0].ToString().Equals("Max Tags") && int.TryParse(row[1].ToString(), out tempInt))
							maxTags = tempInt;
						else if (row[0].ToString().Equals("Tag Count") && bool.TryParse(row[1].ToString(), out tempBool))
							tagCount = tempBool;
					}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

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
							SearchGroup searchGroup = _categories.Where(x => x.Name.Equals(groupName)).FirstOrDefault();
							if (searchGroup != null)
								searchGroup.Description = row[1].ToString().Trim();
							searchGroups.Add(searchGroup);
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Tags
				foreach (SearchGroup searchGroup in searchGroups)
				{
					searchGroup.Tags.Clear();
					dataAdapter = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}$]", searchGroup.Name.Replace(".", "#")), connection);
					dataTable = new DataTable();
					try
					{
						dataAdapter.Fill(dataTable);
						if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
							foreach (DataRow row in dataTable.Rows)
							{
								string value = row[0].ToString().Trim();
								if (!string.IsNullOrEmpty(value))
									searchGroup.Tags.Add(value);
							}
					}
					catch { }
					finally
					{
						dataAdapter.Dispose();
						dataTable.Dispose();
					}
				}
				connection.Close();

				//Save XML
				var xml = new StringBuilder();
				xml.AppendLine("<SDSearch>");
				xml.AppendLine("<MaxTags>" + maxTags + "</MaxTags>");
				xml.AppendLine("<TagCount>" + tagCount + "</TagCount>");
				foreach (SearchGroup group in searchGroups)
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

				var xmlPath = Path.Combine(Application.StartupPath, SalesDepotSearchDestinationFileName);
				using (var sw = new StreamWriter(xmlPath, false))
				{
					sw.Write(xml.ToString());
					sw.Flush();
				}

				ProductionFilesUpdateHelper.UpdateProductionFies(xmlPath);

				AppManager.Instance.ShowInformation("Data was updated.");
			}
		}
	}
}