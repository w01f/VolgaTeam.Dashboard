using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace Asa.Common.Core.Helpers
{
	public static class ZipHelper
	{
		public static void CompressFiles(IEnumerable<string> filesPaths, string compressedFilePath)
		{
			using (var zip = new ZipFile())
			{
				zip.AddFiles(filesPaths, false, "");
				zip.Save(compressedFilePath);
			}
		}

		public static IEnumerable<string> ExtractFiles(string compressedFilePath)
		{
			var zip = ZipFile.Read(compressedFilePath);
			var tempFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			if (!Directory.Exists(tempFolder))
				Directory.CreateDirectory(tempFolder);
			foreach (var e in zip)
			{
				e.Extract(tempFolder, ExtractExistingFileAction.OverwriteSilently);
			}
			return Directory.GetFiles(tempFolder);
		}
	}
}
