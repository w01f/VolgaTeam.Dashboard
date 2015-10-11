using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NewBizWiz.Core.Interop;
using SharpCompress.Common;
using SharpCompress.Reader;
using WebDAVClient.Helpers;
using WebDAVClient.Model;

namespace NewBizWiz.Core.Common
{
	public enum DataActualityState
	{
		None,
		NotExisted,
		Outdated,
		Updated
	}

	public class FileStorageManager
	{
		private const string RemoteStorageUrlTemplate = @"{0}/remote.php/webdav";

		private const string LocalAppSettingsFolderName = "adSalesApps Data";

		public const string IncomingFolderName = "outgoing";
		public const string OutgoingFolderName = "incoming";
		public const string CommonIncomingFolderName = "common";
		public const string LocalFilesFolderName = "local";

		private string _url;
		private string _login;
		private string _password;
		private string _dataFolderName;

		private static readonly FileStorageManager _instance = new FileStorageManager();

		public bool Connected { get; private set; }
		public string Version { get; private set; }

		public static FileStorageManager Instance
		{
			get { return _instance; }
		}

		public DataActualityState DataState { get; private set; }

		private string RemoteStorageUrl
		{
			get { return String.Format(RemoteStorageUrlTemplate, _url); }
		}

		public string LocalStoragePath
		{
			get
			{
				if (Connected)
					return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), LocalAppSettingsFolderName, _dataFolderName);
				return Path.GetTempPath();
			}
		}

		private FileStorageManager() { }

		public WebDAVClient.Client GetClient()
		{
			return GetClient(RemoteStorageUrl, _login, _password);
		}

		private WebDAVClient.Client GetClient(string url, string login, string password)
		{
			return new WebDAVClient.Client(
				new NetworkCredential
				{
					UserName = login,
					Password = password
				})
			{
				Server = url
			};
		}

		public async Task Init()
		{
			await InitCredentials();
			if (Connected)
				await CheckDataSate();
		}

		private async Task InitCredentials()
		{
			var clientConfigPath = Path.Combine(Path.GetDirectoryName(typeof(FileStorageManager).Assembly.Location), "client.txt");
			if (!File.Exists(clientConfigPath))
				throw new FileNotFoundException("File client.txt not found");

			var client = File.ReadAllText(clientConfigPath).Trim();

			_url = "https://adsalescloud.com";
			_login = "acct_cred";
			_password = "SzwX&4.2QF>~q!^L";

			var credentialsFile = new StorageFile(new[] { client, "credentials.txt" });
			if (await credentialsFile.Exists(true))
			{
				await credentialsFile.Download();

				foreach (var configLine in File.ReadAllLines(credentialsFile.LocalPath))
				{
					if (configLine.Contains("Site:"))
						_url = configLine.Replace("Site:", "").Trim();
					else if (configLine.Contains("Login:"))
						_login = configLine.Replace("Login:", "").Trim();
					else if (configLine.Contains("Password:"))
						_password = configLine.Replace("Password:", "").Trim();
					else if (configLine.Contains("DataFolderName:"))
						_dataFolderName = configLine.Replace("DataFolderName:", "").Trim();
				}

				Connected = true;

				if (!Directory.Exists(LocalStoragePath))
					Directory.CreateDirectory(LocalStoragePath);
			}
		}

		private async Task CheckDataSate()
		{
			DataState = DataActualityState.NotExisted;

			var versionFile = new StorageFile(new[] { IncomingFolderName, AppProfileManager.Instance.AppName, "version.txt" });
			if (!versionFile.ExistsLocal())
			{
				DataState = DataActualityState.NotExisted;
				await versionFile.Download();
			}
			else
			{
				var remoteFile = await GetClient().GetFile(versionFile.RemotePath);
				if (File.GetLastWriteTime(versionFile.LocalPath) < remoteFile.LastModified)
				{
					DataState = DataActualityState.Outdated;
					await versionFile.Download();
				}
				else
					DataState = DataActualityState.Updated;
			}
			Version = File.ReadAllText(versionFile.LocalPath);
		}
	}

	public abstract class StorageItem
	{
		public string[] RelativePathParts { get; private set; }

		public string LocalPath
		{
			get { return Path.Combine(FileStorageManager.Instance.LocalStoragePath, Path.Combine(RelativePathParts)); }
		}

		public string RemotePath
		{
			get { return String.Format("/{0}", String.Join(@"/", RelativePathParts)); }
		}

		public string Name
		{
			get { return Path.GetFileName(LocalPath); }
		}

		protected StorageItem(string[] relativePathParts)
		{
			RelativePathParts = relativePathParts;
		}

		public virtual async Task<bool> Exists(bool checkRemoteToo = false)
		{
			if (!checkRemoteToo)
				return ExistsLocal();
			return FileStorageManager.Instance.DataState == DataActualityState.Updated ?
				ExistsLocal() :
				await ExistsRemote();
		}

		public StorageDirectory GetParentFolder()
		{
			return new StorageDirectory(RelativePathParts.Reverse().Skip(1).Reverse().ToArray());
		}

		public abstract bool ExistsLocal();
		protected abstract Task<bool> ExistsRemote();
	}

	public class StorageDirectory : StorageItem
	{
		public StorageDirectory(string[] relativePathParts)
			: base(relativePathParts)
		{ }

		public static async Task<bool> Exists(string[] relativePathParts, bool checkRemoteToo = false)
		{
			var storageItem = new StorageDirectory(relativePathParts);
			return await storageItem.Exists(checkRemoteToo);
		}

		public static async Task CreateSubFolder(string[] relativePathParts, string name, bool remoteToo = false)
		{
			var storageItem = new StorageDirectory(relativePathParts);
			await storageItem.CreateSubFolder(name, remoteToo);
		}

		public override bool ExistsLocal()
		{
			return Directory.Exists(LocalPath);
		}

		protected override async Task<bool> ExistsRemote()
		{
			try
			{
				await FileStorageManager.Instance.GetClient().GetFolder(RemotePath);
				return true;
			}
			catch (WebDAVException exception)
			{
				if (exception.Message.Contains("Status Code: NotFound"))
					return false;
				throw;
			}
		}

		private async Task CreateSubFolder(string name, bool remoteToo = false)
		{
			var subFolderLocalPath = Path.Combine(LocalPath, name);
			if (!Directory.Exists(subFolderLocalPath))
				Directory.CreateDirectory(subFolderLocalPath);
			if (remoteToo)
			{
				var client = FileStorageManager.Instance.GetClient();
				await client.CreateDir(RemotePath, String.Format(@"/{0}", name));
			}
		}

		public IEnumerable<StorageFile> GetFiles(Func<string, bool> filter = null, bool recursive = false)
		{
			if (filter == null)
				filter = item => true;

			var items = new List<StorageFile>();
			if (recursive)
			{
				foreach (var directoryPath in Directory.GetDirectories(LocalPath))
				{
					var subDirectory = new StorageDirectory(RelativePathParts.Merge(Path.GetFileName(directoryPath)));
					items.AddRange(subDirectory.GetFiles(filter, true));
				}
			}
			items.AddRange(Directory.GetFiles(LocalPath)
					.Where(filePath => filter(Path.GetFileName(filePath)))
					.Select(filePath => new StorageFile(RelativePathParts.Merge(Path.GetFileName(filePath)))));
			return items;
		}

		public IEnumerable<StorageDirectory> GetFolders(Func<string, bool> filter = null)
		{
			if (filter == null)
				filter = item => true;

			var items = new List<StorageDirectory>();
			items.AddRange(Directory.GetDirectories(LocalPath)
					.Where(directoryPath => filter(Path.GetFileName(directoryPath)))
					.Select(directoryPath => new StorageDirectory(RelativePathParts.Merge(Path.GetFileName(directoryPath)))));
			return items;
		}

		public async Task Allocate(bool remoteToo)
		{
			if (!Directory.Exists(LocalPath))
				Directory.CreateDirectory(LocalPath);
			if (remoteToo && !await ExistsRemote())
			{
				await GetParentFolder().Allocate(true);
				await CreateSubFolder(RelativePathParts, String.Empty, true);
			}
		}
	}

	public class ArchiveDirectory : StorageDirectory
	{
		private readonly ArchiveFile _archiveFile;

		public ArchiveDirectory(string[] relativePathParts)
			: base(relativePathParts)
		{
			_archiveFile = new ArchiveFile(GetParentFolder().RelativePathParts.Merge(String.Format("{0}.rar", Name)));
		}

		public async Task Download()
		{
			if (FileStorageManager.Instance.DataState == DataActualityState.Updated) return;
			await _archiveFile.Download();
		}
	}

	public class StorageFile : StorageItem
	{
		private readonly Item _remoteSource;
		protected bool _isOutdated;

		public string Extension
		{
			get { return Path.GetExtension(LocalPath); }
		}

		public StorageFile(string[] relativePathParts) : base(relativePathParts) { }

		public StorageFile(string[] parentPathParts, Item remoteSource)
			: base(parentPathParts.Merge(remoteSource.GetName()))
		{
			_remoteSource = remoteSource;
		}

		public static async Task<bool> Exists(string[] relativePathParts, bool checkRemoteToo = false)
		{
			var storageItem = new StorageFile(relativePathParts);
			return await storageItem.Exists(checkRemoteToo);
		}

		public override bool ExistsLocal()
		{
			return File.Exists(LocalPath);
		}

		protected override async Task<bool> ExistsRemote()
		{
			try
			{
				await FileStorageManager.Instance.GetClient().GetFile(RemotePath);
				return true;
			}
			catch (WebDAVException exception)
			{
				if (exception.Message.Contains("Status Code: NotFound"))
					return false;
				throw;
			}
		}

		public async Task Upload()
		{
			var tempFile = Path.GetTempFileName();
			File.Copy(LocalPath, tempFile, true);
			var client = FileStorageManager.Instance.GetClient();
			await AllocateParentFolder(true);
			await client.Upload(RemotePath, File.OpenRead(tempFile), String.Empty);
		}

		public virtual async Task Download()
		{
			var client = FileStorageManager.Instance.GetClient();
			if (ExistsLocal() && FileStorageManager.Instance.DataState == DataActualityState.Updated)
				return;
			var remoteFile = _remoteSource ?? await client.GetFile(RemotePath);
			_isOutdated = !(ExistsLocal() && File.GetLastWriteTime(LocalPath) >= remoteFile.LastModified);
			if (_isOutdated)
			{
				AllocateParentFolder();
				using (var remoteStream = await client.Download(RemotePath))
				{
					if (remoteStream != null)
					{
						using (var localStream = File.Create(LocalPath))
						{
							var buffer = new byte[remoteFile.ContentLength.HasValue ? remoteFile.ContentLength.Value : 1024];
							int bytesRead;
							do
							{
								bytesRead = remoteStream.Read(buffer, 0, buffer.Length);
								localStream.Write(buffer, 0, bytesRead);
							} while (bytesRead > 0);
							localStream.Close();
						}
						remoteStream.Close();
					}
				}
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

	public class ArchiveFile : StorageFile
	{
		public ArchiveFile(string[] relativePathParts) : base(relativePathParts) { }

		public override async Task Download()
		{
			await base.Download();
			if (_isOutdated)
			{
				using (Stream stream = File.OpenRead(LocalPath))
				{
					var reader = ReaderFactory.Open(stream);
					while (reader.MoveToNextEntry())
					{
						if (!reader.Entry.IsDirectory)
							reader.WriteEntryToDirectory(GetParentFolder().LocalPath, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
					}
				}
			}
		}
	}
}
