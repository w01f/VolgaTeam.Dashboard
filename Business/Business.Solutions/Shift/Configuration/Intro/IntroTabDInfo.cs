using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Intro
{
	public class IntroTabDInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public Image Clipart2Image { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public Image Clipart3Image { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public Image Clipart4Image { get; private set; }
		public ClipartConfiguration Clipart4Configuration { get; private set; }

		public IntroTabDInfo() : base(ShiftChildTabType.D)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
			Clipart4Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab2SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubDRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab2SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubDFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab2SubDBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubDBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab2SubD1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubD1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab2SubD2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubD2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab2SubD3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubD3File.LocalPath)
				: null;
			Clipart4Image = resourceManager.ClipartTab2SubD4File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubD4File.LocalPath)
				: null;

			if (!resourceManager.DataIntroPartDFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataIntroPartDFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT02D");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT02DHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT02DClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT02DClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT02DClipart3");
			Clipart4Configuration = ClipartConfiguration.FromXml(node, "SHIFT02DClipart4");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT02DHeader");
		}
	}
}
