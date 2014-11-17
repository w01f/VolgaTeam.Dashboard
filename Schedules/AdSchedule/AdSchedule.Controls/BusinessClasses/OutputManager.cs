using System;
using System.IO;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.Core.Calendar;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.AdSchedule.Controls.BusinessClasses
{
	public class OutputManager
	{
		private const string ExcelOutputTemplateFileName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\ExcelOutput{1}\Newspaper Slides\Print Schedule Formats{2}.xls";

		public const int Columns = 12;

		public const int DetailedGridGridBasedRowsCountWithNotes = 8;
		public const int DetailedGridGridBasedRowsCountWithoutNotes = 10;

		public const int MultiGridGridBasedRowsCountWithNotes = 8;
		public const int MultiGridGridBasedRowsCountWithoutNotes = 10;

		private const string BasicOverviewTemlatesFolderName = @"{0}\Newspaper Slides\basic overview";
		public const string BasicOverviewSlideTemplate = @"basic-{0}.pptx";

		private const string BasicOverviewSummaryTemlpatesFolderName = @"{0}\Newspaper Slides\product summary";
		public const string BasicOverviewSummaryTemplateFileName = "product_summary.pptx";

		private const string MultiSummaryTemlatesFolderName = @"{0}\Newspaper Slides\multi summary";
		public const string MultiSummarySlideTemplate = @"summary-{0}.pptx";

		private const string SnapshotTemlatesFolderName = @"{0}\Newspaper Slides\snapshotnew";

		private const string AdPlanTemlatesFolderName = @"{0}\Newspaper Slides\adplan";

		private const string DetailedGridGridBasedTemlatesFolderName = @"{0}\Newspaper Slides\tables";
		public const string DetailedGridGridBasedSlideTemplate = @"{0} columns_detailed\tables_{1}\table{2}_{1}.pptx";

		private const string MultiGridGridBasedTemlatesFolderName = @"{0}\Newspaper Slides\tables";
		public const string MultiGridGridBasedSlideTemplate = @"{0} columns_multi\tables_{1}_logos\table{2}_{1}.pptx";

		private const string CalendarTemlatesFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\{1}";
		public const string CalendarBackgroundFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\!Calendar_Images";
		public const string BackgroundFilePath = @"{0}\{1}";
		public static string MasterWizardsRootFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Dashboard", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

		public CalendarTemplatesManager TemplatesManager { get; private set; }

		public OutputManager()
		{
			TemplatesManager = new CalendarTemplatesManager();
		}

		public string ExcelOutputTemplateFilePath
		{
			get { return string.Format(ExcelOutputTemplateFileName, new object[] { Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), AdSchedulePowerPointHelper.Instance.Is2003 ? "03" : "07", AdSchedulePowerPointHelper.Instance.Is2003 ? "03" : "07" }); }
		}

		public string BasicOverviewTemlatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(BasicOverviewTemlatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string BasicOverviewSummaryTemplatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(BasicOverviewSummaryTemlpatesFolderName, SettingsManager.Instance.SlideFolder)); }
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

		public string DetailedGridGridBasedTemlatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(DetailedGridGridBasedTemlatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string MultiGridGridBasedTemlatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, SettingsManager.Instance.SelectedWizard, string.Format(MultiGridGridBasedTemlatesFolderName, SettingsManager.Instance.SlideFolder)); }
		}

		public string CalendarTemlatesFolderPath
		{
			get { return string.Format(CalendarTemlatesFolderName, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), SettingsManager.Instance.SlideFolder + "new"); }
		}

		public string CalendarBackgroundFolderPath
		{
			get { return string.Format(CalendarBackgroundFolderName, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}
	}
}