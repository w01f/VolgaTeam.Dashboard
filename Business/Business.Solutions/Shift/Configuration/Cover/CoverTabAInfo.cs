using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Cover
{
	public class CoverTabAInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader1Configuration { get; set; }

		public TextEditorConfiguration Calendar1Configuration { get; set; }

		public CoverTabAInfo() : base(ShiftChildTabType.A)
		{
			Clipart1Configuration = new ClipartConfiguration();
			SubHeader1Configuration = TextEditorConfiguration.Empty();
			Calendar1Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab1SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubARightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab1SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubAFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab1SubABackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubABackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab1SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab1SubA1File.LocalPath)
				: null;

			if (!resourceManager.DataCoverPartAFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataCoverPartAFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT01A");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT01AHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT01ASubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT01AClipart1");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT01AHeader");
			SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT01ASubheader1");
			Calendar1Configuration = TextEditorConfiguration.FromXml(node, "Calendar1");
		}
	}
}
