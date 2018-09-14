using System;
using System.Drawing;
using System.Globalization;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.ROI
{
	public class ROITabDInfo : StarTabWithHeaderInfo
	{
		public override StarChildTabType TabType => StarChildTabType.D;

		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab6_D_Clipart1;
		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab6_D_Clipart2;
		public Image Clipart3Image => _resourceManager.GraphicResources?.Tab6_D_Clipart3;

		public string SubHeader1DefaultValue { get; private set; }
		public decimal? SubHeader2DefaultValue { get; private set; }
		public string SubHeader3DefaultValue { get; private set; }
		public decimal? SubHeader4DefaultValue { get; private set; }
		public string SubHeader5DefaultValue { get; private set; }
		public decimal? SubHeader6DefaultValue { get; private set; }
		public string SubHeader7DefaultValue { get; private set; }
		public decimal? SubHeader8DefaultValue { get; private set; }
		public string SubHeader9DefaultValue { get; private set; }
		public decimal? SubHeader10DefaultValue { get; private set; }
		public string SubHeader11DefaultValue { get; private set; }
		public decimal? SubHeader12DefaultValue { get; private set; }
		public string SubHeader13DefaultValue { get; private set; }
		public string SubHeader14DefaultValue { get; private set; }
		public decimal? SubHeader15DefaultValue { get; private set; }
		public string SubHeader16DefaultValue { get; private set; }
		public string SubHeader17DefaultValue { get; private set; }

		public string SubHeader1Placeholder { get; private set; }
		public string SubHeader3Placeholder { get; private set; }
		public string SubHeader5Placeholder { get; private set; }
		public string SubHeader7Placeholder { get; private set; }
		public string SubHeader9Placeholder { get; private set; }
		public string SubHeader11Placeholder { get; private set; }
		public string SubHeader13Placeholder { get; private set; }
		public string SubHeader14Placeholder { get; private set; }
		public string SubHeader16Placeholder { get; private set; }
		public string SubHeader17Placeholder { get; private set; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public ROITabDInfo() : base(StarTopTabType.ROI)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (!resourceManager.DataROIPartDFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataROIPartDFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP06D");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP06DHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP06DSubHeader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
					case "CP06DSubHeader2":
						SubHeader2DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06DSubHeader3":
						if (item.IsPlaceholder)
							SubHeader3Placeholder = item.Value;
						else
							SubHeader3DefaultValue = item.Value;
						break;
					case "CP06DSubHeader4":
						SubHeader4DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06DSubHeader5":
						if (item.IsPlaceholder)
							SubHeader5Placeholder = item.Value;
						else
							SubHeader5DefaultValue = item.Value;
						break;
					case "CP06DSubHeader6":
						SubHeader6DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06DSubHeader7":
						if (item.IsPlaceholder)
							SubHeader7Placeholder = item.Value;
						else
							SubHeader7DefaultValue = item.Value;
						break;
					case "CP06DSubHeader8":
						SubHeader8DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06DSubHeader9":
						if (item.IsPlaceholder)
							SubHeader9Placeholder = item.Value;
						else
							SubHeader9DefaultValue = item.Value;
						break;
					case "CP06DSubHeader10":
						SubHeader10DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06DSubHeader11":
						if (item.IsPlaceholder)
							SubHeader11Placeholder = item.Value;
						else
							SubHeader11DefaultValue = item.Value;
						break;
					case "CP06DSubHeader12":
						SubHeader12DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06DSubHeader13":
						if (item.IsPlaceholder)
							SubHeader13Placeholder = item.Value;
						else
							SubHeader13DefaultValue = item.Value;
						break;
					case "CP06DSubHeader14":
						if (item.IsPlaceholder)
							SubHeader14Placeholder = item.Value;
						else
							SubHeader14DefaultValue = item.Value;
						break;
					case "CP06DSubHeader15":
						SubHeader15DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06DSubHeader16":
						if (item.IsPlaceholder)
							SubHeader16Placeholder = item.Value;
						else
							SubHeader16DefaultValue = item.Value;
						break;
					case "CP06DSubHeader17":
						if (item.IsPlaceholder)
							SubHeader17Placeholder = item.Value;
						else
							SubHeader17DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP06DClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP06DClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "CP06DClipart3");
		}
	}
}
