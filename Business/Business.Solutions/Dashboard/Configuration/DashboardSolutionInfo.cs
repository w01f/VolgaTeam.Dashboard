using System;
using System.Drawing;
using System.IO;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Dashboard.Dictionaries;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Dashboard.Configuration
{
	public class DashboardSolutionInfo : BaseSolutionInfo
	{
		private ResourceManager _resourceManager;
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

		public override void LoadToggleData(StorageDirectory holderAppDataFolder)
		{
			base.LoadToggleData(holderAppDataFolder);

			_resourceManager = new ResourceManager();

			_resourceManager.Init(DataFolder);

			var document = new XmlDocument();
			if (_resourceManager.SettingsFile.ExistsLocal())
			{
				document.Load(_resourceManager.SettingsFile.LocalPath);

				Enabled = !Boolean.Parse(document.SelectSingleNode(@"//Settings/ButtonDisabled")?.InnerText ?? "false");

				var useImage = Boolean.Parse(document.SelectSingleNode(@"//Settings/ButtonImage")?.InnerText ?? "false");
				if (useImage)
				{
					foreach (var extension in new[] { ".svg", ".png" })
					{
						ToggleImagePath = Path.Combine(DataFolder.LocalPath, String.Format("{0}{1}", Id.ToLower(), extension));
						if (File.Exists(ToggleImagePath))
							break;
					}
				}

				ToggleTitle = document.SelectSingleNode(@"//Settings/RightPanelButton")?.InnerText ?? ToggleTitle;
				CleanslateTitle = document.SelectSingleNode(@"//Settings/Tab_0")?.InnerText;
				CoverTitle = document.SelectSingleNode(@"//Settings/Tab_1/Name")?.InnerText;
				LeadoffStatementTitle = document.SelectSingleNode(@"//Settings/Tab_2/Name")?.InnerText;
				ClientGoalsTitle = document.SelectSingleNode(@"//Settings/Tab_3/Name")?.InnerText;
				TargeCustomersTitle = document.SelectSingleNode(@"//Settings/Tab_4/Name")?.InnerText;
				SimpleSummaryTitle = document.SelectSingleNode(@"//Settings/Tab_5/Name")?.InnerText;
			}
		}

		public override void LoadContentData()
		{
			if (_contentLoaded) return;

			UsersList.Load(_resourceManager.DataUsersFile);
			CoverLists.Load(_resourceManager.DataCoverFile);
			ClientGoalsLists.Load(_resourceManager.DataClientGoalsFile);
			LeadoffStatementLists.Load(_resourceManager.DataLeadoffStatementFile);
			TargetCustomersLists.LoadCombinedData(_resourceManager.DataTargetCustomersFile);
			SimpleSummaryLists.Load(_resourceManager.DataSimpleSummaryFile);

			CleanslateHeaderLogo = _resourceManager.LogoCleanslateHeaderFile.ExistsLocal()
				? Image.FromFile(_resourceManager.LogoCleanslateHeaderFile.LocalPath)
				: null;
			CleanslateSplashLogo = _resourceManager.LogoCleanslateSplashFile.ExistsLocal()
				? Image.FromFile(_resourceManager.LogoCleanslateSplashFile.LocalPath)
				: null;
			CoverSplashLogo = _resourceManager.LogoCoverSplashFile.ExistsLocal()
				? Image.FromFile(_resourceManager.LogoCoverSplashFile.LocalPath)
				: null;
			LeadoffStatementSplashLogo = _resourceManager.LogoLeadoffStatementSplashFile.ExistsLocal()
				? Image.FromFile(_resourceManager.LogoLeadoffStatementSplashFile.LocalPath)
				: null;
			ClientGoalsSplashLogo = _resourceManager.LogoClientGoalsSplashFile.ExistsLocal()
				? Image.FromFile(_resourceManager.LogoClientGoalsSplashFile.LocalPath)
				: null;
			TargeCustomersSplashLogo = _resourceManager.LogoTargetCustomersSplashFile.ExistsLocal()
				? Image.FromFile(_resourceManager.LogoTargetCustomersSplashFile.LocalPath)
				: null;
			SimpleSummarySplashLogo = _resourceManager.LogoSimpleSummarySplashFile.ExistsLocal()
				? Image.FromFile(_resourceManager.LogoSimpleSummarySplashFile.LocalPath)
				: null;

			_contentLoaded = true;
		}
	}
}
