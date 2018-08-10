using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Market
{
	public class MarketTabAInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public Image Clipart2Image { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public Image Clipart3Image { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public Image Clipart4Image { get; private set; }
		public ClipartConfiguration Clipart4Configuration { get; private set; }

		public Image Clipart5Image { get; private set; }
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

		public MarketTabAInfo() : base(ShiftChildTabType.A)
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
			Clipart4Image = resourceManager.ClipartTab5SubA4File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubA4File.LocalPath)
				: null;
			Clipart5Image = resourceManager.ClipartTab5SubA5File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab5SubA5File.LocalPath)
				: null;

			if (!resourceManager.DataMarketPartAFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataMarketPartAFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT05A");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT05AHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT05ACombo1":
						if (!String.IsNullOrEmpty(item.Value))
							Combo1Items.Add(item);
						break;
					case "SHIFT05AMULTIBOX1":
						MemoPopup1DefaultItem = item;
						break;
					case "SHIFT05AMULTIBOX2":
						MemoPopup2DefaultItem = item;
						break;
					case "SHIFT05AMULTIBOX3":
						MemoPopup3DefaultItem = item;
						break;
					case "SHIFT05ASubheader1":
						SubHeader1DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "SHIFT05ASubheader2":
						if (item.IsPlaceholder)
							SubHeader2Placeholder = item.Value;
						else
							SubHeader2DefaultValue = item.Value;
						break;
					case "SHIFT05ASubheader3":
						SubHeader3DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "SHIFT05ASubheader4":
						SubHeader4DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
					case "SHIFT05ASubheader5":
						SubHeader5DefaultValue = Decimal.Parse((item.Value ?? "0").Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, String.Empty),
							NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT05AClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT05AClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT05AClipart3");
			Clipart4Configuration = ClipartConfiguration.FromXml(node, "SHIFT05AClipart4");
			Clipart5Configuration = ClipartConfiguration.FromXml(node, "SHIFT05AClipart5");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT05AHeader");
			Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ACombo1");
			MemoPopup1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05AMULTIBOX1");
			MemoPopup2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05AMULTIBOX2");
			MemoPopup3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05AMULTIBOX3");
			SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ASubheader1");
			SubHeader2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ASubheader2");
			SubHeader3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ASubheader3");
			SubHeader4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ASubheader4");
			SubHeader5Configuration = TextEditorConfiguration.FromXml(node, "SHIFT05ASubheader5");
		}
	}
}
