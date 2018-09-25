using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.NextSteps
{
	public class NextStepsTabCInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab14_C_Clipart1;
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab14_C_Clipart2;
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public List<ListDataItem> Combo1Items { get; }
		public TextEditorConfiguration Combo1Configuration { get; set; }

		public List<ListDataItem> Combo2Items { get; }
		public TextEditorConfiguration Combo2Configuration { get; set; }

		public List<ListDataItem> Combo3Items { get; }
		public TextEditorConfiguration Combo3Configuration { get; set; }

		public List<ListDataItem> Combo4Items { get; }
		public TextEditorConfiguration Combo4Configuration { get; set; }

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

		public NextStepsTabCInfo() : base(ShiftChildTabType.C, ShiftTopTabType.NextSteps)
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

			SubHeader1Configuration = TextEditorConfiguration.Empty();
			SubHeader2Configuration = TextEditorConfiguration.Empty();
			SubHeader3Configuration = TextEditorConfiguration.Empty();
			SubHeader4Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (!resourceManager.DataNextStepsPartCFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataNextStepsPartCFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT14C");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT14CHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT14CCombo1":
						if (!String.IsNullOrEmpty(item.Value))
							Combo1Items.Add(item);
						break;
					case "SHIFT14CCombo2":
						if (!String.IsNullOrEmpty(item.Value))
							Combo2Items.Add(item);
						break;
					case "SHIFT14CCombo3":
						if (!String.IsNullOrEmpty(item.Value))
							Combo3Items.Add(item);
						break;
					case "SHIFT14CCombo4":
						if (!String.IsNullOrEmpty(item.Value))
							Combo4Items.Add(item);
						break;
					case "SHIFT14CSubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
					case "SHIFT14CSubheader2":
						if (item.IsPlaceholder)
							SubHeader2Placeholder = item.Value;
						else
							SubHeader2DefaultValue = item.Value;
						break;
					case "SHIFT14CSubheader3":
						if (item.IsPlaceholder)
							SubHeader3Placeholder = item.Value;
						else
							SubHeader3DefaultValue = item.Value;
						break;
					case "SHIFT14CSubheader4":
						if (item.IsPlaceholder)
							SubHeader4Placeholder = item.Value;
						else
							SubHeader4DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT14CClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT14CClipart2");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT14CHeader");
			Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14CCombo1");
			Combo2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14CCombo2");
			Combo3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14CCombo3");
			Combo4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14CCombo4");
			SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14CSubheader1");
			SubHeader2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14CSubheader2");
			SubHeader3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14CSubheader3");
			SubHeader4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT14CSubheader4");
		}
	}
}
