using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Approach
{
	public class ApproachTabAInfo : ShiftTabWithHeaderInfo
	{
		public TabSelectorConfiguration TabSelector { get; set; }

		public List<ListDataItem> Combo1Items { get; }
		public TextEditorConfiguration Combo1Configuration { get; set; }

		public List<ApproachItemInfo> ApproachItems { get; }

		public ApproachTabAInfo() : base(ShiftChildTabType.A)
		{
			TabSelector = TabSelectorConfiguration.Empty();
			Combo1Items = new List<ListDataItem>();
			Combo1Configuration = TextEditorConfiguration.Empty();
			ApproachItems = new List<ApproachItemInfo>();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab15SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab15SubARightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab15SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab15SubAFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab15SubABackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab15SubABackgroundFile.LocalPath)
				: null;

			if (resourceManager.DataApproachesCommonFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataApproachesCommonFile.LocalPath);

				var itemInfoNodes = document.SelectNodes("//OurApproach/Approach")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var itemInfoNode in itemInfoNodes)
					ApproachItems.Add(ApproachItemInfo.FromXml(itemInfoNode, resourceManager.ClipartTab15SubAFolder));
			}

			if (resourceManager.DataApproachPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataApproachPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT09A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT09AHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
						case "SHIFT09ACombo1":
							if (!String.IsNullOrEmpty(item.Value))
								Combo1Items.Add(item);
							break;
					}
				}

				TabSelector = TabSelectorConfiguration.FromXml(node, "SHIFT09ATabStrip1");

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT09AHeader");
				Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT09ACombo1");

				foreach (var itemInfo in ApproachItems)
					itemInfo.SubheaderConfiguration = TextEditorConfiguration.FromXml(node, String.Format("Button{0}", itemInfo.Id));
			}
		}
	}
}
