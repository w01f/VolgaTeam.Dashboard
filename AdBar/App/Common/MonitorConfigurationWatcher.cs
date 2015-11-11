using System;
using System.Linq;
using System.Windows.Forms;

namespace Asa.Bar.App.Common
{
	class MonitorConfigurationWatcher
	{
		private readonly Timer _checkTimer;
		public int MonitorsCount { get; private set; }

		public event EventHandler<EventArgs> ConfigurationChanged;

		public MonitorConfigurationWatcher()
		{
			_checkTimer = new Timer();
			_checkTimer.Enabled = false;
			_checkTimer.Interval = 2500;
			_checkTimer.Tick += OnCheckTimerTick;
		}

		public void Load()
		{
			MonitorsCount = GetMonitorsCount();
			UpdatePreferedMonitorSettings();
		}

		public void StartWatching()
		{
			_checkTimer.Start();
		}

		private int GetMonitorsCount()
		{
			return Screen.AllScreens.Count();
		}

		private void OnCheckTimerTick(object sender, EventArgs e)
		{
			var currentCount = GetMonitorsCount();
			if (currentCount == MonitorsCount) return;

			MonitorsCount = currentCount;

			UpdatePreferedMonitorSettings();

			if (ConfigurationChanged != null)
				ConfigurationChanged(this, EventArgs.Empty);
		}

		private void UpdatePreferedMonitorSettings()
		{
			if (AppManager.Instance.Settings.UserSettings.PreferedMonitor < MonitorsCount) return;
			AppManager.Instance.Settings.UserSettings.PreferedMonitor = (MonitorsCount - 1);
			AppManager.Instance.Settings.UserSettings.Save();
		}
	}
}
