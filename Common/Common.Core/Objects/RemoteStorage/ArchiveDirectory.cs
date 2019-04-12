﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using SharpCompress.Common;
using SharpCompress.Readers;
using SharpCompress.Readers.Rar;

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
			if (FileStorageManager.Instance.DataState == DataActualityState.Updated ||
				FileStorageManager.Instance.UseLocalMode) return;

			var filter = new Func<string, bool>(
				itemName => itemName.StartsWith(String.Format("{0}.", Name), StringComparison.OrdinalIgnoreCase));

			var existedFiles = (_parentFoder.ExistsLocal() ? _parentFoder.GetLocalFiles(filter).ToArray() : new StorageFile[] { }).ToList();
			var archivePartFiles = (await _parentFoder.GetRemoteFiles(filter)).ToList();

			var successfullyExtracted = true;

			do
			{
				if (existedFiles.Count != archivePartFiles.Count ||
					!existedFiles.All(exsited => archivePartFiles.Any(actual => actual.Name == exsited.Name)) ||
					!successfullyExtracted)
					existedFiles.ForEach(file =>
					{
						try
						{
							if (File.Exists(file.LocalPath))
								File.Delete(file.LocalPath);
						}
						catch
						{
						}
					});

				var isOutdated = false;
				foreach (var archivePartFile in archivePartFiles)
				{
					var fileOutDated = await archivePartFile.IsOutOfDate(true);
					isOutdated |= fileOutDated;
					if (fileOutDated)
						await archivePartFile.Download(true);

				}
				if (isOutdated || !TargetExists(targetPath))
				{
					var fileStreams = new List<FileStream>();
					try
					{
						Cleanup(targetPath);
						Int64 alreadyRead = 0;
						var contentLenght = archivePartFiles.Sum(s => new FileInfo(s.LocalPath).Length);
						fileStreams.AddRange(archivePartFiles.Select(s => s.LocalPath).Select(File.OpenRead).ToList());
						using (var reader = RarReader.Open(fileStreams))
						{
							while (reader.MoveToNextEntry())
							{
								alreadyRead += reader.Entry.CompressedSize;
								reader.WriteEntryToDirectory(targetPath, new ExtractionOptions { ExtractFullPath = true, Overwrite = true });
								FileStorageManager.Instance.ShowExtractionProgress(new FileProcessingProgressEventArgs(NameOnly, contentLenght,
									alreadyRead));
							}
							FileStorageManager.Instance.ShowExtractionProgress(new FileProcessingProgressEventArgs(NameOnly, 100, 100));
						}
						successfullyExtracted = TargetExists(targetPath);
					}
					catch
					{
						successfullyExtracted = false;
					}
					finally
					{
						try
						{
							foreach (var fileStream in fileStreams)
							{
								fileStream.Close();
								fileStream.Dispose();
							}
						}
						catch { }
						fileStreams.Clear();
					}
				}
			} while (!successfullyExtracted);
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
