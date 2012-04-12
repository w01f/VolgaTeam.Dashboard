using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OnlineScheduleBuilder.BusinessClasses
{
    class OutputManager
    {
        public static string MasterWizardsRootFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Dashboard", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
        private const string OneSheetsTemplatesFolderName = @"{0}\Online Slides\onesheets";
        private const string ProductSummaryTemplatesFolderName = @"{0}\Online Slides\summary";
        private const string ExcelTemplatesFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\ExcelOutput{1}\Online Slides"; 
        public const string ProductSummaryTemplateFileName = "online summary-1.ppt";
        public const string ExcelTemplateFileName = "Online_{0}_Output{1}.xls";

        private static OutputManager _instance = new OutputManager();

        public static OutputManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public string OneSheetsTemplatesFolderPath
        {
            get
            {
                return Path.Combine(MasterWizardsRootFolderPath, ConfigurationClasses.SettingsManager.Instance.SelectedWizard, string.Format(OneSheetsTemplatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string ProductSummaryTemplatesFolderPath
        {
            get
            {
                return Path.Combine(MasterWizardsRootFolderPath, ConfigurationClasses.SettingsManager.Instance.SelectedWizard, string.Format(ProductSummaryTemplatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string ExcelTemplatesFolderPath
        {
            get
            {
                return string.Format(ExcelTemplatesFolderName, new object[] { System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles), InteropClasses.PowerPointHelper.Instance.Is2003 ? "03" : "07" });
            }
        }

        private OutputManager()
        {
        }
    }
}
