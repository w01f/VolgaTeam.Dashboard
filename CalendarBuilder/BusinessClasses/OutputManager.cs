using System.IO;

namespace CalendarBuilder.BusinessClasses
{
    class OutputManager
    {
        public static string MasterWizardsRootFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Dashboard", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
        private const string OneSheetsExcelBasedTemplatesFolderName = @"{0}\Calendar Slides\onesheets";
        public const string OneSheetsExcelTemplatesFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\ExcelOutput{1}\Calendar Slides\{2}";
        public const string OneSheetExcelBasedTemplateFileName = @"tvslide1.ppt";
        public const string OneSheetsExcelTemplateFileName = @"{0}.xls";

        private static OutputManager _instance = new OutputManager();

        public static OutputManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public string OneSheetExcelBasedTemplatesFolderPath
        {
            get
            {
                return Path.Combine(MasterWizardsRootFolderPath, ConfigurationClasses.SettingsManager.Instance.SelectedWizard, string.Format(OneSheetsExcelBasedTemplatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        private OutputManager()
        {
        }
    }
}
