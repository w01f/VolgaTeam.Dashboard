using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.Core.Helpers;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class ShareConfiguration
	{
		public List<ListDataItem> HeadersPartAItems { get; set; }
		public List<ListDataItem> PartACombo1Items { get; }
		public List<ListDataItem> PartACombo2Items { get; }
		public List<ListDataItem> PartACombo3Items { get; }
		public List<ListDataItem> PartACombo4Items { get; }
		public string PartASubHeader1DefaultValue { get; private set; }
		public decimal? PartASubHeader2DefaultValue { get; private set; }
		public string PartASubHeader3DefaultValue { get; private set; }
		public string PartASubHeader4DefaultValue { get; private set; }
		public string PartASubHeader5DefaultValue { get; private set; }
		public string PartASubHeader6DefaultValue { get; private set; }
		public string PartASubHeader7DefaultValue { get; private set; }

		public string PartASubHeader1Placeholder { get; private set; }
		public string PartASubHeader3Placeholder { get; private set; }
		public string PartASubHeader4Placeholder { get; private set; }
		public string PartASubHeader5Placeholder { get; private set; }
		public string PartASubHeader6Placeholder { get; private set; }
		public string PartASubHeader7Placeholder { get; private set; }

		public ClipartConfiguration PartAClipart1Configuration { get; private set; }
		public ClipartConfiguration PartAClipart2Configuration { get; private set; }
		public ClipartConfiguration PartAClipart3Configuration { get; private set; }

		public List<ListDataItem> HeadersPartBItems { get; set; }
		public List<ListDataItem> PartBCombo1Items { get; }
		public List<ListDataItem> PartBCombo2Items { get; }
		public string PartBSubHeader1DefaultValue { get; private set; }
		public string PartBSubHeader2DefaultValue { get; private set; }
		public string PartBSubHeader3DefaultValue { get; private set; }
		public decimal? PartBSubHeader4DefaultValue { get; private set; }
		public string PartBSubHeader5DefaultValue { get; private set; }
		public decimal? PartBSubHeader6DefaultValue { get; private set; }
		public string PartBSubHeader7DefaultValue { get; private set; }
		public string PartBSubHeader8DefaultValue { get; private set; }

		public string PartBSubHeader1Placeholder { get; private set; }
		public string PartBSubHeader2Placeholder { get; private set; }
		public string PartBSubHeader3Placeholder { get; private set; }
		public string PartBSubHeader5Placeholder { get; private set; }
		public string PartBSubHeader7Placeholder { get; private set; }
		public string PartBSubHeader8Placeholder { get; private set; }

		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }
		public ClipartConfiguration PartBClipart3Configuration { get; private set; }

		public List<ListDataItem> HeadersPartCItems { get; set; }
		public List<ListDataItem> PartCCombo1Items { get; }
		public List<ListDataItem> PartCCombo2Items { get; }
		public List<ListDataItem> PartCCombo3Items { get; }
		public List<ListDataItem> PartCCombo4Items { get; }
		public List<ListDataItem> PartCCombo5Items { get; }
		public List<ListDataItem> PartCCombo6Items { get; }
		public decimal? PartCSubHeader1DefaultValue { get; private set; }
		public string PartCSubHeader2DefaultValue { get; private set; }
		public string PartCSubHeader3DefaultValue { get; private set; }
		public string PartCSubHeader4DefaultValue { get; private set; }

		public string PartCSubHeader2Placeholder { get; private set; }
		public string PartCSubHeader3Placeholder { get; private set; }
		public string PartCSubHeader4Placeholder { get; private set; }

		public ClipartConfiguration PartCClipart1Configuration { get; private set; }
		public ClipartConfiguration PartCClipart2Configuration { get; private set; }
		public ClipartConfiguration PartCClipart3Configuration { get; private set; }

		public List<ListDataItem> HeadersPartDItems { get; set; }
		public List<ListDataItem> PartDCombo1Items { get; }
		public List<ListDataItem> PartDCombo2Items { get; }
		public List<ListDataItem> PartDCombo3Items { get; }
		public string PartDSubHeader1DefaultValue { get; private set; }
		public string PartDSubHeader2DefaultValue { get; private set; }
		public string PartDSubHeader3DefaultValue { get; private set; }
		public decimal? PartDSubHeader4DefaultValue { get; private set; }
		public string PartDSubHeader5DefaultValue { get; private set; }
		public string PartDSubHeader6DefaultValue { get; private set; }
		public string PartDSubHeader7DefaultValue { get; private set; }
		public string PartDSubHeader8DefaultValue { get; private set; }
		public string PartDSubHeader9DefaultValue { get; private set; }

		public string PartDSubHeader1Placeholder { get; private set; }
		public string PartDSubHeader2Placeholder { get; private set; }
		public string PartDSubHeader3Placeholder { get; private set; }
		public string PartDSubHeader5Placeholder { get; private set; }
		public string PartDSubHeader6Placeholder { get; private set; }
		public string PartDSubHeader7Placeholder { get; private set; }
		public string PartDSubHeader8Placeholder { get; private set; }
		public string PartDSubHeader9Placeholder { get; private set; }

		public ClipartConfiguration PartDClipart1Configuration { get; private set; }
		public ClipartConfiguration PartDClipart2Configuration { get; private set; }
		public ClipartConfiguration PartDClipart3Configuration { get; private set; }

		public List<ListDataItem> HeadersPartEItems { get; set; }
		public List<ListDataItem> PartECombo1Items { get; }
		public List<ListDataItem> PartECombo2Items { get; }
		public List<ListDataItem> PartECombo3Items { get; }
		public List<ListDataItem> PartECombo4Items { get; }
		public string PartESubHeader1DefaultValue { get; private set; }
		public string PartESubHeader2DefaultValue { get; private set; }
		public string PartESubHeader3DefaultValue { get; private set; }
		public string PartESubHeader4DefaultValue { get; private set; }
		public string PartESubHeader5DefaultValue { get; private set; }
		public string PartESubHeader6DefaultValue { get; private set; }
		public decimal? PartESubHeader7DefaultValue { get; private set; }
		public string PartESubHeader8DefaultValue { get; private set; }
		public string PartESubHeader9DefaultValue { get; private set; }
		public string PartESubHeader10DefaultValue { get; private set; }

		public string PartESubHeader1Placeholder { get; private set; }
		public string PartESubHeader2Placeholder { get; private set; }
		public string PartESubHeader3Placeholder { get; private set; }
		public string PartESubHeader4Placeholder { get; private set; }
		public string PartESubHeader5Placeholder { get; private set; }
		public string PartESubHeader6Placeholder { get; private set; }
		public string PartESubHeader8Placeholder { get; private set; }
		public string PartESubHeader9Placeholder { get; private set; }
		public string PartESubHeader10Placeholder { get; private set; }

		public ClipartConfiguration PartEClipart1Configuration { get; private set; }
		public ClipartConfiguration PartEClipart2Configuration { get; private set; }
		public ClipartConfiguration PartEClipart3Configuration { get; private set; }

		public SlideManager PartUSlides { get; }
		public SlideManager PartVSlides { get; }
		public SlideManager PartWSlides { get; }

		public ShareConfiguration()
		{
			HeadersPartAItems = new List<ListDataItem>();
			PartACombo1Items = new List<ListDataItem>();
			PartACombo2Items = new List<ListDataItem>();
			PartACombo3Items = new List<ListDataItem>();
			PartACombo4Items = new List<ListDataItem>();
			PartAClipart1Configuration = new ClipartConfiguration();
			PartAClipart2Configuration = new ClipartConfiguration();
			PartAClipart3Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<ListDataItem>();
			PartBCombo1Items = new List<ListDataItem>();
			PartBCombo2Items = new List<ListDataItem>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();
			PartBClipart3Configuration = new ClipartConfiguration();

			HeadersPartCItems = new List<ListDataItem>();
			PartCCombo1Items = new List<ListDataItem>();
			PartCCombo2Items = new List<ListDataItem>();
			PartCCombo3Items = new List<ListDataItem>();
			PartCCombo4Items = new List<ListDataItem>();
			PartCCombo5Items = new List<ListDataItem>();
			PartCCombo6Items = new List<ListDataItem>();
			PartCClipart1Configuration = new ClipartConfiguration();
			PartCClipart2Configuration = new ClipartConfiguration();
			PartCClipart3Configuration = new ClipartConfiguration();

			HeadersPartDItems = new List<ListDataItem>();
			PartDCombo1Items = new List<ListDataItem>();
			PartDCombo2Items = new List<ListDataItem>();
			PartDCombo3Items = new List<ListDataItem>();
			PartDClipart1Configuration = new ClipartConfiguration();
			PartDClipart2Configuration = new ClipartConfiguration();
			PartDClipart3Configuration = new ClipartConfiguration();

			HeadersPartEItems = new List<ListDataItem>();
			PartECombo1Items = new List<ListDataItem>();
			PartECombo2Items = new List<ListDataItem>();
			PartECombo3Items = new List<ListDataItem>();
			PartECombo4Items = new List<ListDataItem>();
			PartEClipart1Configuration = new ClipartConfiguration();
			PartEClipart2Configuration = new ClipartConfiguration();
			PartEClipart3Configuration = new ClipartConfiguration();

			PartUSlides = new SlideManager();
			PartVSlides = new SlideManager();
			PartWSlides = new SlideManager();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataSharePartAFile.ExistsLocal())
			{
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
								HeadersPartAItems.Add(item);
							break;
						case "CP05ABillionCombo1":
							if (!String.IsNullOrEmpty(item.Value))
								PartACombo1Items.Add(item);
							break;
						case "CP05APercentCombo2":
							if (!String.IsNullOrEmpty(item.Value))
								PartACombo2Items.Add(item);
							break;
						case "CP05APopulationCombo3":
							if (!String.IsNullOrEmpty(item.Value))
								PartACombo3Items.Add(item);
							break;
						case "CP05ASharePointCombo4":
							if (!String.IsNullOrEmpty(item.Value))
								PartACombo4Items.Add(item);
							break;
						case "CP05ASubHeader1":
							if (item.IsPlaceholder)
								PartASubHeader1Placeholder = item.Value;
							else
								PartASubHeader1DefaultValue = item.Value;
							break;
						case "CP05ASubHeader2":
							PartASubHeader2DefaultValue = Decimal.Parse(item.Value ?? "0",
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP05ASubHeader3":
							if (item.IsPlaceholder)
								PartASubHeader3Placeholder = item.Value;
							else
								PartASubHeader3DefaultValue = item.Value;
							break;
						case "CP05ASubHeader4":
							if (item.IsPlaceholder)
								PartASubHeader4Placeholder = item.Value;
							else
								PartASubHeader4DefaultValue = item.Value;
							break;
						case "CP05ASubHeader5":
							if (item.IsPlaceholder)
								PartASubHeader5Placeholder = item.Value;
							else
								PartASubHeader5DefaultValue = item.Value;
							break;
						case "CP05ASubHeader6":
							if (item.IsPlaceholder)
								PartASubHeader6Placeholder = item.Value;
							else
								PartASubHeader6DefaultValue = item.Value;
							break;
						case "CP05ASubHeader7":
							if (item.IsPlaceholder)
								PartASubHeader7Placeholder = item.Value;
							else
								PartASubHeader7DefaultValue = item.Value;
							break;
					}
				}

				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP05AClipart1");
				PartAClipart2Configuration = ClipartConfiguration.FromXml(node, "CP05AClipart2");
				PartAClipart3Configuration = ClipartConfiguration.FromXml(node, "CP05AClipart3");
			}

			if (resourceManager.DataSharePartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSharePartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP05B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "CP05BHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersPartBItems.Add(item);
							break;
						case "CP05BHouseholdsCombo1":
							if (!String.IsNullOrEmpty(item.Value))
								PartBCombo1Items.Add(item);
							break;
						case "CP05BSharePointCombo2":
							if (!String.IsNullOrEmpty(item.Value))
								PartBCombo2Items.Add(item);
							break;
						case "CP05BSubHeader1":
							if (item.IsPlaceholder)
								PartBSubHeader1Placeholder = item.Value;
							else
								PartBSubHeader1DefaultValue = item.Value;
							break;
						case "CP05BSubHeader2":
							if (item.IsPlaceholder)
								PartBSubHeader2Placeholder = item.Value;
							else
								PartBSubHeader2DefaultValue = item.Value;
							break;
						case "CP05BSubHeader3":
							if (item.IsPlaceholder)
								PartBSubHeader3Placeholder = item.Value;
							else
								PartBSubHeader3DefaultValue = item.Value;
							break;
						case "CP05BSubHeader4":
							PartBSubHeader4DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP05BSubHeader5":
							if (item.IsPlaceholder)
								PartBSubHeader5Placeholder = item.Value;
							else
								PartBSubHeader5DefaultValue = item.Value;
							break;
						case "CP05BSubHeader6":
							PartBSubHeader6DefaultValue = Decimal.Parse(item.Value ?? "0",
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP05BSubHeader7":
							if (item.IsPlaceholder)
								PartBSubHeader7Placeholder = item.Value;
							else
								PartBSubHeader7DefaultValue = item.Value;
							break;
						case "CP05BSubHeader8":
							if (item.IsPlaceholder)
								PartBSubHeader8Placeholder = item.Value;
							else
								PartBSubHeader8DefaultValue = item.Value;
							break;
					}
				}
				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP05BClipart1");
				PartBClipart2Configuration = ClipartConfiguration.FromXml(node, "CP05BClipart2");
				PartBClipart3Configuration = ClipartConfiguration.FromXml(node, "CP05BClipart3");
			}

			if (resourceManager.DataSharePartCFile.ExistsLocal())
			{
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
								HeadersPartCItems.Add(item);
							break;
						case "CP05CBillionCombo1":
							if (!String.IsNullOrEmpty(item.Value))
								PartCCombo1Items.Add(item);
							break;
						case "CP05CYearCombo2":
							if (!String.IsNullOrEmpty(item.Value))
								PartCCombo2Items.Add(item);
							break;
						case "CP05CGeographyCombo3":
							if (!String.IsNullOrEmpty(item.Value))
								PartCCombo3Items.Add(item);
							break;
						case "CP05CPercentCombo4":
							if (!String.IsNullOrEmpty(item.Value))
								PartCCombo4Items.Add(item);
							break;
						case "CP05CPopulationCombo5":
							if (!String.IsNullOrEmpty(item.Value))
								PartCCombo5Items.Add(item);
							break;
						case "CP05CSharePercentCombo6":
							if (!String.IsNullOrEmpty(item.Value))
								PartCCombo6Items.Add(item);
							break;
						case "CP05CSubheader1":
							PartCSubHeader1DefaultValue = Decimal.Parse(item.Value ?? "0",
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP05CSubheader2":
							if (item.IsPlaceholder)
								PartCSubHeader2Placeholder = item.Value;
							else
								PartCSubHeader2DefaultValue = item.Value;
							break;
						case "CP05CSubheader3":
							if (item.IsPlaceholder)
								PartCSubHeader3Placeholder = item.Value;
							else
								PartCSubHeader3DefaultValue = item.Value;
							break;
						case "CP05CSubheader4":
							if (item.IsPlaceholder)
								PartCSubHeader4Placeholder = item.Value;
							else
								PartCSubHeader4DefaultValue = item.Value;
							break;
					}
				}
				PartCClipart1Configuration = ClipartConfiguration.FromXml(node, "CP05CClipart1");
				PartCClipart2Configuration = ClipartConfiguration.FromXml(node, "CP05CClipart2");
				PartCClipart3Configuration = ClipartConfiguration.FromXml(node, "CP05CClipart3");
			}

			if (resourceManager.DataSharePartDFile.ExistsLocal())
			{
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
								HeadersPartDItems.Add(item);
							break;
						case "CP05DPercentCombo1":
							if (!String.IsNullOrEmpty(item.Value))
								PartDCombo1Items.Add(item);
							break;
						case "CP05DPopulationCombo2":
							if (!String.IsNullOrEmpty(item.Value))
								PartDCombo2Items.Add(item);
							break;
						case "CP05DSharePercentCombo6":
							if (!String.IsNullOrEmpty(item.Value))
								PartDCombo3Items.Add(item);
							break;
						case "CP05DSubheader1":
							if (item.IsPlaceholder)
								PartDSubHeader1Placeholder = item.Value;
							else
								PartDSubHeader1DefaultValue = item.Value;
							break;
						case "CP05DSubheader2":
							if (item.IsPlaceholder)
								PartDSubHeader2Placeholder = item.Value;
							else
								PartDSubHeader2DefaultValue = item.Value;
							break;
						case "CP05DSubHeader3":
							if (item.IsPlaceholder)
								PartDSubHeader3Placeholder = item.Value;
							else
								PartDSubHeader3DefaultValue = item.Value;
							break;
						case "CP05DSubHeader4":
							PartDSubHeader4DefaultValue = Decimal.Parse(item.Value ?? "0",
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP05DSubHeader5":
							if (item.IsPlaceholder)
								PartDSubHeader5Placeholder = item.Value;
							else
								PartDSubHeader5DefaultValue = item.Value;
							break;
						case "CP05DSubHeader6":
							if (item.IsPlaceholder)
								PartDSubHeader6Placeholder = item.Value;
							else
								PartDSubHeader6DefaultValue = item.Value;
							break;
						case "CP05DSubHeader7":
							if (item.IsPlaceholder)
								PartDSubHeader7Placeholder = item.Value;
							else
								PartDSubHeader7DefaultValue = item.Value;
							break;
						case "CP05DSubHeader8":
							if (item.IsPlaceholder)
								PartDSubHeader8Placeholder = item.Value;
							else
								PartDSubHeader8DefaultValue = item.Value;
							break;
						case "CP05DSubHeader9":
							if (item.IsPlaceholder)
								PartDSubHeader9Placeholder = item.Value;
							else
								PartDSubHeader9DefaultValue = item.Value;
							break;
					}
				}
				PartDClipart1Configuration = ClipartConfiguration.FromXml(node, "CP05DClipart1");
				PartDClipart2Configuration = ClipartConfiguration.FromXml(node, "CP05DClipart2");
				PartDClipart3Configuration = ClipartConfiguration.FromXml(node, "CP05DClipart3");
			}

			if (resourceManager.DataSharePartEFile.ExistsLocal())
			{
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
								HeadersPartEItems.Add(item);
							break;
						case "CP05DBillionCombo1":
							if (!String.IsNullOrEmpty(item.Value))
								PartECombo1Items.Add(item);
							break;
						case "CP05EPercentCombo2":
							if (!String.IsNullOrEmpty(item.Value))
								PartECombo2Items.Add(item);
							break;
						case "CP05EPopulationCombo3":
							if (!String.IsNullOrEmpty(item.Value))
								PartECombo3Items.Add(item);
							break;
						case "CP05ESharePercentCombo3":
							if (!String.IsNullOrEmpty(item.Value))
								PartECombo4Items.Add(item);
							break;
						case "CP05ESubheader1":
							if (item.IsPlaceholder)
								PartESubHeader1Placeholder = item.Value;
							else
								PartESubHeader1DefaultValue = item.Value;
							break;
						case "CP05ESubheader2":
							if (item.IsPlaceholder)
								PartESubHeader2Placeholder = item.Value;
							else
								PartESubHeader2DefaultValue = item.Value;
							break;
						case "CP05ESubheader3":
							if (item.IsPlaceholder)
								PartESubHeader3Placeholder = item.Value;
							else
								PartESubHeader3DefaultValue = item.Value;
							break;
						case "CP05ESubheader4":
							if (item.IsPlaceholder)
								PartESubHeader4Placeholder = item.Value;
							else
								PartESubHeader4DefaultValue = item.Value;
							break;
						case "CP05ESubheader5":
							if (item.IsPlaceholder)
								PartESubHeader5Placeholder = item.Value;
							else
								PartESubHeader5DefaultValue = item.Value;
							break;
						case "CP05ESubheader6":
							if (item.IsPlaceholder)
								PartESubHeader6Placeholder = item.Value;
							else
								PartESubHeader6DefaultValue = item.Value;
							break;
						case "CP05ESubheader7":
							PartESubHeader7DefaultValue = Decimal.Parse(item.Value ?? "0",
								NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
							break;
						case "CP05ESubheader8":
							if (item.IsPlaceholder)
								PartESubHeader8Placeholder = item.Value;
							else
								PartESubHeader8DefaultValue = item.Value;
							break;
						case "CP05ESubheader9":
							if (item.IsPlaceholder)
								PartESubHeader9Placeholder = item.Value;
							else
								PartESubHeader9DefaultValue = item.Value;
							break;
						case "CP05ESubheader10":
							if (item.IsPlaceholder)
								PartESubHeader10Placeholder = item.Value;
							else
								PartESubHeader10DefaultValue = item.Value;
							break;
					}
				}

				PartEClipart1Configuration = ClipartConfiguration.FromXml(node, "CP05EClipart1");
				PartEClipart2Configuration = ClipartConfiguration.FromXml(node, "CP05EClipart2");
				PartEClipart3Configuration = ClipartConfiguration.FromXml(node, "CP05EClipart3");
			}

			if (resourceManager.Tab5PartUSlidesFolder.ExistsLocal())
			{
				PartUSlides.LoadSlides(resourceManager.Tab5PartUSlidesFolder);
			}

			if (resourceManager.Tab5PartVSlidesFolder.ExistsLocal())
			{
				PartVSlides.LoadSlides(resourceManager.Tab5PartVSlidesFolder);
			}

			if (resourceManager.Tab5PartWSlidesFolder.ExistsLocal())
			{
				PartWSlides.LoadSlides(resourceManager.Tab5PartWSlidesFolder);
			}
		}
	}
}
