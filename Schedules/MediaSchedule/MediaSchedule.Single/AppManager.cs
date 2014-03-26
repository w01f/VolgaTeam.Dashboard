using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.CommonGUI;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;

namespace NewBizWiz.MediaSchedule.Single
{
	public class AppManager
	{
		private static readonly AppManager _instance = new AppManager();
		private readonly FloaterManager _floater = new FloaterManager();

		public NBWLink AppConfig { get; private set; }

		private AppManager() { }

		public static AppManager Instance
		{
			get { return _instance; }
		}

		public void RunForm()
		{
			LicenseHelper.Register();
			MediaSchedulePowerPointHelper.Instance.SetPresentationSettings();
			AppConfig = NBWLink.CreateLink(new DirectoryInfo(Application.StartupPath));
			Application.Run(FormMain.Instance);
		}

		public bool RunPowerPoint()
		{
			return MediaSchedulePowerPointHelper.Instance.Connect() && OnlineSchedulePowerPointHelper.Instance.Connect();
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

		public void ShowFloater(Form sender, Action afterShow)
		{
			var defaultText = !String.IsNullOrEmpty(AppConfig.Title) ? AppConfig.Title : String.Format("{0} Seller", MediaMetaData.Instance.DataTypeString);
			var afterBack = new Action(ActivateMainForm);
			_floater.ShowFloater(sender ?? FormMain.Instance, defaultText, AppConfig.Image, afterShow, null, afterBack);
		}
	}
}