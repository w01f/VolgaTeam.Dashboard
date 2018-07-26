using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Market
{
	public class MarketTabCInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.C;

		public Image Clipart1Image { get; private set; }
		public Image Clipart2Image { get; private set; }
		public Image Clipart3Image { get; private set; }
		public Image Clipart4Image { get; private set; }

		public List<ListDataItem> Combo1Items { get; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }
		public ClipartConfiguration Clipart4Configuration { get; private set; }

		public MarketTabCInfo()
		{
			Combo1Items = new List<ListDataItem>();
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
			Clipart4Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab7SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubCRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab7SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubCFooterFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab7SubC1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubC1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab7SubC2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubC2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab7SubC3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubC3File.LocalPath)
				: null;
			Clipart4Image = resourceManager.ClipartTab7SubC4File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubC4File.LocalPath)
				: null;

			if (!resourceManager.DataMarketPartCFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataMarketPartCFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP07C");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP07CHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP07CCombo1":
						if (!String.IsNullOrEmpty(item.Value))
							Combo1Items.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP07CClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP07CClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "CP07CClipart3");
			Clipart4Configuration = ClipartConfiguration.FromXml(node, "CP07CClipart4");
		}
	}
}
