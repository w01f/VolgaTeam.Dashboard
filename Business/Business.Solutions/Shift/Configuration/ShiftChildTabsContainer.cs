using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration
{
	public abstract class ShiftChildTabsContainer : ShiftTopTabInfo
	{
		public List<ShiftChildTabInfo> ChildTabs { get; }

		protected ShiftChildTabsContainer(ShiftTopTabType tabType) : base(tabType)
		{
			ChildTabs = new List<ShiftChildTabInfo>();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			foreach (var tabConfigNode in configNode.SelectNodes(@"./SubTab")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
			{
				var tabId = tabConfigNode.SelectSingleNode("./Type")?.InnerText?.ToLower();

				ShiftChildTabInfo childTabInfo;
				switch (tabId)
				{
					case "u":
						childTabInfo = new SlidesChildTabInfo(ShiftChildTabType.U, TabType);
						break;
					case "v":
						childTabInfo = new SlidesChildTabInfo(ShiftChildTabType.V, TabType);
						break;
					case "w":
						childTabInfo = new SlidesChildTabInfo(ShiftChildTabType.W, TabType);
						break;
					case "x":
						childTabInfo = new TilesChildTabInfo(ShiftChildTabType.X, TabType);
						break;
					case "y":
						childTabInfo = new TilesChildTabInfo(ShiftChildTabType.Y, TabType);
						break;
					case "z":
						childTabInfo = new TilesChildTabInfo(ShiftChildTabType.Z, TabType);
						break;
					default:
						childTabInfo = CreatChildTab(tabId);
						break;

				}
				childTabInfo.LoadData(tabConfigNode, resourceManager);
				ChildTabs.Add(childTabInfo);
			}
		}

		protected abstract ShiftChildTabInfo CreatChildTab(string tabId);
	}
}
