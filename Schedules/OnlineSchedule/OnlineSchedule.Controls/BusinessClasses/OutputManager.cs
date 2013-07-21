using System;
using System.IO;
using NewBizWiz.Core.Common;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;

namespace NewBizWiz.OnlineSchedule.Controls.BusinessClasses
{
	public class OutputManager
	{
		private const string OneSheetsTemplatesFolderName = @"{0}\Online Slides\onesheets";
		private const string ProductSummaryTemplatesFolderName = @"{0}\Online Slides\summary";
		private const string ExcelTemplatesFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\ExcelOutput{1}\Online Slides";
		public const string ProductSummaryTemplateFileName = "online summary-1.ppt";
		public const string ExcelTemplateFileName = "Online_{0}_Output{1}.xls";
		public static string MasterWizardsRootFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Dashboard", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

		public string OneSheetsTemplatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(OneSheetsTemplatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string ProductSummaryTemplatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(ProductSummaryTemplatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string ExcelTemplatesFolderPath
		{
			get { return string.Format(ExcelTemplatesFolderName, new object[] { Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), OnlineSchedulePowerPointHelper.Instance.Is2003 ? "03" : "07" }); }
		}
	}
}