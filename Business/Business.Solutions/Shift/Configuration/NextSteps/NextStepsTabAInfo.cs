using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.NextSteps
{
	public class NextStepsTabAInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab14_A_Clipart1;
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab14_A_Clipart2;
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public Image Clipart3Image => _resourceManager.GraphicResources?.Tab14_A_Clipart3;
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public List<ListDataItem> Combo1Items { get; }
		public TextEditorConfiguration Combo1Configuration { get; set; }

		public List<ListDataItem> Combo2Items { get; }
		public TextEditorConfiguration Combo2Configuration { get; set; }

		public List<ListDataItem> Combo3Items { get; }
		public TextEditorConfiguration Combo3Configuration { get; set; }

		public List<ListDataItem> Combo4Items { get; }
		public TextEditorConfiguration Combo4Configuration { get; set; }

		public NextStepsTabAInfo() : base(ShiftChildTabType.A, ShiftTopTabType.NextSteps)
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
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (resourceManager.DataNextStepsPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataNextStepsPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT14A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT14AHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
						case "SHIFT14ACombo1":
							if (!String.IsNullOrEmpty(item.Value))
								Combo1Items.Add(item);
							break;
						case "SHIFT14ACombo2":
							if (!String.IsNullOrEmpty(item.Value))
								Combo2Items.Add(item);
							break;
						case "SHIFT14ACombo3":
							if (!String.IsNullOrEmpty(item.Value))
								Combo3Items.Add(item);
							break;
						case "SHIFT14ACombo4":
							if (!String.IsNullOrEmpty(item.Value))
								Combo4Items.Add(item);
							break;
					}
				}

				Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT14AClipart1");
				Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT14AClipart2");
				Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT14AClipart3");

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT14AHeader");

				Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14ACombo1");
				Combo2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14ACombo2");
				Combo3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14ACombo3");
				Combo4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14ACombo4");
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
					Combo4Items
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