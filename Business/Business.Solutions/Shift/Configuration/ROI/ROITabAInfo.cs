using System;
using System.Drawing;
using System.Globalization;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.ROI
{
	public class ROITabAInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab13_A_Clipart1;
		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab13_A_Clipart2;
		public Image Clipart3Image => _resourceManager.GraphicResources?.Tab13_A_Clipart3;

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
		public string SubHeader11DefaultValue { get; private set; }
		public string SubHeader12DefaultValue { get; private set; }
		public string SubHeader13DefaultValue { get; private set; }
		public decimal? SubHeader14DefaultValue { get; private set; }
		public string SubHeader15DefaultValue { get; private set; }

		public string SubHeader1Placeholder { get; private set; }
		public string SubHeader3Placeholder { get; private set; }
		public string SubHeader4Placeholder { get; private set; }
		public string SubHeader6Placeholder { get; private set; }
		public string SubHeader7Placeholder { get; private set; }
		public string SubHeader9Placeholder { get; private set; }
		public string SubHeader10Placeholder { get; private set; }
		public string SubHeader11Placeholder { get; private set; }
		public string SubHeader12Placeholder { get; private set; }
		public string SubHeader13Placeholder { get; private set; }
		public string SubHeader15Placeholder { get; private set; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public ROITabAInfo() : base(ShiftChildTabType.A, ShiftTopTabType.ROI)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (!resourceManager.DataROIPartAFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataROIPartAFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP06A");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP06AHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP06ASubHeader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
					case "CP06ASubHeader2":
						SubHeader2DefaultValue = Decimal.Parse(item.Value ?? "0", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06ASubHeader3":
						if (item.IsPlaceholder)
							SubHeader3Placeholder = item.Value;
						else
							SubHeader3DefaultValue = item.Value;
						break;
					case "CP06ASubHeader4":
						if (item.IsPlaceholder)
							SubHeader4Placeholder = item.Value;
						else
							SubHeader4DefaultValue = item.Value;
						break;
					case "CP06ASubHeader5":
						SubHeader5DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06ASubHeader6":
						if (item.IsPlaceholder)
							SubHeader6Placeholder = item.Value;
						else
							SubHeader6DefaultValue = item.Value;
						break;
					case "CP06ASubHeader7":
						if (item.IsPlaceholder)
							SubHeader7Placeholder = item.Value;
						else
							SubHeader7DefaultValue = item.Value;
						break;
					case "CP06ASubHeader8":
						SubHeader8DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06ASubHeader9":
						if (item.IsPlaceholder)
							SubHeader9Placeholder = item.Value;
						else
							SubHeader9DefaultValue = item.Value;
						break;
					case "CP06ASubHeader10":
						if (item.IsPlaceholder)
							SubHeader10Placeholder = item.Value;
						else
							SubHeader10DefaultValue = item.Value;
						break;
					case "CP06ASubHeader11":
						if (item.IsPlaceholder)
							SubHeader11Placeholder = item.Value;
						else
							SubHeader11DefaultValue = item.Value;
						break;
					case "CP06ASubHeader12":
						if (item.IsPlaceholder)
							SubHeader12Placeholder = item.Value;
						else
							SubHeader12DefaultValue = item.Value;
						break;
					case "CP06ASubHeader13":
						if (item.IsPlaceholder)
							SubHeader13Placeholder = item.Value;
						else
							SubHeader13DefaultValue = item.Value;
						break;
					case "CP06ASubHeader14":
						SubHeader14DefaultValue = Decimal.Parse(item.Value ?? "0", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "CP06ASubHeader15":
						if (item.IsPlaceholder)
							SubHeader15Placeholder = item.Value;
						else
							SubHeader15DefaultValue = item.Value;
						break;
				}
			}
			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP06AClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP06AClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "CP06AClipart3");
		}
	}
}
