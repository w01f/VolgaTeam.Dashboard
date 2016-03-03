using System;
using System.Threading;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Dashboard.InteropClasses;

namespace Asa.Dashboard
{
	internal static class Program
	{
		private static Mutex _mutex;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			bool firstInstance;
			const string uniqueIdentifier = "Local\\NewBizWizApplication";
			_mutex = new Mutex(false, uniqueIdentifier, out firstInstance);
			if (firstInstance)
			{
				AppDomain.CurrentDomain.AssemblyResolve += SharedAssemblyHelper.OnAssemblyResolve;

				var onlySlides = args != null && args.Length > 0 && args[0].Equals("addslides", StringComparison.OrdinalIgnoreCase);
				RegistryHelper.MaximizeMainForm = false;
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				AppManager.Instance.RunForm(onlySlides);
			}
			else
			{
				Utilities.ActivatePowerPoint(DashboardPowerPointHelper.Instance.PowerPointObject);
				AppManager.Instance.ActivateMainForm();
			}
		}
	}
}