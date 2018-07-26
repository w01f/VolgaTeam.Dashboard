using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Customer
{
	public class CustomerTabBInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.B;

		public Image Clipart1Image { get; private set; }
		public Image Clipart2Image { get; private set; }

		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader2DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public string SubHeader2Placeholder { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public CustomerTabBInfo()
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab4SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubBRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab4SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubBFooterFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab4SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubB1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab4SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubB2File.LocalPath)
				: null;

			if (!resourceManager.DataCustomerPartBFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataCustomerPartBFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP04B");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP04BHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP04BSubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
					case "CP04BSubheader2":
						if (item.IsPlaceholder)
							SubHeader2Placeholder = item.Value;
						else
							SubHeader2DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP04BClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP04BClipart2");
		}
	}
}
