using System;
using System.IO;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Dashboard.Dictionaries;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Resources.Solutions.Dashboard;

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

		public IDashboardGraphicResources GraphicResources => _resourceManager.GraphicResources;

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
			_resourceManager.LoadGraphicResources();

			if (_contentLoaded) return;

			UsersList.Load(_resourceManager.DataUsersFile);
			CoverLists.Load(_resourceManager.DataCoverFile);
			ClientGoalsLists.Load(_resourceManager.DataClientGoalsFile);
			LeadoffStatementLists.Load(_resourceManager.DataLeadoffStatementFile);
			TargetCustomersLists.LoadCombinedData(_resourceManager.DataTargetCustomersFile);
			SimpleSummaryLists.Load(_resourceManager.DataSimpleSummaryFile);

			_contentLoaded = true;
		}

		public void ReleaseContentData()
		{
			_resourceManager.ReleaseGraphicResources();
		}
	}
}
