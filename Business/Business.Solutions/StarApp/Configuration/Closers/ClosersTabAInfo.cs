using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Closers
{
	public class ClosersTabAInfo : StarTabWithHeaderInfo
	{
		public override StarChildTabType TabType => StarChildTabType.A;

		public Image Clipart1Image { get; private set; }
		public Image Clipart2Image { get; private set; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }

		public ClosersTabAInfo()
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab11SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubARightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab11SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubAFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab11SubABackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab11SubABackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab11SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab11SubA1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab11SubA2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab11SubA2File.LocalPath)
				: null;

			if (!resourceManager.DataClosersPartAFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataClosersPartAFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP11A");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP11AHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP11ASubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP11AClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP11AClipart2");
		}
	}
}
