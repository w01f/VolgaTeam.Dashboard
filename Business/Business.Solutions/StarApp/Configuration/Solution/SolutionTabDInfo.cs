using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Solution
{
	public class SolutionTabDInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.D;

		public Image Clipart1Image { get; private set; }

		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public SolutionTabDInfo()
		{
			Clipart1Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab10SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubDRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab10SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubDFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab10SubDBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubDBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab10SubD1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab10SubD1File.LocalPath)
				: null;

			if (!resourceManager.DataSolutionPartDFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataSolutionPartDFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP10D");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP10DHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP10DSubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
				}
			}
			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP10DClipart1");
		}
	}
}
