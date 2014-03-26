using System;
using System.IO;

namespace NewBizWiz.Core.AdSchedule
{
	public class SettingsManager
	{
		private static SettingsManager _instance;

		private SettingsManager()
		{
			var defaultSaveFolderPath = Path.Combine(String.Format(@"{0}\newlocaldirect.com\sync\Outgoing", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "AppID-" + Common.SettingsManager.Instance.AppID.ToString(), @"Saved_Schedules\Ad Schedule Builder");
			if (!Directory.Exists(defaultSaveFolderPath))
				Directory.CreateDirectory(defaultSaveFolderPath);
			SaveFolder = defaultSaveFolderPath;

			ViewSettingsPath = String.Format(@"{0}\newlocaldirect.com\xml\app\AdScheduleViewSetings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			LocalSettingsPath = String.Format(@"{0}\newlocaldirect.com\xml\app\AdScheduleSetings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
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