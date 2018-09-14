using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Market
{
	public class MarketTabBInfo : StarTabWithHeaderInfo
	{
		public override StarChildTabType TabType => StarChildTabType.B;

		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab7_B_Clipart1;
		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab7_B_Clipart2;
		public Image Clipart3Image => _resourceManager.GraphicResources?.Tab7_B_Clipart3;
		public Image Clipart4Image => _resourceManager.GraphicResources?.Tab7_B_Clipart4;
		public Image Clipart5Image => _resourceManager.GraphicResources?.Tab7_B_Clipart5;

		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader2DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public string SubHeader2Placeholder { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }
		public ClipartConfiguration Clipart4Configuration { get; private set; }
		public ClipartConfiguration Clipart5Configuration { get; private set; }

		public MarketTabBInfo() : base(StarTopTabType.Market)
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
