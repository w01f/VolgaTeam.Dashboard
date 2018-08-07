using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Intro
{
	public class IntroTabCInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public Image Clipart2Image { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public Image Clipart3Image { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public Image Clipart4Image { get; private set; }
		public ClipartConfiguration Clipart4Configuration { get; private set; }

		public IntroTabCInfo() : base(ShiftChildTabType.C)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
			Clipart4Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab2SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubCRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab2SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubCFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab2SubCBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubCBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab2SubC1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubC1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab2SubC2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubC2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab2SubC3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubC3File.LocalPath)
				: null;
			Clipart4Image = resourceManager.ClipartTab2SubC4File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubC4File.LocalPath)
				: null;

			if (!resourceManager.DataIntroPartCFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataIntroPartCFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT02C");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT02CHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT02CClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT02CClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT02CClipart3");
			Clipart4Configuration = ClipartConfiguration.FromXml(node, "SHIFT02CClipart4");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT02CHeader");
		}
	}
}
