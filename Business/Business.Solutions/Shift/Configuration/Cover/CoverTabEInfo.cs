using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Cover
{
	public class CoverTabEInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public Image Clipart2Image { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader1Configuration { get; set; }

		public TextEditorConfiguration Calendar1Configuration { get; set; }

		public CoverTabEInfo() : base(ShiftChildTabType.E)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			SubHeader1Configuration = TextEditorConfiguration.Empty();
			Calendar1Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab1SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubERightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab1SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubEFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab1SubEBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubEBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab1SubE1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab1SubE1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab1SubE2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab1SubE2File.LocalPath)
				: null;

			if (!resourceManager.DataCoverPartEFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataCoverPartEFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT01E");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT01EHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT01ESubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT01EClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT01EClipart2");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT01EHeader");
			SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT01ESubheader1");
			Calendar1Configuration = TextEditorConfiguration.FromXml(node, "Calendar1");
		}
	}
}
