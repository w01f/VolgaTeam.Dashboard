using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.OnlineSchedule.Controls.InteropClasses;

namespace NewBizWiz.OnlineSchedule.Single
{
	internal static class Program
	{
		private static Mutex _mutex;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			bool firstInstance;
			const string uniqueIdentifier = "Local\\AdSellerApplication";
			_mutex = new Mutex(false, uniqueIdentifier, out firstInstance);
			if (firstInstance)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				switch (SettingsManager.Instance.DashboardCode)
				{
					case "tv":
					case "radio":
					case "cable":
						Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
						Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
						Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
						Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
						break;
					default:
						Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
						Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Sunday;
						Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
						Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
						break;
				}
				if (AppManager.Instance.RunPowerPoint())
					AppManager.Instance.RunForm();
			}
			else
			{
				if (AppManager.Instance.RunPowerPoint())
				{
					Utilities.Instance.ActivatePowerPoint(OnlineSchedulePowerPointHelper.Instance.PowerPointObject);
					AppManager.Instance.ActivateMainForm();
				}
			}
		}
	}
}