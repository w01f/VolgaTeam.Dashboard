using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Fishing
{
	public class FishingTabBInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.B;

		public Image Clipart1Image { get; private set; }
		public Image Clipart2Image { get; private set; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public FishingTabBInfo()
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab3SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubBRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab3SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubBFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab3SubBBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubBBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab3SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab3SubB1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab3SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab3SubB2File.LocalPath)
				: null;

			if (!resourceManager.DataFishingPartBFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataFishingPartBFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP03B");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP03BHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP03BClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP03BClipart2");
		}
	}
}
