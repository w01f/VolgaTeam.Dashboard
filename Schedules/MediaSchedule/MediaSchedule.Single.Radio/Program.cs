using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;

namespace NewBizWiz.MediaSchedule.Single.Radio
{
	static class Program
	{
		private static Mutex mutex; //Used to determine if the application is already open

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			bool firstInstance;
			string uniqueIdentifier = "Local\\RadioSellerApplication";
			mutex = new Mutex(false, uniqueIdentifier, out firstInstance);
			if (firstInstance)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				MediaMetaData.Instance.Init<RadioSettingsManager, RadioListManager>(MediaDataType.Radio);
				if (AppManager.Instance.RunPowerPoint())
					AppManager.Instance.RunForm();
			}
			else
			{
				if (!AppManager.Instance.RunPowerPoint()) return;
				Utilities.Instance.ActivatePowerPoint(MediaSchedulePowerPointHelper.Instance.PowerPointObject);
				AppManager.Instance.ActivateMainForm();
			}
		}
	}
}
