using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.Core.Common;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;

namespace NewBizWiz.AdSchedule.Single
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
			MasterWizardManager.Instance.SetMasterWizard();
			Controls.BusinessClasses.BusinessWrapper.Instance.OutputManager.LoadCalendarTemplates();
			AdSchedulePowerPointHelper.Instance.SetPresentationSettings();
			OnlineSchedulePowerPointHelper.Instance.SetPresentationSettings();
			Application.Run(FormMain.Instance);
		}

		public bool RunPowerPoint()
		{
			return AdSchedulePowerPointHelper.Instance.Connect() && OnlineSchedulePowerPointHelper.Instance.Connect();
		}

		public void ActivateMainForm()
		{
			Process[] processList = Process.GetProcesses();
			foreach (Process process in processList.Where(x => x.ProcessName.ToLower().Contains("adseller")))
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