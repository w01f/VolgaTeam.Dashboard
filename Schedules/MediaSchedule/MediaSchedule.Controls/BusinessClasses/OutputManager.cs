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
		private const string OneSheetsTableBasedTemplatesFolderName = @"{0}\{1} Slides\tables";
		public const string OneSheetTableBasedTemplateFileName = @"{0}\{1}_programs\{1}-{2}.ppt";
		public static string MasterWizardsRootFolderPath = String.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\ScheduleBuilders", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

		private const string CalendarTemlatesFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\broadcast_cal\broadcast_slides";
		public const string CalendarSlideTemplate = @"Broadcast_{0}_{1}_{2}.pptx";
		public const string CalendarBackgroundFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\broadcast_cal\broadcast_images";
		public const string BackgroundFilePath = @"{0}\{1}";


		public string OneSheetTableBasedTemplatesFolderPath
		{
			get { return Path.Combine(MasterWizardsRootFolderPath, String.Format(OneSheetsTableBasedTemplatesFolderName, SettingsManager.Instance.SlideFolder, MediaMetaData.Instance.DataTypeString)); }
		}

		public string CalendarTemlatesFolderPath
		{
			get { return string.Format(CalendarTemlatesFolderName, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}

		public string CalendarBackgroundFolderPath
		{
			get { return string.Format(CalendarBackgroundFolderName, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}

		public List<ColorFolder> AvailableColors { get; private set; }

		public OutputManager()
		{
			AvailableColors = new List<ColorFolder>();
			LoadColors();
		}

		private void LoadColors()
		{
			var outputTemplatesFolder = OneSheetTableBasedTemplatesFolderPath;
			if (!Directory.Exists(OneSheetTableBasedTemplatesFolderPath)) return;
			foreach (var directory in Directory.GetDirectories(outputTemplatesFolder))
			{
				var colorFolder = new ColorFolder();
				colorFolder.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Path.GetFileName(directory));
				var imagePath = Path.Combine(directory, "image.png");
				if (File.Exists(imagePath))
					colorFolder.Logo = new Bitmap(imagePath);
				AvailableColors.Add(colorFolder);
			}
		}
	}

	public class ColorFolder
	{
		public string Name { get; set; }
		public Image Logo { get; set; }
	}
}