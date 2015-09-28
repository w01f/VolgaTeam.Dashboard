using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using WebDAVClient.Helpers;

namespace NewBizWiz.Core.Common
{
	public enum StorageTypeEnum
	{
		None = 0,
		Dashboard = 0,
	}

	public class FileStorageManager
	{
		private const string RemoteStorageUrl = "http://adsalescloud.com/remote.php/webdav";
		private const string RemoteStorageLogin = "apps_apex_destin";
		private const string RemoteStoragePassword = "q^MrB]'%7Br54yH^";

		private const string LocalAppSettingsFolderName = "adSalesApps Data";

		public const string IncomingFolderName = "outgoing";
		public const string OutgoingFolderName = "incoming";

		private static readonly FileStorageManager _instance = new FileStorageManager();

		private StorageTypeEnum _storageType;

		public static FileStorageManager Instance
		{
			get { return _instance; }
		}

		public string RootFolderName
		{
			get
			{
				switch (_storageType)
				{
					case StorageTypeEnum.Dashboard:
						return "app_dashboard";
				}
				throw new InvalidEnumArgumentException("Storage Type Undefined");
			}
		}

		public string LocalStoragePath
		{
			get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), LocalAppSettingsFolderName); }
		}

		public string RemoteStoragePath
		{
			get { return RemoteStorageUrl; }
		}

		private FileStorageManager() { }

		public void Init(StorageTypeEnum storageType)
		{
			_storageType = storageType;
			if (!Directory.Exists(LocalStoragePath))
				Directory.CreateDirectory(LocalStoragePath);
		}

		public WebDAVClient.Client GetClient()
		{
			return new WebDAVClient.Client(
				new NetworkCredential
				{
					UserName = RemoteStorageLogin,
					Password = RemoteStoragePassword
				})
			{
				Server = RemoteStorageUrl
			};
		}
	}

	public abstract class StorageItem
	{
		private readonly string[] _relativePathParts;


		public string LocalPath
		{
			get { return Path.Combine(FileStorageManager.Instance.LocalStoragePath, Path.Combine(_relativePathParts)); }
		}

		public string RemotePath
		{
			get { return String.Format("/{0}", String.Join(@"/", _relativePathParts)); }
		}

		protected StorageItem(string[] relativePathParts)
		{
			_relativePathParts = relativePathParts;
		}

		protected abstract bool ExistsLocal();
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
			if (!checkRemoteToo)
				return storageItem.ExistsLocal();
			if (!storageItem.ExistsLocal())
				return false;
			return await storageItem.ExistsRemote();
		}

		public static async Task CreateSubFolder(string[] relativePathParts, string name)
		{
			var storageItem = new StorageDirectory(relativePathParts);
			await storageItem.CreateSubFolder(name);
		}

		protected override bool ExistsLocal()
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

		protected async Task CreateSubFolder(string name)
		{
			var subFolderLocalPath = Path.Combine(LocalPath, name);
			if (!Directory.Exists(subFolderLocalPath))
				Directory.CreateDirectory(subFolderLocalPath);
			var client = FileStorageManager.Instance.GetClient();
			await client.CreateDir(RemotePath, String.Format(@"/{0}", name));
		}
	}

	public class StorageFile : StorageItem
	{
		public StorageFile(string[] relativePathParts) : base(relativePathParts) { }

		public static async Task<bool> Exists(string[] relativePathParts, bool checkRemoteToo = false)
		{
			var storageItem = new StorageFile(relativePathParts);
			if (!checkRemoteToo)
				return storageItem.ExistsLocal();
			if (!storageItem.ExistsLocal())
				return false;
			return await storageItem.ExistsRemote();
		}


		protected override bool ExistsLocal()
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
	}
}
