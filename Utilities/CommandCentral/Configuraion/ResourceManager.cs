using System.Diagnostics;
using System.IO;

namespace CommandCentral.Configuraion
{
	public class ResourceManager
	{
		public string SettingsFilePath { get; }

		public string MainDataSourceFilePath { get; }
		public string TVDataSourceFilePath { get; }
		public string RadioDataSourceFilePath { get; }
		public string CalendarDataSourceFilePath { get; }
		public string OnlineDataSourceFilePath { get; }
		public string SalesLibrariesDataSourceFilePath { get; }
		public string StarAppDataSourceFilePath { get; }

		public string TVImagesFolderPath { get; }
		public string RadioImagesFolderPath { get; }
		public string OnlineImagesFolderPath { get; }

		public ResourceManager()
		{
			var appFolderPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			SettingsFilePath = Path.Combine(appFolderPath, "settings.local");

			MainDataSourceFilePath = Path.Combine(appFolderPath, "cc_source_files", "asa_data.xlsx");
			TVDataSourceFilePath = Path.Combine(appFolderPath, "cc_source_files", "tv_strategy.xlsx");
			RadioDataSourceFilePath = Path.Combine(appFolderPath, "cc_source_files", "radio_strategy.xlsx");
			CalendarDataSourceFilePath = Path.Combine(appFolderPath, "cc_source_files", "broadcast_legend.xlsx");
			OnlineDataSourceFilePath = Path.Combine(appFolderPath, "cc_source_files", "digital_strategy.xlsx");
			SalesLibrariesDataSourceFilePath = Path.Combine(appFolderPath, "cc_source_files", "sd_search.xlsx");
			StarAppDataSourceFilePath = Path.Combine(appFolderPath, "cc_source_files", "star.xlsx");

			TVImagesFolderPath = Path.Combine(appFolderPath, "cc_source_files", "artwork", "tv_images");
			RadioImagesFolderPath = Path.Combine(appFolderPath, "cc_source_files", "artwork", "radio_images");
			OnlineImagesFolderPath = Path.Combine(appFolderPath, "cc_source_files", "artwork");
		}
	}
}
