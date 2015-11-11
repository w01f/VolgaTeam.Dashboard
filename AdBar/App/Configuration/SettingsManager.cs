namespace Asa.Bar.App.Configuration
{
	public class SettingsManager
	{
		public AppConfig Config { get; private set; }
		public UserSettings UserSettings { get; private set; }

		public SettingsManager()
		{
			Config = new AppConfig();
		}

		public void Load()
		{
			UserSettings = UserSettings.Load();
			Config.Load();
		}
	}
}
