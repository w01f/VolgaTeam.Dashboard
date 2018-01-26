using System.IO;
using System.Threading.Tasks;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Bar.App.Configuration
{
	public class ResourceManager
	{
		public static ResourceManager Instance { get; } = new ResourceManager();

		#region Local
		public string AppRootFolderPath { get; }
		public StorageDirectory TempFolder { get; private set; }
		public StorageDirectory AppSettingsFolder { get; private set; }
		public StorageFile AppSettingsFile { get; private set; }
		#endregion

		#region Remote
		public StorageFile AppConfigFile { get; private set; }
		public StorageFile TabsConfigFile { get; private set; }
		public StorageFile WatchedProcessesFile { get; private set; }
		public StorageFile MaintenanceConfigFile { get; private set; }
		public StorageFile PatchUpdaterConfigFile { get; private set; }
		public StorageFile UpdaterConfigFile { get; private set; }
		public StorageFile NoSyncConfigFile { get; private set; }
		public StorageFile SyncFormColorConfigFile { get; private set; }
		public StorageFile IconFile { get; private set; }
		public StorageFile LoginLogoFile { get; private set; }
		public StorageFile SplashLogoFile { get; private set; }
		public StorageFile SyncFormCloseImageFile { get; private set; }
		public StorageFile UndockFormImageFile { get; private set; }
		public StorageFile DockRegularImageFile { get; private set; }
		public StorageFile DockFloaterImageFile { get; private set; }
		public StorageFile ExpandFormImageFile { get; private set; }
		public StorageFile CollapseFormImageFile { get; private set; }
		public StorageFile FloaterLogoFile { get; private set; }
		public StorageFile FloaterCancelImageFile { get; private set; }
		public ArchiveDirectory SpecialAppsFolder { get; private set; }
		public ArchiveDirectory SharedAssembliesFolder { get; private set; }
		public StorageDirectory DataFolder { get; private set; }
		public ArchiveDirectory SyncFilesFolder { get; private set; }
		public StorageDirectory CloudFilesFolder { get; private set; }
		#endregion

		private ResourceManager()
		{
			AppRootFolderPath = Path.GetDirectoryName(typeof(ResourceManager).Assembly.Location);
		}

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

			MaintenanceConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"maintenance.xml"
			});
			if (await MaintenanceConfigFile.Exists(true))
				await MaintenanceConfigFile.Download();

			PatchUpdaterConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"sfx_update.xml"
			});
			if (await PatchUpdaterConfigFile.Exists(true))
				await PatchUpdaterConfigFile.Download();

			UpdaterConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"updater.xml"
			});
			if (await UpdaterConfigFile.Exists(true))
				await UpdaterConfigFile.Download();

			NoSyncConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Resources",
				"NoSync.xml"
			});
			if (await NoSyncConfigFile.Exists(true))
				await NoSyncConfigFile.Download();

			SyncFormColorConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Resources",
				"sync_color.xml"
			});
			if (await SyncFormColorConfigFile.Exists(true))
				await SyncFormColorConfigFile.Download();

			IconFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Resources",
				"icon.ico"
			});
			if (await IconFile.Exists(true))
				await IconFile.Download();

			LoginLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Resources",
				"app_logo.png"
			});
			if (await LoginLogoFile.Exists(true))
				await LoginLogoFile.Download();

			SplashLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Resources",
				"splash_logo.png"
			});
			if (await SplashLogoFile.Exists(true))
				await SplashLogoFile.Download();

			SyncFormCloseImageFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Resources",
				"ProgressCancel.png"
			});
			if (await SyncFormCloseImageFile.Exists(true))
				await SyncFormCloseImageFile.Download();

			UndockFormImageFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Resources",
				"undock_arrow.png"
			});
			if (await UndockFormImageFile.Exists(true))
				await UndockFormImageFile.Download();

			DockRegularImageFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Resources",
				"dock_arrow_ribbon.png"
			});
			if (await DockRegularImageFile.Exists(true))
				await DockRegularImageFile.Download();

			DockFloaterImageFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Resources",
				"dock_arrow_floater.png"
			});
			if (await DockFloaterImageFile.Exists(true))
				await DockFloaterImageFile.Download();

			ExpandFormImageFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Resources",
				"undocked_expand.png"
			});
			if (await ExpandFormImageFile.Exists(true))
				await ExpandFormImageFile.Download();

			CollapseFormImageFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Resources",
				"undocked_collapse.png"
			});
			if (await CollapseFormImageFile.Exists(true))
				await CollapseFormImageFile.Download();

			FloaterLogoFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Resources",
				"collapsed_undocked_logo.png"
			});
			if (await FloaterLogoFile.Exists(true))
				await FloaterLogoFile.Download();

			FloaterCancelImageFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Resources",
				"undockedCancel.png"
			});
			if (await FloaterCancelImageFile.Exists(true))
				await FloaterCancelImageFile.Download();

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

			#region Make local copy
			if (NoSyncConfigFile.ExistsLocal())
				File.Copy(NoSyncConfigFile.LocalPath, Path.Combine(AppRootFolderPath, Path.GetFileName(NoSyncConfigFile.LocalPath)), true);
			if (UpdaterConfigFile.ExistsLocal())
				File.Copy(UpdaterConfigFile.LocalPath, Path.Combine(AppRootFolderPath, Path.GetFileName(UpdaterConfigFile.LocalPath)), true);
			if (SyncFormColorConfigFile.ExistsLocal())
				File.Copy(SyncFormColorConfigFile.LocalPath, Path.Combine(AppRootFolderPath, Path.GetFileName(SyncFormColorConfigFile.LocalPath)), true);
			if (IconFile.ExistsLocal())
				File.Copy(IconFile.LocalPath, Path.Combine(AppRootFolderPath, Path.GetFileName(IconFile.LocalPath)), true);
			if (LoginLogoFile.ExistsLocal())
				File.Copy(LoginLogoFile.LocalPath, Path.Combine(AppRootFolderPath, Path.GetFileName(LoginLogoFile.LocalPath)), true);
			if (SyncFormCloseImageFile.ExistsLocal())
				File.Copy(SyncFormCloseImageFile.LocalPath, Path.Combine(AppRootFolderPath, Path.GetFileName(SyncFormCloseImageFile.LocalPath)), true);
			#endregion
		}
	}
}
