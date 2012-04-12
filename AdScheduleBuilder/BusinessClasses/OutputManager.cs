using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

namespace AdScheduleBuilder.BusinessClasses
{
    class OutputManager
    {
        public static string MasterWizardsRootFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Dashboard", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

        private const string ExcelOutputTemplateFileName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\ExcelOutput{1}\Newspaper Slides\Print Schedule Formats{2}.xls";

        public const int Columns = 12;

        public const string DetailedGridTemplateSheetNameWithNotes = @"Detailed Grid-With AdNotes";
        public const int DetailedGridExcelBasedRowsCountWithNotes = 10;
        public const string DetailedGridTemplateSheetNameWithoutNotes = @"Detailed Grid-No AdNotes";
        public const int DetailedGridExcelBasedRowsCountWithoutNotes = 14;
        public const int DetailedGridGridBasedRowsCountWithNotes = 8;
        public const int DetailedGridGridBasedRowsCountWithoutNotes = 10;

        public const string MultiGridTemplateSheetNameWithNotes = @"Multi Grid-With AdNotes";
        public const int MultiGridExcelBasedRowsCountWithNotes = 8;
        public const string MultiGridTemplateSheetNameWithoutNotes = @"Multi Grid-No AdNotes";
        public const int MultiGridExcelBasedRowsCountWithoutNotes = 8;
        public const int MultiGridGridBasedRowsCountWithNotes = 8;
        public const int MultiGridGridBasedRowsCountWithoutNotes = 10;

        public const string ChronoGridTemplateSheetNameWithNotes = @"Chrono Grid-With AdNotes";
        public const int ChronoGridExcelBasedRowsCountWithNotes = 10;
        public const string ChronoGridTemplateSheetNameWithoutNotes = @"Chrono Grid-No AdNotes";
        public const int ChronoGridExcelBasedRowsCountWithoutNotes = 14;
        public const int ChronoGridGridBasedRowsCountWithNotes = 8;
        public const int ChronoGridGridBasedRowsCountWithoutNotes = 10;

        private const string BasicOverviewTemlatesFolderName = @"{0}\Newspaper Slides\basic overview";
        public const string BasicOverviewSlideTemplate = @"basic-{0}.ppt";

        private const string MultiSummaryTemlatesFolderName = @"{0}\Newspaper Slides\multi summary";
        public const string MultiSummarySlideTemplate = @"summary-{0}.ppt";

        private const string SnapshotTemlatesFolderName = @"{0}\Newspaper Slides\snapshot";
        public const string SnapshotSlideTemplate = @"snapshot-{0}.ppt";

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
        public const string CalendarBackgroundFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\!Excel_Calendars";
        public const string ExcelBackgroundFileName = @"{0}\{1}{0}.xls";

        private static OutputManager _instance = new OutputManager();

        public List<CalendarTemplate> CalendarTemplates { get; set; }

        public static OutputManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public string ExcelOutputTemplateFilePath
        {
            get
            {
                return string.Format(ExcelOutputTemplateFileName, new object[] { System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles), InteropClasses.PowerPointHelper.Instance.Is2003 ? "03" : "07", InteropClasses.PowerPointHelper.Instance.Is2003 ? "03" : "07" });
            }
        }

        public string BasicOverviewTemlatesFolderPath
        {
            get
            {
                return Path.Combine(MasterWizardsRootFolderPath, ConfigurationClasses.SettingsManager.Instance.SelectedWizard, string.Format(BasicOverviewTemlatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string MultiSummaryTemlatesFolderPath
        {
            get
            {
                return Path.Combine(MasterWizardsRootFolderPath, ConfigurationClasses.SettingsManager.Instance.SelectedWizard, string.Format(MultiSummaryTemlatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string SnapshotTemlatesFolderPath
        {
            get
            {
                return Path.Combine(MasterWizardsRootFolderPath, ConfigurationClasses.SettingsManager.Instance.SelectedWizard, string.Format(SnapshotTemlatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string DetailedGridExcelBasedTemlatesFolderPath
        {
            get
            {
                return Path.Combine(MasterWizardsRootFolderPath, ConfigurationClasses.SettingsManager.Instance.SelectedWizard, string.Format(DetailedGridExcelBasedTemlatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string DetailedGridGridBasedTemlatesFolderPath
        {
            get
            {
                return Path.Combine(MasterWizardsRootFolderPath, ConfigurationClasses.SettingsManager.Instance.SelectedWizard, string.Format(DetailedGridGridBasedTemlatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string MultiGridExcelBasedTemlatesFolderPath
        {
            get
            {
                return Path.Combine(MasterWizardsRootFolderPath, ConfigurationClasses.SettingsManager.Instance.SelectedWizard, string.Format(MultiGridExcelBasedTemlatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string MultiGridGridBasedTemlatesFolderPath
        {
            get
            {
                return Path.Combine(MasterWizardsRootFolderPath, ConfigurationClasses.SettingsManager.Instance.SelectedWizard, string.Format(MultiGridGridBasedTemlatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string ChronoGridExcelBasedTemlatesFolderPath
        {
            get
            {
                return Path.Combine(MasterWizardsRootFolderPath, ConfigurationClasses.SettingsManager.Instance.SelectedWizard, string.Format(ChronoGridExcelBasedTemlatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string ChronoGridGridBasedTemlatesFolderPath
        {
            get
            {
                return Path.Combine(MasterWizardsRootFolderPath, ConfigurationClasses.SettingsManager.Instance.SelectedWizard, string.Format(ChronoGridGridBasedTemlatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string CalendarTemlatesFolderPath
        {
            get
            {
                return string.Format(CalendarTemlatesFolderName, new object[] { System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles), ConfigurationClasses.SettingsManager.Instance.SlideFolder + "new" });
            }
        }

        public string CalendarFileLegendPath
        {
            get
            {
                return string.Format(CalendarFileLegendName, System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            }
        }

        public string CalendarBackgroundFolderPath
        {
            get
            {
                return string.Format(CalendarBackgroundFolderName, new object[] { System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles), ConfigurationClasses.SettingsManager.Instance.SlideFolder });
            }
        }

        private OutputManager()
        {
            this.CalendarTemplates = new List<CalendarTemplate>();
        }

        public void LoadCalendarTemplates()
        {
            this.CalendarTemplates.Clear();
            string connnectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";", this.CalendarFileLegendPath);
            OleDbConnection connection = new OleDbConnection(connnectionString);
            try
            {
                connection.Open();
            }
            catch
            {
                AppManager.ShowWarning("Couldn't open file legend file");
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
                                        CalendarTemplate template = new CalendarTemplate();
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
                                            this.CalendarTemplates.Add(template);
                                    }
                                }
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
            }
        }
    }

    class CalendarTemplate
    {
        public string Month { get; set; }
        public bool HasLogo { get; set; }
        public bool IsLarge { get; set; }
        public string Color { get; set; }
        public string TemplateName { get; set; }
        public string SlideMaster { get; set; }

        public CalendarTemplate()
        {
            this.Month = string.Empty;
            this.TemplateName = string.Empty;
            this.SlideMaster = string.Empty;
        }
    }
}
