using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.CommonGUI;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.Core.Common;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;

namespace NewBizWiz.OnlineSchedule.Single
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
			OnlineSchedulePowerPointHelper.Instance.SetPresentationSettings();
			AppConfig = NBWLink.CreateLink(new DirectoryInfo(Application.StartupPath));
			Application.Run(FormMain.Instance);
		}

		public bool RunPowerPoint()
		{
			return OnlineSchedulePowerPointHelper.Instance.Connect();
		}

		public void ActivateMainForm()
		{
			Process[] processList = Process.GetProcesses();
			foreach (Process process in processList.Where(x => x.ProcessName.ToLower().Contains("digitalseller")))
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
			var defaultText = !String.IsNullOrEmpty(AppConfig.Title) ? AppConfig.Title : "SellerPoint WebPRO";
			var afterBack = new Action(ActivateMainForm);
			_floater.ShowFloater(sender ?? FormMain.Instance, defaultText, AppConfig.Image, afterShow, null, afterBack);
		}
	}
}