using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.MediaSchedule.Controls.InteropClasses;

namespace NewBizWiz.MediaSchedule.Single.TV
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			bool firstInstance;
			const string uniqueIdentifier = "Local\\TVSellerApplication";
			new Mutex(false, uniqueIdentifier, out firstInstance);
			if (firstInstance)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				MediaMetaData.Instance.Init<TVSettingsManager, TVListManager>(MediaDataType.TV);
				if (AppManager.Instance.RunPowerPoint())
					AppManager.Instance.RunForm();
			}
			else
			{
				if (!AppManager.Instance.RunPowerPoint()) return;
				Utilities.Instance.ActivatePowerPoint(RegularMediaSchedulePowerPointHelper.Instance.PowerPointObject);
				AppManager.Instance.ActivateMainForm();
			}
		}
	}
}
