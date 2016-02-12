using System.Threading.Tasks;
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

		private ResourceManager() { }

		public async Task Load()
		{
			await Asa.Common.Core.Configuration.ResourceManager.Instance.Load();

			await Asa.Common.Core.Configuration.ResourceManager.Instance.SlideMastersFolder.Download();

			DataUsersFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Data",
				"Users.xml"
			});
			await DataUsersFile.Download();

			DataCoverFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Data",
				"Add Cover.xml"
			});
			await DataCoverFile.Download();

			DataClientGoalsFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Data",
				"Needs Analysis.xml"
			});
			await DataClientGoalsFile.Download();

			DataLeadoffStatementFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Data",
				"Intro Slide.xml"
			});
			await DataLeadoffStatementFile.Download();

			DataTargetCustomersFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Data",
				"Target Customer.xml"
			});
			await DataTargetCustomersFile.Download();
		}
	}
}
