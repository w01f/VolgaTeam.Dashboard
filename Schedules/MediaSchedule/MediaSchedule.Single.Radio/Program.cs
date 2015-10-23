using System;
using System.Threading;
using System.Windows.Forms;
using Asa.Core.Common;
using Asa.Core.MediaSchedule;
using Asa.MediaSchedule.Controls.InteropClasses;

namespace Asa.MediaSchedule.Single.Radio
{
	static class Program
	{
		private static Mutex _mutex; //Used to determine if the application is already open

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			bool firstInstance;
			string uniqueIdentifier = "Local\\RadioSellerApplication";
			_mutex = new Mutex(false, uniqueIdentifier, out firstInstance);
			if (firstInstance)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				AppManager.Instance.RunApplication(MediaDataType.RadioSchedule);
			}
			else
			{
				Utilities.Instance.ActivatePowerPoint(RegularMediaSchedulePowerPointHelper.Instance.PowerPointObject);
				AppManager.Instance.ActivateMainForm();
			}
		}
	}
}
