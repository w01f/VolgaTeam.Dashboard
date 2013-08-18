using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;

namespace NewBizWiz.OnlineSchedule.Single
{
	public class AppManager
	{
		private static readonly AppManager _instance = new AppManager();

		private AppManager() { }

		public static AppManager Instance
		{
			get { return _instance; }
		}

		public void RunForm()
		{
			OnlineSchedulePowerPointHelper.Instance.SetPresentationSettings();
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
	}
}