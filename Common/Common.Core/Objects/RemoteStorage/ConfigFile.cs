﻿using System.IO;
using System.Threading.Tasks;
using Asa.Common.Core.Helpers;

namespace Asa.Common.Core.Objects.RemoteStorage
{
	public class ConfigFile : StorageFile
	{
		public ConfigFile(string[] relativePathParts) : base(relativePathParts) { }

		protected override async Task<bool> ExistsRemote()
		{
			try
			{
				await FileStorageManager.Instance.GetClient().GetFile(RemotePath);
				return true;
			}
			catch
			{
				return ExistsLocal();
			}
		}

		public override async Task Download(bool force = false)
		{
			try
			{
				var client = FileStorageManager.Instance.GetClient();
				var remoteFile = await client.GetFile(RemotePath);
				var isOutdated = !(ExistsLocal() && File.GetLastWriteTime(LocalPath) >= remoteFile.LastModified);
				if (isOutdated)
				{
					AllocateParentFolder();
					using (var remoteStream = await client.Download(RemotePath))
					{
						if (remoteStream != null)
						{
							using (var localStream = File.Create(LocalPath))
							{
								var contentLenght = remoteFile.ContentLength ?? 0;
								var buffer = new byte[1024];
								int bytesRead;
								int alreadyRead = 0;
								do
								{
									bytesRead = remoteStream.Read(buffer, 0, buffer.Length);
									alreadyRead += bytesRead;
									FileStorageManager.Instance.ShowDownloadProgress(new FileProcessingProgressEventArgs(NameOnly, contentLenght, alreadyRead));
									localStream.Write(buffer, 0, bytesRead);
								}
								while (bytesRead > 0);
								localStream.Close();
							}
							remoteStream.Close();
						}
					}
				}
			}
			catch
			{
			}
		}
	}
}
