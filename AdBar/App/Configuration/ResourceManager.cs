using System.Threading.Tasks;
using Asa.Core.Common;

namespace Asa.Bar.App.Configuration
{
	public class ResourceManager
	{
		private static readonly ResourceManager _instance = new ResourceManager();

		public static ResourceManager Instance
		{
			get { return _instance; }
		}

		#region Local
		public StorageDirectory TempFolder { get; private set; }
		public StorageDirectory AppSettingsFolder { get; private set; }
		public StorageFile AppSettingsFile { get; private set; }
		#endregion

		#region Remote
		public StorageFile AppConfigFile { get; private set; }
		public StorageFile TabsConfigFile { get; private set; }
		public StorageFile WatchedProcessesFile { get; private set; }
		public ArchiveDirectory SpecialAppsFolder { get; private set; }
		public ArchiveDirectory SharedAssembliesFolder { get; private set; }
		public StorageDirectory DataFolder { get; private set; }
		public ArchiveDirectory SyncFilesFolder { get; private set; }
		public StorageDirectory CloudFilesFolder { get; private set; }
		#endregion

		private ResourceManager() { }

		public async Task Load()
		{
			#region Local
			TempFolder = new StorageDirectory(new[]
			{
				"Temp"
			});
			if (!await TempFolder.Exists())
				await StorageDirectory.CreateSubFolder(new string[] { }, "Temp");

			AppSettingsFolder = new StorageDirectory(new[]
			{
				FileStorageManager.LocalFilesFolderName,
				AppProfileManager.Instance.AppName,
			});
			if (!await AppSettingsFolder.Exists())
				await StorageDirectory.CreateSubFolder(new[] { FileStorageManager.LocalFilesFolderName }, AppProfileManager.Instance.AppName);

			AppSettingsFile = new StorageFile(new[]
			{
				FileStorageManager.LocalFilesFolderName,
				AppProfileManager.Instance.AppName,
				"Settings.xml"
			});
			AppSettingsFile.AllocateParentFolder();
			#endregion

			#region Remote
			AppConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"Config.xml"
			});
			await AppConfigFile.Download();

			TabsConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"TabNames.xml"
			});
			await TabsConfigFile.Download();

			WatchedProcessesFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"HideList.xml"
			});
			await WatchedProcessesFile.Download();

			SharedAssembliesFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"SharedAssemblies"
			});
			if (await SharedAssembliesFolder.Exists(true))
				await SharedAssembliesFolder.DownloadTo(SharedAssemblyHelper.SharedAssemblyLocationPath);

			SpecialAppsFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"SpecialApps"
			});
			if (await SpecialAppsFolder.Exists(true))
				await SpecialAppsFolder.Download();

			DataFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Data",
			});

			SyncFilesFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Data",
				"SyncFiles"
			});
			if (await SyncFilesFolder.Exists(true))
				await SyncFilesFolder.Download();

			CloudFilesFolder = new StorageDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				FileStorageManager.CommonIncomingFolderName,
				"CloudFiles",
			});
			#endregion
		}
	}
}
