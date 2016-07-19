using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace NinjaUpdate
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				Process.GetProcesses().Where(p => p.ProcessName.ToUpper().Contains("ADBAR")).ToList().ForEach(p => p.Kill());
			}
			catch
			{
			}

			var sourcePath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
				"newlocaldirect.com",
				"sync",
				"Incoming",
				"applications",
				"z_sales_ninja",
				"paste");
			var destinationPath = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
				"newlocaldirect.com");

			CopyFolder(sourcePath, destinationPath);

			var executablePath = Path.Combine(destinationPath, "sales_ninja_mg.exe");
			if (File.Exists(executablePath))
				try
				{
					Process.Start(executablePath);
				}
				catch
				{
				}
		}

		private static void CopyFolder(string sourcePath, string destinationPath)
		{
			foreach (var sourceSubFolder in Directory.GetDirectories(sourcePath))
			{
				var folderName = Path.GetFileName(sourceSubFolder);
				var destinationSubFolder = Path.Combine(destinationPath, folderName);
				if (!Directory.Exists(destinationSubFolder))
					Directory.CreateDirectory(destinationSubFolder);
				CopyFolder(sourceSubFolder, destinationSubFolder);
			}
			foreach (var sourceFile in Directory.GetFiles(sourcePath))
			{
				var fileName = Path.GetFileName(sourceFile);
				var destinationFile = Path.Combine(destinationPath, fileName);
				try
				{
					File.Copy(sourceFile, destinationFile, true);
				}
				catch
				{
				}
			}
		}
	}
}
