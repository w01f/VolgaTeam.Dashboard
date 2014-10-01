using System;
using System.Threading;
using System.Windows.Forms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.QuickShare.Controls.InteropClasses;

namespace NewBizWiz.QuickShare.Single.TV
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
			const string uniqueIdentifier = "Local\\TVQuickShareApplication";
			new Mutex(false, uniqueIdentifier, out firstInstance);
			if (firstInstance)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				MediaMetaData.Instance.Init<TVPackageSettingsManager, TVListManager>(MediaDataType.TV);
				if (AppManager.Instance.RunPowerPoint())
					AppManager.Instance.RunForm();
			}
			else
			{
				if (!AppManager.Instance.RunPowerPoint()) return;
				Utilities.Instance.ActivatePowerPoint(QuickSharePowerPointHelper.Instance.PowerPointObject);
				AppManager.Instance.ActivateMainForm();
			}
		}
	}
}
