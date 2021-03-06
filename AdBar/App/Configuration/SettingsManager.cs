﻿namespace Asa.Bar.App.Configuration
{
    public class SettingsManager
    {
        public AppConfig Config { get; }
        public UserSettings UserSettings { get; private set; }
        public MaintenanceConfiguration MaintenanceConfig { get; }
        public PatchUpdaterConfiguration PatchUpdaterConfig { get; }

        public GrayConnectConfiguration GrayConnectConfig { get; }

        public SettingsManager()
        {
            Config = new AppConfig();
            MaintenanceConfig = new MaintenanceConfiguration();
            PatchUpdaterConfig = new PatchUpdaterConfiguration();
            GrayConnectConfig = new GrayConnectConfiguration();
        }

        public void Load()
        {
            UserSettings = UserSettings.Load();
            Config.Load();
            MaintenanceConfig.Load();
            PatchUpdaterConfig.Load();
        }
    }
}
