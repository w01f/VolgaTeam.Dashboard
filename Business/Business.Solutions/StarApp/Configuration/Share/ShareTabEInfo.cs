using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Share
{
	public class ShareTabEInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.E;

		public Image Clipart1Image { get; private set; }
		public Image Clipart2Image { get; private set; }
		public Image Clipart3Image { get; private set; }

		public List<ListDataItem> Combo1Items { get; }
		public List<ListDataItem> Combo2Items { get; }
		public List<ListDataItem> Combo3Items { get; }
		public List<ListDataItem> Combo4Items { get; }
		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader2DefaultValue { get; private set; }
		public string SubHeader3DefaultValue { get; private set; }
		public string SubHeader4DefaultValue { get; private set; }
		public string SubHeader5DefaultValue { get; private set; }
		public string SubHeader6DefaultValue { get; private set; }
		public decimal? SubHeader7DefaultValue { get; private set; }
		public string SubHeader8DefaultValue { get; private set; }
		public string SubHeader9DefaultValue { get; private set; }
		public string SubHeader10DefaultValue { get; private set; }

		public string SubHeader1Placeholder { get; private set; }
		public string SubHeader2Placeholder { get; private set; }
		public string SubHeader3Placeholder { get; private set; }
		public string SubHeader4Placeholder { get; private set; }
		public string SubHeader5Placeholder { get; private set; }
		public string SubHeader6Placeholder { get; private set; }
		public string SubHeader8Placeholder { get; private set; }
		public string SubHeader9Placeholder { get; private set; }
		public string SubHeader10Placeholder { get; private set; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public ShareTabEInfo()
		{
			Combo1Items = new List<ListDataItem>();
			Combo2Items = new List<ListDataItem>();
			Combo3Items = new List<ListDataItem>();
			Combo4Items = new List<ListDataItem>();
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab5SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubERightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab5SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubEFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab5SubEBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubEBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab5SubE1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubE1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab5SubE2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubE2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab5SubE3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubE3File.LocalPath)
				: null;

			if (!resourceManager.DataSharePartEFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataSharePartEFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP05E");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP05EHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP05DBillionCombo1":
						if (!String.IsNullOrEmpty(item.Value))
							Combo1Items.Add(item);
						break;
					case "CP05EPercentCombo2":
						if (!String.IsNullOrEmpty(item.Value))
							Combo2Items.Add(item);
						break;
					case "CP05EPopulationCombo3":
						if (!String.IsNullOrEmpty(item.Value))
							Combo3Items.Add(item);
						break;
					case "CP05ESharePercentCombo3":
						if (!String.IsNullOrEmpty(item.Value))
							Combo4Items.Add(item);
						break;
					case "CP05ESubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
					case "CP05ESubheader2":
						if (item.IsPlaceholder)
							SubHeader2Placeholder = item.Value;
						else
							SubHeader2DefaultValue = item.Value;
						break;
					case "CP05ESubheader3":
						if (item.IsPlaceholder)
							SubHeader3Placeholder = item.Value;
						else
							SubHeader3DefaultValue = item.Value;
						break;
					case "CP05ESubheader4":
						if (item.IsPlaceholder)
							SubHeader4Placeholder = item.Value;
						else
							SubHeader4DefaultValue = item.Value;
						break;
					case "CP05ESubheader5":
						if (item.IsPlaceholder)
							SubHeader5Placeholder = item.Value;
						else
							SubHeader5DefaultValue = item.Value;
						break;
					case "CP05ESubheader6":
						if (item.IsPlaceholder)
							SubHeader6Placeholder = item.Value;
						else
							SubHeader6DefaultValue = item.Value;
						break;
					case "CP05ESubheader7":
						SubHeader7DefaultValue = Decimal.Parse(item.Value ?? "0",
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP05ESubheader8":
						if (item.IsPlaceholder)
							SubHeader8Placeholder = item.Value;
						else
							SubHeader8DefaultValue = item.Value;
						break;
					case "CP05ESubheader9":
						if (item.IsPlaceholder)
							SubHeader9Placeholder = item.Value;
						else
							SubHeader9DefaultValue = item.Value;
						break;
					case "CP05ESubheader10":
						if (item.IsPlaceholder)
							SubHeader10Placeholder = item.Value;
						else
							SubHeader10DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP05EClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP05EClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "CP05EClipart3");
		}
	}
}
