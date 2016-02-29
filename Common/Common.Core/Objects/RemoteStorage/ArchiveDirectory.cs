﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using SharpCompress.Common;
using SharpCompress.Reader;
using SharpCompress.Reader.Rar;

namespace Asa.Common.Core.Objects.RemoteStorage
{
	public class ArchiveDirectory : StorageDirectory
	{
		private readonly StorageDirectory _parentFoder;

		public ArchiveDirectory(string[] relativePathParts) : base(relativePathParts)
		{
			_parentFoder = GetParentFolder();
		}

		protected override async Task<bool> ExistsRemote()
		{
			return (await _parentFoder.GetRemoteFiles(itemName => itemName.StartsWith(Name, StringComparison.OrdinalIgnoreCase))).Any();
		}

		public async Task Download()
		{
			await Download(_parentFoder.LocalPath);
		}

		public async Task DownloadTo(string targetPath)
		{
			await Download(targetPath);
		}

		private async Task Download(string targetPath)
		{
			if (FileStorageManager.Instance.DataState == DataActualityState.Updated) return;

			var filter = new Func<string, bool>(
				itemName => itemName.StartsWith(Name, StringComparison.OrdinalIgnoreCase));

			var existedFiles = (_parentFoder.ExistsLocal() ? _parentFoder.GetLocalFiles(filter).ToArray() : new StorageFile[] {}).ToList();
			var archivePartFiles = (await _parentFoder.GetRemoteFiles(filter)).ToList();

			if (existedFiles.Count != archivePartFiles.Count)
				existedFiles.ForEach(file =>
				{
					try
					{
						if (File.Exists(file.LocalPath))
							File.Delete(file.LocalPath);
					}
					catch { }
				});

			var isOutdated = false;
			foreach (var archivePartFile in archivePartFiles)
			{
				await archivePartFile.Download();
				isOutdated |= archivePartFile.IsOutdated;
			}
			if (isOutdated || !TargetExists(targetPath))
			{
				Cleanup(targetPath);
				Int64 alreadyRead = 0;
				var contentLenght = archivePartFiles.Sum(s => new FileInfo(s.LocalPath).Length);
				using (var reader = RarReader.Open(archivePartFiles.Select(s => s.LocalPath).Select(File.OpenRead)))
				{
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

		private bool TargetExists(string targetPath)
		{
			var folderPath = targetPath;
			if (folderPath == _parentFoder.LocalPath)
				folderPath = LocalPath;
			return Directory.Exists(folderPath);
		}

		private void Cleanup(string targetPath)
		{
			if (!TargetExists(targetPath)) return;
			var folderPath = targetPath;
			if (folderPath == _parentFoder.LocalPath)
				folderPath = LocalPath;
			Utilities.DeleteFolder(folderPath);
		}
	}
}
