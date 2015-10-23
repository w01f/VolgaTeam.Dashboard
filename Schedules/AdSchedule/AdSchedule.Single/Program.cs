using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Asa.AdSchedule.Controls.InteropClasses;
using Asa.Core.Common;

namespace Asa.AdSchedule.Single
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
			new Mutex(false, uniqueIdentifier, out firstInstance);
			if (firstInstance)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
				Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Sunday;
				Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
				Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
				AppManager.Instance.RunApplication();
			}
			else
			{
				Utilities.Instance.ActivatePowerPoint(AdSchedulePowerPointHelper.Instance.PowerPointObject);
				AppManager.Instance.ActivateMainForm();
			}
		}
	}
}