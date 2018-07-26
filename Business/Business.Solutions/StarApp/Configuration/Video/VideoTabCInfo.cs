using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Video
{
	public class VideoTabCInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.C;

		public Image Clipart1Image { get; private set; }
		
		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public VideoTabCInfo()
		{
			Clipart1Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab8SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubCRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab8SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubCFooterFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab8SubC1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab8SubC1File.LocalPath)
				: null;

			if (!resourceManager.DataVideoPartCFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataVideoPartCFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP08C");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP08CHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP08CSubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP08CClipart1");
		}
	}
}
