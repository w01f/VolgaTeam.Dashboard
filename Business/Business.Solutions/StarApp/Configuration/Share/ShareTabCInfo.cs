using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Share
{
	public class ShareTabCInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.C;

		public Image Clipart1Image { get; private set; }
		public Image Clipart2Image { get; private set; }
		public Image Clipart3Image { get; private set; }

		public List<ListDataItem> Combo1Items { get; }
		public List<ListDataItem> Combo2Items { get; }
		public List<ListDataItem> Combo3Items { get; }
		public List<ListDataItem> Combo4Items { get; }
		public List<ListDataItem> Combo5Items { get; }
		public List<ListDataItem> Combo6Items { get; }
		public decimal? SubHeader1DefaultValue { get; private set; }
		public string SubHeader2DefaultValue { get; private set; }
		public string SubHeader3DefaultValue { get; private set; }
		public string SubHeader4DefaultValue { get; private set; }

		public string SubHeader2Placeholder { get; private set; }
		public string SubHeader3Placeholder { get; private set; }
		public string SubHeader4Placeholder { get; private set; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public ShareTabCInfo()
		{
			Combo1Items = new List<ListDataItem>();
			Combo2Items = new List<ListDataItem>();
			Combo3Items = new List<ListDataItem>();
			Combo4Items = new List<ListDataItem>();
			Combo5Items = new List<ListDataItem>();
			Combo6Items = new List<ListDataItem>();
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab5SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubCRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab5SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubCFooterFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab5SubC1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubC1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab5SubC2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubC2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab5SubC3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubC3File.LocalPath)
				: null;

			if (!resourceManager.DataSharePartCFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataSharePartCFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP05C");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP05CHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP05CBillionCombo1":
						if (!String.IsNullOrEmpty(item.Value))
							Combo1Items.Add(item);
						break;
					case "CP05CYearCombo2":
						if (!String.IsNullOrEmpty(item.Value))
							Combo2Items.Add(item);
						break;
					case "CP05CGeographyCombo3":
						if (!String.IsNullOrEmpty(item.Value))
							Combo3Items.Add(item);
						break;
					case "CP05CPercentCombo4":
						if (!String.IsNullOrEmpty(item.Value))
							Combo4Items.Add(item);
						break;
					case "CP05CPopulationCombo5":
						if (!String.IsNullOrEmpty(item.Value))
							Combo5Items.Add(item);
						break;
					case "CP05CSharePercentCombo6":
						if (!String.IsNullOrEmpty(item.Value))
							Combo6Items.Add(item);
						break;
					case "CP05CSubheader1":
						SubHeader1DefaultValue = Decimal.Parse(item.Value ?? "0",
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP05CSubheader2":
						if (item.IsPlaceholder)
							SubHeader2Placeholder = item.Value;
						else
							SubHeader2DefaultValue = item.Value;
						break;
					case "CP05CSubheader3":
						if (item.IsPlaceholder)
							SubHeader3Placeholder = item.Value;
						else
							SubHeader3DefaultValue = item.Value;
						break;
					case "CP05CSubheader4":
						if (item.IsPlaceholder)
							SubHeader4Placeholder = item.Value;
						else
							SubHeader4DefaultValue = item.Value;
						break;
				}
			}
			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP05CClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP05CClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "CP05CClipart3");
		}
	}
}
