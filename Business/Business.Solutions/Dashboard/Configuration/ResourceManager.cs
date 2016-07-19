using System.Threading.Tasks;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Dashboard.Configuration
{
	class ResourceManager
	{
		public StorageFile DataUsersFile { get; private set; }
		public StorageFile DataCoverFile { get; private set; }
		public StorageFile DataClientGoalsFile { get; private set; }
		public StorageFile DataLeadoffStatementFile { get; private set; }
		public StorageFile DataTargetCustomersFile { get; private set; }
		public StorageFile DataSimpleSummaryFile { get; private set; }

		public async Task Load(StorageDirectory dataFolder)
		{
			DataUsersFile = new StorageFile(dataFolder.RelativePathParts.Merge("Users.xml"));
			await DataUsersFile.Download();

			DataCoverFile = new StorageFile(dataFolder.RelativePathParts.Merge("Add Cover.xml"));
			await DataCoverFile.Download();

			DataClientGoalsFile = new StorageFile(dataFolder.RelativePathParts.Merge("Needs Analysis.xml"));
			await DataClientGoalsFile.Download();

			DataLeadoffStatementFile = new StorageFile(dataFolder.RelativePathParts.Merge("Intro Slide.xml"));
			await DataLeadoffStatementFile.Download();

			DataTargetCustomersFile = new StorageFile(dataFolder.RelativePathParts.Merge("Target Customer.xml"));
			await DataTargetCustomersFile.Download();

			DataSimpleSummaryFile = new StorageFile(dataFolder.RelativePathParts.Merge("Closing Summary.xml"));
			await DataSimpleSummaryFile.Download();
		}
	}
}
