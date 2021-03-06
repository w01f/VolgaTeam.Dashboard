﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Contract
{
	public class ContractTabCInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab15_C_Clipart1;
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab15_C_Clipart2;
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public Image Clipart3Image => _resourceManager.GraphicResources?.Tab15_C_Clipart3;
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

		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader1Configuration { get; set; }

		public string SubHeader2DefaultValue { get; private set; }
		public string SubHeader2Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader2Configuration { get; set; }

		public string SubHeader3DefaultValue { get; private set; }
		public string SubHeader3Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader3Configuration { get; set; }

		public string SubHeader4DefaultValue { get; private set; }
		public string SubHeader4Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader4Configuration { get; set; }

		public string SubHeader5DefaultValue { get; private set; }
		public string SubHeader5Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader5Configuration { get; set; }

		public string SubHeader6DefaultValue { get; private set; }
		public string SubHeader6Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader6Configuration { get; set; }

		public ContractTabCInfo() : base(ShiftChildTabType.C, ShiftTopTabType.Contract)
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

			SubHeader1Configuration = TextEditorConfiguration.Empty();
			SubHeader2Configuration = TextEditorConfiguration.Empty();
			SubHeader3Configuration = TextEditorConfiguration.Empty();
			SubHeader4Configuration = TextEditorConfiguration.Empty();
			SubHeader5Configuration = TextEditorConfiguration.Empty();
			SubHeader6Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (resourceManager.DataContractPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataContractPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT15C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT15CHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
						case "SHIFT15CCombo1":
							if (!String.IsNullOrEmpty(item.Value))
								Combo1Items.Add(item);
							break;
						case "SHIFT15CCombo2":
							if (!String.IsNullOrEmpty(item.Value))
								Combo2Items.Add(item);
							break;
						case "SHIFT15CCombo3":
							if (!String.IsNullOrEmpty(item.Value))
								Combo3Items.Add(item);
							break;
						case "SHIFT15CCombo4":
							if (!String.IsNullOrEmpty(item.Value))
								Combo4Items.Add(item);
							break;
						case "SHIFT15CCombo5":
							if (!String.IsNullOrEmpty(item.Value))
								Combo5Items.Add(item);
							break;
						case "SHIFT15CTAB1SHUBHEADER1":
							if (item.IsPlaceholder)
								SubHeader1Placeholder = item.Value;
							else
								SubHeader1DefaultValue = item.Value;
							break;
						case "SHIFT15CTAB2SHUBHEADER1":
							if (item.IsPlaceholder)
								SubHeader2Placeholder = item.Value;
							else
								SubHeader2DefaultValue = item.Value;
							break;
						case "SHIFT15CTAB3SHUBHEADER1":
							if (item.IsPlaceholder)
								SubHeader3Placeholder = item.Value;
							else
								SubHeader3DefaultValue = item.Value;
							break;
						case "SHIFT15CTAB4SHUBHEADER1":
							if (item.IsPlaceholder)
								SubHeader4Placeholder = item.Value;
							else
								SubHeader4DefaultValue = item.Value;
							break;
						case "SHIFT15CTAB5SHUBHEADER1":
							if (item.IsPlaceholder)
								SubHeader5Placeholder = item.Value;
							else
								SubHeader5DefaultValue = item.Value;
							break;
						case "SHIFT15CTAB6SHUBHEADER1":
							if (item.IsPlaceholder)
								SubHeader6Placeholder = item.Value;
							else
								SubHeader6DefaultValue = item.Value;
							break;
					}
				}

				Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT15CClipart1");
				Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT15CClipart2");
				Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT15CClipart3");

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT15CHeader");

				Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT15CCombo1");
				Combo2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT15CCombo2");
				Combo3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT15CCombo3");
				Combo4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT15CCombo4");
				Combo5Configuration = TextEditorConfiguration.FromXml(node, "SHIFT15CCombo5");

				SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT15CTAB1SHUBHEADER1");
				SubHeader2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT15CTAB2SHUBHEADER1");
				SubHeader3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT15CTAB3SHUBHEADER1");
				SubHeader4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT15CTAB4SHUBHEADER1");
				SubHeader5Configuration = TextEditorConfiguration.FromXml(node, "SHIFT15CTAB5SHUBHEADER1");
				SubHeader6Configuration = TextEditorConfiguration.FromXml(node, "SHIFT15CTAB6SHUBHEADER1");
			}

			if (resourceManager.DataNextStepsCommonFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataNextStepsCommonFile.LocalPath);

				var comboLists = new[]
				{
					Combo1Items,
					Combo2Items,
					Combo3Items,
					Combo4Items,
					Combo5Items
				};

				var proofNodes = document.SelectNodes("//SHIFTContract/Item")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
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
