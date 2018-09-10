using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.NeedsSolutions
{
	public class NeedsSolutionsTabCInfo : ShiftTabWithHeaderInfo
	{
		public TabSelectorConfiguration TabSelector { get; set; }

		public List<ListDataItem> Combo1Items { get; }
		public TextEditorConfiguration Combo1Configuration { get; set; }

		public List<SolutionsItemInfo> SolutionsList { get; }

		public NeedsSolutionsTabCInfo() : base(ShiftChildTabType.C, ShiftTopTabType.IntegratedSolution)
		{
			TabSelector = TabSelectorConfiguration.Empty();
			Combo1Items = new List<ListDataItem>();
			Combo1Configuration = TextEditorConfiguration.Empty();
			SolutionsList = new List<SolutionsItemInfo>();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (resourceManager.DataSolutionsCommonFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionsCommonFile.LocalPath);

				var itemInfoNodes = document.SelectNodes("//Products/Product")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var itemInfoNode in itemInfoNodes)
					SolutionsList.Add(SolutionsItemInfo.FromXml(itemInfoNode, resourceManager.ClipartTab7SubCFolder));
			}

			if (resourceManager.DataNeedsSolutionsPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataNeedsSolutionsPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT07C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT07CHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
						case "SHIFT07CCombo1":
							if (!String.IsNullOrEmpty(item.Value))
								Combo1Items.Add(item);
							break;
					}
				}

				TabSelector = TabSelectorConfiguration.FromXml(node, "SHIFT07CTabStrip1");

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT07CHeader");
				Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07CCombo1");

				foreach (var itemInfo in SolutionsList)
				{
					itemInfo.ButtonConfiguration =
						SolutionButtonConfiguration.FromXml(node, String.Format("ProductButton{0}", itemInfo.Id));
					itemInfo.SubheaderConfiguration =
						TextEditorConfiguration.FromXml(node, String.Format("ProductButton{0}", itemInfo.Id));
				}
			}
		}
	}
}
