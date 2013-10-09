using System;
using System.Diagnostics;
using System.Windows.Forms;
using NewBizWiz.Core.Common;

namespace NewBizWiz.MiniBar
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Process currentProcess = Process.GetCurrentProcess();

			foreach (Process p in Process.GetProcessesByName(currentProcess.ProcessName))
			{
				try
				{
					if (p.Id != currentProcess.Id)
						p.Kill();
				}
				catch {}
			}
			if (args != null && args.Length > 0)
				AppManager.Instance.ShowHidden = args[0].ToLower().Equals("-h");

			lock (AppManager.Locker)
			{
				RegistryHelper.ShowHidden = AppManager.Instance.ShowHidden;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			AppManager.Instance.RunForm();
		}
	}
}