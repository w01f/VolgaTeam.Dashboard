using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AdBar.Plugins.Core;
using DevComponents.DotNetBar;
using DevExpress.Utils;

namespace AdBar.Plugins.Sync
{
	public partial class ContainerControl : UserControl, IAdBarControl
	{
		public ContainerControl()
		{
			InitializeComponent();
			UpdateControlInternal();
		}

		#region IAdBarControl Memebers

		public string ControlName
		{
			get { return "syncsettings"; }
		}

		public IEnumerable<RibbonBar> RibbonBars
		{
			get { return new[] { ribbonBar }; }
		}

		public event EventHandler<AdBarControlStateEventArgs> StateChanged;

		public void UpdateControl(IAdBarControl raisedBy, object[] stateParameters)
		{
			if (raisedBy != null && raisedBy.ControlName.Equals("syncsettings"))
			{
			}
		}

		#endregion

		private void UpdateControlInternal()
		{
			ScheduleSync();
			MonitorRegularSync();
			DisplayLastSync(SettingsManager.Instance.LastSync);
			switchButtonItemSyncHourly.Value = SettingsManager.Instance.SyncHourly;
		}

		private void ScheduleSync()
		{
			SyncManager.SchedulerSyncInBackground(DisplayNextSyncAsync, DisplayLastSyncAsync);
		}

		private void MonitorRegularSync()
		{
			SyncManager.MonitorRegularSyncInBackground(DisplayLastSyncAsync);
		}

		private void DisplayNextSync(DateTime dt)
		{
			labelItemNextSyncValue.Text = dt.ToString("MM/dd/yy h:mm tt");
			if (SettingsManager.Instance.SyncHourly)
				timeEditSyncTime.EditValue = null;
			else
				timeEditSyncTime.Time = dt;
			ribbonBar.RecalcLayout();
		}

		private void DisplayNextSyncAsync(DateTime dt)
		{
			if (InvokeRequired)
				BeginInvoke((MethodInvoker)(() => DisplayNextSync(dt)));
			else
				DisplayNextSync(dt);
		}

		private void DisplayLastSyncAsync(DateTime? dt)
		{
			if (InvokeRequired)
				BeginInvoke((MethodInvoker)(() => DisplayLastSync(dt)));
			else
				DisplayLastSync(dt);
		}

		private void DisplayLastSync(DateTime? dt)
		{
			labelItemLastSyncValue.Text = dt.HasValue ? dt.Value.ToString("MM/dd/yy h:mm tt") : "Not synced yet";
			ribbonBar.RecalcLayout();
		}

		private void switchButtonItemSyncHourly_ValueChanged(object sender, EventArgs e)
		{
			buttonItemSaveSyncTime.Enabled = !switchButtonItemSyncHourly.Value;
			timeEditSyncTime.Enabled = !switchButtonItemSyncHourly.Value;

			SettingsManager.Instance.SyncHourly = switchButtonItemSyncHourly.Value;
			SettingsManager.Instance.SaveSettings();
			ScheduleSync();
		}

		private void buttonItemSaveSyncTime_Click(object sender, EventArgs e)
		{
			SettingsManager.Instance.NextSync = timeEditSyncTime.Time;
			ScheduleSync();
			OutOfTimer_Click(this, EventArgs.Empty);
		}

		private void OutOfTimer_Click(object sender, EventArgs e)
		{
			var oldValue = timeEditSyncTime.Enabled;
			timeEditSyncTime.Enabled = false;
			timeEditSyncTime.Enabled = oldValue;
		}
	}
}