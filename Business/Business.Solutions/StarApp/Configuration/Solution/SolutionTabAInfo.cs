using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Solution
{
	public class SolutionTabAInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.A;

		public Image Clipart1Image { get; private set; }
		
		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public SolutionTabAInfo()
		{
			Clipart1Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab10SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubARightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab10SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab10SubAFooterFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab10SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab10SubA1File.LocalPath)
				: null;

			if (!resourceManager.DataSolutionPartAFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataSolutionPartAFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP10A");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP10AHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP10ASubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
				}
			}
			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP10AClipart1");
		}
	}
}
