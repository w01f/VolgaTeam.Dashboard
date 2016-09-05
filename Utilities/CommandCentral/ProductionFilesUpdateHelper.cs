using System;
using System.IO;
using System.Windows.Forms;

namespace CommandCentral
{
	public static class ProductionFilesUpdateHelper
	{
		public static void UpdateProductionFies(string sourceFilePath)
		{
			var dataRoot = Application.StartupPath;
			if (!"asa_data".Equals(Path.GetFileName(dataRoot), StringComparison.OrdinalIgnoreCase))
				return;
			var outgoingFolder = Path.Combine(Path.GetDirectoryName(dataRoot), "outgoing");
			if (!Directory.Exists(outgoingFolder)) return;
			foreach (var applicationRoot in Directory.GetDirectories(outgoingFolder))
			{
				var appDataFolder = Path.Combine(applicationRoot, "Data");
				if (!Directory.Exists(appDataFolder)) continue;
				UpdateFile(appDataFolder, sourceFilePath);
			}
		}

		private static void UpdateFile(string appDataFolder, string sourceFilePath)
		{
			foreach (var subFolder in Directory.GetDirectories(appDataFolder))
				UpdateFile(subFolder, sourceFilePath);
			var targetFilePath = Path.Combine(appDataFolder, Path.GetFileName(sourceFilePath));
			if (File.Exists(targetFilePath))
				File.Copy(sourceFilePath, targetFilePath, true);
		}
	}
}
