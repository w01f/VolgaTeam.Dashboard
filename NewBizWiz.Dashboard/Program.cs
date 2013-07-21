using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.Dashboard.InteropClasses;
using SettingsManager = NewBizWiz.Core.Dashboard.SettingsManager;

namespace NewBizWiz.Dashboard
{
	internal static class Program
	{
		private static Mutex mutex; //Used to determine if the application is already open

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			bool firstInstance;
			string uniqueIdentifier = "Local\\NewBizWizApplication";
			mutex = new Mutex(false, uniqueIdentifier, out firstInstance);
			bool firstRun = false;
			SettingsManager.Instance.LoadSettings();
			Core.Common.SettingsManager.Instance.CheckStaticFolders(out firstRun);
			if (firstRun)
			{
				Utilities.Instance.ShowWarning("Dashboard Unavailable: You do not have any Files....");
				return;
			}
			if (firstInstance)
			{
				RegistryHelper.MaximizeMainForm = false;
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
				Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
				Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
				Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
				if (args != null && args.Length > 0)
					AppManager.Instance.ShowCover = args[0].ToLower().Equals("showcover");

				if (AppManager.Instance.RunPowerPoint())
					AppManager.Instance.RunForm();
			}
			else
			{
				if (AppManager.Instance.RunPowerPoint())
				{
					Utilities.Instance.ActivatePowerPoint(DashboardPowerPointHelper.Instance.PowerPointObject);
					Utilities.Instance.ActivateMiniBar();
					AppManager.Instance.ActivateMainForm();
				}
			}
		}
	}
}