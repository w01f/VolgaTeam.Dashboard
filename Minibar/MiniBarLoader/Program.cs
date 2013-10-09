using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NewBizWiz.MiniBar.BusinessClasses;

namespace NewBizWiz.MiniBarLoader
{
	internal static class Program
	{
		[STAThread]
		private static void Main(string[] args)
		{
			string minibarPath = String.Format(@"{0}\newlocaldirect.com\app\Minibar\MiniBar.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			SettingsManager.Instance.LoadSettings();
			SettingsManager.Instance.LoadMinibarApplicationSettings();
			if (!SettingsManager.Instance.AutoRunNormal && !SettingsManager.Instance.AutoRunHidden && !SettingsManager.Instance.AutoRunFloat) return;
			if (!File.Exists(minibarPath)) return;
			try
			{
				var processes = Process.GetProcesses();
				foreach (Process process in processes.Where(x => x.ProcessName.ToLower().Contains("minibar") && !x.ProcessName.ToLower().Contains("loader")))
					process.Kill();
			}
			catch {}

			bool useOptions = true;
			if (args != null && args.Length > 0)
				useOptions = !args[0].ToLower().Equals("-f");

			var minibarProcess = new Process();
			if (SettingsManager.Instance.AutoRunHidden && useOptions)
				minibarProcess.StartInfo.Arguments = "-h";
			minibarProcess.StartInfo.FileName = minibarPath;
			minibarProcess.Start();
		}
	}
}