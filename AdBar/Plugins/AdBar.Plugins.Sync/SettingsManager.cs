using System;
using System.IO;
using System.Text;
using System.Xml;

namespace AdBar.Plugins.Sync
{
	public class SettingsManager
	{
		public const string SyncProcessName = @"adSync";
		public const string RegularSyncName = @"adSync4.exe";
		public const string SilentSyncName = @"adSync5.exe";
		private static SettingsManager _instance;
		private readonly string _settingsFileName = String.Format(@"{0}\newlocaldirect.com\xml\app\AdBar-Plugins-Sync.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

		private DateTime? _nextSync = DateTime.Now;
		public DateTime? LastSync { get; set; }
		public DateTime NextSync
		{
			get
			{
				var now = DateTime.Now;
				var next = _nextSync.HasValue ? _nextSync.Value : now;
				next = new DateTime(now.Year, now.Month, now.Day, SyncHourly ? now.Hour : next.Hour, SyncHourly ? 0 : next.Minute, SyncHourly ? 0 : next.Second);
				if (next <= now)
					return SyncHourly ? next.AddHours(1) : next.AddDays(1);
				return next;
			}
			set
			{
				_nextSync = value;
			}
		}
		public bool SyncHourly { get; set; }
		public string SyncSettingsFolderPath { get; set; }

		public static SettingsManager Instance
		{
			get { return _instance ?? (_instance = new SettingsManager()); }
		}

		private SettingsManager()
		{
			SyncSettingsFolderPath = String.Format(@"{0}\newlocaldirect.com\!Update_Settings", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			LoadSettings();
		}

		private void LoadSettings()
		{
			DateTime tempDateTime;
			bool tempBool;
			SyncHourly = false;
			if (File.Exists(_settingsFileName))
			{
				var document = new XmlDocument();
				document.Load(_settingsFileName);
				var node = document.SelectSingleNode(@"/Settings/LastSync");
				if (node != null)
					if (DateTime.TryParse(node.InnerText, out tempDateTime))
						LastSync = tempDateTime;
				node = document.SelectSingleNode(@"/Settings/NextSync");
				if (node != null)
					if (DateTime.TryParse(node.InnerText, out tempDateTime))
						_nextSync = tempDateTime;
				node = document.SelectSingleNode(@"/Settings/SyncHourly");
				if (node != null)
					if (bool.TryParse(node.InnerText, out tempBool))
						SyncHourly = tempBool;
			}
			if (LastSync.HasValue) return;
			LastSync = DateTime.Now;
			NextSync = LastSync.Value;
		}

		public void SaveSettings()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Settings>");
			if (_nextSync.HasValue)
				xml.AppendLine(@"<NextSync>" + _nextSync.Value + @"</NextSync>");
			if (LastSync.HasValue)
				xml.AppendLine(@"<LastSync>" + LastSync.Value + @"</LastSync>");
			xml.AppendLine(@"<SyncHourly>" + SyncHourly + @"</SyncHourly>");
			xml.AppendLine(@"</Settings>");

			using (var sw = new StreamWriter(_settingsFileName, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}
}