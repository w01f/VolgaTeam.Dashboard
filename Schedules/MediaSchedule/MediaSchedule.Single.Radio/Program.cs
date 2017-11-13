using System;
using System.Threading;
using System.Windows.Forms;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using Asa.Media.Controls.BusinessClasses.Managers;

namespace Asa.Media.Single.Radio
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
			var mutex = new Mutex(false, "Local\\RadioSellerApplication", out firstInstance);
			if (firstInstance)
			{
				AppDomain.CurrentDomain.AssemblyResolve += SharedAssemblyHelper.OnAssemblyResolve;
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				AppManager.Instance.RunApplication(MediaDataType.RadioSchedule);
			}
			else
			{
				Utilities.ActivatePowerPoint(BusinessObjects.Instance.PowerPointManager.Processor.PowerPointObject);
				AppManager.Instance.ActivateMainForm();
			}
			GC.KeepAlive(mutex);
		}
	}
}
