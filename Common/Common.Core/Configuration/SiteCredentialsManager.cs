using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Common.Core.Configuration
{
	public class SiteCredentialsManager
	{
		public StorageFile SettingsFile { get; private set; }
		public AuthSettings Settings { get; private set; }
		public static SiteCredentialsManager Instance { get; } = new SiteCredentialsManager();

		private SiteCredentialsManager() { }

		public void Init()
		{
			SettingsFile = new StorageFile(new[]
			{
				FileStorageManager.LocalFilesFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"Credentials.xml"
			});
			SettingsFile.AllocateParentFolder();

			Settings = AuthSettings.Load(SettingsFile);
		}
	}
}
