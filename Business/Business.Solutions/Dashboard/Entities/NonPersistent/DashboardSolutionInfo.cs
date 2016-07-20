using System.Drawing;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Dashboard.Configuration;
using Asa.Business.Solutions.Dashboard.Dictionaries;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Dashboard.Entities.NonPersistent
{
	public class DashboardSolutionInfo : BaseSolutionInfo
	{
		public Users UsersList { get; }
		public CoverLists CoverLists { get; }
		public ClientGoalsLists ClientGoalsLists { get; }
		public LeadoffStatementLists LeadoffStatementLists { get; }
		public TargetCustomersLists TargetCustomersLists { get; }
		public SimpleSummaryLists SimpleSummaryLists { get; }

		public Image CleanslateHeaderLogo { get; private set; }
		public Image CleanslateSplashLogo { get; private set; }
		public Image CoverSplashLogo { get; private set; }
		public Image LeadoffStatementSplashLogo { get; private set; }
		public Image ClientGoalsSplashLogo { get; private set; }
		public Image TargeCustomersSplashLogo { get; private set; }
		public Image SimpleSummarySplashLogo { get; private set; }

		public DashboardSolutionInfo()
		{
			Type = SolutionType.Dashboard;

			UsersList = new Users();
			CoverLists = new CoverLists();
			ClientGoalsLists = new ClientGoalsLists();
			LeadoffStatementLists = new LeadoffStatementLists();
			TargetCustomersLists = new TargetCustomersLists();
			SimpleSummaryLists = new SimpleSummaryLists();
		}

		public override void LoadData(StorageDirectory holderAppDataFolder)
		{
			base.LoadData(holderAppDataFolder);

			var resourceManager = new ResourceManager();

			AsyncHelper.RunSync(() => resourceManager.Load(DataFolder));

			UsersList.Load(resourceManager.DataUsersFile);
			CoverLists.Load(resourceManager.DataCoverFile);
			ClientGoalsLists.Load(resourceManager.DataClientGoalsFile);
			LeadoffStatementLists.Load(resourceManager.DataLeadoffStatementFile);
			TargetCustomersLists.Load(resourceManager.DataTargetCustomersFile);
			SimpleSummaryLists.Load(resourceManager.DataSimpleSummaryFile);

			CleanslateHeaderLogo = resourceManager.LogoCleanslateHeaderFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoCleanslateHeaderFile.LocalPath)
				: null;
			CleanslateSplashLogo = resourceManager.LogoCleanslateSplashFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoCleanslateSplashFile.LocalPath)
				: null;
			CoverSplashLogo = resourceManager.LogoCoverSplashFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoCoverSplashFile.LocalPath)
				: null;
			LeadoffStatementSplashLogo = resourceManager.LogoLeadoffStatementSplashFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoLeadoffStatementSplashFile.LocalPath)
				: null;
			ClientGoalsSplashLogo = resourceManager.LogoClientGoalsSplashFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoClientGoalsSplashFile.LocalPath)
				: null;
			TargeCustomersSplashLogo = resourceManager.LogoTargetCustomersSplashFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTargetCustomersSplashFile.LocalPath)
				: null;
			SimpleSummarySplashLogo = resourceManager.LogoSimpleSummarySplashFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoSimpleSummarySplashFile.LocalPath)
				: null;
		}
	}
}
