using System;
using System.IO;

namespace NewBizWiz.Core.Calendar
{
	public class SettingsManager
	{
		private static readonly SettingsManager _instance = new SettingsManager();
		private SettingsManager()
		{
			HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\NinjaCalHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			var defaultSaveFolderPath = Path.Combine(Common.SettingsManager.Instance.OutgoingFolderPath, @"Saved_Schedules\Calendar Builder");
			if (!Directory.Exists(defaultSaveFolderPath))
				Directory.CreateDirectory(defaultSaveFolderPath);
			SaveFolder = defaultSaveFolderPath;
			ViewSettings = new LocalSettings();
		}

		public string SaveFolder { get; set; }
		public string HelpLinksPath { get; set; }
		public string AdScheduleFolder { get; set; }

		public LocalSettings ViewSettings { get; private set; }

		public static SettingsManager Instance
		{
			get { return _instance; }
		}
	}
}