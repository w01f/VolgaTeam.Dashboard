using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Common.Enums;
using Asa.Business.Solutions.Shift.Configuration.Cleanslate;
using Asa.Business.Solutions.Shift.Enums;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public class ShiftSolutionInfo : BaseSolutionInfo
	{
		public List<ShiftTopTabInfo> TabsInfo { get; }

		public ShiftSolutionInfo()
		{
			Type = SolutionType.Shift;

			TabsInfo = new List<ShiftTopTabInfo>();
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
							tabInfo = new CommonTopTabTabInfo(ShiftTopTabType.Cover);
							break;
						case "intro":
							tabInfo = new CommonTopTabTabInfo(ShiftTopTabType.Intro);
							break;
						case "agenda":
							tabInfo = new CommonTopTabTabInfo(ShiftTopTabType.Agenda);
							break;
						case "goals":
							tabInfo = new CommonTopTabTabInfo(ShiftTopTabType.Goals);
							break;
						case "market":
							tabInfo = new CommonTopTabTabInfo(ShiftTopTabType.Market);
							break;
						case "partnership":
							tabInfo = new CommonTopTabTabInfo(ShiftTopTabType.Partnership);
							break;
						case "needs-solutions":
							tabInfo = new CommonTopTabTabInfo(ShiftTopTabType.NeedsSolutions);
							break;
						case "cbc":
							tabInfo = new CommonTopTabTabInfo(ShiftTopTabType.CBC);
							break;
						case "integrated-solution":
							tabInfo = new CommonTopTabTabInfo(ShiftTopTabType.IntegratedSolution);
							break;
						case "investment":
							tabInfo = new CommonTopTabTabInfo(ShiftTopTabType.Investment);
							break;
						case "next-steps":
							tabInfo = new CommonTopTabTabInfo(ShiftTopTabType.NextSteps);
							break;
						case "contract":
							tabInfo = new CommonTopTabTabInfo(ShiftTopTabType.Contract);
							break;
						case "support-materials":
							tabInfo = new CommonTopTabTabInfo(ShiftTopTabType.SupportMaterials);
							break;
						default:
							throw new ArgumentOutOfRangeException("Star tab type is not defined");
					}
					tabInfo.LoadData(tabConfigNode, resourceManager);
					TabsInfo.Add(tabInfo);
				}
			}
		}
	}
}
