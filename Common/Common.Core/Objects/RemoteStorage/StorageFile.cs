﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using WebDAVClient.Helpers;
using WebDAVClient.Model;

namespace Asa.Common.Core.Objects.RemoteStorage
{
	public class StorageFile : StorageItem
	{
		private readonly Item _remoteSource;

		public string Extension => Path.GetExtension(LocalPath);

		public StorageFile(string[] relativePathParts) : base(relativePathParts) { }

		public StorageFile(string[] parentPathParts, Item remoteSource)
			: base(parentPathParts.Merge(remoteSource.GetName()))
		{
			_remoteSource = remoteSource;
		}

		public override bool ExistsLocal()
		{
			return File.Exists(LocalPath);
		}

		protected override async Task<bool> ExistsRemote()
		{
			try
			{
				if (FileStorageManager.Instance.UseLocalMode) return false;
				await FileStorageManager.Instance.GetClient().GetFile(RemotePath);
				return true;
			}
			catch (WebDAVException exception)
			{
				return false;
			}
			catch (HttpRequestException e)
			{
				FileStorageManager.Instance.SwitchToLocalMode();
				return FileStorageManager.Instance.UseLocalMode;
			}
		}

		public async Task Upload()
		{
			if (FileStorageManager.Instance.UseLocalMode) return;
			var tempFile = Path.GetTempFileName();
			File.Copy(LocalPath, tempFile, true);
			var client = FileStorageManager.Instance.GetClient();
			await AllocateParentFolder(true);
			try
			{
				await client.Upload(RemotePath, File.OpenRead(tempFile), String.Empty);
			}
			catch (HttpRequestException e)
			{
				FileStorageManager.Instance.SwitchToLocalMode();
			}
			catch { }
		}

		public async Task<bool> IsOutOfDate(bool force = false)
		{
			var client = FileStorageManager.Instance.GetClient();
			var remoteFile = _remoteSource!= null && !force?_remoteSource: await client.GetFile(RemotePath);
			return !(ExistsLocal() && File.GetLastWriteTime(LocalPath) >= remoteFile.LastModified);
		}

		public virtual async Task Download(bool force = false)
		{
			try
			{
				var client = FileStorageManager.Instance.GetClient();
				if ((ExistsLocal() && FileStorageManager.Instance.DataState == DataActualityState.Updated && !force) || FileStorageManager.Instance.UseLocalMode)
					return;
				var isOutdated = await IsOutOfDate(force);
				if (isOutdated)
				{
					var remoteFile = _remoteSource != null && !force ? _remoteSource : await client.GetFile(RemotePath);

					AllocateParentFolder();
					var fullyLoaded = false;
					do
					{
						try
						{
							using (var localStream = File.Create(LocalPath))
							{
								using (var remoteStream = await client.Download(RemotePath))
								{
									if (remoteStream != null)
									{
										var alreadyRead = 0;
										var contentLenght = remoteFile.ContentLength.HasValue ? remoteFile.ContentLength.Value : 0;
										var bufferSize = contentLenght / 10;
										var buffer = new byte[bufferSize];
										int bytesRead;
										do
										{
											bytesRead = remoteStream.Read(buffer, 0, buffer.Length);
											alreadyRead += bytesRead;
											FileStorageManager.Instance.ShowDownloadProgress(new FileProcessingProgressEventArgs(NameOnly, contentLenght, alreadyRead));
											localStream.Write(buffer, 0, bytesRead);
										} while (bytesRead > 0);
									}
									remoteStream.Close();
									fullyLoaded = true;
								}
								localStream.Close();
							}
						}
						catch (IOException)
						{
						}
					}
					while (!fullyLoaded);
				}
			}
			catch (WebDAVException)
			{
				throw new FileNotFoundException(String.Format("File {0} not found", LocalPath));
			}
			catch (HttpRequestException)
			{
				FileStorageManager.Instance.SwitchToLocalMode();
			}
		}

		public async Task AllocateParentFolder(bool checkRemoteToo)
		{
			await GetParentFolder().Allocate(checkRemoteToo);
		}

		public void AllocateParentFolder()
		{
			AsyncHelper.RunSync(() => GetParentFolder().Allocate(false));
		}
	}
}
