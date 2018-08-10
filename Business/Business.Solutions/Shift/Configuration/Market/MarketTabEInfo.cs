using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Market
{
	public class MarketTabEInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public Image Clipart2Image { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public List<ListDataItem> Combo1Items { get; }
		public TextEditorConfiguration Combo1Configuration { get; set; }
		public List<ListDataItem> Combo2Items { get; }
		public TextEditorConfiguration Combo2Configuration { get; set; }
		public List<ListDataItem> Combo3Items { get; }
		public TextEditorConfiguration Combo3Configuration { get; set; }
		public List<ListDataItem> Combo4Items { get; }
		public TextEditorConfiguration Combo4Configuration { get; set; }
		public List<ListDataItem> Combo5Items { get; }
		public TextEditorConfiguration Combo5Configuration { get; set; }
		public List<ListDataItem> Combo6Items { get; }
		public TextEditorConfiguration Combo6Configuration { get; set; }
		public List<ListDataItem> Combo7Items { get; }
		public TextEditorConfiguration Combo7Configuration { get; set; }
		public List<ListDataItem> Combo8Items { get; }
		public TextEditorConfiguration Combo8Configuration { get; set; }
		public List<ListDataItem> Combo9Items { get; }
		public TextEditorConfiguration Combo9Configuration { get; set; }
		public List<ListDataItem> Combo10Items { get; }
		public TextEditorConfiguration Combo10Configuration { get; set; }
		public List<ListDataItem> Combo11Items { get; }
		public TextEditorConfiguration Combo11Configuration { get; set; }
		public List<ListDataItem> Combo12Items { get; }
		public TextEditorConfiguration Combo12Configuration { get; set; }
		public List<ListDataItem> Combo13Items { get; }
		public TextEditorConfiguration Combo13Configuration { get; set; }

		public decimal? SubHeader1DefaultValue { get; private set; }
		public TextEditorConfiguration SubHeader1Configuration { get; set; }

		public string SubHeader2DefaultValue { get; private set; }
		public string SubHeader2Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader2Configuration { get; set; }

		public decimal? SubHeader3DefaultValue { get; private set; }
		public TextEditorConfiguration SubHeader3Configuration { get; set; }

		public decimal? SubHeader4DefaultValue { get; private set; }
		public TextEditorConfiguration SubHeader4Configuration { get; set; }

		public decimal? SubHeader5DefaultValue { get; private set; }
		public TextEditorConfiguration SubHeader5Configuration { get; set; }

		public MarketTabEInfo() : base(ShiftChildTabType.E)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();

			Combo1Items = new List<ListDataItem>();
			Combo1Configuration = TextEditorConfiguration.Empty();
			Combo2Items = new List<ListDataItem>();
			Combo2Configuration = TextEditorConfiguration.Empty();
			Combo3Items = new List<ListDataItem>();
			Combo3Configuration = TextEditorConfiguration.Empty();
			Combo4Items = new List<ListDataItem>();
			Combo4Configuration = TextEditorConfiguration.Empty();
			Combo5Items = new List<ListDataItem>();
			Combo5Configuration = TextEditorConfiguration.Empty();
			Combo6Items = new List<ListDataItem>();
			Combo6Configuration = TextEditorConfiguration.Empty();
			Combo7Items = new List<ListDataItem>();
			Combo7Configuration = TextEditorConfiguration.Empty();
			Combo8Items = new List<ListDataItem>();
			Combo8Configuration = TextEditorConfiguration.Empty();
			Combo9Items = new List<ListDataItem>();
			Combo9Configuration = TextEditorConfiguration.Empty();
			Combo10Items = new List<ListDataItem>();
			Combo10Configuration = TextEditorConfiguration.Empty();
			Combo11Items = new List<ListDataItem>();
			Combo11Configuration = TextEditorConfiguration.Empty();
			Combo12Items = new List<ListDataItem>();
			Combo12Configuration = TextEditorConfiguration.Empty();
			Combo13Items = new List<ListDataItem>();
			Combo13Configuration = TextEditorConfiguration.Empty();

			SubHeader1Configuration = TextEditorConfiguration.Empty();
			SubHeader2Configuration = TextEditorConfiguration.Empty();
			SubHeader3Configuration = TextEditorConfiguration.Empty();
			SubHeader4Configuration = TextEditorConfiguration.Empty();
			SubHeader5Configuration = TextEditorConfiguration.Empty();
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

			if (!resourceManager.DataMarketPartEFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataMarketPartEFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT05E");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT05EHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT05ECombo1":
						if (!String.IsNullOrEmpty(item.Value))
							Combo1Items.Add(item);
						break;
					case "SHIFT05ECombo2":
						if (!String.IsNullOrEmpty(item.Value))
							Combo2Items.Add(item);
						break;
					case "SHIFT05ECombo3":
						if (!String.IsNullOrEmpty(item.Value))
							Combo3Items.Add(item);
						break;
					case "SHIFT05ECombo4":
						if (!String.IsNullOrEmpty(item.Value))
							Combo4Items.Add(item);
						break;
					case "SHIFT05ECombo5":
						if (!String.IsNullOrEmpty(item.Value))
							Combo5Items.Add(item);
						break;
					case "SHIFT05ECombo6":
						if (!String.IsNullOrEmpty(item.Value))
							Combo6Items.Add(item);
						break;
					case "SHIFT05ECombo7":
						if (!String.IsNullOrEmpty(item.Value))
							Combo7Items.Add(item);
						break;
					case "SHIFT05ECombo8":
						if (!String.IsNullOrEmpty(item.Value))
							Combo8Items.Add(item);
						break;
					case "SHIFT05ECombo9":
						if (!String.IsNullOrEmpty(item.Value))
							Combo9Items.Add(item);
						break;
					case "SHIFT05ECombo10":
						if (!String.IsNullOrEmpty(item.Value))
							Combo10Items.Add(item);
						break;
					case "SHIFT05ECombo11":
						if (!String.IsNullOrEmpty(item.Value))
							Combo11Items.Add(item);
						break;
					case "SHIFT05ECombo12":
						if (!String.IsNullOrEmpty(item.Value))
							Combo12Items.Add(item);
						break;
					case "SHIFT05ECombo13":
						if (!String.IsNullOrEmpty(item.Value))
							Combo13Items.Add(item);
						break;
					case "SHIFT05ESubheader1":
						SubHeader1DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "SHIFT05ESubheader2":
						if (item.IsPlaceholder)
							SubHeader2Placeholder = item.Value;
						else
							SubHeader2DefaultValue = item.Value;
						break;
					case "SHIFT05ESubheader3":
						SubHeader3DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "SHIFT05ESubheader4":
						SubHeader4DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "SHIFT05ESubheader5":
						SubHeader5DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT05EClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT05EClipart2");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT05EHeader");
			Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ECombo1");
			Combo2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ECombo2");
			Combo3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ECombo3");
			Combo4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ECombo4");
			Combo5Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ECombo5");
			Combo6Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ECombo6");
			Combo7Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ECombo7");
			Combo8Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ECombo8");
			Combo9Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ECombo9");
			Combo10Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ECombo10");
			Combo11Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ECombo11");
			Combo12Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ECombo12");
			Combo13Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ECombo13");
			SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ESubheader1");
			SubHeader2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ESubheader2");
			SubHeader3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ESubheader3");
			SubHeader4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ESubheader4");
			SubHeader5Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ESubheader5");
		}
	}
}
