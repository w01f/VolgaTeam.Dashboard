using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

namespace AdBar.PatchUpdater
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			var patchRoot = args != null && args.Length > 0 ? args[0] : null;
			if (String.IsNullOrEmpty(patchRoot)) return;

			try
			{
				foreach (var p in Process.GetProcessesByName("adsalesapps").ToList())
					p.Kill();
			}
			catch { }

			var configPath = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "updater.xml");
			if (!File.Exists(configPath)) return;
			var document = new XmlDocument();
			document.Load(configPath);
			foreach (var patchNode in document.SelectNodes("/Config/Patch").OfType<XmlNode>())
			{
				var patchConfig = PatchConfig.FromXml(patchNode);
				foreach (var existedFileName in patchConfig.ExistedFileNames)
				{
					var existedFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), existedFileName);
					if (File.Exists(existedFilePath))
						try
						{
							File.Delete(existedFilePath);
						}
						catch { }
				}

				var newFilePath = Path.Combine(patchRoot, patchConfig.TargetFileName);
				if (File.Exists(newFilePath))
				{
					var desktopCopy = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
						patchConfig.TargetFileName);
					try
					{
						File.Copy(newFilePath, desktopCopy, true);
						Process.Start(desktopCopy);
					}
					catch { }
				}
			}
		}
	}
}
