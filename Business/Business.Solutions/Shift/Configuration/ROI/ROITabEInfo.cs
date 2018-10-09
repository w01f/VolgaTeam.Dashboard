using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.ROI
{
	public class ROITabEInfo : ShiftTabWithHeaderInfo
	{
		public Tab1Info Tab1 { get; private set; }
		public Tab2Info Tab2 { get; private set; }
		public Tab3Info Tab3 { get; private set; }

		public ROITabEInfo() : base(ShiftChildTabType.E, ShiftTopTabType.ROI)
		{
			Tab1 = Tab1Info.Empty();
			Tab2 = Tab2Info.Empty();
			Tab3 = Tab3Info.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (!_resourceManager.DataROIPartEFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(_resourceManager.DataROIPartEFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT13E");
			if (node == null) return;

			foreach (var headerNode in (node.SelectNodes("./SHIFT13EHeader")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { }).ToList())
				HeadersItems.Add(ListDataItem.FromXml(headerNode));

			Tab1 = Tab1Info.FromXml(node);
			Tab2 = Tab2Info.FromXml(node);
			Tab3 = Tab3Info.FromXml(node, _resourceManager);

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT13EHeader");
		}

		public class Tab1Info
		{
			public const string GridColumnNameRowTitle = "RowTitle";
			public const string GridColumnNamePrelaunch = "Prelaunch";
			public const string GridColumnNameTotal = "Total";
			public const string GridColumnNameMonthDefaultPrefix = "Month";

			public const string GridRowTitleMediaImpressions = "MediaImpressions";
			public const string GridRowTitleDigitalImpressions = "DigitalImpressions";
			public const string GridRowTitleTotalImpressions = "TotalImpressions";
			public const string GridRowTitleInvestment = "Investment";

			public string Title { get; private set; }

			public List<ListDataItem> Combo1Items { get; }
			public TextEditorConfiguration Combo1Configuration { get; private set; }

			public List<ListDataItem> Combo2Items { get; }
			public TextEditorConfiguration Combo2Configuration { get; private set; }

			public CheckboxInfo Checkbox1 { get; private set; }
			public CheckboxInfo Checkbox2 { get; private set; }

			public Color PrelaunchEmptySpaceBackColor { get; private set; }
			public Color GridLineColor { get; private set; }
			public Color RowTitlesForeColor { get; private set; }
			public Color ValueCellsForeColor { get; private set; }
			public Color HeaderForeColor { get; private set; }

			public Tab1Info()
			{
				Combo1Items = new List<ListDataItem>();
				Combo1Configuration = TextEditorConfiguration.Empty();
				Combo2Items = new List<ListDataItem>();
				Combo2Configuration = TextEditorConfiguration.Empty();

				Checkbox1 = CheckboxInfo.Empty();
				Checkbox2 = CheckboxInfo.Empty();

				PrelaunchEmptySpaceBackColor = Color.Empty;
				GridLineColor = Color.Empty;
				RowTitlesForeColor = Color.Empty;
				ValueCellsForeColor = Color.Empty;
				HeaderForeColor = Color.Empty;
			}

			public static Tab1Info FromXml(XmlNode configNode)
			{
				var tabInfo = Empty();
				if (configNode != null)
				{
					foreach (var titleNode in (configNode.SelectNodes("./SHIFT13ETabStrip")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { }).ToList())
					{
						var titleAttribute = titleNode.Attributes?.OfType<XmlAttribute>()
							.FirstOrDefault(a => String.Equals(a.Name, "Tab1Name", StringComparison.OrdinalIgnoreCase));

						if (titleAttribute == null) continue;

						tabInfo.Title = titleAttribute.Value;
						break;
					}

					foreach (var comboNode in (configNode.SelectNodes("./SHIFT13ETAB1COMBO1")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { }).ToList())
						tabInfo.Combo1Items.Add(ListDataItem.FromXml(comboNode));

					foreach (var comboNode in (configNode.SelectNodes("./SHIFT13ETAB1COMBO2")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { }).ToList())
						tabInfo.Combo2Items.Add(ListDataItem.FromXml(comboNode));

					tabInfo.Checkbox1 = CheckboxInfo.FromXml(configNode.SelectSingleNode("./SHIFT13ECheckbox1"));
					tabInfo.Checkbox2 = CheckboxInfo.FromXml(configNode.SelectSingleNode("./SHIFT13ECheckbox2"));

					tabInfo.Combo1Configuration = TextEditorConfiguration.FromXml(configNode, "SHIFT13ETAB1COMBO1");
					tabInfo.Combo2Configuration = TextEditorConfiguration.FromXml(configNode, "SHIFT13ETAB1COMBO2");

					foreach (var styleNode in (configNode.SelectNodes("./ControlStyle")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { }).ToList())
					{
						var controlAttributeValue = styleNode.Attributes?.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Control", StringComparison.OrdinalIgnoreCase))?.Value;
						switch (controlAttributeValue)
						{
							case "GridLines":
								tabInfo.GridLineColor = ColorTranslator.FromHtml(styleNode.Attributes
									?.OfType<XmlAttribute>()
									.FirstOrDefault(a => String.Equals(a.Name, "Gridlinescolor",
										StringComparison.OrdinalIgnoreCase))?.Value);
								break;
							case "GridRow":
								tabInfo.RowTitlesForeColor = ColorTranslator.FromHtml(styleNode.Attributes
									?.OfType<XmlAttribute>()
									.FirstOrDefault(a => String.Equals(a.Name, "FarLeftHeadercolor",
										StringComparison.OrdinalIgnoreCase))?.Value);
								tabInfo.ValueCellsForeColor = ColorTranslator.FromHtml(styleNode.Attributes
									?.OfType<XmlAttribute>()
									.FirstOrDefault(a => String.Equals(a.Name, "CellTextColor",
										StringComparison.OrdinalIgnoreCase))?.Value);
								break;
							case "GridColumn":
								tabInfo.HeaderForeColor = ColorTranslator.FromHtml(styleNode.Attributes
									?.OfType<XmlAttribute>()
									.FirstOrDefault(a => String.Equals(a.Name, "TopHeadercolor",
										StringComparison.OrdinalIgnoreCase))?.Value);
								break;
							case "PreLaunchShade":
								tabInfo.PrelaunchEmptySpaceBackColor = ColorTranslator.FromHtml(styleNode.Attributes
									?.OfType<XmlAttribute>()
									.FirstOrDefault(a => String.Equals(a.Name, "FillColor",
										StringComparison.OrdinalIgnoreCase))?.Value);
								break;
						}
					}
				}
				return tabInfo;
			}

			public static Tab1Info Empty()
			{
				return new Tab1Info();
			}
		}

		public class Tab2Info
		{
			public string Title { get; private set; }

			public decimal? SubHeader1DefaultValue { get; private set; }
			public TextEditorConfiguration Subheader1Configuration { get; private set; }

			public decimal? SubHeader2DefaultValue { get; private set; }
			public TextEditorConfiguration Subheader2Configuration { get; private set; }

			public string SubHeader3DefaultValue { get; private set; }
			public string SubHeader3Placeholder { get; private set; }
			public TextEditorConfiguration Subheader3Configuration { get; private set; }

			public TextEditorConfiguration Combo2Configuration { get; set; }

			public Tab2Info()
			{
				Subheader1Configuration = TextEditorConfiguration.Empty();
				Subheader2Configuration = TextEditorConfiguration.Empty();
				Subheader3Configuration = TextEditorConfiguration.Empty();
			}

			public static Tab2Info FromXml(XmlNode configNode)
			{
				var tabInfo = Empty();
				if (configNode != null)
				{
					foreach (var titleNode in (configNode.SelectNodes("./SHIFT13ETabStrip")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { }).ToList())
					{
						var titleAttribute = titleNode.Attributes?.OfType<XmlAttribute>()
							.FirstOrDefault(a => String.Equals(a.Name, "Tab2Name", StringComparison.OrdinalIgnoreCase));

						if (titleAttribute == null) continue;

						tabInfo.Title = titleAttribute.Value;
						break;
					}

					var subheader1Item = ListDataItem.FromXml(configNode.SelectSingleNode("./SHIFT13ETAB2Subheader1"));
					if (subheader1Item.IsDefault && !subheader1Item.IsPlaceholder && !String.IsNullOrWhiteSpace(subheader1Item.Value))
					{
						var itemValue = subheader1Item.Value.ToLower();
						var scaleFactor = 1m;
						if (itemValue.EndsWith("b"))
						{
							itemValue = itemValue.Replace("b", "");
							scaleFactor = 1000000000m;
						}
						else if (itemValue.EndsWith("m"))
						{
							itemValue = itemValue.Replace("m", "");
							scaleFactor = 1000000m;
						}

						tabInfo.SubHeader1DefaultValue = Decimal.Parse(itemValue,
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands) *
							scaleFactor;
					}

					var subheader2Item = ListDataItem.FromXml(configNode.SelectSingleNode("./SHIFT13ETAB2Subheader2"));
					if (subheader2Item.IsDefault && !subheader2Item.IsPlaceholder && !String.IsNullOrWhiteSpace(subheader2Item.Value))
					{
						var itemValue = subheader2Item.Value.ToLower();
						var scaleFactor = 1m;
						if (itemValue.EndsWith("b"))
						{
							itemValue = itemValue.Replace("b", "");
							scaleFactor = 1000000000m;
						}
						else if (itemValue.EndsWith("m"))
						{
							itemValue = itemValue.Replace("m", "");
							scaleFactor = 1000000m;
						}

						tabInfo.SubHeader2DefaultValue = Decimal.Parse(itemValue,
															 NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands) *
														 scaleFactor;
					}

					var subheader3Item = ListDataItem.FromXml(configNode.SelectSingleNode("./SHIFT13ETAB2Subheader3"));
					if (subheader3Item.IsDefault && !subheader3Item.IsPlaceholder)
						tabInfo.SubHeader3DefaultValue = subheader3Item.Value;
					else if (subheader3Item.IsPlaceholder)
						tabInfo.SubHeader3Placeholder = subheader3Item.Value;

					tabInfo.Subheader1Configuration = TextEditorConfiguration.FromXml(configNode, "SHIFT13ESubHeader1");
					tabInfo.Subheader2Configuration = TextEditorConfiguration.FromXml(configNode, "SHIFT13ESubHeader2");
					tabInfo.Subheader3Configuration = TextEditorConfiguration.FromXml(configNode, "SHIFT13ESubHeader3");
				}
				return tabInfo;
			}

			public static Tab2Info Empty()
			{
				return new Tab2Info();
			}
		}

		public class Tab3Info
		{
			public string Title { get; private set; }

			public List<ListDataItem> Combo1Items { get; }
			public TextEditorConfiguration Combo1Configuration { get; private set; }

			public List<ListDataItem> Combo2Items { get; }
			public TextEditorConfiguration Combo2Configuration { get; private set; }

			public List<ListDataItem> Combo3Items { get; }
			public TextEditorConfiguration Combo3Configuration { get; private set; }

			public List<ListDataItem> Combo4Items { get; }
			public TextEditorConfiguration Combo4Configuration { get; private set; }

			public decimal? SubHeader1DefaultValue { get; private set; }
			public TextEditorConfiguration Subheader1Configuration { get; private set; }

			public decimal? SubHeader2DefaultValue { get; private set; }
			public TextEditorConfiguration Subheader2Configuration { get; private set; }

			public decimal? SubHeader3DefaultValue { get; private set; }
			public TextEditorConfiguration Subheader3Configuration { get; private set; }

			public decimal? SubHeader4DefaultValue { get; private set; }
			public TextEditorConfiguration Subheader4Configuration { get; private set; }

			public Tab3Info()
			{
				Combo1Items = new List<ListDataItem>();
				Combo1Configuration = TextEditorConfiguration.Empty();
				Combo2Items = new List<ListDataItem>();
				Combo2Configuration = TextEditorConfiguration.Empty();
				Combo3Items = new List<ListDataItem>();
				Combo3Configuration = TextEditorConfiguration.Empty();
				Combo4Items = new List<ListDataItem>();
				Combo4Configuration = TextEditorConfiguration.Empty();

				Subheader1Configuration = TextEditorConfiguration.Empty();
				Subheader2Configuration = TextEditorConfiguration.Empty();
				Subheader3Configuration = TextEditorConfiguration.Empty();
				Subheader4Configuration = TextEditorConfiguration.Empty();
			}

			public static Tab3Info FromXml(XmlNode configNode, ResourceManager resourceManager)
			{
				var tabInfo = Empty();
				if (configNode != null)
				{
					foreach (var titleNode in (configNode.SelectNodes("./SHIFT13ETabStrip")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { }).ToList())
					{
						var titleAttribute = titleNode.Attributes?.OfType<XmlAttribute>()
							.FirstOrDefault(a => String.Equals(a.Name, "Tab3Name", StringComparison.OrdinalIgnoreCase));

						if (titleAttribute == null) continue;

						tabInfo.Title = titleAttribute.Value;
						break;
					}

					foreach (var comboNode in (configNode.SelectNodes("./SHIFT13ETAB3COMBO1")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { }).ToList())
						tabInfo.Combo1Items.Add(ListDataItem.FromXml(comboNode));

					foreach (var comboNode in (configNode.SelectNodes("./SHIFT13ETAB3COMBO2")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { }).ToList())
						tabInfo.Combo2Items.Add(ListDataItem.FromXml(comboNode));

					foreach (var comboNode in (configNode.SelectNodes("./SHIFT13ETAB3COMBO3")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { }).ToList())
						tabInfo.Combo3Items.Add(ListDataItem.FromXml(comboNode));

					foreach (var comboNode in (configNode.SelectNodes("./SHIFT13ETAB3COMBO4")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { }).ToList())
						tabInfo.Combo4Items.Add(ListDataItem.FromXml(comboNode));

					if (resourceManager.DataAgreementCommonFile.ExistsLocal())
					{
						var comboLists = new[]
						{
							tabInfo.Combo1Items,
							tabInfo.Combo2Items,
							tabInfo.Combo3Items,
							tabInfo.Combo4Items,
						};

						var document = new XmlDocument();
						document.Load(resourceManager.DataAgreementCommonFile.LocalPath);

						var televisionNodes = document.SelectNodes("//Settings/Television/Item")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
						foreach (var itemNode in televisionNodes)
						{
							var nodeValue = itemNode?.InnerText;
							if (!String.IsNullOrWhiteSpace(nodeValue))
								foreach (var comboList in comboLists)
									if (!comboList.Any(item => String.Equals(nodeValue, item.Value, StringComparison.OrdinalIgnoreCase)))
										comboList.Add(ListDataItem.FromString(nodeValue));
						}

						var digitalNodes = document.SelectNodes("//Settings/Digital/Item")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
						foreach (var itemNode in digitalNodes)
						{
							var nodeValue = itemNode?.InnerText;
							if (!String.IsNullOrWhiteSpace(nodeValue))
								foreach (var comboList in comboLists)
									if (!comboList.Any(item => String.Equals(nodeValue, item.Value, StringComparison.OrdinalIgnoreCase)))
										comboList.Add(ListDataItem.FromString(nodeValue));
						}
					}


					var subheader1Item = ListDataItem.FromXml(configNode.SelectSingleNode("./SHIFT13ETAB3Subheader1"));
					if (subheader1Item.IsDefault && !subheader1Item.IsPlaceholder && !String.IsNullOrWhiteSpace(subheader1Item.Value))
					{
						var itemValue = subheader1Item.Value.ToLower();
						var scaleFactor = 1m;
						if (itemValue.EndsWith("b"))
						{
							itemValue = itemValue.Replace("b", "");
							scaleFactor = 1000000000m;
						}
						else if (itemValue.EndsWith("m"))
						{
							itemValue = itemValue.Replace("m", "");
							scaleFactor = 1000000m;
						}

						tabInfo.SubHeader1DefaultValue = Decimal.Parse(itemValue,
															 NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands) *
														 scaleFactor;
					}

					var subheader2Item = ListDataItem.FromXml(configNode.SelectSingleNode("./SHIFT13ETAB3Subheader2"));
					if (subheader2Item.IsDefault && !subheader2Item.IsPlaceholder && !String.IsNullOrWhiteSpace(subheader2Item.Value))
					{
						var itemValue = subheader2Item.Value.ToLower();
						var scaleFactor = 1m;
						if (itemValue.EndsWith("b"))
						{
							itemValue = itemValue.Replace("b", "");
							scaleFactor = 1000000000m;
						}
						else if (itemValue.EndsWith("m"))
						{
							itemValue = itemValue.Replace("m", "");
							scaleFactor = 1000000m;
						}

						tabInfo.SubHeader2DefaultValue = Decimal.Parse(itemValue,
															 NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands) *
														 scaleFactor;
					}

					var subheader3Item = ListDataItem.FromXml(configNode.SelectSingleNode("./SHIFT13ETAB3Subheader3"));
					if (subheader3Item.IsDefault && !subheader3Item.IsPlaceholder && !String.IsNullOrWhiteSpace(subheader3Item.Value))
					{
						var itemValue = subheader3Item.Value.ToLower();
						var scaleFactor = 1m;
						if (itemValue.EndsWith("b"))
						{
							itemValue = itemValue.Replace("b", "");
							scaleFactor = 1000000000m;
						}
						else if (itemValue.EndsWith("m"))
						{
							itemValue = itemValue.Replace("m", "");
							scaleFactor = 1000000m;
						}

						tabInfo.SubHeader3DefaultValue = Decimal.Parse(itemValue,
															 NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands) *
														 scaleFactor;
					}

					var subheader4Item = ListDataItem.FromXml(configNode.SelectSingleNode("./SHIFT13ETAB3Subheader4"));
					if (subheader4Item.IsDefault && !subheader4Item.IsPlaceholder && !String.IsNullOrWhiteSpace(subheader4Item.Value))
					{
						var itemValue = subheader4Item.Value.ToLower();
						var scaleFactor = 1m;
						if (itemValue.EndsWith("b"))
						{
							itemValue = itemValue.Replace("b", "");
							scaleFactor = 1000000000m;
						}
						else if (itemValue.EndsWith("m"))
						{
							itemValue = itemValue.Replace("m", "");
							scaleFactor = 1000000m;
						}

						tabInfo.SubHeader4DefaultValue = Decimal.Parse(itemValue,
															 NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands) *
														 scaleFactor;
					}

					tabInfo.Combo1Configuration = TextEditorConfiguration.FromXml(configNode, "SHIFT13ETAB1COMBO1");
					tabInfo.Combo2Configuration = TextEditorConfiguration.FromXml(configNode, "SHIFT13ETAB1COMBO2");
					tabInfo.Combo3Configuration = TextEditorConfiguration.FromXml(configNode, "SHIFT13ETAB1COMBO3");
					tabInfo.Combo4Configuration = TextEditorConfiguration.FromXml(configNode, "SHIFT13ETAB1COMBO4");

					tabInfo.Subheader1Configuration = TextEditorConfiguration.FromXml(configNode, "SHIFT13ETAB3SubHeader1");
					tabInfo.Subheader2Configuration = TextEditorConfiguration.FromXml(configNode, "SHIFT13ETAB3SubHeader2");
					tabInfo.Subheader3Configuration = TextEditorConfiguration.FromXml(configNode, "SHIFT13ETAB3SubHeader3");
					tabInfo.Subheader4Configuration = TextEditorConfiguration.FromXml(configNode, "SHIFT13ETAB3SubHeader4");
				}
				return tabInfo;
			}

			public static Tab3Info Empty()
			{
				return new Tab3Info();
			}
		}
	}
}
