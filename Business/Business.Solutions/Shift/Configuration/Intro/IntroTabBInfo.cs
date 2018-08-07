using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Intro
{
	public class IntroTabBInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public Image Clipart2Image { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public Image Clipart3Image { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public Image Clipart4Image { get; private set; }
		public ClipartConfiguration Clipart4Configuration { get; private set; }

		public IntroTabBInfo() : base(ShiftChildTabType.B)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
			Clipart4Configuration = new ClipartConfiguration();
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
			Clipart3Image = resourceManager.ClipartTab2SubB3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubB3File.LocalPath)
				: null;
			Clipart4Image = resourceManager.ClipartTab2SubB4File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubB4File.LocalPath)
				: null;

			if (!resourceManager.DataIntroPartBFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataIntroPartBFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT02B");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT02BHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT02BClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT02BClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT02BClipart3");
			Clipart4Configuration = ClipartConfiguration.FromXml(node, "SHIFT02BClipart4");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT02BHeader");
		}
	}
}
