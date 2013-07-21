using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace NewBizWiz.MiniBar.BusinessClasses
{
	internal class SyncManager
	{
		private static Timer _timer;

		public static bool CheckSyncSetting()
		{
			bool result = false;
			string syncSettingsFilePath = Path.Combine(SettingsManager.Instance.SyncSettingsFolderPath, SettingsManager.SyncSettingsFileName);
			if (!File.Exists(syncSettingsFilePath))
			{
				SyncPC.AppManager.Instance.SettingsFolderPath = SettingsManager.Instance.SyncSettingsFolderPath;
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

			nextTime = SettingsManager.Instance.NextSync;

			TimeSpan difference = nextTime.Subtract(nowTime);
			var totalMilliseconds = (long)difference.TotalMilliseconds;
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
			string filePath = Path.Combine(SettingsManager.Instance.SyncSettingsFolderPath, SettingsManager.RegularSyncName);
			if (File.Exists(filePath))
			{
				Process.Start(filePath);
				SettingsManager.Instance.LastSync = DateTime.Now;
				SettingsManager.Instance.SaveMinibarSettings();
				FormMainExpanded.Instance.DisplayLastSync(SettingsManager.Instance.LastSync);
				SchedulerSyncInBackground();
			}
			else
				AppManager.Instance.ShowWarning("Couldn't find Sync application");
		}

		private static void SilentSynchronize(object state)
		{
			string filePath = Path.Combine(SettingsManager.Instance.SyncSettingsFolderPath, SettingsManager.SilentSyncName);
			if (File.Exists(filePath))
			{
				Process.Start(filePath);
				SettingsManager.Instance.LastSync = DateTime.Now;
				SettingsManager.Instance.SaveMinibarSettings();
				FormMainExpanded.Instance.DisplayLastSync(SettingsManager.Instance.LastSync);

				SchedulerSyncInBackground();
			}
		}
	}
}