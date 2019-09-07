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
			await Asa.Common.Core.Configuration.ResourceManager.Instance.LoadSubStorageIndependentResources();

			await Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.Download();

			DataUsersFile = new StorageFile(
				Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Users.xml"));

			DataCoverFile = new StorageFile(
				Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Add Cover.xml"));

			DataClientGoalsFile = new StorageFile(
				Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Needs Analysis.xml"));

			DataLeadoffStatementFile = new StorageFile(
				Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Intro Slide.xml"));

			DataTargetCustomersFile = new StorageFile(
				Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Target Customer.xml"));

			TextResourcesFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppSubStorageIndependentFolderName,
				"AppSettings",
				"app_branding.xml"
			});
			if (await TextResourcesFile.Exists(true))
				await TextResourcesFile.Download();

			ImageResourcesFolder = new ArchiveDirectory(new[]
			{
				FileStorageManager.IncomingFolderName,
			    AppProfileManager.Instance.AppSubStorageIndependentFolderName,
				"Resources"
			});
			if (await ImageResourcesFolder.Exists(true))
				await ImageResourcesFolder.Download();

			FormStyleConfigFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
			    AppProfileManager.Instance.AppSubStorageIndependentFolderName,
				"AppSettings",
				"style.xml"
			});
			if (await FormStyleConfigFile.Exists(true))
				await FormStyleConfigFile.Download();
		}
	}
}
