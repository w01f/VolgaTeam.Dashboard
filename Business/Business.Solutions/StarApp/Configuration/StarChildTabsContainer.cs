using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public abstract class StarChildTabsContainer : StarTopTabInfo
	{
		public List<StarChildTabInfo> ChildTabs { get; }

		protected StarChildTabsContainer()
		{
			ChildTabs = new List<StarChildTabInfo>();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			foreach (var tabConfigNode in configNode.SelectNodes(@"./SubTab")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
			{
				var tabId = tabConfigNode.SelectSingleNode("./Type")?.InnerText?.ToLower();

				StarChildTabInfo childTabInfo;
				switch (tabId)
				{
					case "k":
						childTabInfo = new SlidesChildTabInfo(TabType, StarChildTabType.K);
						break;
					case "l":
						childTabInfo = new SlidesChildTabInfo(TabType, StarChildTabType.L);
						break;
					case "m":
						childTabInfo = new SlidesChildTabInfo(TabType, StarChildTabType.M);
						break;
					case "n":
						childTabInfo = new SlidesChildTabInfo(TabType, StarChildTabType.N);
						break;
					case "o":
						childTabInfo = new SlidesChildTabInfo(TabType, StarChildTabType.O);
						break;
					case "u":
						childTabInfo = new SlidesChildTabInfo(TabType, StarChildTabType.U);
						break;
					case "v":
						childTabInfo = new SlidesChildTabInfo(TabType, StarChildTabType.V);
						break;
					case "w":
						childTabInfo = new SlidesChildTabInfo(TabType, StarChildTabType.W);
						break;
					case "x":
						childTabInfo = new TilesChildTabInfo(TabType, StarChildTabType.X);
						break;
					case "y":
						childTabInfo = new TilesChildTabInfo(TabType, StarChildTabType.Y);
						break;
					case "z":
						childTabInfo = new TilesChildTabInfo(TabType, StarChildTabType.Z);
						break;
					default:
						childTabInfo = CreatChildTab(tabId);
						break;

				}
				childTabInfo.LoadData(tabConfigNode, resourceManager);
				ChildTabs.Add(childTabInfo);
			}
		}

		protected abstract StarChildTabInfo CreatChildTab(string tabId);
	}
}
