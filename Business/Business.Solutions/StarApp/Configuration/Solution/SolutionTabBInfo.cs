using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Solution
{
	public class SolutionTabBInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.B;

		public Image Clipart1Image { get; private set; }
		public Image Clipart2Image { get; private set; }
		public Image Clipart3Image { get; private set; }
		
		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public SolutionTabBInfo()
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab10SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubBRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab10SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubBFooterFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab10SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab10SubB1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab10SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab10SubB2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab10SubB3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab10SubB3File.LocalPath)
				: null;

			if (!resourceManager.DataSolutionPartBFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataSolutionPartBFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP10B");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP10BHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP10BSubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
				}
			}
			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP10BClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP10BClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "CP10BClipart3");
		}
	}
}
