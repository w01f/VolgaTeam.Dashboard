using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;

namespace Asa.Common.Core.Objects.RemoteStorage
{
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
}
