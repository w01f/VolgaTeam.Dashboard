using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.Core.Helpers;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class ROIConfiguration
	{
		public List<ListDataItem> HeadersPartAItems { get; set; }
		public string PartASubHeader1DefaultValue { get; private set; }
		public decimal? PartASubHeader2DefaultValue { get; private set; }
		public string PartASubHeader3DefaultValue { get; private set; }
		public string PartASubHeader4DefaultValue { get; private set; }
		public decimal? PartASubHeader5DefaultValue { get; private set; }
		public string PartASubHeader6DefaultValue { get; private set; }
		public string PartASubHeader7DefaultValue { get; private set; }
		public decimal? PartASubHeader8DefaultValue { get; private set; }
		public string PartASubHeader9DefaultValue { get; private set; }
		public string PartASubHeader10DefaultValue { get; private set; }
		public string PartASubHeader11DefaultValue { get; private set; }
		public string PartASubHeader12DefaultValue { get; private set; }
		public string PartASubHeader13DefaultValue { get; private set; }
		public decimal? PartASubHeader14DefaultValue { get; private set; }
		public string PartASubHeader15DefaultValue { get; private set; }

		public string PartASubHeader1Placeholder { get; private set; }
		public string PartASubHeader3Placeholder { get; private set; }
		public string PartASubHeader4Placeholder { get; private set; }
		public string PartASubHeader6Placeholder { get; private set; }
		public string PartASubHeader7Placeholder { get; private set; }
		public string PartASubHeader9Placeholder { get; private set; }
		public string PartASubHeader10Placeholder { get; private set; }
		public string PartASubHeader11Placeholder { get; private set; }
		public string PartASubHeader12Placeholder { get; private set; }
		public string PartASubHeader13Placeholder { get; private set; }
		public string PartASubHeader15Placeholder { get; private set; }

		public ClipartConfiguration PartAClipart1Configuration { get; private set; }
		public ClipartConfiguration PartAClipart2Configuration { get; private set; }
		public ClipartConfiguration PartAClipart3Configuration { get; private set; }

		public List<ListDataItem> HeadersPartBItems { get; set; }
		public string PartBSubHeader1DefaultValue { get; private set; }
		public decimal? PartBSubHeader2DefaultValue { get; private set; }
		public string PartBSubHeader3DefaultValue { get; private set; }
		public string PartBSubHeader4DefaultValue { get; private set; }
		public decimal? PartBSubHeader5DefaultValue { get; private set; }
		public string PartBSubHeader6DefaultValue { get; private set; }
		public string PartBSubHeader7DefaultValue { get; private set; }
		public decimal? PartBSubHeader8DefaultValue { get; private set; }
		public string PartBSubHeader9DefaultValue { get; private set; }
		public string PartBSubHeader10DefaultValue { get; private set; }
		public decimal? PartBSubHeader11DefaultValue { get; private set; }
		public string PartBSubHeader12DefaultValue { get; private set; }
		public string PartBSubHeader13DefaultValue { get; private set; }
		public string PartBSubHeader14DefaultValue { get; private set; }
		public string PartBSubHeader15DefaultValue { get; private set; }
		public string PartBSubHeader16DefaultValue { get; private set; }
		public decimal? PartBSubHeader17DefaultValue { get; private set; }
		public string PartBSubHeader18DefaultValue { get; private set; }
		public string PartBSubHeader19DefaultValue { get; private set; }
		public string PartBSubHeader20DefaultValue { get; private set; }
		public string PartBSubHeader21DefaultValue { get; private set; }
		public string PartBSubHeader22DefaultValue { get; private set; }
		public string PartBSubHeader23DefaultValue { get; private set; }
		public decimal? PartBSubHeader24DefaultValue { get; private set; }
		public string PartBSubHeader25DefaultValue { get; private set; }

		public string PartBSubHeader1Placeholder { get; private set; }
		public string PartBSubHeader3Placeholder { get; private set; }
		public string PartBSubHeader4Placeholder { get; private set; }
		public string PartBSubHeader6Placeholder { get; private set; }
		public string PartBSubHeader7Placeholder { get; private set; }
		public string PartBSubHeader9Placeholder { get; private set; }
		public string PartBSubHeader10Placeholder { get; private set; }
		public string PartBSubHeader12Placeholder { get; private set; }
		public string PartBSubHeader13Placeholder { get; private set; }
		public string PartBSubHeader14Placeholder { get; private set; }
		public string PartBSubHeader15Placeholder { get; private set; }
		public string PartBSubHeader16Placeholder { get; private set; }
		public string PartBSubHeader18Placeholder { get; private set; }
		public string PartBSubHeader19Placeholder { get; private set; }
		public string PartBSubHeader20Placeholder { get; private set; }
		public string PartBSubHeader21Placeholder { get; private set; }
		public string PartBSubHeader22Placeholder { get; private set; }
		public string PartBSubHeader23Placeholder { get; private set; }
		public string PartBSubHeader25Placeholder { get; private set; }

		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }
		public ClipartConfiguration PartBClipart3Configuration { get; private set; }

		public List<ListDataItem> HeadersPartCItems { get; set; }
		public string PartCSubHeader1DefaultValue { get; private set; }
		public decimal? PartCSubHeader2DefaultValue { get; private set; }
		public string PartCSubHeader3DefaultValue { get; private set; }
		public decimal? PartCSubHeader4DefaultValue { get; private set; }
		public string PartCSubHeader5DefaultValue { get; private set; }
		public string PartCSubHeader6DefaultValue { get; private set; }
		public decimal? PartCSubHeader7DefaultValue { get; private set; }
		public string PartCSubHeader8DefaultValue { get; private set; }
		public string PartCSubHeader9DefaultValue { get; private set; }
		public string PartCSubHeader10DefaultValue { get; private set; }
		public string PartCSubHeader11DefaultValue { get; private set; }
		public string PartCSubHeader12DefaultValue { get; private set; }
		public decimal? PartCSubHeader13DefaultValue { get; private set; }
		public string PartCSubHeader14DefaultValue { get; private set; }
		public string PartCSubHeader15DefaultValue { get; private set; }

		public string PartCSubHeader1Placeholder { get; private set; }
		public string PartCSubHeader3Placeholder { get; private set; }
		public string PartCSubHeader5Placeholder { get; private set; }
		public string PartCSubHeader6Placeholder { get; private set; }
		public string PartCSubHeader8Placeholder { get; private set; }
		public string PartCSubHeader9Placeholder { get; private set; }
		public string PartCSubHeader10Placeholder { get; private set; }
		public string PartCSubHeader11Placeholder { get; private set; }
		public string PartCSubHeader12Placeholder { get; private set; }
		public string PartCSubHeader14Placeholder { get; private set; }
		public string PartCSubHeader15Placeholder { get; private set; }

		public ClipartConfiguration PartCClipart1Configuration { get; private set; }
		public ClipartConfiguration PartCClipart2Configuration { get; private set; }
		public ClipartConfiguration PartCClipart3Configuration { get; private set; }

		public List<ListDataItem> HeadersPartDItems { get; set; }
		public string PartDSubHeader1DefaultValue { get; private set; }
		public decimal? PartDSubHeader2DefaultValue { get; private set; }
		public string PartDSubHeader3DefaultValue { get; private set; }
		public decimal? PartDSubHeader4DefaultValue { get; private set; }
		public string PartDSubHeader5DefaultValue { get; private set; }
		public decimal? PartDSubHeader6DefaultValue { get; private set; }
		public string PartDSubHeader7DefaultValue { get; private set; }
		public decimal? PartDSubHeader8DefaultValue { get; private set; }
		public string PartDSubHeader9DefaultValue { get; private set; }
		public decimal? PartDSubHeader10DefaultValue { get; private set; }
		public string PartDSubHeader11DefaultValue { get; private set; }
		public decimal? PartDSubHeader12DefaultValue { get; private set; }
		public string PartDSubHeader13DefaultValue { get; private set; }
		public string PartDSubHeader14DefaultValue { get; private set; }
		public decimal? PartDSubHeader15DefaultValue { get; private set; }
		public string PartDSubHeader16DefaultValue { get; private set; }
		public string PartDSubHeader17DefaultValue { get; private set; }

		public string PartDSubHeader1Placeholder { get; private set; }
		public string PartDSubHeader3Placeholder { get; private set; }
		public string PartDSubHeader5Placeholder { get; private set; }
		public string PartDSubHeader7Placeholder { get; private set; }
		public string PartDSubHeader9Placeholder { get; private set; }
		public string PartDSubHeader11Placeholder { get; private set; }
		public string PartDSubHeader13Placeholder { get; private set; }
		public string PartDSubHeader14Placeholder { get; private set; }
		public string PartDSubHeader16Placeholder { get; private set; }
		public string PartDSubHeader17Placeholder { get; private set; }

		public ClipartConfiguration PartDClipart1Configuration { get; private set; }
		public ClipartConfiguration PartDClipart2Configuration { get; private set; }
		public ClipartConfiguration PartDClipart3Configuration { get; private set; }

		public SlideManager PartUSlides { get; }
		public SlideManager PartVSlides { get; }
		public SlideManager PartWSlides { get; }

		public ROIConfiguration()
		{
			HeadersPartAItems = new List<ListDataItem>();
			PartAClipart1Configuration = new ClipartConfiguration();
			PartAClipart2Configuration = new ClipartConfiguration();
			PartAClipart3Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<ListDataItem>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();
			PartBClipart3Configuration = new ClipartConfiguration();

			HeadersPartCItems = new List<ListDataItem>();
			PartCClipart1Configuration = new ClipartConfiguration();
			PartCClipart2Configuration = new ClipartConfiguration();
			PartCClipart3Configuration = new ClipartConfiguration();

			HeadersPartDItems = new List<ListDataItem>();
			PartDClipart1Configuration = new ClipartConfiguration();
			PartDClipart2Configuration = new ClipartConfiguration();
			PartDClipart3Configuration = new ClipartConfiguration();

			PartUSlides = new SlideManager();
			PartVSlides = new SlideManager();
			PartWSlides = new SlideManager();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataROIPartAFile.ExistsLocal())
			{
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
								HeadersPartAItems.Add(item);
							break;
						case "CP06ASubHeader1":
							if (item.IsPlaceholder)
								PartASubHeader1Placeholder = item.Value;
							else
								PartASubHeader1DefaultValue = item.Value;
							break;
						case "CP06ASubHeader2":
							PartASubHeader2DefaultValue = Decimal.Parse(item.Value ?? "0", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06ASubHeader3":
							if (item.IsPlaceholder)
								PartASubHeader3Placeholder = item.Value;
							else
								PartASubHeader3DefaultValue = item.Value;
							break;
						case "CP06ASubHeader4":
							if (item.IsPlaceholder)
								PartASubHeader4Placeholder = item.Value;
							else
								PartASubHeader4DefaultValue = item.Value;
							break;
						case "CP06ASubHeader5":
							PartASubHeader5DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06ASubHeader6":
							if (item.IsPlaceholder)
								PartASubHeader6Placeholder = item.Value;
							else
								PartASubHeader6DefaultValue = item.Value;
							break;
						case "CP06ASubHeader7":
							if (item.IsPlaceholder)
								PartASubHeader7Placeholder = item.Value;
							else
								PartASubHeader7DefaultValue = item.Value;
							break;
						case "CP06ASubHeader8":
							PartASubHeader8DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06ASubHeader9":
							if (item.IsPlaceholder)
								PartASubHeader9Placeholder = item.Value;
							else
								PartASubHeader9DefaultValue = item.Value;
							break;
						case "CP06ASubHeader10":
							if (item.IsPlaceholder)
								PartASubHeader10Placeholder = item.Value;
							else
								PartASubHeader10DefaultValue = item.Value;
							break;
						case "CP06ASubHeader11":
							if (item.IsPlaceholder)
								PartASubHeader11Placeholder = item.Value;
							else
								PartASubHeader11DefaultValue = item.Value;
							break;
						case "CP06ASubHeader12":
							if (item.IsPlaceholder)
								PartASubHeader12Placeholder = item.Value;
							else
								PartASubHeader12DefaultValue = item.Value;
							break;
						case "CP06ASubHeader13":
							if (item.IsPlaceholder)
								PartASubHeader13Placeholder = item.Value;
							else
								PartASubHeader13DefaultValue = item.Value;
							break;
						case "CP06ASubHeader14":
							PartASubHeader14DefaultValue = Decimal.Parse(item.Value ?? "0", NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06ASubHeader15":
							if (item.IsPlaceholder)
								PartASubHeader15Placeholder = item.Value;
							else
								PartASubHeader15DefaultValue = item.Value;
							break;
					}
				}
				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP06AClipart1");
				PartAClipart2Configuration = ClipartConfiguration.FromXml(node, "CP06AClipart2");
				PartAClipart3Configuration = ClipartConfiguration.FromXml(node, "CP06AClipart3");
			}

			if (resourceManager.DataROIPartBFile.ExistsLocal())
			{
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
								HeadersPartBItems.Add(item);
							break;
						case "CP06BSubHeader1":
							if (item.IsPlaceholder)
								PartBSubHeader1Placeholder = item.Value;
							else
								PartBSubHeader1DefaultValue = item.Value;
							break;
						case "CP06BSubHeader2":
							PartBSubHeader2DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06BSubHeader3":
							if (item.IsPlaceholder)
								PartBSubHeader3Placeholder = item.Value;
							else
								PartBSubHeader3DefaultValue = item.Value;
							break;
						case "CP06BSubHeader4":
							if (item.IsPlaceholder)
								PartBSubHeader4Placeholder = item.Value;
							else
								PartBSubHeader4DefaultValue = item.Value;
							break;
						case "CP06BSubHeader5":
							PartBSubHeader5DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06BSubHeader6":
							if (item.IsPlaceholder)
								PartBSubHeader6Placeholder = item.Value;
							else
								PartBSubHeader6DefaultValue = item.Value;
							break;
						case "CP06BSubHeader7":
							if (item.IsPlaceholder)
								PartBSubHeader7Placeholder = item.Value;
							else
								PartBSubHeader7DefaultValue = item.Value;
							break;
						case "CP06BSubHeader8":
							PartBSubHeader8DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06BSubHeader9":
							if (item.IsPlaceholder)
								PartBSubHeader9Placeholder = item.Value;
							else
								PartBSubHeader9DefaultValue = item.Value;
							break;
						case "CP06BSubHeader10":
							if (item.IsPlaceholder)
								PartBSubHeader10Placeholder = item.Value;
							else
								PartBSubHeader10DefaultValue = item.Value;
							break;
						case "CP06BSubHeader11":
							PartBSubHeader11DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06BSubHeader12":
							if (item.IsPlaceholder)
								PartBSubHeader12Placeholder = item.Value;
							else
								PartBSubHeader12DefaultValue = item.Value;
							break;
						case "CP06BSubHeader13":
							if (item.IsPlaceholder)
								PartBSubHeader13Placeholder = item.Value;
							else
								PartBSubHeader13DefaultValue = item.Value;
							break;
						case "CP06BSubHeader14":
							if (item.IsPlaceholder)
								PartBSubHeader14Placeholder = item.Value;
							else
								PartBSubHeader14DefaultValue = item.Value;
							break;
						case "CP06BSubHeader15":
							if (item.IsPlaceholder)
								PartBSubHeader15Placeholder = item.Value;
							else
								PartBSubHeader15DefaultValue = item.Value;
							break;
						case "CP06BSubHeader16":
							if (item.IsPlaceholder)
								PartBSubHeader16Placeholder = item.Value;
							else
								PartBSubHeader16DefaultValue = item.Value;
							break;
						case "CP06BSubHeader17":
							PartBSubHeader17DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06BSubHeader18":
							if (item.IsPlaceholder)
								PartBSubHeader18Placeholder = item.Value;
							else
								PartBSubHeader18DefaultValue = item.Value;
							break;
						case "CP06BSubHeader19":
							if (item.IsPlaceholder)
								PartBSubHeader19Placeholder = item.Value;
							else
								PartBSubHeader19DefaultValue = item.Value;
							break;
						case "CP06BSubHeader20":
							if (item.IsPlaceholder)
								PartBSubHeader20Placeholder = item.Value;
							else
								PartBSubHeader20DefaultValue = item.Value;
							break;
						case "CP06BSubHeader21":
							if (item.IsPlaceholder)
								PartBSubHeader21Placeholder = item.Value;
							else
								PartBSubHeader21DefaultValue = item.Value;
							break;
						case "CP06BSubHeader22":
							if (item.IsPlaceholder)
								PartBSubHeader22Placeholder = item.Value;
							else
								PartBSubHeader22DefaultValue = item.Value;
							break;
						case "CP06BSubHeader23":
							if (item.IsPlaceholder)
								PartBSubHeader23Placeholder = item.Value;
							else
								PartBSubHeader23DefaultValue = item.Value;
							break;
						case "CP06BSubHeader24":
							PartBSubHeader24DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06BSubHeader25":
							if (item.IsPlaceholder)
								PartBSubHeader25Placeholder = item.Value;
							else
								PartBSubHeader25DefaultValue = item.Value;
							break;
					}
				}
				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP06BClipart1");
				PartBClipart2Configuration = ClipartConfiguration.FromXml(node, "CP06BClipart2");
				PartBClipart3Configuration = ClipartConfiguration.FromXml(node, "CP06BClipart3");
			}

			if (resourceManager.DataROIPartCFile.ExistsLocal())
			{
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
								HeadersPartCItems.Add(item);
							break;
						case "CP06CSubHeader1":
							if (item.IsPlaceholder)
								PartCSubHeader1Placeholder = item.Value;
							else
								PartCSubHeader1DefaultValue = item.Value;
							break;
						case "CP06CSubHeader2":
							PartCSubHeader2DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06CSubHeader3":
							if (item.IsPlaceholder)
								PartCSubHeader3Placeholder = item.Value;
							else
								PartCSubHeader3DefaultValue = item.Value;
							break;
						case "CP06CSubHeader4":
							PartCSubHeader4DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06CSubHeader5":
							if (item.IsPlaceholder)
								PartCSubHeader5Placeholder = item.Value;
							else
								PartCSubHeader5DefaultValue = item.Value;
							break;
						case "CP06CSubHeader6":
							if (item.IsPlaceholder)
								PartCSubHeader6Placeholder = item.Value;
							else
								PartCSubHeader6DefaultValue = item.Value;
							break;
						case "CP06CSubHeader7":
							PartCSubHeader7DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06CSubHeader8":
							if (item.IsPlaceholder)
								PartCSubHeader8Placeholder = item.Value;
							else
								PartCSubHeader8DefaultValue = item.Value;
							break;
						case "CP06CSubHeader9":
							if (item.IsPlaceholder)
								PartCSubHeader9Placeholder = item.Value;
							else
								PartCSubHeader9DefaultValue = item.Value;
							break;
						case "CP06CSubHeader10":
							if (item.IsPlaceholder)
								PartCSubHeader10Placeholder = item.Value;
							else
								PartCSubHeader10DefaultValue = item.Value;
							break;
						case "CP06CSubHeader11":
							if (item.IsPlaceholder)
								PartCSubHeader11Placeholder = item.Value;
							else
								PartCSubHeader11DefaultValue = item.Value;
							break;
						case "CP06CSubHeader12":
							if (item.IsPlaceholder)
								PartCSubHeader12Placeholder = item.Value;
							else
								PartCSubHeader12DefaultValue = item.Value;
							break;
						case "CP06CSubHeader13":
							PartCSubHeader13DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06CSubHeader14":
							if (item.IsPlaceholder)
								PartCSubHeader14Placeholder = item.Value;
							else
								PartCSubHeader14DefaultValue = item.Value;
							break;
						case "CP06CSubHeader15":
							if (item.IsPlaceholder)
								PartCSubHeader15Placeholder = item.Value;
							else
								PartCSubHeader15DefaultValue = item.Value;
							break;
					}
				}
				PartCClipart1Configuration = ClipartConfiguration.FromXml(node, "CP06CClipart1");
				PartCClipart2Configuration = ClipartConfiguration.FromXml(node, "CP06CClipart2");
				PartCClipart3Configuration = ClipartConfiguration.FromXml(node, "CP06CClipart3");
			}

			if (resourceManager.DataROIPartDFile.ExistsLocal())
			{
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
								HeadersPartDItems.Add(item);
							break;
						case "CP06DSubHeader1":
							if (item.IsPlaceholder)
								PartDSubHeader1Placeholder = item.Value;
							else
								PartDSubHeader1DefaultValue = item.Value;
							break;
						case "CP06DSubHeader2":
							PartDSubHeader2DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06DSubHeader3":
							if (item.IsPlaceholder)
								PartDSubHeader3Placeholder = item.Value;
							else
								PartDSubHeader3DefaultValue = item.Value;
							break;
						case "CP06DSubHeader4":
							PartDSubHeader4DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06DSubHeader5":
							if (item.IsPlaceholder)
								PartDSubHeader5Placeholder = item.Value;
							else
								PartDSubHeader5DefaultValue = item.Value;
							break;
						case "CP06DSubHeader6":
							PartDSubHeader6DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06DSubHeader7":
							if (item.IsPlaceholder)
								PartDSubHeader7Placeholder = item.Value;
							else
								PartDSubHeader7DefaultValue = item.Value;
							break;
						case "CP06DSubHeader8":
							PartDSubHeader8DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06DSubHeader9":
							if (item.IsPlaceholder)
								PartDSubHeader9Placeholder = item.Value;
							else
								PartDSubHeader9DefaultValue = item.Value;
							break;
						case "CP06DSubHeader10":
							PartDSubHeader10DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06DSubHeader11":
							if (item.IsPlaceholder)
								PartDSubHeader11Placeholder = item.Value;
							else
								PartDSubHeader11DefaultValue = item.Value;
							break;
						case "CP06DSubHeader12":
							PartDSubHeader12DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06DSubHeader13":
							if (item.IsPlaceholder)
								PartDSubHeader13Placeholder = item.Value;
							else
								PartDSubHeader13DefaultValue = item.Value;
							break;
						case "CP06DSubHeader14":
							if (item.IsPlaceholder)
								PartDSubHeader14Placeholder = item.Value;
							else
								PartDSubHeader14DefaultValue = item.Value;
							break;
						case "CP06DSubHeader15":
							PartDSubHeader15DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP06DSubHeader16":
							if (item.IsPlaceholder)
								PartDSubHeader16Placeholder = item.Value;
							else
								PartDSubHeader16DefaultValue = item.Value;
							break;
						case "CP06DSubHeader17":
							if (item.IsPlaceholder)
								PartDSubHeader17Placeholder = item.Value;
							else
								PartDSubHeader17DefaultValue = item.Value;
							break;
					}
				}

				PartDClipart1Configuration = ClipartConfiguration.FromXml(node, "CP06DClipart1");
				PartDClipart2Configuration = ClipartConfiguration.FromXml(node, "CP06DClipart2");
				PartDClipart3Configuration = ClipartConfiguration.FromXml(node, "CP06DClipart3");
			}

			if (resourceManager.Tab6PartUSlidesFolder.ExistsLocal())
			{
				PartUSlides.LoadSlides(resourceManager.Tab6PartUSlidesFolder);
			}

			if (resourceManager.Tab6PartVSlidesFolder.ExistsLocal())
			{
				PartVSlides.LoadSlides(resourceManager.Tab6PartVSlidesFolder);
			}

			if (resourceManager.Tab6PartWSlidesFolder.ExistsLocal())
			{
				PartWSlides.LoadSlides(resourceManager.Tab6PartWSlidesFolder);
			}
		}
	}
}
