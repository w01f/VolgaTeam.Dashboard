using System;
using System.Threading.Tasks;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;

namespace Asa.Common.Core.Objects.RemoteStorage
{
	public class ArchiveDirectory : StorageDirectory
	{
		private readonly ArchiveFile _archiveFile;

		public ArchiveDirectory(string[] relativePathParts)
			: base(relativePathParts)
		{
			_archiveFile = new ArchiveFile(GetParentFolder().RelativePathParts.Merge(String.Format("{0}.rar", Name)), this);
		}

		public async Task Download()
		{
			if (FileStorageManager.Instance.DataState == DataActualityState.Updated) return;
			await _archiveFile.Download();
		}

		public async Task DownloadTo(string targetPath)
		{
			if (FileStorageManager.Instance.DataState == DataActualityState.Updated) return;
			await _archiveFile.DownloadTo(targetPath);
		}

		protected async override Task<bool> ExistsRemote()
		{
			return await _archiveFile.Exists(true);
		}
	}
}
