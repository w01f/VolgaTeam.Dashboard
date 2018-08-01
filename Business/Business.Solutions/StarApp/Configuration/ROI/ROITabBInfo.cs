using System;
using System.Drawing;
using System.Globalization;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.ROI
{
	public class ROITabBInfo : StarTabWithHeaderInfo
	{
		public override StarChildTabType TabType => StarChildTabType.B;

		public Image Clipart1Image { get; private set; }
		public Image Clipart2Image { get; private set; }
		public Image Clipart3Image { get; private set; }

		public string SubHeader1DefaultValue { get; private set; }
		public decimal? SubHeader2DefaultValue { get; private set; }
		public string SubHeader3DefaultValue { get; private set; }
		public string SubHeader4DefaultValue { get; private set; }
		public decimal? SubHeader5DefaultValue { get; private set; }
		public string SubHeader6DefaultValue { get; private set; }
		public string SubHeader7DefaultValue { get; private set; }
		public decimal? SubHeader8DefaultValue { get; private set; }
		public string SubHeader9DefaultValue { get; private set; }
		public string SubHeader10DefaultValue { get; private set; }
		public decimal? SubHeader11DefaultValue { get; private set; }
		public string SubHeader12DefaultValue { get; private set; }
		public string SubHeader13DefaultValue { get; private set; }
		public string SubHeader14DefaultValue { get; private set; }
		public string SubHeader15DefaultValue { get; private set; }
		public string SubHeader16DefaultValue { get; private set; }
		public decimal? SubHeader17DefaultValue { get; private set; }
		public string SubHeader18DefaultValue { get; private set; }
		public string SubHeader19DefaultValue { get; private set; }
		public string SubHeader20DefaultValue { get; private set; }
		public string SubHeader21DefaultValue { get; private set; }
		public string SubHeader22DefaultValue { get; private set; }
		public string SubHeader23DefaultValue { get; private set; }
		public decimal? SubHeader24DefaultValue { get; private set; }
		public string SubHeader25DefaultValue { get; private set; }

		public string SubHeader1Placeholder { get; private set; }
		public string SubHeader3Placeholder { get; private set; }
		public string SubHeader4Placeholder { get; private set; }
		public string SubHeader6Placeholder { get; private set; }
		public string SubHeader7Placeholder { get; private set; }
		public string SubHeader9Placeholder { get; private set; }
		public string SubHeader10Placeholder { get; private set; }
		public string SubHeader12Placeholder { get; private set; }
		public string SubHeader13Placeholder { get; private set; }
		public string SubHeader14Placeholder { get; private set; }
		public string SubHeader15Placeholder { get; private set; }
		public string SubHeader16Placeholder { get; private set; }
		public string SubHeader18Placeholder { get; private set; }
		public string SubHeader19Placeholder { get; private set; }
		public string SubHeader20Placeholder { get; private set; }
		public string SubHeader21Placeholder { get; private set; }
		public string SubHeader22Placeholder { get; private set; }
		public string SubHeader23Placeholder { get; private set; }
		public string SubHeader25Placeholder { get; private set; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public ROITabBInfo()
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab6SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubBRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab6SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubBFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab6SubBBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab6SubBBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab6SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubB1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab6SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubB2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab6SubB3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab6SubB3File.LocalPath)
				: null;

			if (!resourceManager.DataROIPartBFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataROIPartBFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP06B");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP06BHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP06BSubHeader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
					case "CP06BSubHeader2":
						SubHeader2DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06BSubHeader3":
						if (item.IsPlaceholder)
							SubHeader3Placeholder = item.Value;
						else
							SubHeader3DefaultValue = item.Value;
						break;
					case "CP06BSubHeader4":
						if (item.IsPlaceholder)
							SubHeader4Placeholder = item.Value;
						else
							SubHeader4DefaultValue = item.Value;
						break;
					case "CP06BSubHeader5":
						SubHeader5DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06BSubHeader6":
						if (item.IsPlaceholder)
							SubHeader6Placeholder = item.Value;
						else
							SubHeader6DefaultValue = item.Value;
						break;
					case "CP06BSubHeader7":
						if (item.IsPlaceholder)
							SubHeader7Placeholder = item.Value;
						else
							SubHeader7DefaultValue = item.Value;
						break;
					case "CP06BSubHeader8":
						SubHeader8DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06BSubHeader9":
						if (item.IsPlaceholder)
							SubHeader9Placeholder = item.Value;
						else
							SubHeader9DefaultValue = item.Value;
						break;
					case "CP06BSubHeader10":
						if (item.IsPlaceholder)
							SubHeader10Placeholder = item.Value;
						else
							SubHeader10DefaultValue = item.Value;
						break;
					case "CP06BSubHeader11":
						SubHeader11DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06BSubHeader12":
						if (item.IsPlaceholder)
							SubHeader12Placeholder = item.Value;
						else
							SubHeader12DefaultValue = item.Value;
						break;
					case "CP06BSubHeader13":
						if (item.IsPlaceholder)
							SubHeader13Placeholder = item.Value;
						else
							SubHeader13DefaultValue = item.Value;
						break;
					case "CP06BSubHeader14":
						if (item.IsPlaceholder)
							SubHeader14Placeholder = item.Value;
						else
							SubHeader14DefaultValue = item.Value;
						break;
					case "CP06BSubHeader15":
						if (item.IsPlaceholder)
							SubHeader15Placeholder = item.Value;
						else
							SubHeader15DefaultValue = item.Value;
						break;
					case "CP06BSubHeader16":
						if (item.IsPlaceholder)
							SubHeader16Placeholder = item.Value;
						else
							SubHeader16DefaultValue = item.Value;
						break;
					case "CP06BSubHeader17":
						SubHeader17DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06BSubHeader18":
						if (item.IsPlaceholder)
							SubHeader18Placeholder = item.Value;
						else
							SubHeader18DefaultValue = item.Value;
						break;
					case "CP06BSubHeader19":
						if (item.IsPlaceholder)
							SubHeader19Placeholder = item.Value;
						else
							SubHeader19DefaultValue = item.Value;
						break;
					case "CP06BSubHeader20":
						if (item.IsPlaceholder)
							SubHeader20Placeholder = item.Value;
						else
							SubHeader20DefaultValue = item.Value;
						break;
					case "CP06BSubHeader21":
						if (item.IsPlaceholder)
							SubHeader21Placeholder = item.Value;
						else
							SubHeader21DefaultValue = item.Value;
						break;
					case "CP06BSubHeader22":
						if (item.IsPlaceholder)
							SubHeader22Placeholder = item.Value;
						else
							SubHeader22DefaultValue = item.Value;
						break;
					case "CP06BSubHeader23":
						if (item.IsPlaceholder)
							SubHeader23Placeholder = item.Value;
						else
							SubHeader23DefaultValue = item.Value;
						break;
					case "CP06BSubHeader24":
						SubHeader24DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06BSubHeader25":
						if (item.IsPlaceholder)
							SubHeader25Placeholder = item.Value;
						else
							SubHeader25DefaultValue = item.Value;
						break;
				}
			}
			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP06BClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP06BClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "CP06BClipart3");
		}
	}
}
