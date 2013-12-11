using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using NewBizWiz.Core.Calendar;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.Calendar.Controls.BusinessClasses
{
	public class OutputManager
	{
		private const string CalendarTemlatesFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\{1}";
		private const string CalendarFileLegendName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\FileLegend.xls";
		public const string CalendarBackgroundFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\!Calendar_Images";
		public const string BackgroundFilePath = @"{0}\{1}";
		public static string MasterWizardsRootFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Dashboard", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

		public OutputManager()
		{
			CalendarTemplates = new List<CalendarTemplate>();
		}

		public List<CalendarTemplate> CalendarTemplates { get; set; }

		public string CalendarTemlatesFolderPath
		{
			get { return string.Format(CalendarTemlatesFolderName, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), SettingsManager.Instance.SlideFolder + "new"); }
		}

		public string CalendarFileLegendPath
		{
			get { return string.Format(CalendarFileLegendName, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}

		public string CalendarBackgroundFolderPath
		{
			get { return string.Format(CalendarBackgroundFolderName, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}

		public void LoadCalendarTemplates()
		{
			CalendarTemplates.Clear();
			string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", CalendarFileLegendPath);
			var connection = new OleDbConnection(connnectionString);
			try
			{
				connection.Open();
			}
			catch
			{ }

			if (connection.State == ConnectionState.Open)
			{
				OleDbDataAdapter dataAdapter;
				DataTable dataTable;

				//Load Headers
				dataAdapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connection);
				dataTable = new DataTable();

				try
				{
					dataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
						foreach (DataRow row in dataTable.Rows)
						{
							for (int i = 0; i < 2; i++)
							{
								for (int j = 0; j < 6; j++)
								{
									for (int k = 0; k < 2; k++)
									{
										var template = new CalendarTemplate();
										DateTime tempDate = DateTime.MinValue;
										DateTime.TryParse(row[0].ToString().Trim(), out tempDate);
										template.Month = tempDate.ToString("MMM-yy");
										switch (i)
										{
											case 0:
												template.HasLogo = true;
												break;
											case 1:
												template.HasLogo = false;
												break;
										}
										switch (j)
										{
											case 0:
												template.Color = "Gray";
												break;
											case 1:
												template.Color = "Black";
												break;
											case 2:
												template.Color = "Blue";
												break;
											case 3:
												template.Color = "Green";
												break;
											case 4:
												template.Color = "Orange";
												break;
											case 5:
												template.Color = "Teal";
												break;
										}
										switch (k)
										{
											case 0:
												template.IsLarge = true;
												break;
											case 1:
												template.IsLarge = false;
												break;
										}
										int logoCellNumber = i + 1;
										int colorCellNumber = (j * 2) + 3;
										int sizeCellNumber = colorCellNumber + k;

										if (dataTable.Columns.Count > logoCellNumber)
											if (row[logoCellNumber] != null)
												template.TemplateName = row[logoCellNumber].ToString().Trim();
										if (dataTable.Columns.Count > sizeCellNumber)
											if (row[sizeCellNumber] != null)
												template.SlideMaster = row[sizeCellNumber].ToString().Trim();
										if (!string.IsNullOrEmpty(template.Month) && !string.IsNullOrEmpty(template.TemplateName) && !string.IsNullOrEmpty(template.SlideMaster))
											CalendarTemplates.Add(template);
									}
								}
							}
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
		}

		public string GetSlideName(CalendarOutputData outputData)
		{
			string result = string.Empty;
			var template = CalendarTemplates.FirstOrDefault(x => x.IsLarge == outputData.ShowBigDate && x.HasLogo == outputData.ShowLogo && x.Color.ToLower().Equals(outputData.SlideColor) && x.Month.ToLower().Equals(outputData.Parent.Date.ToString("MMM-yy").ToLower()));
			if (template != null)
				result = template.TemplateName;
			return result;
		}

		public string GetSlideMasterName(CalendarOutputData outputData)
		{
			string result = string.Empty;
			var template = CalendarTemplates.FirstOrDefault(x => x.IsLarge == outputData.ShowBigDate && x.HasLogo == outputData.ShowLogo && x.Color.ToLower().Equals(outputData.SlideColor) && x.Month.ToLower().Equals(outputData.Parent.Date.ToString("MMM-yy").ToLower()));
			if (template != null)
				result = template.SlideMaster;
			return result;
		}
	}

	public class CalendarTemplate
	{
		public CalendarTemplate()
		{
			Month = string.Empty;
			TemplateName = string.Empty;
			SlideMaster = string.Empty;
		}

		public string Month { get; set; }
		public bool HasLogo { get; set; }
		public bool IsLarge { get; set; }
		public string Color { get; set; }
		public string TemplateName { get; set; }
		public string SlideMaster { get; set; }
	}
}