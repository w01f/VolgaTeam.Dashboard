using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Asa.Media.LegacyConverter
{
	class AppSettingsManager
	{
		public string MediaType { get; private set; }
		public string DataFolderName { get; private set; }
		public static AppSettingsManager Instance { get; } = new AppSettingsManager();

		private AppSettingsManager() { }

		public void LoadSettings()
		{
			var appFileName = Process.GetCurrentProcess().MainModule.FileName;
			var settingsFilePath = Path.ChangeExtension(appFileName, "txt");
			if (File.Exists(settingsFilePath))
			{
				var settingsLines = File.ReadAllLines(settingsFilePath);
				if (settingsLines.Any())
				{
					MediaType = settingsLines.ElementAtOrDefault(0);
					DataFolderName = settingsLines.ElementAtOrDefault(1);
				}
			}
		}
	}
}
