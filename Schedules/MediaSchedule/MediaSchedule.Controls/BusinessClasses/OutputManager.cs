using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.MediaSchedule.Controls.BusinessClasses
{
	public class OutputManager
	{
		private const string OneSheetsTableBasedTemplatesFolderName = @"{0}\{1} Slides\tables";
		public const string OneSheetTableBasedTemplateFileName = @"{0}\{1}_programs\{1}-{2}.ppt";
		public static string MasterWizardsRootFolderPath = String.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\ScheduleBuilders", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

		private const string CalendarFileLegendName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\broadcast_cal\broadcast_legend.xls";
		private const string CalendarTemlatesFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\broadcast_cal\broadcast_slides";
		public const string CalendarSlideTemplate = @"Broadcast_{0}_{1}_{2}.pptx";
		public const string CalendarBackgroundFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\broadcast_cal\broadcast_images";
		public const string BackgroundFilePath = @"{0}\{1}";


		public string OneSheetTableBasedTemplatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, String.Format(OneSheetsTableBasedTemplatesFolderName, SettingsManager.Instance.SlideFolder, MediaMetaData.Instance.DataTypeString)); }
		}

		public string CalendarFileLegendPath
		{
			get { return string.Format(CalendarFileLegendName, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}

		public string CalendarTemlatesFolderPath
		{
			get { return string.Format(CalendarTemlatesFolderName, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}

		public string CalendarBackgroundFolderPath
		{
			get { return string.Format(CalendarBackgroundFolderName, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}

		public List<ColorFolder> AvailableColors { get; private set; }

		public OutputManager()
		{
			AvailableColors = new List<ColorFolder>();
			LoadColors();
		}

		private void LoadColors()
		{
			var outputTemplatesFolder = OneSheetTableBasedTemplatesFolderPath;
			if (!Directory.Exists(OneSheetTableBasedTemplatesFolderPath)) return;
			foreach (var directory in Directory.GetDirectories(outputTemplatesFolder))
			{
				var colorFolder = new ColorFolder();
				colorFolder.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Path.GetFileName(directory));
				var imagePath = Path.Combine(directory, "image.png");
				if (File.Exists(imagePath))
					colorFolder.Logo = new Bitmap(imagePath);
				AvailableColors.Add(colorFolder);
			}
		}

		public IEnumerable<BroadcastMonthTemplate> LoadBroadcastMonthTemplates()
		{
			var result = new List<BroadcastMonthTemplate>();
			var connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", CalendarFileLegendPath);
			var connection = new OleDbConnection(connnectionString);
			try
			{
				connection.Open();
			}
			catch
			{
				Utilities.Instance.ShowWarning("Couldn't open legend file");
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

	public class ColorFolder
	{
		public string Name { get; set; }
		public Image Logo { get; set; }
	}
}