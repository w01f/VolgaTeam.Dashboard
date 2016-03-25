using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AdSalesBrowser.PowerPoint
{
	public class PowerPointManager
	{
		private const string LauncherTemplatesFolderName = "LauncherTemplates";
		private const string LauncherTemplate43FileName = "adSALESapps43.potx";
		private const string LauncherTemplate34FileName = "adSALESapps34.potx";
		private const string LauncherTemplate169FileName = "adSALESapps169.potx";

		public static PowerPointManager Instance { get; } = new PowerPointManager();

		public bool CheckPowerPointRunning()
		{
			if (PowerPointSingleton.Instance.Connect())
				return true;
			if (MessageBox.Show(String.Format("PowerPoint is required to run this application.{0}Do you want to go ahead and open PowerPoint?", Environment.NewLine), "Open PowerPoint", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
				return false;
			RunPowerPointLoader();
			return false;
		}

		private void RunPowerPointLoader()
		{
			KillPowerPoint();

			var launcherTemplatesFolderPath = Path.Combine(
				Path.GetDirectoryName(typeof(PowerPointManager).Assembly.Location),
				LauncherTemplatesFolderName);

			var launchertemplateFilePath = String.Empty;

			using (var form = new FormSlideSize())
			{
				var result = form.ShowDialog(FormMain.Instance);
				switch (result)
				{
					case DialogResult.Yes:
						launchertemplateFilePath = Path.Combine(launcherTemplatesFolderPath, LauncherTemplate169FileName);
						break;
					case DialogResult.No:
						launchertemplateFilePath = Path.Combine(launcherTemplatesFolderPath, LauncherTemplate43FileName);
						break;
					case DialogResult.Retry:
						launchertemplateFilePath = Path.Combine(launcherTemplatesFolderPath, LauncherTemplate34FileName);
						break;
				}
			}

			if (!String.IsNullOrEmpty(launchertemplateFilePath))
			{
				AppManager.Instance.ShowFloater(() =>
				{
					var process = new Process();
					process.StartInfo.FileName = launchertemplateFilePath;
					process.StartInfo.UseShellExecute = true;
					process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
					process.Start();
				});
			}
		}

		private void KillPowerPoint()
		{
			Process.GetProcesses().Where(p => p.ProcessName.ToUpper().Contains("POWERPNT")).ToList().ForEach(p => p.Kill());
		}
	}
}
