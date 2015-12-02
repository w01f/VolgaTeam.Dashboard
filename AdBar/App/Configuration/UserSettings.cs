using System;
using System.Drawing;

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
			return SettingsSerializeHelper.Load<UserSettings>(ResourceManager.Instance.AppSettingsFile);
		}

		public void Save()
		{
			this.Save(ResourceManager.Instance.AppSettingsFile);
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
