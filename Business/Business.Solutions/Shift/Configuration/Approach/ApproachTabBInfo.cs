using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Approach
{
	public class ApproachTabBInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public Image Clipart2Image { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public Image Clipart3Image { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public List<ApproachItemInfo> ApproachItems { get; }

		public string Combo1Placeholder { get; private set; }
		public TextEditorConfiguration Combo1Configuration { get; set; }
		public string Combo2Placeholder { get; private set; }
		public TextEditorConfiguration Combo2Configuration { get; set; }
		public string Combo3Placeholder { get; private set; }
		public TextEditorConfiguration Combo3Configuration { get; set; }
		public string Combo4Placeholder { get; private set; }
		public TextEditorConfiguration Combo4Configuration { get; set; }

		public TextEditorConfiguration SubHeader1Configuration { get; set; }
		public TextEditorConfiguration SubHeader2Configuration { get; set; }
		public TextEditorConfiguration SubHeader3Configuration { get; set; }
		public TextEditorConfiguration SubHeader4Configuration { get; set; }

		public ApproachTabBInfo() : base(ShiftChildTabType.B, ShiftTopTabType.Approach)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();

			ApproachItems = new List<ApproachItemInfo>();

			Combo1Configuration = TextEditorConfiguration.Empty();
			Combo2Configuration = TextEditorConfiguration.Empty();
			Combo3Configuration = TextEditorConfiguration.Empty();
			Combo4Configuration = TextEditorConfiguration.Empty();

			SubHeader1Configuration = TextEditorConfiguration.Empty();
			SubHeader2Configuration = TextEditorConfiguration.Empty();
			SubHeader3Configuration = TextEditorConfiguration.Empty();
			SubHeader4Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			Clipart1Image = resourceManager.GraphicResources?.Tab9_B_Clipart1;
			Clipart2Image = resourceManager.GraphicResources?.Tab9_B_Clipart2;
			Clipart3Image = resourceManager.GraphicResources?.Tab9_B_Clipart3;

			if (resourceManager.DataApproachesCommonFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataApproachesCommonFile.LocalPath);

				var itemInfoNodes = document.SelectNodes("//OurApproach/Approach")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var itemInfoNode in itemInfoNodes)
					ApproachItems.Add(ApproachItemInfo.FromXml(itemInfoNode, resourceManager.ClipartTab15SubAFolder));
			}

			if (resourceManager.DataApproachPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataApproachPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT09B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT09BHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
						case "SHIFT09BCombo1":
							if (item.IsPlaceholder)
								Combo1Placeholder = item.Value;
							break;
						case "SHIFT09BCombo2":
							if (item.IsPlaceholder)
								Combo2Placeholder = item.Value;
							break;
						case "SHIFT09BCombo3":
							if (item.IsPlaceholder)
								Combo3Placeholder = item.Value;
							break;
						case "SHIFT09BCombo4":
							if (item.IsPlaceholder)
								Combo4Placeholder = item.Value;
							break;
					}
				}

				Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT09BClipart1");
				Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT09BClipart2");
				Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT09BClipart3");

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT09BHeader");

				Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT09BCombo1");
				Combo2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT09BCombo2");
				Combo3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT09BCombo3");
				Combo4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT09BCombo4");

				SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT09BLinkText1");
				SubHeader2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT09BLinkText2");
				SubHeader3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT09BLinkText3");
				SubHeader4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT09BLinkText4");
			}
		}
	}
}
