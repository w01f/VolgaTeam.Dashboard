using System;
using System.IO;

namespace NewBizWiz.Core.OnlineSchedule
{
	public class SettingsManager
	{
		private static readonly SettingsManager _instance = new SettingsManager();
		private readonly string _defaultSaveFolderPath = string.Empty;

		private SettingsManager()
		{
			HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\OnlineHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			InventoryImagePath = string.Format(@"{0}\newlocaldirect.com\app\online_ribbon_logo.png", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			SaveFolder = Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Outgoing", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "AppID-" + Common.SettingsManager.Instance.AppID.ToString(), @"Saved_Schedules\Online Schedule Builder");
			if (!Directory.Exists(SaveFolder))
				Directory.CreateDirectory(SaveFolder);
		}

		public string SaveFolder { get; set; }
		public string HelpLinksPath { get; set; }
		public string InventoryImagePath { get; set; }

		public static SettingsManager Instance
		{
			get { return _instance; }
		}
	}
}