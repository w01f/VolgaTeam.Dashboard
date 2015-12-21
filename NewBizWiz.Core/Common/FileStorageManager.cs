using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Asa.Core.Interop;
using SharpCompress.Common;
using SharpCompress.Reader;
using WebDAVClient.Helpers;
using WebDAVClient.Model;

namespace Asa.Core.Common
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

		private static readonly FileStorageManager _instance = new FileStorageManager();

		private string _url;
		private string _login;
		private string _password;
		private string _dataFolderName;
		private string _authServer;

		private StorageFile _versionFile;

		public bool Activated { get; private set; }
		public string Version { get; private set; }
		public bool UseLocalMode { get; private set; }

		public event EventHandler<FileProcessingProgressEventArgs> Downloading;
		public event EventHandler<FileProcessingProgressEventArgs> Extracting;
		public event EventHandler<EventArgs> UsingLocalMode;
		public event EventHandler<AuthorizingEventArgs> Authorizing;

		public static FileStorageManager Instance
		{
			get { return _instance; }
		}

		public DataActualityState DataState { get; set; }

		private string RemoteStorageUrl
		{
			get { return String.Format(RemoteStorageUrlTemplate, _url); }
		}

		public string LocalStoragePath
		{
			get
			{
				if (Activated)
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
			_versionFile = new ConfigFile(new[] { IncomingFolderName, AppProfileManager.Instance.AppName, "version.txt" });
			await InitCredentials();
			if (Activated)
				await CheckDataSate();
			if (Activated)
				Authorize();
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

			var credentialsFile = new ConfigFile(new[] { client, "credentials.txt" });
			await credentialsFile.Download();
			if (!credentialsFile.ExistsLocal()) return;
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
				else if (configLine.Contains("AuthService:"))
					_authServer = configLine.Replace("AuthService:", "").Trim();
			}

			Activated = true;

			if (!Directory.Exists(LocalStoragePath))
				Directory.CreateDirectory(LocalStoragePath);
		}

		private void Authorize()
		{
			if (Authorizing == null) return;
			var args = new AuthorizingEventArgs(_authServer);
			Authorizing(this, args);
			if (!args.Authorized &&
				DataState == DataActualityState.NotExisted &&
				_versionFile.ExistsLocal())
				File.Delete(_versionFile.LocalPath);
			Activated = args.Authorized;
		}

		private async Task CheckDataSate()
		{
			DataState = DataActualityState.NotExisted;

			if (!_versionFile.ExistsLocal())
			{
				DataState = DataActualityState.NotExisted;
				await _versionFile.Download();
			}
			else
			{
				try
				{
					var remoteFile = await GetClient().GetFile(_versionFile.RemotePath);
					if (File.GetLastWriteTime(_versionFile.LocalPath) < remoteFile.LastModified)
					{
						DataState = DataActualityState.Outdated;
						await _versionFile.Download();
					}
					else
						DataState = DataActualityState.Updated;
				}
				catch (HttpRequestException e)
				{
					DataState = DataActualityState.Updated;
					SwitchToLocalMode();
				}
			}
			if (_versionFile.ExistsLocal())
				Version = File.ReadAllText(_versionFile.LocalPath);
			else
				Activated = false;
		}

		public void ShowDownloadProgress(FileProcessingProgressEventArgs eventArgs)
		{
			if (Downloading != null)
				Downloading(this, eventArgs);
		}

		public void ShowExtractionProgress(FileProcessingProgressEventArgs eventArgs)
		{
			if (Extracting != null)
				Extracting(this, eventArgs);
		}

		public void SwitchToLocalMode()
		{
			if (UsingLocalMode != null)
				UsingLocalMode(this, EventArgs.Empty);
			UseLocalMode = true;
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

		public string NameOnly
		{
			get { return Path.GetFileNameWithoutExtension(LocalPath); }
		}

		protected StorageItem(string[] relativePathParts)
		{
			RelativePathParts = relativePathParts;
		}

		public virtual async Task<bool> Exists(bool checkRemoteToo = false, bool force = false)
		{
			if (!checkRemoteToo || FileStorageManager.Instance.UseLocalMode)
				return ExistsLocal();
			return FileStorageManager.Instance.DataState == DataActualityState.Updated && !force ?
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
				if (FileStorageManager.Instance.UseLocalMode) return false;
				await FileStorageManager.Instance.GetClient().GetFolder(RemotePath);
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

		private async Task CreateSubFolder(string name, bool remoteToo = false)
		{
			var subFolderLocalPath = Path.Combine(LocalPath, name);
			if (!Directory.Exists(subFolderLocalPath))
				Directory.CreateDirectory(subFolderLocalPath);
			if (remoteToo && !FileStorageManager.Instance.UseLocalMode)
			{
				var client = FileStorageManager.Instance.GetClient();
				try
				{
					await client.CreateDir(RemotePath, String.Format(@"/{0}", name));
				}
				catch (HttpRequestException e)
				{
					FileStorageManager.Instance.SwitchToLocalMode();
				}
			}
		}

		private async Task<IEnumerable<Item>> GetRemoteItems(Func<Item, bool> filter = null)
		{
			if (filter == null)
				filter = item => true;
			var client = FileStorageManager.Instance.GetClient();
			var folderItems = await client.List(RemotePath);
			return folderItems.Where(filter).ToList();
		}

		public IEnumerable<StorageFile> GetLocalFiles(Func<string, bool> filter = null, bool recursive = false)
		{
			if (filter == null)
				filter = item => true;

			var items = new List<StorageFile>();
			if (recursive)
			{
				foreach (var directoryPath in Directory.GetDirectories(LocalPath))
				{
					var subDirectory = new StorageDirectory(RelativePathParts.Merge(Path.GetFileName(directoryPath)));
					items.AddRange(subDirectory.GetLocalFiles(filter, true));
				}
			}
			items.AddRange(Directory.GetFiles(LocalPath)
					.Where(filePath => filter(Path.GetFileName(filePath)))
					.Select(filePath => new StorageFile(RelativePathParts.Merge(Path.GetFileName(filePath)))));
			return items;
		}

		public async Task<IEnumerable<StorageFile>> GetRemoteFiles(Func<string, bool> filter = null, bool recursive = false)
		{
			if (filter == null)
				filter = item => true;

			var items = new List<StorageFile>();
			var subItems = (await GetRemoteItems()).ToList();

			if (recursive)
			{
				foreach (var subItem in subItems.Where(subItem => subItem.IsCollection))
				{
					var subDirectory = new StorageDirectory(RelativePathParts.Merge(subItem.GetName()));
					items.AddRange(await subDirectory.GetRemoteFiles(filter, true));
				}
			}

			items.AddRange(subItems
					.Where(item => !item.IsCollection && filter(item.GetName()))
					.Select(item => new StorageFile(RelativePathParts, item)));

			return items;
		}

		public IEnumerable<StorageDirectory> GetLocalFolders(Func<string, bool> filter = null)
		{
			if (filter == null)
				filter = item => true;

			var items = new List<StorageDirectory>();
			items.AddRange(Directory.GetDirectories(LocalPath)
					.Where(directoryPath => filter(Path.GetFileName(directoryPath)))
					.Select(directoryPath => new StorageDirectory(RelativePathParts.Merge(Path.GetFileName(directoryPath)))));
			return items;
		}

		public async Task<IEnumerable<StorageDirectory>> GetRemoteFolders(Func<string, bool> filter)
		{
			var items = await GetRemoteItems(item => item.IsCollection);
			return items
				.Where(item => filter(item.GetName()))
				.Select(item =>
				{
					var directory = new StorageDirectory(RelativePathParts.Merge(item.GetName()));
					if (!Directory.Exists(directory.LocalPath))
						Directory.CreateDirectory(directory.LocalPath);
					return directory;
				})
				.ToList();
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

		public virtual async Task Download(bool force = false)
		{
			try
			{
				var client = FileStorageManager.Instance.GetClient();
				if ((ExistsLocal() && FileStorageManager.Instance.DataState == DataActualityState.Updated && !force) || FileStorageManager.Instance.UseLocalMode)
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
								var contentLenght = remoteFile.ContentLength.HasValue ? remoteFile.ContentLength.Value : 0;
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
			catch (WebDAVException exception)
			{
				throw new FileNotFoundException(String.Format("Error downloading file {0}", LocalPath));
			}
			catch (HttpRequestException e)
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
					Utilities.Instance.DeleteFolder(_asociatedDirectory.LocalPath);
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
					Utilities.Instance.DeleteFolder(targetPath);
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
								var contentLenght = remoteFile.ContentLength.HasValue ? remoteFile.ContentLength.Value : 0;
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

	public class FileProcessingProgressEventArgs : EventArgs
	{
		public string FileName { get; private set; }
		public decimal TotalSize { get; private set; }
		public decimal DownloadedSize { get; private set; }

		public int ProgressPercent
		{
			get { return (Int32)((DownloadedSize / TotalSize) * 100); }
		}

		public FileProcessingProgressEventArgs(string fileName, decimal totalSize, decimal downloadedSize)
		{
			FileName = fileName;
			TotalSize = totalSize;
			DownloadedSize = downloadedSize;
		}
	}

	public class AuthorizingEventArgs : EventArgs
	{
		public bool Authorized { get; set; }
		public string AuthServer { get; private set; }

		public AuthorizingEventArgs(string authService)
		{
			Authorized = true;
			AuthServer = authService;
		}
	}
}
