﻿using System;
using System.IO;
using System.Text;
using System.Xml;
using NewBizWiz.Core.Calendar;

namespace NewBizWiz.Core.MediaSchedule
{
	public interface IMediaSettingsManager
	{
		string SaveFolder { get; set; }
		string HelpLinksPath { get; set; }
		string IconPath { get; set; }
		string SelectedColor { get; set; }
		bool UseSlideMaster { get; set; }
		CalendarSettings BroadcastCalendarSettings { get; }
		void SaveSettings();
	}

	public abstract class MediaSettingsManager : IMediaSettingsManager
	{
		protected abstract string LocalSettingsPath { get; }

		protected MediaSettingsManager()
		{
			BroadcastCalendarSettings = new CalendarSettings();
			LoadSettings();
		}

		private void LoadSettings()
		{
			if (!File.Exists(LocalSettingsPath)) return;
			var document = new XmlDocument();
			document.Load(LocalSettingsPath);

			var node = document.SelectSingleNode(@"/Settings/SelectedColor");
			if (node != null)
				SelectedColor = node.InnerText;
			node = document.SelectSingleNode(@"/Settings/UseSlideMaster");
			if (node != null)
			{
				bool tempBool;
				if (Boolean.TryParse(node.InnerText, out tempBool))
					UseSlideMaster = tempBool;
			}
			node = document.SelectSingleNode(@"/Settings/BroadcastCalendarSettings");
			if (node != null)
				BroadcastCalendarSettings.Deserialize(node);
		}

		public void SaveSettings()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<Settings>");
			if (!String.IsNullOrEmpty(SelectedColor))
				xml.AppendLine(@"<ThemeName>" + SelectedColor.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ThemeName>");
			xml.AppendLine(@"<UseSlideMaster>" + UseSlideMaster + @"</UseSlideMaster>");
			xml.AppendLine(@"<BroadcastCalendarSettings>" + BroadcastCalendarSettings.Serialize() + @"</BroadcastCalendarSettings>");
			xml.AppendLine(@"</Settings>");
			var userConfigurationPath = Path.Combine(LocalSettingsPath);
			using (var sw = new StreamWriter(userConfigurationPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		public string SaveFolder { get; set; }
		public string HelpLinksPath { get; set; }
		public string IconPath { get; set; }
		public string SelectedColor { get; set; }
		public bool UseSlideMaster { get; set; }
		public CalendarSettings BroadcastCalendarSettings { get; private set; }
	}

	public class TVSettingsManager : MediaSettingsManager
	{
		public TVSettingsManager()
		{
			var defaultSaveFolderPath = Path.Combine(String.Format(@"{0}\newlocaldirect.com\sync\Outgoing", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "AppID-" + Common.SettingsManager.Instance.AppID, @"Saved_Schedules\TV Schedule Builder");
			if (!Directory.Exists(defaultSaveFolderPath))
				Directory.CreateDirectory(defaultSaveFolderPath);
			SaveFolder = defaultSaveFolderPath;
			HelpLinksPath = String.Format(@"{0}\newlocaldirect.com\app\HelpUrls\TVHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			IconPath = Path.Combine(Path.GetDirectoryName(typeof(TVSettingsManager).Assembly.Location), "icon.ico");
		}

		protected override string LocalSettingsPath
		{
			get { return String.Format(@"{0}\newlocaldirect.com\xml\app\TVScheduleSettings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}
	}

	public class RadioSettingsManager : MediaSettingsManager
	{
		public RadioSettingsManager()
		{
			var defaultSaveFolderPath = Path.Combine(String.Format(@"{0}\newlocaldirect.com\sync\Outgoing", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "AppID-" + Common.SettingsManager.Instance.AppID, @"Saved_Schedules\Radio Schedule Builder");
			if (!Directory.Exists(defaultSaveFolderPath))
				Directory.CreateDirectory(defaultSaveFolderPath);
			SaveFolder = defaultSaveFolderPath;
			HelpLinksPath = String.Format(@"{0}\newlocaldirect.com\app\HelpUrls\RadioHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			IconPath = Path.Combine(Path.GetDirectoryName(typeof(RadioSettingsManager).Assembly.Location), "icon.ico");
		}

		protected override string LocalSettingsPath
		{
			get { return String.Format(@"{0}\newlocaldirect.com\xml\app\RadioScheduleSettings.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}
	}
}