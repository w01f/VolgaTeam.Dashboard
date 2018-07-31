using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Cover
{
	public class CoverTabDInfo : ShiftChildTabInfo
	{
		public Image Clipart1Image { get; private set; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }

		public CoverTabDInfo() : base(ShiftChildTabType.D)
		{
			Clipart1Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab1SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubDRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab1SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubDFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab1SubDBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubDBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab1SubD1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab1SubD1File.LocalPath)
				: null;

			if (!resourceManager.DataCoverPartDFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataCoverPartDFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT01D");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT01DHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT01DSubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT01DClipart1");

			EditorConfiguration = TextEditorConfiguration.FromXml(node);
		}
	}
}
