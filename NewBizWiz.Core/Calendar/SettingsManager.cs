﻿using System;
using System.IO;

namespace NewBizWiz.Core.Calendar
{
	public class SettingsManager
	{
		private static readonly SettingsManager _instance = new SettingsManager();
		private SettingsManager()
		{
			HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\NinjaCalHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			var defaultSaveFolderPath = Path.Combine(String.Format(@"{0}\newlocaldirect.com\sync\Outgoing", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "AppID-" + Common.SettingsManager.Instance.AppID.ToString(), @"Saved_Schedules\Calendar Builder");
			if (!Directory.Exists(defaultSaveFolderPath))
				Directory.CreateDirectory(defaultSaveFolderPath);
			SaveFolder = defaultSaveFolderPath;
			IconPath = Path.Combine(Path.GetDirectoryName(typeof(SettingsManager).Assembly.Location), "icon.ico");
			ViewSettings = new LocalSettings();
		}

		public string SaveFolder { get; set; }
		public string HelpLinksPath { get; set; }
		public string IconPath { get; set; }
		public string AdScheduleFolder { get; set; }

		public LocalSettings ViewSettings { get; private set; }

		public static SettingsManager Instance
		{
			get { return _instance; }
		}
	}
}