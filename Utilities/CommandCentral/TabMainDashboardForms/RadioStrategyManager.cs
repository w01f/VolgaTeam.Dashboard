using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Enums;
using CommandCentral.Entities.Common;
using CommandCentral.Entities.Media;
using CommandCentral.InteropClasses;

namespace CommandCentral.TabMainDashboardForms
{
	internal class RadioStrategyManager
	{
		private const string SourceFileName = @"Data\!Main_Dashboard\Radio Source\Radio Strategy.xls";
		private const string TemplatesFilePath = @"Data\!Main_Dashboard\Radio Source\broadcast_legend.xls";
		private const string DestinationFileName = @"Data\!Main_Dashboard\Radio XML\Radio Strategy.xml";
		private const string ImageSourceFolder = @"Data\!Main_Dashboard\Radio Source\Radio Images";

		public const string ButtonText = "Radio Strategy\nData";

		private static readonly List<NameCodePair> _dayparts = new List<NameCodePair>();
		private static readonly List<RadioProgram> _programs = new List<RadioProgram>();

		private static void GetDayparts(OleDbConnection connection)
		{
			try
			{
				_dayparts.Clear();
				var dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
				foreach (DataRow row in dataTable.Rows)
				{
					var daypart = new NameCodePair();
					daypart.Name = row["TABLE_NAME"].ToString().Replace("$", "").Replace('"'.ToString(), "'").Replace("'", "");

					if (!new[]
					{
						"Headers-Positioning Point",
						"Length",
						"Dayparts",
						"Stations",
						"Client Type",
						"Date",
						"Custom Demos",
						"Sources",
						"File-Status",
						"Toggle Defaults"
					}.Contains(daypart.Name.Trim()))
						_dayparts.Add(daypart);
				}
			}
			catch
			{
			}
		}

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
			var slideHeaders = new List<SlideHeader>();
			var positioningPoints = new List<string>();
			var lenghts = new List<string>();
			var clientTypes = new List<string>();
			var flexFlightDatesAllowed = false;
			var customDemos = new List<string>();
			var statuses = new List<SlideHeader>();
			var stations = new List<Station>();
			var broadcastTemplates = new List<BroadcastMonthTemplate>();
			var defaultWeeklyScheduleSettings = new ScheduleSectionSettings();
			var defaultMonthlyScheduleSettings = new ScheduleSectionSettings();
			var defaultSnapshotSettings = new SnapshotSettings();
			var defaultSnapshotSummarySettings = new SnapshotSummarySettings();
			var defaultOptionsSettings = new OptionsSettings();
			var defaultOptionsSummarySettings = new OptionsSummarySettings();
			var defaultBroadcastCalendarSettings = new CalendarToggleSettings();
			var defaultCustomCalendarSettings = new CalendarToggleSettings();

			var connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", Path.Combine(Application.StartupPath, SourceFileName));
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

			if (connection.State != ConnectionState.Open) return;
			OleDbDataAdapter dataAdapter;
			DataTable dataTable;

			//Load Headers And Positioning Points
			dataAdapter = new OleDbDataAdapter("SELECT * FROM [Headers-Positioning Point$]", connection);
			dataTable = new DataTable();

			bool loadHeaders = true;
			bool loadPositioningPoint = false;
			slideHeaders.Clear();
			positioningPoints.Clear();
			try
			{
				dataAdapter.Fill(dataTable);
				if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
					foreach (DataRow row in dataTable.Rows)
					{
						if (row[0].ToString().Trim().Equals("*Positioning Point"))
						{
							loadHeaders = false;
							loadPositioningPoint = true;
							continue;
						}

						if (loadHeaders)
						{
							var title = new SlideHeader();
							title.Value = row[0].ToString().Trim();
							if (dataTable.Columns.Count > 1)
								if (row[1] != null)
									title.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
							if (!string.IsNullOrEmpty(title.Value))
								slideHeaders.Add(title);
						}

						if (loadPositioningPoint)
						{
							var positioningPoint = row[0].ToString().Trim();
							if (!string.IsNullOrEmpty(positioningPoint))
								positioningPoints.Add(positioningPoint);
						}
					}

				slideHeaders.Sort((x, y) =>
				{
					int result = y.IsDefault.CompareTo(x.IsDefault);
					if (result == 0)
						result = 1;
					return result;
				});
				positioningPoints.Sort(WinAPIHelper.StrCmpLogicalW);
			}
			catch
			{
			}
			finally
			{
				dataAdapter.Dispose();
				dataTable.Dispose();
			}

