using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asa.Core.Common
{
	class RemoteUploadManager
	{
		private static readonly List<string> _processedFiles = new List<string>();

		public static async Task Upload(StorageFile targetFile)
		{
			if (_processedFiles.Contains(targetFile.LocalPath)) return;
			_processedFiles.Add(targetFile.LocalPath);
			await targetFile.Upload();
			_processedFiles.Remove(targetFile.LocalPath);
		}
	}
}
