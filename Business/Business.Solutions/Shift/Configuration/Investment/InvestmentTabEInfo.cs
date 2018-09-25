using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration.CBC;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Investment
{
	public class InvestmentTabEInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab12_E_Clipart1;
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab12_E_Clipart2;
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public Image Clipart3Image => _resourceManager.GraphicResources?.Tab12_E_Clipart3;
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

		public InvestmentTabEInfo() : base(ShiftChildTabType.E, ShiftTopTabType.Investment)
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

			if (resourceManager.DataInvestmentPartEFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataInvestmentPartEFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT012E");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT012EHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
						case "SHIFT012ECombo1":
							if (!String.IsNullOrEmpty(item.Value))
								Combo1Items.Add(item);
							break;
						case "SHIFT012ECombo2":
							if (!String.IsNullOrEmpty(item.Value))
								Combo2Items.Add(item);
							break;
						case "SHIFT012ECombo3":
							if (!String.IsNullOrEmpty(item.Value))
								Combo3Items.Add(item);
							break;
						case "SHIFT012ECombo4":
							if (!String.IsNullOrEmpty(item.Value))
								Combo4Items.Add(item);
							break;
						case "SHIFT012ECombo5":
							if (!String.IsNullOrEmpty(item.Value))
								Combo5Items.Add(item);
							break;
						case "SHIFT012ECombo6":
							if (!String.IsNullOrEmpty(item.Value))
								Combo6Items.Add(item);
							break;
						case "SHIFT012ECombo7":
							if (!String.IsNullOrEmpty(item.Value))
								Combo7Items.Add(item);
							break;
						case "SHIFT012ECombo8":
							if (!String.IsNullOrEmpty(item.Value))
								Combo8Items.Add(item);
							break;
						case "SHIFT12ESubheader1":
							if (item.IsPlaceholder)
								SubHeader1Placeholder = item.Value;
							else
								SubHeader1DefaultValue = item.Value;
							break;
					}
				}

				Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT012EClipart1");
				Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT012EClipart2");
				Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT012EClipart3");

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT012EHeader");

				Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012ECombo1");
				Combo2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012ECombo2");
				Combo3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012ECombo3");
				Combo4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012ECombo4");
				Combo5Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012ECombo5");
				Combo6Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012ECombo6");
				Combo7Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012ECombo7");
				Combo8Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012ECombo8");

				SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT012ESubheader1");
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