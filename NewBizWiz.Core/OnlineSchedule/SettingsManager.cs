using System;
using System.IO;

namespace NewBizWiz.Core.OnlineSchedule
{
	public class SettingsManager
	{
		private static readonly SettingsManager _instance = new SettingsManager();

		private SettingsManager()
		{
			HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\OnlineHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			IconPath = Path.Combine(Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location), "icon.ico");
			SaveFolder = Path.Combine(Common.SettingsManager.Instance.OutgoingFolderPath, @"Saved_Schedules\Online Schedule Builder");
			LocalSettingsPath = String.Format(@"{0}\newlocaldirect.com\xml\app\OnlineScheduleSetings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			if (!Directory.Exists(SaveFolder))
				Directory.CreateDirectory(SaveFolder);
		}

		public string SaveFolder { get; set; }
		public string HelpLinksPath { get; set; }
		public string IconPath { get; set; }
		public string LocalSettingsPath { get; set; }

		public static SettingsManager Instance
		{
			get { return _instance; }
		}
	}
}