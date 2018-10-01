using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Shift.Configuration.Agenda;
using Asa.Business.Solutions.Shift.Configuration.Approach;
using Asa.Business.Solutions.Shift.Configuration.CBC;
using Asa.Business.Solutions.Shift.Configuration.Cleanslate;
using Asa.Business.Solutions.Shift.Configuration.Contract;
using Asa.Business.Solutions.Shift.Configuration.Cover;
using Asa.Business.Solutions.Shift.Configuration.Goals;
using Asa.Business.Solutions.Shift.Configuration.IntegratedSolution;
using Asa.Business.Solutions.Shift.Configuration.Intro;
using Asa.Business.Solutions.Shift.Configuration.Investment;
using Asa.Business.Solutions.Shift.Configuration.Market;
using Asa.Business.Solutions.Shift.Configuration.NeedsSolutions;
using Asa.Business.Solutions.Shift.Configuration.NextSteps;
using Asa.Business.Solutions.Shift.Configuration.Partnership;
using Asa.Business.Solutions.Shift.Configuration.ROI;
using Asa.Business.Solutions.Shift.Enums;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class ShiftSolutionInfo : BaseSolutionInfo
	{
		private ResourceManager _resourceManager;

		public List<ShiftTopTabInfo> TabsInfo { get; }

		public ClientGoalsLists ClientGoalsLists { get; }
		public TargetCustomersLists TargetCustomersLists { get; }

		public ShiftSolutionInfo()
		{
			Type = SolutionType.Shift;

			TabsInfo = new List<ShiftTopTabInfo>();

			ClientGoalsLists = new ClientGoalsLists();
			TargetCustomersLists = new TargetCustomersLists();
		}

		public override void LoadToggleData(StorageDirectory holderAppDataFolder)
		{
			base.LoadToggleData(holderAppDataFolder);

			_resourceManager = new ResourceManager();

			_resourceManager.Init(DataFolder);

			if (_resourceManager.SettingsFile.ExistsLocal())
			{
				var document = new XmlDocument();
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
			}
		}

		public override void LoadContentData()
		{
			_resourceManager.LoadGraphicResources();

			if (_contentLoaded) return;

			ClientGoalsLists.Load(_resourceManager.DataClientGoalsFile);
			TargetCustomersLists.LoadHHIData(_resourceManager.DataHHIFile);
			TargetCustomersLists.LoadDemoData(_resourceManager.DataDemoFile);
			TargetCustomersLists.LoadGeographyData(_resourceManager.DataGeographyFile);

			if (_resourceManager.SettingsFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(_resourceManager.SettingsFile.LocalPath);

				foreach (var tabConfigNode in document.SelectNodes(@"//Settings/Tab")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
				{
					var tabId = tabConfigNode.SelectSingleNode("Type")?.InnerText?.ToLower();

					ShiftTopTabInfo tabInfo;
					switch (tabId)
					{
						case "default":
							tabInfo = new CleanslateTabInfo();
							break;
						case "cover":
							tabInfo = new CoverTabInfo();
							break;
						case "intro":
							tabInfo = new IntroTabInfo();
							break;
						case "agenda":
							tabInfo = new AgendaTabInfo();
							break;
						case "goals":
							tabInfo = new GoalsTabInfo();
							break;
						case "market":
							tabInfo = new MarketTabInfo();
							break;
						case "partnership":
							tabInfo = new PartnershipTabInfo();
							break;
						case "needs-solutions":
							tabInfo = new NeedsSolutionsTabInfo();
							break;
						case "cbc":
							tabInfo = new CBCTabInfo();
							break;
						case "integrated-solution":
							tabInfo = new IntegratedSolutionTabInfo();
							break;
						case "investment":
							tabInfo = new InvestmentTabInfo();
							break;
						case "next-steps":
							tabInfo = new NextStepsTabInfo();
							break;
						case "contract":
							tabInfo = new ContractTabInfo();
							break;
						case "support-materials":
							tabInfo = new CommonTopTabInfo(ShiftTopTabType.SupportMaterials);
							break;
						case "spec-builder":
							tabInfo = new CommonTopTabInfo(ShiftTopTabType.SpecBuilder);
							break;
						case "approach":
							tabInfo = new ApproachTabInfo();
							break;
						case "roi":
							tabInfo = new ROITabInfo();
							break;
						default:
							throw new ArgumentOutOfRangeException("Shift tab type is not defined");
					}
					tabInfo.LoadData(tabConfigNode, _resourceManager);
					TabsInfo.Add(tabInfo);
				}
			}

			_contentLoaded = true;
		}


		public void ReleaseContentData()
		{
			_resourceManager.ReleaseGraphicResources();
		}
	}
}
