using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Video
{
	public class VideoTabAInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.A;

		public Image Clipart1Image { get; private set; }
		
		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public VideoTabAInfo()
		{
			Clipart1Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab8SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubARightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab8SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubAFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab8SubABackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubABackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab8SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab8SubA1File.LocalPath)
				: null;

			if (!resourceManager.DataVideoPartAFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataVideoPartAFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP08A");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP08AHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP08ASubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP08AClipart1");
		}
	}
}
