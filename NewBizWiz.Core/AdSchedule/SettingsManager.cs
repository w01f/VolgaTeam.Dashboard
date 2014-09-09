using System;
using System.IO;

namespace NewBizWiz.Core.AdSchedule
{
	public class SettingsManager
	{
		private static SettingsManager _instance;

		private SettingsManager()
		{
			var defaultSaveFolderPath = Path.Combine(Common.SettingsManager.Instance.OutgoingFolderPath, @"Saved_Schedules\Ad Schedule Builder");
			if (!Directory.Exists(defaultSaveFolderPath))
				Directory.CreateDirectory(defaultSaveFolderPath);
			SaveFolder = defaultSaveFolderPath;

			ViewSettingsPath = Path.Combine(Common.SettingsManager.Instance.SettingsPath, "AdScheduleViewSetings.xml");
			LocalSettingsPath = Path.Combine(Common.SettingsManager.Instance.SettingsPath, "AdScheduleSetings.xml");
			HelpLinksPath = String.Format(@"{0}\newlocaldirect.com\app\HelpUrls\AdScheduleHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			IconPath = Path.Combine(Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location), "icon.ico");
		}

		public static SettingsManager Instance
		{
			get
			{
				if (_instance == null)
					_instance = new SettingsManager();
				return _instance;
			}
		}

		public string SaveFolder { get; set; }
		public string ViewSettingsPath { get; set; }
		public string LocalSettingsPath { get; set; }
		public string HelpLinksPath { get; set; }
		public string IconPath { get; set; }
	}
}