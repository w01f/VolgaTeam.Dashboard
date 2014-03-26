using System;
using System.IO;
using NewBizWiz.Core.Common;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;

namespace NewBizWiz.OnlineSchedule.Controls.BusinessClasses
{
	public class OutputManager
	{
		private const string OneSheetsTemplatesFolderName = @"{0}\Online Slides\onesheets";
		private const string ExcelTemplatesFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\ExcelOutput{1}\Online Slides";
		public const string ExcelTemplateFileName = "Online_{0}_Output{1}.xls";
		public static string MasterWizardsRootFolderPath = String.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Dashboard", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
		private const string DigitalPackageTemplatesFolderName = @"{0}\Online Slides\table";
		public const string DigitalPackageTemplateFileName = "digitaltable_{0}{1}.pptx";

		private const string AdPlanTemlatesFolderName = @"{0}\Online Slides\adplan";

		private const string DigitalSummaryTemlpatesFolderName = @"{0}\Online Slides\summary";
		public const string DigitalSummaryTemplateFileName = "digital_summary.pptx";

		public string OneSheetsTemplatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, String.Format(OneSheetsTemplatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string ExcelTemplatesFolderPath
		{
			get { return string.Format(ExcelTemplatesFolderName, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), OnlineSchedulePowerPointHelper.Instance.Is2003 ? "03" : "07"); }
		}

		public string DigitalPackageTemplatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, String.Format(DigitalPackageTemplatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string AdPlanTemlatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(AdPlanTemlatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string DigitalSummaryTemplatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(DigitalSummaryTemlpatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}
	}
}