			//Load Statuses
			dataAdapter = new OleDbDataAdapter("SELECT * FROM [File-Status$]", connection);
			dataTable = new DataTable();
			statuses.Clear();
			try
			{
				dataAdapter.Fill(dataTable);
				if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
					foreach (DataRow row in dataTable.Rows)
					{
						var status = new SlideHeader();
						status.Value = row[0].ToString().Trim();
						if (dataTable.Columns.Count > 1)
							if (row[1] != null)
								status.IsDefault = row[1].ToString().Trim().ToLower().Equals("d");
						if (!string.IsNullOrEmpty(status.Value))
							statuses.Add(status);
					}

				statuses.Sort((x, y) =>
				{
					int result = y.IsDefault.CompareTo(x.IsDefault);
					if (result == 0)
						result = WinAPIHelper.StrCmpLogicalW(x.Value, y.Value);
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

			//Load Lenghts
			lenghts.Clear();
			dataAdapter = new OleDbDataAdapter("SELECT * FROM [Length$]", connection);
			dataTable = new DataTable();
			try
			{
				dataAdapter.Fill(dataTable);
				if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
					foreach (DataRow row in dataTable.Rows)
					{
						string lenght = row[0].ToString().Trim();
						if (!string.IsNullOrEmpty(lenght))
							lenghts.Add(lenght);
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

			//Load Client Types
			dataAdapter = new OleDbDataAdapter("SELECT * FROM [Client Type$]", connection);
			dataTable = new DataTable();
			clientTypes.Clear();
			try
			{
				dataAdapter.Fill(dataTable);
				if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
					foreach (DataRow row in dataTable.Rows)
					{
						var clientType = row[0].ToString().Trim();
						if (!string.IsNullOrEmpty(clientType))
							clientTypes.Add(clientType);
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

			//Load Date Settings
			dataAdapter = new OleDbDataAdapter("SELECT * FROM [Date$]", connection);
			dataTable = new DataTable();
			try
			{
				dataAdapter.Fill(dataTable);
				if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
					foreach (DataRow row in dataTable.Rows)
					{
						bool temp;
						if (row[1] != null && Boolean.TryParse(row[1].ToString().Trim(), out temp))
							flexFlightDatesAllowed = temp;
						break;
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

			//Load Dayparts
			GetDayparts(connection);
			dataAdapter = new OleDbDataAdapter("SELECT * FROM [Dayparts$]", connection);
			dataTable = new DataTable();
			try
			{
				var rowIndex = 0;
				dataAdapter.Fill(dataTable);
				if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 1)
					foreach (DataRow row in dataTable.Rows)
					{
						string code = row[1].ToString().Trim();
						var daypart = _dayparts.FirstOrDefault(x => x.Name.Equals(code));
						if (daypart != null)
						{
							daypart.Code = row[0].ToString().Trim();
							daypart.Index = rowIndex;
						}
						rowIndex++;
					}
				_dayparts.Sort((x, y) => x.Index.CompareTo(y.Index));
			}
			catch
			{
			}
			finally
			{
				dataAdapter.Dispose();
				dataTable.Dispose();
			}

			//Load Custom Demos
			dataAdapter = new OleDbDataAdapter("SELECT * FROM [Custom Demos$]", connection);
			dataTable = new DataTable();
			customDemos.Clear();
			try
			{
				dataAdapter.Fill(dataTable);
				if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
					foreach (DataRow row in dataTable.Rows)
					{
						var customDemo = row[0].ToString().Trim();
						if (!string.IsNullOrEmpty(customDemo))
							customDemos.Add(customDemo);
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

			//Load Stations
			dataAdapter = new OleDbDataAdapter("SELECT * FROM [Stations$]", connection);
			dataTable = new DataTable();
			stations.Clear();
			try
			{
				dataAdapter.Fill(dataTable);
				if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 1)
					foreach (DataRow row in dataTable.Rows)
					{
						var station = new Station();
						station.Name = row[0].ToString().Trim();
						string filePath = Path.Combine(Application.StartupPath, ImageSourceFolder, row[1].ToString().Trim());
						if (File.Exists(filePath))
							station.Logo = new Bitmap(filePath);
						if (!string.IsNullOrEmpty(station.Name))
							stations.Add(station);
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

			//Load View Settings
			dataAdapter = new OleDbDataAdapter("SELECT * FROM [Toggle Defaults$]", connection);
			dataTable = new DataTable();
			try
			{
				dataAdapter.Fill(dataTable);
				if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 1)
				{
					var header = "Weekly Schedule";
					foreach (DataRow row in dataTable.Rows)
					{
						var rowTag = row[0].ToString().Trim();
						var rowValue = row[1].ToString().Trim();
						if (rowTag.StartsWith("*"))
							header = rowTag.Replace("*", String.Empty);
						else if (!String.IsNullOrEmpty(rowTag))
						{
							switch (header)
							{
								case "Weekly Schedule":
									defaultWeeklyScheduleSettings.ApplyValue(rowTag, rowValue);
									break;
								case "Monthly Schedule":
									defaultMonthlyScheduleSettings.ApplyValue(rowTag, rowValue);
									break;
								case "Snapshot-Tab":
									defaultSnapshotSettings.ApplyValue(rowTag, rowValue);
									break;
								case "Snapshot-Summary Slide":
									defaultSnapshotSummarySettings.ApplyValue(rowTag, rowValue);
									break;
								case "Options-Tab":
									defaultOptionsSettings.ApplyValue(rowTag, rowValue);
									break;
								case "Options-Summary Slide":
									defaultOptionsSummarySettings.ApplyValue(rowTag, rowValue);
									break;
								case "Calendar1":
									defaultBroadcastCalendarSettings.ApplyValue(rowTag, rowValue);
									break;
								case "Calendar2":
									defaultCustomCalendarSettings.ApplyValue(rowTag, rowValue);
									break;
							}
						}
						else
							break;
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

			//Load Radio Programs
			_programs.Clear();
			connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=No;IMEX=1"";", Path.Combine(Application.StartupPath, SourceFileName));
			connection = new OleDbConnection(connnectionString);
			try
			{
				connection.Open();
			}
			catch
			{
				AppManager.Instance.ShowWarning("Couldn't open source file");
				return;
			}
			if (connection.State != ConnectionState.Open) return;
			foreach (var daypart in _dayparts)
			{
				dataAdapter = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}$]", daypart.Name), connection);
				dataTable = new DataTable();
				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count >= 4)
						foreach (var row in dataTable.Rows.OfType<DataRow>().Skip(3))
						{
							var program = new RadioProgram();
							program.Station = row[0].ToString().Trim();
							program.Name = row[1].ToString().Trim();
							program.Day = row[2].ToString().Trim();
							program.Time = row[3].ToString().Trim();
							program.Daypart = daypart.Code;
							for (int i = 4; i < 44; i++)
							{
								if (dataTable.Columns.Count <= i) continue;
								if (row[i] == null) continue;
								var demo = new Demo();
								demo.Source = dataTable.Rows[0][i].ToString().Trim();
								demo.DemoType = dataTable.Rows[1][i].ToString().Trim().ToUpper().Equals("IMP") ? DemoType.Imp : DemoType.Rtg;
								demo.Name = dataTable.Rows[2][i].ToString().Trim();
								demo.Value = row[i].ToString().Trim();
								if (!string.IsNullOrEmpty(demo.Name) && !string.IsNullOrEmpty(demo.Value))
									program.Demos.Add(demo);
							}
							if (!string.IsNullOrEmpty(program.Name))
								_programs.Add(program);
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

			broadcastTemplates.AddRange(BroadcastMonthTemplate.Load(Path.Combine(Application.StartupPath, TemplatesFilePath)));

			//Save XML
			var xml = new StringBuilder();
			xml.AppendLine("<RadioStrategy>");
			foreach (var header in slideHeaders)
			{
				xml.Append(@"<SlideHeader ");
				xml.Append("Value = \"" + header.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("IsDefault = \"" + header.IsDefault + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (var status in statuses)
			{
				xml.Append(@"<Status ");
				xml.Append("Value = \"" + status.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (var positionPoint in positioningPoints)
			{
				xml.Append(@"<Statement ");
				xml.Append("Value = \"" + positionPoint.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (var lenght in lenghts)
			{
				xml.Append(@"<Lenght ");
				xml.Append("Value = \"" + lenght.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (var daypart in _dayparts)
			{
				xml.Append(@"<Daypart ");
				xml.Append("Name = \"" + daypart.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Code = \"" + daypart.Code.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			var converter = TypeDescriptor.GetConverter(typeof(Bitmap));
			foreach (var station in stations)
			{
				xml.Append(@"<Station ");
				xml.Append("Name = \"" + station.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Logo = \"" + Convert.ToBase64String((byte[])converter.ConvertTo(station.Logo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (var clientType in clientTypes)
			{
				xml.Append(@"<ClientType ");
				xml.Append("Value = \"" + clientType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			xml.AppendLine(String.Format(@"<FlexFlightDatesAllowed>{0}</FlexFlightDatesAllowed> ", flexFlightDatesAllowed));
			foreach (var customDemo in customDemos)
			{
				xml.Append(@"<CustomDemo ");
				xml.Append("Value = \"" + customDemo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@"/>");
			}
			foreach (var program in _programs)
			{
				xml.Append(@"<Program ");
				xml.Append("Name = \"" + program.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Station = \"" + program.Station.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Daypart = \"" + program.Daypart.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Day = \"" + program.Day.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.Append("Time = \"" + program.Time.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
				xml.AppendLine(@">");
				foreach (var demo in program.Demos)
				{
					xml.Append(@"<Demo ");
					xml.Append("Source = \"" + demo.Source.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("DemoType = \"" + (int)demo.DemoType + "\" ");
					xml.Append("Name = \"" + demo.Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.Append("Value = \"" + demo.Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
					xml.AppendLine(@"/>");
				}
				xml.AppendLine(@"</Program>");
			}
			broadcastTemplates.ForEach(bt => xml.AppendLine(bt.Serialize()));

			xml.AppendLine(@"<DefaultWeeklyScheduleSettings>" + defaultWeeklyScheduleSettings.Serialize() + @"</DefaultWeeklyScheduleSettings>");
			xml.AppendLine(@"<DefaultMonthlyScheduleSettings>" + defaultMonthlyScheduleSettings.Serialize() + @"</DefaultMonthlyScheduleSettings>");
			xml.AppendLine(@"<DefaultSnapshotSettings>" + defaultSnapshotSettings.Serialize() + @"</DefaultSnapshotSettings>");
			xml.AppendLine(@"<DefaultSnapshotSummarySettings>" + defaultSnapshotSummarySettings.Serialize() + @"</DefaultSnapshotSummarySettings>");
			xml.AppendLine(@"<DefaultOptionsSettings>" + defaultOptionsSettings.Serialize() + @"</DefaultOptionsSettings>");
			xml.AppendLine(@"<DefaultOptionsSummarySettings>" + defaultOptionsSummarySettings.Serialize() + @"</DefaultOptionsSummarySettings>");
			xml.AppendLine(@"<DefaultBroadcastCalendarSettings>" + defaultBroadcastCalendarSettings.Serialize() + @"</DefaultBroadcastCalendarSettings>");
			xml.AppendLine(@"<DefaultCustomCalendarSettings>" + defaultCustomCalendarSettings.Serialize() + @"</DefaultCustomCalendarSettings>");

			xml.AppendLine(@"</RadioStrategy>");

			string xmlPath = Path.Combine(Application.StartupPath, DestinationFileName);
			using (var sw = new StreamWriter(xmlPath, false))
			{
				sw.Write(xml.ToString());
				sw.Flush();
			}

			AppManager.Instance.ShowInformation("Data was updated.");
		}
	}
}