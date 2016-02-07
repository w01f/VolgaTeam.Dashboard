using System;
using System.Threading;
using System.Windows.Forms;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using Asa.Media.Controls.InteropClasses;

namespace Asa.Media.Single.TV
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
			var mutex = new Mutex(false, uniqueIdentifier, out firstInstance);
			if (firstInstance)
			{
				AppDomain.CurrentDomain.AssemblyResolve += SharedAssemblyHelper.OnAssemblyResolve;
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				AppManager.Instance.RunApplication(MediaDataType.TVSchedule);
			}
			else
			{
				Utilities.ActivatePowerPoint(RegularMediaSchedulePowerPointHelper.Instance.PowerPointObject);
				AppManager.Instance.ActivateMainForm();
			}
			GC.KeepAlive(mutex);
		}
	}
}
