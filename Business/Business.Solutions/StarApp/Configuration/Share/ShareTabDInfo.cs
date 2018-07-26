using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Share
{
	public class ShareTabDInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.D;

		public Image Clipart1Image { get; private set; }
		public Image Clipart2Image { get; private set; }
		public Image Clipart3Image { get; private set; }

		public List<ListDataItem> Combo1Items { get; }
		public List<ListDataItem> Combo2Items { get; }
		public List<ListDataItem> Combo3Items { get; }
		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader2DefaultValue { get; private set; }
		public string SubHeader3DefaultValue { get; private set; }
		public decimal? SubHeader4DefaultValue { get; private set; }
		public string SubHeader5DefaultValue { get; private set; }
		public string SubHeader6DefaultValue { get; private set; }
		public string SubHeader7DefaultValue { get; private set; }
		public string SubHeader8DefaultValue { get; private set; }
		public string SubHeader9DefaultValue { get; private set; }

		public string SubHeader1Placeholder { get; private set; }
		public string SubHeader2Placeholder { get; private set; }
		public string SubHeader3Placeholder { get; private set; }
		public string SubHeader5Placeholder { get; private set; }
		public string SubHeader6Placeholder { get; private set; }
		public string SubHeader7Placeholder { get; private set; }
		public string SubHeader8Placeholder { get; private set; }
		public string SubHeader9Placeholder { get; private set; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public ShareTabDInfo()
		{
			Combo1Items = new List<ListDataItem>();
			Combo2Items = new List<ListDataItem>();
			Combo3Items = new List<ListDataItem>();
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab5SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubDRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab5SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubDFooterFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab5SubD1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubD1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab5SubD2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubD2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab5SubD3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubD3File.LocalPath)
				: null;

			if (!resourceManager.DataSharePartDFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataSharePartDFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP05D");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP05DHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP05DPercentCombo1":
						if (!String.IsNullOrEmpty(item.Value))
							Combo1Items.Add(item);
						break;
					case "CP05DPopulationCombo2":
						if (!String.IsNullOrEmpty(item.Value))
							Combo2Items.Add(item);
						break;
					case "CP05DSharePercentCombo6":
						if (!String.IsNullOrEmpty(item.Value))
							Combo3Items.Add(item);
						break;
					case "CP05DSubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
					case "CP05DSubheader2":
						if (item.IsPlaceholder)
							SubHeader2Placeholder = item.Value;
						else
							SubHeader2DefaultValue = item.Value;
						break;
					case "CP05DSubHeader3":
						if (item.IsPlaceholder)
							SubHeader3Placeholder = item.Value;
						else
							SubHeader3DefaultValue = item.Value;
						break;
					case "CP05DSubHeader4":
						SubHeader4DefaultValue = Decimal.Parse(item.Value ?? "0",
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP05DSubHeader5":
						if (item.IsPlaceholder)
							SubHeader5Placeholder = item.Value;
						else
							SubHeader5DefaultValue = item.Value;
						break;
					case "CP05DSubHeader6":
						if (item.IsPlaceholder)
							SubHeader6Placeholder = item.Value;
						else
							SubHeader6DefaultValue = item.Value;
						break;
					case "CP05DSubHeader7":
						if (item.IsPlaceholder)
							SubHeader7Placeholder = item.Value;
						else
							SubHeader7DefaultValue = item.Value;
						break;
					case "CP05DSubHeader8":
						if (item.IsPlaceholder)
							SubHeader8Placeholder = item.Value;
						else
							SubHeader8DefaultValue = item.Value;
						break;
					case "CP05DSubHeader9":
						if (item.IsPlaceholder)
							SubHeader9Placeholder = item.Value;
						else
							SubHeader9DefaultValue = item.Value;
						break;
				}
			}
			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP05DClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP05DClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "CP05DClipart3");
		}
	}
}
