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

		public void Init(StorageDirectory dataFolder)
		{
			SettingsFile = new StorageFile(dataFolder.RelativePathParts.Merge("settings.xml"));

			DataUsersFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Users.xml"));
			DataCoverFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Add Cover.xml"));
			DataClientGoalsFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Needs Analysis.xml"));
			DataLeadoffStatementFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Intro Slide.xml"));
			DataTargetCustomersFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Target Customer.xml"));
			DataSimpleSummaryFile = new StorageFile(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.RelativePathParts.Merge("Closing Summary.xml"));

			LogoCleanslateHeaderFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "00_6ms", "design_branding", "tab_1_header.png" }));
			LogoCleanslateSplashFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "00_6ms", "design_branding", "tab_1.png" }));
			LogoCoverSplashFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "01_cover", "design_branding", "tab_2.png" }));
			LogoLeadoffStatementSplashFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "02_intro", "design_branding", "tab_3.png" }));
			LogoClientGoalsSplashFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "03_cna", "design_branding", "tab_4.png" }));
			LogoTargetCustomersSplashFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "04_tgtcust", "design_branding", "tab_5.png" }));
			LogoSimpleSummarySplashFile = new StorageFile(dataFolder.RelativePathParts.Merge(new[] { "05_closing", "design_branding", "tab_6.png" }));
		}
	}
}
