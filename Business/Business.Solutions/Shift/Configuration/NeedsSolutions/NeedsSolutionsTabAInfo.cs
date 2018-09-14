using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.NeedsSolutions
{
	public class NeedsSolutionsTabAInfo : ShiftTabWithHeaderInfo
	{
		public TabSelectorConfiguration TabSelector { get; set; }

		public List<ListDataItem> Combo1Items { get; }
		public TextEditorConfiguration Combo1Configuration { get; set; }

		public List<NeedsItemInfo> NeedsList { get; }

		public NeedsSolutionsTabAInfo() : base(ShiftChildTabType.A, ShiftTopTabType.NeedsSolutions)
		{
			TabSelector = TabSelectorConfiguration.Empty();
			Combo1Items = new List<ListDataItem>();
			Combo1Configuration = TextEditorConfiguration.Empty();
			NeedsList = new List<NeedsItemInfo>();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (resourceManager.DataNeedsCommonFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataNeedsCommonFile.LocalPath);

				var itemInfoNodes = document.SelectNodes("//SHIFTNeeds/Need")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var itemInfoNode in itemInfoNodes)
					NeedsList.Add(NeedsItemInfo.FromXml(itemInfoNode, resourceManager.ClipartTab7SubAFolder));
			}

			if (resourceManager.DataNeedsSolutionsPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataNeedsSolutionsPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT07A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT07AHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
						case "SHIFT07ACombo1":
							if (!String.IsNullOrEmpty(item.Value))
								Combo1Items.Add(item);
							break;
					}
				}

				TabSelector = TabSelectorConfiguration.FromXml(node, "SHIFT07ATabStrip1");

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT07AHeader");
				Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT07ACombo1");

				foreach (var itemInfo in NeedsList)
					itemInfo.SubheaderConfiguration = TextEditorConfiguration.FromXml(node, String.Format("NeedButton{0}", itemInfo.Id));
			}
		}
	}
}
