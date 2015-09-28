using System;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.Dashboard.InteropClasses;
using SettingsManager = NewBizWiz.Core.Dashboard.SettingsManager;

namespace NewBizWiz.Dashboard
{
	internal static class Program
	{
		private static Mutex _mutex;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			bool firstInstance;
			const string uniqueIdentifier = "Local\\NewBizWizApplication";
			_mutex = new Mutex(false, uniqueIdentifier, out firstInstance);
			bool firstRun;
			//SettingsManager.Instance.LoadSettings();
			//Core.Common.SettingsManager.Instance.CheckStaticFolders(out firstRun);
			//if (firstRun)
			//{
			//	Utilities.Instance.ShowWarning("Dashboard Unavailable: You do not have any Files....");
			//	return;
			//}
			if (firstInstance)
			{
				RegistryHelper.MaximizeMainForm = false;
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				AppManager.Instance.RunForm();
			}
			else
			{
				Utilities.Instance.ActivatePowerPoint(DashboardPowerPointHelper.Instance.PowerPointObject);
				AppManager.Instance.ActivateMainForm();
			}
		}
	}
}