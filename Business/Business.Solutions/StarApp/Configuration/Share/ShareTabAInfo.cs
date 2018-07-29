using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Share
{
	public class ShareTabAInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.A;

		public Image Clipart1Image { get; private set; }
		public Image Clipart2Image { get; private set; }
		public Image Clipart3Image { get; private set; }

		public List<ListDataItem> Combo1Items { get; }
		public List<ListDataItem> Combo2Items { get; }
		public List<ListDataItem> Combo3Items { get; }
		public List<ListDataItem> Combo4Items { get; }
		public string SubHeader1DefaultValue { get; private set; }
		public decimal? SubHeader2DefaultValue { get; private set; }
		public string SubHeader3DefaultValue { get; private set; }
		public string SubHeader4DefaultValue { get; private set; }
		public string SubHeader5DefaultValue { get; private set; }
		public string SubHeader6DefaultValue { get; private set; }
		public string SubHeader7DefaultValue { get; private set; }

		public string SubHeader1Placeholder { get; private set; }
		public string SubHeader3Placeholder { get; private set; }
		public string SubHeader4Placeholder { get; private set; }
		public string SubHeader5Placeholder { get; private set; }
		public string SubHeader6Placeholder { get; private set; }
		public string SubHeader7Placeholder { get; private set; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public ShareTabAInfo()
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

			RightLogo = resourceManager.LogoTab5SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubARightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab5SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubAFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab5SubABackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab5SubABackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab5SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubA1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab5SubA2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubA2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab5SubA3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubA3File.LocalPath)
				: null;

			if (!resourceManager.DataSharePartAFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataSharePartAFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP05A");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP05AHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP05ABillionCombo1":
						if (!String.IsNullOrEmpty(item.Value))
							Combo1Items.Add(item);
						break;
					case "CP05APercentCombo2":
						if (!String.IsNullOrEmpty(item.Value))
							Combo2Items.Add(item);
						break;
					case "CP05APopulationCombo3":
						if (!String.IsNullOrEmpty(item.Value))
							Combo3Items.Add(item);
						break;
					case "CP05ASharePointCombo4":
						if (!String.IsNullOrEmpty(item.Value))
							Combo4Items.Add(item);
						break;
					case "CP05ASubHeader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
					case "CP05ASubHeader2":
						SubHeader2DefaultValue = Decimal.Parse(item.Value ?? "0",
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP05ASubHeader3":
						if (item.IsPlaceholder)
							SubHeader3Placeholder = item.Value;
						else
							SubHeader3DefaultValue = item.Value;
						break;
					case "CP05ASubHeader4":
						if (item.IsPlaceholder)
							SubHeader4Placeholder = item.Value;
						else
							SubHeader4DefaultValue = item.Value;
						break;
					case "CP05ASubHeader5":
						if (item.IsPlaceholder)
							SubHeader5Placeholder = item.Value;
						else
							SubHeader5DefaultValue = item.Value;
						break;
					case "CP05ASubHeader6":
						if (item.IsPlaceholder)
							SubHeader6Placeholder = item.Value;
						else
							SubHeader6DefaultValue = item.Value;
						break;
					case "CP05ASubHeader7":
						if (item.IsPlaceholder)
							SubHeader7Placeholder = item.Value;
						else
							SubHeader7DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP05AClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP05AClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "CP05AClipart3");
		}
	}
}
