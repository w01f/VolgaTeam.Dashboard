using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Ionic.Zip;

namespace Asa.Reset
{
	static class ResetHelper
	{
		private const string BarProcessName = "adsalesapps";
		private const string BarFolderName = "adSalesApps SFX";
		private const string LocalDataRootFolderName = "adSalesApps Data";
		private const string IncomingFolderName = "incoming";

		public static void ResetApp()
		{
			KillBarProcess();
			BackupUserFiles();
			DeleteAppFolders();
		}

		private static void KillBarProcess()
		{
			foreach (var process in Process.GetProcesses().Where(process => Path.GetFileName(process.ProcessName).Equals(BarProcessName, StringComparison.OrdinalIgnoreCase)))
				process.Kill();
		}

		private static void BackupUserFiles()
		{
			var dataRootFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), LocalDataRootFolderName);
			using (var zip = new ZipFile())
			{
				foreach (var appAccountPath in Directory.GetDirectories(dataRootFolderPath))
				{
					var accountName = Path.GetFileName(appAccountPath);
					var incomingFolderPath = Path.Combine(appAccountPath, IncomingFolderName);
					if (!Directory.Exists(incomingFolderPath)) continue;
					zip.AddDirectory(incomingFolderPath, Path.Combine(accountName, IncomingFolderName));
				}
				zip.Save(Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
					String.Format("old_asa_files_{0}.zip", DateTime.Now.ToString("MMddyy_hhmm"))));
			}
		}

		private static void DeleteAppFolders()
		{
			var dataRootFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), LocalDataRootFolderName);
			if (Directory.Exists(dataRootFolderPath))
				DeleteFolder(new DirectoryInfo(dataRootFolderPath));
			var barFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), BarFolderName);
			if (Directory.Exists(barFolderPath))
				DeleteFolder(new DirectoryInfo(barFolderPath));
		}

		private static void MakeFolderAvailable(DirectoryInfo folder)
		{
			try
			{
				foreach (var subFolder in folder.GetDirectories())
					MakeFolderAvailable(subFolder);
				foreach (FileInfo file in folder.GetFiles())
					if (File.Exists(file.FullName))
						File.SetAttributes(file.FullName, FileAttributes.Normal);
			}
			catch { }
		}

		private static void DeleteFolder(DirectoryInfo folder, string filter = "")
		{
			try
			{
				if (!folder.Exists) return;
				MakeFolderAvailable(folder);
				foreach (var subFolder in folder.GetDirectories())
					DeleteFolder(subFolder, filter);
				foreach (var file in folder.GetFiles())
				{
					try
					{
						if (File.Exists(file.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
							File.Delete(file.FullName);
					}
					catch
					{
						try
						{
							Thread.Sleep(100);
							if (File.Exists(file.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
								File.Delete(file.FullName);
						}
						catch { }
					}
				}
				try
				{
					if (Directory.Exists(folder.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
						Directory.Delete(folder.FullName, false);
				}
				catch
				{
					try
					{
						Thread.Sleep(100);
						if (Directory.Exists(folder.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
							Directory.Delete(folder.FullName, false);
					}
					catch { }
				}
			}
			catch { }
		}
	}
}
