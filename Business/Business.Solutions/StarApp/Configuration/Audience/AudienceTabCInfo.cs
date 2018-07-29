using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Audience
{
	public class AudienceTabCInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.C;

		public Image Clipart1Image { get; private set; }
		public Image Clipart2Image { get; private set; }
		public Image Clipart3Image { get; private set; }
		public Image Clipart4Image { get; private set; }

		public List<ListDataItem> Combo1Items { get; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }
		public ClipartConfiguration Clipart4Configuration { get; private set; }

		public AudienceTabCInfo()
		{
			Combo1Items = new List<ListDataItem>();
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
			Clipart4Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab9SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubCRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab9SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubCFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab9SubCBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubCBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab9SubC1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab9SubC1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab9SubC2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab9SubC2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab9SubC3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab9SubC3File.LocalPath)
				: null;
			Clipart4Image = resourceManager.ClipartTab9SubC4File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab9SubC4File.LocalPath)
				: null;

			if (!resourceManager.DataAudiencePartCFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataAudiencePartCFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP09C");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP09CHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP09CCombo1":
						if (!String.IsNullOrEmpty(item.Value))
							Combo1Items.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP09CClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP09CClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "CP09CClipart3");
			Clipart4Configuration = ClipartConfiguration.FromXml(node, "CP09CClipart4");
		}
	}
}
