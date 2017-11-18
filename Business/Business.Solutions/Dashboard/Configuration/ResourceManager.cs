using System.Threading.Tasks;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Dashboard.Configuration
{
	class ResourceManager
	{
		public StorageFile SettingsFile { get; private set; }

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
			SettingsFile = new StorageFile(dataFolder.RelativePathParts.Merge("settings.xml"));
			await SettingsFile.Download();

			DataUsersFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "data", "Users.xml" }));
			await DataUsersFile.Download();

			DataCoverFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "data", "Add Cover.xml" }));
			await DataCoverFile.Download();

			DataClientGoalsFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_cna", "data", "Needs Analysis.xml" }));
			await DataClientGoalsFile.Download();

			DataLeadoffStatementFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "data", "Intro Slide.xml" }));
			await DataLeadoffStatementFile.Download();

			DataTargetCustomersFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_tgtcust", "data", "Target Customer.xml" }));
			await DataTargetCustomersFile.Download();

			DataSimpleSummaryFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_closing", "data", "Closing Summary.xml" }));
			await DataSimpleSummaryFile.Download();

			LogoCleanslateHeaderFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "00_6ms", "design_branding", "tab_1_header.png" }));
			await LogoCleanslateHeaderFile.Download();

			LogoCleanslateSplashFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "00_6ms", "design_branding", "tab_1.png" }));
			await LogoCleanslateSplashFile.Download();

			LogoCoverSplashFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "design_branding", "tab_2.png" }));
			await LogoCoverSplashFile.Download();

			LogoLeadoffStatementSplashFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "design_branding", "tab_3.png" }));
			await LogoLeadoffStatementSplashFile.Download();

			LogoClientGoalsSplashFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_cna", "design_branding", "tab_4.png" }));
			await LogoClientGoalsSplashFile.Download();

			LogoTargetCustomersSplashFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_tgtcust", "design_branding", "tab_5.png" }));
			await LogoTargetCustomersSplashFile.Download();

			LogoSimpleSummarySplashFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_closing", "design_branding", "tab_6.png" }));
			await LogoSimpleSummarySplashFile.Download();
		}
	}
}
