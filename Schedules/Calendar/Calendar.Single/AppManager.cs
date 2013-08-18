using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.Calendar.Controls.BusinessClasses;
using NewBizWiz.Calendar.Controls.InteropClasses;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Calendar.Single
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
			BusinessWrapper.Instance.OutputManager.LoadCalendarTemplates();
			CalendarPowerPointHelper.Instance.SetPresentationSettings();
			Application.Run(FormMain.Instance);
		}

		public bool RunPowerPoint()
		{
			return CalendarPowerPointHelper.Instance.Connect();
		}

		public static void ActivateMainForm()
		{
			Process[] processList = Process.GetProcesses();
			foreach (Process process in processList.Where(x => x.ProcessName.ToLower().Contains("calendarbuilder")))
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