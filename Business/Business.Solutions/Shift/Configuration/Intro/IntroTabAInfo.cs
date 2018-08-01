using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Intro
{
	public class IntroTabAInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public Image Clipart2Image { get; private set; }
		public Image Clipart3Image { get; private set; }
		public Image Clipart4Image { get; private set; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }
		public ClipartConfiguration Clipart4Configuration { get; private set; }

		public IntroTabAInfo() : base(ShiftChildTabType.A)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
			Clipart4Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab2SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubARightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab2SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubAFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab2SubABackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab2SubABackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab2SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubA1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab2SubA2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubA2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab2SubA3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubA3File.LocalPath)
				: null;
			Clipart4Image = resourceManager.ClipartTab2SubA4File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab2SubA4File.LocalPath)
				: null;

			if (!resourceManager.DataIntroPartAFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataIntroPartAFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT02A");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT02AHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT02AClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT02AClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT02AClipart3");
			Clipart4Configuration = ClipartConfiguration.FromXml(node, "SHIFT02AClipart4");

			EditorConfiguration = TextEditorConfiguration.FromXml(node);
		}
	}
}
