using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.CNA
{
	public class CNATabBInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.B;

		public Image Clipart1Image { get; private set; }
		public Image Clipart2Image { get; private set; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public CNATabBInfo()
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab2SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubBRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab2SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubBFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab2SubBBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubBBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab2SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubB1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab2SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubB2File.LocalPath)
				: null;

			if (!resourceManager.DataCNAPartBFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataCNAPartBFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP02B");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP02BHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP02BClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP02BClipart2");
		}
	}
}
