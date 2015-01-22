using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CommandCentral.Entities.Common;
using CommandCentral.InteropClasses;

namespace CommandCentral.TabMainDashboardForms
{
	internal class ClosingSummaryManager
	{
		private const string ClosingSummarySourceFileName = @"Data\!Main_Dashboard\Basic Slides Source\Closing Summary.xls";
		private const string ClosingSummaryDestinationFileName = @"Data\!Main_Dashboard\Basic Slides XML\Closing Summary.xml";

		public const string ButtonText = "Closing\nSummary";

		public static void ViewDataFile()
		{
			AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, ClosingSummaryDestinationFileName));
		}

		public static void ViewSourceFile()
		{
			AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, ClosingSummarySourceFileName));
		}

		public static void UpdateData()
		{
			var headers = new List<SlideHeader>();
			var details = new List<string>();


			string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, ClosingSummarySourceFileName));
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

				//Load Headers
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Headers$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							var title = new SlideHeader();
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
							result = WinAPIHelper.StrCmpLogicalW(x.Value, y.Value);
						return result;
					});
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}

				//Load Details
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Details$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string detail = row[0].ToString().Trim();
							if (!string.IsNullOrEmpty(detail))
								details.Add(detail);
						}
				}
				catch { }
				finally
				{
					dataAdapter.Dispose();
					dataTable.Dispose();
				}
				connection.Close();

				//Save XML
				var xml = new StringBuilder();
				xml.AppendLine("<SimpleSummary>");
				foreach (SlideHeader header in headers)
				{
					xml.Append(@"<SlideHeader ");
					xml.Append("Value = \"" + header.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("IsDefault = \"" + header.IsDefault + "\" ");
					xml.AppendLine(@"/>");
				}
				foreach (string detail in details)
				{
					xml.Append(@"<Detail ");
					xml.Append("Value = \"" + detail.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.AppendLine(@"/>");
				}
				xml.AppendLine(@"</SimpleSummary>");

				string xmlPath = Path.Combine(Application.StartupPath, ClosingSummaryDestinationFileName);
				using (var sw = new StreamWriter(xmlPath, false))
				{
					sw.Write(xml.ToString());
					sw.Flush();
				}

				AppManager.Instance.ShowInformation("Data was updated.");
			}
		}
	}
}