using System;
using System.Drawing;
using System.IO;
using System.Xml;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Dashboard.Configuration;
using Asa.Business.Solutions.Dashboard.Dictionaries;
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

		public string CleanslateTitle { get; private set; }
		public string CoverTitle { get; private set; }
		public string LeadoffStatementTitle { get; private set; }
		public string ClientGoalsTitle { get; private set; }
		public string TargeCustomersTitle { get; private set; }
		public string SimpleSummaryTitle { get; private set; }

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

			resourceManager.Init(DataFolder);

			var document = new XmlDocument();
			if (resourceManager.SettingsFile.ExistsLocal())
			{
				document.Load(resourceManager.SettingsFile.LocalPath);

				Enabled = !Boolean.Parse(document.SelectSingleNode(@"//Settings/ButtonDisabled")?.InnerText ?? "false");

				var useImage = Boolean.Parse(document.SelectSingleNode(@"//Settings/ButtonImage")?.InnerText ?? "false");
				if (useImage)
					ToggleImagePath = Path.Combine(DataFolder.LocalPath, String.Format("{0}.png", Id.ToLower()));

				ToggleTitle = document.SelectSingleNode(@"//Settings/RightPanelButton")?.InnerText ?? ToggleTitle;
				CleanslateTitle = document.SelectSingleNode(@"//Settings/Tab_0")?.InnerText;
				CoverTitle = document.SelectSingleNode(@"//Settings/Tab_1/Name")?.InnerText;
				LeadoffStatementTitle = document.SelectSingleNode(@"//Settings/Tab_2/Name")?.InnerText;
				ClientGoalsTitle = document.SelectSingleNode(@"//Settings/Tab_3/Name")?.InnerText;
				TargeCustomersTitle = document.SelectSingleNode(@"//Settings/Tab_4/Name")?.InnerText;
				SimpleSummaryTitle = document.SelectSingleNode(@"//Settings/Tab_5/Name")?.InnerText;
			}


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
