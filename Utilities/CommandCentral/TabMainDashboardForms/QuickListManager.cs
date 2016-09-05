using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CommandCentral.Entities.Online;

namespace CommandCentral.TabMainDashboardForms
{
	internal class QuickListManager
	{
		private const string SourceFileName = @"Data\!Main_Dashboard\QuickList Source\quick list.xls";
		private const string DestinationFileName = @"Data\!Main_Dashboard\QuickList XML\quick list.xml";

		public const string ButtonText = "Quick List\nData";

		private static List<Category> _categories = new List<Category>();

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
			var print = new List<string>();
			var digital = new List<string>();
			var tv = new List<string>();
			var radio = new List<string>();
			string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, SourceFileName));
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

				//Load Print
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Print$]", connection);
				dataTable = new DataTable();
				print.Clear();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string temp = row[0].ToString().Trim();
							if (!string.IsNullOrEmpty(temp))
								print.Add(temp);
						}
				}
				catch {}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Digital
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Digital$]", connection);
				dataTable = new DataTable();
				digital.Clear();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string temp = row[0].ToString().Trim();
							if (!string.IsNullOrEmpty(temp))
								digital.Add(temp);
						}
				}
				catch {}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load TV
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [TV$]", connection);
				dataTable = new DataTable();
				tv.Clear();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string temp = row[0].ToString().Trim();
							if (!string.IsNullOrEmpty(temp))
								tv.Add(temp);
						}
				}
				catch {}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Radio
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Radio$]", connection);
				dataTable = new DataTable();
				radio.Clear();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string temp = row[0].ToString().Trim();
							if (!string.IsNullOrEmpty(temp))
								radio.Add(temp);
						}
				}
				catch {}
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				connection.Close();
			}
			//Save XML
			var xml = new StringBuilder();
			xml.AppendLine("<QuickList>");
			foreach (string record in print)
			{
				xml.Append(@"<Print ");
				xml.Append("Value = \"" + record.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (string record in digital)
			{
				xml.Append(@"<Digital ");
				xml.Append("Value = \"" + record.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (string record in tv)
			{
				xml.Append(@"<TV ");
				xml.Append("Value = \"" + record.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (string record in radio)
			{
				xml.Append(@"<Radio ");
				xml.Append("Value = \"" + record.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			xml.AppendLine(@"</QuickList>");

			var xmlPath = Path.Combine(Application.StartupPath, DestinationFileName);
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