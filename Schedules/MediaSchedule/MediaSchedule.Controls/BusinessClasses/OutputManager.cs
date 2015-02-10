using System;
using System.IO;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.MediaSchedule.Controls.BusinessClasses
{
	public class OutputManager
	{
		public static string MasterWizardsRootFolderPath = String.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\ScheduleBuilders", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
		private const string OneSheetTemplatesFolderName = @"{0}\{1} Slides\tables";
		public const string OneSheetTemplateFileName = @"{0}\{1}\{2}_programs\{2}-{3}.pptx";

		private const string StrategyTemplatesFolderName = @"{0}\{1} Slides\strategy";
		public const string StrategyTemplateFileName = @"strategy_{0}.pptx";

		private const string SnapshotTemplatesFolderName = @"{0}\{1} Slides\snapshot";
		public const string SnapshotTemplateFileName = @"{0}\{1}\1s{2}r\{2}rows_{3}.pptx";
		public const string SnapshotSummaryTemplateFileName = @"{0}\summary\snapshot_summary_{1}.pptx";

		private const string OptionsTemplatesFolderName = @"{0}\{1} Slides\options";
		public const string OptionsTemplateFileName = @"{0}\options{1}.pptx";
		public const string OptionsColumnWidthsFileName = @"table_column_widths.txt";
		public const string OptionsSummaryTemplateFileName = @"{0}\summary\options_summary_{1}.pptx";

		private const string CalendarTemlatesFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\broadcast_cal\broadcast_slides";
		public const string CalendarSlideTemplate = @"Broadcast_{0}_{1}_{2}.pptx";
		public const string CalendarBackgroundFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\broadcast_cal\broadcast_images";

		public const string BackgroundFilePath = @"{0}\{1}";

		public string OneSheetTemplatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, String.Format(OneSheetTemplatesFolderName, SettingsManager.Instance.SlideFolder, MediaMetaData.Instance.DataTypeString)); }
		}

		public string StrategyTemplatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, String.Format(StrategyTemplatesFolderName, SettingsManager.Instance.SlideFolder, MediaMetaData.Instance.DataTypeString)); }
		}

		public string SnapshotTemplatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, String.Format(SnapshotTemplatesFolderName, SettingsManager.Instance.SlideFolder, MediaMetaData.Instance.DataTypeString)); }
		}

		public string OptionsTemplatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, String.Format(OptionsTemplatesFolderName, SettingsManager.Instance.SlideFolder, MediaMetaData.Instance.DataTypeString)); }
		}

		public string BroadcastCalendarTemlatesFolderPath
		{
			get { return string.Format(CalendarTemlatesFolderName, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}

		public string CalendarBackgroundFolderPath
		{
			get { return string.Format(CalendarBackgroundFolderName, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}

		public OutputColorList ScheduleColors { get; private set; }
		public OutputColorList SnapshotColors { get; private set; }
		public OutputColorList OptionsColors { get; private set; }
		public OutputColorList CalendarColors { get; private set; }

		public OutputManager()
		{
			ScheduleColors = new OutputColorList(OneSheetTemplatesFolderPath);
			SnapshotColors = new OutputColorList(SnapshotTemplatesFolderPath);
			OptionsColors = new OutputColorList(OptionsTemplatesFolderPath);
			CalendarColors = new OutputColorList(CalendarBackgroundFolderPath);
		}
	}
}