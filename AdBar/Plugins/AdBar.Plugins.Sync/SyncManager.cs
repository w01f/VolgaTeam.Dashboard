using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace AdBar.Plugins.Sync
{
	internal class SyncManager
	{
		private static Timer _silentSyncTimer;

		public static long GetMillisecondsForNextInvocation(Action<DateTime> displayNextSync)
		{
			DateTime nowTime = DateTime.Now;

			DateTime nextTime = DateTime.Now;

			nextTime = SettingsManager.Instance.NextSync;

			var difference = nextTime.Subtract(nowTime);
			var totalMilliseconds = (long)difference.TotalMilliseconds;
			displayNextSync(nextTime);
			return totalMilliseconds;
		}

		public static void SchedulerSyncInBackground(Action<DateTime> displayNextSync, Action<DateTime?> displayLastSync)
		{
			StopBackgroundSync();
			_silentSyncTimer = new Timer((obj) => SilentSynchronize(displayNextSync, displayLastSync), null, GetMillisecondsForNextInvocation(displayNextSync), Timeout.Infinite);
		}

		public static void StopBackgroundSync()
		{
			if (_silentSyncTimer != null)
				_silentSyncTimer.Dispose();
		}

		private static void SilentSynchronize(Action<DateTime> displayNextSync, Action<DateTime?> displayLastSync)
		{
			var filePath = Path.Combine(SettingsManager.Instance.SyncSettingsFolderPath, SettingsManager.SilentSyncName);
			if (!File.Exists(filePath)) return;
			Process.Start(filePath);
			SettingsManager.Instance.LastSync = DateTime.Now;
			SettingsManager.Instance.SaveSettings();
			displayLastSync(SettingsManager.Instance.LastSync);
			SchedulerSyncInBackground(displayNextSync, displayLastSync);
		}

		public static void MonitorRegularSyncInBackground(Action<DateTime?> displayLastSync)
		{
			var thread = new Thread(delegate()
			{
				while (true)
				{
					Thread.Sleep(100);
					if (!Process.GetProcesses().Any(p => p.ProcessName.ToLower().Contains(SettingsManager.SyncProcessName.ToLower()))) continue;
					SettingsManager.Instance.LastSync = DateTime.Now;
					SettingsManager.Instance.SaveSettings();
					displayLastSync(SettingsManager.Instance.LastSync);
				}
			});
		    thread.IsBackground = true;
			thread.Start();
		}
	}
}