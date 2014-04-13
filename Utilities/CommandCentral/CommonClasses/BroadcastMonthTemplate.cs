using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace CommandCentral.CommonClasses
{
	public class BroadcastMonthTemplate
	{
		public DateTime? Month { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<BroadcastMonthTemplate>");
			result.AppendLine(@"<Month>" + Month + @"</Month>");
			result.AppendLine(@"<StartDate>" + StartDate + @"</StartDate>");
			result.AppendLine(@"<EndDate>" + EndDate + @"</EndDate>");
			result.AppendLine(@"</BroadcastMonthTemplate>");
			return result.ToString();
		}

		public static IEnumerable<BroadcastMonthTemplate> Load(string path)
		{
			var result = new List<BroadcastMonthTemplate>();
			var connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", path);
			var connection = new OleDbConnection(connnectionString);
			try
			{
				connection.Open();
			}
			catch
			{
				AppManager.Instance.ShowInformation("Couldn't open legend file");
				return result;
			}
			if (connection.State != ConnectionState.Open) return result;
			var yearPages = new List<string>();
			var dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
			yearPages.AddRange(dataTable.Rows.OfType<DataRow>().Select(r => r["TABLE_NAME"].ToString().Replace("$", "").Replace('"'.ToString(), "'").Replace("'", "")));
			foreach (var yearPage in yearPages)
			{
				int year;
				if (!Int32.TryParse(yearPage, out year)) continue;
				var dataAdapter = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}$]", yearPage), connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					BroadcastMonthTemplate broadcastMonth = null;
					foreach (var row in dataTable.Rows.OfType<DataRow>())
					{
						var monthName = row[0].ToString();
						var weekNumber = row[1].ToString();
						if (!String.IsNullOrEmpty(monthName))
						{
							if (broadcastMonth != null && broadcastMonth.Month.HasValue && broadcastMonth.StartDate.HasValue && broadcastMonth.EndDate.HasValue)
								result.Add(broadcastMonth);
							broadcastMonth = new BroadcastMonthTemplate();
							switch (monthName.Replace("*", "").ToUpper())
							{
								case "JAN":
									broadcastMonth.Month = new DateTime(year, 1, 1);
									break;
								case "FEB":
									broadcastMonth.Month = new DateTime(year, 2, 1);
									break;
								case "MAR":
									broadcastMonth.Month = new DateTime(year, 3, 1);
									break;
								case "APR":
									broadcastMonth.Month = new DateTime(year, 4, 1);
									break;
								case "MAY":
									broadcastMonth.Month = new DateTime(year, 5, 1);
									break;
								case "JUN":
									broadcastMonth.Month = new DateTime(year, 6, 1);
									break;
								case "JUL":
									broadcastMonth.Month = new DateTime(year, 7, 1);
									break;
								case "AUG":
									broadcastMonth.Month = new DateTime(year, 8, 1);
									break;
								case "SEP":
									broadcastMonth.Month = new DateTime(year, 9, 1);
									break;
								case "OCT":
									broadcastMonth.Month = new DateTime(year, 10, 1);
									break;
								case "NOV":
									broadcastMonth.Month = new DateTime(year, 11, 1);
									break;
								case "DEC":
									broadcastMonth.Month = new DateTime(year, 12, 1);
									break;
							}
							DateTime date;
							if (DateTime.TryParse(row[2].ToString(), out date))
								broadcastMonth.StartDate = date;
						}
						else if (!String.IsNullOrEmpty(weekNumber))
						{
							DateTime date;
							if (DateTime.TryParse(row[8].ToString(), out date))
								broadcastMonth.EndDate = date;
						}
						else break;
					}
					if (broadcastMonth != null && broadcastMonth.Month.HasValue && broadcastMonth.StartDate.HasValue && broadcastMonth.EndDate.HasValue)
						result.Add(broadcastMonth);
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
			return result;
		}
	}
}
