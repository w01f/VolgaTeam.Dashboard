using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Investment
{
	public class InvestmentTabFInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab12_F_Clipart1;
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab12_F_Clipart2;
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public Image Clipart3Image => _resourceManager.GraphicResources?.Tab12_F_Clipart3;
		public ClipartConfiguration Clipart3Configuration { get; private set; }

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

		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader1Configuration { get; set; }

		public InvestmentTabFInfo() : base(ShiftChildTabType.F, ShiftTopTabType.Investment)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();

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

			SubHeader1Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (resourceManager.DataInvestmentPartFFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataInvestmentPartFFile.LocalPath);

				var node = document.SelectSingleNode(@"//SHIFT012F");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT012FHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
						case "SHIFT012FCombo1":
							if (!String.IsNullOrEmpty(item.Value))
								Combo1Items.Add(item);
							break;
						case "SHIFT012FCombo2":
							if (!String.IsNullOrEmpty(item.Value))
								Combo2Items.Add(item);
							break;
						case "SHIFT012FCombo3":
							if (!String.IsNullOrEmpty(item.Value))
								Combo3Items.Add(item);
							break;
						case "SHIFT012FCombo4":
							if (!String.IsNullOrEmpty(item.Value))
								Combo4Items.Add(item);
							break;
						case "SHIFT012FCombo5":
							if (!String.IsNullOrEmpty(item.Value))
								Combo5Items.Add(item);
							break;
						case "SHIFT012FCombo6":
							if (!String.IsNullOrEmpty(item.Value))
								Combo6Items.Add(item);
							break;
						case "SHIFT012FCombo7":
							if (!String.IsNullOrEmpty(item.Value))
								Combo7Items.Add(item);
							break;
						case "SHIFT012FCombo8":
							if (!String.IsNullOrEmpty(item.Value))
								Combo8Items.Add(item);
							break;
						case "SHIFT12FSubheader1":
							if (item.IsPlaceholder)
								SubHeader1Placeholder = item.Value;
							else
								SubHeader1DefaultValue = item.Value;
							break;
					}
				}

				Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT012FClipart1");
				Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT012FClipart2");
				Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT012FClipart3");

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT012FHeader");

				Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012FCombo1");
				Combo2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012FCombo2");
				Combo3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012FCombo3");
				Combo4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012FCombo4");
				Combo5Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012FCombo5");
				Combo6Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012FCombo6");
				Combo7Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012FCombo7");
				Combo8Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012FCombo8");

				SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012FSubheader1");
			}

			if (resourceManager.DataProofOfPerformanceCommonFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataProofOfPerformanceCommonFile.LocalPath);

				var comboLists = new[]
				{
					Combo1Items,
					Combo2Items,
					Combo3Items,
					Combo4Items,
					Combo5Items,
					Combo6Items,
					Combo7Items,
					Combo8Items
				};

				var proofNodes = document.SelectNodes("//SHIFTProof/Item")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var proofNode in proofNodes)
				{
					var nodeValue = proofNode.Attributes?.OfType<XmlAttribute>()
						.FirstOrDefault(a => String.Equals(a.Name, "Description", StringComparison.OrdinalIgnoreCase))?.Value;
					if (!String.IsNullOrWhiteSpace(nodeValue))
						foreach (var comboList in comboLists)
							if (!comboList.Any(item => String.Equals(nodeValue, item.Value, StringComparison.OrdinalIgnoreCase)))
								comboList.Add(ListDataItem.FromString(nodeValue));
				}
			}
		}
	}
}