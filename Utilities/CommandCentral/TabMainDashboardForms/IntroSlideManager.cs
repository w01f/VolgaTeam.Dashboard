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
	internal class IntroSlideManager
	{
		private const string IntroSlideSourceFileName = @"Data\!Main_Dashboard\Basic Slides Source\Intro Slide.xls";
		private const string IntroSlideDestinationFileName = @"Data\!Main_Dashboard\Basic Slides XML\Intro Slide.xml";


		public const string ButtonText = "Intro\nSlide";

		public static void ViewDataFile()
		{
			AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, IntroSlideDestinationFileName));
		}

		public static void ViewSourceFile()
		{
			AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, IntroSlideSourceFileName));
		}

		public static void UpdateData()
		{
			var headers = new List<SlideHeader>();
			var statements = new List<string>();


			string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, IntroSlideSourceFileName));
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

				//Load Statements
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Statements$]", connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							string statement = row[0].ToString().Trim();
							if (!string.IsNullOrEmpty(statement) && !statement.Contains("*Leadoff"))
								statements.Add(statement);
						}
					statements.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x, y));
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
				xml.AppendLine("<LeadOff>");
				foreach (SlideHeader header in headers)
				{
					xml.Append(@"<SlideHeader ");
					xml.Append("Value = \"" + header.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("IsDefault = \"" + header.IsDefault + "\" ");
					xml.AppendLine(@"/>");
				}
				foreach (string statement in statements)
				{
					xml.Append(@"<Statement ");
					xml.Append("Value = \"" + statement.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.AppendLine(@"/>");
				}
				xml.AppendLine(@"</LeadOff>");

				string xmlPath = Path.Combine(Application.StartupPath, IntroSlideDestinationFileName);
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