using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Customer
{
	public class CustomerTabAInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.A;

		public Image Clipart1Image { get; private set; }
		public Image Clipart2Image { get; private set; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public CustomerTabAInfo()
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab4SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubARightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab4SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubAFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab4SubABackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubABackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab4SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubA1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab4SubA2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubA2File.LocalPath)
				: null;

			if (!resourceManager.DataCustomerPartAFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataCustomerPartAFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP04A");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP04AHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP04AClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP04AClipart2");
		}
	}
}
