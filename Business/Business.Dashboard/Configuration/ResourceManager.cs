using System;
using System.Threading.Tasks;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Dashboard.Configuration
{
	public class ResourceManager
	{
		public static ResourceManager Instance { get; } = new ResourceManager();

		public StorageFile DataUsersFile { get; private set; }
		public StorageFile DataCoverFile { get; private set; }
		public StorageFile DataClientGoalsFile { get; private set; }
		public StorageFile DataLeadoffStatementFile { get; private set; }
		public StorageFile DataTargetCustomersFile { get; private set; }

		public StorageFile TextResourcesFile { get; private set; }
		public ArchiveDirectory ImageResourcesFolder { get; private set; }
		public StorageFile FormStyleConfigFile { get; private set; }

		private ResourceManager() { }

		public async Task Load()
		{
			await Asa.Common.Core.Configuration.ResourceManager.Instance.Load();

			DataUsersFile = new StorageFile(
				AppProfileManager.Instance.AppDataFolder.RelativePathParts.Merge("Users.xml"));
			await DataUsersFile.Download();

			DataCoverFile = new StorageFile(
				AppProfileManager.Instance.AppDataFolder.RelativePathParts.Merge("Add Cover.xml"));
			await DataCoverFile.Download();

			DataClientGoalsFile = new StorageFile(
				AppProfileManager.Instance.AppDataFolder.RelativePathParts.Merge("Needs Analysis.xml"));
			await DataClientGoalsFile.Download();

			DataLeadoffStatementFile = new StorageFile(
				AppProfileManager.Instance.AppDataFolder.RelativePathParts.Merge("Intro Slide.xml"));
			await DataLeadoffStatementFile.Download();

			DataTargetCustomersFile = new StorageFile(
				AppProfileManager.Instance.AppDataFolder.RelativePathParts.Merge("Target Customer.xml"));
			await DataTargetCustomersFile.Download();

			TextResourcesFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"app_branding.xml"
			});
			if (await TextResourcesFile.Exists(true))
				await TextResourcesFile.Download();

			ImageResourcesFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Resources"
			});
			if (await ImageResourcesFolder.Exists(true))
				await ImageResourcesFolder.Download();

			FormStyleConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"AppSettings",
				"style.xml"
			});
			if (await FormStyleConfigFile.Exists(true))
				await FormStyleConfigFile.Download();
		}
	}
}
