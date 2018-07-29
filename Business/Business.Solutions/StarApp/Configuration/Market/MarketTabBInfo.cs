using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Market
{
	public class MarketTabBInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.B;

		public Image Clipart1Image { get; private set; }
		public Image Clipart2Image { get; private set; }
		public Image Clipart3Image { get; private set; }
		public Image Clipart4Image { get; private set; }
		public Image Clipart5Image { get; private set; }

		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader2DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public string SubHeader2Placeholder { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }
		public ClipartConfiguration Clipart4Configuration { get; private set; }
		public ClipartConfiguration Clipart5Configuration { get; private set; }

		public MarketTabBInfo()
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
			Clipart4Configuration = new ClipartConfiguration();
			Clipart5Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab7SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubBRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab7SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubBFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab7SubBBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab7SubBBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab7SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubB1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab7SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubB2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab7SubB3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubB3File.LocalPath)
				: null;
			Clipart4Image = resourceManager.ClipartTab7SubB4File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubB4File.LocalPath)
				: null;
			Clipart5Image = resourceManager.ClipartTab7SubB5File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab7SubB5File.LocalPath)
				: null;

			if (!resourceManager.DataMarketPartBFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataMarketPartBFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP07B");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP07BHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP07BSubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
					case "CP07BSubheader2":
						if (item.IsPlaceholder)
							SubHeader2Placeholder = item.Value;
						else
							SubHeader2DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP07BClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP07BClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "CP07BClipart3");
			Clipart4Configuration = ClipartConfiguration.FromXml(node, "CP07BClipart4");
			Clipart5Configuration = ClipartConfiguration.FromXml(node, "CP07BClipart5");
		}
	}
}
