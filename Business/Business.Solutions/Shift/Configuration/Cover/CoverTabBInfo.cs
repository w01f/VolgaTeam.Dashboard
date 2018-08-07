using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Cover
{
	public class CoverTabBInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader1Configuration { get; set; }

		public TextEditorConfiguration Calendar1Configuration { get; set; }

		public CoverTabBInfo() : base(ShiftChildTabType.B)
		{
			Clipart1Configuration = new ClipartConfiguration();
			SubHeader1Configuration = TextEditorConfiguration.Empty();
			Calendar1Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab1SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubBRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab1SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubBFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab1SubBBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubBBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab1SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab1SubB1File.LocalPath)
				: null;

			if (!resourceManager.DataCoverPartBFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataCoverPartBFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT01B");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT01BHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT01BSubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT01BClipart1");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT01BHeader");
			SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT01BSubheader1");
			Calendar1Configuration = TextEditorConfiguration.FromXml(node, "Calendar1");
		}
	}
}
