using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Investment
{
	public class InvestmentTabDInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab12_D_Clipart1;
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab12_D_Clipart2;
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public Image Clipart3Image => _resourceManager.GraphicResources?.Tab12_D_Clipart3;
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public Image Clipart4Image => _resourceManager.GraphicResources?.Tab12_D_Clipart4;
		public ClipartConfiguration Clipart4Configuration { get; private set; }

		public Image Clipart5Image => _resourceManager.GraphicResources?.Tab12_D_Clipart5;
		public ClipartConfiguration Clipart5Configuration { get; private set; }

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

		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader1Configuration { get; set; }

		public string SubHeader2DefaultValue { get; private set; }
		public string SubHeader2Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader2Configuration { get; set; }

		public InvestmentTabDInfo() : base(ShiftChildTabType.D, ShiftTopTabType.Investment)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
			Clipart4Configuration = new ClipartConfiguration();
			Clipart5Configuration = new ClipartConfiguration();

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

			SubHeader1Configuration = TextEditorConfiguration.Empty();
			SubHeader2Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (!resourceManager.DataInvestmentPartDFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataInvestmentPartDFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT12D");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT12DHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT12DCombo1":
						if (!String.IsNullOrEmpty(item.Value))
							Combo1Items.Add(item);
						break;
					case "SHIFT12DCombo2":
						if (!String.IsNullOrEmpty(item.Value))
							Combo2Items.Add(item);
						break;
					case "SHIFT12DCombo3":
						if (!String.IsNullOrEmpty(item.Value))
							Combo3Items.Add(item);
						break;
					case "SHIFT12DCombo4":
						if (!String.IsNullOrEmpty(item.Value))
							Combo4Items.Add(item);
						break;
					case "SHIFT12DCombo5":
						if (!String.IsNullOrEmpty(item.Value))
							Combo5Items.Add(item);
						break;
					case "SHIFT12DCombo6":
						if (!String.IsNullOrEmpty(item.Value))
							Combo6Items.Add(item);
						break;
					case "SHIFT12DSubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
					case "SHIFT12DSubheader2":
						if (item.IsPlaceholder)
							SubHeader2Placeholder = item.Value;
						else
							SubHeader2DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT12DClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT12DClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT12DClipart3");
			Clipart4Configuration = ClipartConfiguration.FromXml(node, "SHIFT12DClipart4");
			Clipart5Configuration = ClipartConfiguration.FromXml(node, "SHIFT12DClipart5");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT12DHeader");

			Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT12DCombo1");
			Combo2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT12DCombo2");
			Combo3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT12DCombo3");
			Combo4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT12DCombo4");
			Combo5Configuration = TextEditorConfiguration.FromXml(node, "SHIFT12DCombo5");
			Combo6Configuration = TextEditorConfiguration.FromXml(node, "SHIFT12DCombo6");

			SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT12DSubheader1");
			SubHeader2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT12DSubheader2");
		}
	}
}