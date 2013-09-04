using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.BusinessClasses
{
	public class OutputManager
	{
		private const string ExcelOutputTemplateFileName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\ExcelOutput{1}\Newspaper Slides\Print Schedule Formats{2}.xls";

		public const int Columns = 12;

		public const string DetailedGridTemplateSheetNameWithNotes = @"Detailed Grid-With AdNotes";
		public const int DetailedGridExcelBasedRowsCountWithNotes = 10;
		public const string DetailedGridTemplateSheetNameWithoutNotes = @"Detailed Grid-No AdNotes";
		public const int DetailedGridExcelBasedRowsCountWithoutNotes = 14;
		public const int DetailedGridGridBasedRowsCountWithNotes = 8;
		public const int DetailedGridGridBasedRowsCountWithoutNotes = 9;

		public const string MultiGridTemplateSheetNameWithNotes = @"Multi Grid-With AdNotes";
		public const int MultiGridExcelBasedRowsCountWithNotes = 8;
		public const string MultiGridTemplateSheetNameWithoutNotes = @"Multi Grid-No AdNotes";
		public const int MultiGridExcelBasedRowsCountWithoutNotes = 8;
		public const int MultiGridGridBasedRowsCountWithNotes = 8;
		public const int MultiGridGridBasedRowsCountWithoutNotes = 9;

		private const string BasicOverviewTemlatesFolderName = @"{0}\Newspaper Slides\basic overview";
		public const string BasicOverviewSlideTemplate = @"basic-{0}.ppt";

		private const string MultiSummaryTemlatesFolderName = @"{0}\Newspaper Slides\multi summary";
		public const string MultiSummarySlideTemplate = @"summary-{0}.ppt";

		private const string SnapshotTemlatesFolderName = @"{0}\Newspaper Slides\snapshotnew";

		private const string AdPlanTemlatesFolderName = @"{0}\Newspaper Slides\adplan";

		private const string DetailedGridExcelBasedTemlatesFolderName = @"{0}\Newspaper Slides\detailed grid";
		private const string DetailedGridGridBasedTemlatesFolderName = @"{0}\Newspaper Slides\tables";
		public const string DetailedGridExcelBasedSlideTemplate = @"detailed-{0}.ppt";
		public const string DetailedGridGridBasedSlideTemplate = @"{0} columns_detailed\tables_{1}\table{2}_{1}.ppt";

		private const string MultiGridExcelBasedTemlatesFolderName = @"{0}\Newspaper Slides\multi grid";
		private const string MultiGridGridBasedTemlatesFolderName = @"{0}\Newspaper Slides\tables";
		public const string MultiGridExcelBasedSlideTemplate = @"multi-{0}-{1}.ppt";
		public const string MultiGridGridBasedSlideTemplate = @"{0} columns_multi\tables_{1}_logos\table{2}_{1}.ppt";

		private const string ChronoGridExcelBasedTemlatesFolderName = @"{0}\Newspaper Slides\chrono grid";
		private const string ChronoGridGridBasedTemlatesFolderName = @"{0}\Newspaper Slides\tables";
		public const string ChronoGridExcelbasedSlideTemplate = @"chrono-{0}.ppt";
		public const string ChronoGridGridBasedSlideTemplate = @"{0} columns_chrono\tables_{1}\table{2}_{1}.ppt";

		private const string CalendarTemlatesFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\{1}";
		private const string CalendarFileLegendName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\FileLegend.xls";
		public const string CalendarSlideTemplate = @"{0}\{1}";
		public const string CalendarBackgroundFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\!Calendar_Images";
		public const string BackgroundFilePath = @"{0}\{1}";
		public static string MasterWizardsRootFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Dashboard", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

		public OutputManager()
		{
			CalendarTemplates = new List<CalendarTemplate>();
		}

		public List<CalendarTemplate> CalendarTemplates { get; set; }

		public string ExcelOutputTemplateFilePath
		{
			get { return string.Format(ExcelOutputTemplateFileName, new object[] { Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), AdSchedulePowerPointHelper.Instance.Is2003 ? "03" : "07", AdSchedulePowerPointHelper.Instance.Is2003 ? "03" : "07" }); }
		}

		public string BasicOverviewTemlatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(BasicOverviewTemlatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string MultiSummaryTemlatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(MultiSummaryTemlatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string SnapshotTemlatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(SnapshotTemlatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string AdPlanTemlatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(AdPlanTemlatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string DetailedGridExcelBasedTemlatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(DetailedGridExcelBasedTemlatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string DetailedGridGridBasedTemlatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(DetailedGridGridBasedTemlatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string MultiGridExcelBasedTemlatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(MultiGridExcelBasedTemlatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string MultiGridGridBasedTemlatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(MultiGridGridBasedTemlatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string ChronoGridExcelBasedTemlatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(ChronoGridExcelBasedTemlatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string ChronoGridGridBasedTemlatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(ChronoGridGridBasedTemlatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string CalendarTemlatesFolderPath
		{
			get { return string.Format(CalendarTemlatesFolderName, new object[] { Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), SettingsManager.Instance.SlideFolder + "new" }); }
		}

		public string CalendarFileLegendPath
		{
			get { return string.Format(CalendarFileLegendName, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}

		public string CalendarBackgroundFolderPath
		{
			get { return string.Format(CalendarBackgroundFolderName, new object[] { Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), SettingsManager.Instance.SlideFolder }); }
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
			{
				Utilities.Instance.ShowWarning("Couldn't open legend file");
				return;
			}

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