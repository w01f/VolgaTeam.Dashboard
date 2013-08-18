using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.Calendar.Controls.InteropClasses;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Calendar.Single
{
	internal static class Program
	{
		private static Mutex mutex; //Used to determine if the application is already open

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			bool firstInstance;
			string uniqueIdentifier = "Local\\CalendarBuilderApplication";
			mutex = new Mutex(false, uniqueIdentifier, out firstInstance);
			if (firstInstance)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
				Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Sunday;
				Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
				Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
				if (AppManager.Instance.RunPowerPoint())
					AppManager.Instance.RunForm();
			}
			else
			{
				if (AppManager.Instance.RunPowerPoint())
				{
					Utilities.Instance.ActivatePowerPoint(CalendarPowerPointHelper.Instance.PowerPointObject);
					Utilities.Instance.ActivateMiniBar();
					AppManager.ActivateMainForm();
				}
			}
		}
	}
}