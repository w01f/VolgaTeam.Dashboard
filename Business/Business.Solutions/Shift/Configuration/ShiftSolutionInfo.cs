using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Dictionaries;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Shift.Configuration.Agenda;
using Asa.Business.Solutions.Shift.Configuration.Cleanslate;
using Asa.Business.Solutions.Shift.Configuration.Cover;
using Asa.Business.Solutions.Shift.Configuration.Goals;
using Asa.Business.Solutions.Shift.Configuration.Intro;
using Asa.Business.Solutions.Shift.Configuration.Market;
using Asa.Business.Solutions.Shift.Enums;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class ShiftSolutionInfo : BaseSolutionInfo
	{
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

		public override void LoadData(StorageDirectory holderAppDataFolder)
		{
			base.LoadData(holderAppDataFolder);

			var resourceManager = new ResourceManager();

			resourceManager.Init(DataFolder);

			if (resourceManager.SettingsFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.SettingsFile.LocalPath);

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
							tabInfo = new CommonTopTabInfo(ShiftTopTabType.Partnership);
							break;
						case "needs-solutions":
							tabInfo = new CommonTopTabInfo(ShiftTopTabType.NeedsSolutions);
							break;
						case "cbc":
							tabInfo = new CommonTopTabInfo(ShiftTopTabType.CBC);
							break;
						case "integrated-solution":
							tabInfo = new CommonTopTabInfo(ShiftTopTabType.IntegratedSolution);
							break;
						case "investment":
							tabInfo = new CommonTopTabInfo(ShiftTopTabType.Investment);
							break;
						case "next-steps":
							tabInfo = new CommonTopTabInfo(ShiftTopTabType.NextSteps);
							break;
						case "contract":
							tabInfo = new CommonTopTabInfo(ShiftTopTabType.Contract);
							break;
						case "support-materials":
							tabInfo = new CommonTopTabInfo(ShiftTopTabType.SupportMaterials);
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					tabInfo.LoadData(tabConfigNode, resourceManager);
					TabsInfo.Add(tabInfo);
				}
			}

			ClientGoalsLists.Load(resourceManager.DataClientGoalsFile);
			TargetCustomersLists.LoadHHIData(resourceManager.DataHHIFile);
			TargetCustomersLists.LoadDemoData(resourceManager.DataDemoFile);
			TargetCustomersLists.LoadGeographyData(resourceManager.DataGeographyFile);
		}
	}
}
