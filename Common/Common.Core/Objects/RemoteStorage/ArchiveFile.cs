using System;
using System.IO;
using System.Threading.Tasks;
using Asa.Common.Core.Helpers;
using SharpCompress.Common;
using SharpCompress.Reader;

namespace Asa.Common.Core.Objects.RemoteStorage
{
	public class ArchiveFile : StorageFile
	{
		private readonly ArchiveDirectory _asociatedDirectory;

		public ArchiveFile(string[] relativePathParts, ArchiveDirectory asociatedDirectory)
			: base(relativePathParts)
		{
			_asociatedDirectory = asociatedDirectory;
		}

		public override async Task Download(bool force = false)
		{
			await base.Download(force);
			if (_isOutdated || !_asociatedDirectory.ExistsLocal())
			{
				if (_asociatedDirectory.ExistsLocal())
					Utilities.DeleteFolder(_asociatedDirectory.LocalPath);
				var contentLenght = new FileInfo(LocalPath).Length;
				Int64 alreadyRead = 0;
				using (Stream stream = File.OpenRead(LocalPath))
				{
					var reader = ReaderFactory.Open(stream);
					while (reader.MoveToNextEntry())
					{
						if (reader.Entry.IsDirectory) continue;
						alreadyRead += reader.Entry.CompressedSize;
						reader.WriteEntryToDirectory(GetParentFolder().LocalPath, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
						FileStorageManager.Instance.ShowExtractionProgress(new FileProcessingProgressEventArgs(NameOnly, contentLenght, alreadyRead));
					}
					FileStorageManager.Instance.ShowExtractionProgress(new FileProcessingProgressEventArgs(NameOnly, 100, 100));
				}
			}
		}

		public async Task DownloadTo(string targetPath, bool force = false)
		{
			await base.Download(force);
			if (_isOutdated || !Directory.Exists(targetPath))
			{
				if (Directory.Exists(targetPath))
					Utilities.DeleteFolder(targetPath);
				var contentLenght = new FileInfo(LocalPath).Length;
				Int64 alreadyRead = 0;
				using (Stream stream = File.OpenRead(LocalPath))
				{
					var reader = ReaderFactory.Open(stream);
					while (reader.MoveToNextEntry())
					{
						if (reader.Entry.IsDirectory) continue;
						alreadyRead += reader.Entry.CompressedSize;
						reader.WriteEntryToDirectory(targetPath, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
						FileStorageManager.Instance.ShowExtractionProgress(new FileProcessingProgressEventArgs(NameOnly, contentLenght, alreadyRead));
					}
					FileStorageManager.Instance.ShowExtractionProgress(new FileProcessingProgressEventArgs(NameOnly, 100, 100));
				}
			}
		}
	}
}
