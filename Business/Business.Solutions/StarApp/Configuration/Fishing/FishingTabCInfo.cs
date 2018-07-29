using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Fishing
{
	public class FishingTabCInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.C;

		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader2DefaultValue { get; private set; }
		public string SubHeader3DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public string SubHeader2Placeholder { get; private set; }
		public string SubHeader3Placeholder { get; private set; }

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab3SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubCRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab3SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubCFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab3SubCBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubCBackgroundFile.LocalPath)
				: null;

			if (!resourceManager.DataFishingPartCFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataFishingPartCFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP03C");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP03CHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP03CSubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
					case "CP03CSubheader2":
						if (item.IsPlaceholder)
							SubHeader2Placeholder = item.Value;
						else
							SubHeader2DefaultValue = item.Value;
						break;
					case "CP03CSubheader3":
						if (item.IsPlaceholder)
							SubHeader3Placeholder = item.Value;
						else
							SubHeader3DefaultValue = item.Value;
						break;
				}
			}
		}
	}
}
