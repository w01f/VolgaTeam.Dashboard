using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Asa.Core.Common;

namespace Asa.Bar.Plugins.Common.BusinessClasses
{
	public class PluginController
	{
		private readonly string _powerPointLoaderPath= String.Format(@"{0}\newlocaldirect.com\app\PowerPointLoader.exe", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
	
		public void RunPowerPoint()
		{
			if (File.Exists(_powerPointLoaderPath))
			{
				var process = new Process();
				process.StartInfo.FileName = _powerPointLoaderPath;
				process.Start();

				var thread = new Thread(delegate()
				{
					while (Process.GetProcesses().Any(x => x.ProcessName.ToLower().Contains(Path.GetFileNameWithoutExtension(_powerPointLoaderPath).ToLower())))
						Thread.Sleep(1000);
				});
				thread.Start();

				while (thread.IsAlive)
					Application.DoEvents();
			}
			else
				Utilities.Instance.ShowWarning("Couldn't find PowerPointLoader app");
		}

		public bool AplicationDetected()
		{
			return Process.GetProcesses().Any(x => x.ProcessName.Contains("adSALESapp") || x.ProcessName.Contains("ProSlides") || x.ProcessName.Contains("OneDomain"));
		}

		public void CloseActiveApplications()
		{
			var processList = Process.GetProcesses();
			foreach (Process process in processList.Where(x => x.ProcessName.ToLower().Contains("adsalesapp") || x.ProcessName.ToLower().Contains("proslides") || x.ProcessName.ToLower().Contains("onedomain") || x.ProcessName.ToLower().Contains("salesdepot") || x.ProcessName.ToLower().Contains("medialibrary")))
				process.Kill();
		}
	}
}
