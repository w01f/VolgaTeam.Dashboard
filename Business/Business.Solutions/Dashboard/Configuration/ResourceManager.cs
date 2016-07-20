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

		public StorageFile LogoCleanslateHeaderFile { get; private set; }
		public StorageFile LogoCleanslateSplashFile { get; private set; }
		public StorageFile LogoCoverSplashFile { get; private set; }
		public StorageFile LogoLeadoffStatementSplashFile { get; private set; }
		public StorageFile LogoClientGoalsSplashFile { get; private set; }
		public StorageFile LogoTargetCustomersSplashFile { get; private set; }
		public StorageFile LogoSimpleSummarySplashFile { get; private set; }

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

			var imageResourceFolder = new StorageDirectory(dataFolder.RelativePathParts.Merge("responsive_images"));

			LogoCleanslateHeaderFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("tab_1_header.png"));
			await LogoCleanslateHeaderFile.Download();

			LogoCleanslateSplashFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("tab_1.png"));
			await LogoCleanslateSplashFile.Download();

			LogoCoverSplashFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("tab_2.png"));
			await LogoCoverSplashFile.Download();

			LogoLeadoffStatementSplashFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("tab_3.png"));
			await LogoLeadoffStatementSplashFile.Download();

			LogoClientGoalsSplashFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("tab_4.png"));
			await LogoClientGoalsSplashFile.Download();

			LogoTargetCustomersSplashFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("tab_5.png"));
			await LogoTargetCustomersSplashFile.Download();

			LogoSimpleSummarySplashFile = new StorageFile(imageResourceFolder.RelativePathParts.Merge("tab_6.png"));
			await LogoSimpleSummarySplashFile.Download();
		}
	}
}
