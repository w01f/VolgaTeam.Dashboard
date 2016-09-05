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
	class ROIFormulaManager
	{
		private const string ROIFormulaSourceFileName = @"Data\!App_Data\app_sales_PRO_Source\ROI Formula.xls";
		private const string ROIFormulaDestinationFileName = @"Data\!App_Data\app_sales_PRO_XML\ROI Formula.xml";

		public const string ButtonText = "ROI\nFormula";

		public static void ViewDataFile()
		{
			AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, ROIFormulaDestinationFileName));
		}

		public static void ViewSourceFile()
		{
			AppManager.Instance.OpenFile(Path.Combine(Application.StartupPath, ROIFormulaSourceFileName));
		}

		public static void UpdateData()
		{
			List<SlideHeader> headers = new List<SlideHeader>();

			string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, ROIFormulaSourceFileName));
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
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Headers$]", connection);
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

				connection.Close();

				//Save XML
				StringBuilder xml = new StringBuilder();
				xml.AppendLine("<ROIFormula>");
				foreach (SlideHeader header in headers)
				{
					xml.Append(@"<SlideHeader ");
					xml.Append("Value = \"" + header.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("IsDefault = \"" + header.IsDefault + "\" ");
					xml.AppendLine(@"/>");
				}
				xml.AppendLine(@"</ROIFormula>");

				var xmlPath = Path.Combine(Application.StartupPath, ROIFormulaDestinationFileName);
				using (StreamWriter sw = new StreamWriter(xmlPath, false))
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
