using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace MiniBar.BusinessClasses
{
    class SyncManager
    {
        private static Timer _timer = null;

        public static bool CheckSyncSetting()
        {
            bool result = false;
            string syncSettingsFilePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.SyncSettingsFolderPath, ConfigurationClasses.SettingsManager.SyncSettingsFileName);
            if (!File.Exists(syncSettingsFilePath))
            {
                SyncPC.AppManager.Instance.SettingsFolderPath = ConfigurationClasses.SettingsManager.Instance.SyncSettingsFolderPath;
                result = SyncPC.AppManager.Instance.OpenSettingForm();
            }
            else
                result = true;
            return result;
        }


        public static long GetMillisecondsForNextInvocation()
        {
            DateTime nowTime = DateTime.Now;

            DateTime nextTime = DateTime.Now;

            nextTime = ConfigurationClasses.SettingsManager.Instance.NextSync;

            TimeSpan difference = nextTime.Subtract(nowTime);
            long totalMilliseconds = (long)difference.TotalMilliseconds;
            if (FormMain.Instance != null)
                FormMain.Instance.Invoke(new AppManager.SingleParamDelegate(FormMainExpanded.Instance.DisplayNextSync), nextTime);
            return totalMilliseconds;
        }

        public static void SchedulerSyncInBackground()
        {
            StopBackgroundSync();
            _timer = new Timer(SilentSynchronize, null, GetMillisecondsForNextInvocation(), Timeout.Infinite);
        }

        public static void StopBackgroundSync()
        {
            if (_timer != null)
                _timer.Dispose();
        }

        public static void RegularSynchronize()
        {
            string filePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.SyncSettingsFolderPath, ConfigurationClasses.SettingsManager.RegularSyncName);
            if (File.Exists(filePath))
            {
                Process.Start(filePath);
                ConfigurationClasses.SettingsManager.Instance.LastSync = DateTime.Now;
                ConfigurationClasses.SettingsManager.Instance.SaveMinibarSettings();
                FormMainExpanded.Instance.DisplayLastSync(ConfigurationClasses.SettingsManager.Instance.LastSync);
                SchedulerSyncInBackground();
            }
            else
                AppManager.Instance.ShowWarning("Couldn't find Sync application");
        }
        private static void SilentSynchronize(object state)
        {
            string filePath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.SyncSettingsFolderPath, ConfigurationClasses.SettingsManager.SilentSyncName);
            if (File.Exists(filePath))
            {
                Process.Start(filePath);
                ConfigurationClasses.SettingsManager.Instance.LastSync = DateTime.Now;
                ConfigurationClasses.SettingsManager.Instance.SaveMinibarSettings();
                FormMainExpanded.Instance.DisplayLastSync(ConfigurationClasses.SettingsManager.Instance.LastSync);

                SchedulerSyncInBackground();
            }
        }
    }
}
