using System.Threading.Tasks;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Core.Dashboard
{
	public class ResourceManager
	{
		private static readonly ResourceManager _instance = new ResourceManager();

		public static ResourceManager Instance
		{
			get { return _instance; }
		}

		public StorageFile DataUsersFile { get; private set; }
		public StorageFile DataCoverFile { get; private set; }
		public StorageFile DataClientGoalsFile { get; private set; }
		public StorageFile DataLeadoffStatementFile { get; private set; }
		public StorageFile DataTargetCustomersFile { get; private set; }
		public StorageFile DataSimpleSummaryFile { get; private set; }

		private ResourceManager(){}

		public async Task Load()
		{
			await Common.ResourceManager.Instance.Load();

			await Common.ResourceManager.Instance.SlideMastersFolder.Download();

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

			DataSimpleSummaryFile = new StorageFile(new[]
			{
				FileStorageManager.IncomingFolderName,
				AppProfileManager.Instance.AppName,
				"Data",
				"Closing Summary.xml"
			});
			await DataSimpleSummaryFile.Download();
		}
	}
}
