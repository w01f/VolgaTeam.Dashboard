using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace Asa.Bar.App.Configuration
{
	[Serializable]
	public class UserSettings
	{
		public string SelectedBrowser { get; set; }
		public int PreferedMonitor { get; set; }
		public ColorEx AccentColor { get; set; }
		public bool LoadAtStartup { get; set; }

		public UserSettings()
		{
			Reset();
		}

		public static UserSettings Load()
		{
			var defaultSettings = new UserSettings();
			if (ResourceManager.Instance.AppSettingsFile.ExistsLocal())
			{
				try
				{
					using (var stream = File.OpenRead(ResourceManager.Instance.AppSettingsFile.LocalPath))
					{
						var bf = new XmlSerializer(typeof(UserSettings));
						return (UserSettings)bf.Deserialize(stream);
					}
				}
				catch
				{
				}
			}
			return defaultSettings;
		}

		public void Save()
		{
			try
			{
				using (var stream = File.CreateText(ResourceManager.Instance.AppSettingsFile.LocalPath))
				{
					var bf = new XmlSerializer(typeof(UserSettings));
					bf.Serialize(stream, this);
				}
			}
			catch
			{
			}
		}

		private void Reset()
		{
			PreferedMonitor = 0;
			AccentColor = Color.Transparent;
			SelectedBrowser = null;
			LoadAtStartup = true;
		}
	}
}
