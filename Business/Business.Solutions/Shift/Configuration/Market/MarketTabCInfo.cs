using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Market
{
	public class MarketTabCInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab5_C_Clipart1;
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab5_C_Clipart2;
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public Image Clipart3Image => _resourceManager.GraphicResources?.Tab5_C_Clipart3;
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public Image Clipart4Image => _resourceManager.GraphicResources?.Tab5_C_Clipart4;
		public ClipartConfiguration Clipart4Configuration { get; private set; }

		public Image Clipart5Image => _resourceManager.GraphicResources?.Tab5_C_Clipart5;
		public ClipartConfiguration Clipart5Configuration { get; private set; }

		public List<ListDataItem> Combo1Items { get; }
		public TextEditorConfiguration Combo1Configuration { get; set; }

		public ListDataItem MemoPopup1DefaultItem { get; private set; }
		public TextEditorConfiguration MemoPopup1Configuration { get; set; }

		public ListDataItem MemoPopup2DefaultItem { get; private set; }
		public TextEditorConfiguration MemoPopup2Configuration { get; set; }

		public ListDataItem MemoPopup3DefaultItem { get; private set; }
		public TextEditorConfiguration MemoPopup3Configuration { get; set; }

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

		public MarketTabCInfo() : base(ShiftChildTabType.C, ShiftTopTabType.Market)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
			Clipart4Configuration = new ClipartConfiguration();
			Clipart5Configuration = new ClipartConfiguration();

			Combo1Items = new List<ListDataItem>();
			Combo1Configuration = TextEditorConfiguration.Empty();

			MemoPopup1Configuration = TextEditorConfiguration.Empty();
			MemoPopup2Configuration = TextEditorConfiguration.Empty();
			MemoPopup3Configuration = TextEditorConfiguration.Empty();

			SubHeader1Configuration = TextEditorConfiguration.Empty();
			SubHeader2Configuration = TextEditorConfiguration.Empty();
			SubHeader3Configuration = TextEditorConfiguration.Empty();
			SubHeader4Configuration = TextEditorConfiguration.Empty();
			SubHeader5Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (!resourceManager.DataMarketPartCFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataMarketPartCFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT05C");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT05CHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT05CCombo1":
						if (!String.IsNullOrEmpty(item.Value))
							Combo1Items.Add(item);
						break;
					case "SHIFT05CMULTIBOX1":
						MemoPopup1DefaultItem = item;
						break;
					case "SHIFT05CMULTIBOX2":
						MemoPopup2DefaultItem = item;
						break;
					case "SHIFT05CMULTIBOX3":
						MemoPopup3DefaultItem = item;
						break;
					case "SHIFT05CSubheader1":
						SubHeader1DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "SHIFT05CSubheader2":
						if (item.IsPlaceholder)
							SubHeader2Placeholder = item.Value;
						else
							SubHeader2DefaultValue = item.Value;
						break;
					case "SHIFT05CSubheader3":
						SubHeader3DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "SHIFT05CSubheader4":
						SubHeader4DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "SHIFT05CSubheader5":
						SubHeader5DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT05CClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT05CClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT05CClipart3");
			Clipart4Configuration = ClipartConfiguration.FromXml(node, "SHIFT05CClipart4");
			Clipart5Configuration = ClipartConfiguration.FromXml(node, "SHIFT05CClipart5");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT05CHeader");
			Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05CCombo1");
			MemoPopup1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05CMULTIBOX1");
			MemoPopup2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05CMULTIBOX2");
			MemoPopup3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05CMULTIBOX3");
			SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05CSubheader1");
			SubHeader2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05CSubheader2");
			SubHeader3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05CSubheader3");
			SubHeader4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05CSubheader4");
			SubHeader5Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05CSubheader5");
		}
	}
}
