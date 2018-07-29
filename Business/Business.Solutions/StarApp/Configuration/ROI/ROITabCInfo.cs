using System;
using System.Drawing;
using System.Globalization;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.ROI
{
	public class ROITabCInfo : StarChildTabInfo
	{
		public override StarChildTabType TabType => StarChildTabType.C;

		public Image Clipart1Image { get; private set; }
		public Image Clipart2Image { get; private set; }
		public Image Clipart3Image { get; private set; }

		public string SubHeader1DefaultValue { get; private set; }
		public decimal? SubHeader2DefaultValue { get; private set; }
		public string SubHeader3DefaultValue { get; private set; }
		public decimal? SubHeader4DefaultValue { get; private set; }
		public string SubHeader5DefaultValue { get; private set; }
		public string SubHeader6DefaultValue { get; private set; }
		public decimal? SubHeader7DefaultValue { get; private set; }
		public string SubHeader8DefaultValue { get; private set; }
		public string SubHeader9DefaultValue { get; private set; }
		public string SubHeader10DefaultValue { get; private set; }
		public string SubHeader11DefaultValue { get; private set; }
		public string SubHeader12DefaultValue { get; private set; }
		public decimal? SubHeader13DefaultValue { get; private set; }
		public string SubHeader14DefaultValue { get; private set; }
		public string SubHeader15DefaultValue { get; private set; }

		public string SubHeader1Placeholder { get; private set; }
		public string SubHeader3Placeholder { get; private set; }
		public string SubHeader5Placeholder { get; private set; }
		public string SubHeader6Placeholder { get; private set; }
		public string SubHeader8Placeholder { get; private set; }
		public string SubHeader9Placeholder { get; private set; }
		public string SubHeader10Placeholder { get; private set; }
		public string SubHeader11Placeholder { get; private set; }
		public string SubHeader12Placeholder { get; private set; }
		public string SubHeader14Placeholder { get; private set; }
		public string SubHeader15Placeholder { get; private set; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public ROITabCInfo()
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab6SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubCRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab6SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubCFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab6SubCBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubCBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab6SubC1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubC1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab6SubC2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubC2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab6SubC3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubC3File.LocalPath)
				: null;

			if (!resourceManager.DataROIPartCFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataROIPartCFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP06C");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP06CHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP06CSubHeader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
					case "CP06CSubHeader2":
						SubHeader2DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06CSubHeader3":
						if (item.IsPlaceholder)
							SubHeader3Placeholder = item.Value;
						else
							SubHeader3DefaultValue = item.Value;
						break;
					case "CP06CSubHeader4":
						SubHeader4DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06CSubHeader5":
						if (item.IsPlaceholder)
							SubHeader5Placeholder = item.Value;
						else
							SubHeader5DefaultValue = item.Value;
						break;
					case "CP06CSubHeader6":
						if (item.IsPlaceholder)
							SubHeader6Placeholder = item.Value;
						else
							SubHeader6DefaultValue = item.Value;
						break;
					case "CP06CSubHeader7":
						SubHeader7DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06CSubHeader8":
						if (item.IsPlaceholder)
							SubHeader8Placeholder = item.Value;
						else
							SubHeader8DefaultValue = item.Value;
						break;
					case "CP06CSubHeader9":
						if (item.IsPlaceholder)
							SubHeader9Placeholder = item.Value;
						else
							SubHeader9DefaultValue = item.Value;
						break;
					case "CP06CSubHeader10":
						if (item.IsPlaceholder)
							SubHeader10Placeholder = item.Value;
						else
							SubHeader10DefaultValue = item.Value;
						break;
					case "CP06CSubHeader11":
						if (item.IsPlaceholder)
							SubHeader11Placeholder = item.Value;
						else
							SubHeader11DefaultValue = item.Value;
						break;
					case "CP06CSubHeader12":
						if (item.IsPlaceholder)
							SubHeader12Placeholder = item.Value;
						else
							SubHeader12DefaultValue = item.Value;
						break;
					case "CP06CSubHeader13":
						SubHeader13DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06CSubHeader14":
						if (item.IsPlaceholder)
							SubHeader14Placeholder = item.Value;
						else
							SubHeader14DefaultValue = item.Value;
						break;
					case "CP06CSubHeader15":
						if (item.IsPlaceholder)
							SubHeader15Placeholder = item.Value;
						else
							SubHeader15DefaultValue = item.Value;
						break;
				}
			}
			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP06CClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP06CClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "CP06CClipart3");
		}
	}
}
