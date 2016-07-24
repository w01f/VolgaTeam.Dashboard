using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PowerPointLauncher
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Process.GetProcesses().Where(p => p.ProcessName.ToUpper().Contains("POWERPNT")).ToList().ForEach(p => p.Kill());
			var executableName = Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.FileName);
			var rootPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			foreach (var f in Directory.GetFiles(rootPath, String.Format("{0}.pot?", executableName)))
			{
				try
				{
					Process.Start(new ProcessStartInfo(f) { UseShellExecute = true });
					break;
				}
				catch
				{
				}
			}
		}
	}
}
