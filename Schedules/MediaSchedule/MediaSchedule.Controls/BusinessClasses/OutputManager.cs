using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
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

		public List<ColorFolder> ScheduleColors { get; private set; }
		public List<ColorFolder> SnapshotColors { get; private set; }
		public List<ColorFolder> OptionsColors { get; private set; }

		public OutputManager()
		{
			ScheduleColors = new List<ColorFolder>();
			SnapshotColors = new List<ColorFolder>();
			OptionsColors = new List<ColorFolder>();
			LoadColors();
		}

		private void LoadColors()
		{
			ScheduleColors.AddRange(LoadColors(OneSheetTemplatesFolderPath));
			SnapshotColors.AddRange(LoadColors(SnapshotTemplatesFolderPath));
			OptionsColors.AddRange(LoadColors(OptionsTemplatesFolderPath));
		}

		private static IEnumerable<ColorFolder> LoadColors(string colorFolderPath)
		{
			var colorFolders = new List<ColorFolder>();
			if (!Directory.Exists(colorFolderPath)) return colorFolders;
			foreach (var directory in Directory.GetDirectories(colorFolderPath))
			{
				var colorFolder = new ColorFolder();
				colorFolder.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Path.GetFileName(directory));
				var imagePath = Path.Combine(directory, "image.png");
				if (File.Exists(imagePath))
					colorFolder.Logo = new Bitmap(imagePath);
				colorFolders.Add(colorFolder);
			}
			return colorFolders;
		}
	}

	public class ColorFolder
	{
		public string Name { get; set; }
		public Image Logo { get; set; }
	}
}