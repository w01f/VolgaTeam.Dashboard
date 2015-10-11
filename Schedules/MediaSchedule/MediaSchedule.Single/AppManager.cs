using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using NewBizWiz.CommonGUI.Common;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;

namespace NewBizWiz.MediaSchedule.Single
{
	public class AppManager
	{
		private static readonly AppManager _instance = new AppManager();
		private readonly FloaterManager _floater = new FloaterManager();

		private AppManager() { }

		public static AppManager Instance
		{
			get { return _instance; }
		}

		public void RunApplication(MediaDataType mediaType)
		{
			LicenseHelper.Register();

			RunPowerPoint();
			Utilities.Instance.ActivatePowerPoint(RegularMediaSchedulePowerPointHelper.Instance.PowerPointObject);

			MediaMetaData.Instance.Init(mediaType);
			AppProfileManager.Instance.InitApplication(MediaMetaData.Instance.AppType);

			using (var form = new FormProgress())
			{
				form.TopMost = true;
				form.Show();

				form.laProgress.Text = "Checking data version...";
				var thread = new Thread(() => AsyncHelper.RunSync(FileStorageManager.Instance.Init));
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();

				if (FileStorageManager.Instance.Connected)
				{
					if (FileStorageManager.Instance.DataState == DataActualityState.NotExisted)
						form.laProgress.Text = "Loading data from server for the 1st time...";
					else if (FileStorageManager.Instance.DataState == DataActualityState.Outdated)
						form.laProgress.Text = "Updating data from server...";
					else
						form.laProgress.Text = "Loading data...";

					thread = new Thread(() => AsyncHelper.RunSync(() => Controller.Instance.InitBusinessObjects()));
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();

					FormMain.Instance.Init();
				}

				form.Close();
			}
			if (FileStorageManager.Instance.Connected)
				Application.Run(FormMain.Instance);
			else
				Utilities.Instance.ShowWarning("This app is not activated. Contact adSALESapps Support (help@adSALESapps.com)");
		}

		public void RunPowerPoint()
		{
			RegularMediaSchedulePowerPointHelper.Instance.Connect(false);
			OnlineSchedulePowerPointHelper.Instance.Connect(false);
		}

		public void ActivateMainForm()
		{
			var processList = Process.GetProcesses();
			foreach (var process in processList.Where(x => x.ProcessName.ToLower().Contains(String.Format("{0}seller", MediaMetaData.Instance.DataTypeString.ToLower()))))
			{
				if (process.MainWindowHandle.ToInt32() != 0)
				{
					Utilities.Instance.ActivateForm(process.MainWindowHandle, true, false);
					break;
				}
			}
		}

		public void ShowFloater(Form sender, FloaterRequestedEventArgs e)
		{
			var afterBack = new Action(ActivateMainForm);
			_floater.ShowFloater(sender ?? FormMain.Instance, e.Logo, e.AfterShow, null, afterBack);
		}
	}
}